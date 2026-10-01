# Gate API update execution plan

This document tracks the catch-up from the last completed API version, `v4.106.116`, to the current official endpoint contracts. It is a living execution contract: findings can change the order and scope, and a group below can require several small development turns.

## Baseline and evidence

- Last completed release: `v4.106.116` dated 04 August 2026, implemented on 08 August 2026.
- Documentation checked on 01 October 2026: the website identifies `v4.106.146`, but its last changelog entry is `v4.106.144` dated 17 September 2026. The linked official C# SDK also identifies API version `v4.106.144`. Changes for `145` and `146` remain unverified.
- The changelog identifies endpoints to inspect; it is not the complete implementation specification. Reconcile each touched endpoint with its entire current request, response, authentication, validation, and documented error contract.
- Partial fixes do not mark an entire release complete. The package version stays at the last completed release until a separate version update is justified; no publishing or pushing is part of this work.

## Work order

All abbreviated release numbers below have the prefix `v4.106.`. This order prioritizes known request incompatibilities before additive features.

| Order | Scope | State |
| --- | --- | --- |
| 1 | Spot POV cancellation correction from `127`: both DELETE routes, optional bulk filter, string IDs, signatures, full response contract and cancellation semantics | Completed |
| 2 | Stock order compatibility from `121`, including current lead-trading headers and shared response changes from `141` and `143`; account for remaining rate-limit and fee documentation work separately | Next |
| 3 | P2P advertisement payment mappings from `117`; reconcile the complete current advertisement endpoint | Pending |
| 4 | Spot currency-pair limits and unified-market quote support from `122` and `133` | Pending |
| 5 | Margin market status from `124`, then Futures ADL states from `125` and `usd1` settlement from `126`; preserve the DeFi settlement restriction | Pending |
| 6 | CrossEx symbol and position changes from `130` and `131`, then LIGHTER support from `139`; assess missing isolated-margin endpoints against current documentation | Pending |
| 7 | TradFi response changes from `132`, order leverage from `138`, and authentication verification from `143`; both affected symbol queries are already signed in this wrapper | Pending |
| 8 | Remaining OTC change from `127`, then pre-upload and related business submissions from `135` | Pending |
| 9 | Remaining Stock category and market/account changes from `136` and `143`, without introducing a C# breaking change solely because the Java SDK changed accessor types | Pending |
| 10 | REST announcement queries from `142` and `144` | Pending |
| 11 | Assess public SDK Launch removal from `123` and Unified documentation changes from `128`; do not infer a service shutdown from SDK removal | Pending |
| 12 | Resolve the undocumented `145` and `146` version gap using official evidence before claiming full catch-up | Evidence pending |

## Verification and review checkpoints

- For each development turn: record sources and actual scope in the changelog, run focused regressions and the offline test suite, build affected targets, inspect the diff, and commit with a descriptive title and body. Preserve unrelated user edits and never push.
- Never place, cancel, transfer, upload, or otherwise mutate financial accounts in live verification. Signed request construction is tested with a recording HTTP handler and dummy credentials.
- Check that user-supplied identifiers cannot change the target path or broaden the scope of mutating requests. The POV cancellation tests exposed this risk even after the documented route correction.
- After four development turns, review changes, documentation, and this plan. Do not continue past five turns without that retrospective and an explicit scope/order revision where needed.
- Current catch-up development turn: 1. First review checkpoint is due after turn 4 and mandatory before turn 6.
- A previously completed out-of-order endpoint is rechecked when its original release is reached, without duplicating implementation or declaring unfinished sibling endpoints complete.

## Current turn

01 October 2026: completed reconciliation of the two Spot POV cancellation endpoints. Public method signatures and response models are unchanged. Corrected transport routes and HTTP methods, and added single-order ID validation after local tests exposed URI normalization to collection/parent paths. Invalid routing syntax now throws before network I/O. No live cancellation was attempted. The OTC part of `v4.106.127` remains open; Stock order compatibility is next.

Verification: 44 focused offline Spot tests and 317 total offline tests passed. The Release solution build passed for both `netstandard2.0` and `netstandard2.1`, including the examples and tests, with zero warnings and errors. The diff was reviewed and passed whitespace checks. Route regressions and routing-syntax safety regressions were observed failing before their respective fixes.

## Sources

- [Official changelog](https://www.gate.com/docs/developers/apiv4/en/#changelog)
- [Official C# SDK](https://github.com/gate/gateapi-csharp)
- [Bulk Spot POV cancellation](https://www.gate.com/docs/developers/apiv4/en/spot/#cancel-spot-pov-orders)
- [Single Spot POV cancellation](https://www.gate.com/docs/developers/apiv4/en/spot/#cancel-a-spot-pov-order)
