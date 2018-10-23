using JetBrains.Annotations;
using System;

namespace Annex.Reflection
{
    public static partial class TypeExtensions
    {
        /// <summary>
        /// Unwraps a nullable to the underlying type.
        /// </summary>
        /// <param name="this">The source type.</param>
        /// <returns>The underlying type if nullable, otherwise the source type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
        /// <example>
        /// <code language="csharp">
        /// typeof(int?).UnwrapNullable(); // int
        /// typeof(DateTime?).UnwrapNullable(); // DateTime
        /// typeof(object).UnwrapNullable(); // object
        /// </code>
        /// </example>
        public static Type UnwrapNullable([NotNull] this Type @this) =>
            Nullable.GetUnderlyingType(@this) ?? @this;
    }
}
