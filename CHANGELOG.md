## Change Log & Release Notes

* Unreleased
  * Added converter behavior tests.
    * Covered seconds and milliseconds timestamp deserialization, Gate decimal fields that may arrive as empty strings, mapped enum serialization/deserialization, array payload models, and stream event converters.
  * Added REST request construction smoke tests.
    * Added reusable test infrastructure for capturing outgoing HTTP requests without calling the live API.
    * Added Alpha client request construction tests for public GET query serialization, signed GET query/header serialization, and signed POST request-object body serialization.
  * Added APIv4 authentication contract tests.
    * Added fixture-backed tests for Gate's official REST signature examples for signed GET and POST requests.
    * Added authentication behavior tests for unsigned requests and missing credentials.
    * Exposed the internal REST signature builder to the test assembly so the production signing path is covered directly.
  * Added the initial automated test project and Alpha API fixtures.
    * Added an xUnit test project with shared JSON fixture loading, public HTTP capture helpers, and opt-in live public integration tests controlled by `GATEIO_RUN_LIVE_TESTS`.
    * Added documented Alpha response fixtures, captured live public Alpha responses, and contract tests for Alpha account, quote, order, currency, ticker, and token models.
    * Added REST client surface tests to guard the currently exposed top-level API modules.
    * Corrected Alpha quote `max_amount` handling to stay string-based because Gate's documented response can contain non-decimal values.
  * Added centralized request logging across REST and WebSocket clients.
    * REST requests now log start, success, failure, exception, elapsed time, endpoint, response type, and signed/request parameter metadata from the shared request path.
    * WebSocket subscribe, query, ping, and unsubscribe flows now log through shared stream client paths without duplicating logging in every module client.
  * Added Alpha REST API support against the current Gate Alpha API documentation.
    * Added `rest.Alpha` for Alpha account assets, account book history, quotes, order placement, order queries, currencies, tickers, and token information.
    * Added Alpha request models, response models, and separate enums for order side, gas mode, order status, quote error type, and currency status.
  * Added Announcements WebSocket stream support against the current Gate Announcement WebSocket documentation.
    * Added `stream.Announcements` with ping and subscriptions for listing, delisting, fee, ETF, deposit/withdrawal, rename, precision, and engine upgrade announcement summaries.
    * Added the Announcements WebSocket address, language/type enums, request handling, and announcement summary stream model.
  * Added CrossEx WebSocket stream support against the current Gate CrossEx WebSocket documentation.
    * Added `stream.CrossEx` with public market streams for last price, index price, mark price, order book, order book updates, tickers, trades, klines, funding rates, and open interest.
    * Added CrossEx private WebSocket login plus user order, asset, trade, position, margin position, and margin interest subscriptions.
    * Added CrossEx WebSocket API commands for order placement/cancel/update, futures and margin leverage updates, account settings updates, and full position close.
    * Added CrossEx WebSocket addresses, stream request/response models, public market payload models, and kline interval enum.
  * Added Unified WebSocket stream support against the current Gate Unified WebSocket documentation.
    * Added `stream.Unified` with ping, asset overview, and asset detail subscriptions.
    * Added the Unified WebSocket base address to client options and constants.
    * Added Unified stream models for account-level asset overview and per-currency asset detail updates.
  * Updated Options WebSocket streams against the current Gate Options WebSocket documentation.
    * Corrected the mark-price stream channel to `options.mark_prices`.
    * Fixed contract and underlying candlestick subscriptions to handle the documented array payloads and added the missing `10s`, `4h`, `8h`, `1d`, and `7d` interval values.
    * Added a legacy Options order-book subscription overload that can receive both `all` snapshots and `update` price-level notifications.
    * Updated Options stream parsing for empty book-ticker prices, empty ticker index prices, mapped stream enums, underlying ticker names, order message timestamps, and newly documented user-trade fields.
  * Added TradFi WebSocket stream support against the current Gate TradFi WebSocket documentation.
    * Added `stream.TradFi` with ping, ticker, candlestick, best bid/ask, user order, user position, and user balance subscriptions.
    * Added the TradFi WebSocket base address to client options and constants.
    * Added TradFi stream models and stream-specific enums for order operation type and position side.
    * Updated TradFi kline intervals with the WebSocket-documented `5m`, `30m`, and `1M` values.
  * Updated Delivery WebSocket streams against the current Gate Delivery WebSocket API documentation.
    * Added BTC-settled Delivery clients and WebSocket addresses alongside the existing USDT settlement.
    * Added normal Delivery candlestick subscription support in addition to mark-price candlesticks.
    * Added a Delivery legacy order-book subscription overload that handles both `all` snapshots and `update` price-level notifications.
    * Updated shared futures/delivery stream parsing for Delivery ticker fields, empty book-ticker prices, empty auto-order stop prices, and `all` stream response events.
  * Updated Futures WebSocket streams against the current Gate Futures WebSocket API documentation.
    * Added `futures.obu`, `futures.public_liquidates`, `futures.contract_stats`, and `futures.position_adl_rank` subscription methods.
    * Fixed `futures.order_book_update` validation to allow the documented `20ms` and `100ms` update speeds with the current depth limits.
    * Added Futures stream payload models for order book V2 updates, public liquidations, contract stats, and position ADL rank updates.
    * Updated Futures stream order book, candlestick, trade, order, position, liquidation, deleverage, balance, and auto-order models with documented fields, mapped enum converters, and decimal size handling.
  * Updated Spot WebSocket streams against the current Gate Spot WebSocket API documentation.
    * Added `spot.obu`, `spot.orders_v2`, `spot.usertrades_v2`, `spot.priceorders`, and deprecated `spot.trades_v2` subscription methods.
    * Updated Spot stream payload models with newly documented trade, candlestick, order book, balance, order, and price-order fields.
    * Fixed Spot order-book-difference interval validation to allow the documented `20ms` and `100ms` update speeds.
    * Fixed authenticated stream unsubscription to send the original channel, payload, and a fresh unsubscribe signature.
  * Cleaned project build warnings.
    * Added missing XML documentation summaries for public API models, request objects, stream payloads, stream clients, and enums.
    * Fixed malformed XML documentation comments so documentation generation succeeds cleanly.
  * Moved Spot and Options stream models into their owning module folders.
    * Moved Spot stream payload models from `Models/StreamApi/Spot` to `Spot/Stream` and renamed them from `SpotStream*` to `GateSpotStream*`.
    * Moved Options stream payload models from `Models/StreamApi/Options` to `Options/Stream` and renamed them from `OptionsStream*` to `GateOptionsStream*`.
    * Updated stream client subscriptions to use the new module namespaces and model names while leaving the root `Models/StreamApi` request/response/status/latency models in place.
  * Added Bot REST API support against the current Gate API v4 Bot documentation.
    * Added `api.Bot` and a new `Bot` module with client, enum, request, and response models.
    * Covered AIHub strategy recommendations, spot grid, margin grid, infinite grid, futures grid, spot martingale, contract martingale, running strategy list, strategy detail, and strategy stop endpoints.
    * Unwrapped Bot `code`/`message` response envelopes so public methods return the documented `data` payloads.
    * Added request-object overloads for recommendation, strategy creation, running strategy, detail, and stop calls, including optional Bot request headers.
    * Kept semantically numeric Bot amounts, prices, ratios, leverage, PnL values, counts, durations, and timestamps typed as numeric or `DateTime` public properties while sending Gate's documented string body/query values where required.
    * Updated examples and README with Bot request-object overload usage.
  * Added CrossEx REST API support against the current Gate API v4 CrossEx documentation.
    * Added `api.CrossEx` and a new `CrossEx` module with client, enum, request, and response models.
    * Covered trading pair, risk-limit, transfer currency, transfer history/create, order create/detail/update/cancel, convert quote/order, account query/update, leverage query/update, close position, interest-rate, fee, position, ADL rank, open-order, historical order/position/margin-interest/trade, account-book, and coin-discount-rate endpoints.
    * Added request-object overloads for query and mutation calls, including transfer, order, convert, account, leverage, position, history, account-book, and coin-discount-rate requests.
    * Kept semantically numeric CrossEx IDs, amounts, prices, quantities, rates, leverage, fees, limits, and timestamps typed as numeric or `DateTime` public properties while sending Gate's documented string body values where required.
    * Sent CrossEx millisecond time filters for history/account-book endpoints and Unix-second filters for transfer history according to the current docs.
    * Split CrossEx enum definitions into separate files to match the request and response model layout.
    * Updated examples and README with CrossEx request-object overload usage.
  * Added P2P REST API support against the current Gate API v4 P2P documentation.
    * Added `api.P2p` and a new `P2p` module with client, enum, request, and response models.
    * Covered account info, counterparty info, payment methods, pending/completed order list, order detail, payment/receipt/cancel actions, advertisement publish/status/detail/list, chat history, chat send, and chat file upload endpoints.
    * Added request-object overloads for account, payment, order, advertisement, and chat endpoints, including the high-parameter advertisement publish/edit call.
    * Unwrapped P2P `code`/`message` response envelopes for data endpoints while preserving action result metadata for mutation endpoints.
    * Kept semantically numeric P2P order/ad/payment IDs, prices, amounts, rates, counts, and timestamps as numeric or `DateTime` public properties while sending Gate's documented string body values where required.
    * Split P2P request and response models into separate files for easier review and maintenance.
    * Split P2P enum definitions into separate files to match the request and response model layout.
    * Updated examples and README with P2P request-object overload usage.
  * Added OTC REST API support against the current Gate API v4 OTC documentation.
    * Added `api.Otc` and a new `Otc` module with client, enum, request, and response models.
    * Covered quote, fiat order create/paid/cancel/list/detail, stablecoin order create/list, default bank account, and bank account list endpoints.
    * Added request-object overloads for quote, fiat order creation, stablecoin order creation, order actions, and fiat/stablecoin order list queries.
    * Unwrapped OTC `code`/`message` response envelopes for data endpoints while preserving action result metadata for order actions.
    * Kept semantically numeric order/user/bank IDs, amounts, rates, counts, and timestamps as numeric or `DateTime` public properties while sending string body/query values where the API requires them.
    * Split OTC request and response models into separate files for easier review and maintenance.
    * Split OTC enum definitions into separate files to match the request and response model layout.
    * Updated examples and README with OTC request-object overload usage.
  * Added Earn REST API support against the current Gate API v4 Earn documentation.
    * Added `api.Earn` and a new `Earn` module with client, enum, request, and response models.
    * Covered Dual Investment product/order/balance/refund/reinvest/recommendation endpoints.
    * Covered Staking coin, swap, order, award, and asset endpoints.
    * Covered Auto Invest plan create/update/stop/add-position, coin, minimum-amount, execution-record, order-detail, configuration, detail, and list endpoints.
    * Covered Fixed-Term Earn product list, product-by-asset list, subscription list/create, early redemption, and history endpoints.
    * Added request-object overloads for high-parameter Earn methods and kept semantically numeric IDs, amounts, rates, prices, quantities, and timestamps typed as numeric or `DateTime` public properties.
    * Split Earn request and response models into separate files for easier review and maintenance.
    * Split Earn enum definitions into separate files to match the request and response model layout.
    * Updated examples and README with Earn request-object overload usage.
  * Added Multi-Collateral Loan REST API support against the current Gate API v4 Multi-collateral-loan documentation.
    * Added `api.MultiCollateralLoan` and a new `MultiCollateralLoan` module with client, enum, request, and response models.
    * Covered order list/create/detail, repayment record/create, collateral record/adjustment, quota, supported currencies, LTV, fixed-rate, and current-rate endpoints.
    * Added request-object overloads for order queries and creation, repayment records and repayment, collateral records and adjustment, currency quota, and current-rate queries.
    * Kept semantically numeric order/record IDs, amounts, prices, LTV values, quotas, and rates as numeric public properties while supporting Gate's numeric string responses.
    * Sent Multi-Collateral Loan time filters as Unix seconds and mapped response timestamps with the existing date converter.
    * Updated examples and README with Multi-Collateral Loan request-object overload usage.
  * Added EarnUni REST API support against the current Gate API v4 EarnUni documentation.
    * Added `api.EarnUni` and a new `EarnUni` module with client, enum, request, and response models.
    * Covered lending currencies, lending orders, lending records, total interest, interest records, interest status, annualized chart, and estimated-rate endpoints.
    * Added request-object overloads for lend queries, lend create/update calls, lend-record queries, interest-record queries, and chart queries.
    * Kept semantically numeric EarnUni amounts, rates, and chart values as decimal public properties while supporting Gate's numeric string responses.
    * Sent EarnUni time filters as Unix seconds and preserved millisecond response timestamp deserialization through the existing date converter.
    * Updated examples and README with EarnUni request-object overload usage.
    * Removed the obsolete `SYNC.md` workspace sync note.
  * Updated rebate endpoints against the current Gate API v4 Rebate documentation.
    * Fixed agency transaction and commission history methods to deserialize the documented `total`/`list` response objects instead of lists.
    * Added `Rebate/Requests` request models for transaction history, commission history, broker history, partner subordinate lists, user subordinate relationships, and partner aggregated data.
    * Switched Rebate `DateTime` query filters to Unix seconds instead of milliseconds.
    * Added partner transaction history, partner commission history, partner subordinate list, broker commission history, broker transaction history, user rebate info, user subordinate relationship, recent partner application, partner eligibility, and partner aggregated data endpoints.
    * Added Rebate response models for broker sub-broker info, partner subordinate users, user subordinate relationships, partner application records, eligibility, and aggregated partner statistics.
    * Kept semantically numeric Rebate IDs, amounts, fees, rates, counts, and totals as numeric public properties while supporting Gate's numeric string responses.
    * Updated examples and README with Rebate request-object overload usage.
  * Updated account endpoints against the current Gate API v4 Account documentation.
    * Added `GetMainKeysAsync` for `GET /account/main_keys` with `GateAccountKeyInfo`, API key permission, and API key state models.
    * Added `Account/Requests` request models for STP group create/query, STP group user add/remove, and GT fee deduction calls.
    * Added request-object overloads for STP group create/query, STP group user add/remove, and GT fee deduction updates.
    * Added correctly named `AddUsersToStpGroupAsync`, `RemoveUserFromStpGroupAsync`, `RemoveUsersFromStpGroupAsync`, `GetDebitFeeAsync`, and `SetDebitFeeAsync` while preserving the existing compatibility methods.
    * Updated STP group user removal to support multiple documented `user_id` values as a comma-separated query parameter.
    * Updated Account response models with `CurrencyPairs`, `CreatorId`, main-key permission sections, and the documented legacy unified API key mode while preserving old aliases.
    * Updated examples and README with Account request-object overload usage.
  * Updated options endpoints against the current Gate API v4 Options documentation.
    * Added request-object overloads for options contract, settlement, user settlement, order book, candlestick, trade, account-book, position, liquidation, order, cancel-order, countdown cancel-all, user trade, and MMP settings calls.
    * Added `Options/Requests` request models for the new Options client overloads.
    * Added `AmendOrderAsync` and `UpdateOrderAsync` for `PUT /options/orders/{order_id}`.
    * Fixed Options `DateTime` query filters to send Unix seconds instead of milliseconds.
    * Fixed `GetBalanceAsync` to call `GET /options/accounts` instead of the public trades endpoint.
    * Fixed options liquidation history queries to send the documented `underlying` and `contract` query parameters.
    * Added missing options enum values for one-day contracts, single-currency/portfolio margin modes, and point account-book change types.
    * Updated Options response models with missing contract strike/price-limit fields, position Greeks, typed enum converters, and numeric order book/user-trade IDs and sizes.
    * Updated examples and README with Options request-object overload usage.
  * Added TradFi REST API support against the current Gate API v4 TradFi documentation.
    * Added `api.TradFi` and a new `TradFi` module with client, enum, request, and response models.
    * Covered MT5 account, symbol categories, symbols, symbol details, klines, tickers, user creation, account assets, transactions, orders, order history, positions, and position history endpoints.
    * Added request-object overloads for TradFi symbol details, klines, transaction queries/transfers, order creation/update/history, and position update/close/history calls.
    * Unwrapped TradFi response envelopes so public methods return the documented data payloads in the same style as the other clients.
    * Kept semantically numeric TradFi IDs, prices, volumes, balances, and PnL fields as numeric public properties while preserving string fields where the API can return empty values.
    * Updated examples and README with TradFi usage.
  * Updated delivery endpoints against the current Gate API v4 Delivery documentation.
    * Added request-object overloads for delivery trades, candlesticks, balance history, orders, order cancellation, user trades, position close history, liquidations, settlements, risk-limit tiers, and price-triggered order queries/cancellation.
    * Added `Delivery/Requests` query and cancellation models for the new Delivery client overloads.
    * Switched Delivery `DateTime` query filters for trades, candlesticks, and account-book history to Unix seconds.
    * Fixed Delivery account queries to deserialize the documented single account object instead of a list.
    * Updated Delivery contract and risk-limit tier models with `settle_fee_rate` and `deduction`.
    * Added shared futures response fields used by Delivery for account user IDs, account-book IDs, price-triggered order string IDs, and decimal trigger-order amounts.
    * Relaxed Delivery contract validation to accept both documented contract formats (`BASE_QUOTE` and `BASE_QUOTE_YYYYMMDD`).
    * Added `GateFuturesSelfTradeAction.None` for the `"-"` value returned by Delivery order responses.
    * Updated examples and README with Delivery request-object overload usage.
  * Updated futures endpoints against the current Gate API v4 Futures documentation.
    * Added request-object overloads for futures trades, candlesticks, funding rates, stats, liquidations, balance history, positions, orders, user trades, position close history, ADL history, countdown cancel-all, and price-triggered order queries.
    * Added batch funding-rate history, historical position time-range, and futures trail-order endpoints.
    * Switched Futures `DateTime` query filters to Unix seconds.
    * Fixed futures position queries to send the documented `holding`, `limit`, and `offset` query parameters.
    * Fixed futures cross-margin mode endpoints to send body parameters instead of query parameters.
    * Added `GetLeverageAsync` and optional `pid` support for futures leverage updates.
    * Added `market_order_slip_ratio` support to futures order requests and mapped enum serialization for futures order and price-triggered order request bodies.
    * Added Futures response fields for contract funding and market-order metadata, order value/slippage/margin-mode metadata, and position leverage/margin mode.
    * Fixed cancellation of all futures price-triggered orders so omitting the contract cancels all documented matches.
    * Fixed the duplicate `GateFuturesOrderFinishAs.SelfTradePrevention` enum value.
    * Updated examples and README with Futures request-object overload usage.
  * Audited the recent documentation updates for string-backed numeric fields and restored public numeric types where the API value is semantically numeric.
    * Kept Wallet transaction, transfer, small-balance, and transfer-status IDs numeric while still allowing JSON numeric strings to deserialize.
    * Restored isolated margin balance-history IDs and lending tier amounts to numeric types.
    * Restored Flash Swap preview IDs to numeric types in request and response models while keeping the string preview-id overload for numeric string inputs.
    * Updated Spot currency supply and market-cap fields to decimal values.
    * Left semantic string fields as strings where the API can return non-numeric values, such as withdrawal IDs and open-ended unified tier limits.
  * Updated flash swap endpoints against the current Gate API v4 Flash Swap documentation.
    * Added request-object overloads for flash swap currency-pair queries and order-list queries.
    * Added `GateSwapPreviewRequest` for flash swap order previews and kept the old `GateSwapOrderRequest` preview overload as an obsolete compatibility path.
    * Updated flash swap preview flows to support providing either `sell_amount` or `buy_amount`.
    * Kept flash swap preview IDs as numeric values in request and preview response models and added a string-based `PlaceOrderAsync` overload for numeric string inputs.
    * Updated flash swap currency-pair query default limit to 1000 and validate it against the documented 1-1000 range.
    * Added `GateSwapOrder.UpdateTime` for the `update_time` value returned by flash swap order responses.
    * Updated examples and README with Flash Swap request-object overload usage.
  * Updated spot endpoints against the current Gate API v4 Spot documentation.
    * Added request-object overloads for market trades, private trades, candlesticks, transaction history, open orders, orders, personal trade history, countdown cancel-all, insurance history, and price-triggered order queries.
    * Added `Spot/Requests` query models for the new Spot client overloads.
    * Switched Spot `DateTime` query filters to Unix seconds for trade, candlestick, order, account-book, insurance, and personal trade-history queries.
    * Added Spot order `slippage` support for market orders.
    * Fixed Spot order body serialization by applying mapped enum converters to order, cancel, close-position, and price-triggered order requests.
    * Fixed batch Spot order cancellation to send cancel requests in the request body.
    * Removed stale Spot validation that rejected `auto_borrow` and `auto_repay` when both are enabled.
    * Added `GateSpotPriceTriggeredOrderAccountType` for price-triggered order account values `normal`, `margin`, and `unified`.
    * Fixed personal Spot trade history so order filters are optional.
    * Added missing Spot response fields for currency supply/category data, market price-limit data, slippage, GT maker/taker fees, trade deal value, RPI maker fee, and RPI market maker status.
    * Updated Spot response models for fractional `create_time_ms` values and added the `price_protect_cancelled` finish status.
    * Added correctly spelled `GateSpotOrder.FinishAs` while preserving the obsolete `FiniashAs` compatibility alias.
    * Updated examples and README with Spot request-object overload usage.
  * Updated isolated margin endpoints against the current Gate API v4 Isolated-Margin documentation.
    * Added request-object overloads for balance history, transferable amount, borrow/repay, loans, loan records, interest records, maximum borrowable, and leverage setting.
    * Added `Margin/Requests` request models for the isolated margin client.
    * Fixed balance history and interest history query timestamps to send Unix seconds.
    * Added `from` and `to` filters for isolated margin interest record queries.
    * Stopped sending the stale `type` query parameter for isolated margin interest record queries.
    * Fixed `SetAutoRepaymentAsync` to send `status` as a query parameter.
    * Fixed authentication flags for isolated margin estimate-rate and current lending-tier endpoints.
    * Updated borrow/repay and leverage-setting calls to return no-content results per the current API.
    * Split isolated margin loan records into `GateMarginLoanRecord` and added `GateMarginLoanType` for active loans and interest records.
    * Updated isolated margin response models for timestamp converters, interest statuses, and tier upper-limit/tier-amount values.
    * Updated examples and README with Isolated Margin request-object overload usage.
  * Updated unified endpoints against the current Gate API v4 Unified documentation.
    * Added request-object overloads for account info, borrow/repay, loans, loan history, interest history, account mode, leverage settings, historical lending rates, and collateral currency updates.
    * Added `GetBatchBorrowableAsync` for unified account batch borrowable queries.
    * Fixed `SetAccountModeAsync` to call `PUT /unified/unified_mode` and include nested settings such as the options switch.
    * Updated unified interest record queries to send `from` and `to` as Unix seconds.
    * Fixed public unified endpoints to be called without authentication for currency discount tiers, loan margin tiers, portfolio calculator, supported currencies, and historical lending rates.
    * Flattened nested currency discount tier responses.
    * Updated borrow/repay calls to return `GateUnifiedLoanResult`.
    * Updated unified response models for account mode, options order loss, balance versions, interest statuses, loan timestamps, loan margin tiers, portfolio risk units, repay types, and open-ended tier limits.
    * Added correctly spelled `SetCollateralCurrenciesAsync` while preserving the existing `SetCollateralCurenciesAsync` compatibility method.
    * Updated examples and README with Unified request-object overload usage.
  * Updated sub-account endpoints against the current Gate API v4 SubAccount documentation.
    * Added request-object overloads for creating sub-accounts and creating/updating sub-account API keys.
    * Added `GateSubAccountCreateRequest` and `GateSubAccountApiKeyRequest` under `SubAccount/Requests`.
    * Fixed `LockSubAccountAsync` and `UnlockSubAccountAsync` to call `POST` instead of `PUT`.
    * Updated `UpdateApiKeyAsync` to return a no-content result (`RestCallResult<object>`) per the current API response.
    * Added `GateSubAccountApiKey.Secret` and typed API key states with `GateSubAccountApiKeyState`.
    * Added `GateUnifiedAccountMode.SingleCurrency` for sub-account unified mode responses.
    * Updated examples and README with SubAccount request-object overload usage.
  * Updated wallet endpoints against the current Gate API v4 Wallet documentation.
    * Added request-object overloads for high-parameter Wallet methods: withdrawal/deposit record queries, trading-account transfers, main-sub transfers, main-sub transfer history, sub-account to sub-account transfers, sub-account balance queries, and UID transfer history.
    * Added `GetLowCapExchangeListAsync` for `GET /wallet/getLowCapExchangeList`.
    * Fixed UID transfer history to call `GET /wallet/push` instead of the withdrawal push endpoint.
    * Switched Wallet time query parameters to Unix seconds.
    * Flattened nested-list Wallet responses for withdrawal records, sub-account futures balances, small balances, and small balance history.
    * Updated Wallet response models with missing fields for currency chains, deposit addresses, withdrawal records, sub-account balances, trading fees, total balances, small balance history, and UID transfer history.
    * Fixed Wallet response field types for withdrawal fee percentages and sub-account margin locked amounts while keeping numeric IDs numeric.
    * Added `GateWalletSubAccountType.Options`.
    * Updated examples and README with Wallet request-object overload usage and the low-cap token endpoint.
  * Updated wallet withdrawal endpoints against the current Gate API v4 Withdrawal documentation.
    * Fixed `GateWalletRestApiClient.WithdrawAsync` parameter ordering so the sixth argument maps to `withdraw_order_id`.
    * Added `GateWalletWithdrawalRequest` and `WithdrawAsync(GateWalletWithdrawalRequest request, CancellationToken ct = default)`.
    * Added string-based `CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)` while preserving the existing long overload.
    * Kept `GateWalletTransaction.Id`, `GateWalletTransaction.BlockNumber`, and `GateWalletTransferId.Id` as numeric public properties while supporting Gate's numeric string responses.
    * Added newer withdrawal status enum values: `CANCELPEND`, `FVERIFY`, `LOCKED`, and `REJECT`.
    * Added `GateRestApiClient.Withdrawal` as an alias to `Wallet` for discoverability without moving ownership away from the Wallet client.

* Version 4.105.10 - 19 Oct 2025
  * ApiSharp updated to version 4.1.0

* Version 4.105.9 - 28 Sep 2025
  * Added missing endpoints as below
    * GateSpotRestApiClient
      - GetTransactionHistoryAsync
      - AmendOrdersAsync
      - GetInsuranceHistoryAsync

    * GateMarginRestApiClient
      - GetUserLendingTiersAsync
      - GetCurrentLendingTiersAsync
      - SetLeverageAsync
      - GetIsolatedBalancesAsync

    * GateOptionsRestApiClient
      - GetAccountAsync
      - CancelAllAsync
      - SetMMPAsync
      - GetMMPAsync
      - ResetMMPAsync

    * GateUnifiedRestApiClient
      - GetTransferablesAsync
      - GetCurrencyDiscountTiersAsync
      - GetLoanMarginTiersAsync
      - CalculatePortfolioAsync
      - GetCurrenciesAsync
      - GetHistoricalLendingRatesAsync
      - SetCollateralCurenciesAsync

* Version 4.105.5 - 13 Aug 2025
  * Update API to version 4.105.4 and refactor margin methods
  * Refactored margin methods to use isolated margin.
  * Renamed `SetPositionMarginAsync` to `SetMarginAsync`.
  * Added new methods for borrowing, repaying, and market details.
  * Introduced `GateFuturesContractStatus`, `GateMarginRepaymentType`, `GateMarginUniInterestStatus`, and `GateMarginUniOrderType` enums.
  * Enhanced records with new properties for better data structure.
  * Updated `GateWalletTransaction` with additional fields.
  * Removed `GateMarginUni` classes for a unified margin system.

* Version 4.105.4 - 13 Aug 2025
  * Update API to version [4.105.4](https://www.gate.com/docs/developers/apiv4/en/#gate-api-v4-105-4) and refactor margin methods
  * Refactored margin methods to use isolated margin.
  * Renamed `SetPositionMarginAsync` to `SetMarginAsync`.
  * Added new methods for borrowing, repaying, and market details.
  * Introduced `GateFuturesContractStatus`, `GateMarginRepaymentType`, `GateMarginUniInterestStatus`, and `GateMarginUniOrderType` enums.
  * Enhanced records with new properties for better data structure.
  * Updated `GateWalletTransaction` with additional fields.
  * Removed `GateMarginUni` classes for a unified margin system.
  * Updated namespaces for better organization.

* Version 4.5.811 - 11 Aug 2025
  * ApiSharp updated to version 4.0.1.

* Version 4.5.711 - 11 Jul 2025
  * Fixed [Issue 8](https://github.com/burakoner/Gate.IO.Api/issues/8)

* Version 4.5.612 - 12 Jun 2025
  * Added [Margin Uni](https://www.gate.com/docs/developers/apiv4/en/#marginuni) Section with all endpoints.
  * Added [Futures List risk limit tiers](https://www.gate.com/docs/developers/apiv4/en/#list-risk-limit-tiers) API endpoint.
  * Added [Delivery List risk limit tiers](https://www.gate.com/docs/developers/apiv4/en/#list-risk-limit-tiers-2) API endpoint.
