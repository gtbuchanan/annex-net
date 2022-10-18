namespace Annex.Test.Time.Pollution;

using Annex.Time.Pollution;

public sealed class TimeSpanExtensionsTest
{
    [Theory]
    [AutoDomainData]
    public void Ticks(int sut) => sut.Ticks().ShouldBe(TimeSpan.FromTicks(sut));

    [Theory]
    [AutoDomainData]
    public void Milliseconds(int sut) =>
        sut.Milliseconds().ShouldBe(TimeSpan.FromMilliseconds(sut));

    [Theory]
    [AutoDomainData]
    public void Seconds(int sut) => sut.Seconds().ShouldBe(TimeSpan.FromSeconds(sut));

    [Theory]
    [AutoDomainData]
    public void Minutes(int sut) => sut.Minutes().ShouldBe(TimeSpan.FromMinutes(sut));

    [Theory]
    [AutoDomainData]
    public void Hours(int sut) => sut.Hours().ShouldBe(TimeSpan.FromHours(sut));

    [Theory]
    [AutoDomainData]
    public void Days(int sut) => sut.Days().ShouldBe(TimeSpan.FromDays(sut));
}
