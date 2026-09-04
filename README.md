# Unity Commons

A foundation library of general-purpose utilities for Unity 6 — extension methods, collections, a lightweight dependency-injection container, object pooling, and common base types. It is the shared dependency underneath the [MVVM UI](https://github.com/pzagora/MVVM-UI) packages, but has no dependencies of its own.

## Installation

Add to `Packages/manifest.json`:

```json
"com.zagoradev.unity.commons": "https://github.com/pzagora/Unity-Commons.git"
```

Requires Unity `6000.0+`. The runtime code (assembly `Commons`) ships in player builds; editor helpers live in the `Editor/` folder.

## What's inside

| Area | Highlights |
| --- | --- |
| **Extensions** | Null-safe, allocation-conscious helpers for arrays, `List`, `ICollection`, `IEnumerable`, `Queue`, `Stack`, `Dictionary`, plus `string`, numeric, `Vector`, `Transform`, `GameObject`, `Component`, `CanvasGroup` and TMP text. |
| **Injection** | Attribute-driven DI: mark fields with `[Inject]`, register bindings through `Install`, and resolve an object's dependencies with `Binder.Bind(this)`. |
| **Pooling** | `GenericObjectPool<T>` for reusing instances without per-frame allocation. |
| **Collections** | `SerializableDictionary` and `SerializableTuple` that survive Unity serialization. |
| **Mono** | `ValidatedMonoBehaviour` and `DependentMonoBehaviour` base classes with inspector validation and dependency wiring. |
| **Services** | `CoroutineService` for running coroutines detached from a specific `MonoBehaviour`. |
| **Helpers** | `Safe` (condition-guarded call / get / set) and `Wait.UntilApproximately` (await a float value approaching a target). |
| **Math** | `Hash.Compute(...)` for order-sensitive composite hash codes. |
| **Interfaces** | Small contracts — `IDestroyable`, `IService`, `IModule`, `IIndexed` (+ `Indexed`). |
| **Attributes** | `[Required]`, `[StrongHeader]`, `[LocalizedKey]` inspector/metadata attributes. |
| **PlayerPrefs** | `ATargetedPlayerPrefs` base for namespaced, typed preference access. |

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

// Dependency injection
public class Player
{
    [Inject] private readonly IInventoryService _inventory;

    public Player() => Binder.Bind(this);
}
```

## Tests

EditMode tests live in `Tests/EditModeTests/` (assembly `Commons.EditModeTests`) and cover the collection, numeric and generic extensions plus `Hash`. Run them from **Window ▸ General ▸ Test Runner ▸ EditMode**.

## Project structure

```
Runtime/
  Attributes/    Inspector and metadata attributes
  Collections/   Serializable dictionary and tuple
  Extensions/    Extension methods (Collections/ + component/value-type helpers)
  Helpers/       Safe, Wait
  Injection/     Attribute-driven DI (Binder, Inject, Install, Track)
  Interfaces/    IDestroyable, IService, IModule, IIndexed
  Math/          Hash
  Mono/          Validated/Dependent MonoBehaviour base classes
  PlayerPrefs/   Targeted PlayerPrefs base
  Pooling/       GenericObjectPool
  Services/      CoroutineService
Editor/          Editor-only drawers and helpers
Tests/           EditMode tests
```
