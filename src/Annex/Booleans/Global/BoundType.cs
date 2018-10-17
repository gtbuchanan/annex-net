namespace Annex.Booleans.Global
{
    /// <summary>
    ///     Represents the type of range bounds.
    /// </summary>
    public enum BoundType
    {
        /// <summary>
        ///     Inclusive low and high bounds.
        /// </summary>
        Include,

        /// <summary>
        ///     Exclusive low and high bounds.
        /// </summary>
        Exclude,

        /// <summary>
        ///     Inclusive low and exclusive high bounds.
        /// </summary>
        IncludeExclude,

        /// <summary>
        ///     Exclusive low and inclusive high bounds.
        /// </summary>
        ExcludeInclude
    }
}
