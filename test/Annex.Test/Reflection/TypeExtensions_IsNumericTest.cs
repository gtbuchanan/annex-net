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
                    .GetValues<NumericTypeOption>(EnumMemberSelection.Flags)
                    .Where(nc => nc != kvp.Value)
                    .Select(nc => (kvp.Key, nc)))
                .Concat(// Non-numerics
                    from t in NonNumericTestTypes
                    from v in Enums.GetValues<NumericTypeOption>()
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
    public void NullThisThrowsArgumentNullException(NumericTypeOption option) =>
        Should.Throw<ArgumentNullException>(() => default(Type?)!.IsNumeric(option))
            .ParamName
            .ShouldBe("this");

    [Test]
    public void InvalidOptionThrowsArgumentOutOfRangeException()
    {
        const NumericTypeOption option = (NumericTypeOption)byte.MaxValue;

        var ex = Should.Throw<ArgumentOutOfRangeException>(() =>
            typeof(object).IsNumeric(option));

        ex.ShouldSatisfyAllConditions(
            () => ex.ParamName.ShouldBe("option"),
            () => ex.ActualValue.ShouldBe(option));
    }

    [Theory]
    [TestCaseSource(nameof(DefaultCases))]
    public void DefaultsToAllOptions(Type type, bool expected) =>
        type.IsNumeric().ShouldBe(expected);

    [Theory]
    [TestCaseSource(nameof(ClassCases))]
    public void ReturnsExpectedResult(
        Type type, NumericTypeOption option, bool expected) =>
        type.IsNumeric(option).ShouldBe(expected);
}
