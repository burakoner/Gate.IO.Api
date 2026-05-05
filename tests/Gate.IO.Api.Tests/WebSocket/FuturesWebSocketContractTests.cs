using Gate.IO.Api.Base;
using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class FuturesWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/Futures";

    [Fact]
    public void Futures_subscription_request_and_success_response_deserialize()
    {
        var request = JsonFixture.Deserialize<GateStreamRequest>($"{FixtureRoot}/request.subscribe_futures_orders.json");
        var payload = Assert.IsType<JArray>(request.Payload);

        Assert.Equal(1001, request.Id);
        Assert.Equal(1545459681, request.Timestamp);
        Assert.Equal("futures.orders", request.Channel);
        Assert.Equal(StreamRequestEvent.Subscribe, request.Event);
        Assert.Equal(["20011", "BTC_USD"], payload.Values<string>().ToArray());
        Assert.Equal("api_key", request.Auth.Method);
        Assert.Equal("xxxx", request.Auth.ApiKey);
        Assert.Equal("xxxx", request.Auth.Signature);

        var response = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.subscribe_success.json");

        Assert.Equal(1001, response.Id);
        Assert.Equal("futures.orders", response.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, response.Event);
        Assert.Equal("success", response.Data.Status);
    }

    [Fact]
    public void Futures_public_market_stream_updates_deserialize()
    {
        var tickers = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamPerpetualTicker>>>(
            $"{FixtureRoot}/futures.tickers.update.json");
        var ticker = Assert.Single(tickers.Data);
        Assert.Equal("BTC_USDT", ticker.Contract);
        Assert.Equal(96.4m, ticker.Last);
        Assert.Equal(124724m, ticker.TotalSize);

        var trades = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamTrade>>>(
            $"{FixtureRoot}/futures.trades.update.json");
        var trade = Assert.Single(trades.Data);
        Assert.Equal(27753479, trade.Id);
        Assert.Equal(-108m, trade.Size);
        Assert.True(trade.IsInternal);

        var candlesticks = JsonFixture.Deserialize<GateStreamResponse<List<FuturesStreamCandlestick>>>(
            $"{FixtureRoot}/futures.candlesticks.update.json");
        Assert.Equal(2, candlesticks.Data.Count);
        Assert.Equal("1m_BTC_USD", candlesticks.Data[0].Subscription);
        Assert.False(candlesticks.Data[0].IsClosed);
        Assert.True(candlesticks.Data[1].IsClosed);

        var publicLiquidations = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamPublicLiquidation>>>(
            $"{FixtureRoot}/futures.public_liquidates.update.json");
        var liquidation = Assert.Single(publicLiquidations.Data);
        Assert.Equal("BTC_USD", liquidation.Contract);
        Assert.Equal(-124m, liquidation.Size);

        var stats = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamContractStats>>>(
            $"{FixtureRoot}/futures.contract_stats.update.json");
        var stat = Assert.Single(stats.Data);
        Assert.Equal("BTC_USDT", stat.Contract);
        Assert.Equal(124724m, stat.OpenInterest);
        Assert.Equal(8865m, stat.MarkPrice);
    }

    [Fact]
    public void Futures_order_book_stream_updates_deserialize()
    {
        var ticker = JsonFixture.Deserialize<GateStreamResponse<GateFuturesStreamBookTicker>>(
            $"{FixtureRoot}/futures.book_ticker.update.json");
        Assert.Equal("BTC_USDT", ticker.Data.Symbol);
        Assert.Equal(2517661113, ticker.Data.OrderBookUpdateId);
        Assert.Equal(54664.5m, ticker.Data.BestBidPrice);
        Assert.Equal(95m, ticker.Data.BestAskAmount);

        var snapshot = JsonFixture.Deserialize<GateStreamResponse<GateFuturesStreamBookSnapshot>>(
            $"{FixtureRoot}/futures.order_book.update.json");
        Assert.Equal(StreamResponseEvent.All, snapshot.Event);
        Assert.Equal("BTC_USD", snapshot.Data.Symbol);
        Assert.Equal(93973511, snapshot.Data.LastUpdateId);
        Assert.Equal(20, snapshot.Data.Level);
        Assert.Equal(2, snapshot.Data.Asks.Count);

        var difference = JsonFixture.Deserialize<GateStreamResponse<GateFuturesStreamBookDifference>>(
            $"{FixtureRoot}/futures.order_book_update.update.json");
        Assert.True(difference.Data.IsFullSnapshot);
        Assert.Equal("BTC_USD", difference.Data.Contract);
        Assert.Equal(2517661101, difference.Data.OrderBookFirstUpdateId);
        Assert.Equal(100, difference.Data.Level);
        Assert.Equal(54664.5m, difference.Data.Bids[1].Price);

        var v2 = JsonFixture.Deserialize<GateStreamResponse<GateFuturesStreamOrderBookV2Update>>(
            $"{FixtureRoot}/futures.obu.update.json");
        Assert.True(v2.Data.IsFullSnapshot);
        Assert.Equal("ob.BTC_USDT.50", v2.Data.Stream);
        Assert.Equal(73777715168, v2.Data.LastUpdateId);
        Assert.Equal(104027.2m, v2.Data.Asks[0].Price);
    }

    [Fact]
    public void Futures_private_order_trade_and_risk_stream_updates_deserialize()
    {
        var orders = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesOrder>>>(
            $"{FixtureRoot}/futures.orders.update.json");
        var order = Assert.Single(orders.Data);
        Assert.Equal(4872460, order.OrderId);
        Assert.Equal(1628736847325, order.CreateTimeInMilliseconds);
        Assert.Equal(GateFuturesOrderFinishAs.Filled, order.FinishAs);
        Assert.Equal(GateFuturesOrderStatus.Finished, order.Status);
        Assert.Equal(GateFuturesTimeInForce.GoodTillCancelled, order.TimeInForce);
        Assert.Equal(0.03m, order.MarketOrderSlipRatio);
        Assert.Null(order.StopLossPrice);
        Assert.Null(order.StopProfitPrice);

        var trades = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesUserTrade>>>(
            $"{FixtureRoot}/futures.usertrades.update.json");
        var trade = Assert.Single(trades.Data);
        Assert.Equal(3335259, trade.Id);
        Assert.Equal(GateFuturesTradeRole.Maker, trade.Role);
        Assert.Equal(-0.00025m, trade.Fee);

        var liquidations = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesUserLiquidation>>>(
            $"{FixtureRoot}/futures.liquidates.update.json");
        var liquidation = Assert.Single(liquidations.Data);
        Assert.Equal("BTC_USD", liquidation.Contract);
        Assert.Equal(124m, liquidation.Size);
        Assert.Equal(3914424, liquidation.OrderId);

        var deleverages = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesUserDeleverage>>>(
            $"{FixtureRoot}/futures.auto_deleverages.update.json");
        var deleverage = Assert.Single(deleverages.Data);
        Assert.Equal(1040, deleverage.UserId);
        Assert.Equal(10m, deleverage.PositionSize);

        var riskLimits = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamReduceRiskLimit>>>(
            $"{FixtureRoot}/futures.reduce_risk_limits.update.json");
        var riskLimit = Assert.Single(riskLimits.Data);
        Assert.Equal("BTC_USD", riskLimit.Contract);
        Assert.Equal(50m, riskLimit.LeverageMax);
    }

    [Fact]
    public void Futures_private_position_balance_and_auto_order_stream_updates_deserialize()
    {
        var positionCloses = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesPositionClose>>>(
            $"{FixtureRoot}/futures.position_closes.update.json");
        var positionClose = Assert.Single(positionCloses.Data);
        Assert.Equal(GateFuturesPositionSide.Long, positionClose.Side);
        Assert.Equal(209m, positionClose.LongPrice);
        Assert.Null(positionClose.ShortPrice);

        var balances = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamBalance>>>(
            $"{FixtureRoot}/futures.balances.update.json");
        var balance = Assert.Single(balances.Data);
        Assert.Equal(GateFuturesBalanceChangeType.Fee, balance.Type);
        Assert.Equal(211000, balance.UserId);
        Assert.Equal(9.998739899488m, balance.Balance);

        var positions = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesPosition>>>(
            $"{FixtureRoot}/futures.positions.update.json");
        var position = Assert.Single(positions.Data);
        Assert.Equal(GateFuturesPositionMode.Single, position.Mode);
        Assert.Equal(GateFuturesPositionMarginMode.Isolated, position.PositionMarginMode);
        Assert.Equal(7, position.UpdateId);
        Assert.Equal(40010.5m, position.MarkPrice);

        var adlRanks = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamAdlRank>>>(
            $"{FixtureRoot}/futures.position_adl_rank.update.json");
        var adlRank = Assert.Single(adlRanks.Data);
        Assert.Equal(2124426495, adlRank.UserId);
        Assert.Equal(GateFuturesPositionMode.Single, adlRank.Mode);
        Assert.Equal(1, adlRank.RankDivision);

        var autoOrders = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamAutoOrder>>>(
            $"{FixtureRoot}/futures.autoorders.update.json");
        var autoOrder = Assert.Single(autoOrders.Data);
        Assert.Equal(12345, autoOrder.OrderId);
        Assert.Equal(GateFuturesPriceTriggerStatus.Finished, autoOrder.Status);
        Assert.Equal(GateFuturesTriggerType.CloseLongPosition, autoOrder.Type);
        Assert.Equal(GateFuturesTimeInForce.GoodTillCancelled, autoOrder.Order.TimeInForce);
        Assert.Null(autoOrder.Order.AutoSize);
    }

    [Fact]
    public void Futures_stream_request_serialization_maps_payloads_and_auth()
    {
        var publicRequest = new GateStreamRequest
        {
            Id = 42,
            Timestamp = 1545459681,
            Channel = "futures.order_book_update",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "BTC_USDT", "100ms", "100" },
        };
        var publicJson = JObject.Parse(JsonConvert.SerializeObject(publicRequest));

        Assert.Equal("subscribe", publicJson["event"]!.ToString());
        Assert.Equal("futures.order_book_update", publicJson["channel"]!.ToString());
        Assert.Equal(["BTC_USDT", "100ms", "100"], publicJson["payload"]!.Values<string>().ToArray());
        Assert.Null(publicJson["auth"]);

        var privateRequest = new GateStreamRequest
        {
            Id = 43,
            Timestamp = 1545459681,
            Channel = "futures.positions",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "20011", "!all" },
            Auth = new StreamRequestAuth
            {
                ApiKey = "key",
                Signature = "signature",
            },
        };
        var privateJson = JObject.Parse(JsonConvert.SerializeObject(privateRequest));

        Assert.Equal("futures.positions", privateJson["channel"]!.ToString());
        Assert.Equal(["20011", "!all"], privateJson["payload"]!.Values<string>().ToArray());
        Assert.Equal("api_key", privateJson["auth"]!["method"]!.ToString());
        Assert.Equal("key", privateJson["auth"]!["KEY"]!.ToString());
        Assert.Equal("signature", privateJson["auth"]!["SIGN"]!.ToString());
    }

    [Fact]
    public async Task Futures_order_book_subscription_validation_matches_documented_values()
    {
        var client = new GateWebSocketClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Futures.USDT.SubscribeToOrderBookDifferencesAsync("BTC_USDT", 10, 20, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Futures.USDT.SubscribeToOrderBookDifferencesAsync("BTC_USDT", 20, 50, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Futures.USDT.SubscribeToOrderBookSnapshotsAsync("BTC_USDT", 500, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Futures.USDT.SubscribeToOrderBookV2UpdatesAsync("BTC_USDT", 100, _ => { }));
    }
}
