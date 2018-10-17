using Annex.Time.Global;
using NUnit.Framework;
using Shouldly;
using System;

namespace Annex.Test.Time.Global
{
    public sealed class GlobalTimeSpanExtensionsTest
    {
        [Test, AutoDomainData]
        public void Ticks(int sut) => sut.Ticks().ShouldBe(TimeSpan.FromTicks(sut));

        [Test, AutoDomainData]
        public void Milliseconds(int sut) => sut.Milliseconds().ShouldBe(TimeSpan.FromMilliseconds(sut));

        [Test, AutoDomainData]
        public void Seconds(int sut) => sut.Seconds().ShouldBe(TimeSpan.FromSeconds(sut));

        [Test, AutoDomainData]
        public void Minutes(int sut) => sut.Minutes().ShouldBe(TimeSpan.FromMinutes(sut));

        [Test, AutoDomainData]
        public void Hours(int sut) => sut.Hours().ShouldBe(TimeSpan.FromHours(sut));

        [Test, AutoDomainData]
        public void Days(int sut) => sut.Days().ShouldBe(TimeSpan.FromDays(sut));
    }
}
