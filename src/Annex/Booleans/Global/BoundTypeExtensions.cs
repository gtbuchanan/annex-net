using System.Collections.Generic;

namespace Annex.Booleans.Global
{
    internal static class BoundTypeExtensions
    {
        private static readonly HashSet<BoundType> Excludes = new HashSet<BoundType>
        {
            BoundType.Exclude,
            BoundType.ExcludeInclude,
            BoundType.IncludeExclude
        };

        public static bool IsExclude(this BoundType @this) => Excludes.Contains(@this);
    }
}
