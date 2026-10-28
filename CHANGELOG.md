# Changelog

All notable changes to this package are documented here. The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and this package adheres to Semantic Versioning.

## [2.1.5]

### Changed
- Published packages are signed with the zagora.dev Unity organization (Unity Package Manager CLI in the publish workflow), so Unity 6.3+ no longer warns that the package has no signature.

## [2.1.4]

### Changed
- First version published to the zagora.dev UPM registry (2.1.3 was tagged but never published). The publish workflow takes its target from the `UPM_PUBLISH_REGISTRY` repo variable, so CI publishes through the Worker's workers.dev address, which zagora.dev's Bot Fight Mode doesn't challenge.

## [2.1.3]

### Changed
- Dependencies are declared as versions instead of git URLs, so they resolve from the zagora.dev UPM registry (`https://upm.zagora.dev`) and install automatically; UniTask comes from OpenUPM.
- Declared dependencies the assemblies already relied on: `com.cysharp.unitask`, `com.unity.ugui`.
- Installation instructions use the registry.

## [2.1.2]

### Added
- `documentationUrl`, `changelogUrl` and `licensesUrl` in `package.json`, so Package Manager links to the README, CHANGELOG and LICENSE on GitHub.

## [2.1.1]

### Changed
- `TextBakerCJK` logs through `CommonsLog` (Reporter when installed) instead of `Debug.Log*`.

## [2.1.0]

### Added
- `CommonsLog` — internal logging for the Commons packages (exposed only to Commons-family assemblies via `InternalsVisibleTo`). Calls [Unity Reporter](https://github.com/pzagora/Unity-Reporter) (`Report.Event/Warning/Error/Exception` with the package as source) when `PACKAGE_REPORTER` is defined — set automatically by a version define on `com.zagoradev.unity.reporter` — and `Debug.Log*` otherwise, so Reporter stays optional.

### Changed
- `ValidatedMonoBehaviour` reports missing references through `CommonsLog`.

## [2.0.1]

### Added
- Author metadata in `package.json`.

## [2.0.0]

### Changed
- **BREAKING:** Dependency injection, pooling, services and logging were extracted into separate packages, so Commons no longer bundles them. Dependency injection moved to the standalone [Unity Dependency Injection](https://github.com/pzagora/Unity-DependencyInjection) package (Commons no longer references it); `GenericObjectPool`/`PoolSettings` to [Commons.Pooling](https://github.com/pzagora/Unity-Commons.Pooling); `CoroutineService` to [Commons.Services](https://github.com/pzagora/Unity-Commons.Services); the `Report` logging façade to [Unity Reporter](https://github.com/pzagora/Unity-Reporter). Pooling and Services compile back into the `Commons` assembly via `.asmref`, so their types stay in namespace `Commons`.

### Removed
- **BREAKING:** `DependentMonoBehaviour` — binding is now handled automatically by the Dependency Injection weaver. Derive from `ValidatedMonoBehaviour` (or `MonoBehaviour`) and mark fields `[Inject]`.
- **BREAKING:** `IDestroyable` — unified on `System.IDisposable`. Types that implemented `IDestroyable.Destroy()` now implement `IDisposable.Dispose()`.

## [1.2.0]

### Added
- `PoolSettings` — a ScriptableObject holding `GenericObjectPool` sizing (time-to-live, initial size, max size) so several pools can share one configuration asset instead of repeating the numbers inline.

## [1.1.0]

### Added
- `SafeArea` component — insets the `RectTransform` it is on to `Screen.safeArea` via anchors, re-applying on resolution/orientation changes. A `RectEdge` flags field selects which edges conform, so content can bleed under an edge (e.g. the home indicator) while respecting another (e.g. the notch); backgrounds placed outside it stay full-bleed, so no letterbox bars. `[ExecuteAlways]` so it previews live in the Scene view under the Device Simulator, with a custom inspector that draws the edges as a spatial toggle selector and shows the current safe area plus a Simulator hint.

## [1.0.4]

### Added
- `RectTransformExtensions` — `SetAnchored(Vector2)`, `SetAnchoredX` and `SetAnchoredY` on `anchoredPosition`.
- `InspectorIconAttribute` — names a built-in editor icon (`EditorGUIUtility.IconContent`) a custom inspector can use to represent a field in compact, width-constrained layouts.

## [1.0.3]

### Fixed
- `TryFind` and `TryFindLast` on `ListExtensions` and `ArrayExtensions` no longer report a match when nothing matches. They previously compared the found value against a `null` sentinel, which for value-type collections always reported success; existence is now determined by index.

### Added
- EditMode tests for the collection, numeric and generic extensions and `Hash`.
- README and LICENSE (MIT).
