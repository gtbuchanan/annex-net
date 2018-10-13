using Annex.Linq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Annex.Test.Linq
{
    public sealed class LookupExtensions_ToDictionaryTest
    {
        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => ((ILookup<object, object>)null).ToDictionary());

        [Test, AutoDomainData]
        public void ReturnsMatchingDictionary(IDictionary<object, IEnumerable<object>> expected)
        {
            var lookup = expected
                .SelectMany(kvp => kvp.Value, Tuple.Create)
                .ToLookup(x => x.Item1.Key, x => x.Item2);

            var sut = lookup.ToDictionary();

            // TODO: Use Shouldly when it compares dictionaries correctly
            Assert.That(sut, Is.EqualTo(expected));
        }
    }
}
