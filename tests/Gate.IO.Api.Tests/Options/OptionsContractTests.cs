using Gate.IO.Api.Options;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Options;

[Trait("Category", "Contract")]
public class OptionsContractTests
{
    [Fact]
    public void Documented_options_public_market_responses_deserialize()
    {
        var underlyings = JsonFixture.Deserialize<List<GateOptionsUnderlying>>("Docs/Options/underlyings.success.json");
        var expirations = JsonFixture.Deserialize<List<long>>("Docs/Options/expirations.success.json");
        var contracts = JsonFixture.Deserialize<List<GateOptionsContract>>("Docs/Options/contracts.success.json");
        var contract = JsonFixture.Deserialize<GateOptionsContract>("Docs/Options/contract.success.json");
        var settlements = JsonFixture.Deserialize<List<GateOptionsSettlement>>("Docs/Options/settlements.success.json");
        var settlement = JsonFixture.Deserialize<GateOptionsSettlement>("Docs/Options/settlement.success.json");
        var orderBook = JsonFixture.Deserialize<GateOptionsOrderBook>("Docs/Options/order_book.success.json");
        var tickers = JsonFixture.Deserialize<List<GateOptionsContractTicker>>("Docs/Options/tickers.success.json");
        var underlyingTicker = JsonFixture.Deserialize<GateOptionsUnderlyingTicker>("Docs/Options/underlying_ticker.success.json");
        var candlesticks = JsonFixture.Deserialize<List<GateOptionsCandlestick>>("Docs/Options/candlesticks.success.json");
        var underlyingCandlesticks = JsonFixture.Deserialize<List<GateOptionsCandlestickMark>>("Docs/Options/underlying_candlesticks.success.json");
        var trades = JsonFixture.Deserialize<List<GateOptionsTrade>>("Docs/Options/trades.success.json");

        Assert.Single(underlyings);
        Assert.Equal("BTC_USDT", underlyings[0].Underlying);
        Assert.Equal(70000m, underlyings[0].IndexPrice);
        Assert.Equal(1637913600, Assert.Single(expirations));
        Assert.Single(contracts);
        Assert.Equal(GateOptionsContractPeriod.OneWeek, contracts[0].Period);
        Assert.Equal(65000m, contracts[0].StrikePrice);
        Assert.Equal(0.0001m, contract.Multiplier);
        Assert.Single(settlements);
        Assert.Equal(312.35m, settlements[0].Profit);
        Assert.Equal(11687.65m, settlement.SettlePrice);
        Assert.Equal(123456, orderBook.Id);
        Assert.Equal(1.52m, orderBook.Asks[0].Price);
        Assert.Single(tickers);
        Assert.Equal(14010m, tickers[0].MarkPrice);
        Assert.Equal("13", tickers[0].Leverage);
        Assert.Equal(33505, underlyingTicker.TradePut);
        Assert.Equal(76543.3m, underlyingTicker.IndexPrice);
        Assert.Equal(1.032m, candlesticks[0].Close);
        Assert.Equal(100.5m, underlyingCandlesticks[0].QuoteVolume);
        Assert.Equal(121234231, trades[0].Id);
    }

    [Fact]
    public void Documented_options_private_account_position_and_order_responses_deserialize()
    {
        var account = JsonFixture.Deserialize<GateOptionsAccount>("Docs/Options/account.success.json");
        var balance = JsonFixture.Deserialize<GateOptionsBalance>("Docs/Options/account.success.json");
        var accountBook = JsonFixture.Deserialize<List<GateOptionsBalanceChange>>("Docs/Options/account_book.success.json");
        var positions = JsonFixture.Deserialize<List<GateOptionsPosition>>("Docs/Options/positions.success.json");
        var position = JsonFixture.Deserialize<GateOptionsPosition>("Docs/Options/position.success.json");
        var liquidations = JsonFixture.Deserialize<List<GateOptionsUserLiquidation>>("Docs/Options/user_liquidations.success.json");
        var userSettlements = JsonFixture.Deserialize<List<GateOptionsUserSettlement>>("Docs/Options/user_settlements.success.json");
        var orders = JsonFixture.Deserialize<List<GateOptionsOrder>>("Docs/Options/orders.success.json");
        var order = JsonFixture.Deserialize<GateOptionsOrder>("Docs/Options/order.success.json");
        var userTrades = JsonFixture.Deserialize<List<GateOptionsUserTrade>>("Docs/Options/user_trades.success.json");
        var mmps = JsonFixture.Deserialize<List<GateOptionsMMP>>("Docs/Options/mmp_list.success.json");
        var mmp = JsonFixture.Deserialize<GateOptionsMMP>("Docs/Options/mmp.success.json");

        Assert.Equal(666, account.UserId);
        Assert.Equal(GateOptionsMarginMode.ClassicSpotMarginMode, account.MarginMode);
        Assert.Equal(1514.901537m, account.Available);
        Assert.Equal(666, balance.UserId);
        Assert.Equal(GateOptionsBalanceChangeType.Fee, accountBook[0].Type);
        Assert.Single(positions);
        Assert.Equal(232323, positions[0].CloseOrder.OrderId);
        Assert.Equal(-320m, position.UnrealisedPnl);
        Assert.Single(liquidations);
        Assert.Equal(GateOptionsSide.Short, liquidations[0].Side);
        Assert.Equal(-1, userSettlements[0].Size);
        Assert.Single(orders);
        Assert.Equal(GateOptionsOrderStatus.Finished, order.Status);
        Assert.Equal(GateOptionsOrderFinishAs.Filled, order.FinishAs);
        Assert.Equal(GateOptionsTimeInForce.GoodTillCancelled, order.TimeInForce);
        Assert.Equal(GateOptionsTraderRole.Maker, userTrades[0].Role);
        Assert.Equal(5000, mmps[0].Window);
        Assert.Equal(10m, mmp.QuantityLimit);
    }

    [Fact]
    public void Captured_live_public_options_responses_deserialize()
    {
        var underlyings = JsonFixture.Deserialize<List<GateOptionsUnderlying>>("Live/Options/underlyings.json");
        var expirations = JsonFixture.Deserialize<List<long>>("Live/Options/expirations.BTC_USDT.json");
        var contracts = JsonFixture.Deserialize<List<GateOptionsContract>>("Live/Options/contracts.BTC_USDT.first_expiration.json");
        var contract = JsonFixture.Deserialize<GateOptionsContract>("Live/Options/contract.BTC_USDT-20260507-86000-P.json");
        var settlements = JsonFixture.Deserialize<List<GateOptionsSettlement>>("Live/Options/settlements.BTC_USDT.limit1.json");
        var orderBook = JsonFixture.Deserialize<GateOptionsOrderBook>("Live/Options/order_book.BTC_USDT-20260507-86000-P.limit5.json");
        var tickers = JsonFixture.Deserialize<List<GateOptionsContractTicker>>("Live/Options/tickers.BTC_USDT.json");
        var underlyingTicker = JsonFixture.Deserialize<GateOptionsUnderlyingTicker>("Live/Options/underlying_ticker.BTC_USDT.json");
        var candlesticks = JsonFixture.Deserialize<List<GateOptionsCandlestick>>("Live/Options/candlesticks.BTC_USDT-20260507-86000-P.1m.limit1.json");
        var underlyingCandlesticks = JsonFixture.Deserialize<List<GateOptionsCandlestickMark>>("Live/Options/underlying_candlesticks.BTC_USDT.1m.limit1.json");
        var trades = JsonFixture.Deserialize<List<GateOptionsTrade>>("Live/Options/trades.limit1.json");

        Assert.NotEmpty(underlyings);
        Assert.Contains(underlyings, x => x.Underlying == "BTC_USDT");
        Assert.NotEmpty(expirations);
        Assert.NotEmpty(contracts);
        Assert.Equal("BTC_USDT-20260507-86000-P", contract.Name);
        Assert.NotNull(settlements);
        Assert.NotEmpty(orderBook.Asks);
        Assert.NotEmpty(orderBook.Bids);
        Assert.NotEmpty(tickers);
        Assert.True(underlyingTicker.TradePut >= 0);
        Assert.NotNull(candlesticks);
        Assert.NotEmpty(underlyingCandlesticks);
        Assert.NotNull(trades);
    }
}
