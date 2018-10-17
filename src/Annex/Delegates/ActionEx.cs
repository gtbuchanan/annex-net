using JetBrains.Annotations;
using System;

namespace Annex.Delegates
{
    /// <summary>
    ///     Provides an additional set of static members for <see cref="Action"/>.
    /// </summary>
    [PublicAPI]
    public static class ActionEx
    {
        /// <summary>
        ///     An empty <see cref="Action"/> (i.e. NOP or No Operation).
        /// </summary>
        /// <see href="https://en.wikipedia.org/wiki/NOP">Wikipedia</see>
        /// <remarks>
        ///     This method is named 'Empty' instead of 'NoOp' to match other .NET representations
        ///     of NOP values, such as:
        ///     <see cref="System.Linq.Expressions.Expression.Empty"/>,
        ///     <see cref="System.Linq.Enumerable.Empty{T}"/>, and
        ///     Observable.Empty{T}.
        /// </remarks>
        public static Action Empty { get; } = () => { };
    }
}
