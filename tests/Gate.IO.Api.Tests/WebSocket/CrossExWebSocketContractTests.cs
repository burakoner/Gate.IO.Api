using ApiSharp.Authentication;
using Gate.IO.Api.Base;
using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class CrossExWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/CrossEx";

    [Fact]
    public void CrossEx_login_request_success_response_and_signature_deserialize()
    {
        var request = JsonFixture.Deserialize<GateCrossExStreamRequest>($"{FixtureRoot}/request.login.json");
        var requestPayload = Assert.IsType<JObject>(request.Payload);

        Assert.Equal(1754272114, request.Timestamp);
        Assert.Null(request.Channel);
        Assert.Equal("login", request.Event);
        Assert.Equal("api_key", requestPayload["method"]!.ToString());
        Assert.Equal("key", requestPayload["api_key"]!.ToString());

        var authentication = new GateAuthentication(new ApiCredentials("key", "secret"));
        var payload = authentication.CreateCrossExLoginPayload(1754272114);

        Assert.Equal("api_key", payload.Method);
        Assert.Equal("key", payload.ApiKey);
        Assert.Equal(requestPayload["sign"]!.ToString(), payload.Sign);

        var response = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExStreamLoginResult>>(
            $"{FixtureRoot}/response.login_success.json");

        Assert.Equal("login", response.Event);
        Assert.Equal("644ff57f-add4-407a-97aa-b654c4ede839", response.Payload.ConnectionId);
        Assert.Equal("100000", response.Result.Code);
        Assert.Equal("success", response.Result.Message);
        Assert.True(response.Result.Success);
        Assert.Equal(1756434168928, response.TimeInMilliseconds);
    }

    [Fact]
    public void CrossEx_subscription_requests_and_success_response_deserialize()
    {
        var lastPriceRequest = JsonFixture.Deserialize<GateCrossExStreamRequest>(
            $"{FixtureRoot}/request.subscribe_last_price.json");
        var lastPricePayload = Assert.IsType<JArray>(lastPriceRequest.Payload);

        Assert.Equal(1754272114, lastPriceRequest.Timestamp);
        Assert.Equal("last_price", lastPriceRequest.Channel);
        Assert.Equal("subscribe", lastPriceRequest.Event);
        Assert.Equal(
            ["BINANCE_SPOT_BTC_USDT", "OKX_FUTURE_BTC_USDT", "GATE_FUTURE_ETH_USDT"],
            lastPricePayload.Values<string>().ToArray());

        var orderRequest = JsonFixture.Deserialize<GateCrossExStreamRequest>(
            $"{FixtureRoot}/request.subscribe_order_private.json");
        var orderPayload = Assert.IsType<JArray>(orderRequest.Payload);

        Assert.Equal("order", orderRequest.Channel);
        Assert.Equal("subscribe", orderRequest.Event);
        Assert.Equal(["GATE_FUTURE_ETH_USDT"], orderPayload.Values<string>().ToArray());

        var response = JsonFixture.Deserialize<GateCrossExStreamResponse<List<string>>>(
            $"{FixtureRoot}/response.subscribe_order_success.json");

        Assert.Equal("order", response.Channel);
        Assert.Equal("subscribe", response.Event);
        Assert.Equal(["OKX_FUTURE_ADA_USDT"], response.Payload);
        Assert.True(response.Result.Success);
    }

    [Fact]
    public void CrossEx_public_price_and_rate_stream_updates_deserialize()
    {
        var lastPrice = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamLastPrice>>(
            $"{FixtureRoot}/last_price.update.json");
        Assert.Equal("last_price", lastPrice.Channel);
        Assert.Equal(StreamResponseEvent.Update, lastPrice.Event);
        Assert.Equal("BINANCE_SPOT_BTC_USDT", lastPrice.Data.Symbol);
        Assert.Equal(65873.49000000m, lastPrice.Data.LastPrice);

        var indexPrice = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamIndexPrice>>(
            $"{FixtureRoot}/index_price.update.json");
        Assert.Equal("BINANCE_MARGIN_BTC_USDT", indexPrice.Data.Symbol);
        Assert.Equal(65590.40739130m, indexPrice.Data.IndexPrice);

        var markPrice = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamMarkPrice>>(
            $"{FixtureRoot}/mark_price.update.json");
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", markPrice.Data.Symbol);
        Assert.Equal(65566.30000000m, markPrice.Data.MarkPrice);

        var fundingRate = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamFundingRate>>(
            $"{FixtureRoot}/funding_rate.update.json");
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", fundingRate.Data.Symbol);
        Assert.Equal(0.00002534m, fundingRate.Data.FundingRate);
        Assert.Equal(1773388800000, fundingRate.Data.NextFundingTime);

        var openInterest = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamOpenInterest>>(
            $"{FixtureRoot}/open_interest.update.json");
        Assert.Equal("OKX_FUTURE_BTC_USDT", openInterest.Data.Symbol);
        Assert.Equal(2929823.88000001813m, openInterest.Data.OpenInterest);
        Assert.Equal(29298.2388000001813m, openInterest.Data.OpenInterestValue);
    }

    [Fact]
    public void CrossEx_public_market_stream_updates_deserialize()
    {
        var ticker = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamTicker>>(
            $"{FixtureRoot}/ticker.update.json");
        Assert.Equal("ticker", ticker.Channel);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", ticker.Data.Symbol);
        Assert.Equal(67280.50m, ticker.Data.LastPrice);
        Assert.Equal(35m, ticker.Data.BidSize);
        Assert.Equal(5523487123.55m, ticker.Data.QuoteVolume24h);
        Assert.Equal(1710001234567, ticker.Data.Timestamp);

        var trade = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamTrade>>(
            $"{FixtureRoot}/trade.update.json");
        Assert.Equal("trade", trade.Channel);
        Assert.Equal("BTCUSDT", trade.Data.Symbol);
        Assert.Equal("18473628192", trade.Data.TradeId);
        Assert.Equal(67250.12m, trade.Data.Price);
        Assert.Equal(0.015m, trade.Data.Quantity);
        Assert.Equal("BUY", trade.Data.Side);
        Assert.False(trade.Data.IsBuyerMaker);

        var kline = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamKline>>(
            $"{FixtureRoot}/kline_1m.update.json");
        Assert.Equal("kline_1m", kline.Channel);
        Assert.Equal("BYBIT_FUTURE_BTC_USDT", kline.Data.Symbol);
        Assert.Equal(95000m, kline.Data.OpenPrice);
        Assert.Equal(95050m, kline.Data.ClosePrice);
        Assert.Equal(1234.5m, kline.Data.Volume);
        Assert.True(kline.Data.IsClosed);
    }

    [Fact]
    public void CrossEx_public_order_book_stream_updates_deserialize()
    {
        var snapshot = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamOrderBook>>(
            $"{FixtureRoot}/order_book_5.update.json");
        Assert.Equal("order_book_5", snapshot.Channel);
        Assert.Equal("BINANCE_SPOT_BTC_USDT", snapshot.Data.Symbol);
        Assert.Equal(1771990690844, snapshot.Data.Timestamp);
        Assert.Equal(2, snapshot.Data.Asks.Count);
        Assert.Equal(65485.33000000m, snapshot.Data.Asks[0].Price);
        Assert.Equal(2.25708000m, snapshot.Data.Asks[0].Quantity);
        Assert.Equal(65484.12000000m, snapshot.Data.Bids[0].Price);

        var update = JsonFixture.Deserialize<GateStreamResponse<GateCrossExStreamOrderBookUpdate>>(
            $"{FixtureRoot}/order_book_update.update.json");
        Assert.Equal("order_book_update", update.Channel);
        Assert.False(update.Data.IsSnapshot);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", update.Data.Symbol);
        Assert.Equal(88686516370, update.Data.FirstUpdateId);
        Assert.Equal(88686516769, update.Data.LastUpdateId);
        Assert.Equal(65479.75000000m, update.Data.Bids[0].Price);
    }

    [Fact]
    public void CrossEx_private_user_stream_updates_deserialize()
    {
        var orders = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExOrder>>(
            $"{FixtureRoot}/order.update.json");
        Assert.Equal("order", orders.Channel);
        Assert.True(orders.Result.Success);
        Assert.Equal("2072652940337152", orders.Payload.OrderId);
        Assert.Equal("OKX_FUTURE_ADA_USDT", orders.Payload.Symbol);
        Assert.Equal(0.8499m, orders.Payload.Price);
        Assert.Equal(10m, orders.Payload.Quantity);
        Assert.False(orders.Payload.ReduceOnly);

        var asset = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExAccountAsset>>(
            $"{FixtureRoot}/asset.update.json");
        Assert.Equal("asset", asset.Channel);
        Assert.Equal("USDT", asset.Payload.Coin);
        Assert.Equal(9967.013209m, asset.Payload.Balance);
        Assert.Equal(9940.013209m, asset.Payload.AvailableBalance);

        var trade = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExTrade>>(
            $"{FixtureRoot}/usertrades.update.json");
        Assert.Equal("usertrades", trade.Channel);
        Assert.Equal(2072784922594048, trade.Payload.TransactionId);
        Assert.Equal("GATE_SPOT_ADA_USDT", trade.Payload.Symbol);
        Assert.Equal(13.36m, trade.Payload.Quantity);
        Assert.Equal(0.004008000000000000m, trade.Payload.Fee);

        var position = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExPosition>>(
            $"{FixtureRoot}/position.update.json");
        Assert.Equal("position", position.Channel);
        Assert.Equal(20087051449819136, position.Payload.PositionId);
        Assert.Equal("NONE", position.Payload.PositionSide);
        Assert.Equal(0.8499m, position.Payload.MarkPrice);
        Assert.Equal(12m, position.Payload.ClosedPnl);

        var marginPosition = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExMarginPosition>>(
            $"{FixtureRoot}/margin_position.update.json");
        Assert.Equal("margin_position", marginPosition.Channel);
        Assert.Equal("subscribe", marginPosition.Event);
        Assert.Equal(20116122196200192, marginPosition.Payload.PositionId);
        Assert.Equal("DOGE", marginPosition.Payload.AssetCoin);
        Assert.Equal(61.72130605m, marginPosition.Payload.AssetQuantity);
        Assert.Equal(10.00003424116m, marginPosition.Payload.Liability);

        var marginInterest = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExStreamMarginInterest>>(
            $"{FixtureRoot}/margin_interest.update.json");
        Assert.Equal("margin_interest", marginInterest.Channel);
        Assert.Equal(2101724189861376, marginInterest.Payload.InterestId);
        Assert.Equal(10m, marginInterest.Payload.Liability);
        Assert.Equal(0.00000343m, marginInterest.Payload.InterestRate);
        Assert.Equal("IMMEDIATE_OPEN_ORDER", marginInterest.Payload.InterestType);
    }

    [Fact]
    public void CrossEx_websocket_api_responses_deserialize()
    {
        var placeOrder = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExOrderActionResult>>(
            $"{FixtureRoot}/api.place_order.success.json");
        Assert.Equal("place_order", placeOrder.Channel);
        Assert.Equal("api", placeOrder.Event);
        Assert.Equal("2072652940337152", placeOrder.Payload.OrderId);
        Assert.True(placeOrder.Result.Success);

        var cancelOrder = JsonFixture.Deserialize<GateCrossExStreamResponse<JToken>>(
            $"{FixtureRoot}/api.cancel_order.success.json");
        Assert.Equal("cancel_order", cancelOrder.Channel);
        Assert.Null(cancelOrder.Payload);
        Assert.True(cancelOrder.Result.Success);

        var leverage = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExLeverageResult>>(
            $"{FixtureRoot}/api.set_leverage.success.json");
        Assert.Equal("set_leverage", leverage.Channel);
        Assert.Equal("GATE_FUTURE_ETH_USDT", leverage.Payload.Symbol);
        Assert.Equal(10m, leverage.Payload.Leverage);

        var marginLeverage = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExLeverageResult>>(
            $"{FixtureRoot}/api.set_margin_leverage.success.json");
        Assert.Equal("set_margin_leverage", marginLeverage.Channel);
        Assert.Equal("OKX_MARGIN_ADA_USDT", marginLeverage.Payload.Symbol);

        var accountUpdate = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExAccountUpdateResult>>(
            $"{FixtureRoot}/api.update_accounts.success.json");
        Assert.Equal("ISOLATED_EXCHANGE", accountUpdate.Payload.AccountMode);
        Assert.Equal("DUAL", accountUpdate.Payload.PositionMode);
        Assert.Equal("BINANCE", accountUpdate.Payload.ExchangeType);

        var closePosition = JsonFixture.Deserialize<GateCrossExStreamResponse<GateCrossExOrderActionResult>>(
            $"{FixtureRoot}/api.close_position.success.json");
        Assert.Equal("close_position", closePosition.Channel);
        Assert.Equal("2072652940337152", closePosition.Payload.OrderId);
        Assert.True(closePosition.Result.Success);
    }

    [Fact]
    public void CrossEx_stream_request_serialization_maps_public_private_and_api_payloads()
    {
        var publicRequest = new GateCrossExStreamRequest
        {
            Timestamp = 1754272114,
            Channel = "ticker",
            Event = "subscribe",
            Payload = new[] { "BINANCE_FUTURE_BTC_USDT" },
        };
        var publicJson = JObject.Parse(JsonConvert.SerializeObject(publicRequest));

        Assert.Equal(1754272114, publicJson["time"]!.Value<long>());
        Assert.Equal("subscribe", publicJson["event"]!.ToString());
        Assert.Equal("ticker", publicJson["channel"]!.ToString());
        Assert.Equal(["BINANCE_FUTURE_BTC_USDT"], publicJson["payload"]!.Values<string>().ToArray());

        var apiRequest = new GateCrossExStreamRequest
        {
            Timestamp = 1754272114,
            Channel = "place_order",
            Event = "api",
            Payload = new Dictionary<string, object>
            {
                { "symbol", "GATE_FUTURE_ETH_USDT" },
                { "side", "BUY" },
                { "qty", "0.01" },
                { "price", "2800" },
                { "type", "LIMIT" },
            },
        };
        var apiJson = JObject.Parse(JsonConvert.SerializeObject(apiRequest));

        Assert.Equal("api", apiJson["event"]!.ToString());
        Assert.Equal("place_order", apiJson["channel"]!.ToString());
        Assert.Equal("GATE_FUTURE_ETH_USDT", apiJson["payload"]!["symbol"]!.ToString());
        Assert.Equal("0.01", apiJson["payload"]!["qty"]!.ToString());
        Assert.Equal("LIMIT", apiJson["payload"]!["type"]!.ToString());
        Assert.Null(apiJson["auth"]);
    }

    [Fact]
    public async Task CrossEx_order_book_subscription_validation_matches_documented_values()
    {
        var client = new GateWebSocketClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.CrossEx.SubscribeToOrderBooksAsync(["BINANCE_FUTURE_BTC_USDT"], 2, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.CrossEx.SubscribeToOrderBooksAsync(["BINANCE_FUTURE_BTC_USDT"], 25, _ => { }));
    }
}
