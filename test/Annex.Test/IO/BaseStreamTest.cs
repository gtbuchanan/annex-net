namespace Annex.Test.IO;

using System.Diagnostics.CodeAnalysis;
using Annex.IO;

public sealed class BaseStreamTest
{
    [Test]
    public void CanRead_ReturnsFalse()
    {
        using var sut = new TestStream();

        sut.CanRead.ShouldBeFalse();
    }

    [Test]
    public void CanWrite_ReturnsFalse()
    {
        using var sut = new TestStream();

        sut.CanWrite.ShouldBeFalse();
    }

    [Test]
    public void CanSeek_ReturnsFalse()
    {
        using var sut = new TestStream();

        sut.CanSeek.ShouldBeFalse();
    }

    [Test]
    public void Length_ThrowsNotSupportedException()
    {
        using var sut = new TestStream();

        Should.Throw<NotSupportedException>(() => sut.Length);
    }

    [Test]
    public void Position_ThrowsNotSupportedException()
    {
        using var sut = new TestStream();

        sut.ShouldSatisfyAllConditions(
            () => Should.Throw<NotSupportedException>(() => sut.Position),
            () => Should.Throw<NotSupportedException>(() => sut.Position = 0));
    }

    [Test]
    public void Read_ThrowsNotSupportedException()
    {
        using var sut = new TestStream();

        Should.Throw<NotSupportedException>(() => sut.Read(Array.Empty<byte>(), 0, 0));
    }

    [Test]
    public void Write_ThrowsNotSupportedException()
    {
        using var sut = new TestStream();

        Should.Throw<NotSupportedException>(() => sut.Write(Array.Empty<byte>(), 0, 0));
    }

    [Test]
    public void Seek_ThrowsNotSupportedException()
    {
        using var sut = new TestStream();

        Should.Throw<NotSupportedException>(() => sut.Seek(0, SeekOrigin.Begin));
    }

    [Test]
    public void SetLength_ThrowsNotSupportedException()
    {
        using var sut = new TestStream();

        Should.Throw<NotSupportedException>(() => sut.SetLength(0));
    }

    [Test]
    public void Flush_DoesNotThrow()
    {
        using var sut = new TestStream();

        Should.NotThrow(sut.Flush);
    }

    [ExcludeFromCodeCoverage]
    private sealed class TestStream : BaseStream
    {
    }
}
