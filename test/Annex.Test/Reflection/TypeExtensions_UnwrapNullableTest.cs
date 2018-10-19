using Annex.Reflection;
using NUnit.Framework;
using Shouldly;
using System;

namespace Annex.Test.Reflection
{
    public sealed class TypeExtensions_UnwrapNullableTest
    {
        [Test]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => default(Type).UnwrapNullable())
                .ParamName.ShouldBe("this");

        [Test]
        public void UnwrapsNullable() =>
            typeof(int?).UnwrapNullable().ShouldBe(typeof(int));

        [Test]
        public void PassthroughNonNullable() =>
            typeof(object).UnwrapNullable().ShouldBe(typeof(object));
    }
}
