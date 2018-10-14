using Annex.Reactive.Linq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Annex.Reactive.Test.Linq
{
    public sealed class ObservableExtensions_ToDictionaryTest
    {
        [Test]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() =>
                ((IObservable<(object, object)>)null).ToDictionary());

        [Test, AutoDomainData]
        public void ShouldCreateMatchingDictionary(IDictionary<string, string> dict) =>
            dict.ToObservable()
                .Select(kvp => (kvp.Key, kvp.Value))
                .ToDictionary()
                .Wait()
                .ShouldBe(dict, true);
    }
}
