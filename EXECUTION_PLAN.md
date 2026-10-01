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
| 2 | Stock contracts from `121`, `136`, `141` and the Stock portion of `143`: order sessions, opt-in lead-trading context, Japanese exchanges, asset types, option assets, categories, rate-limit and fee documentation | Completed |
| 3 | P2P advertisement payment mappings from `117`; reconcile the complete current advertisement endpoint | Completed |
| 4 | Spot currency-pair limits and unified-market quote support from `122` and `133` | Next |
| 5 | Margin market status from `124`, then Futures ADL states from `125` and `usd1` settlement from `126`; preserve the DeFi settlement restriction | Pending |
| 6 | CrossEx symbol and position changes from `130` and `131`, then LIGHTER support from `139`; assess missing isolated-margin endpoints against current documentation | Pending |
| 7 | TradFi response changes from `132`, order leverage from `138`, and authentication verification from `143`; both affected symbol queries are already signed in this wrapper | Pending |
| 8 | Remaining OTC change from `127`, then pre-upload and related business submissions from `135` | Pending |
| 9 | Stock category and market/account changes from `136` and `143`, without a C# breaking accessor change solely because the Java SDK changed | Completed in order 2 |
| 10 | REST announcement queries from `142` and `144` | Pending |
| 11 | Assess public SDK Launch removal from `123` and Unified documentation changes from `128`; do not infer a service shutdown from SDK removal | Pending |
| 12 | Resolve the undocumented `145` and `146` version gap using official evidence before claiming full catch-up | Evidence pending |

## Verification and review checkpoints

- For each development turn: record sources and actual scope in the changelog, run focused regressions and the offline test suite, build affected targets, inspect the diff, and commit with a descriptive title and body. Preserve unrelated user edits and never push.
- Never place, cancel, transfer, upload, or otherwise mutate financial accounts in live verification. Signed request construction is tested with a recording HTTP handler and dummy credentials.
- Check that user-supplied identifiers cannot change the target path or broaden the scope of mutating requests. The POV cancellation tests exposed this risk even after the documented route correction.
- After four development turns, review changes, documentation, and this plan. Do not continue past five turns without that retrospective and an explicit scope/order revision where needed.
- Current catch-up development turn: 3. First review checkpoint is due after turn 4 and mandatory before turn 6.
- A previously completed out-of-order endpoint is rechecked when its original release is reached, without duplicating implementation or declaring unfinished sibling endpoints complete.

## Current turn

01 October 2026: reconciled `POST /p2p/merchant/books/place_biz_push_order` against its complete current request, response and authentication contracts, using `117` as the index. Existing request fields, wire types and risk response models were already present. Added payment-map preflight validation while retaining the optional field and existing public signatures: keys must match enabled payment types, duplicate keys and malformed maps are rejected, and supplied IDs are not reformatted. Corrected the test that enabled only `bank` but also selected a `swift` account.

Added guards for documented operation/limit/price flags, required edit IDs and the fixed-price fiat maximum. The official full request example uses a publish operation with an edit ID and a fiat maximum above the stated fixed-price total; follow the parameter descriptions instead of copying those inconsistent example values. Omitted price mode and floating valuation are not inferred from UnitPrice. Account ownership and the existing advertisement's limit unit remain server-verified; no live account lookup or automatic payment selection is added. HTTP 200 business rejection code `70305102` remains in the public action response and must not be treated as a saved advertisement.

Verification: 46 focused P2P tests and 379 offline tests passed, excluding both `PublicIntegration` and `LiveCapture`. The Release solution build passed for `netstandard2.0` and `netstandard2.1`, including examples and tests, with zero warnings and errors. The mismatched payment type, missing edit ID and excessive fixed-price fiat maximum regressions failed before their fixes. Coverage includes all four operations, optional payment-map omission, raw ID/string preservation, all submitted wire-field types, preflight exclusions, independently recomputed signatures, complete risk details and fractional timestamps. No authenticated or state-changing live call was made. Remaining releases and the version gap are still open; package/assembly versions remain `4.106.116`. Spot quote-currency support is next, followed by the first retrospective checkpoint after development turn 4.

## Sources

- [Official changelog](https://www.gate.com/docs/developers/apiv4/en/#changelog)
- [Official C# SDK](https://github.com/gate/gateapi-csharp)
- [Bulk Spot POV cancellation](https://www.gate.com/docs/developers/apiv4/en/spot/#cancel-spot-pov-orders)
- [Single Spot POV cancellation](https://www.gate.com/docs/developers/apiv4/en/spot/#cancel-a-spot-pov-order)
- [Stock API and lead-trading scope](https://www.gate.com/docs/developers/apiv4/en/stock/)
- [Stock order creation](https://www.gate.com/docs/developers/apiv4/en/stock/#create-order)
- [Stock symbols](https://www.gate.com/docs/developers/apiv4/en/stock/#query-symbol-list)
- [Stock symbol details](https://www.gate.com/docs/developers/apiv4/en/stock/#query-symbol-details)
- [Stock account assets](https://www.gate.com/docs/developers/apiv4/en/stock/#query-user-assets)
- [Japanese and Korean stock fee rates](https://www.gate.com/docs/developers/apiv4/en/stock/#query-fee-rates-for-japanese-and-korean-stocks)
- [P2P advertisement submission](https://www.gate.com/docs/developers/apiv4/en/p2p/#publish-ad-order)
- [P2P payment method list](https://www.gate.com/docs/developers/apiv4/en/p2p/#get-payment-method-list)
