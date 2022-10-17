namespace Annex.Test.Reflection;

using Annex.Numerics;
using Annex.Reflection;
using EnumsNET;

public sealed class TypeExtensions_IsNumericTest
{
    public static readonly Type[] NonNumericTestTypes =
    {
        typeof(object),
        typeof(int?),
        typeof(DateTime),
    };

    public static readonly object?[] DefaultCases =
        NumericAnnex.TypeMap.Keys
            .Select(t => new object?[] { t, true })
            .Concat(NonNumericTestTypes
                .Select(t => new object?[] { t, false }))
            .ToArray();

    public static readonly object?[] ClassCases =
        NumericAnnex.TypeMap // Numerics right class
            .Select(kvp => new object?[] { kvp.Key, kvp.Value, true })
            .Concat(NumericAnnex.TypeMap
                .SelectMany(kvp => Enums // Numerics wrong class
                    .GetValues<NumericClass>(EnumMemberSelection.Flags)
                    .Where(nc => nc != kvp.Value)
                    .Select(nc => (kvp.Key, nc)))
                .Concat(// Non-numerics
                    from t in NonNumericTestTypes
                    from v in Enums.GetValues<NumericClass>()
                    select (t, v))
                .Select(t => new object?[] { t.Item1, t.Item2, false }))
            .ToArray();

    [Test]
    public void DefaultNullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => default(Type?)!.IsNumeric())
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(NumericClass numericClass) =>
        Should.Throw<ArgumentNullException>(() => default(Type?)!.IsNumeric(numericClass))
            .ParamName
            .ShouldBe("this");

    [Test]
    public void InvalidNumericClassThrowsArgumentOutOfRangeException()
    {
        const NumericClass numericClass = (NumericClass)byte.MaxValue;

        var ex = Should.Throw<ArgumentOutOfRangeException>(() =>
            typeof(object).IsNumeric(numericClass));

        ex.ShouldSatisfyAllConditions(
            () => ex.ParamName.ShouldBe("numericClass"),
            () => ex.ActualValue.ShouldBe(numericClass));
    }

    [Theory]
    [TestCaseSource(nameof(DefaultCases))]
    public void DefaultsToAllNumericClasses(Type type, bool expected) =>
        type.IsNumeric().ShouldBe(expected);

    [Theory]
    [TestCaseSource(nameof(ClassCases))]
    public void ReturnsExpectedResult(
        Type type, NumericClass numericClass, bool expected) =>
        type.IsNumeric(numericClass).ShouldBe(expected);
}
