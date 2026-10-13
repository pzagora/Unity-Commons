# Changelog

All notable changes to this package are documented here. The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and this package adheres to Semantic Versioning.

## [1.0.4]

### Added
- `RectTransformExtensions` — `SetAnchored(Vector2)`, `SetAnchoredX` and `SetAnchoredY` on `anchoredPosition`.

## [1.0.3]

### Fixed
- `TryFind` and `TryFindLast` on `ListExtensions` and `ArrayExtensions` no longer report a match when nothing matches. They previously compared the found value against a `null` sentinel, which for value-type collections always reported success; existence is now determined by index.

### Added
- EditMode tests for the collection, numeric and generic extensions and `Hash`.
- README and LICENSE (MIT).
