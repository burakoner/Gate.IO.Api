using Gate.IO.Api.Base;
using Gate.IO.Api.Delivery;
using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class DeliveryWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/Delivery";

    [Fact]
    public void Delivery_subscription_request_and_success_response_deserialize()
    {
        var request = JsonFixture.Deserialize<GateStreamRequest>($"{FixtureRoot}/request.subscribe_delivery_orders.json");
        var payload = Assert.IsType<JArray>(request.Payload);

        Assert.Equal(2001, request.Id);
        Assert.Equal(1545459681, request.Timestamp);
        Assert.Equal("futures.orders", request.Channel);
        Assert.Equal(StreamRequestEvent.Subscribe, request.Event);
        Assert.Equal(["20011", "BTC_USDT_20230630"], payload.Values<string>().ToArray());
        Assert.Equal("api_key", request.Auth.Method);
        Assert.Equal("xxxx", request.Auth.ApiKey);
        Assert.Equal("xxxx", request.Auth.Signature);

        var response = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.subscribe_success.json");

        Assert.Equal(2001, response.Id);
        Assert.Equal("futures.orders", response.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, response.Event);
        Assert.Null(response.Error);
        Assert.Equal("success", response.Data.Status);
    }

    [Fact]
    public void Delivery_public_market_stream_updates_deserialize()
    {
        var tickers = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamPerpetualTicker>>>(
            $"{FixtureRoot}/futures.tickers.update.json");
        var ticker = Assert.Single(tickers.Data);
        Assert.Equal("BTC_USDT_20230630", ticker.Contract);
        Assert.Equal(118.4m, ticker.Last);
        Assert.Null(ticker.QuantoBaseRate);
        Assert.Equal(117m, ticker.Volume24hBtc);

        var trades = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamTrade>>>(
            $"{FixtureRoot}/futures.trades.update.json");
        var trade = Assert.Single(trades.Data);
        Assert.Equal("BTC_USDT_20230630", trade.Contract);
        Assert.Equal(27753479, trade.Id);
        Assert.Equal(-108m, trade.Size);
        Assert.Equal(96.4m, trade.Price);
        Assert.Null(trade.IsInternal);

        var candlesticks = JsonFixture.Deserialize<GateStreamResponse<List<FuturesStreamCandlestick>>>(
            $"{FixtureRoot}/futures.candlesticks.update.json");
        Assert.Equal(2, candlesticks.Data.Count);
        Assert.Equal("1m_BTC_USDT_20230630", candlesticks.Data[0].Subscription);
        Assert.Equal(95.4m, candlesticks.Data[0].Close);
        Assert.Equal(0m, candlesticks.Data[0].Amount);
        Assert.Null(candlesticks.Data[0].IsClosed);
    }

    [Fact]
    public void Delivery_order_book_stream_updates_deserialize()
    {
        var bookTicker = JsonFixture.Deserialize<GateStreamResponse<GateFuturesStreamBookTicker>>(
            $"{FixtureRoot}/futures.book_ticker.update.json");
        Assert.Equal("BTC_USDT_20230630", bookTicker.Data.Symbol);
        Assert.Equal(2517661076, bookTicker.Data.OrderBookUpdateId);
        Assert.Equal(54696.6m, bookTicker.Data.BestBidPrice);
        Assert.Equal(47061m, bookTicker.Data.BestAskAmount);

        var snapshot = JsonFixture.Deserialize<GateStreamResponse<GateFuturesStreamBookSnapshot>>(
            $"{FixtureRoot}/futures.order_book.all.json");
        Assert.Equal(StreamResponseEvent.All, snapshot.Event);
        Assert.Equal("BTC_USDT_20230630", snapshot.Data.Symbol);
        Assert.Equal(93973511, snapshot.Data.LastUpdateId);
        Assert.Equal(2, snapshot.Data.Asks.Count);
        Assert.Equal(97.0m, snapshot.Data.Bids[0].Price);

        var legacyUpdate = JsonFixture.Deserialize<GateStreamResponse<List<GateDeliveryStreamOrderBookUpdate>>>(
            $"{FixtureRoot}/futures.order_book.update.json");
        var update = Assert.Single(legacyUpdate.Data);
        Assert.Equal("BTC_USDT_20230630", update.Contract);
        Assert.Equal(97.5m, update.Price);
        Assert.Equal(6541m, update.Size);
        Assert.Equal(93973512, update.Id);

        var difference = JsonFixture.Deserialize<GateStreamResponse<GateFuturesStreamBookDifference>>(
            $"{FixtureRoot}/futures.order_book_update.update.json");
        Assert.Null(difference.Data.IsFullSnapshot);
        Assert.Equal("BTC_USDT_20230630", difference.Data.Contract);
        Assert.Equal(2517661101, difference.Data.OrderBookFirstUpdateId);
        Assert.Equal(2517661113, difference.Data.OrderBookLastUpdateId);
        Assert.Equal(2, difference.Data.Bids.Count);
        Assert.Equal(95m, difference.Data.Asks[1].Size);
    }

    [Fact]
    public void Delivery_private_order_trade_and_risk_stream_updates_deserialize()
    {
        var orders = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesOrder>>>(
            $"{FixtureRoot}/futures.orders.update.json");
        var order = Assert.Single(orders.Data);
        Assert.Equal(93282759, order.OrderId);
        Assert.Equal(20011, order.UserId);
        Assert.Equal("BTC_USDT_20230630", order.Contract);
        Assert.Equal(1545141817123, order.CreateTimeInMilliseconds);
        Assert.Equal(1545640868123, order.FinishTimeInMilliseconds);
        Assert.Equal(GateFuturesOrderFinishAs.Filled, order.FinishAs);
        Assert.Equal(GateFuturesOrderStatus.Finished, order.Status);
        Assert.Equal(GateFuturesTimeInForce.GoodTillCancelled, order.TimeInForce);

        var trades = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesUserTrade>>>(
            $"{FixtureRoot}/futures.usertrades.update.json");
        var trade = Assert.Single(trades.Data);
        Assert.Equal(12651269, trade.Id);
        Assert.Equal(GateFuturesTradeRole.Maker, trade.Role);
        Assert.Equal(56945246, trade.OrderId);
        Assert.Equal(10m, trade.Size);

        var liquidations = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesUserLiquidation>>>(
            $"{FixtureRoot}/futures.liquidates.update.json");
        var liquidation = Assert.Single(liquidations.Data);
        Assert.Equal("BTC_USDT_20230630", liquidation.Contract);
        Assert.Equal(-124m, liquidation.Size);
        Assert.Equal(4093362, liquidation.OrderId);

        var deleverages = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesUserDeleverage>>>(
            $"{FixtureRoot}/futures.auto_deleverages.update.json");
        var deleverage = Assert.Single(deleverages.Data);
        Assert.Equal(1040, deleverage.UserId);
        Assert.Equal(10m, deleverage.PositionSize);

        var riskLimits = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamReduceRiskLimit>>>(
            $"{FixtureRoot}/futures.reduce_risk_limits.update.json");
        var riskLimit = Assert.Single(riskLimits.Data);
        Assert.Equal("ETH_USD", riskLimit.Contract);
        Assert.Equal(0, riskLimit.CancelOrders);
        Assert.Equal(10m, riskLimit.LeverageMax);
        Assert.Equal(450m, riskLimit.RiskLimit);
    }

    [Fact]
    public void Delivery_private_position_balance_and_auto_order_stream_updates_deserialize()
    {
        var positionCloses = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesPositionClose>>>(
            $"{FixtureRoot}/futures.position_closes.update.json");
        var positionClose = Assert.Single(positionCloses.Data);
        Assert.Equal(GateFuturesPositionSide.Long, positionClose.Side);
        Assert.Equal(-0.000624354791m, positionClose.Pnl);
        Assert.Null(positionClose.LongPrice);
        Assert.Null(positionClose.ShortPrice);

        var balances = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamBalance>>>(
            $"{FixtureRoot}/futures.balances.update.json");
        var balance = Assert.Single(balances.Data);
        Assert.Equal(GateFuturesBalanceChangeType.Fee, balance.Type);
        Assert.Equal(20011, balance.UserId);
        Assert.Null(balance.Currency);
        Assert.Equal(9.998739899488m, balance.Balance);

        var positions = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesPosition>>>(
            $"{FixtureRoot}/futures.positions.update.json");
        var position = Assert.Single(positions.Data);
        Assert.Equal(10003, position.UserId);
        Assert.Equal("BTC_USDT_20230630", position.Contract);
        Assert.Equal(70m, position.Size);
        Assert.Equal(10m, position.Leverage);
        Assert.NotNull(position.Time);

        var autoOrders = JsonFixture.Deserialize<GateStreamResponse<List<GateFuturesStreamAutoOrder>>>(
            $"{FixtureRoot}/futures.autoorders.update.json");
        var autoOrder = Assert.Single(autoOrders.Data);
        Assert.Equal(1543255, autoOrder.UserId);
        Assert.Equal(9256, autoOrder.OrderId);
        Assert.Equal(GateFuturesPriceTriggerStatus.Open, autoOrder.Status);
        Assert.Null(autoOrder.Type);
        Assert.Equal(GateFuturesTimeInForce.GoodTillCancelled, autoOrder.Order.TimeInForce);
        Assert.Null(autoOrder.StopTrigger.TriggerPrice);
        Assert.Null(autoOrder.Order.AutoSize);
    }

    [Fact]
    public void Delivery_stream_request_serialization_maps_payloads_and_auth()
    {
        var publicRequest = new GateStreamRequest
        {
            Id = 52,
            Timestamp = 1545459681,
            Channel = "futures.order_book_update",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "BTC_USDT_20230630", "100ms", "20" },
        };
        var publicJson = JObject.Parse(JsonConvert.SerializeObject(publicRequest));

        Assert.Equal("subscribe", publicJson["event"]!.ToString());
        Assert.Equal("futures.order_book_update", publicJson["channel"]!.ToString());
        Assert.Equal(["BTC_USDT_20230630", "100ms", "20"], publicJson["payload"]!.Values<string>().ToArray());
        Assert.Null(publicJson["auth"]);

        var privateRequest = new GateStreamRequest
        {
            Id = 53,
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
    public async Task Delivery_order_book_subscription_validation_matches_documented_values()
    {
        var client = new GateWebSocketClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Delivery.USDT.SubscribeToOrderBookDifferencesAsync("BTC_USDT_20230630", 20, 20, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Delivery.USDT.SubscribeToOrderBookDifferencesAsync("BTC_USDT_20230630", 100, 25, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Delivery.USDT.SubscribeToOrderBookSnapshotsAsync("BTC_USDT_20230630", 500, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Delivery.USDT.SubscribeToOrderBookAsync("BTC_USDT_20230630", 500, _ => { }, _ => { }));
    }
}
