namespace Annex.Test.Linq;

using System.Collections;
using Annex.Linq;

public sealed class EnumerableComparer_GetHashCodeTest
{
    [Test]
    public void Generic_NullObjReturnsZero() =>
        EnumerableComparer<object>.Default.GetHashCode(null).ShouldBe(0);

    [Theory]
    [AutoDomainData]
    public void Generic_ItemEquality(IEnumerable<object> value)
    {
        IEqualityComparer<IEnumerable<object>> sut = EnumerableComparer<object>.Default;
        sut.GetHashCode(value).ShouldBe(sut.GetHashCode(value.ToArray()));
    }

    [Test]
    public void NullObjReturnsZero()
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        sut.GetHashCode(null!).ShouldBe(0);
    }

    [Theory]
    [AutoDomainData]
    public void InvalidTypeObjThrowsArgumentException(object value)
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        Should.Throw<ArgumentException>(() => sut.GetHashCode(value))
            .ParamName
            .ShouldBe("obj");
    }

    [Theory]
    [AutoDomainData]
    public void ItemEquality(IEnumerable<object> value)
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;
        sut.GetHashCode(value).ShouldBe(sut.GetHashCode(value.ToArray()));
    }
}
