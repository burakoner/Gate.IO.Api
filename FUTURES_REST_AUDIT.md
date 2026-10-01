# Futures REST reconciliation — 02 October 2026

Release `v4.106.126` is closed against the current whole [official Futures reference](https://www.gate.com/docs/developers/apiv4/en/futures/), using the [changelog](https://www.gate.com/docs/developers/apiv4/en/#changelog) as an endpoint index rather than a historical schema fence. This completes the Futures group, not the whole catch-up. Progress and remaining work are owned by [EXECUTION_PLAN.md](EXECUTION_PLAN.md).

## Coverage and evidence

The current module has 71 HTTP-method/path operations on 64 distinct paths: 17 public and 54 signed. [FuturesRestCoverageTests](tests/Gate.IO.Api.Tests/Futures/FuturesRestCoverageTests.cs) contains the executable method/path/auth inventory. Its 73 cases include trading/mark/index variants of the same candle operation; each is verified on BTC, USDT and USD1 with dummy credentials and independently calculated HMAC signatures. This establishes local request construction, not market availability or live financial acceptance.

| Family | Operations | Paths | Authentication |
| --- | ---: | ---: | --- |
| Public contracts, ADL, markets, funding, insurance, statistics and risk | 17 | 17 | Public, including POST funding_rates |
| Accounts, positions, holding/hedge modes, margin, leverage and risk | 18 | 18 | Signed |
| Standard/list/history orders, batches, BBO and countdown | 12 | 8 | Signed |
| Private trades, position close/liquidation/ADL histories and fees | 6 | 6 | Signed |
| Trail strategies | 7 | 7 | Signed |
| Chase strategies | 5 | 5 | Signed |
| Price-triggered strategies | 6 | 3 | Signed |

Four previously absent paths were added: GET `contracts_all` (including delisted contracts), POST `positions/{contract}/set_leverage` (explicit margin mode), POST `set_position_mode` (account holding mode) and POST [bbo_orders](https://www.gate.com/docs/developers/apiv4/en/futures/#level-based-bbo-contract-order-placement). The first three are documented under the current reference's “Query all contract information (including delisted)”, “Update Leverage for Specified Mode” and “Set Position Holding Mode, replacing the dual_mode interface” sections. Ordinary trading candles now have public wrapper access alongside mark/index variants.

Transport corrections follow the actual endpoint tables: [fee](https://www.gate.com/docs/developers/apiv4/en/futures/#query-futures-market-trading-fee-rates) is signed GET/query; [batch_cancel_orders](https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-batch-orders-by-specified-id-list) is signed POST/body; `risk_limit_table` is unsigned GET under “Query risk limit table by table_id”. Remaining tables and linked schemas were compared for query/body fields, read-only restrictions, types, result shapes and errors. The official C# SDK identifies version 144 and is supplemental, not authority over the current website.

## Contract decisions and compatibility

The seven user-approved source-breaking migrations are `GateFuturesTrade.Size` and `GateFuturesContract.OrderSizeMinimum`, `OrderSizeMaximum`, `TradeSize`, `PositionSize`, `MinimumLeverage`, `MaximumLeverage` to `decimal`, following [FuturesTrade](https://www.gate.com/docs/developers/apiv4/en/futures/#futurestrade) and [Contract](https://www.gate.com/docs/developers/apiv4/en/futures/#contract). These fields reject precision loss; existing long identities remain exact Int64, never string replacements. Other quantity, timestamp and identity types are not conflated. BBO and legacy price-order `size` are documented integers; decimal standard orders are not used as their alias.

Private liquidation range/offset filters, separate timerange trade_id, trade_value, account split-position metadata, history.cross_settle, contract circuit-breaker metadata and deprecated ticker fields are retained. Existing raw strings remain raw where the reference provides examples rather than a closed enumeration. Legacy positional cancellation tokens remain compatible. Explicit null arguments may need a cast where a new DTO overload accompanies an existing string overload.

Document disagreements are handled explicitly:

- Ordinary candle description documents natural `1w`/`30d`, but its enum table omits them. Support the description while keeping premium-index intervals separate; no live acceptance claim.
- Batch funding schema is flat while its example is nested. Decode both, reject other shapes instead of returning empty success.
- Trail stop/update examples are flat while their schemas wrap `order`. Decode both and require a usable positive identity. Creation/detail business codes must be explicit integers; nonzero is an error even with HTTP 200.
- Price-order single amendment's example adds `contract`, but its writable schema does not. Keep the schema-based contract from earlier turns.
- Chase body settlement is explicitly overridden by its path. Preserve it; do not apply price-order amendment's strict matching rule to Chase.
- BBO auto_size requires size=0 but does not document standard orders' reduce_only=true prerequisite. A review regression ensures omission/false/true is preserved without importing that restriction.

Fail-closed checks for useful strategy containers and countdown timestamps are client hardening, not assertions that the generated schema marks every response property required. Empty documented lists remain valid. Batch item errors and failed strategy states stay visible; ACK, RESULT, an ID or HTTP success alone proves no terminal financial outcome. The client does not look up account/order state, retry, select instructions or cancel holdings to change mode.

## Verification and limits

336 new tests, 616 focused Futures/Delivery tests and 1,068 total offline tests passed. [FuturesCatchUpTests](tests/Gate.IO.Api.Tests/Futures/FuturesCatchUpTests.cs) covers the additional contract and safety regressions; existing single/price-order and shared Delivery tests remain green. Release builds for netstandard2.0/netstandard2.1, examples and tests passed with zero warnings/errors. PublicIntegration/LiveCapture were excluded and examples were not run.

USD1 here is REST only: no added WebSocket, Delivery or DeFi settlement. Package/assembly versions remain 4.106.116; nothing is published or pushed. The website's 145/146 changelog gap and non-Futures catch-up groups remain unverified/open.
