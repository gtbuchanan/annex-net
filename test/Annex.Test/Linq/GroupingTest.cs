using Annex.Linq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Annex.Test.Linq
{
    public sealed class GroupingTest
    {
        [Test, AutoDomainData]
        public void Constructor_NullKeyDoesNotThrow(IEnumerable<object> values) =>
            Should.NotThrow(() => new Grouping<object, object>(null, values));

        [Test, AutoDomainData]
        public void Constructor_NullValuesDoesNotThrow(object key) =>
            Should.NotThrow(() => new Grouping<object, object>(key, null));

        [Test, AutoDomainData]
        public void Key_ReturnsKey(object key, IEnumerable<object> values) =>
            new Grouping<object, object>(key, values).Key.ShouldBe(key);

        [Test, AutoDomainData]
        public void GetEnumerator_NullValuesReturnsEmpty(object key) =>
            new Grouping<object, object>(key, null).AsEnumerable().ShouldBeEmpty();

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void GetEnumerator_ReturnsValues(object key, IEnumerable<object> values) =>
            new Grouping<object, object>(key, values).AsEnumerable().ShouldBe(values);

        public static TestCaseData[] Empty_HasKeyWithNoValuesCases =
        {
            new TestCaseData(typeof(object), null),
            new TestCaseData(typeof(object), new object()),
            new TestCaseData(typeof(int), null),
            new TestCaseData(typeof(int), int.MaxValue),
            new TestCaseData(typeof(string), null),
            new TestCaseData(typeof(string), Guid.NewGuid().ToString()),
            new TestCaseData(typeof(Guid), null),
            new TestCaseData(typeof(Guid), Guid.NewGuid())
        };

        [TestCaseSource(nameof(Empty_HasKeyWithNoValuesCases))]
        public void Empty_HasKeyWithNoValues(Type keyType, object expectedKey)
        {
            // ReSharper disable once PossibleNullReferenceException
            var sut = typeof(Grouping)
                .GetMethod(nameof(Grouping.Empty), BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(keyType, typeof(object))
                .Invoke(null, new[] { expectedKey ?? Type.Missing });
            if (expectedKey == null && keyType.IsValueType)
                expectedKey = Activator.CreateInstance(keyType);
            ShouldBeEmptyGroup((dynamic)sut, (dynamic)expectedKey);
        }

        private static void ShouldBeEmptyGroup<TKey, TElement>(IGrouping<TKey, TElement> grouping, TKey expectedKey) =>
            grouping.ShouldSatisfyAllConditions(
                () => grouping.Key.ShouldBe(expectedKey),
                () => grouping.AsEnumerable().ShouldBeEmpty());

        [Test, AutoDomainData]
        public void Create_NullKeyDoesNotThrow(IEnumerable<object> values) =>
            Should.NotThrow(() => Grouping.Create((object)null, values));

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Create_NullValuesThrowsArgumentNullException(object key) =>
            Should.Throw<ArgumentNullException>(() => Grouping.Create<object, object>(key, null));

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Create_ReturnsGrouping(object key, IEnumerable<object> values)
        {
            var sut = Grouping.Create(key, values);

            var grouping = sut.ShouldBeOfType<Grouping<object, object>>();
            grouping.ShouldSatisfyAllConditions(
                () => grouping.Key.ShouldBe(key),
                () => grouping.AsEnumerable().ShouldBe(values));
        }
    }
}
