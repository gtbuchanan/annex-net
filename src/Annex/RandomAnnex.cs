#if !NET6_0_OR_GREATER
namespace Annex;

using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

/// <summary>
/// Represents a pseudo-random number generator, which is an algorithm that produces a sequence of
/// numbers that meet certain statistical requirements for randomness.
/// </summary>
public static class RandomAnnex
{
    /// <summary>
    /// Gets a thread-safe <see cref="Random"/> instance that may be used concurrently from any
    /// thread.
    /// </summary>
    /// <remarks>
    /// This is a polyfill for
    /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.random.shared?view=net-6.0">
    /// Random.Shared</see> from .NET 6.
    /// </remarks>
    public static Random Shared { get; } = new ThreadSafeRandom();

    /// <inheritdoc />
    [SuppressMessage(
        "Security",
        "CA5394:Do not use insecure randomness",
        Justification = "Intentional re-implementation of base Random class for polyfill.")]
    private sealed class ThreadSafeRandom : Random
    {
        private const int BufferSize = 4;

        private static readonly RandomNumberGenerator Generator =
            RandomNumberGenerator.Create();

        private readonly ThreadLocal<Random> local = new(() =>
        {
            var buffer = new byte[BufferSize];
            Generator.GetBytes(buffer);
            return new Random(BitConverter.ToInt32(buffer, 0));
        });

        private Random Inner => this.local.Value!;

        /// <inheritdoc />
        public override int Next() => this.Inner.Next();

        /// <inheritdoc />
        public override int Next(int maxValue) => this.Inner.Next(maxValue);

        /// <inheritdoc />
        public override int Next(int minValue, int maxValue) => this.Inner.Next(minValue, maxValue);

        /// <inheritdoc />
        public override double NextDouble() => this.Inner.NextDouble();

        /// <inheritdoc />
        public override void NextBytes(byte[] buffer) => this.Inner.NextBytes(buffer);
    }
}
#endif
