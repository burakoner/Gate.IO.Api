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

## Rest Api Examples

```csharp
var api = new GateRestApiClient();
api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

// Wallet Methods
var wallet_01 = await api.Wallet.WithdrawAsync("CURRENCY", 1.0m, "CHAIN", "ADDRESS", "MEMO", "CLIENT-ORDER-ID");
var wallet_02 = await api.Wallet.TransferAsync(1_000_000_000, "CURRENCY", 1.0m);
var wallet_03 = await api.Wallet.CancelWithdrawalAsync(1_000_000_000);
var wallet_04 = await api.Wallet.GetCurrencyChainsAsync("CURRENCY");
var wallet_05 = await api.Wallet.GetDepositAddressAsync("CURRENCY");
var wallet_06 = await api.Wallet.GetWithdrawalsAsync();
var wallet_07 = await api.Wallet.GetDepositsAsync();
var wallet_08 = await api.Wallet.TransfersBetweenTradingAccountsAsync("CURRENCY", GateWalletAccountType.Spot, GateWalletAccountType.Futures, 100.0m);
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

// SubAccount Methods
var subaccount_01 = await api.SubAccount.CreateSubAccountAsync("LOGIN-NAME", "PASSWORD", "EMAIL", "REMARKS");
var subaccount_02 = await api.SubAccount.GetSubAccountsAsync();
var subaccount_03 = await api.SubAccount.GetSubAccountAsync(1_000_000_000);
var subaccount_04 = await api.SubAccount.CreateApiKeyAsync(1_000_000_000);
var subaccount_05 = await api.SubAccount.GetApiKeysAsync(1_000_000_000);
var subaccount_06 = await api.SubAccount.UpdateApiKeyAsync(1_000_000_000, "API-KEY");
var subaccount_07 = await api.SubAccount.DeleteApiKeyAsync(1_000_000_000, "API-KEY");
var subaccount_08 = await api.SubAccount.GetApiKeyAsync(1_000_000_000, "API-KEY");
var subaccount_09 = await api.SubAccount.LockSubAccountAsync(1_000_000_000);
var subaccount_10 = await api.SubAccount.UnlockSubAccountAsync(1_000_000_000);

// Unified Methods
var unified_01 = await api.Unified.GetAccountInfoAsync();
var unified_02 = await api.Unified.GetBorrowableAsync("CURRENCY");
var unified_03 = await api.Unified.GetTransferableAsync("CURRENCY");
var unified_04 = await api.Unified.BorrowAsync("CURRENCY", 100.0m);
var unified_05 = await api.Unified.RepayAsync("CURRENCY", 100.0m);
var unified_06 = await api.Unified.GetLoansAsync();
var unified_07 = await api.Unified.GetLoanHistoryAsync();
var unified_08 = await api.Unified.GetInterestHistoryAsync();
var unified_09 = await api.Unified.GetRiskUnitsAsync();
var unified_10 = await api.Unified.SetAccountModeAsync(GateUnifiedAccountMode.Portfolio);
var unified_11 = await api.Unified.GetAccountModeAsync();
var unified_12 = await api.Unified.GetEstimatedLendingRatesAsync([]);
var unified_13 = await api.Unified.GetLeverageConfigsAsync("CURRENCY");
var unified_14 = await api.Unified.GetLeverageSettingsAsync();
var unified_15 = await api.Unified.SetLeverageSettingsAsync("CURRENCY", 10);

// Spot Methods
var spot_01 = await api.Spot.GetCurrenciesAsync();
var spot_02 = await api.Spot.GetCurrencyAsync("CURRENCY");
var spot_03 = await api.Spot.GetMarketsAsync();
var spot_04 = await api.Spot.GetMarketAsync("SYMBOL");
var spot_05 = await api.Spot.GetTickersAsync();
var spot_06 = await api.Spot.GetOrderBookAsync("SYMBOL");
var spot_07 = await api.Spot.GetTradesAsync("SYMBOL");
var spot_08 = await api.Spot.GetPrivateTradesAsync("SYMBOL"); // Private (Signed)
var spot_09 = await api.Spot.GetCandlesticksAsync("SYMBOL", GateSpotCandlestickInterval.FourHours);
var spot_10 = await api.Spot.GetUserFeeRatesAsync(["SYMBOL"]);
var spot_11 = await api.Spot.GetBalancesAsync();
var spot_12 = await api.Spot.PlaceOrdersAsync(new List<GateSpotOrderRequest>
{
    new GateSpotOrderRequest
    {
        Symbol = "SYMBOL",
        Side = GateSpotOrderSide.Buy,
        Type = GateSpotOrderType.Limit,
        Amount = 1.0m,
        Price = 1.0m
    }
}
);
var spot_13 = await api.Spot.GetOpenOrdersAsync();
var spot_14 = await api.Spot.CloseLiquidatedPositionsAsync(new GateSpotCloseRequest
{
    Symbol = "SYMBOL",
    Price = 1001.01m,
    ProcessingMode = GateSpotActionMode.Full,
});
var spot_15 = await api.Spot.PlaceOrderAsync("SYMBOL", GateSpotAccountType.Spot, GateSpotOrderType.Market, GateSpotOrderSide.Buy, GateSpotTimeInForce.GoodTillCancelled, 100.01m);
var spot_16 = await api.Spot.PlaceOrderAsync(new GateSpotOrderRequest
{
    Symbol = "SYMBOL",
    Account = GateSpotAccountType.Spot,
    Type = GateSpotOrderType.Market,
    Side = GateSpotOrderSide.Buy,
    TimeInForce = GateSpotTimeInForce.GoodTillCancelled,
    Amount = 100.01m
});
var spot_17 = await api.Spot.GetOrdersAsync("SYMBOL", GateSpotOrderQueryStatus.Open);
var spot_18 = await api.Spot.CancelOrdersAsync("SYMBOL");
var spot_19 = await api.Spot.GetOrderAsync("SYMBOL", 1_000_000_000);
var spot_20 = await api.Spot.CancelOrderAsync("SYMBOL", 1_000_000_000);
var spot_21 = await api.Spot.GetTradeHistoryAsync();
var spot_22 = await api.Spot.GetServerTimeAsync();
var spot_23 = await api.Spot.CancelAllAsync(30);
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
        Account = GateSpotAccountType.Spot,
        Type = GateSpotOrderType.Limit,
        Side = GateSpotOrderSide.Buy,
        TimeInForce = GateSpotTriggerTimeInForce.GoodTillCancelled,
        Price = "100.00",
        Amount = "100.02",
        ClientOrderId = "CLIENT-ORDER-ID"
    }
});
var spot_26 = await api.Spot.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open);
var spot_27 = await api.Spot.CancelPriceTriggeredOrdersAsync();
var spot_28 = await api.Spot.GetPriceTriggeredOrderAsync();
var spot_29 = await api.Spot.CancelPriceTriggeredOrderAsync();

// Margin Methods
var margin_01 = await api.Margin.GetBalancesAsync();
var margin_02 = await api.Margin.GetBalanceHistoryAsync();
var margin_03 = await api.Margin.GetFundingBalancesAsync();
var margin_04 = await api.Margin.SetAutoRepaymentAsync(GateMarginAutoRepaymentStatus.Enabled);
var margin_05 = await api.Margin.GetAutoRepaymentAsync();
var margin_06 = await api.Margin.GetTransferableAmountAsync("CURRENCY");

// Margin Uni Methods
var marginuni_01 = await api.MarginUni.GetMarketsAsync();
var marginuni_02 = await api.MarginUni.GetMarketsAsync("SYMBOL");
var marginuni_03 = await api.MarginUni.GetEstimatedInterestRateAsync(["BTC", "ETH"]);
var marginuni_04 = await api.MarginUni.BorrowAsync("SYMBOL", "CURRENCY", 100.0m);
var marginuni_05 = await api.MarginUni.RepayAsync("SYMBOL", "CURRENCY", 100.0m);
var marginuni_06 = await api.MarginUni.GetLoansAsync("SYMBOL", "CURRENCY");
var marginuni_07 = await api.MarginUni.GetLoanHistoryAsync();
var marginuni_08 = await api.MarginUni.GetInterestHistoryAsync();
var marginuni_09 = await api.MarginUni.GetMaximumBorrowableAsync("SYMBOL", "CURRENCY");

// Flash-Swap Methods
var swap_02 = await api.FlashSwap.GetMarketsAsync();
var swap_03 = await api.FlashSwap.PreviewOrderAsync("SELL-CURRENCY", 100.0M, "BUY-CURRENCY", 1000.0M);
var swap_04 = await api.FlashSwap.PreviewOrderAsync(new GateSwapOrderRequest
{
    SellCurrency = "SELL-CURRENCY",
    SellAmount = 100.0m,
    BuyCurrency = "BUY-CURRENCY",
    BuyAmount = 1000.0m,
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
var swap_07 = await api.FlashSwap.GetOrdersAsync();
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
var perpetual_04 = await api.Futures[settle].GetTradesAsync("CONTRACT");
var perpetual_05 = await api.Futures[settle].GetMarkPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
var perpetual_06 = await api.Futures[settle].GetIndexPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
var perpetual_07 = await api.Futures[settle].GetPremiumIndexCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
var perpetual_08 = await api.Futures[settle].GetTickersAsync();
var perpetual_09 = await api.Futures[settle].GetFundingRateHistoryAsync("CONTRACT");
var perpetual_10 = await api.Futures[settle].GetInsuranceHistoryAsync();
var perpetual_11 = await api.Futures[settle].GetStatsAsync("CONTRACT");
var perpetual_12 = await api.Futures[settle].GetIndexConstituentsAsync("INDEX");
var perpetual_13 = await api.Futures[settle].GetLiquidationsAsync("CONTRACT");
var perpetual_14 = await api.Futures[settle].GetRiskLimitTiersAsync("CONTRACT");
var perpetual_15 = await api.Futures[settle].GetBalancesAsync();
var perpetual_16 = await api.Futures[settle].GetBalanceHistoryAsync();
var perpetual_17 = await api.Futures[settle].GetPositionsAsync();
var perpetual_18 = await api.Futures[settle].GetPositionAsync("CONTRACT");
var perpetual_19 = await api.Futures[settle].SetPositionMarginAsync("CONTRACT", 100.0M);
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
var perpetual_30 = await api.Futures[settle].PlaceOrderAsync(new GateFuturesOrderRequest { });
var perpetual_31 = await api.Futures[settle].GetOrderAsync();
var perpetual_32 = await api.Futures[settle].CancelOrderAsync();
var perpetual_33 = await api.Futures[settle].AmendOrderAsync();
var perpetual_34 = await api.Futures[settle].GetUserTradesAsync("CONTRACT", orderId: 1_000_000_001);
var perpetual_35 = await api.Futures[settle].GetUserTradesAsync("CONTRACT", role: GateFuturesTradeRole.Maker);
var perpetual_36 = await api.Futures[settle].GetUserTradesAsync("CONTRACT", from: DateTime.UtcNow.AddDays(-7), to: DateTime.UtcNow);
var perpetual_37 = await api.Futures[settle].GetPositionClosesAsync();
var perpetual_38 = await api.Futures[settle].GetPositionClosesAsync("CONTRACT", from: DateTime.UtcNow.AddDays(-7), to: DateTime.UtcNow);
var perpetual_39 = await api.Futures[settle].GetUserLiquidationsAsync();
var perpetual_40 = await api.Futures[settle].GetAdlHistoryAsync("CONTRACT");
var perpetual_41 = await api.Futures[settle].GetAdlHistoryAsync("CONTRACT", from: DateTime.UtcNow.AddDays(-7), to: DateTime.UtcNow);
var perpetual_42 = await api.Futures[settle].CancelAllAsync(30);
var perpetual_43 = await api.Futures[settle].GetTradingFeesAsync();
var perpetual_44 = await api.Futures[settle].CancelOrdersAsync([]);
var perpetual_45 = await api.Futures[settle].AmendOrdersAsync([]);
var perpetual_46 = await api.Futures[settle].GetRiskLimitTableAsync("TABLE-ID");
var perpetual_47 = await api.Futures[settle].PlacePriceTriggeredOrderAsync(
    GateFuturesTriggerType.CloseShortPosition,
    GateFuturesTriggerPrice.MarkPrice,
    GateFuturesTriggerStrategy.ByPrice,
    GateSpotTriggerCondition.GreaterThanOrEqualTo,
    100.01m, TimeSpan.FromMinutes(15), "CONTRACT", 100.00m, 25, true,
    GateFuturesTimeInForce.GoodTillCancelled,
    "CLIENT-ORDER-ID", false, GateFuturesOrderAutoSize.CloseLong
);
var perpetual_48 = await api.Futures[settle].PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest { });
var perpetual_49 = await api.Futures[settle].GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open);
var perpetual_50 = await api.Futures[settle].CancelPriceTriggeredOrdersAsync();
var perpetual_51 = await api.Futures[settle].GetPriceTriggeredOrderAsync(1_000_000_001);
var perpetual_52 = await api.Futures[settle].CancelPriceTriggeredOrderAsync(1_000_000_001);

// Delivery Futures Methods
var delivery_01 = await api.Delivery.USDT.GetContractsAsync();
var delivery_02 = await api.Delivery.USDT.GetContractAsync("CONTRACT");
var delivery_03 = await api.Delivery.USDT.GetOrderBookAsync("CONTRACT");
var delivery_04 = await api.Delivery.USDT.GetTradesAsync("CONTRACT");
var delivery_05 = await api.Delivery.USDT.GetMarkPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
var delivery_06 = await api.Delivery.USDT.GetIndexPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
var delivery_07 = await api.Delivery.USDT.GetTickersAsync();
var delivery_08 = await api.Delivery.USDT.GetInsuranceHistoryAsync();
var delivery_09 = await api.Delivery.USDT.GetBalancesAsync();
var delivery_10 = await api.Delivery.USDT.GetBalanceHistoryAsync(GateFuturesBalanceChangeType.Funding);
var delivery_11 = await api.Delivery.USDT.GetPositionsAsync();
var delivery_12 = await api.Delivery.USDT.GetPositionAsync("CONTRACT");
var delivery_13 = await api.Delivery.USDT.SetPositionMarginAsync("CONTRACT", 100.0m);
var delivery_14 = await api.Delivery.USDT.SetLeverageAsync("CONTRACT", 10);
var delivery_15 = await api.Delivery.USDT.SetRiskLimitAsync("CONTRACT", 25);
var delivery_16 = await api.Delivery.USDT.PlaceOrderAsync("CONTRACT", 25);
var delivery_17 = await api.Delivery.USDT.PlaceOrderAsync(new GateFuturesOrderRequest { });
var delivery_18 = await api.Delivery.USDT.GetOrdersAsync("CONTRACT", GateFuturesOrderStatus.Open);
var delivery_19 = await api.Delivery.USDT.CancelOrdersAsync("CONTRACT", GateFuturesOrderSide.Bid);
var delivery_20 = await api.Delivery.USDT.GetOrderAsync();
var delivery_21 = await api.Delivery.USDT.CancelOrderAsync();
var delivery_22 = await api.Delivery.USDT.GetUserTradesAsync();
var delivery_23 = await api.Delivery.USDT.GetPositionClosesAsync();
var delivery_24 = await api.Delivery.USDT.GetUserLiquidationsAsync();
var delivery_25 = await api.Delivery.USDT.GetUserSettlementsAsync();
var delivery_26 = await api.Delivery.USDT.PlacePriceTriggeredOrderAsync(
    GateFuturesTriggerType.CloseShortPosition,
    GateFuturesTriggerPrice.MarkPrice,
    GateFuturesTriggerStrategy.ByPrice,
    GateSpotTriggerCondition.GreaterThanOrEqualTo,
    100.01m, TimeSpan.FromMinutes(15), "CONTRACT", 100.00m, 25, true,
    GateFuturesTimeInForce.GoodTillCancelled,
    "CLIENT-ORDER-ID", false, GateFuturesOrderAutoSize.CloseLong
);
var delivery_27 = await api.Delivery.USDT.PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest { });
var delivery_28 = await api.Delivery.USDT.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open);
var delivery_29 = await api.Delivery.USDT.CancelPriceTriggeredOrdersAsync("CONTRACT");
var delivery_30 = await api.Delivery.USDT.GetPriceTriggeredOrderAsync(1_000_000_001);
var delivery_31 = await api.Delivery.USDT.CancelPriceTriggeredOrderAsync(1_000_000_001);

// Options Methods
var options_01 = await api.Options.GetUnderlyingsAsync();
var options_02 = await api.Options.GetExpirationsAsync("UNDERLYING");
var options_03 = await api.Options.GetContractsAsync("UNDERLYING");
var options_04 = await api.Options.GetContractAsync("CONTRACT");
var options_05 = await api.Options.GetSettlementsAsync("UNDERLYING");
var options_06 = await api.Options.GetSettlementAsync("UNDERLYING", "CONTRACT", 1728321316);
var options_07 = await api.Options.GetUserSettlementsAsync("UNDERLYING");
var options_08 = await api.Options.GetOrderBookAsync("CONTRACT");
var options_09 = await api.Options.GetContractTickersAsync("UNDERLYING");
var options_10 = await api.Options.GetUnderlyingTickersAsync("UNDERLYING");
var options_11 = await api.Options.GetCandlesticksAsync("CONTRACT", GateOptionsCandlestickInterval.OneHour);
var options_12 = await api.Options.GetUnderlyingCandlesticksAsync("UNDERLYIN", GateOptionsCandlestickInterval.OneMinute);
var options_13 = await api.Options.GetTradesAsync("CONTRACT", GateOptionsType.Put);
var options_14 = await api.Options.GetBalanceAsync();
var options_15 = await api.Options.GetBalanceHistoryAsync(GateOptionsBalanceChangeType.Rebate);
var options_16 = await api.Options.GetUnderlyingPositionsAsync("UNDERLYING");
var options_17 = await api.Options.GetContractPositionAsync("CONTRACT");
var options_18 = await api.Options.GetUserLiquidationsAsync("UNDERLYING");
var options_19 = await api.Options.PlaceOrderAsync("CONTRACT", 25);
var options_20 = await api.Options.GetOrdersAsync(GateOptionsOrderStatus.Open);
var options_21 = await api.Options.CancelOrdersAsync();
var options_22 = await api.Options.GetOrderAsync(1_000_000_001);
var options_23 = await api.Options.CancelOrderAsync(1_000_000_001);
var options_24 = await api.Options.GetUserTradesAsync("UNDERLYING");

// Account Methods
var account_01 = await api.Account.GetAccountAsync();
var account_02 = await api.Account.GetRateLimitsAsync();
var account_03 = await api.Account.CreateStpGroupAsync("STP-NAME");
var account_04 = await api.Account.GetStpGroupsAsync("STP-NAME");
var account_05 = await api.Account.GetStpGroupUsersAsync(1_000_000_001);
var account_06 = await api.Account.AddUserToStpGroupAsync(1_000_000_001, []);
var account_07 = await api.Account.RemoveUserToStpGroupAsync(1_000_000_001, 2_000_000_001);
var account_08 = await api.Account.SetGtDeductionAsync(true);
var account_09 = await api.Account.GetGtDeductionAsync();

// Rebate Methods
var rebate_01 = await api.Rebate.GetTransactionHistoryAsync();
var rebate_02 = await api.Rebate.GetCommissionHistoryAsync();
```

## WebSocket Api Examples

The Gate.IO.Api socket client provides several socket endpoint to which can be subscribed.

```csharp
var ws = new GateWebSocketClient();
ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

// TODO: Readme
```
