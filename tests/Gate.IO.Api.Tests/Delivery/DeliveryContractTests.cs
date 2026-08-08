using Gate.IO.Api.Delivery;
using Gate.IO.Api.Futures;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Delivery;

[Trait("Category", "Contract")]
public class DeliveryContractTests
{
    [Fact]
    public void Documented_delivery_public_market_responses_deserialize()
    {
        var contracts = JsonFixture.Deserialize<List<GateDeliveryContract>>("Docs/Delivery/contracts.success.json");
        var contract = JsonFixture.Deserialize<GateDeliveryContract>("Docs/Delivery/contract.success.json");
        var orderBook = JsonFixture.Deserialize<GateFuturesOrderBook>("Docs/Delivery/order_book.success.json");
        var trades = JsonFixture.Deserialize<List<GateFuturesTrade>>("Docs/Delivery/trades.success.json");
        var candlesticks = JsonFixture.Deserialize<List<GateFuturesCandlestick>>("Docs/Delivery/candlesticks.success.json");
        var tickers = JsonFixture.Deserialize<List<GateFuturesTicker>>("Docs/Delivery/tickers.success.json");
        var insurance = JsonFixture.Deserialize<List<GateFuturesInsuranceBalance>>("Docs/Delivery/insurance.success.json");
        var riskLimitTiers = JsonFixture.Deserialize<List<GateDeliveryRiskLimitTier>>("Docs/Delivery/risk_limit_tiers.success.json");

        Assert.Single(contracts);
        Assert.Equal("BTC_USDT_20200814", contracts[0].Contract);
        Assert.Equal(GateFuturesDeliveryCycle.Weekly, contracts[0].Cycle);
        Assert.Equal(GateFuturesContractType.Direct, contracts[0].Type);
        Assert.Equal(GateFuturesMarkType.Index, contracts[0].MarkType);
        Assert.Equal(9017m, contract.LastPrice);
        Assert.Equal(123456, orderBook.Id);
        Assert.Equal(1.52m, orderBook.Asks[0].Price);
        Assert.Equal(-100, trades[0].Size);
        Assert.Equal(10m, candlesticks[0].Close);
        Assert.Equal(9017m, tickers[0].Last);
        Assert.Equal(83.0031m, insurance[0].Balance);
        Assert.Equal(20000m, riskLimitTiers[0].RiskLimit);
    }

    [Fact]
    public void Documented_delivery_private_account_position_and_order_responses_deserialize()
    {
        var account = JsonFixture.Deserialize<GateFuturesBalance>("Docs/Delivery/account.success.json");
        var accountBook = JsonFixture.Deserialize<List<GateFuturesBalanceChange>>("Docs/Delivery/account_book.success.json");
        var positions = JsonFixture.Deserialize<List<GateFuturesPosition>>("Docs/Delivery/positions.success.json");
        var positionCloses = JsonFixture.Deserialize<List<GateFuturesPositionClose>>("Docs/Delivery/position_close.success.json");
        var liquidations = JsonFixture.Deserialize<List<GateFuturesUserLiquidation>>("Docs/Delivery/liquidates.success.json");
        var settlements = JsonFixture.Deserialize<List<GateDeliveryUserSettlement>>("Docs/Delivery/settlements.success.json");
        var userTrades = JsonFixture.Deserialize<List<GateFuturesUserTrade>>("Docs/Delivery/my_trades.success.json");
        var orders = JsonFixture.Deserialize<List<GateFuturesOrder>>("Docs/Delivery/orders.success.json");
        var order = JsonFixture.Deserialize<GateFuturesOrder>("Docs/Delivery/order.success.json");
        var priceOrders = JsonFixture.Deserialize<List<GateFuturesPriceTriggeredOrder>>("Docs/Delivery/price_orders.success.json");
        var priceOrder = JsonFixture.Deserialize<GateFuturesPriceTriggeredOrder>("Docs/Delivery/price_order.success.json");

        Assert.Equal("USDT", account.Currency);
        Assert.Equal(9707.803567115145m, account.Total);
        Assert.Equal(GateFuturesBalanceChangeType.Fee, accountBook[0].Type);
        Assert.Equal(GateFuturesPositionMode.Single, positions[0].Mode);
        Assert.Equal(GateFuturesPositionSide.Long, positionCloses[0].Side);
        Assert.Equal(600m, liquidations[0].Size);
        Assert.Equal(-6.87498m, settlements[0].Profit);
        Assert.Equal(GateFuturesTradeRole.Taker, userTrades[0].Role);
        Assert.Single(orders);
        Assert.Equal(GateFuturesOrderStatus.Finished, order.Status);
        Assert.Equal(GateFuturesOrderFinishAs.Filled, order.FinishAs);
        Assert.Equal(GateFuturesSelfTradeAction.None, order.SelfTradeAction);
        Assert.Equal(GateFuturesPriceTriggerStatus.Finished, priceOrders[0].Status);
        Assert.Equal(GateFuturesOrderFinishAs.Cancelled, priceOrders[0].FinishAs);
        Assert.Equal("100.5", priceOrders[0].Order.Amount);
        Assert.Equal(GateFuturesPositionMarginMode.Isolated, priceOrders[0].PositionMarginMode);
        Assert.Equal(GateSpotTriggerCondition.GreaterThanOrEqualTo, priceOrder.Trigger.Rule);
        Assert.Equal(GateFuturesTriggerType.CloseLongOrder, priceOrder.Type);
    }

    [Fact]
    public void Captured_live_public_delivery_responses_deserialize()
    {
        var contracts = JsonFixture.Deserialize<List<GateDeliveryContract>>("Live/Delivery/contracts.usdt.json");
        var contract = JsonFixture.Deserialize<GateDeliveryContract>("Live/Delivery/contract.usdt.first.json");
        var tickers = JsonFixture.Deserialize<List<GateFuturesTicker>>("Live/Delivery/tickers.usdt.first.json");
        var orderBook = JsonFixture.Deserialize<GateFuturesOrderBook>("Live/Delivery/order_book.usdt.first.limit5.json");
        var trades = JsonFixture.Deserialize<List<GateFuturesTrade>>("Live/Delivery/trades.usdt.first.limit1.json");
        var candlesticks = JsonFixture.Deserialize<List<GateFuturesCandlestick>>("Live/Delivery/candlesticks.usdt.first.1m.limit1.json");
        var insurance = JsonFixture.Deserialize<List<GateFuturesInsuranceBalance>>("Live/Delivery/insurance.usdt.limit1.json");
        var riskLimitTiers = JsonFixture.Deserialize<List<GateDeliveryRiskLimitTier>>("Live/Delivery/risk_limit_tiers.usdt.first.limit1.json");

        Assert.NotEmpty(contracts);
        Assert.False(string.IsNullOrWhiteSpace(contracts[0].Contract));
        Assert.Equal(contracts[0].Contract, contract.Contract);
        Assert.NotEmpty(tickers);
        Assert.Equal(contract.Contract, tickers[0].Contract);
        Assert.NotEmpty(orderBook.Asks);
        Assert.NotEmpty(orderBook.Bids);
        Assert.NotNull(trades);
        Assert.NotNull(candlesticks);
        Assert.NotEmpty(insurance);
        Assert.NotEmpty(riskLimitTiers);
    }
}
