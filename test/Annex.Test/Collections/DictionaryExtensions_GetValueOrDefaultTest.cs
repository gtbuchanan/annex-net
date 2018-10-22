#if NETSTANDARD || NETFRAMEWORK
using Annex.Collections;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace Annex.Test.Collections
{
    public sealed class DictionaryExtensions_GetValueOrDefaultTest
    {
        [Test, AutoDomainData]
        public void DefaultNullThisThrowsArgumentNullException(object key) =>
            Should.Throw<ArgumentNullException>(() =>
                default(IDictionary<object, object>).GetValueOrDefault(key))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public void DefaultNullKeyThrowsArgumentNullException(IDictionary<object, object> sut) =>
            Should.Throw<ArgumentNullException>(() => sut.GetValueOrDefault(null))
                .ParamName.ShouldBe("key");

        [Test, AutoDomainData]
        public void DefaultExistingKeyReturnsValue(object key, object value) =>
            new Dictionary<object, object> { { key, value } }
                .GetValueOrDefault(key)
                .ShouldBe(value);

        [Test, AutoDomainData]
        public void DefaultNoKeyReturnsDefaultValue(object key) =>
            new Dictionary<object, object>()
                .GetValueOrDefault(key)
                .ShouldBeNull();

        [Test, AutoDomainData]
        public void NullThisThrowsArgumentNullException(object key, object defaultValue) =>
            Should.Throw<ArgumentNullException>(() =>
                default(IDictionary<object, object>).GetValueOrDefault(key, defaultValue))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public void NullKeyThrowsArgumentNullException(IDictionary<object, object> sut, object defaultValue) =>
            Should.Throw<ArgumentNullException>(() => sut.GetValueOrDefault(null, defaultValue))
                .ParamName.ShouldBe("key");

        [Test, AutoDomainData]
        public void ExistingKeyReturnsValue(object key, object value, object defaultValue) =>
            new Dictionary<object, object> { { key, value } }
                .GetValueOrDefault(key, defaultValue)
                .ShouldBe(value);

        [Test, AutoDomainData]
        public void NoKeyReturnsDefaultValue(object key, object defaultValue) =>
            new Dictionary<object, object>()
                .GetValueOrDefault(key, defaultValue)
                .ShouldBe(defaultValue);
    }
}
#endif
