namespace Annex.Test.Linq;

using Annex.Linq;

public sealed class EnumerableComparer_DefaultTest
{
    [Test]
    public void IsSingleton() => EnumerableComparer<object>.Default
        .ShouldBeSameAs(EnumerableComparer<object>.Default);
}
