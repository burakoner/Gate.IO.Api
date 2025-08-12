## Change Log & Release Notes

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
