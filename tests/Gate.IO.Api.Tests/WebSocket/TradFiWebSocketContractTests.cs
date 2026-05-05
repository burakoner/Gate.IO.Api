using Gate.IO.Api.Base;
using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.TradFi;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class TradFiWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/TradFi";

    [Fact]
    public void TradFi_subscription_requests_and_success_response_deserialize()
    {
        var publicRequest = JsonFixture.Deserialize<GateStreamRequest>($"{FixtureRoot}/request.subscribe_tradfi_tickers.json");
        var publicPayload = Assert.IsType<JObject>(publicRequest.Payload);

        Assert.Equal(3001, publicRequest.Id);
        Assert.Equal(1768362181, publicRequest.Timestamp);
        Assert.Equal("tradfi.tickers", publicRequest.Channel);
        Assert.Equal(StreamRequestEvent.Subscribe, publicRequest.Event);
        Assert.Equal(["XAUUSD"], publicPayload["markets"]!.Values<string>().ToArray());
        Assert.Null(publicRequest.Auth);

        var privateRequest = JsonFixture.Deserialize<GateStreamRequest>($"{FixtureRoot}/request.subscribe_tradfi_orders.json");
        var privatePayload = Assert.IsType<JArray>(privateRequest.Payload);

        Assert.Equal(3002, privateRequest.Id);
        Assert.Equal("tradfi.orders", privateRequest.Channel);
        Assert.Empty(privatePayload);
        Assert.Equal("api_key", privateRequest.Auth.Method);
        Assert.Equal("xxxx", privateRequest.Auth.ApiKey);
        Assert.Equal("xxxx", privateRequest.Auth.Signature);

        var response = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.subscribe_success.json");

        Assert.Equal(3002, response.Id);
        Assert.Equal("tradfi.orders", response.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, response.Event);
        Assert.Null(response.Error);
        Assert.Equal("success", response.Data.Status);
    }

    [Fact]
    public void TradFi_public_market_stream_updates_deserialize()
    {
        var tickers = JsonFixture.Deserialize<GateStreamResponse<List<GateTradFiStreamTicker>>>(
            $"{FixtureRoot}/tradfi.tickers.update.json");
        var ticker = Assert.Single(tickers.Data);
        Assert.Equal("XAUUSD", ticker.Symbol);
        Assert.Equal(4587.16m, ticker.OpenPrice);
        Assert.Equal(4622.95m, ticker.LastPrice);
        Assert.Equal(35.79m, ticker.PriceChangeAmount);
        Assert.Equal(0.78m, ticker.PriceChangeRate);
        Assert.Equal(4625.59m, ticker.High);
        Assert.Equal(4587.07m, ticker.Low);

        var candlesticks = JsonFixture.Deserialize<GateStreamResponse<List<GateTradFiStreamCandlestick>>>(
            $"{FixtureRoot}/tradfi.candlesticks.update.json");
        var candlestick = Assert.Single(candlesticks.Data);
        Assert.Equal("1d_XAUUSD", candlestick.Subscription);
        Assert.Equal(0m, candlestick.Volume);
        Assert.Equal(4627.14m, candlestick.Close);
        Assert.Equal(4627.31m, candlestick.High);
        Assert.Equal(4587.07m, candlestick.Low);
        Assert.Equal(4587.16m, candlestick.Open);
        Assert.Equal(0m, candlestick.Amount);
        Assert.False(candlestick.IsClosed);

        var orderBooks = JsonFixture.Deserialize<GateStreamResponse<List<GateTradFiStreamOrderBookTicker>>>(
            $"{FixtureRoot}/tradfi.order_book.update.json");
        var orderBook = Assert.Single(orderBooks.Data);
        Assert.Equal("XAUUSD", orderBook.Symbol);
        Assert.Equal(4633.24m, orderBook.Bid);
        Assert.Equal(4633.33m, orderBook.Ask);
    }

    [Fact]
    public void TradFi_private_user_stream_updates_deserialize()
    {
        var orders = JsonFixture.Deserialize<GateStreamResponse<List<GateTradFiStreamOrder>>>(
            $"{FixtureRoot}/tradfi.orders.update.json");
        var order = Assert.Single(orders.Data);
        Assert.Equal(2536849, order.OrderId);
        Assert.Equal(2124580619, order.GateUserId);
        Assert.Equal("NZDSEK", order.Symbol);
        Assert.Equal(GateTradFiOrderSide.Buy, order.Side);
        Assert.Equal(0.01m, order.Volume);
        Assert.Equal(0m, order.FillVolume);
        Assert.Equal(5.29483m, order.Price);
        Assert.Equal(0m, order.TakeProfitPrice);
        Assert.Equal(0m, order.StopLossPrice);
        Assert.False(order.Finished);
        Assert.Equal("-", order.FinishedAs);
        Assert.Equal(GateTradFiStreamOrderOperationType.Sell, order.OperationType);

        var positions = JsonFixture.Deserialize<GateStreamResponse<List<GateTradFiStreamPosition>>>(
            $"{FixtureRoot}/tradfi.position.update.json");
        var position = Assert.Single(positions.Data);
        Assert.Equal(2536849, position.PositionId);
        Assert.Equal(2124580619, position.GateUserId);
        Assert.Equal(GateTradFiStreamPositionSide.Long, position.Side);
        Assert.Equal("NZDSEK", position.Symbol);
        Assert.Equal(0.01m, position.Volume);
        Assert.Equal(5.29483m, position.OpenPrice);
        Assert.Equal(0m, position.TakeProfitPrice);
        Assert.Equal(0m, position.StopLossPrice);

        var balances = JsonFixture.Deserialize<GateStreamResponse<List<GateTradFiStreamBalance>>>(
            $"{FixtureRoot}/tradfi.balance.update.json");
        var balance = Assert.Single(balances.Data);
        Assert.Equal(84776, balance.DealId);
        Assert.Equal(2124580619, balance.GateUserId);
        Assert.Equal(-20m, balance.Change);
        Assert.Equal(string.Empty, balance.Comment);
    }

    [Fact]
    public void TradFi_stream_request_serialization_maps_payloads_and_auth()
    {
        var tickerRequest = new GateStreamRequest
        {
            Id = 62,
            Timestamp = 1768362181,
            Channel = "tradfi.tickers",
            Event = StreamRequestEvent.Subscribe,
            Payload = new
            {
                markets = new[] { "XAUUSD" }
            },
        };
        var tickerJson = JObject.Parse(JsonConvert.SerializeObject(tickerRequest));

        Assert.Equal("subscribe", tickerJson["event"]!.ToString());
        Assert.Equal("tradfi.tickers", tickerJson["channel"]!.ToString());
        Assert.Equal(["XAUUSD"], tickerJson["payload"]!["markets"]!.Values<string>().ToArray());
        Assert.Null(tickerJson["auth"]);

        var candlestickRequest = new GateStreamRequest
        {
            Id = 63,
            Timestamp = 1768362860,
            Channel = "tradfi.candlesticks",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "1d", "XAUUSD" },
        };
        var candlestickJson = JObject.Parse(JsonConvert.SerializeObject(candlestickRequest));

        Assert.Equal(["1d", "XAUUSD"], candlestickJson["payload"]!.Values<string>().ToArray());

        var orderBookRequest = new GateStreamRequest
        {
            Id = 64,
            Timestamp = 1768372119,
            Channel = "tradfi.order_book",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "XAUUSD" },
        };
        var orderBookJson = JObject.Parse(JsonConvert.SerializeObject(orderBookRequest));

        Assert.Equal(["XAUUSD"], orderBookJson["payload"]!.Values<string>().ToArray());

        var privateRequest = new GateStreamRequest
        {
            Id = 65,
            Timestamp = 1768373164,
            Channel = "tradfi.orders",
            Event = StreamRequestEvent.Subscribe,
            Payload = Array.Empty<string>(),
            Auth = new StreamRequestAuth
            {
                ApiKey = "key",
                Signature = "signature",
            },
        };
        var privateJson = JObject.Parse(JsonConvert.SerializeObject(privateRequest));

        Assert.Empty(privateJson["payload"]!.Values<string>());
        Assert.Equal("api_key", privateJson["auth"]!["method"]!.ToString());
        Assert.Equal("key", privateJson["auth"]!["KEY"]!.ToString());
        Assert.Equal("signature", privateJson["auth"]!["SIGN"]!.ToString());
    }
}
