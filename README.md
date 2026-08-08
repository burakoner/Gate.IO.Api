# Gate.IO.Api

A .Net wrapper for the Gate.IO API as described on [Gate.IO](https://www.gate.io/docs/developers/apiv4/en/), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/burakoner/Gate.IO.Api/issues)**

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
var wallet_09 = await api.Wallet.TransferBetweenMainAndSubAccountsAsync("CURRENCY", 1_000_000_000, GateWalletTransferDirection.From, 100.0m);
var wallet_10 = await api.Wallet.GetTransfersBetweenMainAndSubAccountsAsync();
var wallet_11 = await api.Wallet.TransferBetweenSubAccountsAsync("CURRENCY", 1_000_000_000, GateWalletSubAccountType.Spot, 2_000_000_000, GateWalletSubAccountType.Futures, 100.0m);
var wallet_12 = await api.Wallet.GetWithdrawalStatusAsync();
var wallet_13 = await api.Wallet.GetSubAccountBalancesAsync();
var wallet_14 = await api.Wallet.GetSubAccountMarginBalancesAsync();
var wallet_15 = await api.Wallet.GetSubAccountFuturesBalancesAsync();
var wallet_16 = await api.Wallet.GetSubAccountCrossMarginBalancesAsync();
var wallet_17 = await api.Wallet.GetSavedAddressesAsync("CURRENCY");
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
var unified_21 = await api.Unified.GetCurrenciesAsync();
var unified_22 = await api.Unified.GetHistoricalLendingRatesAsync(new GateUnifiedHistoricalLendingRatesQueryRequest { Currency = "CURRENCY", Tier = "1" });
var unified_23 = await api.Unified.SetCollateralCurrenciesAsync(new GateUnifiedCollateralCurrenciesRequest { Type = GateUnifiedCollateralType.Custom, EnableList = new List<string> { "BTC" }, DisableList = new List<string> { "ETH" } });

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

// Isolated Margin Methods
var margin_01 = await api.IsolatedMargin.GetBalancesAsync("SYMBOL");
var margin_02 = await api.IsolatedMargin.GetBalanceHistoryAsync(new GateMarginBalanceHistoryQueryRequest { Symbol = "SYMBOL", Currency = "CURRENCY", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var margin_03 = await api.IsolatedMargin.GetFundingBalancesAsync("CURRENCY");
var margin_04 = await api.IsolatedMargin.SetAutoRepaymentAsync(GateMarginAutoRepaymentStatus.Enabled);
var margin_05 = await api.IsolatedMargin.GetAutoRepaymentAsync();
var margin_06 = await api.IsolatedMargin.GetTransferableAmountAsync(new GateMarginTransferableAmountRequest { Currency = "CURRENCY", Symbol = "SYMBOL" });
var margin_07 = await api.IsolatedMargin.GetMarketsAsync();
var margin_08 = await api.IsolatedMargin.GetMarketsAsync("SYMBOL");
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

// Dictionary Access for Futures (Perpetual & Delivery) Methods
var sample_11 = await api.Futures[GateFuturesSettlement.BTC].GetContractsAsync();
var sample_13 = await api.Futures[GateFuturesSettlement.USDT].GetContractsAsync();
var sample_14 = await api.Delivery[GateDeliverySettlement.USDT].GetContractsAsync();

// Perpetual Futures Methods
var settle = GateFuturesSettlement.USDT;
var perpetual_01 = await api.Futures[settle].GetContractsAsync();
var perpetual_02 = await api.Futures[settle].GetContractAsync("CONTRACT");
var perpetual_03 = await api.Futures[settle].GetOrderBookAsync("CONTRACT");
var perpetual_04 = await api.Futures[settle].GetTradesAsync(new GateFuturesTradeQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Limit = 100 });
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
var perpetual_17 = await api.Futures[settle].GetPositionsAsync(new GateFuturesPositionQueryRequest { Holding = true, Limit = 100 });
var perpetual_17b = await api.Futures[settle].GetHistoricalPositionsAsync(new GateFuturesHistoricalPositionQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_18 = await api.Futures[settle].GetPositionAsync("CONTRACT");
var perpetual_19 = await api.Futures[settle].SetPositionMarginAsync("CONTRACT", 100.0M);
var perpetual_19b = await api.Futures[settle].GetLeverageAsync("CONTRACT", GateFuturesPositionMarginMode.Isolated, GateFuturesDualModeSide.DualLong);
var perpetual_20 = await api.Futures[settle].SetLeverageAsync("CONTRACT", 10);
var perpetual_21 = await api.Futures[settle].SetMarginModeAsync("CONTRACT", GateFuturesMarginMode.Cross);
var perpetual_22 = await api.Futures[settle].SwithMarginModeUnderHedgeAsync("CONTRACT", GateFuturesMarginMode.Isolated);
var perpetual_23 = await api.Futures[settle].SetRiskLimitAsync("CONTRACT", 25);
var perpetual_24 = await api.Futures[settle].SetDualModeAsync(true);
var perpetual_25 = await api.Futures[settle].GetDualModePositionsAsync("CONTRACT");
var perpetual_26 = await api.Futures[settle].SetDualModeMarginAsync("CONTRACT", GateFuturesDualModeSide.DualLong, 100);
var perpetual_27 = await api.Futures[settle].SetDualModeLeverageAsync("CONTRACT", 10);
var perpetual_28 = await api.Futures[settle].SetDualModeRiskLimitAsync("CONTRACT", 25);
var perpetual_29 = await api.Futures[settle].PlaceOrderAsync("CONTRACT", 25);
var perpetual_30 = await api.Futures[settle].PlaceOrderAsync(new GateFuturesOrderRequest { Contract = "CONTRACT", Size = 25, Price = 100.0m, TimeInForce = GateFuturesTimeInForce.GoodTillCancelled, MarketOrderSlipRatio = 0.03m });
var perpetual_30b = await api.Futures[settle].GetOrdersAsync(new GateFuturesOrderQueryRequest { Contract = "CONTRACT", Status = GateFuturesOrderStatus.Open, Limit = 100 });
var perpetual_31 = await api.Futures[settle].GetOrderAsync();
var perpetual_32 = await api.Futures[settle].CancelOrderAsync();
var perpetual_33 = await api.Futures[settle].AmendOrderAsync();
var perpetual_34 = await api.Futures[settle].GetUserTradesAsync("CONTRACT", orderId: 1_000_000_001);
var perpetual_35 = await api.Futures[settle].GetUserTradesAsync(new GateFuturesUserTradeQueryRequest { Contract = "CONTRACT", OrderId = 1_000_000_001, Limit = 100 });
var perpetual_36 = await api.Futures[settle].GetUserTradesAsync(new GateFuturesUserTradeTimeRangeQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Role = GateFuturesTradeRole.Maker });
var perpetual_37 = await api.Futures[settle].GetPositionClosesAsync();
var perpetual_38 = await api.Futures[settle].GetPositionClosesAsync(new GateFuturesPositionCloseQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_39 = await api.Futures[settle].GetUserLiquidationsAsync();
var perpetual_40 = await api.Futures[settle].GetAdlHistoryAsync("CONTRACT");
var perpetual_41 = await api.Futures[settle].GetAdlHistoryAsync(new GateFuturesAdlHistoryQueryRequest { Contract = "CONTRACT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow });
var perpetual_42 = await api.Futures[settle].CancelAllAsync(new GateFuturesCountdownCancelAllRequest { Timeout = 30, Contract = "CONTRACT" });
var perpetual_43 = await api.Futures[settle].GetTradingFeesAsync();
var perpetual_44 = await api.Futures[settle].CancelOrdersAsync([]);
var perpetual_45 = await api.Futures[settle].AmendOrdersAsync([]);
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
    GateFuturesTriggerType.CloseShortPosition,
    GateFuturesTriggerPrice.MarkPrice,
    GateFuturesTriggerStrategy.ByPrice,
    GateSpotTriggerCondition.GreaterThanOrEqualTo,
    100.01m, TimeSpan.FromMinutes(15), "CONTRACT", 100.00m, 25, true,
    GateFuturesTimeInForce.GoodTillCancelled,
    "CLIENT-ORDER-ID", false, GateFuturesOrderAutoSize.CloseLong
);
var perpetual_48 = await api.Futures[settle].PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest
{
    PositionMarginMode = GateFuturesPositionMarginMode.Cross,
    Order = new GateFuturesInitial { Contract = "CONTRACT", Amount = "0.5", Price = "0", TimeInForce = GateFuturesTimeInForce.ImmediateOrCancel },
    Trigger = new GateFuturesTrigger { PriceType = GateFuturesTriggerPrice.MarkPrice, Price = "100.01", Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo }
});
var perpetual_48b = await api.Futures[settle].AmendPriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderUpdateRequest { OrderId = 1_000_000_001, Amount = "0.25", TriggerPrice = "101.00", PriceType = GateFuturesTriggerPrice.MarkPrice });
var perpetual_49 = await api.Futures[settle].GetPriceTriggeredOrdersAsync(new GateFuturesPriceTriggeredOrderQueryRequest { Status = GateSpotTriggerFilter.Open, Contract = "CONTRACT", Limit = 100 });
var perpetual_50 = await api.Futures[settle].CancelPriceTriggeredOrdersAsync();
var perpetual_51 = await api.Futures[settle].GetPriceTriggeredOrderAsync(1_000_000_001);
var perpetual_52 = await api.Futures[settle].CancelPriceTriggeredOrderAsync(1_000_000_001);

// TradFi Methods
var tradfi_01 = await api.TradFi.GetMt5AccountAsync();
var tradfi_02 = await api.TradFi.GetSymbolCategoriesAsync();
var tradfi_03 = await api.TradFi.GetSymbolsAsync();
var tradfi_04 = await api.TradFi.GetSymbolDetailsAsync(new GateTradFiSymbolDetailsRequest { Symbols = ["XAUUSD"] });
var tradfi_05 = await api.TradFi.GetCandlesticksAsync(new GateTradFiCandlestickQueryRequest { Symbol = "XAUUSD", Interval = GateTradFiKlineInterval.OneHour, Limit = 100 });
var tradfi_06 = await api.TradFi.GetTickerAsync("XAUUSD");
var tradfi_07 = await api.TradFi.CreateUserAsync();
var tradfi_08 = await api.TradFi.GetAccountAssetsAsync();
var tradfi_09 = await api.TradFi.GetTransactionsAsync(new GateTradFiTransactionQueryRequest { BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, Page = 1, PageSize = 50 });
var tradfi_10 = await api.TradFi.CreateTransactionAsync(new GateTradFiTransactionRequest { Asset = "USDT", Change = 100.0m, Type = GateTradFiTransactionType.Deposit });
var tradfi_11 = await api.TradFi.GetOrdersAsync();
var tradfi_12 = await api.TradFi.PlaceOrderAsync(new GateTradFiOrderRequest { Symbol = "XAUUSD", Side = GateTradFiOrderSide.Buy, PriceType = GateTradFiOrderPriceType.Market, Price = 0m, Volume = 0.01m });
var tradfi_13 = await api.TradFi.UpdateOrderAsync(1_000_000_001, new GateTradFiOrderUpdateRequest { Price = 100.0m, TakeProfitPrice = 110.0m, StopLossPrice = 90.0m });
var tradfi_14 = await api.TradFi.CancelOrderAsync(1_000_000_001);
var tradfi_15 = await api.TradFi.GetOrderHistoryAsync(new GateTradFiOrderHistoryQueryRequest { Symbol = "XAUUSD", BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow });
var tradfi_16 = await api.TradFi.GetPositionsAsync();
var tradfi_17 = await api.TradFi.UpdatePositionAsync(1_000_000_001, new GateTradFiPositionUpdateRequest { TakeProfitPrice = 110.0m, StopLossPrice = 90.0m });
var tradfi_18 = await api.TradFi.ClosePositionAsync(1_000_000_001, new GateTradFiClosePositionRequest { CloseType = 1, CloseVolume = 0.01m });
var tradfi_19 = await api.TradFi.GetPositionHistoryAsync(new GateTradFiPositionHistoryQueryRequest { Symbol = "XAUUSD", BeginTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow });

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
var delivery_16 = await api.Delivery.USDT.PlaceOrderAsync("CONTRACT", 25);
var delivery_17 = await api.Delivery.USDT.PlaceOrderAsync(new GateFuturesOrderRequest { });
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
var earn_01 = await api.Earn.GetDualInvestmentPlansAsync(new GateEarnDualPlanQueryRequest { Coin = "BTC", Type = GateEarnDualOptionType.Put, QuoteCurrency = "USDT", Sort = GateEarnDualPlanSort.Apy });
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
var otc_02 = await api.Otc.CreateFiatOrderAsync(new GateOtcFiatOrderRequest { Type = GateOtcOrderType.Buy, CryptoCurrency = "USDT", FiatCurrency = "USD", CryptoAmount = 30000.0m, FiatAmount = 30000.0m, QuoteToken = "QUOTE-TOKEN", BankId = 1_000_000_001 });
var otc_03 = await api.Otc.CreateStableCoinOrderAsync(new GateOtcStableCoinOrderRequest { PayCoin = "USDC", GetCoin = "USDT", PayAmount = 30000.0m, GetAmount = 20000.0m, Side = GateOtcQuoteSide.Pay, QuoteToken = "QUOTE-TOKEN" });
var otc_04 = await api.Otc.GetDefaultBankAccountAsync();
var otc_05 = await api.Otc.GetBankAccountsAsync();
var otc_06 = await api.Otc.MarkFiatOrderAsPaidAsync(new GateOtcOrderIdRequest { OrderId = 1_000_000_001 });
var otc_07 = await api.Otc.CancelFiatOrderAsync(new GateOtcOrderIdRequest { OrderId = 1_000_000_001 });
var otc_08 = await api.Otc.GetFiatOrdersAsync(new GateOtcFiatOrderListRequest { Type = GateOtcOrderType.Buy, FiatCurrency = "USD", CryptoCurrency = "USDT", StartTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, PageNumber = 1, PageSize = 10 });
var otc_09 = await api.Otc.GetStableCoinOrdersAsync(new GateOtcStableCoinOrderListRequest { CoinName = "USDT", Status = "PROCESSING", StartTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow, PageNumber = 1, PageSize = 10 });
var otc_10 = await api.Otc.GetFiatOrderAsync(new GateOtcOrderIdRequest { OrderId = 1_000_000_001 });

// P2P Methods
var p2p_01 = await api.P2p.GetUserInfoAsync();
var p2p_02 = await api.P2p.GetCounterpartyUserInfoAsync(new GateP2pCounterpartyUserInfoRequest { BusinessUserId = "BIZ-UID" });
var p2p_03 = await api.P2p.GetPaymentMethodsAsync(new GateP2pPaymentMethodsRequest { Fiat = "USD" });
var p2p_04 = await api.P2p.GetPendingTransactionsAsync(new GateP2pPendingTransactionsRequest { CryptoCurrency = "USDT", FiatCurrency = "USD", OrderTab = GateP2pOrderTab.Pending, SelectType = GateP2pOrderSide.Sell, StartTime = DateTime.UtcNow.AddDays(-7), EndTime = DateTime.UtcNow });
var p2p_05 = await api.P2p.GetCompletedTransactionsAsync(new GateP2pCompletedTransactionsRequest { CryptoCurrency = "USDT", FiatCurrency = "USD", SelectType = GateP2pOrderSide.Sell, QueryDispute = true, Page = 1, PerPage = 10 });
var p2p_06 = await api.P2p.GetTransactionDetailsAsync(new GateP2pTransactionDetailsRequest { TransactionId = 40_000_001, Channel = "" });
var p2p_07 = await api.P2p.ConfirmPaymentAsync(new GateP2pConfirmPaymentRequest { TransactionId = 40_000_001, PaymentMethod = "bank" });
var p2p_08 = await api.P2p.ConfirmReceiptAsync(new GateP2pTransactionIdRequest { TransactionId = 40_000_001 });
var p2p_09 = await api.P2p.CancelOrderAsync(new GateP2pCancelOrderRequest { TransactionId = 40_000_001, ReasonId = "1", ReasonMemo = "Canceled after agreement with the counterparty" });
var p2p_10 = await api.P2p.SubmitAdvertisementAsync(new GateP2pAdRequest { CurrencyType = "USDT", ExchangeType = "USD", Type = GateP2pAdOperationType.PublishSell, UnitPrice = 1.1m, Number = 100.0m, PayType = "bank", PayTypeJson = "{\"bank\":\"10001\"}", MinAmount = 10.0m, MaxAmount = 500.0m, RateFixed = 1, ExpireMinutes = 20 });
var p2p_11 = await api.P2p.UpdateAdvertisementStatusAsync(new GateP2pAdStatusUpdateRequest { AdvertisementId = 2_124_000_001, Status = GateP2pAdStatusUpdate.Delisted });
var p2p_12 = await api.P2p.GetAdvertisementAsync(new GateP2pAdvertisementIdRequest { AdvertisementId = 2_124_000_001 });
var p2p_13 = await api.P2p.GetMyAdvertisementsAsync(new GateP2pAdListRequest { Asset = "USDT", FiatUnit = "USD", TradeType = GateP2pOrderSide.Sell });
var p2p_14 = await api.P2p.GetAdvertisementsAsync(new GateP2pAdListRequest { Asset = "USDT", FiatUnit = "USD", TradeType = GateP2pOrderSide.Sell });
var p2p_15 = await api.P2p.GetChatHistoryAsync(new GateP2pChatHistoryRequest { TransactionId = 40_000_001, LastReceived = DateTime.UtcNow.AddMinutes(-10), FirstReceived = DateTime.UtcNow.AddHours(-1) });
var p2p_16 = await api.P2p.SendChatMessageAsync(new GateP2pSendChatMessageRequest { TransactionId = 40_000_001, Type = GateP2pChatMessageType.Text, Message = "Payment completed, please check" });
var p2p_17 = await api.P2p.UploadChatFileAsync(new GateP2pUploadChatFileRequest { ContentType = "image/png", Base64Content = "BASE64-CONTENT" });

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
var crossex_01 = await api.CrossEx.GetSymbolsAsync(new GateCrossExSymbolsQueryRequest { Symbols = new[] { "BINANCE_FUTURE_BTC_USDT" } });
var crossex_02 = await api.CrossEx.GetRiskLimitsAsync(new GateCrossExRiskLimitQueryRequest { Symbols = new[] { "BINANCE_FUTURE_BTC_USDT" } });
var crossex_03 = await api.CrossEx.GetTransferCoinsAsync(new GateCrossExTransferCoinQueryRequest { Coin = "USDT" });
var crossex_04 = await api.CrossEx.GetTransferHistoryAsync(new GateCrossExTransferHistoryQueryRequest { Coin = "USDT", From = DateTime.UtcNow.AddDays(-7), To = DateTime.UtcNow, Page = 1, Limit = 100 });
var crossex_05 = await api.CrossEx.TransferAsync(new GateCrossExTransferRequest { Coin = "USDT", Amount = 100.0m, From = GateCrossExTransferAccountType.Spot, To = GateCrossExTransferAccountType.CrossExGate, Text = "CLIENT-TRANSFER-ID" });
var crossex_06 = await api.CrossEx.PlaceOrderAsync(new GateCrossExOrderRequest { Symbol = "BINANCE_SPOT_BTC_USDT", Side = GateCrossExOrderSide.Buy, Type = GateCrossExOrderType.Limit, TimeInForce = GateCrossExTimeInForce.GoodTillCancelled, Quantity = 0.001m, Price = 60000.0m, Text = "CLIENT-ORDER-ID" });
var crossex_07 = await api.CrossEx.GetOrderAsync("ORDER-ID");
var crossex_08 = await api.CrossEx.UpdateOrderAsync("ORDER-ID", new GateCrossExOrderUpdateRequest { Quantity = 0.001m, Price = 61000.0m });
var crossex_09 = await api.CrossEx.CancelOrderAsync("ORDER-ID");
var crossex_10 = await api.CrossEx.GetConvertQuoteAsync(new GateCrossExConvertQuoteRequest { ExchangeType = GateCrossExExchangeType.Gate, FromCoin = "USDT", ToCoin = "BTC", FromAmount = 100.0m });
var crossex_11 = await api.CrossEx.CreateConvertOrderAsync(new GateCrossExConvertOrderRequest { QuoteId = "QUOTE-ID" });
var crossex_12 = await api.CrossEx.GetAccountAsync(new GateCrossExAccountQueryRequest { ExchangeType = GateCrossExExchangeType.Gate });
var crossex_13 = await api.CrossEx.UpdateAccountAsync(new GateCrossExAccountUpdateRequest { PositionMode = GateCrossExPositionMode.Single, AccountMode = GateCrossExAccountMode.CrossExchange, ExchangeType = GateCrossExExchangeType.Gate });
var crossex_14 = await api.CrossEx.GetContractLeveragesAsync(new GateCrossExLeverageQueryRequest { Symbols = new[] { "BINANCE_FUTURE_BTC_USDT" } });
var crossex_15 = await api.CrossEx.UpdateContractLeverageAsync(new GateCrossExLeverageRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", Leverage = 5.0m });
var crossex_16 = await api.CrossEx.GetMarginLeveragesAsync(new GateCrossExLeverageQueryRequest { Symbols = new[] { "GATE_MARGIN_BTC_USDT" } });
var crossex_17 = await api.CrossEx.UpdateMarginLeverageAsync(new GateCrossExLeverageRequest { Symbol = "GATE_MARGIN_BTC_USDT", Leverage = 3.0m });
var crossex_18 = await api.CrossEx.ClosePositionAsync(new GateCrossExClosePositionRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", PositionSide = GateCrossExPositionSide.Long });
var crossex_19 = await api.CrossEx.GetInterestRatesAsync(new GateCrossExCoinExchangeQueryRequest { Coin = "USDT", ExchangeType = GateCrossExExchangeType.Gate });
var crossex_20 = await api.CrossEx.GetFeesAsync();
var crossex_21 = await api.CrossEx.GetPositionsAsync(new GateCrossExPositionQueryRequest { Symbol = "BINANCE_FUTURE_BTC_USDT", ExchangeType = GateCrossExExchangeType.Binance });
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
```

## WebSocket Api Examples

The Gate.IO.Api socket client provides several socket endpoint to which can be subscribed.

```csharp
var ws = new GateWebSocketClient();
ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

// TODO: Readme
```
