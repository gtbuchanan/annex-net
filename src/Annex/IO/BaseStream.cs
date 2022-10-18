namespace Annex.IO;

/// <summary>
/// Base stream class that implements all of Stream's abstract members.
/// </summary>
/// <seealso href="https://github.com/dotnet/samples/blob/main/csharp/parallel/ParallelExtensionsExtras/CoordinationDataStructures/AbstractStreamBase.cs">
/// Adapted from Microsoft ParallelExtensionsExtras
/// </seealso>
[PublicAPI]
public abstract class BaseStream : Stream
{
    /// <inheritdoc />
    public override bool CanRead => false;

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override bool CanSeek => false;

    /// <inheritdoc />
    public override long Length => throw new NotSupportedException();

    /// <inheritdoc />
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override void Flush()
    {
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException();

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) =>
        throw new NotSupportedException();

    /// <inheritdoc />
    public override void SetLength(long value) => throw new NotSupportedException();

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException();
}
