using Gate.IO.Api.Futures;
using Gate.IO.Api.Margin;
using Gate.IO.Api.Options;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Swap;
using Gate.IO.Api.Wallet;

namespace Gate.IO.Api.Examples;

internal class Program
{
    static async Task Main(string[] args)
    {
        var api = new GateRestApiClient();
        api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

        // Wallet Methods
        var wallet_01 = await api.Wallet.WithdrawAsync("CURRENCY", 1.0m, "CHAIN", "ADDRESS", "MEMO", "CLIENT-ORDER-ID");
        var wallet_02 = await api.Wallet.PushAsync(1_000_000_000, "CURRENCY", 1.0m);
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
        var wallet_18 = await api.Wallet.GetTotalBalanceAsync();

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

        // Flash-Swap Methods
        var swap_02 = await api.Swap.GetMarketsAsync();
        var swap_03 = await api.Swap.PreviewOrderAsync("SELL-CURRENCY", 100.0M, "BUY-CURRENCY", 1000.0M);
        var swap_04 = await api.Swap.PreviewOrderAsync(new GateSwapOrderRequest
        {
            SellCurrency = "SELL-CURRENCY",
            SellAmount = 100.0m,
            BuyCurrency = "BUY-CURRENCY",
            BuyAmount = 1000.0m,
        });
        var swap_05 = await api.Swap.PlaceOrderAsync(swap_04.Data.PreviewId, "SELL-CURRENCY", 100.0m, "BUY-CURRENCY", 1000.0m);
        var swap_06 = await api.Swap.PlaceOrderAsync(new GateSwapOrderRequest
        {
            BuyCurrency = "SELL-CURRENCY",
            BuyAmount = 1000.0m,
            SellCurrency = "BUY-CURRENCY",
            SellAmount = 100.0m,
            PreviewId = swap_04.Data.PreviewId
        });
        var swap_07 = await api.Swap.GetOrdersAsync();
        var swap_08 = await api.Swap.GetOrderAsync(1_000_000_000);

        // Access for Futures (Perpetual & Delivery) Methods
        var sample_01 = await api.Futures.Perpetual.BTC.GetContractsAsync();
        var sample_02 = await api.Futures.Perpetual.USD.GetContractsAsync();
        var sample_03 = await api.Futures.Perpetual.USDT.GetContractsAsync();
        var sample_04 = await api.Futures.Delivery.USDT.GetContractsAsync();

        // Dictionary Access for Futures (Perpetual & Delivery) Methods
        var sample_11 = await api.Futures.Perpetual[GateFuturesSettlement.BTC].GetContractsAsync();
        var sample_12 = await api.Futures.Perpetual[GateFuturesSettlement.USD].GetContractsAsync();
        var sample_13 = await api.Futures.Perpetual[GateFuturesSettlement.USDT].GetContractsAsync();
        var sample_14 = await api.Futures.Delivery[GateDeliverySettlement.USDT].GetContractsAsync();

        // Perpetual Futures Methods
        var settle = GateFuturesSettlement.USDT;
        var perpetual_01 = await api.Futures.Perpetual[settle].GetContractsAsync();
        var perpetual_02 = await api.Futures.Perpetual[settle].GetContractAsync("CONTRACT");
        var perpetual_03 = await api.Futures.Perpetual[settle].GetOrderBookAsync("CONTRACT");
        var perpetual_04 = await api.Futures.Perpetual[settle].GetTradesAsync("CONTRACT");
        var perpetual_05 = await api.Futures.Perpetual[settle].GetMarkPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
        var perpetual_06 = await api.Futures.Perpetual[settle].GetIndexPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
        var perpetual_07 = await api.Futures.Perpetual[settle].GetPremiumIndexCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
        var perpetual_08 = await api.Futures.Perpetual[settle].GetTickersAsync();
        var perpetual_09 = await api.Futures.Perpetual[settle].GetFundingRateHistoryAsync("CONTRACT");
        var perpetual_10 = await api.Futures.Perpetual[settle].GetInsuranceHistoryAsync();
        var perpetual_11 = await api.Futures.Perpetual[settle].GetStatsAsync("CONTRACT");
        var perpetual_12 = await api.Futures.Perpetual[settle].GetIndexConstituentsAsync("INDEX");
        var perpetual_13 = await api.Futures.Perpetual[settle].GetLiquidationsAsync("CONTRACT");
        var perpetual_14 = await api.Futures.Perpetual[settle].GetBalancesAsync();
        var perpetual_15 = await api.Futures.Perpetual[settle].GetBalanceHistoryAsync();
        var perpetual_16 = await api.Futures.Perpetual[settle].GetPositionsAsync();
        var perpetual_17 = await api.Futures.Perpetual[settle].GetPositionAsync("CONTRACT");
        var perpetual_18 = await api.Futures.Perpetual[settle].SetPositionMarginAsync("CONTRACT", 100.0M);
        var perpetual_19 = await api.Futures.Perpetual[settle].SetLeverageAsync("CONTRACT", 10);
        var perpetual_20 = await api.Futures.Perpetual[settle].SetRiskLimitAsync("CONTRACT", 25);
        var perpetual_21 = await api.Futures.Perpetual[settle].SetDualModeAsync(true);
        var perpetual_22 = await api.Futures.Perpetual[settle].GetDualModePositionsAsync("CONTRACT");
        var perpetual_23 = await api.Futures.Perpetual[settle].SetDualModeMarginAsync("CONTRACT", GateFuturesDualModeSide.DualLong, 100);
        var perpetual_24 = await api.Futures.Perpetual[settle].SetDualModeLeverageAsync("CONTRACT", 10);
        var perpetual_25 = await api.Futures.Perpetual[settle].SetDualModeRiskLimitAsync("CONTRACT", 25);
        var perpetual_26 = await api.Futures.Perpetual[settle].PlaceOrderAsync("CONTRACT", 25);
        var perpetual_27 = await api.Futures.Perpetual[settle].PlaceOrderAsync(new GateFuturesOrderRequest { });
        var perpetual_28 = await api.Futures.Perpetual[settle].GetOrderAsync();
        var perpetual_29 = await api.Futures.Perpetual[settle].CancelOrderAsync();
        var perpetual_30 = await api.Futures.Perpetual[settle].AmendOrderAsync();
        var perpetual_31 = await api.Futures.Perpetual[settle].GetUserTradesAsync("CONTRACT", orderId: 1_000_000_001);
        var perpetual_32 = await api.Futures.Perpetual[settle].GetUserTradesAsync("CONTRACT", role: GateFuturesTradeRole.Maker);
        var perpetual_33 = await api.Futures.Perpetual[settle].GetPositionClosesAsync();
        var perpetual_34 = await api.Futures.Perpetual[settle].GetPositionClosesAsync();
        var perpetual_35 = await api.Futures.Perpetual[settle].GetUserLiquidationsAsync();
        var perpetual_36 = await api.Futures.Perpetual[settle].CancelAllAsync(30);
        var perpetual_37 = await api.Futures.Perpetual[settle].PlacePriceTriggeredOrderAsync(
            GateFuturesTriggerType.CloseShortPosition,
            GateFuturesTriggerPrice.MarkPrice,
            GateFuturesTriggerStrategy.ByPrice,
            GateSpotTriggerCondition.GreaterThanOrEqualTo,
            100.01m, TimeSpan.FromMinutes(15), "CONTRACT", 100.00m, 25, true,
            GateFuturesTimeInForce.GoodTillCancelled,
            "CLIENT-ORDER-ID", false, GateFuturesOrderAutoSize.CloseLong
        );
        var perpetual_38 = await api.Futures.Perpetual[settle].PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest { });
        var perpetual_39 = await api.Futures.Perpetual[settle].GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open);
        var perpetual_40 = await api.Futures.Perpetual[settle].CancelPriceTriggeredOrdersAsync();
        var perpetual_41 = await api.Futures.Perpetual[settle].GetPriceTriggeredOrderAsync(1_000_000_001);
        var perpetual_42 = await api.Futures.Perpetual[settle].CancelPriceTriggeredOrderAsync(1_000_000_001);

        // Delivery Futures Methods
        var delivery_01 = await api.Futures.Delivery.USDT.GetContractsAsync();
        var delivery_02 = await api.Futures.Delivery.USDT.GetContractAsync("CONTRACT");
        var delivery_03 = await api.Futures.Delivery.USDT.GetOrderBookAsync("CONTRACT");
        var delivery_04 = await api.Futures.Delivery.USDT.GetTradesAsync("CONTRACT");
        var delivery_05 = await api.Futures.Delivery.USDT.GetMarkPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
        var delivery_06 = await api.Futures.Delivery.USDT.GetIndexPriceCandlesticksAsync("CONTRACT", GateFuturesCandlestickInterval.OneDay);
        var delivery_07 = await api.Futures.Delivery.USDT.GetTickersAsync();
        var delivery_08 = await api.Futures.Delivery.USDT.GetInsuranceHistoryAsync();
        var delivery_09 = await api.Futures.Delivery.USDT.GetBalancesAsync();
        var delivery_10 = await api.Futures.Delivery.USDT.GetBalanceHistoryAsync(GateFuturesBalanceChangeType.Funding);
        var delivery_11 = await api.Futures.Delivery.USDT.GetPositionsAsync();
        var delivery_12 = await api.Futures.Delivery.USDT.GetPositionAsync("CONTRACT");
        var delivery_13 = await api.Futures.Delivery.USDT.SetPositionMarginAsync("CONTRACT", 100.0m);
        var delivery_14 = await api.Futures.Delivery.USDT.SetLeverageAsync("CONTRACT", 10);
        var delivery_15 = await api.Futures.Delivery.USDT.SetRiskLimitAsync("CONTRACT", 25);
        var delivery_16 = await api.Futures.Delivery.USDT.PlaceOrderAsync("CONTRACT", 25);
        var delivery_17 = await api.Futures.Delivery.USDT.PlaceOrderAsync(new GateFuturesOrderRequest { });
        var delivery_18 = await api.Futures.Delivery.USDT.GetOrdersAsync("CONTRACT", GateFuturesOrderStatus.Open);
        var delivery_19 = await api.Futures.Delivery.USDT.CancelOrdersAsync("CONTRACT", GateFuturesOrderSide.Bid);
        var delivery_20 = await api.Futures.Delivery.USDT.GetOrderAsync();
        var delivery_21 = await api.Futures.Delivery.USDT.CancelOrderAsync();
        var delivery_22 = await api.Futures.Delivery.USDT.GetUserTradesAsync();
        var delivery_23 = await api.Futures.Delivery.USDT.GetPositionClosesAsync();
        var delivery_24 = await api.Futures.Delivery.USDT.GetUserLiquidationsAsync();
        var delivery_25 = await api.Futures.Delivery.USDT.GetUserSettlementsAsync();
        var delivery_26 = await api.Futures.Delivery.USDT.PlacePriceTriggeredOrderAsync(
            GateFuturesTriggerType.CloseShortPosition,
            GateFuturesTriggerPrice.MarkPrice,
            GateFuturesTriggerStrategy.ByPrice,
            GateSpotTriggerCondition.GreaterThanOrEqualTo,
            100.01m, TimeSpan.FromMinutes(15), "CONTRACT", 100.00m, 25, true,
            GateFuturesTimeInForce.GoodTillCancelled,
            "CLIENT-ORDER-ID", false, GateFuturesOrderAutoSize.CloseLong
        );
        var delivery_27 = await api.Futures.Delivery.USDT.PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest { });
        var delivery_28 = await api.Futures.Delivery.USDT.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open);
        var delivery_29 = await api.Futures.Delivery.USDT.CancelPriceTriggeredOrdersAsync("CONTRACT");
        var delivery_30 = await api.Futures.Delivery.USDT.GetPriceTriggeredOrderAsync(1_000_000_001);
        var delivery_31 = await api.Futures.Delivery.USDT.CancelPriceTriggeredOrderAsync(1_000_000_001);

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

        // Rebate Methods
        var rebate_01 = await api.Rebate.GetTransactionHistoryAsync();
        var rebate_02 = await api.Rebate.GetCommissionHistoryAsync();
    }
}