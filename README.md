# Gate.IO.Api

A .Net wrapper for the Gate.IO API as described on [Gate.IO](https://www.gate.io/docs/developers/apiv4/en/), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/burakoner/Gate.IO.Api/issues)**

The current API catch-up scope, completed steps and review checkpoints are tracked in the [execution plan](EXECUTION_PLAN.md).

## Donations

Donations are greatly appreciated and a motivation to keep improving.

**BTC**:  33WbRKqt7wXARVdAJSu1G1x3QnbyPtZ2bH  
**ETH**:  0x65b02DB9b67B73f5f1E983ae10796f91dEd57B64  
**USDT (TRC-20)**:  TXwqoD7doMESgitfWa8B2gHL7HuweMmNBJ  

## Installation

![Nuget version](https://img.shields.io/nuget/v/Gate.IO.Api.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/Gate.IO.Api.svg)
Available on [Nuget](https://www.nuget.org/packages/Gate.IO.Api).

```console
PM> Install-Package Gate.IO.Api
```

To get started with Gate.IO.Api first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/Gate.IO.Api). Using Visual Studio this can be done in two ways.

### Using the package manager

In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'Gate.IO.Api' and hit enter. The Gate.IO.Api package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use Gate.IO.Api in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console

In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that Gate.IO.Api will be installed in. After selecting the correct project type  `Install-Package Gate.IO.Api`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using Gate.IO.Api.

## Getting started

After installing it's time to actually use it. To get started we have to add the Gate.IO.Api namespace:  `using Gate.IO.Api;`.

Gate.IO.Api provides two clients to interact with the Gate.IO.Api. The  `GateRestApiClient`  provides all rest API calls. The  `GateWebSocketClientOptions` provides functions to interact with the websocket provided by the Gate.IO.Api. Both clients are disposable and as such can be used in a  `using`statement.

## Running tests

The default test run is CI friendly and does not require Gate.IO credentials or live network access:

```console
dotnet test "Gate.IO Api Client.sln" -v:minimal
```

Tests are grouped with xUnit `Category` traits so focused runs can use standard filters:

```console
dotnet test "Gate.IO Api Client.sln" -v:minimal --filter "Category=Unit"
dotnet test "Gate.IO Api Client.sln" -v:minimal --filter "Category=Contract"
dotnet test "Gate.IO Api Client.sln" -v:minimal --filter "Category=PublicIntegration"
```

`PublicIntegration` tests are opt-in at runtime. Without `GATEIO_RUN_LIVE_TESTS=1` they return immediately and do not call Gate.IO. To run the public, unauthenticated live smoke tests:

```powershell
$env:GATEIO_RUN_LIVE_TESTS = "1"
dotnet test "Gate.IO Api Client.sln" -v:minimal --filter "Category=PublicIntegration"
Remove-Item Env:\GATEIO_RUN_LIVE_TESTS
```

Public REST fixture refreshes are separate from normal live smoke tests and are also opt-in. They write normalized JSON under `tests/Gate.IO.Api.Tests/Fixtures/Live`:

```powershell
$env:GATEIO_CAPTURE_PUBLIC_FIXTURES = "1"
dotnet test "Gate.IO Api Client.sln" -v:minimal --filter "Category=LiveCapture"
Remove-Item Env:\GATEIO_CAPTURE_PUBLIC_FIXTURES
```

Use `GATEIO_CAPTURE_PUBLIC_FIXTURE_FILTER` to refresh only matching catalog entries by module, endpoint name, path, or fixture path:

```powershell
$env:GATEIO_CAPTURE_PUBLIC_FIXTURES = "1"
$env:GATEIO_CAPTURE_PUBLIC_FIXTURE_FILTER = "Spot/currencies.json;Unified/portfolio_calculator"
dotnet test "Gate.IO Api Client.sln" -v:minimal --filter "Category=LiveCapture"
Remove-Item Env:\GATEIO_CAPTURE_PUBLIC_FIXTURES
Remove-Item Env:\GATEIO_CAPTURE_PUBLIC_FIXTURE_FILTER
```

Authenticated private endpoints are covered by contract tests and request construction/signing tests using fixtures and fake credentials. Do not commit real API keys or private account responses.

## Stock trading

Stock limit orders require `GateStockTradingSession.All`; market orders require `GateStockTradingSession.Regular`. Only `day` time in force is supported. The [current Stock documentation](https://www.gate.com/docs/developers/apiv4/en/stock/) specifies 5 qps for the operations other than exchanges, whose limit is not stated. The wrapper does not enforce these limits automatically; pace requests in the consuming application. `GetFeeRatesAsync` returns Japanese and Korean stock fee rates.

Personal trading remains the default. To explicitly select Stock lead trading, construct a separate client with `StockLeadTrading = true`:

```csharp
using var leadApi = new GateRestApiClient(new GateRestApiClientOptions { StockLeadTrading = true });
// Configure this client's API credentials before using signed endpoints.
```

This setting is captured at construction and sends `x-gate-trader-copy-type: stock_copy` only for eligible Stock requests. Both transaction history and fund transfers (`GET`/`POST /stock/transactions`) are excluded, and other modules are unaffected. Changing the options object later does not switch an existing client's trading context. Use separate clients for personal and lead trading.

Japanese exchange queries use `GateStockExchange.Japan`. Symbol responses expose nullable `AssetType` (`STOCK`/`ETF`), and account assets expose nullable option market value and PnL fields so an omitted field is not mistaken for zero. `Category` remains a string supporting the documented `CS`, `ETF`, `ADRC`, `ADR`, `ETV`, `PFD`, `ETS`, `ETN` and `FUND` values.

## P2P advertisement submission

For [advertisement creation and editing](https://www.gate.com/docs/developers/apiv4/en/p2p/#publish-ad-order), `PayType` contains enabled payment **types**, such as `bank,swift`, not account IDs. Obtain the types and the current user's corresponding payment method IDs through `GetPaymentMethodsAsync`. If `PayTypeJson` is supplied, it is a JSON **string**, for example `{"bank":"10001","swift":"10002"}`. Its keys must be enabled in `PayType`. The mapping remains optional; the wrapper neither fetches accounts nor invents or rewrites IDs. The server verifies whether those accounts belong to the current user.

Use the request overload for editing and supply `OrderId`. Preserve the existing advertisement's limit unit: fiat-limit edits must keep `LimitBasis = GateP2pAdLimitBasis.Fiat`. The wrapper does not fetch the existing advertisement to infer its unit. For explicit fixed-price fiat limits, `FiatMaxAmount` must not exceed `Number * UnitPrice`; floating-price and unspecified-mode valuation remains server-side.

`RestCallResult.Success` alone does not confirm that the advertisement was saved. Require `result.Data?.BusinessCode == 0`, which confirms an explicit success code. A missing code remains `null` and is not success; `70305102` means content risk control rejected the submission, with the prompt in `result.Data.Data.RiskEvent`. The legacy `Code` accessor remains available but returns zero for an absent code, so do not use it alone as a success check. Business rejections are not converted into transport errors or retried automatically.

## Spot POV cancellation

Single and bulk POV cancellation use the signed [current DELETE contracts](https://www.gate.com/docs/developers/apiv4/en/spot/#cancel-spot-pov-orders), with no request body. An omitted bulk `Symbol` targets all eligible Spot POV orders; supply it unless that broad scope is intentional. Missing/null response bodies and null bulk order items fail with the original HTTP metadata retained. A genuine empty array remains valid. Neither HTTP success nor a status such as `CREATED` or `CANCELING` proves completed cancellation; inspect the returned order/list and confirm subsequent status in your application. The wrapper does not retry or poll automatically after an ambiguous response.

The shared `GateSpotPovOrder` model retains its public types and all 16 current fields. Its three decimal amount/price values and five `long` millisecond values now reject lossy/malformed input rather than rounding or substituting zero. Optional prices/times preserve genuine omission/null, and explicit zero remains zero. Saved amount/price JSON is written as numeric **strings**, matching the documented wire shape; consumers expecting saved numeric tokens need to accommodate that change. This stricter parsing also applies to POV list/create/detail responses through the shared model. The added missing-container guards are scoped to cancellation, not every Spot method; unrelated generic converters remain unchanged.

## Spot unified market quotes

The [current Spot contract](https://www.gate.com/docs/developers/apiv4/en/spot/#create-order) distinguishes a market's `Quote` from its actual trading quote. `GetMarketsAsync` and `GetMarketAsync` expose `TradeQuotes`; `null` means the market does not support unified quotes. Select a supported actual currency explicitly through `GateSpotOrderRequest.TradeQuote` for single or batch orders. An omitted quote stays omitted, with no automatic currency substitution or account switch. For a market buy, `Amount` is expressed in the actual quote currency; limit orders and market sells use base-currency quantity.

Use `GateSpotCancelOrdersRequest.TradeQuote` to restrict [bulk cancellation](https://www.gate.com/docs/developers/apiv4/en/spot/#cancel-all-open-orders-in-specified-currency-pair) to one actual quote. Omitting it includes all quotes matching the other filters; omitting `Symbol` or `Account` further broadens the scope. Existing overloads keep their signatures and do not add a quote filter. HTTP success is not proof that every cancellation succeeded: inspect each returned order's nullable `Succeeded`, `ErrorLabel` and `ErrorMessage`.

Orders and trades expose the returned actual `TradeQuote`. The public and personal trade queries do not document a quote query filter, so none is invented. `CreateTimeInMillisecondsPrecise` preserves fractional milliseconds in trade responses; the existing `long` accessor retains its truncating behavior. For `MarketOrderMaxStock` and `MarketOrderMaxMoney`, both `null` and zero mean no limit. These limits are returned as-is, not converted into an automatic order-sizing policy. Use the existing client `ReceiveWindow` option for the documented `x-gate-exptime` header.

## Isolated margin market metadata

The public [lending market list](https://www.gate.com/docs/developers/apiv4/en/isolated-margin/#list-lending-markets) and [market details](https://www.gate.com/docs/developers/apiv4/en/isolated-margin/#get-lending-market-details) expose `GateMarginMarket.Status` as the returned string: `enabled` or `disabled`. Missing/null status stays `null`, and unknown strings are preserved. To confirm an explicit enabled response, check `result.Success && result.Data?.Status == "enabled"`; HTTP success alone is not market availability, and an enabled market does not guarantee permission or capacity to borrow. The historical `GateMarginMarketStatus` numeric enum is unchanged and is not used for this string field.

`DelistedTime` is a nullable raw `long` matching the documented `int64`. The current contract does not specify its unit or the meaning of zero, so the wrapper does not convert it to `DateTime` or interpret sentinels. Missing/null stays `null`, and disabled status is not treated as proof of delisting. Borrow minima and leverage keep their existing decimal accessors and read the documented numeric strings without relaxing invalid-value handling. The single-market `GetMarketsAsync(string)` overload requires a literal currency-pair path segment; missing values, whitespace and URL routing syntax are rejected before I/O, without trimming or substituting a market.

## Futures market ADL risk

Use `api.Futures.BTC.GetAdlRiskStatesAsync()`, `api.Futures.USDT.GetAdlRiskStatesAsync()` or `api.Futures.USD1.GetAdlRiskStatesAsync()` for the public [market-level ADL risk endpoint](https://www.gate.com/docs/developers/apiv4/en/futures/#list-market-level-adl-risk-states). It returns the server's `Settlement` and a `States` dictionary keyed by contract, not a list or the current user's position ranking/history. Each item exposes the raw `State` (`normal`, `warning`, `adl_risk`) and exact `CalculatedAtInMilliseconds` Unix timestamp. No query filters or authentication are added, even when API credentials are configured.

Check transport success, the returned settlement and the requested dictionary entry before interpreting a snapshot. Missing/null required response fields fail deserialization instead of becoming `normal`, an empty mapping or time zero. Empty mappings contain no market evidence; null entries and unknown/empty state strings remain unconfirmed, not normal. Calculation times are preserved, with no automatic freshness threshold, polling or trading action. A reported market state is not an account-level guarantee against ADL.

`GateFuturesSettlement.USD1` adds the `usd1` REST settlement and is accessible through both `api.Futures.USD1` and the indexer. Existing BTC/USDT values and clients are unchanged. This does not add a WebSocket URL, Delivery settlement or DeFi API; the [v4.106.126 changelog](https://www.gate.com/docs/developers/apiv4/en/#changelog) limits DeFi Futures to `btc`/`usdt`. The 02 October 2026 reconciliation covers all 71 current Futures REST operations on 64 paths, closing release `126`. This is not a claim of full catch-up or live exchange acceptance; package/assembly versions remain `4.106.116`. See the [audit record](FUTURES_REST_AUDIT.md) and [execution plan](EXECUTION_PLAN.md).

## Futures REST migration and remaining families

Seven approved public type changes are required: `GateFuturesTrade.Size` and `GateFuturesContract.OrderSizeMinimum`, `OrderSizeMaximum`, `TradeSize`, `PositionSize`, `MinimumLeverage`, `MaximumLeverage` are now `decimal`. Update consumers that assign these quantities to integer variables. The [contract](https://www.gate.com/docs/developers/apiv4/en/futures/#contract) and [trade](https://www.gate.com/docs/developers/apiv4/en/futures/#futurestrade) schemas use numeric strings without an integer-only restriction. These fields read exact decimal strings or integer tokens and reject floating JSON tokens, rounding, underflow and overflow. Existing numeric IDs stay `long`; integer strings retain Int64 precision, while fractional, boolean, nonnumeric and overflowing IDs fail. Existing string Chase IDs and raw `id_string` metadata keep their types.

`GetAllContractsAsync()` adds the separate `contracts_all` query, including delisted contracts. Discover the actual contract for your selected settlement; the client does not substitute a symbol. `SetPositionLeverageAsync()` uses the new explicit `margin_mode` query, without replacing legacy leverage-zero semantics. `SetPositionModeAsync()` uses the account holding modes `Single`, `Dual`, `DualPlus`, distinct from an individual position's `single`/`dual_long`/`dual_short`. Changing holding mode requires no holdings or pending orders; this remains server-enforced, with no automatic cancellation. `PlaceBboOrderAsync()` requires an integer signed quantity, book direction and depth 1-20. Direction selects asks/bids and does not rewrite quantity or infer account mode.

Trading, mark and index candlesticks accept DTO `Timezone` (`all`, `utc0`, `utc8`). Range requests omit the conflicting recent `limit`; recent limits are 1-2000, or 1-1000 for premium index. `NaturalWeek` maps to `1w`, distinct from `OneWeek` (`7d`); existing `OneMonth` (`30d`) is retained. The ordinary candle parameter description explicitly documents `1w`/`30d` although its enum table omits them. Premium index has neither timezone nor `10s`/`1w`/`30d`; those inputs fail before I/O. Calendar enum values are not elapsed-second durations. Private liquidation history now has a DTO with `From`, `To`, `At`, `Limit`, `Offset` and optional `Contract`, preserving the legacy overload.

Trading fees now use signed GET query parameters; batch ID cancellation uses signed POST with an array of invariant integer strings. Risk-table queries are public and send no credentials. Batch creation/amendment accepts 1-10 entries, cancellation 1-20; inputs are enumerated once. A batch transport success does not confirm each item succeeded: inspect nullable `Succeeded`, `ErrorLabel`, `ErrorMessage` and available order state. ACK/RESULT may be partial. Filtered bulk cancellation with omitted `Contract` intentionally has account-wide scope; blank or malformed contracts are rejected, not turned into an omitted filter. Countdown accepts 0 to disable or at least 5 seconds; a missing/null response timestamp fails instead of fabricating a successful date.

Trail creation/detail validate the envelope's integer business code as well as a positive order ID. Stop/update accept both documented flat and `order`-wrapped responses. Chase creation/stop/stop-all now accept optional body `Settlement`; unlike price-order amendment, the Chase contract explicitly gives the path precedence, so the supplied body is preserved. Existing Chase IDs remain strings. Required result containers must be present, while empty lists remain valid. These structural checks are client safety rules, not a claim that every response field is schema-required. Strategies preserve failed state/error labels; returned IDs or HTTP 200 do not prove creation, execution or cancellation reached its terminal state. There are no automatic retries, polling, account lookups or financial actions.

## Futures standard single orders

The turn 12 retrospective corrected all Futures REST `DateTime` query-filter conversions: explicit `Local` values now represent their UTC instant, while `Unspecified` keeps its legacy UTC interpretation. Prefer UTC inputs. Existing Unix-second overloads, rounding and optional omission are unchanged; range checks compare transmitted timestamps. This is not a library-wide time-helper change. Countdown `triggerTime` is decoded as exact Unix **milliseconds**, including zero, accepting the documented integer and example's integer string; fractional, boolean, overflowing or out-of-date-range values fail instead of becoming a guessed/default date. Empty/null strategy or countdown bodies cannot bypass their required-container checks; actual empty strategy lists remain valid.

Current [creation](https://www.gate.com/docs/developers/apiv4/en/futures/#place-futures-order), [detail](https://www.gate.com/docs/developers/apiv4/en/futures/#query-single-order-details), [amendment](https://www.gate.com/docs/developers/apiv4/en/futures/#amend-single-order) and [cancellation](https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-single-order) are signed on BTC/USDT/USD1. Quantities are contracts, not currency units. Creation requires a literal contract and explicit `ImmediateOrCancel` when `Price=0`. `Close=true` requires `Size=0`; `AutoSize=CloseLong/CloseShort` additionally requires `ReduceOnly=true`. Omit `AutoSize` with null, not `None`. No instruction, account mode, precision, slippage bound or leverage is selected automatically.

For detail/amend/cancel, supply exactly one positive numeric `long` order ID or custom `t-` identifier. The latter permits at most 28 ASCII letters/digits/underscore/hyphen/dot after the prefix; routing syntax and whitespace are rejected before I/O. Custom-text lookup of an unfilled cancelled order expires after 60 seconds; filled/partially filled orders remain queryable by text. IDs and `PositionId` retain `long`; fractional, boolean, nonnumeric and overflowing identities fail rather than becoming another ID. Absent optional response IDs retain legacy defaults and do not identify an order. Shared response parsing also affects existing batch, Delivery and WebSocket consumers, without applying the single-order preflight to their routes.

Saved `GateFuturesOrderRequest` JSON requires non-null `contract`, `size` and `price`. Explicit unknown enum strings, blank/infinite decimals, underflow and decimal precision loss fail instead of being omitted or becoming zero. Use documented decimal strings: floating numeric JSON is rejected even with `FloatParseHandling.Decimal`, because a reader can round before the converter sees the value. Exact integer tokens and optional nulls remain supported; typed C# decimal requests still serialize as strings. Futures batch creation uses the same DTO and preflight.

Single amendment uses only optional `size`, `price`, `amend_text`, internal-user `text` and `action_mode`, following [FuturesOrderAmendment](https://www.gate.com/docs/developers/apiv4/en/futures/#futuresorderamendment) rather than the contradictory POST-style example containing `contract`. New size includes fills; at/below the filled quantity cancels, and original side/close/reduce-only constraints depend on server state. Explicit zero/empty values are preserved. Cancellation sends optional `action_mode` in the query, not the body. Existing `ReceiveWindow` supplies the optional Unix-millisecond `x-gate-exptime` header; null omits it. ACK/RESULT can return partial orders: inspect actual `Status`/`FinishAs`, not missing-field defaults or HTTP success, for execution/cancellation evidence. No automatic retry, polling or terminal-state synthesis is added.

## Futures price-triggered orders

All six [current price-order endpoints](https://www.gate.com/docs/developers/apiv4/en/futures/#query-auto-order-list) are signed and available on BTC, USDT and USD1 REST clients. Creation requires `Order` and `Trigger`, their documented prices, a literal contract and rule. For [amendment](https://www.gate.com/docs/developers/apiv4/en/futures/#modify-a-single-auto-order), optional string `Settlement` must exactly match the selected client's `btc`, `usdt` or `usd1`; `null` omits it. Blank, unknown and mismatched values fail before I/O and are not overwritten. Supply an actual created order ID for amendment, detail and single cancellation.

Creation supports `gtc`/`ioc`; the server default for omitted `tif` is `gtc`, so market-price creation requires explicit `ImmediateOrCancel`. The current explanation permits only `strategy_type=0`, despite listing 1 as a described strategy. `CloseLongOrder`/`CloseShortOrder` and `IsClose`/`IsReduceOnly` are read-only; construct a new request using writable `Close`/`ReduceOnly` instead. The POST example contradicts this restriction, so follow the parameter rules. To omit `AutoSize` in a DTO, use `null`, not `None`; the older non-nullable overload translates `None` into omission. `Amount` takes precedence over `Size`, but both supplied values are preserved. Position mode, required close flags, closing direction, contract precision and trigger relationships to live prices remain server-side; the wrapper does not fetch or infer them.

List queries accept `open`/`finished`, a positive limit and a nonnegative offset, retaining the existing client defaults of 100 and 0 without inventing an upper bound. For [bulk cancellation](https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-all-auto-orders), `contract = null` intentionally includes all eligible orders in the selected settlement. Explicit blank/malformed filters are rejected without trimming or widening scope. HTTP success is not blanket cancellation: inspect each order's `Status` and `FinishAs`; `succeeded` describes successful triggering, not execution fills. Optional create/amend IDs can be absent, so transport success alone is not proof of an identified new order. No polling or retry is added.

Response `initial`, `trigger`, `initial.contract`, both prices and `trigger.rule` are required in the current Futures and Delivery schemas; missing/null values now fail deserialization in the shared model. Valid Delivery contracts and outgoing requests are preserved; Futures-only preflight checks are not applied to Delivery. Existing enums, return types, string quantities, int64 IDs and timestamp accessors remain compatible.

The shared price-order models reject explicit unknown `order_type`, `pos_margin_mode`, `tif` and `auto_size` strings during JSON deserialization instead of discarding them as omitted values. This also applies to responses; an unsupported mapping is an error, not a guessed default. Numeric trigger enums accept exact integers or legacy integer strings, not fractional or boolean values that could be coerced into another trading instruction. Known mappings and null omission remain compatible; unrelated modules' converters are unchanged.

## TradFi CFD migration

The current [CFD API reference](https://www.gate.com/docs/developers/apiv4/en/cfd/) requires signed account, user activation, asset, commission, symbol-detail and order-submission requests. The three account response models no longer expose `Mt5Uid`; remove references to that retired identifier. `GateTradFiSymbolDetails.Leverage` is now the raw documented string, so consumers previously using its integer accessor must migrate too.

Use optional `GateTradFiOrderRequest.Leverage` to submit an integer multiplier. Null omits it, including through the unchanged positional overload. Permitted multipliers are symbol-dependent; the client does not select leverage, fetch account settings or infer a list format from the symbol-detail string. Pass one symbol/category per collection entry, not embedded CSV; detail queries accept at most ten symbols.

`PlaceOrderAsync` returns a queue task acknowledgement: `GateTradFiOrderId.Id` is **not an order ID** for update/cancel. It stays `long` by project policy, parsing numeric strings exactly; nonnumeric/out-of-range task IDs fail deserialization. Omitted IDs still default to zero and do not identify a task. Transport success is not proof of execution. Nonzero business `code` or nonempty `label` in unwrapped TradFi envelopes produces an error with HTTP metadata retained, without retry or polling. Optional response data is not synthesized into proof of an opened account or created order.

Personal trading remains the default. CFD lead trading is explicit and captured when constructing a separate client:

```csharp
using var cfdLeadApi = new GateRestApiClient(new GateRestApiClientOptions { TradFiLeadTrading = true });
```

Eligible TradFi requests use the request-scoped `x-gate-trader-copy-type: cfd_copy` header; user activation and both transaction methods are excluded. It does not change shared HTTP defaults, Stock context or an existing client's context when options are later modified. These examples are not safe to run as a batch against a live financial account.

## CrossEx symbols, position history and isolated margin

The current [CrossEx API reference](https://www.gate.com/docs/developers/apiv4/en/crossex/) is the contract for these three endpoints, reconciled for `v4.106.130/131`. LIGHTER order/transfer/quote support from `139` is reconciled separately below; neither scope is a claim that the entire CrossEx module is audited.

`GetSymbolsAsync` remains public and unsigned, including when credentials are configured. `GateCrossExSymbol.SupportsCross` and `SupportsRpi` map the documented string flags to `bool?`; missing/null means unknown, not false or permission to trade. Serialization writes lowercase string flags; legacy boolean tokens remain readable, while malformed values fail. `DelistTime` is milliseconds and zero means not delisted. The deprecated nullable `ContractSize`, legacy `DefaultLeverage` and captured nullable market-size metadata remain compatible. Pass one nonblank symbol per collection element, not embedded CSV; null/empty collections intentionally request all symbols, but invalid supplied elements are not dropped.

`GetHistoricalPositionsAsync` is signed. It preserves optional page/limit/symbol/from/to filters, with millisecond times and a documented maximum limit of 1000; historical-order `Attributes` are not forwarded. `GateCrossExHistoricalPosition.MarginMode` retains raw `CROSS`/`ISOLATED` or unfamiliar values, with no assumed mode on omission. `PositionId` and `UserId` stay nullable `long`: exact numeric strings/integers are accepted, but fractional/boolean/nonnumeric/overflow IDs now fail rather than selecting another identity. No accessor type migration is required.

Prefer UTC for position-history filters. This endpoint now converts explicit `DateTimeKind.Local` instants to UTC before validation/serialization; `Unspecified` retains the existing UTC interpretation. The shared time helper and other endpoint contracts are unchanged.

Symbol metadata requires all 14 documented non-optional keys rather than fabricating missing limits as zero. Two required keys, `max_market_size` and deprecated `contract_size`, still permit null because captured legacy venue responses contain it despite the string schema. The other required values reject null. Symbol and historical-position monetary fields accept exact decimal strings/integers; lossy floating tokens, underflow/overflow and blank values fail rather than being rounded or replaced with zero/null. Symbol order counts and delisting timestamps also require exact Int64 values. These scoped parsing changes keep existing accessor types but require saved response JSON to follow the documented wire shapes; serialization writes decimal strings.

`UpdateIsolatedMarginAsync` sends a signed POST to `/crossex/positions/margin`, only for Hyperliquid isolated futures positions. Use an explicit `GateCrossExIsolatedMarginRequest` or the symbol/margin/optional-side overload. Positive margin increases and negative margin decreases; the client writes the supplied `decimal` unchanged as an invariant string. **The server truncates beyond two decimal places.** Nullable `PositionSide` is omitted; the server defaults to `NONE` for one-way positions, and the wrapper never selects a hedge side or changes margin mode. Availability, isolated-position eligibility and available funds remain server-side.

HTTP 202 is acceptance only, not completed adjustment. `GateCrossExIsolatedMarginResponse.Margin` is the returned adjustment for this request, not resulting total position margin; returned symbol/side are not filled from the input. Missing required symbol/margin, absent response objects and missing list containers fail with HTTP metadata retained; empty arrays remain valid. Saved request JSON must supply symbol and an exact decimal string/integer margin. Unknown side mappings and lossy floating tokens fail instead of silently changing the financial instruction. Calling this method is an explicit financial mutation; it adds no automatic lookups, mode changes, retries or polling. Verification used no live exchange calls.

## CrossEx LIGHTER orders, transfers and quotes

The entire current signed POST contracts for `/crossex/orders`, `/crossex/transfers` and `/crossex/convert/quote` are reconciled using [v4.106.139](https://www.gate.com/docs/developers/apiv4/en/#changelog) as the index and the [endpoint reference](https://www.gate.com/docs/developers/apiv4/en/crossex/) plus the [CrossEx error guide](https://www.gate.com/docs/developers/crossex/) as the specification. Existing methods, convenience overloads and accessor types remain; `Lighter=9` and `CrossExLighter=10` append enum members without renumbering earlier values.

For futures orders, pass a symbol such as `LIGHTER_FUTURE_ADA_USDC` unchanged. Lighter has no dedicated spot/margin legs in CrossEx. Orders require an explicit side and positive base quantity except spot/margin market buys, which require positive quote quantity. Limit orders, including omitted `Type` (server default LIMIT), require a price. Optional type/time-in-force/reduce-only/position-side stay omitted when null, preserving the documented defaults without choosing account mode or a hedge side. Explicit `false`/`NONE` remain string tokens, and RPI remains available for eligible limit orders; market orders reject POC/RPI. Margin orders require explicit LONG/SHORT. Supplied order text must be shorter than 64 characters and use only `a-z`, `0-9`, `-`, `_`; uppercase example placeholders must be replaced. The documented rate is 100 requests per 10 seconds, with at most 1,000 open orders per user.

USDC transfers support `Spot` to/from `CrossExLighter`, serialized as `SPOT` and `CROSSEX_LIGHTER`. Coin, amount and source/destination are preserved; optional transfer text is omitted when null. The transfer rate is 10 requests per 10 seconds. Currency availability, minimum transfer amounts, fees and balance remain server checks; no alternative transfer path is selected automatically.

For LIGHTER quotes, use `ExchangeType=Lighter` and the documented `LIGHTER_USDC` / `CROSSEX_USDT` asset names in either direction. Cross-exchange mode is required; isolated-exchange mode is not supported for these swaps. The wrapper sends the caller's asset names unchanged and does not translate generic currency names or change account mode. The quote endpoint supports BINANCE/OKX/GATE/BYBIT/HYPERLIQUID/KRAKEN/LIGHTER, not the shared enum's CROSSEX/DERIBIT members. Assets must differ, and the amount must be positive with at most 16 transmitted decimal places: excess scale, including trailing decimal places, is rejected, never rounded. Venue-specific directions, dynamic maximum amount and eligibility remain server checks. The quote rate is 100 requests per day; requesting a quote never executes it.

Saved `GateCrossExOrderRequest`, `GateCrossExTransferRequest` and `GateCrossExConvertQuoteRequest` now use documented snake_case names, mapped string enums and invariant decimal strings; migrate older PascalCase/numeric-enum/numeric-fraction snapshots. Every schema-required field must be present and non-null; conditional order requirements are checked before HTTP. Unknown enum strings, token coercion and precision loss fail instead of altering an instruction. Optional nulls remain omitted, and documented legacy boolean reduce-only tokens remain readable. HTTP request bodies continue to use the same existing signed transport.

Transfer acknowledgements require both `tx_id` and `text`. Quotes require all seven documented fields and exact monetary values. `GateCrossExConvertQuote.ValidMilliseconds` stays `long`, accepts exact Int64 strings/integers and writes the documented string token when saved. The docs call `valid_ms` a millisecond validity timestamp but illustrate `5000`; no duration, expiry date or clock origin is inferred. Quote IDs and action/transfer IDs already typed as `string` remain opaque strings; no existing `long` identity changes type. Saved response decimal fields now write strings; lossy floating tokens and missing required values fail. Order acknowledgements require a nonblank order ID, but do not fabricate optional echoed text.

A valid order acknowledgement is asynchronous acceptance, not venue acceptance or execution. Later `FAIL` means CrossEx validation failed; `REJECT` means the venue rejected the order. HTTP errors retain status, raw data and the stable label in `Error.Data`; use the label, not diagnostic `message`/`detail`, for programmatic decisions. Malformed/empty HTTP-success payloads also fail. Tick/lot size, balances, position-mode compatibility, RPI access and risk limits remain server-side. The LIGHTER capacity error is preserved without placing taker trades to replenish capacity. There are no automatic retries, order queries, transfers, quote execution or mode changes. All verification was offline; the adjacent missing GET/POST margin-mode family remains separate work.

## OTC fiat order creation

`CreateFiatOrderAsync(GateOtcFiatOrderRequest)` follows the complete current [fiat-order contract](https://www.gate.com/docs/developers/apiv4/en/otc/#create-fiat-order). `Side` validates the quote's `side` (`PAY`/`GET`), not its `order_type` (`FIAT`/`STABLE`). Legacy `FIAT`/`CRYPTO` also work; `STABLE` is rejected for this endpoint but remains valid quote metadata. The original convenience signature and its C# `Side=Fiat` default are retained. New integrations should explicitly copy the actual quote side into the matching `GateOtcOrderKind` value, not infer it from BUY/SELL.

Optional `ReceiveType` maps `Company`/`Gate`/`Recipient`/`Person` to `YOU`/`GATE`/`RECIPIENT`/`PERSON`. Corporate accounts may use YOU/GATE/RECIPIENT; individual accounts may use GATE/PERSON. Null omits the field; there is no inferred account type or default remittance name. `BankId` remains `long` and is sent as an exact integer string. Supply an actual positive bank ID and nonblank quote token/currencies; these identity/token checks are client safety constraints, not a guessed default bank. No quote/bank lookup, amount substitution, rounding, minimum amount or account-eligibility decision is made locally.

Saved fiat request JSON now uses the ten documented snake_case keys, with eight required nonnull keys. Migrate older PascalCase saved DTOs to this wire shape. Enum instructions must use their documented strings; amounts must be exact decimal strings or integers, and bank IDs exact Int64 integers/numeric strings. Unknown enum values, lossy floating tokens, decimal precision loss and fractional/boolean/overflowing IDs fail rather than changing the instruction. C# accessor types are unchanged; standalone DTO serialization keeps BankId numeric, while HTTP construction writes the required bank_id string.

Nonzero business `code` is an error even with HTTP 200; explicit business codes also survive HTTP-error envelopes separately from the HTTP status. A successful acknowledgement requires integer code=0, a string message and an Int64 integer timestamp; absent/malformed bodies fail with HTTP metadata retained and payload-free parser diagnostics. The OtcActionResponse schema does not specify a timestamp unit. Fiat creation retains its existing Unix-second DateTime interpretation for compatibility, not as a newly verified wire-unit guarantee. It contains no order ID and does not establish payment/remittance completion. No automatic retry or polling is added, including after ambiguous responses. The reconciled scope includes fiat creation, pre-upload, bank creation, the two supplements and order/paid; other OTC envelopes remain outside this catch-up scope.

## OTC temporary upload credentials

`CreatePreUploadAsync` implements signed POST `/otc/upload/pre_upload` using the complete current [endpoint and linked schemas](https://www.gate.com/docs/developers/apiv4/en/otc/#pre-upload-file-temporary-bucket). Use the DTO or content-type/optional-scene overload. Png/Jpeg/Jpg/Pdf map to the four exact base64 MIME strings; no file bytes are accepted. Null scene is omitted, leaving the server's `general` default; explicit General/Bank/Assessment/Credit are supported. Unknown instructions fail before I/O and in saved request JSON.

```csharp
var preUpload = await api.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png, GateOtcUploadScene.Bank);
// Only if preUpload.Success: preUpload.Data.Data has FileKey, Url, Fields and ExpiresIn.
// Credential issuance does not upload a file, bind a bank or confirm payment. Do not log these values.
```

The result retains the complete code/message/data/timestamp acknowledgement. Nonzero business codes fail even with HTTP 200; an explicit valid business code in an HTTP-error envelope is also preserved separately from the HTTP status. Success requires all four envelope keys, four data keys and seven case-sensitive Policy keys; malformed/missing values fail without fabricated credentials. Timestamp is exact integer Unix seconds in UTC; ExpiresIn is the returned integer, not a forced 5400-second constant. Opaque fields remain strings, including future string fields and date-looking values in saved JSON; no casing, decoding or date normalization is applied to signed form pairs.

The caller performs a separate direct S3 POST using the returned URL and every Fields pair unchanged, with the file part last. The current Policy allows 1..10485760 bytes and expires after the returned validity (currently 90 minutes). The wrapper never follows that URL, forwards Gate authentication, uploads, refreshes credentials or retries automatically. Do not decode FileKey when later passing it to bank/create or order/paid. The server checks ownership and object existence at business submission; issuing credentials or an S3 HTTP 204 is not business approval.

Policy credentials are sensitive. The scoped parser does not copy malformed credentials into Error.Data or parser diagnostics. With the user's approved logging change, all Gate REST clients suppress ApiSharp's payload/header diagnostics and emit only operation metadata (method, endpoint path, elapsed time, HTTP status and error/exception type/code). No exception object/message, response/error body, query values or authentication header is attached to those logs. A successful transport log is not business success. Explicit ILogger and the configured BaseClient.LoggerFactory remain usable for these metadata logs. WebSocket logging, custom HttpClient handlers and caller logging are outside this change.

Returned diagnostics are not globally redacted: RawResponse still captures a success body when explicitly enabled, and ApiSharp retains HTTP-error bodies in `Raw` even when that option is disabled. Error messages/data, request headers, DTOs and Fields can also contain sensitive values. Never log the whole result or DTO merely because library logs are now metadata-only. No client option or returned error/raw data is silently changed.

## OTC bank card submission

`CreateBankCardAsync(GateOtcBankCreateRequest)` follows the complete current [bank-creation endpoint and schemas](https://www.gate.com/docs/developers/apiv4/en/otc/#create-bank-card). Its Task/RestCallResult return shape stays intact; **GateOtcBankCreateResult.BankId is now long**, as approved. Update callers that assign it to int. Bank account name/name/country/address/IBAN/SWIFT remain required. Three routing/correspondent-bank fields are optional. All supplied strings remain unchanged; account-name Base64 requirements depend on the actual gateway and are not selected automatically.

Choose exactly one proof input: DocumentationFileKey with required FileType, raw DocumentationUpload, or the existing Base64 DocumentationFile. Key and FileType are sent unchanged as form values; this endpoint accepts plaintext or Base64 forms. DocumentationUpload is a local GateOtcFileUpload carrying bytes, a safe filename and optional plaintext MIME header. It creates one real documentation_file part, not an extra documentation_upload form field. No disk path is read. Omitted MIME uses application/octet-stream without sniffing bytes. Legacy DocumentationFile must be valid Base64 file content, not a placeholder or data URL; it is decoded into a real file part with the fallback name documentation_file and octet-stream MIME. Use DocumentationUpload when actual filename/MIME metadata is needed. Empty content and unsafe file metadata fail before I/O as client safety checks; no undocumented direct-upload size/format limit is invented.

Binary multipart is assembled once and its exact bytes are hashed and sent through the existing GateRequest/HttpClient, retaining request-body metadata redaction. JSON/text signatures are unchanged; supplements now reuse this byte transport for real file parts too. Replacing the protected request factory with a non-Gate implementation does not support this new binary path and fails explicitly. No new HTTP stack, dependency, S3 upload or automatic downstream action is added.

HTTP 200 with a nonzero business code is an error; an explicit valid business code in an HTTP-error envelope is preserved separately from the HTTP status. Success requires integer code=0, string message, a data object and exact integer bank_id/status; missing/malformed values fail with HTTP metadata retained. Status remains an open int, not an inferred approved/pending enum. Timestamp is optional raw long? because its unit is unspecified. Root Code/Message/Timestamp are exposed on the existing flattened result but marked JsonIgnore to preserve the two-field bank data JSON shape. To save the whole acknowledgement, serialize GateOtcBankCreateResponse with those root values and Data. Saved bank request JSON now uses snake_case keys, so older PascalCase snapshots need migration; documentation_upload is local saved-input metadata whose byte content is Base64 in JSON. Required strings and integer fields reject coercion, while date-looking strings remain intact.

This method submits bank materials when explicitly called; a successful acknowledgement does not establish review approval, usable banking details or financial settlement. Server ownership/object-existence checks can reject a key, and Global non-same-name accounts may need manual review. No automatic upload, retry, lookup or polling is performed.

## OTC supplementary materials and payment notification

The complete current [personal](https://www.gate.com/docs/developers/apiv4/en/otc/#submit-bank-card-supplement-materials-personal) and [enterprise](https://www.gate.com/docs/developers/apiv4/en/otc/#submit-bank-card-supplement-materials-enterprise) supplement contracts require BankId, not every direct-file field. Select materials from the current checklist and the matching user_type. Personal files are IdDocumentFront/IdDocumentBack/AddressProof; enterprise files are Certificate/ShareHolders/Passport/ShareHoldingStructure/FundsStatement/Additional, with optional UserId. The client does not look up the checklist or invent account eligibility, required categories or a minimum-file count. The server still validates business requirements.

Each existing Base64 string is optional and decoded into a real multipart file part. Alternatively, set its matching `...Upload` property to GateOtcFileUpload; use exactly one local representation per material. Multiple files and RelationshipProof may be mixed. Legacy files use the field name and application/octet-stream; raw uploads retain safe filenames and plaintext MIME metadata. Empty/invalid content, duplicate representations and unsafe headers fail before HTTP, including errors in a later file. No undocumented supplement size/format cap or MIME sniffing is added. Exact assembled bytes are signed, and recorded multipart request metadata omits the content.

RelationshipProof remains caller-supplied JSON text, sent unchanged: the documentation does not publish its full category/container schema, so no typed shape is invented. Unlike bank/create and order/paid, each pre-upload item inside this JSON requires the **plaintext object path** (decode the pre-upload FileKey, or use the identical returned Fields["key"]) and **plaintext MIME**. The endpoint performs encoding before persistence. The wrapper does not decode arbitrary strings or build/merge this JSON automatically. Direct-upload material fields and this text can coexist; actual checklist/gateway rules remain the caller's responsibility.

The complete current [order/paid contract](https://www.gate.com/docs/developers/apiv4/en/otc/#mark-fiat-order-as-paid-deposit-confirmation) sends JSON with required OrderId and PaymentReceiptFileKey; ClientOrderId and PaymentReceipt are optional gateway-compatible fields, not substitutes for the required fields on this path. Both receipt fields are forwarded unchanged, without guessing gateway precedence or rewriting identity. Pass the pre-upload base64 FileKey unchanged; legacy production-bucket keys remain supported. The service/gateway validates ownership, existence and the documented jpg/jpeg/png/pdf and 10 MB limits. This request carries no file bytes and does not automatically upload or inspect an object. Its acknowledgement does not prove a bank transfer settled or the order reached a terminal state.

All three operations require the full code/message/integer timestamp acknowledgement and reject nonzero business codes even with HTTP 200. Explicit HTTP-error business codes, status and Raw are preserved without retry; malformed success bodies produce generic null-data parser errors. **Timestamp stays DateTime by the user's explicit compatibility decision.** Supplements and payment notification retain the shared converter's existing unit heuristic and 0/-1 default-date sentinel; fiat creation retains its existing seconds interpretation. These are legacy views, not units guaranteed by OtcActionResponse. Saved action DTOs retain the existing millisecond converter shape and cannot reproduce every raw timestamp exactly; retain the raw response yourself if exact wire evidence is needed, without logging sensitive bodies.

Saved submission requests now use current snake_case keys; migrate older PascalCase snapshots. Only schema-required identity/receipt fields are required in saved JSON, and all supplied scalar strings reject numeric/date coercion. The matching `..._upload` nested fields are local saved-input metadata, never additional HTTP parts; their bytes use Base64 JSON. Example empty byte arrays deliberately fail before HTTP and must be replaced with real checklist materials. `v4.106.135`'s indexed endpoints and these dependent submission contracts are reconciled, subject to the explicit retained DateTime compatibility exception; this does not claim all OTC endpoints or live financial acceptance are verified.

## Unified account snapshots

`GetAccountInfoAsync` is signed GET `/unified/accounts`, with optional currency and `long?` sub-account filters; the legacy positional cancellation-token overload remains compatible. The complete current [endpoint and schemas](https://www.gate.com/docs/developers/apiv4/en/unified/#get-unified-account-information) expose 22 account fields and 21 per-currency balance fields, already represented locally. Risk values are server-calculated and account-mode-dependent; the updated [margin-formula reference](https://www.gate.com/help/unified-account/risk_control_mechanism/33018) concerns multi-currency mode. The wrapper adds no risk calculator and does not apply that formula to every mode.

All schema properties are optional. Partial objects remain readable, and legacy nonnullable defaults are retained without treating omitted values as zero risk, usable margin or an unlocked account. Missing mode is undefined enum zero, not Classic. Optional collateral/funding flags remain nullable; raw funding/funding_version strings are retained. Deprecated total/leverage and balance fields stay available with updated XML descriptions. The source gives no refresh_time unit, so its existing converter is retained, not replaced with an invented unit.

UserId/SubAccountId/BalanceVersion keep their exact long types. Existing typed monetary fields accept exact decimal strings/integers and write strings; fractional/boolean/overflowing IDs and decimal rounding/underflow/overflow fail. Saved query JSON uses currency/sub_uid; migrate older PascalCase saved query DTOs. Empty/null or non-object HTTP-success responses are parse failures, not fabricated zero-balance snapshots; a real `{}` remains a valid partial object, not financial evidence.

## Rest Api Examples

```csharp
var api = new GateRestApiClient();
api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

// Wallet Methods
var wallet_01 = await api.Wallet.WithdrawAsync("CURRENCY", 1.0m, "CHAIN", "ADDRESS", "MEMO", "CLIENT-ORDER-ID");
var wallet_01b = await api.Wallet.WithdrawAsync(new GateWalletWithdrawalRequest
{
    Currency = "CURRENCY",
    Amount = 1.0m,
    Chain = "CHAIN",
    Address = "ADDRESS",
    Memo = "MEMO",
    WithdrawalOrderId = "CLIENT-ORDER-ID",
});
var wallet_02 = await api.Wallet.TransferAsync(1_000_000_000, "CURRENCY", 1.0m);
var wallet_03 = await api.Wallet.CancelWithdrawalAsync(1_000_000_000);
var wallet_04 = await api.Wallet.GetCurrencyChainsAsync("CURRENCY");
var wallet_05 = await api.Wallet.GetDepositAddressAsync("CURRENCY");
var wallet_06 = await api.Wallet.GetWithdrawalsAsync();
var wallet_06b = await api.Wallet.GetWithdrawalsAsync(new GateWalletWithdrawalQueryRequest
{
    Currency = "CURRENCY",
    From = DateTime.UtcNow.AddDays(-7),
    To = DateTime.UtcNow,
});
var wallet_07 = await api.Wallet.GetDepositsAsync();
var wallet_08 = await api.Wallet.TransfersBetweenTradingAccountsAsync("CURRENCY", GateWalletAccountType.Spot, GateWalletAccountType.Futures, 100.0m);
var wallet_08b = await api.Wallet.TransfersBetweenTradingAccountsAsync(new GateWalletTransferRequest
{
    Currency = "CURRENCY",
    From = GateWalletAccountType.Spot,
    To = GateWalletAccountType.Futures,
    Amount = 100.0m,
    Settle = "USDT",
});
var wallet_08c = await api.Wallet.GetTradingAccountTransferAsync("59636381286");
var wallet_09 = await api.Wallet.TransferBetweenMainAndSubAccountsAsync("CURRENCY", 1_000_000_000, GateWalletTransferDirection.From, 100.0m);
var wallet_10 = await api.Wallet.GetTransfersBetweenMainAndSubAccountsAsync();
var wallet_11 = await api.Wallet.TransferBetweenSubAccountsAsync("CURRENCY", 1_000_000_000, GateWalletSubAccountType.Spot, 2_000_000_000, GateWalletSubAccountType.Futures, 100.0m);
var wallet_12 = await api.Wallet.GetWithdrawalStatusAsync();
var wallet_13 = await api.Wallet.GetSubAccountBalancesAsync();
var wallet_14 = await api.Wallet.GetSubAccountMarginBalancesAsync();
var wallet_15 = await api.Wallet.GetSubAccountFuturesBalancesAsync();
var wallet_16 = await api.Wallet.GetSubAccountCrossMarginBalancesAsync();
var wallet_17 = await api.Wallet.GetSavedAddressesAsync(new GateWalletSavedAddressQueryRequest { Chain = "CHAIN", Verified = true, Limit = 25, Page = 1 });
var wallet_18 = await api.Wallet.GetTotalBalancesAsync();
var wallet_19 = await api.Wallet.GetLowCapExchangeListAsync();

// SubAccount Methods
var subaccount_01 = await api.SubAccount.CreateSubAccountAsync(new GateSubAccountCreateRequest { Login = "LOGIN-NAME", Password = "PASSWORD", Email = "EMAIL", Remark = "REMARKS" });
var subaccount_02 = await api.SubAccount.GetSubAccountsAsync();
var subaccount_03 = await api.SubAccount.GetSubAccountAsync(1_000_000_000);
var subaccount_04 = await api.SubAccount.CreateApiKeyAsync(1_000_000_000, new GateSubAccountApiKeyRequest
{
    Name = "spot",
    Permissions = new List<GateSubAccountApiKeyPermission>
    {
        new GateSubAccountApiKeyPermission { Name = GateSubAccountApiKeyPermissionSection.Spot, ReadOnly = false }
    },
});
var subaccount_05 = await api.SubAccount.GetApiKeysAsync(1_000_000_000);
var subaccount_06 = await api.SubAccount.UpdateApiKeyAsync(1_000_000_000, "API-KEY", new GateSubAccountApiKeyRequest { IpWhitelist = new List<string> { "127.0.0.1" } });
var subaccount_07 = await api.SubAccount.DeleteApiKeyAsync(1_000_000_000, "API-KEY");
var subaccount_08 = await api.SubAccount.GetApiKeyAsync(1_000_000_000, "API-KEY");
var subaccount_09 = await api.SubAccount.LockSubAccountAsync(1_000_000_000);
var subaccount_10 = await api.SubAccount.UnlockSubAccountAsync(1_000_000_000);

// Unified Methods
var unified_01 = await api.Unified.GetAccountInfoAsync(new GateUnifiedAccountInfoRequest { Currency = "CURRENCY", SubAccountId = 1_000_000_000 });
var unified_02 = await api.Unified.GetBorrowableAsync("CURRENCY");
var unified_03 = await api.Unified.GetTransferableAsync("CURRENCY");
var unified_04 = await api.Unified.GetTransferablesAsync(new List<string> { "BTC", "ETH" });
var unified_05 = await api.Unified.GetBatchBorrowableAsync(new List<string> { "BTC", "ETH" });
var unified_06 = await api.Unified.BorrowOrRepayAsync(new GateUnifiedLoanRequest { Currency = "CURRENCY", Type = GateUnifiedLoanDirection.Borrow, Amount = 100.0m, Text = "CLIENT-ID" });
var unified_07 = await api.Unified.RepayAsync("CURRENCY", 100.0m, true);
var unified_08 = await api.Unified.GetLoansAsync(new GateUnifiedLoanQueryRequest { Currency = "CURRENCY", Type = GateUnifiedLoanType.Platform });
var unified_09 = await api.Unified.GetLoanHistoryAsync(new GateUnifiedLoanRecordQueryRequest { Currency = "CURRENCY", Type = GateUnifiedLoanDirection.Borrow });
var unified_10 = await api.Unified.GetInterestHistoryAsync(new GateUnifiedInterestRecordQueryRequest { Currency = "CURRENCY", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var unified_11 = await api.Unified.GetRiskUnitsAsync();
var unified_12 = await api.Unified.SetAccountModeAsync(new GateUnifiedAccountModeRequest { Mode = GateUnifiedAccountMode.Portfolio, Settings = new GateUnifiedAccountModeSettings { SpotHedge = true, Options = true } });
var unified_13 = await api.Unified.GetAccountModeAsync();
var unified_14 = await api.Unified.GetEstimatedLendingRatesAsync(new List<string> { "BTC", "ETH" });
var unified_15 = await api.Unified.GetCurrencyDiscountTiersAsync();
var unified_16 = await api.Unified.GetLoanMarginTiersAsync();
var unified_17 = await api.Unified.CalculatePortfolioAsync(new GateUnifiedPortfolioCalculatorRequest { SpotHedge = true });
var unified_18 = await api.Unified.GetLeverageConfigsAsync("CURRENCY");
var unified_19 = await api.Unified.GetLeverageSettingsAsync();
var unified_20 = await api.Unified.SetLeverageSettingsAsync(new GateUnifiedLeverageSettingRequest { Currency = "CURRENCY", Leverage = 10 });
var unified_20b = await api.Unified.SetAllLeverageSettingsAsync(10);
var unified_21 = await api.Unified.GetCurrenciesAsync();
var unified_22 = await api.Unified.GetHistoricalLendingRatesAsync(new GateUnifiedHistoricalLendingRatesQueryRequest { Currency = "CURRENCY", Tier = "1" });
var unified_23 = await api.Unified.SetCollateralCurrenciesAsync(new GateUnifiedCollateralCurrenciesRequest { Type = GateUnifiedCollateralType.Custom, EnableList = new List<string> { "BTC" }, DisableList = new List<string> { "ETH" } });
var unified_24 = await api.Unified.GetEstimatedQuickRepaymentAsync();
var unified_25 = await api.Unified.CreateQuickRepaymentAsync(new GateUnifiedQuickRepaymentRequest { DebtCurrencies = new List<string> { "BTC" }, AvailableCurrencies = new List<string> { "USDT" } });
var unified_26 = await api.Unified.GetDeltaNeutralAsync();
var unified_27 = await api.Unified.SetDeltaNeutralAsync(true);

// Spot Methods
var spot_01 = await api.Spot.GetCurrenciesAsync();
var spot_02 = await api.Spot.GetCurrencyAsync("CURRENCY");
var spot_03 = await api.Spot.GetMarketsAsync();
var spot_04 = await api.Spot.GetMarketAsync("SYMBOL");
var spot_05 = await api.Spot.GetTickersAsync();
var spot_06 = await api.Spot.GetOrderBookAsync("SYMBOL");
var spot_07 = await api.Spot.GetTradesAsync(new GateSpotTradeQueryRequest { Symbol = "SYMBOL", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Limit = 100 });
var spot_08 = await api.Spot.GetPrivateTradesAsync(new GateSpotTradeQueryRequest { Symbol = "SYMBOL", Limit = 100 }); // Private (Signed)
var spot_09 = await api.Spot.GetCandlesticksAsync(new GateSpotCandlestickQueryRequest { Symbol = "SYMBOL", Interval = GateSpotCandlestickInterval.FourHours, Limit = 100 });
var spot_10 = await api.Spot.GetUserFeeRatesAsync(["SYMBOL"]);
var spot_11 = await api.Spot.GetBalancesAsync();
var spot_12 = await api.Spot.PlaceOrdersAsync(new List<GateSpotOrderRequest>
{
    new GateSpotOrderRequest
    {
        ClientOrderId = "t-batch-order",
        Symbol = "SYMBOL",
        Account = GateSpotAccountType.Spot,
        Side = GateSpotOrderSide.Buy,
        Type = GateSpotOrderType.Limit,
        TimeInForce = GateSpotTimeInForce.GoodTillCancelled,
        Amount = 1.0m,
        Price = 1.0m,
        StopProfit = new GateSpotOrderTpsl { TriggerPrice = "1.10", OrderPrice = "1.09" },
        StopLoss = new GateSpotOrderTpsl { TriggerPrice = "0.90", OrderPrice = "0.89" }
    }
}
);
var spot_13 = await api.Spot.GetOpenOrdersAsync(new GateSpotOpenOrdersRequest { Account = GateSpotAccountType.Spot, Limit = 100 });
var spot_14 = await api.Spot.CloseLiquidatedPositionsAsync(new GateSpotCloseRequest
{
    Symbol = "SYMBOL",
    Price = 1001.01m,
    ProcessingMode = GateSpotActionMode.Full,
});
var spot_15 = await api.Spot.PlaceOrderAsync("SYMBOL", GateSpotAccountType.Spot, GateSpotOrderType.Market, GateSpotOrderSide.Buy, GateSpotTimeInForce.ImmediateOrCancel, 100.01m);
var spot_16 = await api.Spot.PlaceOrderAsync(new GateSpotOrderRequest
{
    Symbol = "SYMBOL",
    Account = GateSpotAccountType.Spot,
    Type = GateSpotOrderType.Limit,
    Side = GateSpotOrderSide.Buy,
    TimeInForce = GateSpotTimeInForce.GoodTillCancelled,
    Amount = 1.0m,
    Price = 1.0m,
    StopProfit = new GateSpotOrderTpsl { TriggerPrice = "1.10", OrderPrice = "1.09" },
    StopLoss = new GateSpotOrderTpsl { TriggerPrice = "0.90", OrderPrice = "0.89" }
});
var spot_17 = await api.Spot.GetOrdersAsync(new GateSpotOrderQueryRequest { Symbol = "SYMBOL", Status = GateSpotOrderQueryStatus.Open, Account = GateSpotAccountType.Spot, Limit = 100 });
var spot_18 = await api.Spot.CancelOrdersAsync("SYMBOL");
var spot_19 = await api.Spot.GetOrderAsync("SYMBOL", 1_000_000_000);
var spot_20 = await api.Spot.CancelOrderAsync("SYMBOL", 1_000_000_000);
var spot_21 = await api.Spot.GetTradeHistoryAsync(new GateSpotTradeHistoryQueryRequest { Symbol = "SYMBOL", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Limit = 100 });
var spot_22 = await api.Spot.GetServerTimeAsync();
var spot_23 = await api.Spot.CancelAllAsync(new GateSpotCountdownCancelAllRequest { Timeout = 30, Symbol = "SYMBOL" });
var spot_24 = await api.Spot.PlacePriceTriggeredOrderAsync(
    "SYMBOL",
    100.01m,
    GateSpotTriggerCondition.GreaterThanOrEqualTo,
    TimeSpan.FromMinutes(15),
    GateSpotAccountType.Spot,
    GateSpotOrderType.Limit,
    GateSpotOrderSide.Buy,
    GateSpotTriggerTimeInForce.GoodTillCancelled,
    100.00m, 100.02m, "CLIENT-ORDER-ID"
    );
var spot_25 = await api.Spot.PlacePriceTriggeredOrderAsync(new GateSpotPriceTriggeredOrderRequest
{
    Symbol = "SYMBOL",
    Trigger = new GateSpotTriggerPrice
    {
        Price = "100.01",
        Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo,
        Expiration = Convert.ToInt32(TimeSpan.FromMinutes(15).TotalSeconds),
    },
    Order = new GateSpotTriggerOrder
    {
        Account = GateSpotPriceTriggeredOrderAccountType.Normal,
        Type = GateSpotOrderType.Limit,
        Side = GateSpotOrderSide.Buy,
        TimeInForce = GateSpotTriggerTimeInForce.GoodTillCancelled,
        Price = "100.00",
        Amount = "100.02",
        ClientOrderId = "CLIENT-ORDER-ID"
    }
});
var spot_26 = await api.Spot.GetPriceTriggeredOrdersAsync(new GateSpotPriceTriggeredOrderQueryRequest { Status = GateSpotTriggerFilter.Open, Account = GateSpotPriceTriggeredOrderAccountType.Normal, Symbol = "SYMBOL" });
var spot_27 = await api.Spot.CancelPriceTriggeredOrdersAsync();
var spot_28 = await api.Spot.GetPriceTriggeredOrderAsync();
var spot_29 = await api.Spot.CancelPriceTriggeredOrderAsync();
var spot_30 = await api.Spot.AmendOrderAsync(new GateSpotAmendRequest { Symbol = "SYMBOL", OrderId = 1_000_000_000, Price = "1.01", StopProfit = new GateSpotOrderTpsl { TriggerPrice = "1.10", OrderPrice = "1.09" } });
var spot_31 = await api.Spot.AmendOrdersAsync([new GateSpotAmendRequest { Symbol = "SYMBOL", ClientOrderId = "t-batch-order", Amount = "0.5", StopLoss = new GateSpotOrderTpsl() }]); // Empty object cancels stop loss; null leaves it unchanged.
var spot_32 = await api.Spot.GetPovOrdersAsync(new GateSpotPovOrderQueryRequest { Status = GateSpotOrderQueryStatus.Open, Symbol = "SYMBOL", Limit = 100 });
var spot_33 = await api.Spot.PlacePovOrderAsync(new GateSpotPovOrderRequest { Symbol = "SYMBOL", Side = GateSpotOrderSide.Buy, Amount = 1m, ParticipationRate = GateSpotPovParticipationRate.FivePercent, TimeToLive = GateSpotPovTimeToLive.OneHour, LimitPrice = 1m, ClientOrderId = "t-pov-order" });
var spot_34 = await api.Spot.GetPovOrderAsync("POV-ORDER-ID");
var spot_35 = await api.Spot.CancelPovOrderAsync("POV-ORDER-ID");
var spot_36 = await api.Spot.CancelPovOrdersAsync("SYMBOL"); // Omit SYMBOL only when intentionally cancelling every eligible Spot POV order.
// POV cancellations use signed DELETE /spot/pov_orders/{order_id} and DELETE /spot/pov_orders, without a request body.
// POV cancel responses can still contain a non-terminal status such as CREATED or CANCELING. Confirm the returned list/order and follow-up status before treating cancellation as complete.

// Isolated Margin Methods
var margin_01 = await api.IsolatedMargin.GetBalancesAsync("SYMBOL");
var margin_02 = await api.IsolatedMargin.GetBalanceHistoryAsync(new GateMarginBalanceHistoryQueryRequest { Symbol = "SYMBOL", Currency = "CURRENCY", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var margin_03 = await api.IsolatedMargin.GetFundingBalancesAsync("CURRENCY");
var margin_04 = await api.IsolatedMargin.SetAutoRepaymentAsync(GateMarginAutoRepaymentStatus.Enabled);
var margin_05 = await api.IsolatedMargin.GetAutoRepaymentAsync();
var margin_06 = await api.IsolatedMargin.GetTransferableAmountAsync(new GateMarginTransferableAmountRequest { Currency = "CURRENCY", Symbol = "SYMBOL" });
var margin_07 = await api.IsolatedMargin.GetMarketsAsync();
var margin_08 = await api.IsolatedMargin.GetMarketsAsync("SYMBOL");
Console.WriteLine($"Explicitly enabled: {margin_08.Success && margin_08.Data?.Status == "enabled"}; raw delisting time: {margin_08.Data?.DelistedTime}"); // Missing/unknown status is unconfirmed; enabled is not a borrowing guarantee.
var margin_09 = await api.IsolatedMargin.GetEstimatedInterestRateAsync(new List<string> { "BTC", "ETH" });
var margin_10 = await api.IsolatedMargin.BorrowOrRepayAsync(new GateMarginLoanRequest { Symbol = "SYMBOL", Currency = "CURRENCY", Type = GateMarginUniOrderType.Borrow, Amount = 100.0m });
var margin_11 = await api.IsolatedMargin.RepayAsync("SYMBOL", "CURRENCY", 100.0m, true);
var margin_12 = await api.IsolatedMargin.GetLoansAsync(new GateMarginLoanQueryRequest { Symbol = "SYMBOL", Currency = "CURRENCY" });
var margin_13 = await api.IsolatedMargin.GetLoanHistoryAsync(new GateMarginLoanRecordQueryRequest { Symbol = "SYMBOL", Currency = "CURRENCY", Type = GateMarginUniOrderType.Borrow });
var margin_14 = await api.IsolatedMargin.GetInterestHistoryAsync(new GateMarginInterestRecordQueryRequest { Symbol = "SYMBOL", Currency = "CURRENCY", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var margin_15 = await api.IsolatedMargin.GetMaximumBorrowableAsync(new GateMarginBorrowableRequest { Symbol = "SYMBOL", Currency = "CURRENCY" });
var margin_16 = await api.IsolatedMargin.GetUserLendingTiersAsync("SYMBOL");
var margin_17 = await api.IsolatedMargin.GetCurrentLendingTiersAsync("SYMBOL");
var margin_18 = await api.IsolatedMargin.SetLeverageAsync(new GateMarginLeverageSettingRequest { Symbol = "SYMBOL", Leverage = 10 });
var margin_19 = await api.IsolatedMargin.GetIsolatedBalancesAsync("SYMBOL");

// Flash-Swap Methods
var swap_02 = await api.FlashSwap.GetMarketsAsync(new GateSwapMarketQueryRequest { Currency = "SELL-CURRENCY", Limit = 1000 });
var swap_03 = await api.FlashSwap.PreviewOrderAsync("SELL-CURRENCY", "BUY-CURRENCY", sellAmount: 100.0m);
var swap_04 = await api.FlashSwap.PreviewOrderAsync(new GateSwapPreviewRequest
{
    SellCurrency = "SELL-CURRENCY",
    SellAmount = 100.0m,
    BuyCurrency = "BUY-CURRENCY",
});
var swap_05 = await api.FlashSwap.PlaceOrderAsync(swap_04.Data.PreviewId, "SELL-CURRENCY", 100.0m, "BUY-CURRENCY", 1000.0m);
var swap_06 = await api.FlashSwap.PlaceOrderAsync(new GateSwapOrderRequest
{
    BuyCurrency = "SELL-CURRENCY",
    BuyAmount = 1000.0m,
    SellCurrency = "BUY-CURRENCY",
    SellAmount = 100.0m,
    PreviewId = swap_04.Data.PreviewId
});
var swap_07 = await api.FlashSwap.GetOrdersAsync(new GateSwapOrderQueryRequest { Status = GateSwapOrderStatus.Success, SellCurrency = "SELL-CURRENCY", BuyCurrency = "BUY-CURRENCY", Limit = 100 });
var swap_08 = await api.FlashSwap.GetOrderAsync(1_000_000_000);

// Access for Futures (Perpetual & Delivery) Methods
var sample_01 = await api.Futures.BTC.GetContractsAsync();
var sample_03 = await api.Futures.USDT.GetContractsAsync();
var sample_04 = await api.Delivery.USDT.GetContractsAsync();
var sample_05 = await api.Futures.USD1.GetAdlRiskStatesAsync(); // Public, read-only market snapshot; no financial action is inferred.
if (sample_05.Success && sample_05.Data?.Settlement == "usd1"
    && sample_05.Data.States.TryGetValue("BTC_USD1", out var marketAdl) && marketAdl != null)
    Console.WriteLine($"Market ADL: {marketAdl.State}; calculated at (Unix ms): {marketAdl.CalculatedAtInMilliseconds}");

// Dictionary Access for Futures (Perpetual & Delivery) Methods
var sample_11 = await api.Futures[GateFuturesSettlement.BTC].GetContractsAsync();
var sample_13 = await api.Futures[GateFuturesSettlement.USDT].GetContractsAsync();
var sample_14 = await api.Delivery[GateDeliverySettlement.USDT].GetContractsAsync();

// Perpetual Futures Methods
// Catalog only, not a live workflow. Replace placeholders and deliberately select each financial action; never run the whole example with live credentials.
var settle = GateFuturesSettlement.USDT;
var perpetual_01 = await api.Futures[settle].GetContractsAsync();
var perpetual_01b = await api.Futures[settle].GetAllContractsAsync(); // Includes delisted contracts; presence does not imply tradability.
var perpetual_02 = await api.Futures[settle].GetContractAsync("CONTRACT");
var perpetual_03 = await api.Futures[settle].GetOrderBookAsync("CONTRACT");
var perpetual_04 = await api.Futures[settle].GetTradesAsync(new GateFuturesTradeQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Limit = 100 });
var perpetual_04b = await api.Futures[settle].GetCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "BTC_USDT", Interval = GateFuturesCandlestickInterval.NaturalWeek, Timezone = "utc0", Limit = 100 });
var perpetual_05 = await api.Futures[settle].GetMarkPriceCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "CONTRACT", Interval = GateFuturesCandlestickInterval.OneDay, Limit = 100 });
var perpetual_06 = await api.Futures[settle].GetIndexPriceCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "CONTRACT", Interval = GateFuturesCandlestickInterval.OneDay, Limit = 100 });
var perpetual_07 = await api.Futures[settle].GetPremiumIndexCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "CONTRACT", Interval = GateFuturesCandlestickInterval.OneDay, Limit = 100 });
var perpetual_08 = await api.Futures[settle].GetTickersAsync();
var perpetual_09 = await api.Futures[settle].GetFundingRateHistoryAsync(new GateFuturesFundingRateQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_09b = await api.Futures[settle].GetBatchFundingRateHistoryAsync(new GateFuturesBatchFundingRateRequest { Contracts = ["CONTRACT", "CONTRACT2"] });
var perpetual_10 = await api.Futures[settle].GetInsuranceHistoryAsync();
var perpetual_11 = await api.Futures[settle].GetStatsAsync(new GateFuturesStatsQueryRequest { Contract = "CONTRACT", Interval = GateFuturesStatsInterval.OneHour });
var perpetual_12 = await api.Futures[settle].GetIndexConstituentsAsync("INDEX");
var perpetual_13 = await api.Futures[settle].GetLiquidationsAsync(new GateFuturesLiquidationQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddHours(-1), To = DateTime.UtcNow });
var perpetual_14 = await api.Futures[settle].GetRiskLimitTiersAsync("CONTRACT");
var perpetual_15 = await api.Futures[settle].GetBalancesAsync();
var perpetual_16 = await api.Futures[settle].GetBalanceHistoryAsync(new GateFuturesBalanceHistoryQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_17 = await api.Futures[settle].GetPositionsAsync(new GateFuturesPositionQueryRequest { Holding = true }); // Omit Limit to return all current positions; explicit values must be 1-100.
var perpetual_17b = await api.Futures[settle].GetHistoricalPositionsAsync(new GateFuturesHistoricalPositionQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_18 = await api.Futures[settle].GetPositionAsync("CONTRACT");
var perpetual_19 = await api.Futures[settle].SetPositionMarginAsync("CONTRACT", 100.0M);
var perpetual_19b = await api.Futures[settle].GetLeverageAsync("CONTRACT", GateFuturesPositionMarginMode.Isolated, GateFuturesDualModeSide.DualLong);
var perpetual_20 = await api.Futures[settle].SetLeverageAsync("CONTRACT", 10);
var perpetual_20b = await api.Futures[settle].SetPositionLeverageAsync("BTC_USDT", 10, GateFuturesPositionMarginMode.Isolated); // Explicit instruction, not an inferred leverage/mode.
var perpetual_21 = await api.Futures[settle].SetMarginModeAsync("CONTRACT", GateFuturesMarginMode.Cross);
var perpetual_22 = await api.Futures[settle].SwithMarginModeUnderHedgeAsync("CONTRACT", GateFuturesMarginMode.Isolated);
var perpetual_23 = await api.Futures[settle].SetRiskLimitAsync("CONTRACT", 25);
var perpetual_24 = await api.Futures[settle].SetDualModeAsync(true);
var perpetual_24b = await api.Futures[settle].SetPositionModeAsync(GateFuturesAccountPositionMode.DualPlus); // Account-wide change; server requires no holdings or pending orders.
var perpetual_25 = await api.Futures[settle].GetDualModePositionsAsync("CONTRACT");
var perpetual_26 = await api.Futures[settle].SetDualModeMarginAsync("CONTRACT", GateFuturesDualModeSide.DualLong, 100);
var perpetual_27 = await api.Futures[settle].SetDualModeLeverageAsync("CONTRACT", 10);
var perpetual_28 = await api.Futures[settle].SetDualModeRiskLimitAsync("CONTRACT", 25);
// Illustrative only: use a real contract for the selected settlement and suitable account/market settings. Do not run these as a live batch.
var perpetual_29 = await api.Futures[settle].PlaceOrderAsync("BTC_USDT", 25.5m, price: 100.0m, timeInForce: GateFuturesTimeInForce.GoodTillCancelled);
var perpetual_30 = await api.Futures[settle].PlaceOrderAsync(new GateFuturesOrderRequest { Contract = "BTC_USDT", Size = 25.5m, Price = 100.0m, TimeInForce = GateFuturesTimeInForce.GoodTillCancelled, MarketOrderSlipRatio = 0.03m, PositionMarginMode = GateFuturesPositionMarginMode.Isolated, ActionMode = GateFuturesActionMode.Full, TakeProfitTriggerPrice = 110.0m, StopLossTriggerPrice = 90.0m });
var perpetual_30b = await api.Futures[settle].GetOrdersAsync(new GateFuturesOrderQueryRequest { Contract = "CONTRACT", Status = GateFuturesOrderStatus.Open, Limit = 100 });
var perpetual_30c = await api.Futures[settle].PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USDT", Size = 1, Direction = GateFuturesBboDirection.Buy, Level = 1 }); // Integer quantity; not a standard decimal-order alias.
// Replace the example ID with an actual existing order ID. Transport success/ACK is not proof of a fill or cancellation.
var perpetual_31 = await api.Futures[settle].GetOrderAsync(orderId: 1_000_000_001);
var perpetual_32 = await api.Futures[settle].CancelOrderAsync(orderId: 1_000_000_001, actionMode: GateFuturesActionMode.Result);
var perpetual_33 = await api.Futures[settle].AmendOrderAsync(orderId: 1_000_000_001, size: 20.5m, price: 101.0m, actionMode: GateFuturesActionMode.Full);
var perpetual_33b = await api.Futures[settle].CancelOrdersAsync(new GateFuturesOrderCancelAllRequest { Contract = "CONTRACT", ExcludeReduceOnly = true, ActionMode = GateFuturesActionMode.Acknowledge });
var perpetual_34 = await api.Futures[settle].GetUserTradesAsync("CONTRACT", orderId: 1_000_000_001);
var perpetual_35 = await api.Futures[settle].GetUserTradesAsync(new GateFuturesUserTradeQueryRequest { Contract = "CONTRACT", OrderId = 1_000_000_001, Limit = 100 });
var perpetual_36 = await api.Futures[settle].GetUserTradesAsync(new GateFuturesUserTradeTimeRangeQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Role = GateFuturesTradeRole.Maker });
var perpetual_37 = await api.Futures[settle].GetPositionClosesAsync();
var perpetual_38 = await api.Futures[settle].GetPositionClosesAsync(new GateFuturesPositionCloseQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_39 = await api.Futures[settle].GetUserLiquidationsAsync();
var perpetual_39b = await api.Futures[settle].GetUserLiquidationsAsync(new GateFuturesUserLiquidationQueryRequest { Contract = "BTC_USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Offset = 0, Limit = 100 });
var perpetual_40 = await api.Futures[settle].GetAdlHistoryAsync("CONTRACT");
var perpetual_41 = await api.Futures[settle].GetAdlHistoryAsync(new GateFuturesAdlHistoryQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_42 = await api.Futures[settle].CancelAllAsync(new GateFuturesCountdownCancelAllRequest { Timeout = 30, Contract = "CONTRACT" });
var perpetual_43 = await api.Futures[settle].GetTradingFeesAsync();
// Replace this ID with an actual order ID; inspect every result item, not just transport Success.
var perpetual_44 = await api.Futures[settle].CancelOrdersAsync(new[] { 1_000_000_001L }); // POST, 1-20 IDs.
var perpetual_45 = await api.Futures[settle].AmendOrdersAsync(new[] { new GateFuturesOrderAmendRequest { OrderId = 1_000_000_001L, Price = 101.0m } }); // 1-10 entries.
var perpetual_46 = await api.Futures[settle].GetRiskLimitTableAsync("TABLE-ID");
var perpetual_46b = await api.Futures[settle].PlaceTrailOrderAsync(new GateFuturesTrailOrderRequest { Contract = "CONTRACT", Amount = 10, ActivationPrice = 50000, IsGreaterThanOrEqual = true, PriceType = GateFuturesTrailPriceType.Latest, PriceOffset = "0.1%" });
var perpetual_46c = await api.Futures[settle].GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest { Contract = "CONTRACT", IsFinished = false });
var perpetual_46d = await api.Futures[settle].GetTrailOrderAsync(1_000_000_001);
var perpetual_46e = await api.Futures[settle].UpdateTrailOrderAsync(new GateFuturesTrailOrderUpdateRequest { OrderId = 1_000_000_001, Amount = 20, PriceOffset = "0.2%" });
var perpetual_46f = await api.Futures[settle].CancelTrailOrderAsync(1_000_000_001);
var perpetual_46g = await api.Futures[settle].CancelTrailOrdersAsync(new GateFuturesTrailOrdersCancelRequest { Contract = "CONTRACT" });
var perpetual_46h = await api.Futures[settle].GetTrailOrderChangeLogAsync(new GateFuturesTrailOrderChangeLogQueryRequest { OrderId = 1_000_000_001 });
var perpetual_46i = await api.Futures[settle].PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest { Contract = "CONTRACT", Amount = "10", PriceLimit = "0", OffsetLimit = "100", PriceType = GateFuturesChaseOrderPriceType.PriceGap, PriceGapType = GateFuturesChaseOrderPriceGapType.Absolute, PriceGapValue = "10" });
var perpetual_46j = await api.Futures[settle].CancelChaseOrderAsync("1000000001");
var perpetual_46k = await api.Futures[settle].CancelChaseOrdersAsync(new GateFuturesChaseOrdersCancelRequest { Contract = "CONTRACT", PositionMarginMode = GateFuturesPositionMarginMode.Isolated });
var perpetual_46l = await api.Futures[settle].GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest { Contract = "CONTRACT", IsFinished = false, SortBy = GateFuturesChaseOrderSort.CreatedAt, PageNumber = 1, PageSize = 100 });
var perpetual_46m = await api.Futures[settle].GetChaseOrderAsync("1000000001");
var perpetual_47 = await api.Futures[settle].PlacePriceTriggeredOrderAsync(
    GateFuturesTriggerType.PlanCloseShortPosition,
    GateFuturesTriggerPrice.MarkPrice,
    GateFuturesTriggerStrategy.ByPrice,
    GateSpotTriggerCondition.GreaterThanOrEqualTo,
    100.01m, TimeSpan.FromMinutes(15), "CONTRACT", 100.00m, 25, false,
    GateFuturesTimeInForce.GoodTillCancelled,
    "CLIENT-ORDER-ID", true, GateFuturesOrderAutoSize.None
);
var perpetual_48 = await api.Futures[settle].PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest
{
    PositionMarginMode = GateFuturesPositionMarginMode.Cross,
    Order = new GateFuturesInitial { Contract = "CONTRACT", Amount = "0.5", Price = "0", TimeInForce = GateFuturesTimeInForce.ImmediateOrCancel },
    Trigger = new GateFuturesTrigger { PriceType = GateFuturesTriggerPrice.MarkPrice, Price = "100.01", Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo }
});
// Use an existing order ID from this same settlement. Omit Settlement if the route is sufficient.
var perpetual_48b = await api.Futures.USD1.AmendPriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderUpdateRequest { Settlement = "usd1", OrderId = 1_000_000_001, Amount = "0.25", TriggerPrice = "101.00", PriceType = GateFuturesTriggerPrice.MarkPrice });
var perpetual_49 = await api.Futures[settle].GetPriceTriggeredOrdersAsync(new GateFuturesPriceTriggeredOrderQueryRequest { Status = GateSpotTriggerFilter.Open, Contract = "CONTRACT", Limit = 100 });
var perpetual_50 = await api.Futures[settle].CancelPriceTriggeredOrdersAsync("CONTRACT"); // Null intentionally broadens scope to all eligible orders; inspect every returned status.
var perpetual_51 = await api.Futures[settle].GetPriceTriggeredOrderAsync(1_000_000_001);
var perpetual_52 = await api.Futures[settle].CancelPriceTriggeredOrderAsync(1_000_000_001);

// TradFi Methods
var tradfi_01 = await api.TradFi.GetMt5AccountAsync();
var tradfi_02 = await api.TradFi.GetSymbolCategoriesAsync();
var tradfi_03 = await api.TradFi.GetSymbolCommissionsAsync(new GateTradFiSymbolCommissionQueryRequest { Symbols = ["XAUUSD"], CategoryCodes = ["metal"] });
var tradfi_04 = await api.TradFi.GetSymbolsAsync();
var tradfi_05 = await api.TradFi.GetSymbolDetailsAsync(new GateTradFiSymbolDetailsRequest { Symbols = ["XAUUSD"] });
var tradfi_06 = await api.TradFi.GetCandlesticksAsync(new GateTradFiCandlestickQueryRequest { Symbol = "XAUUSD", Interval = GateTradFiKlineInterval.OneHour, Limit = 100 });
var tradfi_07 = await api.TradFi.GetTickerAsync("XAUUSD");
var tradfi_08 = await api.TradFi.CreateUserAsync();
var tradfi_09 = await api.TradFi.GetAccountAssetsAsync();
var tradfi_10 = await api.TradFi.GetTransactionsAsync(new GateTradFiTransactionQueryRequest { BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, Page = 1, PageSize = 50 });
var tradfi_11 = await api.TradFi.CreateTransactionAsync(new GateTradFiTransactionRequest { Asset = "USDT", Change = 100.0m, Type = GateTradFiTransactionType.Deposit });
var tradfi_12 = await api.TradFi.GetOrdersAsync();
var tradfi_13 = await api.TradFi.PlaceOrderAsync(new GateTradFiOrderRequest { Symbol = "XAUUSD", Side = GateTradFiOrderSide.Buy, PriceType = GateTradFiOrderPriceType.Market, Price = 0m, Volume = 0.01m });
// Leverage is intentionally omitted; choose an allowed multiplier explicitly via the DTO when needed.
// tradfi_13.Data.Id is a queue task ID, not the actual order ID required by the next calls.
var tradfi_14 = await api.TradFi.UpdateOrderAsync(1_000_000_001, new GateTradFiOrderUpdateRequest { Price = 100.0m, TakeProfitPrice = 110.0m, StopLossPrice = 90.0m });
var tradfi_15 = await api.TradFi.CancelOrderAsync(1_000_000_001);
var tradfi_16 = await api.TradFi.GetOrderHistoryAsync(new GateTradFiOrderHistoryQueryRequest { Symbol = "XAUUSD", BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow });
var tradfi_17 = await api.TradFi.GetPositionsAsync();
var tradfi_18 = await api.TradFi.UpdatePositionAsync(1_000_000_001, new GateTradFiPositionUpdateRequest { TakeProfitPrice = 110.0m, StopLossPrice = 90.0m });
var tradfi_19 = await api.TradFi.ClosePositionAsync(1_000_000_001, new GateTradFiClosePositionRequest { CloseType = 1, CloseVolume = 0.01m });
var tradfi_20 = await api.TradFi.GetPositionHistoryAsync(new GateTradFiPositionHistoryQueryRequest { Page = 1, PageSize = 50, Symbol = "XAUUSD", BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow });

// Stock Methods
var stock_01 = await api.Stock.GetAssetsAsync(GateStockPnlCalculationType.AverageCost, GateStockPnlPriceType.Intraday);
var stock_02 = await api.Stock.GetSymbolsAsync(new GateStockSymbolQueryRequest { Exchange = GateStockExchange.UnitedStates, IncludeLocalizedDescriptions = true, Page = 1, PageSize = 50 });
var stock_03 = await api.Stock.GetSymbolDetailsAsync(new GateStockSymbolDetailsQueryRequest { Symbols = ["AAPL"], Page = 1, PageSize = 50 });
var stock_04 = await api.Stock.GetOrderBookAsync("AAPL");
var stock_05 = await api.Stock.GetOrdersAsync("AAPL");
var stock_06 = await api.Stock.PlaceOrderAsync(new GateStockOrderRequest { Symbol = "AAPL", Side = GateStockOrderSide.Buy, Volume = 1m, PriceType = GateStockOrderPriceType.Limit, TradingSession = GateStockTradingSession.All, TimeInForce = GateStockTimeInForce.Day, Price = 200m, ClientOrderId = "CLIENT-STOCK-ORDER-ID" });
var stock_07 = await api.Stock.CancelAllOrdersAsync();
var stock_08 = await api.Stock.GetOrderHistoryAsync(new GateStockOrderHistoryQueryRequest { Symbol = "AAPL", BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, Page = 1, PageSize = 50 });
var stock_09 = await api.Stock.UpdateOrderAsync(1_000_000_001, new GateStockOrderUpdateRequest { Volume = 1m, Price = 201m });
var stock_10 = await api.Stock.CancelOrderAsync(1_000_000_001);
var stock_11 = await api.Stock.GetPositionsAsync(new GateStockPositionQueryRequest { Symbol = "AAPL", Exchange = GateStockExchange.UnitedStates });
var stock_12 = await api.Stock.ClosePositionAsync(new GateStockClosePositionRequest { Symbol = "AAPL", CloseType = GateStockPositionCloseType.Partial, CloseVolume = 1m });
var stock_13 = await api.Stock.GetTransactionsAsync(new GateStockTransactionQueryRequest { BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, Page = 1, PageSize = 50 });
var stock_14 = await api.Stock.CreateTransactionAsync(new GateStockTransferRequest { Asset = "USDT", Change = 100m, Type = GateStockTransferType.Deposit, ReferenceId = "CLIENT-STOCK-TRANSFER-ID" });
var stock_15 = await api.Stock.GetExchangesAsync();
var stock_16 = await api.Stock.GetFeeRatesAsync();

// Delivery Futures Methods
var delivery_01 = await api.Delivery.USDT.GetContractsAsync();
var delivery_02 = await api.Delivery.USDT.GetContractAsync("CONTRACT");
var delivery_03 = await api.Delivery.USDT.GetOrderBookAsync("CONTRACT");
var delivery_04 = await api.Delivery.USDT.GetTradesAsync(new GateDeliveryTradeQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Limit = 100 });
var delivery_05 = await api.Delivery.USDT.GetMarkPriceCandlesticksAsync(new GateDeliveryCandlestickQueryRequest { Contract = "CONTRACT", Interval = GateFuturesCandlestickInterval.OneDay, Limit = 100 });
var delivery_06 = await api.Delivery.USDT.GetIndexPriceCandlesticksAsync(new GateDeliveryCandlestickQueryRequest { Contract = "CONTRACT", Interval = GateFuturesCandlestickInterval.OneDay, Limit = 100 });
var delivery_07 = await api.Delivery.USDT.GetTickersAsync();
var delivery_08 = await api.Delivery.USDT.GetInsuranceHistoryAsync();
var delivery_09 = await api.Delivery.USDT.GetBalancesAsync();
var delivery_10 = await api.Delivery.USDT.GetBalanceHistoryAsync(new GateDeliveryBalanceHistoryQueryRequest { Type = GateFuturesBalanceChangeType.Funding, From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var delivery_11 = await api.Delivery.USDT.GetPositionsAsync();
var delivery_12 = await api.Delivery.USDT.GetPositionAsync("CONTRACT");
var delivery_13 = await api.Delivery.USDT.SetPositionMarginAsync("CONTRACT", 100.0m);
var delivery_14 = await api.Delivery.USDT.SetLeverageAsync("CONTRACT", 10);
var delivery_15 = await api.Delivery.USDT.SetRiskLimitAsync("CONTRACT", 25);
var delivery_16 = await api.Delivery.USDT.PlaceOrderAsync("CONTRACT", 25, price: 100.0m, timeInForce: GateFuturesTimeInForce.GoodTillCancelled);
var delivery_17 = await api.Delivery.USDT.PlaceOrderAsync(new GateDeliveryOrderRequest { Contract = "CONTRACT", Size = 25, Price = 100.0m });
var delivery_18 = await api.Delivery.USDT.GetOrdersAsync(new GateDeliveryOrderQueryRequest { Contract = "CONTRACT", Status = GateFuturesOrderStatus.Open, Limit = 100 });
var delivery_19 = await api.Delivery.USDT.CancelOrdersAsync(new GateDeliveryCancelOrdersRequest { Contract = "CONTRACT", Side = GateFuturesOrderSide.Bid });
var delivery_20 = await api.Delivery.USDT.GetOrderAsync();
var delivery_21 = await api.Delivery.USDT.CancelOrderAsync();
var delivery_22 = await api.Delivery.USDT.GetUserTradesAsync(new GateDeliveryUserTradeQueryRequest { Contract = "CONTRACT", Limit = 100 });
var delivery_23 = await api.Delivery.USDT.GetPositionClosesAsync(new GateDeliveryPositionCloseQueryRequest { Contract = "CONTRACT", Limit = 100 });
var delivery_24 = await api.Delivery.USDT.GetUserLiquidationsAsync(new GateDeliveryLiquidationQueryRequest { Contract = "CONTRACT", At = DateTime.UtcNow });
var delivery_25 = await api.Delivery.USDT.GetUserSettlementsAsync(new GateDeliverySettlementQueryRequest { Contract = "CONTRACT", At = DateTime.UtcNow });
var delivery_26 = await api.Delivery.USDT.GetRiskLimitTiersAsync(new GateDeliveryRiskLimitTierQueryRequest { Contract = "CONTRACT", Limit = 100 });
var delivery_27 = await api.Delivery.USDT.PlacePriceTriggeredOrderAsync(
    GateFuturesTriggerType.CloseShortPosition,
    GateFuturesTriggerPrice.MarkPrice,
    GateFuturesTriggerStrategy.ByPrice,
    GateSpotTriggerCondition.GreaterThanOrEqualTo,
    100.01m, TimeSpan.FromMinutes(15), "CONTRACT", 100.00m, 25, true,
    GateFuturesTimeInForce.GoodTillCancelled,
    "CLIENT-ORDER-ID", false, GateFuturesOrderAutoSize.CloseLong
);
var delivery_28 = await api.Delivery.USDT.PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest { });
var delivery_29 = await api.Delivery.USDT.GetPriceTriggeredOrdersAsync(new GateDeliveryPriceTriggeredOrderQueryRequest { Status = GateSpotTriggerFilter.Open, Contract = "CONTRACT", Limit = 100 });
var delivery_30 = await api.Delivery.USDT.CancelPriceTriggeredOrdersAsync(new GateDeliveryPriceTriggeredOrderCancelRequest { Contract = "CONTRACT" });
var delivery_31 = await api.Delivery.USDT.GetPriceTriggeredOrderAsync(1_000_000_001);
var delivery_32 = await api.Delivery.USDT.CancelPriceTriggeredOrderAsync(1_000_000_001);

// Options Methods
var options_01 = await api.Options.GetUnderlyingsAsync();
var options_02 = await api.Options.GetExpirationsAsync("UNDERLYING");
var options_03 = await api.Options.GetContractsAsync(new GateOptionsContractQueryRequest { Underlying = "UNDERLYING", Expiration = 1_724_976_000 });
var options_04 = await api.Options.GetContractAsync("CONTRACT");
var options_05 = await api.Options.GetSettlementsAsync(new GateOptionsSettlementQueryRequest { Underlying = "UNDERLYING", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Limit = 100 });
var options_06 = await api.Options.GetSettlementAsync("UNDERLYING", "CONTRACT", 1728321316);
var options_07 = await api.Options.GetUserSettlementsAsync(new GateOptionsUserSettlementQueryRequest { Underlying = "UNDERLYING", Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var options_08 = await api.Options.GetOrderBookAsync(new GateOptionsOrderBookRequest { Contract = "CONTRACT", Interval = 0.1m, Limit = 10, WithId = true });
var options_09 = await api.Options.GetContractTickersAsync("UNDERLYING");
var options_10 = await api.Options.GetUnderlyingTickersAsync("UNDERLYING");
var options_11 = await api.Options.GetCandlesticksAsync(new GateOptionsCandlestickQueryRequest { Contract = "CONTRACT", Interval = GateOptionsCandlestickInterval.OneHour, Limit = 100 });
var options_12 = await api.Options.GetUnderlyingCandlesticksAsync(new GateOptionsUnderlyingCandlestickQueryRequest { Underlying = "UNDERLYING", Interval = GateOptionsCandlestickInterval.OneMinute, Limit = 100 });
var options_13 = await api.Options.GetTradesAsync(new GateOptionsTradeQueryRequest { Contract = "CONTRACT", Type = GateOptionsType.Put, Limit = 100 });
var options_14 = await api.Options.GetBalanceAsync();
var options_15 = await api.Options.GetAccountAsync();
var options_16 = await api.Options.GetBalanceHistoryAsync(new GateOptionsBalanceHistoryQueryRequest { Type = GateOptionsBalanceChangeType.Rebate, From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var options_17 = await api.Options.GetUnderlyingPositionsAsync(new GateOptionsPositionQueryRequest { Underlying = "UNDERLYING" });
var options_18 = await api.Options.GetContractPositionAsync("CONTRACT");
var options_19 = await api.Options.GetUserLiquidationsAsync(new GateOptionsUserLiquidationQueryRequest { Underlying = "UNDERLYING", Contract = "CONTRACT" });
var options_20 = await api.Options.PlaceOrderAsync(new GateOptionsOrderRequest { Contract = "CONTRACT", Size = 25, Price = 100.0m, TimeInForce = GateOptionsTimeInForce.GoodTillCancelled, ClientOrderId = "CLIENT-ORDER-ID" });
var options_21 = await api.Options.GetOrdersAsync(new GateOptionsOrderQueryRequest { Status = GateOptionsOrderStatus.Open, Underlying = "UNDERLYING", Contract = "CONTRACT", Limit = 100 });
var options_22 = await api.Options.CancelOrdersAsync(new GateOptionsCancelOrdersRequest { Underlying = "UNDERLYING", Contract = "CONTRACT", Side = GateOptionsOrderSide.Bid });
var options_23 = await api.Options.GetOrderAsync(1_000_000_001);
var options_24 = await api.Options.AmendOrderAsync(1_000_000_001, new GateOptionsOrderUpdateRequest { Contract = "CONTRACT", Price = 101.0m, Size = 25 });
var options_25 = await api.Options.CancelOrderAsync(1_000_000_001);
var options_26 = await api.Options.CancelAllAsync(new GateOptionsCountdownCancelAllRequest { Timeout = 30, Underlying = "UNDERLYING", Contract = "CONTRACT" });
var options_27 = await api.Options.GetUserTradesAsync(new GateOptionsUserTradeQueryRequest { Underlying = "UNDERLYING", Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var options_28 = await api.Options.GetMMPAsync("UNDERLYING");
var options_29 = await api.Options.SetMMPAsync(new GateOptionsMMPRequest { Underlying = "UNDERLYING", Window = 5000, FrozenPeriod = 200, QuantityLimit = 10.0m, DeltaLimit = 10.0m });
var options_30 = await api.Options.ResetMMPAsync("UNDERLYING");

// EarnUni Methods
var earnuni_01 = await api.EarnUni.GetCurrenciesAsync();
var earnuni_02 = await api.EarnUni.GetCurrencyAsync("USDT");
var earnuni_03 = await api.EarnUni.GetLendsAsync(new GateEarnUniLendQueryRequest { Currency = "USDT", Limit = 100 });
var earnuni_04 = await api.EarnUni.CreateLendAsync(new GateEarnUniLendRequest { Currency = "USDT", Amount = 100.0m, Type = GateEarnUniLendOperationType.Lend, MinimumRate = 0.0001m });
var earnuni_05 = await api.EarnUni.UpdateLendAsync(new GateEarnUniLendUpdateRequest { Currency = "USDT", MinimumRate = 0.0001m });
var earnuni_06 = await api.EarnUni.GetLendRecordsAsync(new GateEarnUniLendRecordQueryRequest { Currency = "USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Type = GateEarnUniLendOperationType.Lend });
var earnuni_07 = await api.EarnUni.GetInterestAsync("USDT");
var earnuni_08 = await api.EarnUni.GetInterestRecordsAsync(new GateEarnUniInterestRecordQueryRequest { Currency = "USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var earnuni_09 = await api.EarnUni.GetInterestStatusAsync("USDT");
var earnuni_10 = await api.EarnUni.GetChartAsync(new GateEarnUniChartQueryRequest { Asset = "USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var earnuni_11 = await api.EarnUni.GetEstimatedRatesAsync();

// Multi-Collateral Loan Methods
var multiCollateralLoan_01 = await api.MultiCollateralLoan.GetOrdersAsync(new GateMultiCollateralLoanOrderQueryRequest { OrderType = GateMultiCollateralLoanOrderType.Current, Sort = GateMultiCollateralLoanOrderSort.TimeDescending, Limit = 100 });
var multiCollateralLoan_02 = await api.MultiCollateralLoan.PlaceOrderAsync(new GateMultiCollateralLoanOrderRequest { BorrowCurrency = "BTC", BorrowAmount = 1.0m, OrderType = GateMultiCollateralLoanOrderType.Fixed, FixedType = GateMultiCollateralLoanFixedType.SevenDays, FixedRate = 0.00001m, AutoRenew = true, AutoRepay = true, CollateralCurrencies = new[] { new GateMultiCollateralLoanCurrencyAmount { Currency = "USDT", Amount = 1000.0m } } });
var multiCollateralLoan_03 = await api.MultiCollateralLoan.GetOrderAsync(1_000_000_001);
var multiCollateralLoan_04 = await api.MultiCollateralLoan.GetRepaymentRecordsAsync(new GateMultiCollateralLoanRepaymentRecordQueryRequest { Type = GateMultiCollateralLoanRepaymentType.Repay, BorrowCurrency = "BTC", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var multiCollateralLoan_05 = await api.MultiCollateralLoan.RepayAsync(new GateMultiCollateralLoanRepayRequest { OrderId = 1_000_000_001, RepayItems = new[] { new GateMultiCollateralLoanRepayItem { Currency = "BTC", Amount = 1.0m, RepaidAll = false } } });
var multiCollateralLoan_06 = await api.MultiCollateralLoan.GetCollateralRecordsAsync(new GateMultiCollateralLoanCollateralRecordQueryRequest { CollateralCurrency = "USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var multiCollateralLoan_07 = await api.MultiCollateralLoan.AdjustCollateralAsync(new GateMultiCollateralLoanCollateralAdjustRequest { OrderId = 1_000_000_001, Type = GateMultiCollateralLoanCollateralOperationType.Append, Collaterals = new[] { new GateMultiCollateralLoanCurrencyAmount { Currency = "USDT", Amount = 1000.0m } } });
var multiCollateralLoan_08 = await api.MultiCollateralLoan.GetCurrencyQuotasAsync(new GateMultiCollateralLoanCurrencyQuotaRequest { Type = GateMultiCollateralLoanCurrencyQuotaType.Collateral, Currencies = new[] { "BTC", "USDT" } });
var multiCollateralLoan_09 = await api.MultiCollateralLoan.GetCurrenciesAsync();
var multiCollateralLoan_10 = await api.MultiCollateralLoan.GetLtvAsync();
var multiCollateralLoan_11 = await api.MultiCollateralLoan.GetFixedRatesAsync();
var multiCollateralLoan_12 = await api.MultiCollateralLoan.GetCurrentRatesAsync(new GateMultiCollateralLoanCurrentRateRequest { Currencies = new[] { "BTC", "GT" }, VipLevel = "0" });

// Earn Methods
var earn_01 = await api.Earn.GetDualInvestmentPlansAsync(new GateEarnDualPlanQueryRequest { Coin = "BTC", Type = GateEarnDualOptionType.Put, QuoteCurrency = GateEarnDualQuoteCurrency.USDT, Sort = GateEarnDualPlanSort.Apy });
var earn_02 = await api.Earn.GetDualInvestmentOrdersAsync(new GateEarnDualOrderQueryRequest { Coin = "BTC", Status = GateEarnDualOrderQueryStatus.All, From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var earn_03 = await api.Earn.PlaceDualInvestmentOrderAsync(new GateEarnDualOrderRequest { PlanId = 1_000_000_001, Amount = 1.0m, Text = "t-client-text" });
var earn_04 = await api.Earn.GetDualInvestmentBalanceAsync();
var earn_05 = await api.Earn.GetDualInvestmentRefundPreviewAsync(1_000_000_001);
var earn_06 = await api.Earn.RefundDualInvestmentOrderAsync(new GateEarnDualRefundRequest { OrderId = 1_000_000_001, RequestId = "REQUEST-ID" });
var earn_07 = await api.Earn.UpdateDualInvestmentReinvestAsync(new GateEarnDualReinvestUpdateRequest { OrderId = 1_000_000_001, Status = 1, EffectiveTimeDuration = 86_400 });
var earn_08 = await api.Earn.GetDualInvestmentRecommendationsAsync(new GateEarnDualRecommendationRequest { Coin = "BTC", Type = GateEarnDualOptionType.Put, Mode = GateEarnDualRecommendationMode.Normal });
var earn_09 = await api.Earn.GetStakingCoinsAsync(new GateEarnStakingCoinQueryRequest { CoinType = GateEarnStakingCoinType.Lock });
var earn_10 = await api.Earn.SwapStakingCoinAsync(new GateEarnStakingSwapRequest { Coin = "GT", Side = GateEarnStakingOperationType.Stake, Amount = 1.0m, ProductId = 1_000_000_001 });
var earn_11 = await api.Earn.GetStakingOrdersAsync(new GateEarnStakingOrderQueryRequest { Coin = "GT", Type = GateEarnStakingOperationType.Stake, Page = 1 });
var earn_12 = await api.Earn.GetStakingAwardsAsync(new GateEarnStakingAwardQueryRequest { Coin = "GT", Page = 1 });
var earn_13 = await api.Earn.GetStakingAssetsAsync(new GateEarnStakingAssetQueryRequest { Coin = "GT" });
var earn_14 = await api.Earn.CreateAutoInvestPlanAsync(new GateEarnAutoInvestPlanCreateRequest { PlanMoney = "USDT", PlanAmount = 100.0m, PeriodType = GateEarnAutoInvestPeriodType.Weekly, PeriodDay = 1, PeriodHour = 12, Items = new[] { new GateEarnAutoInvestPortfolioItem { Asset = "BTC", Ratio = 100.0m } }, FundSource = GateEarnAutoInvestFundSource.Spot, FundFlow = GateEarnAutoInvestFundFlow.AutoInvest });
var earn_15 = await api.Earn.UpdateAutoInvestPlanAsync(new GateEarnAutoInvestPlanUpdateRequest { PlanId = 1_000_000_001, FundSource = GateEarnAutoInvestFundSource.Spot });
var earn_16 = await api.Earn.StopAutoInvestPlanAsync(new GateEarnAutoInvestPlanStopRequest { PlanId = 1_000_000_001 });
var earn_17 = await api.Earn.AddAutoInvestPositionAsync(new GateEarnAutoInvestAddPositionRequest { PlanId = 1_000_000_001, Amount = 100.0m });
var earn_18 = await api.Earn.GetAutoInvestCoinsAsync("USDT");
var earn_19 = await api.Earn.GetAutoInvestMinimumAmountAsync(new GateEarnAutoInvestMinInvestAmountRequest { Money = "USDT", Items = new[] { new GateEarnAutoInvestPortfolioItem { Asset = "BTC", Ratio = 100.0m } } });
var earn_20 = await api.Earn.GetAutoInvestExecutionRecordsAsync(new GateEarnAutoInvestExecutionRecordsRequest { PlanId = 1_000_000_001, Page = 1, PageSize = 20 });
var earn_21 = await api.Earn.GetAutoInvestOrderDetailsAsync(new GateEarnAutoInvestOrderDetailsRequest { PlanId = 1_000_000_001, RecordId = 1_000_000_002 });
var earn_22 = await api.Earn.GetAutoInvestConfigAsync();
var earn_23 = await api.Earn.GetAutoInvestPlanAsync(1_000_000_001);
var earn_24 = await api.Earn.GetAutoInvestPlansAsync(new GateEarnAutoInvestPlanListRequest { Status = GateEarnAutoInvestPlanStatus.Active, Page = 1, PageSize = 20 });
var earn_25 = await api.Earn.GetFixedTermProductsAsync(new GateEarnFixedTermProductQueryRequest { Asset = "USDT", Type = GateEarnFixedTermProductType.All, Page = 1, Limit = 100 });
var earn_26 = await api.Earn.GetFixedTermProductsByAssetAsync(new GateEarnFixedTermProductByAssetRequest { Asset = "USDT", Type = GateEarnFixedTermProductType.All });
var earn_27 = await api.Earn.GetFixedTermLendsAsync(new GateEarnFixedTermLendQueryRequest { OrderType = GateEarnFixedTermOrderType.Current, Asset = "USDT", Page = 1, Limit = 100 });
var earn_28 = await api.Earn.CreateFixedTermLendAsync(new GateEarnFixedTermLendRequest { ProductId = 1_000_000_001, Amount = 100.0m, ReinvestStatus = 1 });
var earn_29 = await api.Earn.RedeemFixedTermOrderAsync(new GateEarnFixedTermPreRedeemRequest { OrderId = 1_000_000_001 });
var earn_30 = await api.Earn.GetFixedTermHistoryAsync(new GateEarnFixedTermHistoryRequest { Type = GateEarnFixedTermHistoryType.Subscription, Asset = "USDT", Page = 1, Limit = 100, StartAt = DateTime.UtcNow.AddDays(-7), EndAt = DateTime.UtcNow });

// Account Methods
var account_01 = await api.Account.GetAccountAsync();
var account_02 = await api.Account.GetMainKeysAsync();
var account_03 = await api.Account.GetRateLimitsAsync();
var account_04 = await api.Account.CreateStpGroupAsync(new GateAccountStpGroupRequest { Name = "STP-NAME" });
var account_05 = await api.Account.GetStpGroupsAsync(new GateAccountStpGroupQueryRequest { Name = "STP-NAME" });
var account_06 = await api.Account.GetStpGroupUsersAsync(1_000_000_001);
var account_07 = await api.Account.AddUsersToStpGroupAsync(1_000_000_001, new GateAccountStpGroupUsersRequest { UserIds = new[] { 2_000_000_001L } });
var account_08 = await api.Account.RemoveUsersFromStpGroupAsync(1_000_000_001, new GateAccountStpGroupUsersRequest { UserIds = new[] { 2_000_000_001L } });
var account_09 = await api.Account.SetDebitFeeAsync(new GateAccountDebitFeeRequest { Enabled = true });
var account_10 = await api.Account.GetDebitFeeAsync();

// Rebate Methods
var rebate_01 = await api.Rebate.GetTransactionHistoryAsync(new GateRebateTransactionHistoryRequest { Symbol = "GT_USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var rebate_02 = await api.Rebate.GetCommissionHistoryAsync(new GateRebateCommissionHistoryRequest { Currency = "GT", CommissionType = GateRebateCommissionType.Direct, From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var rebate_03 = await api.Rebate.GetPartnerTransactionHistoryAsync(new GateRebateTransactionHistoryRequest { Symbol = "GT_USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var rebate_04 = await api.Rebate.GetPartnerCommissionHistoryAsync(new GateRebateCommissionHistoryRequest { Currency = "GT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var rebate_05 = await api.Rebate.GetPartnerSubListAsync(new GateRebatePartnerSubListRequest { UserId = 1_000_000_001, Limit = 100 });
var rebate_06 = await api.Rebate.GetBrokerCommissionHistoryAsync(new GateRebateBrokerHistoryRequest { UserId = 1_000_000_001, From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var rebate_07 = await api.Rebate.GetBrokerTransactionHistoryAsync(new GateRebateBrokerHistoryRequest { UserId = 1_000_000_001, From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var rebate_08 = await api.Rebate.GetUserInfoAsync();
var rebate_09 = await api.Rebate.GetUserSubRelationAsync(new GateRebateUserSubRelationRequest { UserIds = new[] { 1_000_000_001L, 1_000_000_002L } });
var rebate_10 = await api.Rebate.GetRecentPartnerApplicationAsync();
var rebate_11 = await api.Rebate.CheckPartnerEligibilityAsync();
var rebate_12 = await api.Rebate.GetPartnerAggregatedDataAsync(new GateRebatePartnerAggregatedDataRequest { StartDate = "2024-01-01 00:00:00", EndDate = "2024-01-07 23:59:59", BusinessType = GateRebateBusinessType.All });

// OTC Methods
var otc_01 = await api.Otc.GetQuoteAsync(new GateOtcQuoteRequest { Side = GateOtcQuoteSide.Pay, PayCoin = "USDT", GetCoin = "USD", PayAmount = 30000.0m, CreateQuoteToken = true });
// Replace placeholders with the actual quote side/token/amounts and selected bank ID. Null ReceiveType selects no local remittance name.
var otc_02 = await api.Otc.CreateFiatOrderAsync(new GateOtcFiatOrderRequest { Type = GateOtcOrderType.Buy, Side = GateOtcOrderKind.Pay, CryptoCurrency = "USDT", FiatCurrency = "USD", CryptoAmount = 30000.0m, FiatAmount = 30000.0m, QuoteToken = "QUOTE-TOKEN", BankId = 1_000_000_001, ReceiveType = null });
var otc_03 = await api.Otc.CreateStableCoinOrderAsync("USDC", "USDT", 30000.0m, 20000.0m, GateOtcQuoteSide.Pay, "QUOTE-TOKEN");
var otc_04 = await api.Otc.GetBankAccountsAsync();
// Supply the actual key after a separate successful S3 upload; this call submits bank materials, not review approval.
var otc_05 = await api.Otc.CreateBankCardAsync(new GateOtcBankCreateRequest { BankAccountName = "ACCOUNT-NAME", BankName = "BANK-NAME", BankCountry = "GB", BankAddress = "BANK-ADDRESS", Iban = "IBAN", Swift = "SWIFT", DocumentationFileKey = "ACTUAL-PRE-UPLOADED-KEY", FileType = "aW1hZ2UvcG5n" });
var otc_06 = await api.Otc.DeleteBankCardAsync("BANK-CARD-ID");
var otc_07 = await api.Otc.SetDefaultBankCardAsync("BANK-CARD-ID");
var otc_08 = await api.Otc.GetBankSupplementChecklistAsync("BANK-CARD-ID");
var otc_09 = await api.Otc.SubmitPersonalBankSupplementAsync(new GateOtcBankPersonalSupplementRequest { BankId = "BANK-CARD-ID", IdDocumentFront = "BASE64-ID-FRONT", IdDocumentBack = "BASE64-ID-BACK", AddressProof = "BASE64-ADDRESS-PROOF" });
var otc_10 = await api.Otc.SubmitEnterpriseBankSupplementAsync(new GateOtcBankEnterpriseSupplementRequest { BankId = "BANK-CARD-ID", Certificate = "BASE64-CERTIFICATE", ShareHolders = "BASE64-SHAREHOLDERS", Passport = "BASE64-PASSPORT", ShareHoldingStructure = "BASE64-STRUCTURE" });
var otc_11 = await api.Otc.MarkFiatOrderAsPaidAsync(new GateOtcMarkOrderPaidRequest { OrderId = "1000000001", PaymentReceiptFileKey = "PAYMENT-RECEIPT-FILE-KEY" });
var otc_12 = await api.Otc.CancelFiatOrderAsync(new GateOtcOrderIdRequest { OrderId = "1000000001" });
var otc_13 = await api.Otc.GetFiatOrdersAsync(new GateOtcFiatOrderListRequest { Type = GateOtcOrderType.Buy, FiatCurrency = "USD", CryptoCurrency = "USDT", StartTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, PageNumber = 1, PageSize = 10 });
var otc_14 = await api.Otc.GetStableCoinOrdersAsync(new GateOtcStableCoinOrderListRequest { CoinName = "USDT", Status = "PROCESSING", StartTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, PageNumber = 1, PageSize = 10 });
var otc_15 = await api.Otc.GetFiatOrderAsync(new GateOtcOrderIdRequest { OrderId = "1000000001" });
// Credentials only: no direct S3 upload or subsequent business submission is performed here. Do not log Policy values.
var otc_16 = await api.Otc.CreatePreUploadAsync(new GateOtcUploadPreUploadRequest { ContentType = GateOtcUploadContentType.Png, Scene = GateOtcUploadScene.Bank });

// P2P Methods
// Gate's P2P "Query spot balance" guide reuses GET /spot/accounts; it does not define a separate P2P endpoint.
var p2p_spot_balance = await api.Spot.GetBalancesAsync();
var p2p_01 = await api.P2p.GetUserInfoAsync();
var p2p_02 = await api.P2p.GetCounterpartyUserInfoAsync(new GateP2pCounterpartyUserInfoRequest { BusinessUserId = "BIZ-UID" });
var p2p_03 = await api.P2p.GetPaymentMethodsAsync(new GateP2pPaymentMethodsRequest { Fiat = "USD" });
var p2p_04 = await api.P2p.GetPendingTransactionsAsync(new GateP2pPendingTransactionsRequest { CryptoCurrency = "USDT", FiatCurrency = "USD", OrderTab = GateP2pOrderTab.Pending, SelectType = GateP2pOrderSide.Sell, StartTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow });
var p2p_05 = await api.P2p.GetCompletedTransactionsAsync(new GateP2pCompletedTransactionsRequest { CryptoCurrency = "USDT", FiatCurrency = "USD", SelectType = GateP2pOrderSide.Sell, QueryDispute = true, Page = 1, PerPage = 10 });
var p2p_06 = await api.P2p.GetTransactionDetailsAsync(new GateP2pTransactionDetailsRequest { TransactionId = 40_000_001, Channel = "" });
var p2p_07 = await api.P2p.ConfirmPaymentAsync(new GateP2pConfirmPaymentRequest { TransactionId = 40_000_001, PaymentMethod = "bank" });
var p2p_08 = await api.P2p.ConfirmReceiptAsync(new GateP2pTransactionIdRequest { TransactionId = 40_000_001 });
var p2p_09 = await api.P2p.CancelOrderAsync(new GateP2pCancelOrderRequest { TransactionId = 40_000_001, ReasonId = "1", ReasonMemo = "Canceled after agreement with the counterparty" });
var p2p_10 = await api.P2p.SubmitAdvertisementAsync(new GateP2pAdRequest { CurrencyType = "USDT", ExchangeType = "USD", Type = GateP2pAdOperationType.PublishSell, UnitPrice = 1.1m, Number = 100.0m, PayType = "bank,swift", PayTypeJson = "{\"bank\":\"10001\",\"swift\":\"10002\"}", LimitBasis = GateP2pAdLimitBasis.Fiat, FiatMinAmount = 100.0m, FiatMaxAmount = 110.0m, PolymarketRestricted = false, RateFixed = 1, ExpireMinutes = 20 });
var p2p_11 = await api.P2p.UpdateAdvertisementStatusAsync(new GateP2pAdStatusUpdateRequest { AdvertisementId = 2_124_000_001, Status = GateP2pAdStatusUpdate.Delisted });
var p2p_12 = await api.P2p.GetAdvertisementAsync(new GateP2pAdvertisementIdRequest { AdvertisementId = "2124000001" });
var p2p_13 = await api.P2p.GetMyAdvertisementsAsync(new GateP2pAdListRequest { Asset = "USDT", FiatUnit = "USD", TradeType = GateP2pOrderSide.Sell });
var p2p_14 = await api.P2p.GetAdvertisementsAsync(new GateP2pMarketAdListRequest { Asset = "USDT", FiatUnit = "USD", TradeType = GateP2pOrderSide.Sell });
var p2p_15 = await api.P2p.GetChatHistoryAsync(new GateP2pChatHistoryRequest { TransactionId = 40_000_001, LastReceived = DateTime.UtcNow.AddMinutes(-10), FirstReceived = DateTime.UtcNow.AddHours(-1) });
var p2p_16 = await api.P2p.SendChatMessageAsync(new GateP2pSendChatMessageRequest { TransactionId = 40_000_001, Type = GateP2pChatMessageType.Text, Message = "Payment completed, please check" });
var p2p_17 = await api.P2p.UploadChatFileAsync(new GateP2pUploadChatFileRequest { ContentType = "image/png", Base64Content = "BASE64-CONTENT" });
var p2p_18 = await api.P2p.SetMerchantWorkHoursAsync(new GateP2pMerchantWorkHoursRequest { WorkStatus = GateP2pMerchantWorkMode.CustomHours, CycleType = GateP2pMerchantWorkCycle.Weekly, DayOfWeek = "1,2,3,4,5", TimeZone = "+3", StartTime = "09:00", EndTime = "18:00" });

// Bot Methods
var bot_01 = await api.Bot.GetStrategyRecommendationsAsync(new GateBotRecommendationRequest { Market = "BTC_USDT", StrategyType = GateBotStrategyType.SpotGrid, Scene = GateBotDiscoverScene.TopOne, Limit = 1 });
var bot_02 = await api.Bot.CreateSpotGridAsync(new GateBotSpotGridCreateRequest { Market = "BTC_USDT", CreateParameters = new GateBotSpotGridCreateParameters { Money = 100.0m, LowPrice = 50000.0m, HighPrice = 70000.0m, GridNumber = 10, PriceType = GateBotGridPriceType.Arithmetic } });
var bot_03 = await api.Bot.CreateMarginGridAsync(new GateBotMarginGridCreateRequest { Market = "BTC_USDT", CreateParameters = new GateBotMarginGridCreateParameters { Money = 100.0m, LowPrice = 50000.0m, HighPrice = 70000.0m, GridNumber = 10, PriceType = GateBotGridPriceType.Arithmetic, Leverage = 3.0m, Direction = GateBotFuturesDirection.Long } });
var bot_04 = await api.Bot.CreateInfiniteGridAsync(new GateBotInfiniteGridCreateRequest { Market = "BTC_USDT", CreateParameters = new GateBotInfiniteGridCreateParameters { Money = 100.0m, PriceFloor = 50000.0m, ProfitPerGrid = 0.01m, GridNumber = 10, PriceType = GateBotGridPriceType.Arithmetic } });
var bot_05 = await api.Bot.CreateFuturesGridAsync(new GateBotFuturesGridCreateRequest { Market = "BTC_USDT", CreateParameters = new GateBotFuturesGridCreateParameters { Money = 100.0m, LowPrice = 50000.0m, HighPrice = 70000.0m, GridNumber = 10, PriceType = GateBotGridPriceType.Arithmetic, Leverage = 3.0m, Direction = GateBotFuturesDirection.Long } });
var bot_06 = await api.Bot.CreateSpotMartingaleAsync(new GateBotSpotMartingaleCreateRequest { Market = "BTC_USDT", CreateParameters = new GateBotSpotMartingaleCreateParameters { InvestAmount = 100.0m, PriceDeviation = 0.02m, MaxOrders = 5, TakeProfitRatio = 0.01m } });
var bot_07 = await api.Bot.CreateContractMartingaleAsync(new GateBotContractMartingaleCreateRequest { Market = "BTC_USDT", CreateParameters = new GateBotContractMartingaleCreateParameters { InvestAmount = 100.0m, PriceDeviation = 0.02m, MaxOrders = 5, TakeProfitRatio = 0.01m, Direction = GateBotContractMartingaleDirection.Buy, Leverage = 3.0m } });
var bot_08 = await api.Bot.GetRunningPortfoliosAsync(new GateBotRunningPortfolioQueryRequest { StrategyType = GateBotStrategyType.SpotGrid, Market = "BTC_USDT", Page = 1, PageSize = 20 });
var bot_09 = await api.Bot.GetPortfolioDetailAsync(new GateBotPortfolioDetailRequest { StrategyId = "STRATEGY-ID", StrategyType = GateBotStrategyType.SpotGrid });
var bot_10 = await api.Bot.StopPortfolioAsync(new GateBotPortfolioStopRequest { StrategyId = "STRATEGY-ID", StrategyType = GateBotStrategyType.SpotGrid });

// CrossEx Methods
var crossex_01 = await api.CrossEx.GetSymbolsAsync(new GateCrossExSymbolsQueryRequest { Symbols = new[] { "KRAKEN_FUTURE_ADA_USD" } });
var crossex_02 = await api.CrossEx.GetRiskLimitsAsync(new GateCrossExRiskLimitQueryRequest { Symbols = new[] { "BINANCE_FUTURE_BTC_USDT" } });
var crossex_03 = await api.CrossEx.GetTransferCoinsAsync(new GateCrossExTransferCoinQueryRequest { Coin = "USDT" });
var crossex_04 = await api.CrossEx.GetTransferHistoryAsync(new GateCrossExTransferHistoryQueryRequest { Coin = "USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100 });
var crossex_05 = await api.CrossEx.TransferAsync(new GateCrossExTransferRequest { Coin = "USDT", Amount = 100.0m, From = GateCrossExTransferAccountType.Spot, To = GateCrossExTransferAccountType.CrossExKraken, Text = "CLIENT-TRANSFER-ID" });
// A successful action response acknowledges asynchronous acceptance only. Confirm State via GetOrderAsync or the private order stream: FAIL is CrossEx validation; REJECT is venue rejection.
// LIGHTER futures use LIGHTER_FUTURE_ADA_USDC; change the symbol/amount only for an intended, eligible trade.
var crossex_06 = await api.CrossEx.PlaceOrderAsync(new GateCrossExOrderRequest { Symbol = "KRAKEN_FUTURE_ADA_USD", Side = GateCrossExOrderSide.Buy, Type = GateCrossExOrderType.Limit, TimeInForce = GateCrossExTimeInForce.GoodTillCancelled, Quantity = 1m, Price = 0.5m, Text = "client-order-id" });
var crossex_07 = await api.CrossEx.GetOrderAsync("ORDER-ID");
var crossex_08 = await api.CrossEx.UpdateOrderAsync("ORDER-ID", new GateCrossExOrderUpdateRequest { Quantity = 0.001m, Price = 61000.0m });
var crossex_09 = await api.CrossEx.CancelOrderAsync("ORDER-ID");
var crossex_09b = await api.CrossEx.CancelOrdersAsync(new[] { new GateCrossExBatchCancelOrderRequest { OrderId = "ORDER-ID" }, new GateCrossExBatchCancelOrderRequest { Text = "client-order-id" } });
var crossex_10 = await api.CrossEx.GetConvertQuoteAsync(new GateCrossExConvertQuoteRequest { ExchangeType = GateCrossExExchangeType.Gate, FromCoin = "USDT", ToCoin = "BTC", FromAmount = 100.0m });
var crossex_11 = await api.CrossEx.CreateConvertOrderAsync(new GateCrossExConvertOrderRequest { QuoteId = "QUOTE-ID" });
var crossex_12 = await api.CrossEx.GetAccountAsync(new GateCrossExAccountQueryRequest { ExchangeType = GateCrossExExchangeType.Gate });
var crossex_13 = await api.CrossEx.UpdateAccountAsync(new GateCrossExAccountUpdateRequest { PositionMode = GateCrossExPositionMode.Single, AccountMode = GateCrossExAccountMode.CrossExchange, ExchangeType = GateCrossExExchangeType.Gate });
var crossex_14 = await api.CrossEx.GetContractLeveragesAsync(new GateCrossExLeverageQueryRequest { Symbols = new[] { "BINANCE_FUTURE_BTC_USDT" } });
var crossex_15 = await api.CrossEx.UpdateContractLeverageAsync(new GateCrossExLeverageRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", Leverage = 5.0m });
var crossex_16 = await api.CrossEx.GetMarginLeveragesAsync(new GateCrossExLeverageQueryRequest { Symbols = new[] { "GATE_MARGIN_BTC_USDT" } });
var crossex_17 = await api.CrossEx.UpdateMarginLeverageAsync(new GateCrossExLeverageRequest { Symbol = "GATE_MARGIN_BTC_USDT", Leverage = 3.0m });
// Explicit financial action for an existing Hyperliquid isolated position only. Do not run this catalogue as a batch.
// The server truncates -30.129 to two decimal places; HTTP 202 is acceptance, not proof of completed adjustment.
var crossex_17b = await api.CrossEx.UpdateIsolatedMarginAsync(new GateCrossExIsolatedMarginRequest { Symbol = "HYPERLIQUID_FUTURE_CXMT_USDC", Margin = -30.129m, PositionSide = GateCrossExPositionSide.None });
var crossex_18 = await api.CrossEx.ClosePositionAsync(new GateCrossExClosePositionRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", PositionSide = GateCrossExPositionSide.Long }); // Requires no open orders and a position strictly below min notional or min size; PositionSide is required for margin positions.
var crossex_19 = await api.CrossEx.GetInterestRatesAsync(new GateCrossExCoinExchangeQueryRequest { Coin = "USDT", ExchangeType = GateCrossExExchangeType.Gate });
var crossex_20 = await api.CrossEx.GetFeesAsync();
var crossex_21 = await api.CrossEx.GetPositionsAsync(new GateCrossExPositionQueryRequest { Symbol = "KRAKEN_FUTURE_ADA_USD", ExchangeType = GateCrossExExchangeType.Kraken });
var crossex_22 = await api.CrossEx.GetMarginPositionsAsync(new GateCrossExPositionQueryRequest { Symbol = "GATE_MARGIN_BTC_USDT", ExchangeType = GateCrossExExchangeType.Gate });
var crossex_23 = await api.CrossEx.GetAdlRankAsync(new GateCrossExAdlRankQueryRequest { Symbol = "BINANCE_FUTURE_BTC_USDT" });
var crossex_24 = await api.CrossEx.GetOpenOrdersAsync(new GateCrossExOpenOrdersQueryRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", ExchangeType = GateCrossExExchangeType.Binance, BusinessType = GateCrossExBusinessType.Future });
var crossex_25 = await api.CrossEx.GetHistoricalOrdersAsync(new GateCrossExHistoryQueryRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100, Attributes = [GateCrossExOrderAttribute.Common, GateCrossExOrderAttribute.Settlement] });
var crossex_26 = await api.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100 });
var crossex_27 = await api.CrossEx.GetHistoricalMarginPositionsAsync(new GateCrossExHistoryQueryRequest { Symbol = "GATE_MARGIN_BTC_USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100 });
var crossex_28 = await api.CrossEx.GetMarginInterestHistoryAsync(new GateCrossExMarginInterestHistoryQueryRequest { Symbol = "GATE_MARGIN_BTC_USDT", ExchangeType = GateCrossExExchangeType.Gate, From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100 });
var crossex_29 = await api.CrossEx.GetTradeHistoryAsync(new GateCrossExHistoryQueryRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100 });
var crossex_30 = await api.CrossEx.GetAccountBookAsync(new GateCrossExAccountBookQueryRequest { Coin = "USDT", StatementType = "TRANSFER_IN", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100 });
var crossex_31 = await api.CrossEx.GetCoinDiscountRatesAsync(new GateCrossExCoinExchangeQueryRequest { Coin = "USDT", ExchangeType = GateCrossExExchangeType.Gate });
var crossex_32 = await api.CrossEx.GetMarketTickersAsync(new[] { "GATE_FUTURE_BTC_USDT", "GATE_SPOT_BTC_USDT" });
var crossex_33 = await api.CrossEx.GetMarketFundingInfoAsync(new[] { "BINANCE_FUTURE_BTC_USDT", "KRAKEN_FUTURE_BTC_USD" });
```

## WebSocket Api Examples

The Gate.IO.Api socket client provides several socket endpoint to which can be subscribed.

```csharp
var ws = new GateWebSocketClient();
ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

// TODO: Readme
```
