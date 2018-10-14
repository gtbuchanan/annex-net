using Annex.Linq;
using NUnit.Framework;
using Shouldly;

namespace Annex.Test.Linq
{
    public sealed class EnumerableComparer_DefaultTest
    {
        [Test]
        public void IsSingleton() => EnumerableComparer<object>.Default
            .ShouldBeSameAs(EnumerableComparer<object>.Default);
    }
}
