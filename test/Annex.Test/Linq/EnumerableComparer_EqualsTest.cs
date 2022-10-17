#if NET6_0_OR_GREATER
#pragma warning disable RCS1249
#endif

namespace Annex.Test.Linq;

using System.Collections;
using Annex.Linq;

public sealed class EnumerableComparer_EqualsTest
{
    [Test]
    public void Generic_NullEquality()
    {
        IEqualityComparer<IEnumerable<object>> sut = EnumerableComparer<object>.Default;

        sut.Equals(null!, null!).ShouldBeTrue();
    }

    [Theory]
    [AutoDomainData]
    public void Generic_SingleNull1(IEnumerable<object> value)
    {
        IEqualityComparer<IEnumerable<object>> sut = EnumerableComparer<object>.Default;

        sut.Equals(value, null!).ShouldBeFalse();
    }

    [Theory]
    [AutoDomainData]
    public void Generic_SingleNull2(IEnumerable<object> value)
    {
        IEqualityComparer<IEnumerable<object>> sut = EnumerableComparer<object>.Default;

        sut.Equals(null!, value).ShouldBeFalse();
    }

    [Theory]
    [AutoDomainData]
    public void Generic_ReferenceEquality(IEnumerable<object> value)
    {
        IEqualityComparer<IEnumerable<object>> sut = EnumerableComparer<object>.Default;

        sut.Equals(value, value).ShouldBeTrue();
    }

    [Theory]
    [AutoDomainData]
    public void Generic_ItemEquality(IEnumerable<object> value)
    {
        IEqualityComparer<IEnumerable<object>> sut = EnumerableComparer<object>.Default;

        sut.Equals(value.ToArray(), value.ToList()).ShouldBeTrue();
    }

    [Test]
    public void NullEquality()
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        sut.Equals(null, null).ShouldBeTrue();
    }

    [Theory]
    [AutoDomainData]
    public void SingleNull1(object value)
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        sut.Equals(value, null).ShouldBeFalse();
    }

    [Theory]
    [AutoDomainData]
    public void SingleNull2(object value)
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        sut.Equals(null, value).ShouldBeFalse();
    }

    [Theory]
    [AutoDomainData]
    public void InvalidTypeThrowsArgumentException(object value)
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        Should.Throw<ArgumentException>(() => sut.Equals(value, value))
            .ParamName
            .ShouldBe("x");
    }

    [Theory]
    [AutoDomainData]
    public void ReferenceEquality(IEnumerable<object> value)
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        sut.Equals(value, value).ShouldBeTrue();
    }

    [Theory]
    [AutoDomainData]
    public void ItemEquality(IEnumerable<object> value)
    {
        IEqualityComparer sut = EnumerableComparer<object>.Default;

        sut.Equals(value.ToArray(), value.ToList()).ShouldBeTrue();
    }
}
