namespace Annex.IO;

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

/// <summary>
/// Writeable stream for using a separate thread in a producer/consumer scenario to reduce memory
/// saturation.
/// </summary>
/// <remarks>
/// Each instance of this stream should only have one consumer.
/// </remarks>
/// <seealso href="https://github.com/dotnet/samples/blob/main/csharp/parallel/ParallelExtensionsExtras/CoordinationDataStructures/TransferStream.cs">
/// Adapted from Microsoft ParallelExtensionsExtras with Read support added
/// </seealso>
/// <example>
/// Assuming <c>data</c> is a lazy <see cref="IEnumerable{T}"/> in each of the examples below, the
/// data is processed and forwarded when received rather than loading all of the data into memory.
///
/// When you have a method that returns a writable stream:
/// <code language="csharp">
/// var request = (FtpWebRequest)WebRequest.Create("ftp://www.contoso.com/test.csv");
/// request.Method = WebRequestMethods.Ftp.UploadFile;
/// request.Credentials = new NetworkCredential("anonymous", "jane.doe@contoso.com");
/// using var requestStream = request.GetRequestStream()
/// using var stream = new TransferStream(requestStream);
/// using var streamWriter = new StreamWriter(stream) { AutoFlush = true };
/// using var csvWriter = new CsvWriter(streamWriter); // CsvHelper package
/// csvWriter.WriteRecords(data);
/// </code>
///
/// When you have a method that accepts the stream as an argument:
/// <code language="csharp">
/// using var sftp = new SftpClient("sftp.contoso.com", "jane.doe", "password"); // SSH.NET package
/// sftp.Connect();
/// using var stream = new TransferStream();
/// using var streamWriter = new StreamWriter(stream) { AutoFlush = true };
/// using var csvWriter = new CsvWriter(streamWriter); // CsvHelper package
/// var task = Task.Run(() => sftp.UploadFile(stream, "test.csv"));
/// csvWriter.WriteRecords(data);
/// stream.CompleteWriting(); // Necessary to avoid `await task` hanging
/// await task;
/// </code>
///
/// Exception handling would need to be added to handle deletion of partial files being written to
/// the output in an error condition. It would also be better to name the file with a different
/// extension while uploading (e.g. tmp) and rename it to the true extension when finished.
/// </example>
[PublicAPI]
public sealed class TransferStream : BaseStream
{
    private readonly BlockingCollection<byte[]> chunks;

    private readonly Stream? inner;

    private readonly Task? writeTask;

    private readonly IEnumerator<byte[]>? enumerator;

    private byte[] buffer = Array.Empty<byte>();

    private int offset;

    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransferStream"/> class.
    /// </summary>
    /// <remarks>
    /// You must call <see cref="CompleteWriting"/> to signal when writing has been completed or the
    /// reader of the stream will hang indefinitely.
    /// </remarks>
    public TransferStream()
    {
        this.chunks = new BlockingCollection<byte[]>();
        this.enumerator = this.chunks.GetConsumingEnumerable().GetEnumerator();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransferStream"/> class.
    /// </summary>
    /// <param name="writableStream">The writeable stream.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="writableStream"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="writableStream"/> is not writeable.
    /// </exception>
    public TransferStream(Stream writableStream)
    {
        if (!writableStream.CanWrite)
            throw new ArgumentException("Target stream is not writeable.", nameof(writableStream));

        this.chunks = new BlockingCollection<byte[]>();
        this.inner = writableStream;
        this.writeTask = Task.Factory.StartNew(this.WriteChunks, TaskCreationOptions.LongRunning);
    }

    /// <inheritdoc />
    public override bool CanRead => this.enumerator != null;

    /// <inheritdoc />
    public override bool CanWrite => true;

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        if (!this.CanRead)
            throw new NotSupportedException();
        if (offset < 0 || offset >= buffer.Length)
            throw new ArgumentOutOfRangeException(nameof(offset), offset, null);
        if (count < 0 || offset + count > buffer.Length)
            throw new ArgumentOutOfRangeException(nameof(count), count, null);

        var bufferCount = 0;
        var bufferRemaining = this.buffer.Length - this.offset;
        if (bufferRemaining > 0)
        {
            bufferCount = Math.Min(bufferRemaining, count);
            Buffer.BlockCopy(this.buffer, this.offset, buffer, offset, bufferCount);
            this.offset += bufferCount;
        }

        if (bufferCount == count || !this.enumerator!.MoveNext())
            return bufferCount;

        this.offset = 0;
        this.buffer = this.enumerator.Current;

        // Recursively dequeue independent buffers until desired count is read or we're finished
        // writing and the queue is empty.
        return bufferCount + this.Read(buffer, offset + bufferCount, count - bufferCount);
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        if (offset < 0 || offset >= buffer.Length)
            throw new ArgumentOutOfRangeException(nameof(offset), offset, null);
        if (count < 0 || offset + count > buffer.Length)
            throw new ArgumentOutOfRangeException(nameof(count), count, null);
        if (count == 0) return;

        var chunk = new byte[count];
        Buffer.BlockCopy(buffer, offset, chunk, 0, count);
        this.chunks.Add(chunk);
    }

    /// <summary>
    /// Signals that writing has completed.
    /// </summary>
    /// <remarks>
    /// This only needs to be called when a writable stream was not provided during initialization.
    /// If a writable stream was provided, the completion will occur automatically during disposal.
    /// </remarks>
    public void CompleteWriting() => this.chunks.CompleteAdding();

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (this.disposed) return;

        base.Dispose(disposing);

        if (disposing)
        {
            // Complete the collection and wait for the consumer to process all of the data.
            this.chunks.CompleteAdding();
            this.writeTask?.Wait();
            this.enumerator?.Dispose();
            this.chunks.Dispose();
        }

        this.disposed = true;
    }

    private void WriteChunks()
    {
        foreach (var chunk in this.chunks.GetConsumingEnumerable())
            this.inner!.Write(chunk, 0, chunk.Length);
    }
}
