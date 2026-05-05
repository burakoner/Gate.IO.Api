## Change Log & Release Notes

* Unreleased
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
    * Updated isolated margin response models for string record IDs, timestamp converters, interest statuses, and tier upper-limit/tier-amount values.
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
    * Fixed Wallet response field types for transfer status IDs, withdrawal fee percentages, sub-account margin locked amounts, and small balance history IDs.
    * Added `GateWalletSubAccountType.Options`.
    * Updated examples and README with Wallet request-object overload usage and the low-cap token endpoint.
  * Updated wallet withdrawal endpoints against the current Gate API v4 Withdrawal documentation.
    * Fixed `GateWalletRestApiClient.WithdrawAsync` parameter ordering so the sixth argument maps to `withdraw_order_id`.
    * Added `GateWalletWithdrawalRequest` and `WithdrawAsync(GateWalletWithdrawalRequest request, CancellationToken ct = default)`.
    * Added string-based `CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)` while preserving the existing long overload.
    * Changed `GateWalletTransaction.Id`, `GateWalletTransaction.BlockNumber`, and `GateWalletTransferId.Id` to string to match API responses.
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
