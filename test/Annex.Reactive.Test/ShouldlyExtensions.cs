namespace Annex.Reactive.Test;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal static class ShouldlyExtensions
{
    [Pure]
    public static void ShouldBe<T>(
        this IEnumerable<T> @this, params T[] args) =>
        @this.ShouldBe(args.AsEnumerable());
}
