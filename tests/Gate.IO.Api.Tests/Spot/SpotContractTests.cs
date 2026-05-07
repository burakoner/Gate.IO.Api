using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Spot;

[Trait("Category", "Contract")]
public class SpotContractTests
{
    [Fact]
    public void Documented_spot_currency_and_market_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateSpotCurrency>>("Docs/Spot/currencies.success.json");
        var currency = JsonFixture.Deserialize<GateSpotCurrency>("Docs/Spot/currency.success.json");
        var markets = JsonFixture.Deserialize<List<GateSpotMarket>>("Docs/Spot/currency_pairs.success.json");
        var market = JsonFixture.Deserialize<GateSpotMarket>("Docs/Spot/currency_pair.success.json");

        Assert.Single(currencies);
        Assert.Equal("GT", currency.Symbol);
        Assert.Equal(2_100_000m, currency.TotalSupply);
        Assert.Equal("ETH_USDT", markets[0].Symbol);
        Assert.Equal(GateSpotMarketStatus.Tradable, market.Status);
        Assert.Equal(GateSpotMarketType.Normal, market.Type);
        Assert.Equal(100000m, market.MarketOrderMaxStock);
    }

    [Fact]
    public void Documented_spot_public_market_data_responses_deserialize()
    {
        var tickers = JsonFixture.Deserialize<List<GateSpotTicker>>("Docs/Spot/tickers.success.json");
        var orderBook = JsonFixture.Deserialize<GateSpotOrderBook>("Docs/Spot/order_book.success.json");
        var trades = JsonFixture.Deserialize<List<GateSpotTrade>>("Docs/Spot/trades.success.json");
        var candles = JsonFixture.Deserialize<List<GateSpotCandlestick>>("Docs/Spot/candlesticks.success.json");
        var insurance = JsonFixture.Deserialize<List<GateSpotInsurance>>("Docs/Spot/insurance_history.success.json");

        Assert.Single(tickers);
        Assert.Equal("BTC3L_USDT", tickers[0].Symbol);
        Assert.Equal(2.46140352m, tickers[0].Last);
        Assert.Equal(123456, orderBook.Id);
        Assert.Equal(1.52m, orderBook.Asks[0].Price);
        Assert.Single(trades);
        Assert.Equal(GateSpotOrderSide.Sell, trades[0].Side);
        Assert.Equal(1548000000123, trades[0].CreateTimeInMilliseconds);
        Assert.Single(candles);
        Assert.Equal(4.214m, candles[0].Volume);
        Assert.True(candles[0].WindowClosed);
        Assert.Single(insurance);
        Assert.Equal(1021.21m, insurance[0].Balance);
    }

    [Fact]
    public void Documented_spot_account_and_order_responses_deserialize()
    {
        var balances = JsonFixture.Deserialize<List<GateSpotBalance>>("Docs/Spot/accounts.success.json");
        var accountBook = JsonFixture.Deserialize<List<GateSpotTransaction>>("Docs/Spot/account_book.success.json");
        var order = JsonFixture.Deserialize<GateSpotOrder>("Docs/Spot/order.success.json");
        var batchOrders = JsonFixture.Deserialize<List<GateSpotBatchOrder>>("Docs/Spot/batch_orders.success.json");
        var openOrders = JsonFixture.Deserialize<List<GateSpotOpenOrders>>("Docs/Spot/open_orders.success.json");
        var cancelOrders = JsonFixture.Deserialize<List<GateSpotCancelOrder>>("Docs/Spot/cancel_orders.success.json");

        Assert.Single(balances);
        Assert.Equal(968.8m, balances[0].Available);
        Assert.Single(accountBook);
        Assert.Equal(123456, accountBook[0].Id);
        Assert.Equal(1.03m, accountBook[0].Change);
        Assert.Equal(GateSpotOrderStatus.Closed, order.Status);
        Assert.Equal(GateSpotOrderType.Limit, order.Type);
        Assert.Equal(GateSpotAccountType.Unified, order.Account);
        Assert.Equal(GateSpotOrderSide.Buy, order.Side);
        Assert.Equal(GateSpotTimeInForce.GoodTillCancelled, order.TimeInForce);
        Assert.Equal(GateSpotSelfTradeAction.None, order.SelfTradingPreventionAction);
        Assert.Equal(GateSpotFinishAs.Filled, order.FinishAs);
        Assert.Single(batchOrders);
        Assert.True(batchOrders[0].Succeeded);
        Assert.Single(openOrders);
        Assert.Equal(GateSpotOrderStatus.Open, openOrders[0].Orders[0].Status);
        Assert.Single(cancelOrders);
        Assert.Equal(GateSpotAccountType.Spot, cancelOrders[0].Account);
    }

    [Fact]
    public void Documented_spot_trade_fee_and_price_order_responses_deserialize()
    {
        var privateTrades = JsonFixture.Deserialize<List<GateSpotPrivateTrade>>("Docs/Spot/private_trades.success.json");
        var tradeHistory = JsonFixture.Deserialize<List<GateSpotTradeHistory>>("Docs/Spot/trade_history.success.json");
        var fees = JsonFixture.Deserialize<Dictionary<string, GateSpotUserTradingFee>>("Docs/Spot/batch_fee.success.json");
        var countdown = JsonFixture.Deserialize<GateSpotCountdown>("Docs/Spot/countdown_cancel_all.success.json");
        var priceOrders = JsonFixture.Deserialize<List<GateSpotPriceTriggeredOrder>>("Docs/Spot/price_orders.success.json");
        var priceOrderId = JsonFixture.Deserialize<GateSpotPriceTriggeredOrderId>("Docs/Spot/price_order_id.success.json");

        Assert.Single(privateTrades);
        Assert.Equal(GateSpotTraderRole.Maker, privateTrades[0].Role);
        Assert.Single(tradeHistory);
        Assert.Equal(GateSpotOrderSide.Sell, tradeHistory[0].Side);
        Assert.Equal(1548000000123, tradeHistory[0].CreateTimeInMilliseconds);
        Assert.Equal(0.001m, fees["BTC_USDT"].MakerFee);
        Assert.NotEqual(default, countdown.Time);
        Assert.Single(priceOrders);
        Assert.Equal(GateSpotTriggerStatus.Open, priceOrders[0].Status);
        Assert.Equal(GateSpotTriggerCondition.GreaterThanOrEqualTo, priceOrders[0].Trigger.Rule);
        Assert.Equal(GateSpotPriceTriggeredOrderAccountType.Normal, priceOrders[0].Order.Account);
        Assert.Equal(1432329, priceOrderId.OrderId);
    }

    [Fact]
    public void Captured_live_public_spot_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateSpotCurrency>>("Live/Spot/currencies.json");
        var currency = JsonFixture.Deserialize<GateSpotCurrency>("Live/Spot/currencies.GT.json");
        var markets = JsonFixture.Deserialize<List<GateSpotMarket>>("Live/Spot/currency_pairs.json");
        var market = JsonFixture.Deserialize<GateSpotMarket>("Live/Spot/currency_pairs.BTC_USDT.json");
        var tickers = JsonFixture.Deserialize<List<GateSpotTicker>>("Live/Spot/tickers.BTC_USDT.json");
        var orderBook = JsonFixture.Deserialize<GateSpotOrderBook>("Live/Spot/order_book.BTC_USDT.limit5.json");
        var trades = JsonFixture.Deserialize<List<GateSpotTrade>>("Live/Spot/trades.BTC_USDT.limit1.json");
        var candles = JsonFixture.Deserialize<List<GateSpotCandlestick>>("Live/Spot/candlesticks.BTC_USDT.1m.limit1.json");
        var serverTime = JsonFixture.Deserialize<GateSpotTime>("Live/Spot/time.json");
        var insurance = JsonFixture.Deserialize<List<GateSpotInsurance>>("Live/Spot/insurance_history.BTC.margin.json");

        Assert.NotEmpty(currencies);
        Assert.Equal("GT", currency.Symbol);
        Assert.NotEmpty(markets);
        Assert.Equal("BTC_USDT", market.Symbol);
        Assert.Equal(GateSpotMarketStatus.Tradable, market.Status);
        Assert.NotEmpty(tickers);
        Assert.NotEmpty(orderBook.Asks);
        Assert.NotEmpty(orderBook.Bids);
        Assert.NotEmpty(trades);
        Assert.NotEmpty(candles);
        Assert.NotEqual(default, serverTime.Time);
        Assert.NotNull(insurance);
    }
}
