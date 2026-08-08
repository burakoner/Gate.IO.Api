using Gate.IO.Api.Futures;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Contract")]
public class FuturesContractTests
{
    [Fact]
    public void Documented_futures_public_market_responses_deserialize()
    {
        var contracts = JsonFixture.Deserialize<List<GateFuturesContract>>("Docs/Futures/contracts.success.json");
        var contract = JsonFixture.Deserialize<GateFuturesContract>("Docs/Futures/contract.success.json");
        var orderBook = JsonFixture.Deserialize<GateFuturesOrderBook>("Docs/Futures/order_book.success.json");
        var trades = JsonFixture.Deserialize<List<GateFuturesTrade>>("Docs/Futures/trades.success.json");
        var candlesticks = JsonFixture.Deserialize<List<GateFuturesCandlestick>>("Docs/Futures/candlesticks.success.json");
        var tickers = JsonFixture.Deserialize<List<GateFuturesTicker>>("Docs/Futures/tickers.success.json");
        var fundingRates = JsonFixture.Deserialize<List<GateFuturesFundingRate>>("Docs/Futures/funding_rate.success.json");
        var insurance = JsonFixture.Deserialize<List<GateFuturesInsuranceBalance>>("Docs/Futures/insurance.success.json");
        var stats = JsonFixture.Deserialize<List<GateFuturesStats>>("Docs/Futures/contract_stats.success.json");
        var constituents = JsonFixture.Deserialize<GateFuturesIndexConstituents>("Docs/Futures/index_constituents.success.json");
        var liquidations = JsonFixture.Deserialize<List<GateFuturesLiquidation>>("Docs/Futures/liq_orders.success.json");
        var riskLimitTiers = JsonFixture.Deserialize<List<GateFuturesRiskLimitTier>>("Docs/Futures/risk_limit_tiers.success.json");

        Assert.Single(contracts);
        Assert.Equal("BTC_USDT", contracts[0].Contract);
        Assert.Equal(GateFuturesContractType.Direct, contracts[0].Type);
        Assert.Equal(GateFuturesMarkType.Index, contracts[0].MarkType);
        Assert.Equal(GateFuturesContractStatus.Trading, contracts[0].Status);
        Assert.Equal(38026m, contract.LastPrice);
        Assert.Equal(123456, orderBook.Id);
        Assert.Equal(1.52m, orderBook.Asks[0].Price);
        Assert.Equal(-100, trades[0].Size);
        Assert.Equal(10m, candlesticks[0].Close);
        Assert.Equal(38026m, tickers[0].Last);
        Assert.Equal(0.000157m, fundingRates[0].Rate);
        Assert.Equal(1000000m, insurance[0].Balance);
        Assert.Equal(38026m, stats[0].MarkPrice);
        Assert.Equal("BTC_USDT", constituents.Index);
        Assert.Equal(-100, liquidations[0].Size);
        Assert.Equal(20000m, riskLimitTiers[0].RiskLimit);
    }

    [Fact]
    public void Documented_futures_batch_funding_rate_response_deserializes_from_nested_shape()
    {
        var token = JsonFixture.Parse("Docs/Futures/funding_rates.success.json");
        var rates = token[0]!.ToObject<List<GateFuturesBatchFundingRate>>();

        Assert.NotNull(rates);
        Assert.Single(rates!);
        Assert.Equal("BTC_USDT", rates![0].Contract);
        Assert.Equal(0.000157m, rates[0].Data[0].Rate);
    }

    [Fact]
    public void Documented_futures_private_account_and_order_responses_deserialize()
    {
        var account = JsonFixture.Deserialize<GateFuturesBalance>("Docs/Futures/account.success.json");
        var accountBook = JsonFixture.Deserialize<List<GateFuturesBalanceChange>>("Docs/Futures/account_book.success.json");
        var order = JsonFixture.Deserialize<GateFuturesOrder>("Docs/Futures/order.success.json");
        var priceOrders = JsonFixture.Deserialize<List<GateFuturesPriceTriggeredOrder>>("Docs/Futures/price_orders.success.json");
        var priceOrder = JsonFixture.Deserialize<GateFuturesPriceTriggeredOrder>("Docs/Futures/price_order.success.json");
        var chaseOrder = JsonFixture.Parse("Docs/Futures/chase_order.success.json")["order"]!.ToObject<GateFuturesChaseOrder>();

        Assert.Equal("USDT", account.Currency);
        Assert.Equal(10.5m, account.Total);
        Assert.Equal(GateFuturesBalanceChangeType.Fee, accountBook[0].Type);
        Assert.Equal(GateFuturesOrderStatus.Finished, order.Status);
        Assert.Equal(GateFuturesOrderFinishAs.Filled, order.FinishAs);
        Assert.Equal(GateFuturesTimeInForce.GoodTillCancelled, order.TimeInForce);
        Assert.Equal(GateFuturesSelfTradeAction.CancelNewest, order.SelfTradeAction);
        Assert.Equal(10.5m, order.Size);
        Assert.Equal(0.5m, order.Iceberg);
        Assert.Equal(GateFuturesActionMode.Full, order.ActionMode);
        Assert.Equal(3800m, order.TakeProfitTriggerPrice);
        Assert.Equal(3700m, order.StopLossTriggerPrice);
        Assert.Equal(GateFuturesPriceTriggerStatus.Finished, priceOrders[0].Status);
        Assert.Equal(GateFuturesOrderFinishAs.Cancelled, priceOrders[0].FinishAs);
        Assert.Equal("100.5", priceOrders[0].Order.Amount);
        Assert.False(priceOrders[0].Order.IsReduceOnly!.Value);
        Assert.False(priceOrders[0].Order.IsClose!.Value);
        Assert.Equal(GateFuturesPositionMarginMode.Cross, priceOrders[0].PositionMarginMode);
        Assert.Equal(GateSpotTriggerCondition.GreaterThanOrEqualTo, priceOrder.Trigger.Rule);
        Assert.Equal(GateFuturesTriggerType.CloseLongOrder, priceOrder.Type);
        Assert.Equal("1283293", priceOrder.OrderIdString);
        Assert.NotNull(chaseOrder);
        Assert.Equal("9007199254740993", chaseOrder!.OrderId);
        Assert.Equal("100000", chaseOrder.UserId);
        Assert.Equal("BTC_USDT", chaseOrder.Contract);
        Assert.Equal("usdt", chaseOrder.Settlement);
        Assert.Equal("10.5", chaseOrder.Amount);
        Assert.Equal("65000", chaseOrder.PriceLimit);
        Assert.False(chaseOrder.ReduceOnly!.Value);
        Assert.Equal("t-chase-1", chaseOrder.ClientOrderId);
        Assert.Equal(1778716800, chaseOrder.CreateTime);
        Assert.Equal(1778716860, chaseOrder.FinishTime);
        Assert.Equal(3, chaseOrder.OriginalStatus);
        Assert.Equal("finished", chaseOrder.Status);
        Assert.Equal("cancelled", chaseOrder.Reason);
        Assert.Equal("2.5", chaseOrder.FillAmount);
        Assert.Equal("64000.25", chaseOrder.AverageFillPrice);
        Assert.Equal("9007199254740994", chaseOrder.SubOrderId);
        Assert.True(chaseOrder.IsDualMode!.Value);
        Assert.Equal("long", chaseOrder.SideLabel);
        Assert.Equal("long", chaseOrder.PositionSideOutput);
        Assert.Equal("64010.5", chaseOrder.ChasePrice);
        Assert.Equal((uint)1, chaseOrder.IntervalSeconds);
        Assert.Equal(1778716860000, chaseOrder.UpdatedAt);
        Assert.Equal("64010", chaseOrder.SubOrderPrice);
        Assert.False(chaseOrder.SubOrderOngoing!.Value);
        Assert.Equal("cancelled", chaseOrder.SubOrderFinishAs);
        Assert.Equal(2, chaseOrder.PriceType);
        Assert.Equal("1", chaseOrder.PriceGapType);
        Assert.Equal("10", chaseOrder.PriceGapValue);
        Assert.Equal("FINISHED", chaseOrder.StatusCode);
        Assert.Equal("1778716800.123456", chaseOrder.CreateTimePrecise);
        Assert.Equal("1778716860.654321", chaseOrder.FinishTimePrecise);
        Assert.Equal("isolated", chaseOrder.PositionMarginMode);
        Assert.Equal("dual", chaseOrder.PositionMode);
        Assert.Equal("10", chaseOrder.Leverage);
        Assert.Equal(string.Empty, chaseOrder.ErrorLabel);
    }

    [Fact]
    public void Captured_live_public_futures_responses_deserialize()
    {
        var contracts = JsonFixture.Deserialize<List<GateFuturesContract>>("Live/Futures/contracts.usdt.limit1.json");
        var contract = JsonFixture.Deserialize<GateFuturesContract>("Live/Futures/contract.BTC_USDT.json");
        var tickers = JsonFixture.Deserialize<List<GateFuturesTicker>>("Live/Futures/tickers.BTC_USDT.json");
        var orderBook = JsonFixture.Deserialize<GateFuturesOrderBook>("Live/Futures/order_book.BTC_USDT.limit5.json");
        var trades = JsonFixture.Deserialize<List<GateFuturesTrade>>("Live/Futures/trades.BTC_USDT.limit1.json");
        var candlesticks = JsonFixture.Deserialize<List<GateFuturesCandlestick>>("Live/Futures/candlesticks.BTC_USDT.1m.limit1.json");
        var fundingRates = JsonFixture.Deserialize<List<GateFuturesFundingRate>>("Live/Futures/funding_rate.BTC_USDT.limit1.json");
        var batchFundingRates = JsonFixture.Deserialize<List<GateFuturesBatchFundingRate>>("Live/Futures/funding_rates.BTC_USDT.json");
        var insurance = JsonFixture.Deserialize<List<GateFuturesInsuranceBalance>>("Live/Futures/insurance.usdt.limit1.json");
        var stats = JsonFixture.Deserialize<List<GateFuturesStats>>("Live/Futures/contract_stats.BTC_USDT.1h.limit1.json");
        var constituents = JsonFixture.Deserialize<GateFuturesIndexConstituents>("Live/Futures/index_constituents.BTC_USDT.json");
        var liquidations = JsonFixture.Deserialize<List<GateFuturesLiquidation>>("Live/Futures/liq_orders.BTC_USDT.limit1.json");
        var riskLimitTiers = JsonFixture.Deserialize<List<GateFuturesRiskLimitTier>>("Live/Futures/risk_limit_tiers.BTC_USDT.limit1.json");

        Assert.NotEmpty(contracts);
        Assert.Equal(GateFuturesContractStatus.Trading, contracts[0].Status);
        Assert.Equal("BTC_USDT", contract.Contract);
        Assert.Single(tickers);
        Assert.Equal("BTC_USDT", tickers[0].Contract);
        Assert.NotEmpty(orderBook.Asks);
        Assert.NotEmpty(orderBook.Bids);
        Assert.NotEmpty(trades);
        Assert.NotEmpty(candlesticks);
        Assert.NotEmpty(fundingRates);
        Assert.Single(batchFundingRates);
        Assert.Equal("BTC_USDT", batchFundingRates[0].Contract);
        Assert.NotEmpty(insurance);
        Assert.NotEmpty(stats);
        Assert.Equal("BTC_USDT", constituents.Index);
        Assert.NotNull(liquidations);
        Assert.NotEmpty(riskLimitTiers);
    }
}
