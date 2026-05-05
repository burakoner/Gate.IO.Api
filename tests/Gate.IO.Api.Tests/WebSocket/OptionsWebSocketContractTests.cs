using Gate.IO.Api.Base;
using Gate.IO.Api.Options;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class OptionsWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/Options";

    [Fact]
    public void Options_subscription_requests_and_success_response_deserialize()
    {
        var publicRequest = JsonFixture.Deserialize<GateStreamRequest>(
            $"{FixtureRoot}/request.subscribe_options_contract_tickers.json");
        var publicPayload = Assert.IsType<JArray>(publicRequest.Payload);

        Assert.Equal(4001, publicRequest.Id);
        Assert.Equal(1630576352, publicRequest.Timestamp);
        Assert.Equal("options.contract_tickers", publicRequest.Channel);
        Assert.Equal(StreamRequestEvent.Subscribe, publicRequest.Event);
        Assert.Equal(["BTC_USDT-20211231-59800-C"], publicPayload.Values<string>().ToArray());
        Assert.Null(publicRequest.Auth);

        var privateRequest = JsonFixture.Deserialize<GateStreamRequest>(
            $"{FixtureRoot}/request.subscribe_options_orders.json");
        var privatePayload = Assert.IsType<JArray>(privateRequest.Payload);

        Assert.Equal(4002, privateRequest.Id);
        Assert.Equal("options.orders", privateRequest.Channel);
        Assert.Equal(["1001", "BTC_USDT-20211130-65000-C"], privatePayload.Values<string>().ToArray());
        Assert.Equal("api_key", privateRequest.Auth.Method);
        Assert.Equal("xxxx", privateRequest.Auth.ApiKey);
        Assert.Equal("xxxx", privateRequest.Auth.Signature);

        var response = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.subscribe_success.json");

        Assert.Equal(4002, response.Id);
        Assert.Equal("options.orders", response.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, response.Event);
        Assert.Null(response.Error);
        Assert.Equal("success", response.Data.Status);
    }

    [Fact]
    public void Options_public_ticker_price_contract_and_settlement_streams_deserialize()
    {
        var contractTicker = JsonFixture.Deserialize<GateStreamResponse<GateOptionsContractTicker>>(
            $"{FixtureRoot}/options.contract_tickers.update.json").Data;
        Assert.Equal("BTC_USDT-20211231-59800-P", contractTicker.Name);
        Assert.Equal(11349.5m, contractTicker.LastPrice);
        Assert.Equal(11170.19m, contractTicker.MarkPrice);
        Assert.Equal(0m, contractTicker.IndexPrice);
        Assert.Equal(993, contractTicker.PositionSize);
        Assert.Equal(-0.78311m, contractTicker.Delta);
        Assert.Equal("3.5541112718136", contractTicker.Leverage);

        var underlyingTicker = JsonFixture.Deserialize<GateStreamResponse<GateOptionsUnderlyingTicker>>(
            $"{FixtureRoot}/options.ul_tickers.update.json").Data;
        Assert.Equal("BTC_USDT", underlyingTicker.Name);
        Assert.Equal(800, underlyingTicker.TradePut);
        Assert.Equal(41700, underlyingTicker.TradeCall);
        Assert.Equal(50695.43m, underlyingTicker.IndexPrice);

        var underlyingPrice = JsonFixture.Deserialize<GateStreamResponse<GateOptionsStreamUnderlyingPrice>>(
            $"{FixtureRoot}/options.ul_price.update.json").Data;
        Assert.Equal("BTC_USDT", underlyingPrice.Underlying);
        Assert.Equal(49653.24m, underlyingPrice.Price);
        Assert.Equal(1639143988931, underlyingPrice.TimeInMilliseconds);

        var markPrice = JsonFixture.Deserialize<GateStreamResponse<GateOptionsStreamContractPrice>>(
            $"{FixtureRoot}/options.mark_prices.update.json").Data;
        Assert.Equal("BTC_USDT-20211231-59800-P", markPrice.Contract);
        Assert.Equal(11021.27m, markPrice.Price);
        Assert.Equal(1639143401676, markPrice.TimeInMilliseconds);

        var settlement = JsonFixture.Deserialize<GateStreamResponse<GateOptionsStreamSettlement>>(
            $"{FixtureRoot}/options.settlements.update.json").Data;
        Assert.Equal("BTC_USDT-20211130-55000-P", settlement.Contract);
        Assert.Equal(GateOptionsContractPeriod.OneWeek, settlement.Period);
        Assert.Equal(0.5m, settlement.Profit);
        Assert.Equal(65000m, settlement.StrikePrice);

        var contract = JsonFixture.Deserialize<GateStreamResponse<GateOptionsStreamContract>>(
            $"{FixtureRoot}/options.contracts.update.json").Data;
        Assert.Equal("BTC_USDT-20211130-50000-P", contract.Contract);
        Assert.False(contract.IsCall);
        Assert.Equal(GateOptionsContractPeriod.OneWeek, contract.Period);
        Assert.Equal(0.0001m, contract.Multiplier);
        Assert.Equal(100000, contract.OrdersLimit);
    }

    [Fact]
    public void Options_public_trade_candlestick_and_order_book_streams_deserialize()
    {
        var trades = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamContractTrade>>>(
            $"{FixtureRoot}/options.trades.update.json");
        var trade = Assert.Single(trades.Data);
        Assert.Equal("BTC_USDT-20211231-59800-C", trade.Contract);
        Assert.Equal(12279, trade.Id);
        Assert.Equal(997.8m, trade.Price);
        Assert.Equal(-100, trade.Size);
        Assert.Equal(1639144526597, trade.TimeInMilliseconds);

        var underlyingTrades = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamUnderlyingTrade>>>(
            $"{FixtureRoot}/options.ul_trades.update.json");
        var underlyingTrade = Assert.Single(underlyingTrades.Data);
        Assert.Equal("BTC_USDT", underlyingTrade.Underlying);
        Assert.True(underlyingTrade.IsCall);
        Assert.Equal(-100, underlyingTrade.Size);

        var contractCandlesticks = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamCandlestick>>>(
            $"{FixtureRoot}/options.contract_candlesticks.update.json");
        var contractCandlestick = Assert.Single(contractCandlesticks.Data);
        Assert.Equal("10s_BTC_USDT-20211231-59800-C", contractCandlestick.Subscription);
        Assert.Equal(1041.4m, contractCandlestick.Close);
        Assert.Equal(100m, contractCandlestick.Volume);

        var underlyingCandlesticks = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamCandlestick>>>(
            $"{FixtureRoot}/options.ul_candlesticks.update.json");
        var underlyingCandlestick = Assert.Single(underlyingCandlesticks.Data);
        Assert.Equal("10s_BTC_USDT", underlyingCandlestick.Subscription);
        Assert.Equal(1041.4m, underlyingCandlestick.Open);

        var bookTicker = JsonFixture.Deserialize<GateStreamResponse<GateOptionsStreamBookTicker>>(
            $"{FixtureRoot}/options.book_ticker.update.json");
        Assert.Equal("BTC_USDT-20211130-50000-C", bookTicker.Data.Symbol);
        Assert.Equal(2517661076, bookTicker.Data.OrderBookUpdateId);
        Assert.Equal(54696.6m, bookTicker.Data.BestBidPrice);
        Assert.Equal(47061m, bookTicker.Data.BestAskAmount);

        var difference = JsonFixture.Deserialize<GateStreamResponse<GateOptionsStreamBookDifference>>(
            $"{FixtureRoot}/options.order_book_update.update.json");
        Assert.Equal("BTC_USDT-20211130-50000-C", difference.Data.Symbol);
        Assert.Equal(2517661101, difference.Data.OrderBookFirstUpdateId);
        Assert.Equal(2517661113, difference.Data.OrderBookLastUpdateId);
        Assert.Equal(2, difference.Data.Bids.Count);
        Assert.Equal(95, difference.Data.Asks[1].Size);

        var snapshot = JsonFixture.Deserialize<GateStreamResponse<GateOptionsStreamBookSnapshot>>(
            $"{FixtureRoot}/options.order_book.all.json");
        Assert.Equal(StreamResponseEvent.All, snapshot.Event);
        Assert.Equal("BTC_USDT-20211130-50000-C", snapshot.Data.Contract);
        Assert.Equal(93973511, snapshot.Data.OrderBookId);
        Assert.Equal(2, snapshot.Data.Asks.Count);
        Assert.Equal(97.0m, snapshot.Data.Bids[0].Price);

        var legacyUpdate = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamOrderBookUpdate>>>(
            $"{FixtureRoot}/options.order_book.update.json");
        var update = Assert.Single(legacyUpdate.Data);
        Assert.Equal("BTC_USDT-20211130-50000-C", update.Contract);
        Assert.Equal(49525.6m, update.Price);
        Assert.Equal(7726, update.Size);
        Assert.Equal(93973511, update.Id);
    }

    [Fact]
    public void Options_private_order_trade_and_liquidation_streams_deserialize()
    {
        var orders = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsOrder>>>(
            $"{FixtureRoot}/options.orders.update.json");
        var order = Assert.Single(orders.Data);
        Assert.Equal(106, order.OrderId);
        Assert.Equal(9001, order.UserId);
        Assert.Equal("BTC_USDT-20211130-65000-C", order.Contract);
        Assert.Equal(GateOptionsOrderFinishAs.Cancelled, order.FinishAs);
        Assert.Equal(GateOptionsOrderStatus.Finished, order.Status);
        Assert.Equal(GateOptionsTimeInForce.GoodTillCancelled, order.TimeInForce);
        Assert.Equal(-10, order.Size);
        Assert.Equal(1639051907000, order.TimeInMilliseconds);

        var trades = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsUserTrade>>>(
            $"{FixtureRoot}/options.usertrades.update.json");
        var trade = Assert.Single(trades.Data);
        Assert.Equal(1, trade.TradeId);
        Assert.Equal(557940, trade.OrderId);
        Assert.Equal(GateOptionsTraderRole.Taker, trade.Role);
        Assert.Equal(0.001m, trade.Fee);
        Assert.Equal("t-xer01sax4yu", trade.ClientOrderId);

        var liquidations = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamUserLiquidation>>>(
            $"{FixtureRoot}/options.liquidates.update.json");
        var liquidation = Assert.Single(liquidations.Data);
        Assert.Equal(1001, liquidation.UserId);
        Assert.Equal(1190m, liquidation.InitialMargin);
        Assert.Equal(1042.5m, liquidation.MaintenanceMargin);
        Assert.Equal(1639051907000, liquidation.TimeInMilliseconds);
    }

    [Fact]
    public void Options_private_settlement_position_balance_streams_deserialize()
    {
        var settlements = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamUserSettlement>>>(
            $"{FixtureRoot}/options.user_settlements.update.json");
        var settlement = Assert.Single(settlements.Data);
        Assert.Equal("BTC_USDT-20211130-65000-C", settlement.Contract);
        Assert.Equal(9001, settlement.UserId);
        Assert.Equal(-13.028m, settlement.RealisedPnl);
        Assert.Equal(70000m, settlement.SettlePrice);

        var positionCloses = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamPositionClose>>>(
            $"{FixtureRoot}/options.position_closes.update.json");
        var positionClose = Assert.Single(positionCloses.Data);
        Assert.Equal("BTC_USDT-20211130-50000-C", positionClose.Contract);
        Assert.Equal(11001, positionClose.UserId);
        Assert.Equal(GateOptionsSide.Long, positionClose.Side);
        Assert.Equal(-0.0056m, positionClose.PNL);

        var balances = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamBalance>>>(
            $"{FixtureRoot}/options.balances.update.json");
        var balance = Assert.Single(balances.Data);
        Assert.Equal(11001, balance.UserId);
        Assert.Equal(GateOptionsBalanceChangeType.SettlementPNL, balance.Type);
        Assert.Equal(60.79009m, balance.Balance);
        Assert.Equal(-0.5m, balance.Change);

        var positions = JsonFixture.Deserialize<GateStreamResponse<List<GateOptionsStreamPosition>>>(
            $"{FixtureRoot}/options.positions.update.json");
        var position = Assert.Single(positions.Data);
        Assert.Equal(9010, position.UserId);
        Assert.Equal("BTC_USDT-20211130-65000-C", position.Contract);
        Assert.Equal(-13.028m, position.RealisedPnl);
        Assert.Equal(0, position.Size);
    }

    [Fact]
    public void Options_stream_request_serialization_maps_payloads_and_auth()
    {
        var publicRequest = new GateStreamRequest
        {
            Id = 72,
            Timestamp = 1630576352,
            Channel = "options.contract_candlesticks",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "10s", "BTC_USDT-20211231-59800-C" },
        };
        var publicJson = JObject.Parse(JsonConvert.SerializeObject(publicRequest));

        Assert.Equal("subscribe", publicJson["event"]!.ToString());
        Assert.Equal("options.contract_candlesticks", publicJson["channel"]!.ToString());
        Assert.Equal(["10s", "BTC_USDT-20211231-59800-C"], publicJson["payload"]!.Values<string>().ToArray());
        Assert.Null(publicJson["auth"]);

        var depthRequest = new GateStreamRequest
        {
            Id = 73,
            Timestamp = 1630650445,
            Channel = "options.order_book_update",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "BTC_USDT-20211130-50000-C", "1000ms", "20" },
        };
        var depthJson = JObject.Parse(JsonConvert.SerializeObject(depthRequest));

        Assert.Equal(["BTC_USDT-20211130-50000-C", "1000ms", "20"], depthJson["payload"]!.Values<string>().ToArray());

        var privateRequest = new GateStreamRequest
        {
            Id = 74,
            Timestamp = 1630654851,
            Channel = "options.positions",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "1001", "!all" },
            Auth = new StreamRequestAuth
            {
                ApiKey = "key",
                Signature = "signature",
            },
        };
        var privateJson = JObject.Parse(JsonConvert.SerializeObject(privateRequest));

        Assert.Equal("options.positions", privateJson["channel"]!.ToString());
        Assert.Equal(["1001", "!all"], privateJson["payload"]!.Values<string>().ToArray());
        Assert.Equal("api_key", privateJson["auth"]!["method"]!.ToString());
        Assert.Equal("key", privateJson["auth"]!["KEY"]!.ToString());
        Assert.Equal("signature", privateJson["auth"]!["SIGN"]!.ToString());
    }

    [Fact]
    public async Task Options_order_book_subscription_validation_matches_documented_values()
    {
        var client = new GateWebSocketClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Options.SubscribeToOrderBookDifferencesAsync("BTC_USDT-20211130-50000-C", 20, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Options.SubscribeToOrderBookDifferencesAsync("BTC_USDT-20211130-50000-C", 100, 25, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Options.SubscribeToOrderBookSnapshotsAsync("BTC_USDT-20211130-50000-C", 100, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Options.SubscribeToOrderBookAsync("BTC_USDT-20211130-50000-C", 100, _ => { }, _ => { }));
    }
}
