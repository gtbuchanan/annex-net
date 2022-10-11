using JetBrains.Annotations;
using System;
using System.Security.Cryptography;

namespace Annex.Security
{
    /// <summary>
    /// Represents a cryptographically secure random number generator.
    /// </summary>
    /// <example>
    /// <code language="csharp">
    /// using (var cryptoRandom = new CryptoRandom())
    ///     Enumerable.Range(1, 100).Shuffle(cryptoRandom).ToArray(); // Cryptographically random shuffle
    /// </code>
    /// </example>
    /// <seealso href="http://download.microsoft.com/download/3/A/7/3A7FA450-1F33-41F7-9E6D-3AA95B5A6AEA/MSDNMagazineSeptember2007en-us.chm">
    /// Adapted from MSDN Magazine September 2007 &quot;Tales from the CryptoRandom&quot;
    /// </seealso>
    /// <seealso href="http://download.microsoft.com/download/f/2/7/f279e71e-efb0-4155-873d-5554a0608523/NETMatters2007_09.exe">
    /// Tales from the CryptoRandom Source
    /// </seealso>
    [PublicAPI]
    public sealed class CryptoRandom : Random, IDisposable
    {
        private RandomNumberGenerator Generator { get; }

        private byte[] Buffer { get; } = new byte[4];

        private IDisposable Disposable { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="CryptoRandom"/> class. Defaults to <see cref="RandomNumberGenerator.Create()"/>.
        /// </summary>
        public CryptoRandom() : this(RandomNumberGenerator.Create()) => Disposable = Generator;

        /// <summary>
        /// Creates a new instance of the <see cref="CryptoRandom"/> class with the specified <see cref="RandomNumberGenerator"/>
        /// </summary>
        /// <param name="generator">The source random number generator.</param>
        /// <exception cref="ArgumentNullException"><paramref name="generator"/> is <c>null</c>.</exception>
        public CryptoRandom([NotNull] RandomNumberGenerator generator) => Generator = generator;

        /// <inheritdoc />
        public override int Next()
        {
            Generator.GetBytes(Buffer);
            return BitConverter.ToInt32(Buffer, 0) & 0x7FFFFFFF;
        }

        /// <inheritdoc />
        public override int Next(int maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            return Next(0, maxValue);
        }

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
                Generator.GetBytes(Buffer);
                var rand = BitConverter.ToUInt32(Buffer, 0);

                const long MAX = 1 + (long)uint.MaxValue;
                var remainder = MAX % diff;
                if (rand < MAX - remainder)
                    return (int)(minValue + rand % diff);
            }
        }

        /// <inheritdoc />
        public override double NextDouble()
        {
            Generator.GetBytes(Buffer);
            var rand = BitConverter.ToUInt32(Buffer, 0);
            return rand / (1.0 + uint.MaxValue);
        }

        /// <inheritdoc />
        public override void NextBytes(byte[] buffer) => Generator.GetBytes(buffer);

        /// <inheritdoc />
        public void Dispose() => Disposable?.Dispose();
    }
}
