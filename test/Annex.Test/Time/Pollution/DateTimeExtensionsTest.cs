namespace Annex.Test.Time.Pollution;

using System.Linq;
using Annex.Time.Pollution;

public sealed class DateTimeExtensionsTest
{
    private const int MinYear = 1900;

    private const int MaxYear = 2100;

    [Theory]
    [AutoDomainData]
    public void January(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 31);
        var year = g.First(IsValidYear);

        day.January(year).ShouldBe(new DateTime(year, 1, day));
    }

    [Theory]
    [AutoDomainData]
    public void February(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 28);
        var year = g.First(IsValidYear);

        day.February(year).ShouldBe(new DateTime(year, 2, day));
    }

    [Theory]
    [AutoDomainData]
    public void March(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 31);
        var year = g.First(IsValidYear);

        day.March(year).ShouldBe(new DateTime(year, 3, day));
    }

    [Theory]
    [AutoDomainData]
    public void April(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 30);
        var year = g.First(IsValidYear);

        day.April(year).ShouldBe(new DateTime(year, 4, day));
    }

    [Theory]
    [AutoDomainData]
    public void May(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 31);
        var year = g.First(IsValidYear);

        day.May(year).ShouldBe(new DateTime(year, 5, day));
    }

    [Theory]
    [AutoDomainData]
    public void June(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 30);
        var year = g.First(IsValidYear);

        day.June(year).ShouldBe(new DateTime(year, 6, day));
    }

    [Theory]
    [AutoDomainData]
    public void July(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 31);
        var year = g.First(IsValidYear);

        day.July(year).ShouldBe(new DateTime(year, 7, day));
    }

    [Theory]
    [AutoDomainData]
    public void August(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 31);
        var year = g.First(IsValidYear);

        day.August(year).ShouldBe(new DateTime(year, 8, day));
    }

    [Theory]
    [AutoDomainData]
    public void September(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 30);
        var year = g.First(IsValidYear);

        day.September(year).ShouldBe(new DateTime(year, 9, day));
    }

    [Theory]
    [AutoDomainData]
    public void October(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 31);
        var year = g.First(IsValidYear);

        day.October(year).ShouldBe(new DateTime(year, 10, day));
    }

    [Theory]
    [AutoDomainData]
    public void November(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 30);
        var year = g.First(IsValidYear);

        day.November(year).ShouldBe(new DateTime(year, 11, day));
    }

    [Theory]
    [AutoDomainData]
    public void December(Generator<int> g)
    {
        var day = g.First(n => n is >= 1 and <= 31);
        var year = g.First(IsValidYear);

        day.December(year).ShouldBe(new DateTime(year, 12, day));
    }

    private static bool IsValidYear(int n) => n is >= MinYear and <= MaxYear;
}
