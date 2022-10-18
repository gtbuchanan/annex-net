namespace Annex.Security;

using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

/// <summary>
/// Represents a cryptographically secure random number generator.
/// </summary>
/// <example>
/// <code language="csharp">
/// using (var cryptoRandom = new CryptoRandom())
///     Enumerable.Range(1, 100).Shuffle(cryptoRandom).ToArray(); // Cryptographically random shuffle
/// </code>
/// </example>
/// <seealso href="https://learn.microsoft.com/en-us/archive/msdn-magazine/2007/september/net-matters-tales-from-the-cryptorandom">
/// Adapted from MSDN Magazine September 2007 "Tales from the CryptoRandom"
/// </seealso>
[PublicAPI]
public sealed class CryptoRandom : Random, IDisposable
{
    private const int BufferSize = 4;

    private const int Mask = 0x7FFFFFFF;

    private readonly RandomNumberGenerator generator;

    private readonly byte[] buffer = new byte[BufferSize];

    private readonly bool dispose;

    /// <summary>
    /// Initializes a new instance of the <see cref="CryptoRandom"/> class.
    /// Creates a new instance of the <see cref="CryptoRandom"/> class with the specified <see cref="RandomNumberGenerator"/>.
    /// </summary>
    /// <param name="generator">The source random number generator.</param>
    /// <exception cref="ArgumentNullException"><paramref name="generator"/> is <c>null</c>.</exception>
    public CryptoRandom(RandomNumberGenerator generator) =>
        this.generator = generator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CryptoRandom"/> class.
    /// Creates a new instance of the <see cref="CryptoRandom"/> class. Defaults to <see cref="RandomNumberGenerator.Create()"/>.
    /// </summary>
    public CryptoRandom()
        : this(RandomNumberGenerator.Create()) =>
        this.dispose = true;

    /// <inheritdoc />
    public override int Next()
    {
        this.generator.GetBytes(this.buffer);
        return BitConverter.ToInt32(this.buffer, 0) & Mask;
    }

    /// <inheritdoc />
    public override int Next(int maxValue) =>
        maxValue < 0
            ? throw new ArgumentOutOfRangeException(nameof(maxValue))
            : this.Next(0, maxValue);

    /// <inheritdoc />
    public override int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue)
            throw new ArgumentOutOfRangeException(nameof(minValue));

        if (minValue == maxValue)
            return minValue;

        long diff = maxValue - minValue;

        while (true)
        {
            this.generator.GetBytes(this.buffer);
            var rand = BitConverter.ToUInt32(this.buffer, 0);

            const long Max = 1 + (long)uint.MaxValue;
            var remainder = Max % diff;
            if (rand < Max - remainder)
                return (int)(minValue + (rand % diff));
        }
    }

    /// <inheritdoc />
    public override double NextDouble()
    {
        this.generator.GetBytes(this.buffer);
        var rand = BitConverter.ToUInt32(this.buffer, 0);
        return rand / (1.0 + uint.MaxValue);
    }

    /// <inheritdoc />
    public override void NextBytes(byte[] buffer) =>
        this.generator.GetBytes(buffer);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage] // Unable to test disposal of private field
    public void Dispose()
    {
        if (this.dispose)
            this.generator.Dispose();
    }
}
