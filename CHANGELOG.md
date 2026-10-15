# Changelog

All notable changes to this package are documented here. The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and this package adheres to Semantic Versioning.

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
