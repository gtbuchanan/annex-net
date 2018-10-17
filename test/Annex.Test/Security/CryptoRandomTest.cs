using Annex.Security;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;

namespace Annex.Test.Security
{
    public sealed class CryptoRandomTest
    {
        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Constructor_NullGeneratorThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => new CryptoRandom(null))
                .ParamName.ShouldBe("generator");

        [Test]
        public void Constructor_DefaultUsesRNGCryptoServiceProvider() =>
            new CryptoRandom().Generator.ShouldBeOfType<RNGCryptoServiceProvider>();

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void Next_NegativeMaxValueThrowsArgumentOutOfRangeException(CryptoRandom sut) =>
            Should.Throw<ArgumentOutOfRangeException>(() => sut.Next(-1))
                .ParamName.ShouldBe("maxValue");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void Next_MinValueGreaterThanMaxValueThrowsArgumentOutOfRangeException(CryptoRandom sut, int maxValue, Generator<int> g) =>
            Should.Throw<ArgumentOutOfRangeException>(() => sut.Next(g.First(n => n > maxValue), maxValue))
                .ParamName.ShouldBe("minValue");

        [Test, AutoDomainData]
        public void Next_SameMinMaxReturnsMinValue(CryptoRandom sut, int value) =>
            sut.Next(value, value).ShouldBe(value);

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NextBytes_NullBufferThrowsArgumentNullException(CryptoRandom sut) =>
            Should.Throw<ArgumentNullException>(() => sut.NextBytes(null));

        [Test, AutoDomainData]
        public void Dispose_DoesNotDisposeProvidedGenerator([Substitute]RandomNumberGenerator rng)
        {
            var sut = new CryptoRandom(rng);

            sut.Dispose();

            rng.DidNotReceive().Dispose();
        }
    }
}
