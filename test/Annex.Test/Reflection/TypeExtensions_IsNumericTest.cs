using Annex.Numerics;
using Annex.Reflection;
using EnumsNET;
using MoreLinq.Extensions;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;

namespace Annex.Test.Reflection
{
    public sealed class TypeExtensions_IsNumericTest
    {
        private static readonly Type[] NonNumericTestTypes = { typeof(object), typeof(int?), typeof(DateTime) };

        public static TestCaseData[] DefaultCases =
            NumericEx.TypeMap.Keys
                .Select(t => new TestCaseData(t) { ExpectedResult = true })
                .Concat(NonNumericTestTypes.Select(t => new TestCaseData(t) { ExpectedResult = false }))
                .ToArray();

        public static TestCaseData[] ClassCases =
            NumericEx.TypeMap // Numerics right class
                .Select(kvp => new TestCaseData(kvp.Key, kvp.Value) { ExpectedResult = true })
                .Concat(NumericEx.TypeMap
                    .SelectMany(kvp => Enums // Numerics wrong class
                        .GetValues<NumericClass>(EnumMemberSelection.Flags)
                        .Where(nc => nc != kvp.Value)
                        .Select(nc => (kvp.Key, nc)))
                    .Concat(NonNumericTestTypes // Non-numerics
                        .Cartesian(Enums.GetValues<NumericClass>(), ValueTuple.Create))
                    .Select(t => new TestCaseData(t.Item1, t.Item2) { ExpectedResult = false }))
                .ToArray();

        [Test]
        public void DefaultNullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => default(Type).IsNumeric())
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public void NullThisThrowsArgumentNullException(NumericClass numericClass) =>
            Should.Throw<ArgumentNullException>(() => default(Type).IsNumeric(numericClass))
                .ParamName.ShouldBe("this");

        [Test]
        public void InvalidNumericClassThrowsArgumentOutOfRangeException()
        {
            var numericClass = (NumericClass)byte.MaxValue;

            var ex = Should.Throw<ArgumentOutOfRangeException>(() =>
                typeof(object).IsNumeric(numericClass));

            ex.ShouldSatisfyAllConditions(
                () => ex.ParamName.ShouldBe("numericClass"),
                () => ex.ActualValue.ShouldBe(numericClass));
        }

        [TestCaseSource(nameof(DefaultCases))]
        public bool DefaultsToAllNumericClasses(Type type) => type.IsNumeric();

        [TestCaseSource(nameof(ClassCases))]
        public bool ReturnsExpectedResult(Type type, NumericClass numericClass) => type.IsNumeric(numericClass);
    }
}
