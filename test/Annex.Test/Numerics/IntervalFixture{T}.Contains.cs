namespace Annex.Test.Numerics;

using Annex.Numerics;
using AutoFixture;

public partial class IntervalFixture<T>
{
    public sealed class Contains : IntervalFixture<T>
    {
        [Theory]
        [AutoDomainData]
        public void WhenUnbounded(Generator<T> generator)
        {
            var values = CreateFiveSequential(generator, Comparer<T>.Default);
            var sut = Interval<T>.Unbounded;

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { true, true, true, true, true }),
                () => sut.Classification.ShouldBe(IntervalClassification.Unbounded));
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

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, false, false, false, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.Empty));
        }

        [Theory]
        [AutoDomainData]
        public void WhenDegenerate(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);

            // Mid value passed on purpose for test case
            var left = LeftIntervalEndpoint.Create(values[2], true);
            var right = RightIntervalEndpoint.Create(values[2], true);
            var sut = new Interval<T>(left, right, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, false, true, false, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.Degenerate));
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftOpen(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[1], false);
            var sut = new Interval<T>(left, RightIntervalEndpoint<T>.Unbounded, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, false, true, true, true }),
                () => sut.Classification.ShouldBe(IntervalClassification.LeftOpen));
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftClosed(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[1], true);
            var sut = new Interval<T>(left, RightIntervalEndpoint<T>.Unbounded, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, true, true, true, true }),
                () => sut.Classification.ShouldBe(IntervalClassification.LeftClosed));
        }

        [Theory]
        [AutoDomainData]
        public void WhenRightOpen(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var right = RightIntervalEndpoint.Create(values[3], false);
            var sut = new Interval<T>(LeftIntervalEndpoint<T>.Unbounded, right, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { true, true, true, false, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.RightOpen));
        }

        [Theory]
        [AutoDomainData]
        public void WhenRightClosed(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var right = RightIntervalEndpoint.Create(values[3], true);
            var sut = new Interval<T>(LeftIntervalEndpoint<T>.Unbounded, right, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { true, true, true, true, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.RightClosed));
        }

        [Theory]
        [AutoDomainData]
        public void WhenOpen(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[1], false);
            var right = RightIntervalEndpoint.Create(values[3], false);
            var sut = new Interval<T>(left, right, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, false, true, false, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.Open));
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftClosedRightOpen(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[1], true);
            var right = RightIntervalEndpoint.Create(values[3], false);
            var sut = new Interval<T>(left, right, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, true, true, false, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.LeftClosedRightOpen));
        }

        [Theory]
        [AutoDomainData]
        public void WhenLeftOpenRightClosed(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[1], false);
            var right = RightIntervalEndpoint.Create(values[3], true);
            var sut = new Interval<T>(left, right, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, false, true, true, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.LeftOpenRightClosed));
        }

        [Theory]
        [AutoDomainData]
        public void WhenClosed(Generator<T> generator)
        {
            var comparer = Comparer<T>.Default;
            var values = CreateFiveSequential(generator, comparer);
            var left = LeftIntervalEndpoint.Create(values[1], true);
            var right = RightIntervalEndpoint.Create(values[3], true);
            var sut = new Interval<T>(left, right, comparer);

            sut.ShouldSatisfyAllConditions(
                () => values
                    .Select(sut.Contains)
                    .ShouldBe(new[] { false, true, true, true, false }),
                () => sut.Classification.ShouldBe(IntervalClassification.Closed));
        }
    }
}
