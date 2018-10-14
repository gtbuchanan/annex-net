using JetBrains.Annotations;
using Shouldly;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Annex.Reactive.Test
{
    [ExcludeFromCodeCoverage]
    internal static class ShouldlyExtensions
    {
        [Pure]
        public static void ShouldBe<T>([NotNull]this IEnumerable<T> @this, [NotNull]params T[] args) =>
            @this.ShouldBe(args.AsEnumerable());
    }
}
