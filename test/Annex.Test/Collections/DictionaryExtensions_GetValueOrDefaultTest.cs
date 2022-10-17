#if NETSTANDARD || NETFRAMEWORK
namespace Annex.Test.Collections;

using Annex.Collections;

public sealed class DictionaryExtensions_GetValueOrDefaultTest
{
    [Theory]
    [AutoDomainData]
    public void DefaultNullThisThrowsArgumentNullException(object key) =>
        Should.Throw<ArgumentNullException>(() =>
            default(IDictionary<object, object>?)!.GetValueOrDefault(key))
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void DefaultNullKeyThrowsArgumentNullException(IDictionary<object, object> sut) =>
        Should.Throw<ArgumentNullException>(() => sut.GetValueOrDefault(null!))
            .ParamName
            .ShouldBe("key");

    [Theory]
    [AutoDomainData]
    public void DefaultExistingKeyReturnsValue(object key, object value) =>
        new Dictionary<object, object> { { key, value } }
            .GetValueOrDefault(key)
            .ShouldBe(value);

    [Theory]
    [AutoDomainData]
    public void DefaultNoKeyReturnsDefaultValue(object key) =>
        new Dictionary<object, object>()
            .GetValueOrDefault(key)
            .ShouldBeNull();

    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(object key, object defaultValue) =>
        Should.Throw<ArgumentNullException>(() =>
            default(IDictionary<object, object>?)!.GetValueOrDefault(key, defaultValue))
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void NullKeyThrowsArgumentNullException(
        IDictionary<object, object> sut, object defaultValue) =>
        Should.Throw<ArgumentNullException>(() => sut.GetValueOrDefault(null!, defaultValue))
            .ParamName
            .ShouldBe("key");

    [Theory]
    [AutoDomainData]
    public void ExistingKeyReturnsValue(object key, object value, object defaultValue) =>
        new Dictionary<object, object> { { key, value } }
            .GetValueOrDefault(key, defaultValue)
            .ShouldBe(value);

    [Theory]
    [AutoDomainData]
    public void NoKeyReturnsDefaultValue(object key, object defaultValue) =>
        new Dictionary<object, object>()
            .GetValueOrDefault(key, defaultValue)
            .ShouldBe(defaultValue);
}
#endif
