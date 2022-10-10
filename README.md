<h1 align="center">
<img src="./logo.svg" alt="Annex.NET Logo" width="100">
<br />
Annex&#46;NET
</h1>

<div align="center">

[![Build Status](https://dev.azure.com/gtbuchanan/annex-net/_apis/build/status/gtbuchanan.annex-net?branchName=master)](https://dev.azure.com/gtbuchanan/annex-net/_build/latest?definitionId=1)
[![codecov](https://codecov.io/gh/gtbuchanan/annex-net/branch/master/graph/badge.svg)](https://codecov.io/gh/gtbuchanan/annex-net)
[![Gitter chat](https://badges.gitter.im/gitterHQ/gitter.png)](https://gitter.im/gtbuchanan/annex-net)

</div>

Useful additions to the .NET Framework and:

* [Reactive Extensions](https://github.com/dotnet/reactive)

### Goals

* Fill gaps in existing libraries and provide other useful utilities that compliment core functionality, *not* provide abstractions or alternative APIs.

* Get equivalent features added to the existing libraries. Though, we understand the likelyhood of that happening for many of the features is low (especially the global ones).

### Don't reinvent the wheel

* [akarnokd's Reactive Extensions](https://github.com/akarnokd/reactive-extensions) - More reactive extensions
* [AsyncEx](https://github.com/StephenCleary/AsyncEx) - A helper library for async/await
* [Enums.NET](https://github.com/TylerBrinkley/Enums.NET) - Like `Enum`, but better ([Rejected Runtime Proposal](https://github.com/dotnet/runtime/issues/20008))
* [Humanizer](https://github.com/Humanizr/Humanizer) - String formatting done right
* [Interactive Extensions](https://github.com/dotnet/reactive/tree/main/Ix.NET/Source/System.Interactive) - Familiar "reactive extensions" for `IEnumerable`
* [MoreLINQ](https://github.com/morelinq/MoreLINQ) - All the LINQ you could ask for
* [MoreRx](https://github.com/quinmars/MoreRx) - More reactive extensions
* [NodaTime](https://github.com/nodatime/nodatime) - A better date and time API
* [Rxx](https://github.com/RxDave/Rxx) - More reactive extensions _(Outdated)_

## Contribute

We'll only consider contributions in the spirit of the [goals](#goals) (e.g. the feature fills gaps in an existing library and could be considered useful to a large portion of people).

### Build

1. Open PowerShell
2. Run `./build` from the root repository path

## Thanks

* StackOverflow contributors

* Jon Skeet for his abundant knowledge and contributions from [MiscUtil](http://jonskeet.uk/csharp/miscutil) and [C# in Depth](http://csharpindepth.com/)
