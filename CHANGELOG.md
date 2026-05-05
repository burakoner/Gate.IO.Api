## Change Log & Release Notes

* Unreleased
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
