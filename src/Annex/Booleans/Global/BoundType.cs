using JetBrains.Annotations;

namespace Annex.Booleans.Global
{
    /// <summary>
    /// Represents a type of range bounds.
    /// </summary>
    [PublicAPI]
    public enum BoundType
    {
        /// <summary>
        /// Inclusive low and high bounds.
        /// </summary>
        Include,

        /// <summary>
        /// Exclusive low and high bounds.
        /// </summary>
        Exclude,

        /// <summary>
        /// Inclusive low and exclusive high bounds.
        /// </summary>
        IncludeExclude,

        /// <summary>
        /// Exclusive low and inclusive high bounds.
        /// </summary>
        ExcludeInclude
    }
}
