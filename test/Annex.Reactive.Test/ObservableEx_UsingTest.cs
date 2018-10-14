using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace Annex.Reactive.Test
{
    public sealed class ObservableEx_UsingTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullResourceFactoryThrowsArgumentNullException(
            Func<CancellationTokenSource, IObservable<Unit>> observableFactory) =>
            Should.Throw<ArgumentNullException>(() => ObservableEx.Using(null, observableFactory));

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullObservableFactoryThrowsArgumentNullException(
            Func<CancellationToken, Task<CancellationTokenSource>> resourceFactory) =>
            Should.Throw<ArgumentNullException>(() => ObservableEx
                .Using<Unit, CancellationTokenSource>(resourceFactory, null));
    }
}
