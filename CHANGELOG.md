## Change Log & Release Notes

* Unreleased
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
