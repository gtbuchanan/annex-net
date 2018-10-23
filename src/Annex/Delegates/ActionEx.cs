using JetBrains.Annotations;
using System;

namespace Annex.Delegates
{
    /// <summary>
    /// Provides an additional set of static members for <see cref="Action"/>.
    /// </summary>
    [PublicAPI]
    public static class ActionEx
    {
        /// <summary>
        /// An empty <see cref="Action"/> (i.e. NOP or No Operation).
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// public void SomeMethodWithAForcedCallback(Action callback) { ... }
        ///
        /// SomeMethodWithAForcedCallback(ActionEx.Empty);
        /// </code>
        /// </example>
        /// <remarks>
        /// This method is named 'Empty' instead of 'NoOp' to match other .NET representations
        /// of NOP values, such as:
        /// <see cref="System.Linq.Expressions.Expression.Empty"/>,
        /// <see cref="System.Linq.Enumerable.Empty{T}"/>, and
        /// Observable.Empty{T}.
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/NOP_(code)">Wikipedia: NOP</seealso>
        [NotNull]
        public static Action Empty { get; } = () => { };
    }
}
