namespace Annex.Test.Numerics;

using Annex.Numerics;
using AutoFixture;

[TestFixture(typeof(int))]
[TestFixture(typeof(double))]
[TestFixture(typeof(decimal))]
[TestFixture(typeof(DateTimeOffset))]
[Category("Interval")]
public partial class IntervalFixture<T>
{
    private static IList<T> CreateFiveSequential(
        Generator<T> generator, IComparer<T> comparer)
    {
        const int ValueLimit = 5;
        var list = generator.Take(ValueLimit).ToList();
        list.Sort(comparer);
        return list;
    }

    public sealed class Unbounded : IntervalFixture<T>
    {
        [Test]
        public void IsSingleton()
        {
            var sut = Interval<T>.Unbounded;

            sut.ShouldBe(Interval<T>.Unbounded);
        }

        [Test]
        public void IsUnbounded()
        {
            var sut = Interval<T>.Unbounded;

            sut.ShouldBe(new Interval<T>(
                LeftIntervalEndpoint<T>.Unbounded,
                RightIntervalEndpoint<T>.Unbounded,
                Comparer<T>.Default));
        }
    }
}
