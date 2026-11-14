namespace Annex.Test.Time.Pollution;

using System.Linq;
using Annex.Numerics;
using Annex.Time.Pollution;

public sealed class DateTimeExtensionsTest
{
    private static readonly Interval<int> YearInterval = Interval.Create(1900, 2100);

    private static readonly Interval<int> Day28Interval = Interval.Create(1, 28);

    private static readonly Interval<int> Day30Interval = Interval.Create(1, 30);

    private static readonly Interval<int> Day31Interval = Interval.Create(1, 31);

    [Theory]
    [AutoDomainData]
    public void January(Generator<int> g)
    {
        var day = g.First(Day31Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.January(year).ShouldBe(new DateTime(year, 1, day));
    }

    [Theory]
    [AutoDomainData]
    public void February(Generator<int> g)
    {
        var day = g.First(Day28Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.February(year).ShouldBe(new DateTime(year, 2, day));
    }

    [Theory]
    [AutoDomainData]
    public void March(Generator<int> g)
    {
        var day = g.First(Day31Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.March(year).ShouldBe(new DateTime(year, 3, day));
    }

    [Theory]
    [AutoDomainData]
    public void April(Generator<int> g)
    {
        var day = g.First(Day30Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.April(year).ShouldBe(new DateTime(year, 4, day));
    }

    [Theory]
    [AutoDomainData]
    public void May(Generator<int> g)
    {
        var day = g.First(Day31Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.May(year).ShouldBe(new DateTime(year, 5, day));
    }

    [Theory]
    [AutoDomainData]
    public void June(Generator<int> g)
    {
        var day = g.First(Day30Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.June(year).ShouldBe(new DateTime(year, 6, day));
    }

    [Theory]
    [AutoDomainData]
    public void July(Generator<int> g)
    {
        var day = g.First(Day31Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.July(year).ShouldBe(new DateTime(year, 7, day));
    }

    [Theory]
    [AutoDomainData]
    public void August(Generator<int> g)
    {
        var day = g.First(Day31Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.August(year).ShouldBe(new DateTime(year, 8, day));
    }

    [Theory]
    [AutoDomainData]
    public void September(Generator<int> g)
    {
        var day = g.First(Day30Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.September(year).ShouldBe(new DateTime(year, 9, day));
    }

    [Theory]
    [AutoDomainData]
    public void October(Generator<int> g)
    {
        var day = g.First(Day31Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.October(year).ShouldBe(new DateTime(year, 10, day));
    }

    [Theory]
    [AutoDomainData]
    public void November(Generator<int> g)
    {
        var day = g.First(Day30Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.November(year).ShouldBe(new DateTime(year, 11, day));
    }

    [Theory]
    [AutoDomainData]
    public void December(Generator<int> g)
    {
        var day = g.First(Day31Interval.Contains);
        var year = g.First(YearInterval.Contains);

        day.December(year).ShouldBe(new DateTime(year, 12, day));
    }
}
