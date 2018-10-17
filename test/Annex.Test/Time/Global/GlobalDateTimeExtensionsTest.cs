using Annex.Booleans.Global;
using Annex.Time.Global;
using AutoFixture;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;

namespace Annex.Test.Time.Global
{
    public sealed class GlobalDateTimeExtensionsTest
    {
        [Test, AutoDomainData]
        public void January(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 31));
            var year = g.First(n => n.InRange(1900, 2100));

            day.January(year).ShouldBe(new DateTime(year, 1, day));
        }

        [Test, AutoDomainData]
        public void February(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 28));
            var year = g.First(n => n.InRange(1900, 2100));

            day.February(year).ShouldBe(new DateTime(year, 2, day));
        }

        [Test, AutoDomainData]
        public void March(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 31));
            var year = g.First(n => n.InRange(1900, 2100));

            day.March(year).ShouldBe(new DateTime(year, 3, day));
        }

        [Test, AutoDomainData]
        public void April(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 20));
            var year = g.First(n => n.InRange(1900, 2100));

            day.April(year).ShouldBe(new DateTime(year, 4, day));
        }

        [Test, AutoDomainData]
        public void May(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 31));
            var year = g.First(n => n.InRange(1900, 2100));

            day.May(year).ShouldBe(new DateTime(year, 5, day));
        }

        [Test, AutoDomainData]
        public void June(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 30));
            var year = g.First(n => n.InRange(1900, 2100));

            day.June(year).ShouldBe(new DateTime(year, 6, day));
        }

        [Test, AutoDomainData]
        public void July(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 31));
            var year = g.First(n => n.InRange(1900, 2100));

            day.July(year).ShouldBe(new DateTime(year, 7, day));
        }

        [Test, AutoDomainData]
        public void August(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 31));
            var year = g.First(n => n.InRange(1900, 2100));

            day.August(year).ShouldBe(new DateTime(year, 8, day));
        }

        [Test, AutoDomainData]
        public void September(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 30));
            var year = g.First(n => n.InRange(1900, 2100));

            day.September(year).ShouldBe(new DateTime(year, 9, day));
        }

        [Test, AutoDomainData]
        public void October(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 31));
            var year = g.First(n => n.InRange(1900, 2100));

            day.October(year).ShouldBe(new DateTime(year, 10, day));
        }

        [Test, AutoDomainData]
        public void November(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 30));
            var year = g.First(n => n.InRange(1900, 2100));

            day.November(year).ShouldBe(new DateTime(year, 11, day));
        }

        [Test, AutoDomainData]
        public void December(Generator<int> g)
        {
            var day = g.First(n => n.InRange(1, 31));
            var year = g.First(n => n.InRange(1900, 2100));

            day.December(year).ShouldBe(new DateTime(year, 12, day));
        }
    }
}
