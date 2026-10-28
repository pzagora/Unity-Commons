# Unity Commons

A foundation library of general-purpose utilities for Unity 6 — extension methods, collections, common base types and small contracts. It is the shared dependency underneath the [MVVM UI](https://github.com/pzagora/Unity-MVVM) packages. Several concerns that used to live here now ship as [companion packages](#companion-packages).

## Installation

Install from the zagora.dev UPM registry — add the scoped registry (and OpenUPM, for UniTask) to `Packages/manifest.json`, then the package; its dependencies install automatically:

```json
"scopedRegistries": [
    { "name": "zagora.dev", "url": "https://upm.zagora.dev", "scopes": [ "com.zagoradev" ] },
    { "name": "OpenUPM", "url": "https://package.openupm.com", "scopes": [ "com.cysharp" ] }
],
"dependencies": {
  "com.zagoradev.unity.commons": "2.1.6"
}
```

Requires Unity `6000.0+`. The runtime code (assembly `Commons`) ships in player builds; editor helpers live in the `Editor/` folder.

## What's inside

| Area | Highlights |
| --- | --- |
| **Extensions** | Null-safe, allocation-conscious helpers for arrays, `List`, `ICollection`, `IEnumerable`, `Queue`, `Stack`, `Dictionary`, plus `string`, numeric, `Vector`, `Transform`, `GameObject`, `Component`, `CanvasGroup` and TMP text. |
| **Collections** | `SerializableDictionary` and `SerializableTuple` that survive Unity serialization. |
| **Mono** | `ValidatedMonoBehaviour` base class with inspector validation of `[Required]` fields. |
| **Helpers** | `Safe` (condition-guarded call / get / set) and `Wait.UntilApproximately` (await a float value approaching a target). |
| **Math** | `Hash.Compute(...)` for order-sensitive composite hash codes. |
| **Interfaces** | Small contracts — `IService`, `IModule`, `IIndexed` (+ `Indexed`). |
| **Attributes** | `[Required]`, `[StrongHeader]`, `[LocalizedKey]` inspector/metadata attributes. |
| **PlayerPrefs** | `ATargetedPlayerPrefs` base for namespaced, typed preference access. |

## Companion packages

Some areas that previously shipped inside Commons now live in their own repositories:

| Package | Contents | Relationship to Commons |
| --- | --- | --- |
| [Commons.Pooling](https://github.com/pzagora/Unity-Commons.Pooling) | `GenericObjectPool<T>`, `PoolSettings`. | Compiles **into** the `Commons` assembly via `.asmref`; requires Commons. |
| [Commons.Services](https://github.com/pzagora/Unity-Commons.Services) | `CoroutineService` (+ `ICoroutineService`), `SceneService<TScene>`. | Compiles **into** the `Commons` assembly via `.asmref`; requires Commons. |
| [Commons.AsyncPipeline](https://github.com/pzagora/Unity-Commons.AsyncPipeline) | `AsyncPipeline<TStepId>` — weighted, cancellable async step sequences. | Compiles **into** the `Commons` assembly via `.asmref`; requires Commons. |
| [Commons.Addressables](https://github.com/pzagora/Unity-Commons.Addressables) | `AssetScope`, folder-convention tooling, key generation, build validation, scopes debugger. | Own assembly (`Commons.Addressables`); requires Commons and Addressables. |

Dependency injection and logging were also extracted, but as fully independent packages that Commons does **not** depend on: [Unity Dependency Injection](https://github.com/pzagora/Unity-DependencyInjection) and [Unity Reporter](https://github.com/pzagora/Unity-Reporter).

### Logging

Commons packages log through the internal `CommonsLog` (`Event` / `Warning` / `Error` / `Exception`, each taking a source name). When Unity Reporter is installed, the `Commons` assembly's version defines set `PACKAGE_REPORTER` and calls go straight to `Report` with that source; otherwise they compile to `Debug.Log*` as `[Source] message`. Version defines only see packages installed through the Package Manager; a project that embeds these packages under `Assets/` adds `PACKAGE_REPORTER` to its Scripting Define Symbols instead.

## Examples

```csharp
using Commons;

// Collection extensions
var items = new List<int> { 1, 2, 3 };
if (items.TryFind(x => x > 1, out var found)) { /* found == 2 */ }
items.AddUnique(2);          // no-op, already present
items.Swap(0, 2);            // { 3, 2, 1 }

// Numeric extensions
var multiplier = 25.ToAdditionalPercentageMultiplier(); // 1.25f
var clamped = (-5).ClampToPositive();                   // 0
```

## Tests

EditMode tests live in `Tests/EditModeTests/` (assembly `Commons.EditModeTests`) and cover the collection, numeric and generic extensions plus `Hash`. Run them from **Window ▸ General ▸ Test Runner ▸ EditMode**.

## Project structure

```
Runtime/
  Attributes/    Inspector and metadata attributes
  Collections/   Serializable dictionary and tuple
  Constants/     Shared constant strings
  Enums/         Shared enums
  Extensions/    Extension methods (Collections/ + component/value-type helpers)
  Fonts/         FontPreset
  Helpers/       Safe, Wait
  Interfaces/    IService, IModule, IIndexed
  Math/          Hash
  Mono/          ValidatedMonoBehaviour base class
  PlayerPrefs/   Targeted PlayerPrefs base
  Text/          StyleLinkedTMP
  UI/            SafeArea, RectEdge
Editor/          Editor-only drawers and helpers
Tests/           EditMode tests
```

Pooling and Services now live in [companion packages](#companion-packages); dependency injection and logging live in their own independent packages (see above).
