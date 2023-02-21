using Gate.IO.Api;
using Gate.IO.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateApiDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var ws = new GateStreamClient(new GateStreamClientOptions
            {
                RawResponse = true,
                ApiCredentials = new ApiSharp.Authentication.ApiCredentials("b491b7c59b9a466ca09c5e0573d81fcc", "62f95d662a3a40df1f3c821d988ee9f865cc5835dfd2b1cf2e77057c5c9d53f3"),
            });
            /*
            var ticker = await ws.Spot.SubscribeToTickersAsync(new List<string>
            {
                "BTC_USDT", "ETH_USDT", "XRP_USDT", "GT_USDT", "KEY_USDT", "FIL_USDT", "SOL_USDT", "MAGIC_USDT",
            }, (data) =>
            {
                Console.WriteLine($"S:{data.Data.Symbol} L:{data.Data.Last} H:{data.Data.High24h} L:{data.Data.Low24h} LA:{data.Data.LowestAsk} HB:{data.Data.HighestBid}");
            });
            Console.WriteLine("Subscribed");
            Console.ReadLine();
            await ws.Spot.UnsubscribeAsync(ticker.Data);
            */

            /*
            var ws_trades = await ws.Spot.SubscribeToTradesAsync(new List<string>
            {
                "BTC_USDT", "ETH_USDT", "XRP_USDT", "GT_USDT", "KEY_USDT", "FIL_USDT", "SOL_USDT", "MAGIC_USDT",
            }, (data) =>
            {
                Console.WriteLine($"S:{data.Data.Symbol} S:{data.Data.Side} P:{data.Data.Price} A:{data.Data.Amount}");
            });
            Console.WriteLine("Subscribed");
            Console.ReadLine();
            await ws.Spot.UnsubscribeAsync(ws_trades.Data);
            */

            /*
            var ws_klines = await ws.Spot.SubscribeToCandlesticksAsync("BTC_USDT", SpotCandlestickInterval.OneMinute, (data) =>
            {
                Console.WriteLine($"S:{data.Data.Subscription} O:{data.Data.Open} H:{data.Data.High} L:{data.Data.Low} C:{data.Data.Close} BaseVolume:{data.Data.BaseVolume} QuoteVolume:{data.Data.QuoteVolume}");
            });
            Console.WriteLine("Subscribed");
            Console.ReadLine();
            await ws.Spot.UnsubscribeAsync(ws_klines.Data);
            */

            /*
            var ws_bookticker = await ws.Spot.SubscribeToBookTickersAsync(new List<string>
            {
                "BTC_USDT", "ETH_USDT", "XRP_USDT", "GT_USDT", "KEY_USDT", "FIL_USDT", "SOL_USDT", "MAGIC_USDT",
            }, (data) =>
            {
                Console.WriteLine($"S:{data.Data.Symbol} BP:{data.Data.BestBidPrice} BA:{data.Data.BestBidAmount} AP:{data.Data.BestAskPrice} AA:{data.Data.BestAskAmount}");
            });
            Console.WriteLine("Subscribed");
            Console.ReadLine();
            await ws.Spot.UnsubscribeAsync(ws_bookticker.Data);
            */

            /**/
            var ws_bookdiff = await ws.Spot.SubscribeToOrderBookDifferencesAsync("BTC_USDT", 100, (data) =>
            {
                // Console.WriteLine($"S:{data.Raw}");
                Console.WriteLine($"S:{data.Data.Symbol} F:{data.Data.OrderBookFirstUpdateId} L:{data.Data.OrderBookLastUpdateId} Rows:{data.Data.OrderBookLastUpdateId - data.Data.OrderBookFirstUpdateId} A:{data.Data.Asks.Count()} B:{data.Data.Bids.Count()}");
            });
            Console.WriteLine("Subscribed");
            Console.ReadLine();
            await ws.Spot.UnsubscribeAsync(ws_bookdiff.Data);
            /**/

            /*
            var ws_booksnapshot = await ws.Spot.SubscribeToOrderBookSnapshotsAsync("BTC_USDT", 100, 20, (data) =>
            {
                // Console.WriteLine($"S:{data.Raw}");
                Console.WriteLine($"S:{data.Data.Symbol} F:{data.Data.Time} L:{data.Data.LastUpdateId} A:{data.Data.Asks.Count()} B:{data.Data.Bids.Count()}");
            });
            Console.WriteLine("Subscribed");
            Console.ReadLine();
            await ws.Spot.UnsubscribeAsync(ws_booksnapshot.Data);
            */
            
            /*
            var ws_userorders = await ws.Spot.SubscribeToUserOrdersAsync(new List<string>
            {
                "BTC_USDT", "ETH_USDT", "XRP_USDT", "GT_USDT", "KEY_USDT", "FIL_USDT", "SOL_USDT", "MAGIC_USDT",
            }, (data) =>
            {
                // Console.WriteLine($"S:{data.Raw}");
                Console.WriteLine($"S:{data.Data.Symbol} F:{data.Data.Status}");
            });
            Console.WriteLine("Subscribed");
            Console.ReadLine();
            await ws.Spot.UnsubscribeAsync(ws_userorders.Data);
            */








            var api = new GateRestApiClient(new GateRestApiClientOptions
            {
                ApiCredentials = new ApiSharp.Authentication.ApiCredentials("b491b7c59b9a466ca09c5e0573d81fcc", "62f95d662a3a40df1f3c821d988ee9f865cc5835dfd2b1cf2e77057c5c9d53f3"),
                RawResponse = true,
            });
            //var spot_01 = await api.Spot.GetServerTimeAsync();
            //var spot_02 = await api.Spot.GetAllCurrenciesAsync();
            //var spot_03 = await api.Spot.GetCurrencyAsync("BTC");
            //var spot_04 = await api.Spot.GetAllPairsAsync();
            //var spot_05 = await api.Spot.GetPairAsync("BTC_USDT");
            //var spot_06 = await api.Spot.GetTickersAsync();
            //var spot_07 = await api.Spot.GetTickersAsync("BTC_USDT");
            //var spot_08 = await api.Spot.GetOrderBookAsync("BTC_USDT");
            //var spot_09 = await api.Spot.GetOrderBookAsync("BTC_USDT", 10);
            //var spot_10 = await api.Spot.GetOrderBookAsync("BTC_USDT", limit: 50);
            //var spot_11 = await api.Spot.GetOrderBookAsync("BTC_USDT", withId: true);
            //var spot_12 = await api.Spot.GetMarketTradesAsync("BTC_USDT");
            //var spot_13 = await api.Spot.GetCandlesticksAsync("BTC_USDT", CandlestickInterval.OneHour);
            //var spot_14 = await api.Spot.GetUserFeeRatesAsync();
            //var spot_15 = await api.Spot.GetUserFeeRatesAsync("BTC_USDT");
            //var spot_16 = await api.Spot.GetUserFeeRatesAsync(new List<string> { "BTC_USDT", "GT_USDT" });
            //var spot_17 = await api.Spot.GetSpotBalancesAsync();
            //var spot_18 = await api.Spot.GetSpotBalancesAsync("GT");
            //var spot_19 = await api.Spot.GetSpotBalancesAsync("BTC");
            //var spot_20 = await api.Spot.GetSpotBalancesAsync("USDT");
            //var spot_21 = await api.Spot.PlaceOrderAsync(AccountType.Spot, "BTC_USDT", SpotOrderType.Market, SpotOrderSide.Buy, SpotOrderTimeInForce.FillOrKill, 100.0m);
            //var spot_22 = await api.Spot.PlaceOrderAsync(new OrderRequest
            //{
            //    Symbol = "BTC_USDT",
            //    Account = AccountType.Spot,
            //    Side = SpotOrderSide.Buy,
            //    Type = SpotOrderType.Market,
            //    TimeInForce = SpotOrderTimeInForce.FillOrKill,
            //    Amount = 100.0m,
            //});
            //var spot_23 = await api.Spot.PlaceBatchOrdersAsync(new List<OrderRequest>
            //{
            //    new OrderRequest {
            //        Symbol = "BTC_USDT",
            //        Account = AccountType.Spot,
            //        Side = SpotOrderSide.Buy,
            //        Type = SpotOrderType.Market,
            //        TimeInForce = SpotOrderTimeInForce.FillOrKill,
            //        Amount = 100.0m,
            //        ClientOrderId = string.Concat("t-", Guid.NewGuid().ToString().AsSpan(0,28)),
            //    },
            //    new OrderRequest {
            //        Symbol = "BTC_USDT",
            //        Account = AccountType.Spot,
            //        Side = SpotOrderSide.Buy,
            //        Type = SpotOrderType.Market,
            //        TimeInForce = SpotOrderTimeInForce.FillOrKill,
            //        Amount = 100.0m,
            //        ClientOrderId = string.Concat("t-", Guid.NewGuid().ToString().AsSpan(0,28)),
            //    },
            //    new OrderRequest {
            //        Symbol = "BTC_USDT",
            //        Account = AccountType.Spot,
            //        Side = SpotOrderSide.Buy,
            //        Type = SpotOrderType.Market,
            //        TimeInForce = SpotOrderTimeInForce.FillOrKill,
            //        Amount = 100.0m,
            //        ClientOrderId = string.Concat("t-", Guid.NewGuid().ToString().AsSpan(0,28)),
            //    },
            //});
            //var spot_24 = await api.Spot.GetOpenOrdersAsync(AccountType.Spot);
            //var spot_25 = await api.Spot.CloseLiquidatedPositionsAsync(...);
            //var spot_26 = await api.Spot.GetOrdersAsync(...);
            //var spot_27 = await api.Spot.GetOrdersAsync(...);
            //var spot_28 = await api.Spot.CancelOrderAsync(...);
            //var spot_29 = await api.Spot.CancelOrdersAsync(...);
            //var spot_30 = await api.Spot.GetOrderAsync(...);
            //var spot_31 = await api.Spot.AmendOrderAsync(AccountType.Spot, "BTC_USDT", 156456, "", 15.0M);
            //var spot_32 = await api.Spot.GetUserTradesAsync();
            //var spot_33 = await api.Spot.CountdownCancelOrdersAsync(30);
            //var spot_34 = await api.Spot.PlacePriceTriggeredOrderAsync(AccountType2.Spot, "BTC_USDT", 25000m, PriceTriggerCondition.GreaterThanOrEqualTo, TimeSpan.FromHours(2), SpotOrderType.Limit, SpotOrderSide.Sell, SpotPriceOrderTimeInForce.GoodTillCancelled, 1.00m, 26000.0m);
            //var spot_35 = await api.Spot.PlacePriceTriggeredOrderAsync(new PriceTriggeredOrderRequest
            //{
            //    Symbol = "BTC_USDT",
            //    Trigger = new PriceTrigger
            //    {
            //        Price = "25000",
            //        Rule = PriceTriggerCondition.GreaterThanOrEqualTo,
            //        Expiration = Convert.ToInt32(TimeSpan.FromHours(2).TotalSeconds)
            //    },
            //    Put = new PricePutOrder
            //    {
            //        Account = AccountType2.Spot,
            //        Type = SpotOrderType.Limit,
            //        Side = SpotOrderSide.Sell,
            //        TimeInForce = SpotPriceOrderTimeInForce.GoodTillCancelled,
            //        Amount = "1.00",
            //        Price = "26000.00"
            //    }
            //});
            //var spot_36 = await api.Spot.CancelPriceTriggeredOrdersAsync(...);
            //var spot_37 = await api.Spot.GetPriceTriggeredOrderAsync(1000001);
            //var spot_38 = await api.Spot.CancelPriceTriggeredOrderAsync(1000001);

            //var wallet_01 = await api.Wallet.WithdrawAsync(...);
            //var wallet_02 = await api.Wallet.CancelWithdrawalAsync(...);
            //var wallet_03 = await api.Wallet.GetCurrencyChainsAsync("BTC");
            //var wallet_04 = await api.Wallet.GetCurrencyChainsAsync("USDT");
            //var wallet_05 = await api.Wallet.GetDepositAddressAsync("BTC");
            //var wallet_06 = await api.Wallet.GetDepositAddressAsync("USDT");
            //var wallet_07 = await api.Wallet.GetWithdrawalsAsync();
            //var wallet_99 = await api.Wallet.GetTotalBalanceAsync();

            //var subaccount_01 = await api.SubAccount.CreateSubAccountAsync("burakoner");


            //var margin_01 = await api.Margin.GetAllPairsAsync();
            //var margin_02 = await api.Margin.GetPairAsync("BTC_USDT");
            //var margin_03 = await api.Margin.GetFundingBookAsync("BTC");
            //var margin_04 = await api.Margin.GetFundingBookAsync("USDT");
            //var margin_05 = await api.Margin.GetMarginBalancesAsync();
            //var margin_06 = await api.Margin.GetMarginBalancesAsync("BTC_USDT");
            //var margin_07 = await api.Margin.GetMarginBalancesHistoryAsync();
            //var margin_08 = await api.Margin.GetFundingBalancesAsync();



            //var cross_01 = await api.CrossMargin.GetCurrenciesAsync();
            //var cross_02 = await api.CrossMargin.GetCurrencyAsync("BTC");
            //var cross_03 = await api.CrossMargin.GetCurrencyAsync("USDT");

            //var swap_xx = await api.FlashSwap.GetAllCurrenciesAsync();

            //var futures_01 = await api.Futures.GetContractsAsync(FuturesSettle.USDT);
            //var futures_02 = await api.Futures.GetContractAsync(FuturesSettle.USDT, "BTC_USDT");
            //var futures_03 = await api.Futures.GetOrderBookAsync(FuturesSettle.USDT, "BTC_USDT");
            //var futures_04 = await api.Futures.GetOrderBookAsync(FuturesSettle.USDT, "BTC_USDT", 0.10m);
            //var futures_05 = await api.Futures.GetOrderBookAsync(FuturesSettle.USDT, "BTC_USDT", limit: 50);
            //var futures_06 = await api.Futures.GetOrderBookAsync(FuturesSettle.USDT, "BTC_USDT", withId: true);
            //var futures_07 = await api.Futures.GetTradesAsync(FuturesSettle.USDT, "BTC_USDT");
            //var futures_08 = await api.Futures.GetCandlesticksAsync(FuturesSettle.USDT, "BTC_USDT", CandlestickInterval.OneHour);
            //var futures_09 = await api.Futures.GetPremiumIndexCandlesticksAsync(FuturesSettle.USDT, "BTC_USDT", CandlestickInterval.OneHour);
            //var futures_10 = await api.Futures.GetTickersAsync(FuturesSettle.USDT);
            //var futures_11 = await api.Futures.GetTickersAsync(FuturesSettle.USDT, "BTC_USDT");
            //var futures_12 = await api.Futures.GetFundingRateHistoryAsync(FuturesSettle.USDT, "BTC_USDT");
            //var futures_13 = await api.Futures.GetInsuranceHistoryAsync(FuturesSettle.USDT);
            //var futures_14 = await api.Futures.GetStatsAsync(FuturesSettle.USDT, "BTC_USDT", FuturesStatsInterval.OneDay);
            //var futures_15 = await api.Futures.GetIndexConstituentsAsync(FuturesSettle.USDT, "BTC_USDT");
            //var futures_16 = await api.Futures.GetLiquidationHistoryAsync(FuturesSettle.USDT, "BTC_USDT");
            //var futures_17 = await api.Futures.GetAccountAsync(FuturesSettle.USDT);

            Console.ReadLine();
            Console.ReadLine();
        }
    }
}