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
| 4 | Spot currency-pair limits and unified-market quote support from `122` and `133`, including the shared batch request and full cancellation/trade contracts | Completed |
| 5a | Margin market status from `124`; reconcile both currency-pair queries in a bounded turn | Completed |
| 5b | Futures ADL states from `125` and `usd1` settlement from `126`; preserve the DeFi settlement restriction | Next |
| 6 | TradFi breaking response changes from `132`, order leverage from `138`, and authentication verification from `143`; both affected symbol queries are already signed in this wrapper | Pending |
| 7 | CrossEx symbol and position changes from `130` and `131`, then LIGHTER support from `139`; assess missing isolated-margin endpoints against current documentation | Pending |
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
- Current catch-up development turn: 5. First retrospective is complete; the next is due after turn 8 and mandatory before turn 10.
- A previously completed out-of-order endpoint is rechecked when its original release is reached, without duplicating implementation or declaring unfinished sibling endpoints complete.

## Current turn

01 October 2026: reconciled GET /margin/uni/currency_pairs and GET /margin/uni/currency_pairs/{currency_pair}, indexed by `124`, against both full current endpoint contracts and UniCurrencyPair. All six response fields are now represented. Both endpoints remain public, bodyless GETs without query parameters; configured credentials do not add authentication headers. Existing method signatures and decimal accessors are preserved.

Added optional raw string Status and nullable int64 DelistedTime. Missing metadata is not defaulted to disabled or zero; unknown status strings and returned zero values are preserved. The current documentation does not specify the time unit or zero semantics, so no DateTime conversion or delisting inference is introduced. The legacy 0/1 GateMarginMarketStatus enum is unchanged rather than repurposed for a string contract. Borrow minima and leverage already read the documented numeric strings correctly; their decimal types and serialization are unchanged. Pre-commit review rejected reusing the permissive decimal-string converter here: it would interpret undocumented empty/infinite strings as zero. Four regressions preserve the existing strict behavior instead.

The complete request contract also exposed missing single-market validation: null/empty symbols and URL routing syntax could target the collection or another path. Added preflight rejection for missing values, whitespace, dot segments, separators, query/fragment syntax, percent escapes and controls. Valid literal symbols are sent unchanged; there is no closed market allowlist, silent trimming or automatic market lookup. Updated README/XML guidance and compiled examples without adding any financial operation.

Verification: 16 missing-metadata/routing regressions failed before correction. Added 39 tests covering both routes, optional/unknown status, raw int64 bounds/zero, decimal precision and invalid-value handling, public authentication with and without dummy credentials, empty lists and HTTP errors. 57 focused Margin-filter tests and 448 offline tests passed, excluding both PublicIntegration and LiveCapture. The Release solution build passed for netstandard2.0 and netstandard2.1, including examples and tests, with zero warnings/errors. No live API call was made. Package/assembly versions remain 4.106.116; remaining releases and the 145/146 evidence gap are not declared complete. Futures is the next bounded group, and the turn 8 retrospective checkpoint is unchanged.

## Retrospective after development turn 4

Reviewed the four catch-up turns' production diffs, regressions, README/XML guidance, changelog and work order against current official endpoint documentation. POV cancellation retains the two signed DELETE routes, routing-syntax preflight protection and asynchronous completion warning. Stock lead context remains request-scoped, captured at construction and excluded from both transaction methods; session rules and additive nullable asset fields remain consistent with the current contract. The prior Stock authentication exceptions are retained as previously observed production behavior, not claimed as newly verified evidence.

Found and fixed a P2P success-detection gap: the action schema marks code optional, but the legacy integer accessor defaulted an absent code to zero. Added nullable BusinessCode and changed the guidance to require an explicit zero. Code remains a compatibility accessor, so callers must migrate their success check; missing codes and risk rejections are not success. Existing payment ownership, edit-unit checks and floating valuation remain server-side, without automatic lookups or retries.

Forward revision: split the combined Margin/Futures group into two bounded steps. Keep Margin next and Futures after it, then inspect TradFi's documented breaking response removal before CrossEx's additive fields. This avoids combining unrelated financial contracts in one turn and advances a known compatibility change. No automatic throttler, quote-selection policy or historical version fence is introduced. The next review checkpoint is after turn 8.

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
- [Spot currency pairs](https://www.gate.com/docs/developers/apiv4/en/spot/#query-all-supported-currency-pairs)
- [Spot currency pair details](https://www.gate.com/docs/developers/apiv4/en/spot/#query-single-currency-pair-details)
- [Spot order creation](https://www.gate.com/docs/developers/apiv4/en/spot/#create-order)
- [Spot batch order creation](https://www.gate.com/docs/developers/apiv4/en/spot/#batch-place-orders)
- [Spot bulk cancellation](https://www.gate.com/docs/developers/apiv4/en/spot/#cancel-all-open-orders-in-specified-currency-pair)
- [Spot public trades](https://www.gate.com/docs/developers/apiv4/en/spot/#query-market-transaction-records)
- [Spot personal trades](https://www.gate.com/docs/developers/apiv4/en/spot/#query-personal-trading-records)
- [Isolated margin lending markets](https://www.gate.com/docs/developers/apiv4/en/isolated-margin/#list-lending-markets)
- [Isolated margin lending market details](https://www.gate.com/docs/developers/apiv4/en/isolated-margin/#get-lending-market-details)
- [UniCurrencyPair schema](https://www.gate.com/docs/developers/apiv4/en/isolated-margin/#unicurrencypair)
- [Official C# UniCurrencyPair model](https://github.com/gate/gateapi-csharp/blob/master/docs/UniCurrencyPair.md)
