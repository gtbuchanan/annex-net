namespace Annex.Test.Numerics;

using System.Globalization;
using Annex.Numerics;
using AutoFixture;

public partial class IntervalFixture<T>
{
    public sealed new class ToString : IntervalFixture<T>
    {
        [Test]
        public void InvalidFormatThrowsFormatException()
        {
            var sut = Interval<T>.Unbounded;

            Should.Throw<FormatException>(() => sut.ToString("X", null));
        }

        [Test]
        public void WhenUnbounded()
        {
            var sut = Interval<T>.Unbounded;

            sut.ToString().ShouldBe("(-∞, +∞)");
        }

        [Test]
        public void WhenUnboundedCommaCulture()
        {
            var sut = Interval<T>.Unbounded;

            sut.ToString(null, CultureInfo.GetCultureInfo("fr-FR")).ShouldBe("(-∞; +∞)");
        }

        [Test]
        public void WhenUnboundedUnambiguous()
        {
            var sut = Interval<T>.Unbounded;

            sut.ToString("U", CultureInfo.InvariantCulture).ShouldBe("]-∞, +∞[");
        }

        [Test]
        public void WhenUnboundedUnambiguousCommaCulture()
        {
            var sut = Interval<T>.Unbounded;

            sut.ToString("U", CultureInfo.GetCultureInfo("fr-FR")).ShouldBe("]-∞; +∞[");
        }

        [Theory]
        [AutoDomainData]
        public void WhenEmpty(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);

            // Values switched on purpose for test case
            var left = LeftIntervalEndpoint.Create(values[3], false);
            var right = RightIntervalEndpoint.Create(values[1], false);
            var sut = new Interval<T>(left, right, comparer);

            sut.ToString().ShouldBe($"({values[3]}, {values[1]})");
        }

        [Theory]
        [AutoDomainData]
        public void WhenDegenerate(T value)
        {
            var comparer = Comparer<T>.Default;
            var left = LeftIntervalEndpoint.Create(value, true);
            var right = RightIntervalEndpoint.Create(value, true);
            var sut = new Interval<T>(left, right, comparer);

            sut.ToString().ShouldBe($"[{value}, {value}]");
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftOpen(T leftValue)
        {
            var comparer = Comparer<T>.Default;
            var left = LeftIntervalEndpoint.Create(leftValue, false);
            var sut = new Interval<T>(left, RightIntervalEndpoint<T>.Unbounded, comparer);

            sut.ToString().ShouldBe($"({leftValue}, +∞)");
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftClosed(T leftValue)
        {
            var comparer = Comparer<T>.Default;
            var left = LeftIntervalEndpoint.Create(leftValue, true);
            var sut = new Interval<T>(left, RightIntervalEndpoint<T>.Unbounded, comparer);

            sut.ToString().ShouldBe($"[{leftValue}, +∞)");
        }

        [Theory]
        [AutoDomainData]
        public void WhenRightOpen(T rightValue)
        {
            var comparer = Comparer<T>.Default;
            var right = RightIntervalEndpoint.Create(rightValue, false);
            var sut = new Interval<T>(LeftIntervalEndpoint<T>.Unbounded, right, comparer);

            sut.ToString().ShouldBe($"(-∞, {rightValue})");
        }

        [Theory]
        [AutoDomainData]
        public void WhenRightClosed(T rightValue)
        {
            var comparer = Comparer<T>.Default;
            var right = RightIntervalEndpoint.Create(rightValue, true);
            var sut = new Interval<T>(LeftIntervalEndpoint<T>.Unbounded, right, comparer);

            sut.ToString().ShouldBe($"(-∞, {rightValue}]");
        }

        [Theory]
        [AutoDomainData]
        public void WhenOpen(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[0], false);
            var right = RightIntervalEndpoint.Create(values[1], false);
            var sut = new Interval<T>(left, right, comparer);

            sut.ToString().ShouldBe($"({values[0]}, {values[1]})");
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftClosedRightOpen(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[0], true);
            var right = RightIntervalEndpoint.Create(values[1], false);
            var sut = new Interval<T>(left, right, comparer);

            sut.ToString().ShouldBe($"[{values[0]}, {values[1]})");
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftOpenRightClosed(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[0], false);
            var right = RightIntervalEndpoint.Create(values[1], true);
            var sut = new Interval<T>(left, right, comparer);

            sut.ToString().ShouldBe($"({values[0]}, {values[1]}]");
        }

        [Theory]
        [AutoDomainData]
        public void WhenClosed(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var sut = new Interval<T>(values[0], values[1]);

            sut.ToString().ShouldBe($"[{values[0]}, {values[1]}]");
        }
    }
}
