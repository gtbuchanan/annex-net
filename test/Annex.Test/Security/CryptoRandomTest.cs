namespace Annex.Test.Security;

using System.Security.Cryptography;
using Annex.Security;
using AutoFixture.AutoNSubstitute;

public sealed class CryptoRandomTest
{
    [Test]
    public void Constructor_NullGeneratorThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => new CryptoRandom(null!))
            .ParamName
            .ShouldBe("generator");

    [Theory]
    [AutoDomainData]
    public void Next_ReturnsDerivedValue([Substitute] RandomNumberGenerator rng)
    {
        rng.GetBytes(Arg.Do<byte[]>(buffer =>
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = 1;
        }));

        using var sut = new CryptoRandom(rng);

        sut.Next().ShouldBe(16843009);
    }

    [Theory]
    [AutoDomainData]
    public void Next1_NegativeMaxValueThrowsArgumentOutOfRangeException(CryptoRandom sut) =>
        Should.Throw<ArgumentOutOfRangeException>(() => sut.Next(-1))
            .ParamName
            .ShouldBe("maxValue");

    [Theory]
    [AutoDomainData]
    public void Next1_ReturnsDerivedValue([Substitute] RandomNumberGenerator rng)
    {
        rng.GetBytes(Arg.Do<byte[]>(buffer =>
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = 1;
        }));

        using var sut = new CryptoRandom(rng);

        sut.Next(10).ShouldBe(9);
    }

    [Theory]
    [AutoDomainData]
    public void Next1_LessThanMaxValue(CryptoRandom sut, int maxValue) =>
        sut.Next(maxValue).ShouldBeLessThan(maxValue);

    [Theory]
    [AutoDomainData]
    public void Next2_MinValueGreaterThanMaxValueThrowsArgumentOutOfRangeException(
        CryptoRandom sut,
        int maxValue,
        Generator<int> g) =>
        Should.Throw<ArgumentOutOfRangeException>(
            () => sut.Next(g.First(n => n > maxValue), maxValue))
            .ParamName
            .ShouldBe("minValue");

    [Theory]
    [AutoDomainData]
    public void Next2_SameMinMaxReturnsMinValue(CryptoRandom sut, int value) =>
        sut.Next(value, value).ShouldBe(value);

    [Theory]
    [AutoDomainData]
    public void Next2_SameMinMaxReturnsMinValue(CryptoRandom sut, Generator<int> g)
    {
        var minValue = g.First();
        var maxValue = g.First(n => n > minValue);
        sut.Next(minValue, maxValue).ShouldBeInRange(minValue, maxValue);
    }

    [Theory]
    [AutoDomainData]
    public void Next2_ReturnsDerivedValue([Substitute] RandomNumberGenerator rng)
    {
        rng.GetBytes(Arg.Do<byte[]>(buffer =>
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = 1;
        }));

        using var sut = new CryptoRandom(rng);

        sut.Next(1, 10).ShouldBe(5);
    }

    [Theory]
    [AutoDomainData]
    public void NextDouble_ReturnsDerivedValue([Substitute] RandomNumberGenerator rng)
    {
        rng.GetBytes(Arg.Do<byte[]>(buffer =>
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = 1;
        }));

        using var sut = new CryptoRandom(rng);

        sut.NextDouble().ShouldBe(0.003921568626537919d);
    }

    [Theory]
    [AutoDomainData]
    public void NextBytes_NullBufferThrowsArgumentNullException(CryptoRandom sut) =>
        Should.Throw<ArgumentNullException>(() => sut.NextBytes(null!));

    [Theory]
    [AutoDomainData]
    public void NextBytes_FillsDerivedValues([Substitute] RandomNumberGenerator rng)
    {
        rng.GetBytes(Arg.Do<byte[]>(buffer =>
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = (byte)i;
        }));

        var buffer = new byte[4];
        using var sut = new CryptoRandom(rng);

        sut.NextBytes(buffer);

        buffer.ShouldBe(new byte[] { 0, 1, 2, 3 });
    }

    [Theory]
    [AutoDomainData]
    public void Dispose_DoesNotDisposeProvidedGenerator([Substitute] RandomNumberGenerator rng)
    {
        var sut = new CryptoRandom(rng);

        sut.Dispose();

        ((IDisposable)rng).DidNotReceive().Dispose();
    }
}
