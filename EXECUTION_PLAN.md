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
| 5b3 | Inventory remaining inherited Futures contracts indexed by `126`, then select a bounded standard-order family for full reconciliation; prioritize mutating order and position/account contracts before public metadata and strategy families | Next; do not combine the whole Futures module in one turn |
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
- Current catch-up development turn: 8. Retrospectives after turns 4 and 8 are complete; the next is due after turn 12 and mandatory before turn 14.
- Keep existing numeric identifier accessors as `long` when the wire format is a numeric string, as explicitly requested by the user. Reject nonnumeric/out-of-range values rather than rounding or introducing string identifiers. Document acknowledgement/task identity separately from actual order identity.
- A previously completed out-of-order endpoint is rechecked when its original release is reached, without duplicating implementation or declaring unfinished sibling endpoints complete.

## Current turn

02 October 2026: reconciled the six TradFi contracts indexed by `132`, `138` and `143` against the current CFD module: MT5 account, user activation, account assets, symbol commissions, symbol details and order submission. Removed the three public Mt5Uid properties with the user's approval and refreshed their fixtures. Symbol-detail Leverage now preserves the documented string; this is a separate source-breaking accessor correction. Monetary decimal-string mappings remain strict and existing account fields are unchanged.

Order requests support nullable integer Leverage without choosing a default or guessing allowed symbol multipliers. Existing positional overloads and cancellation tokens remain compatible. Required symbol and side/price-type enums are checked before I/O. Commission filters keep category-only support; symbol details keep their ten-symbol maximum, and embedded CSV entries cannot bypass it. Both symbol operations were already signed; signatures for all six affected routes are now independently checked against the documentation.

The common TradFi data/list handler previously returned Success=true for HTTP 200 business errors. Nonzero code or nonempty label now returns an error while retaining HTTP metadata; omitted/zero code and empty label remain valid success envelopes. No retry, polling or automatic financial action is added. Direct cancellation now uses that handler, retains the documented empty acknowledgement and signed DELETE route, and requires a positive actual long order ID formatted invariantly.

Added construction-time, opt-in TradFiLeadTrading for the documented cfd_copy header. Personal trading remains default; activation and GET/POST transactions are excluded. Context is request-scoped and does not leak into shared HTTP defaults or Stock. Cancellation participates too. This central context/error correction is not a claim that every other TradFi request/response schema was freshly audited.

Order submission returns a queue task ID, not an actual order ID or fill confirmation. The user explicitly overrode the proposed string migration: Id stays long, numeric strings parse exactly, and nonnumeric/out-of-range values fail rather than silently changing identity. An absent optional ID still yields the legacy zero and identifies no task. Optional account/asset values retain existing numeric defaults; absent data or transport success alone does not prove activation, funds or execution. The examples now distinguish task identity from the actual IDs used for update/cancel and deliberately omit leverage.

Verification: added 65 tests across TradFi and the retrospective Futures fixes; 71 focused TradFi tests, 207 focused Futures/Delivery tests and 659 offline tests passed, excluding PublicIntegration and LiveCapture. The Release solution build passed for netstandard2.0/netstandard2.1, including examples/tests, with zero warnings/errors. Fixtures and compiled examples were verified without executing the example program or making live exchange calls. Package/assembly versions remain 4.106.116; no publication or push.

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
