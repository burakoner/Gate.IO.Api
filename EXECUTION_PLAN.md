# Gate API update execution plan

This document tracks the catch-up from the last completed API version, `v4.106.116`, to the current official endpoint contracts. It is a living execution contract: findings can change the order and scope, and a group below can require several small development turns.

## Baseline and evidence

- Last completed release: `v4.106.116` dated 04 August 2026, implemented on 08 August 2026.
- Documentation checked on 02 October 2026: the website identifies `v4.106.146`, but its last changelog entry is `v4.106.144` dated 17 September 2026. The linked official C# SDK also identifies API version `v4.106.144`. Changes for `145` and `146` remain unverified.
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
| 5b3 | Inventory remaining Futures families indexed by `126`; reconcile standard single-order POST creation and GET/PUT/DELETE ID routes on btc/usdt/usd1 | Completed bounded scope |
| 5b4 | Standard collection orders: GET orders, GET orders_timerange and filtered bulk DELETE orders; include list/history response differences and scope-safe filters | Next bounded group |
| 5b5 | Batch creation/amendment/cancellation and countdown cancellation; reconcile each complete contract and per-item failure semantics | Pending; split if needed |
| 5b6 | Missing POST bbo_orders in the current official module index; reconcile and add separately rather than assuming a standard-order alias | Pending; newly evidenced endpoint |
| 5b7 | Position/dual-mode margin, leverage, risk and mode mutations, plus accounts/fees/private histories | Pending; select small state-sensitive families before public metadata |
| 5b8 | Public contract/market/risk metadata and trail/chase strategies | Pending; separate metadata and mutating strategy turns |
| 6 | TradFi response removal from `132`, optional order leverage from `138`, authentication from `143`, and the full current affected contracts plus direct cancellation context | Completed bounded scope; IDs retain `long` by explicit user policy |
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
- Current catch-up development turn: 9. Retrospectives after turns 4 and 8 are complete; the next is due after turn 12 and mandatory before turn 14.
- Keep existing numeric identifier accessors as `long` when the wire format is a numeric string, as explicitly requested by the user. Reject nonnumeric/out-of-range values rather than rounding or introducing string identifiers. Document acknowledgement/task identity separately from actual order identity.
- A previously completed out-of-order endpoint is rechecked when its original release is reached, without duplicating implementation or declaring unfinished sibling endpoints complete.

## Current turn

02 October 2026, turn 9: inventoried the remaining local Futures REST families and selected four standard single-order operations for complete current-contract reconciliation: POST /orders and GET/PUT/DELETE /orders/{order_id}. The existing methods, request/response fields, settlement routing and signed transport match the current tables and linked FuturesOrder/FuturesOrderAmendment schemas. No field is added merely because a code example contradicts its schema: single amendment has no contract body property. BBO placement is present in the official index but absent locally and is now a separate pending group.

Twenty-five failing regressions exposed unsafe custom-ID routing, permissive contract/tag regexes, missing/blank saved creation values becoming zero/null and unknown nullable enum strings disappearing before preflight. Single-order validation now requires one positive numeric ID or literal custom t- identifier, uses invariant numeric routing and rejects undefined creation/action enums. Creation enforces explicit market ioc, close=true with size=0, and full hedge closing with size=0/reduce_only=true. It does not infer account mode, fetch order state or select trading instructions. Quantity precision, price/iceberg/slippage limits, STP membership and amendment relationships to fills remain server-side.

Saved creation JSON requires contract/size/price. Its six decimal fields reject blank/infinite, underflowing or precision-losing values instead of selecting zero/omission; exact strings/integers remain supported. A further failing regression proved even decimal-aware readers can underflow before the converter, so floating JSON tokens are rejected rather than claiming recoverable precision. Typed C# decimal calls still serialize as strings. Known enum mappings/null omission are unchanged. This hardening also affects the shared Futures batch creation DTO, but batch routes are not freshly reconciled in this turn.

A further failing regression proved fractional numeric response IDs could be rounded into another long identity. Exact Int64 parsing now covers the standard response id/user/refu/stp_id and request pid, accepting integer tokens or numeric integer strings and rejecting float/boolean/nonnumeric/overflow values. Accessors remain long; serialization remains numeric. The response model is shared with batch, Delivery and WebSocket consumers: current Delivery integer identity fields and valid existing regressions were checked, without claiming a full audit of those routes or changing their preflight. ACK/RESULT and error-only batch items remain partial; absent IDs/default fields do not confirm creation, execution or cancellation.

Verification: 73 new tests, 280 focused offline Futures/Delivery tests and 732 total offline tests passed, excluding PublicIntegration and LiveCapture. Tests independently recompute signatures for the four operations in all three settlements and verify exact long/decimal values, fractional timestamps, optional omission/zero/false, custom IDs, the legacy positional token argument, existing ReceiveWindow expiration, partial ACK/error items and HTTP 400/429/500 metadata without retry. The Release solution build passed for netstandard2.0/netstandard2.1, examples and tests, with zero warnings/errors. README/XML guidance and compiled examples now reflect these constraints and use an actual-ID placeholder for detail. No live exchange calls or example execution. Package/assembly versions remain 4.106.116; no publication or push.

Forward revision: collection/list/time-range/bulk cancellation is the next bounded family. Batch/countdown and the newly identified BBO endpoint remain separate, followed by small position/account mutation families, market metadata and trail/chase strategies. Revisit grouping from findings rather than treating this order as a locked checklist. Release 126 and the 145/146 evidence gap remain incomplete. The next cross-turn retrospective is turn 12.

## Retrospective after development turn 8

Reviewed production changes, tests, README/XML guidance, compiled examples, changelog and execution contract from turns 5 through 8 against current Margin, Futures, Delivery and CFD documentation. Margin retains six documented market fields, optional raw status/int64 delisting metadata without guessed units, strict numeric-string parsing and literal detail routing. ADL retains its required dynamic market map, raw state and exact milliseconds; missing/null values are not normal evidence. BTC/USDT identifiers and clients remain intact, and USD1 registration does not imply WebSocket, Delivery or DeFi support.

Found and fixed two price-order deserialization gaps through eight failing regressions: unknown optional string enums became null before preflight, while fractional/boolean numeric trigger values could be coerced to valid enums. A converter scoped to the shared price-order models now rejects explicit unknown mappings instead of selecting an omitted/default instruction. The numeric converter accepts only exact Int32 tokens or legacy integer strings. Current Futures and Delivery schemas support the retained known mappings; null omission, all six response order types and valid Delivery contracts remain compatible. Unsupported shared response mappings now fail explicitly too. The generic converter and unrelated modules are unchanged.

TradFi review found the HTTP-success/business-error bug and the queue-task versus order-ID distinction beyond the indexed changelog fields. Both are addressed without inventing leverage rules or converting ID accessors to string. Source-breaking Mt5Uid removal and symbol-detail Leverage migration, default personal context, envelope semantics and acknowledgement limitations are reflected in code, guidance and tests.

Forward revision: inspect the remaining Futures inventory next, beginning with a bounded standard-order family after evidence review, not an all-module rewrite. The local client still contains standard/batch/countdown orders, positions and dual-mode margin/leverage/risk mutations, accounts/fees/histories, public market metadata and trailing/chase strategy routes. These are candidate audit groups, not completed contracts or newly proven defects. Prioritize financially mutating core contracts; split batches, position/account management and strategy families into separate turns where needed, and revise the order again from actual findings before CrossEx. Release `126` remains incomplete until the inherited contracts are reconciled; the undocumented `145`/`146` gap remains open. Next retrospective: turn 12.

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
- [Futures standard order creation](https://www.gate.com/docs/developers/apiv4/en/futures/#place-futures-order)
- [Futures single standard order detail](https://www.gate.com/docs/developers/apiv4/en/futures/#query-single-order-details)
- [Futures single standard order amendment](https://www.gate.com/docs/developers/apiv4/en/futures/#amend-single-order)
- [Futures single standard order cancellation](https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-single-order)
- [FuturesOrder schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futuresorder)
- [FuturesOrderAmendment schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futuresorderamendment)
- [BBO placement index](https://www.gate.com/docs/developers/apiv4/en/futures/#level-based-bbo-contract-order-placement)
- [Delivery integer identity compatibility](https://www.gate.com/docs/developers/apiv4/en/delivery/#deliveryorder)
- [FuturesPriceTriggeredOrder schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futurespricetriggeredorder)
- [FuturesUpdatePriceTriggeredOrder schema](https://www.gate.com/docs/developers/apiv4/en/futures/#futuresupdatepricetriggeredorder)
- [Delivery's shared price-order schema](https://www.gate.com/docs/developers/apiv4/en/delivery/#futurespricetriggeredorder)
- [Official C# Futures ADL response models](https://github.com/gate/gateapi-csharp/blob/master/docs/FuturesADLRiskStates.md)
- [CFD API and lead-trading context](https://www.gate.com/docs/developers/apiv4/en/cfd/)
- [TradFi MT5 account](https://www.gate.com/docs/developers/apiv4/en/cfd/#query-mt5-account-information)
- [TradFi user activation](https://www.gate.com/docs/developers/apiv4/en/cfd/#create-cfd-user)
- [TradFi account assets](https://www.gate.com/docs/developers/apiv4/en/cfd/#query-account-assets)
- [TradFi symbol commissions](https://www.gate.com/docs/developers/apiv4/en/cfd/#query-symbol-commission-rates)
- [TradFi symbol details](https://www.gate.com/docs/developers/apiv4/en/cfd/#query-trading-symbol-details)
- [TradFi order submission](https://www.gate.com/docs/developers/apiv4/en/cfd/#create-order)
- [TradFi order cancellation](https://www.gate.com/docs/developers/apiv4/en/cfd/#cancel-order)
- [TradFi order-request schema](https://www.gate.com/docs/developers/apiv4/en/cfd/#tradfiorderrequest)
