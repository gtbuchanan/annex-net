using Shouldly;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Annex.Reactive.Test
{
    [ExcludeFromCodeCoverage]
    internal static class ShouldlyExtensions
    {
        [Pure]
        public static void ShouldBe<T>(
            [JetBrains.Annotations.NotNull] this IEnumerable<T> @this,
            [JetBrains.Annotations.NotNull] params T[] args) =>
            @this.ShouldBe(args.AsEnumerable());
    }
}
