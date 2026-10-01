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
| 5b1 | Complete market-level ADL risk endpoint from `125` and shared `usd1` REST client registration from `126`; do not infer WebSocket, Delivery or DeFi support | Completed bounded scope; `126` is not complete |
| 5b2 | Reconcile all six Futures price-triggered order endpoints against current contracts using `126` as the index; include the missing optional amendment body settlement and safe client/body consistency | Next |
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
- Current catch-up development turn: 6. First retrospective is complete; the next is due after turn 8 and mandatory before turn 10.
- A previously completed out-of-order endpoint is rechecked when its original release is reached, without duplicating implementation or declaring unfinished sibling endpoints complete.

## Current turn

01 October 2026: added the complete current GET /futures/{settle}/adl_risk_states contract indexed by `125`, including all three documented settlements (`btc`, `usdt`, `usd1`). The public method GetAdlRiskStatesAsync is available on each REST settlement client. The response is an object containing the server's settlement and a dynamic contract-to-state dictionary, not user position ranking or execution history. Both response models retain the raw state string and exact int64 Unix calculation timestamp in milliseconds.

The current schema marks settle, states, state and calculated_at_ms required. Missing/null required fields fail deserialization instead of synthesizing normal states, empty maps or zero times. Empty maps have no market evidence; dictionary keys, null entries and unknown/empty state strings are preserved without a normal-state inference. The reported settlement is not overwritten from the selected client. No contract filter, signed headers, freshness policy, automatic polling or trading action is invented.

Added USD1=4 to the shared REST settlement enum and registered a separate USD1 client/property/indexer entry, preserving BTC=1 and USDT=3. Existing method bodies and default settlement selection are unchanged. REST support does not imply WebSocket or Delivery support: no host URL or Delivery enum value is added. No DeFi endpoint is implemented or expanded; the published btc/usdt restriction is retained, without claiming a full DeFi audit.

Forward scope revision: the current price-order amendment contract includes an optional body settle field missing from the wrapper. Split the Futures group before combining this endpoint family with the new ADL feature. The next bounded step reconciles all six price-order routes, requests, responses and validation, including client/body settlement consistency. Shared USD1 registration is available to inherited methods, but their full contracts are not declared freshly audited here and release `126` remains incomplete. TradFi remains after this step; the turn 8 review checkpoint is unchanged.

Verification: the USD1 registration regression failed before correction. Added 33 tests covering all three settlements with/without dummy credentials, dictionary shape, all documented and unknown states, required-field failures, exact int64 values above double precision and at its bounds, empty maps, settlement preservation and HTTP errors without retry. 62 focused Futures tests and 481 offline tests passed, excluding both PublicIntegration and LiveCapture. The Release solution build passed for netstandard2.0 and netstandard2.1, including examples and tests, with zero warnings/errors. No live exchange API call was made; the new compiled example is read-only and was not run. Package/assembly versions remain 4.106.116, and the 145/146 evidence gap remains open.

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
- [Market-level Futures ADL risk states](https://www.gate.com/docs/developers/apiv4/en/futures/#list-market-level-adl-risk-states)
- [FuturesADLRiskStates schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futuresadlriskstates)
- [FuturesADLRiskState schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futuresadlriskstate)
- [Futures price-order amendment](https://www.gate.com/docs/developers/apiv4/en/futures/#modify-a-single-auto-order)
- [Official C# Futures ADL response models](https://github.com/gate/gateapi-csharp/blob/master/docs/FuturesADLRiskStates.md)
