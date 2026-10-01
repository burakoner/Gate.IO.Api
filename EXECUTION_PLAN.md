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
| 5b2 | Reconcile all six Futures price-triggered order endpoints against current contracts using `126` as the index; include the missing optional amendment body settlement and safe client/body consistency | Completed bounded scope |
| 5b3 | Inventory remaining inherited Futures contracts indexed by `126` and split any current-document differences into bounded audits before claiming the release complete | Pending; scope/order decision at the turn 8 review |
| 6 | TradFi breaking response changes from `132`, order leverage from `138`, and authentication verification from `143`; both affected symbol queries are already signed in this wrapper | Next |
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
- Current catch-up development turn: 7. First retrospective is complete; the next is due after turn 8 and mandatory before turn 10.
- A previously completed out-of-order endpoint is rechecked when its original release is reached, without duplicating implementation or declaring unfinished sibling endpoints complete.

## Current turn

01 October 2026: reconciled all six current Futures price-order contracts indexed by `126`: list/create/bulk DELETE at /futures/{settle}/price_orders, detail/single DELETE at /{order_id}, and PUT /amend with the ID in the body. All remain signed on btc/usdt/usd1. No return type, shared enum value, default settlement or HTTP method was changed.

Added optional string Settlement to the amendment body. Exact route/body equality is enforced without trimming, overwriting or inferring a value. A regression exposed the generic nullable enum converter silently turning an unknown settlement string into null; using the documented string wire type preserves the supplied value for explicit rejection. Other optional amendment fields stay omitted, and an ID-only body remains supported because the schema does not require another field.

Added Futures-only preflight guards for required creation objects/prices/rule, current writable types and enums, gtc/ioc, explicit market-price ioc, readonly response fields, malformed contracts, positive existing IDs and list pagination. The explicit statement that only strategy_type=0 is supported takes precedence over the table describing 1; the readonly order-type restriction takes precedence over the conflicting POST example. No live price, account mode, closing direction, quantity precision, tif for an existing amendment, or upper pagination bound is inferred. Amount and Size remain as supplied, with server-side Amount precedence. Exact textual-zero detection avoids rounding a tiny nonzero price into a market order.

Retrospective within this bounded diff: blank bulk filters and the shared helper's accidental pipe allowance failed regressions before correction. The local guard accepts one-character base assets without altering other modules' helpers. The legacy non-nullable creation overload now omits auto_size for None; DTO callers use null, and explicit empty/unknown sides are rejected. Both current Futures and Delivery schemas require initial/trigger, initial.contract, both prices and trigger.rule, so missing/null fields fail deserialization in the shared model. Valid Delivery contracts and outgoing requests remain compatible; no Futures-only preflight rules, settlement or URL were added to Delivery.

Response fields, all documented lifecycle/finish/type values, numeric trigger encodings, string quantities, int64 IDs and existing fractional timestamp accessors are covered. Bulk HTTP 200 is not blanket cancellation; open and triggered responses are not rewritten, and no retry or polling is added. Optional acknowledgement IDs remain optional: the existing numeric getter still defaults to zero when absent, so consumers must check a returned identifier, not transport success alone.

Forward revision: shared USD1 registration plus this family do not constitute a fresh audit of every inherited Futures endpoint. Added an explicit remaining-contract inventory step for the turn 8 scope review; `126` is still incomplete. TradFi's known breaking changes remain next, followed by the code/documentation/plan retrospective. No WebSocket or DeFi USD1 support is inferred. Package/assembly versions remain 4.106.116, and the undocumented 145/146 gap stays open.

Verification: added 113 tests; 191 focused Futures/Delivery tests and 594 offline tests passed, excluding both PublicIntegration and LiveCapture. The Release solution build passed for netstandard2.0 and netstandard2.1, including examples and tests, with zero warnings/errors. Independently recomputed signatures cover all six routes under all three settlements. Corrected the older creation regression's planned-long quantity sign and the partial amendment's close flag to match the parameter explanations. No live exchange API call was made; examples were compiled but not run. No publishing or push.

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
- [Futures price-order list](https://www.gate.com/docs/developers/apiv4/en/futures/#query-auto-order-list)
- [Futures price-order creation](https://www.gate.com/docs/developers/apiv4/en/futures/#create-price-triggered-order)
- [Futures bulk price-order cancellation](https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-all-auto-orders)
- [Futures single price-order detail](https://www.gate.com/docs/developers/apiv4/en/futures/#query-single-auto-order-details)
- [Futures single price-order cancellation](https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-single-auto-order)
- [FuturesPriceTriggeredOrder schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futurespricetriggeredorder)
- [FuturesUpdatePriceTriggeredOrder schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futuresupdatepricetriggeredorder)
- [Delivery's shared price-order schema](https://www.gate.com/docs/developers/apiv4/en/delivery/#futurespricetriggeredorder)
- [Official C# Futures ADL response models](https://github.com/gate/gateapi-csharp/blob/master/docs/FuturesADLRiskStates.md)
