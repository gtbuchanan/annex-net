namespace Annex.Test.IO;

using Annex.IO;

public sealed class TransferStreamTest
{
    [Test]
    public void Constructor_NullWritableStream_ThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => new TransferStream(null!))
            .ParamName
            .ShouldBe("writableStream");

    [Test]
    public void Constructor_NotWritableStream_ThrowsArgumentNullException()
    {
        using var stream = Substitute.For<Stream>();
        stream.CanWrite.Returns(false);
        Should.Throw<ArgumentException>(() => new TransferStream(stream))
            .ParamName
            .ShouldBe("writableStream");
    }

    [Test]
    public void CanRead_ReturnsTrue()
    {
        using var sut = new TransferStream();

        sut.CanRead.ShouldBeTrue();
    }

    [Test]
    public void CanRead_WritableStream_ReturnsFalse()
    {
        using var ms = new MemoryStream();
        using var sut = new TransferStream(ms);

        sut.CanRead.ShouldBeFalse();
    }

    [Test]
    public void CanWrite_ReturnsTrue()
    {
        using var sut = new TransferStream();

        sut.CanWrite.ShouldBeTrue();
    }

    [Test]
    public void CanWrite_WritableStream_ReturnsTrue()
    {
        using var ms = new MemoryStream();
        using var sut = new TransferStream(ms);

        sut.CanWrite.ShouldBeTrue();
    }

    [Test]
    public void Read_NullBuffer_ThrowsArgumentNullException()
    {
        using var sut = new TransferStream();

        Should.Throw<ArgumentNullException>(() => sut.Read(null!, 0, 0))
            .ParamName
            .ShouldBe("buffer");
    }

    [Theory]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void Read_Offset_ThrowsArgumentOutOfRangeException(int offset)
    {
        using var sut = new TransferStream();

        var ex = Should.Throw<ArgumentOutOfRangeException>(
            () => sut.Read(Array.Empty<byte>(), offset, 0));

        ex.ShouldSatisfyAllConditions(
            () => ex.ParamName.ShouldBe("offset"),
            () => ex.ActualValue.ShouldBe(offset));
    }

    [Theory]
    [TestCase(-1)]
    [TestCase(2)]
    public void Read_Count_ThrowsArgumentOutOfRangeException(int count)
    {
        using var sut = new TransferStream();

        var ex = Should.Throw<ArgumentOutOfRangeException>(
            () => sut.Read(new byte[1], 0, count));

        ex.ShouldSatisfyAllConditions(
            () => ex.ParamName.ShouldBe("count"),
            () => ex.ActualValue.ShouldBe(count));
    }

    [Test]
    public void Read_WritableStream_ThrowsNotSupportedException()
    {
        using var ms = new MemoryStream();
        using var sut = new TransferStream(ms);

        Should.Throw<NotSupportedException>(() => sut.Read(Array.Empty<byte>(), 0, 0));
    }

    [Test]
    public void Write_NullBuffer_ThrowsArgumentNullException()
    {
        using var sut = new TransferStream();

        Should.Throw<ArgumentNullException>(() => sut.Write(null!, 0, 0))
            .ParamName
            .ShouldBe("buffer");
    }

    [Theory]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void Write_WritableStream_Offset_ThrowsArgumentOutOfRangeException(int offset)
    {
        using var ms = new MemoryStream();
        using var sut = new TransferStream(ms);

        var ex = Should.Throw<ArgumentOutOfRangeException>(
            () => sut.Write(Array.Empty<byte>(), offset, 0));

        ex.ShouldSatisfyAllConditions(
            () => ex.ParamName.ShouldBe("offset"),
            () => ex.ActualValue.ShouldBe(offset));
    }

    [Theory]
    [TestCase(-1)]
    [TestCase(2)]
    public void Write_WritableStream_Count_ThrowsArgumentOutOfRangeException(int count)
    {
        using var ms = new MemoryStream();
        using var sut = new TransferStream(ms);

        var ex = Should.Throw<ArgumentOutOfRangeException>(
            () => sut.Write(new byte[1], 0, count));

        ex.ShouldSatisfyAllConditions(
            () => ex.ParamName.ShouldBe("count"),
            () => ex.ActualValue.ShouldBe(count));
    }

    [Theory]
    [AutoDomainData]
    public void Write_WritableStream_CountZero_WritesNothing(byte[] buffer)
    {
        using var stream = Substitute.For<Stream>();
        stream.CanWrite.Returns(true);
        using var sut = new TransferStream(stream);

        sut.Write(buffer, 0, 0);

        stream.DidNotReceive().Write(Arg.Any<byte[]>(), Arg.Any<int>(), Arg.Any<int>());
    }

    [Theory]
    [AutoDomainData]
    public void Write_WritableStream_AutoFlushes(Generator<byte> byteGenerator)
    {
        using var countdown = new CountdownEvent(2);
        var calls = new List<byte[]>(2);
        using var stream = Substitute.For<Stream>();
        stream.CanWrite.Returns(true);
        stream
            .When(s => s.Write(Arg.Any<byte[]>(), Arg.Any<int>(), Arg.Any<int>()))
            .Do(ci =>
            {
                calls.Add(ci.ArgAt<byte[]>(0));
                countdown.Signal();
            });
        using var sut = new TransferStream(stream);
        var buffer = byteGenerator.Take(100).ToArray();

        sut.Write(buffer, 0, 50);
        sut.Write(buffer, 50, 50);
        countdown.Wait(1_000); // Limit should never be reached

        calls.ShouldBe(new List<byte[]>
        {
            buffer.Take(50).ToArray(),
            buffer.Skip(50).ToArray(),
        });
    }

    [Theory]
    [AutoDomainData]
    public void MultipleSmallWritesFillsSingleLargeReadBuffer(Generator<byte> byteGenerator)
    {
        var buffer = new byte[10];
        using var sut = new TransferStream();
        var writeBuffer1 = byteGenerator.Take(5).ToArray();
        sut.Write(writeBuffer1, 0, writeBuffer1.Length);
        var writeBuffer2 = byteGenerator.Take(5).ToArray();
        sut.Write(writeBuffer2, 0, writeBuffer2.Length);

        sut.Read(buffer, 0, buffer.Length);

        buffer.ShouldBe(writeBuffer1.Concat(writeBuffer2).ToArray());
    }

    [Theory]
    [AutoDomainData]
    public void SingleLargeWriteFillsMultipleSmallReadBuffers(Generator<byte> byteGenerator)
    {
        var buffer1 = new byte[5];
        var buffer2 = new byte[5];
        using var sut = new TransferStream();
        var writeBuffer = byteGenerator.Take(10).ToArray();
        sut.Write(writeBuffer, 0, writeBuffer.Length);

        sut.Read(buffer1, 0, buffer1.Length);
        sut.Read(buffer2, 0, buffer2.Length);

        buffer1.ShouldBe(writeBuffer.Take(5).ToArray());
        buffer2.ShouldBe(writeBuffer.Skip(5).ToArray());
    }

    [Test]
    [Timeout(1_000)]
    public void CompleteWriting_FreesRead()
    {
        using var sut = new TransferStream();
        var task = Task.Run(() => sut.Read(new byte[10], 0, 10));
        sut.CompleteWriting();
        task.Wait();
    }

    [Test]
    public void Dispose_DoesNotDisposeWritableStream()
    {
        using var stream = Substitute.For<Stream>();
        stream.CanWrite.Returns(true);
        using var sut = new TransferStream(stream);

        sut.Dispose();
        ((IDisposable)stream).DidNotReceive().Dispose();
    }
}
