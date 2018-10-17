using Annex.Delegates;
using NUnit.Framework;
using Shouldly;

namespace Annex.Test.Delegates
{
    public sealed class ActionEx_EmptyTest
    {
        [Test]
        public void IsSingleton() => ActionEx.Empty.ShouldBeSameAs(ActionEx.Empty);
    }
}
