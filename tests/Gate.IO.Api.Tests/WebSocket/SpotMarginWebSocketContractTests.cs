using Gate.IO.Api.Base;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class SpotMarginWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/Spot";

    [Fact]
    public void Spot_subscription_request_and_success_response_deserialize()
    {
        var request = JsonFixture.Deserialize<GateStreamRequest>($"{FixtureRoot}/request.subscribe_spot_orders.json");
        var payload = Assert.IsType<JArray>(request.Payload);

        Assert.Equal(123456789, request.Id);
        Assert.Equal(1611541000, request.Timestamp);
        Assert.Equal("spot.orders", request.Channel);
        Assert.Equal(StreamRequestEvent.Subscribe, request.Event);
        Assert.Equal(["BTC_USDT", "GT_USDT"], payload.Values<string>().ToArray());
        Assert.NotNull(request.Auth);
        Assert.Equal("api_key", request.Auth.Method);
        Assert.Equal("xxxx", request.Auth.ApiKey);
        Assert.Equal("xxxx", request.Auth.Signature);

        var response = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.subscribe_success.json");

        Assert.Equal(123456789, response.Id);
        Assert.Equal("spot.orders", response.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, response.Event);
        Assert.Equal("success", response.Data.Status);
        Assert.Null(response.Error);
    }

    [Fact]
    public void Spot_market_stream_updates_deserialize()
    {
        var ticker = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamTicker>>(
            $"{FixtureRoot}/spot.tickers.update.json");
        Assert.Equal("spot.tickers", ticker.Channel);
        Assert.Equal(StreamResponseEvent.Update, ticker.Event);
        Assert.Equal("BTC_USDT", ticker.Data.Symbol);
        Assert.Equal(18640.4m, ticker.Data.Last);
        Assert.Equal(27960600.12m, ticker.Data.QuoteVolume);

        var trade = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamTrade>>(
            $"{FixtureRoot}/spot.trades.update.json");
        Assert.Equal(309143071, trade.Data.Id);
        Assert.Equal(5736713, trade.Data.MarketId);
        Assert.Equal(GateSpotOrderSide.Sell, trade.Data.Side);
        Assert.Equal(16.4700000000m, trade.Data.Amount);
        Assert.Equal(0.4705000000m, trade.Data.Price);

        var candlestick = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamCandlestick>>(
            $"{FixtureRoot}/spot.candlesticks.update.json");
        Assert.Equal("1m_BTC_USDT", candlestick.Data.Subscription);
        Assert.True(candlestick.Data.IsClosed);
        Assert.Equal(19128.1m, candlestick.Data.Close);
        Assert.Equal(3.8283m, candlestick.Data.BaseVolume);
    }

    [Fact]
    public void Spot_order_book_stream_updates_deserialize()
    {
        var bookTicker = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamBookTicker>>(
            $"{FixtureRoot}/spot.book_ticker.update.json");
        Assert.Equal("BTC_USDT", bookTicker.Data.Symbol);
        Assert.Equal(48776306, bookTicker.Data.OrderBookUpdateId);
        Assert.Equal(19137.74m, bookTicker.Data.BestBidPrice);
        Assert.Equal(0.6135m, bookTicker.Data.BestAskAmount);

        var difference = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamBookDifference>>(
            $"{FixtureRoot}/spot.order_book_update.update.json");
        Assert.True(difference.Data.IsFullSnapshot);
        Assert.Equal("depthUpdate", difference.Data.EventName);
        Assert.Equal(48776301, difference.Data.OrderBookFirstUpdateId);
        Assert.Equal(2, difference.Data.Bids.Count);
        Assert.Equal(19137.74m, difference.Data.Bids[0].Price);

        var snapshot = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamBookSnapshot>>(
            $"{FixtureRoot}/spot.order_book.update.json");
        Assert.Equal(48791820, snapshot.Data.LastUpdateId);
        Assert.Equal("5", snapshot.Data.Level);
        Assert.Equal(2, snapshot.Data.Asks.Count);
        Assert.Equal(19080.24m, snapshot.Data.Asks[0].Price);

        var v2 = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamOrderBookV2Update>>(
            $"{FixtureRoot}/spot.obu.update.json");
        Assert.True(v2.Data.IsFullSnapshot);
        Assert.Equal("ob.BTC_USDT.50", v2.Data.Stream);
        Assert.Equal(73777715168, v2.Data.LastUpdateId);
        Assert.Equal(104027.2m, v2.Data.Asks[0].Price);
    }

    [Fact]
    public void Spot_user_stream_updates_deserialize()
    {
        var orders = JsonFixture.Deserialize<GateStreamResponse<List<GateSpotStreamOrderUpdate>>>(
            $"{FixtureRoot}/spot.orders.update.json");
        var order = Assert.Single(orders.Data);
        Assert.Equal(399123456, order.OrderId);
        Assert.Equal(GateSpotOrderUpdateEvent.Put, order.Event);
        Assert.Equal(GateSpotOrderType.Limit, order.Type);
        Assert.Equal(GateSpotOrderStatus.Open, order.Status);
        Assert.Equal(GateSpotOrderSide.Sell, order.Side);
        Assert.Equal(GateSpotFinishAs.Open, order.FinishAs);
        Assert.Equal(26253.3m, order.Price);

        var trades = JsonFixture.Deserialize<GateStreamResponse<List<GateSpotTradeHistory>>>(
            $"{FixtureRoot}/spot.usertrades.update.json");
        var trade = Assert.Single(trades.Data);
        Assert.Equal(5736713, trade.Id);
        Assert.Equal(1000001, trade.UserId);
        Assert.Equal(GateSpotOrderSide.Sell, trade.Side);
        Assert.Equal(GateSpotTraderRole.Taker, trade.Role);
        Assert.Equal(10000.00000000m, trade.Price);
        Assert.Equal(0.00200000000000m, trade.Fee);
    }

    [Fact]
    public void Spot_margin_balance_loan_and_price_order_updates_deserialize()
    {
        var spotBalance = JsonFixture.Deserialize<GateStreamResponse<List<GateSpotStreamUserBalance>>>(
            $"{FixtureRoot}/spot.balances.update.json");
        var spot = Assert.Single(spotBalance.Data);
        Assert.Equal(1000001, spot.UserId);
        Assert.Equal("USDT", spot.Currency);
        Assert.Equal(222244.3827652m, spot.Total);
        Assert.Equal(GateSpotBalanceChangeType.OrderCreate, spot.ChangeType);

        var marginBalance = JsonFixture.Deserialize<GateStreamResponse<List<GateSpotStreamMarginBalance>>>(
            $"{FixtureRoot}/spot.margin_balances.update.json");
        var margin = Assert.Single(marginBalance.Data);
        Assert.Equal("BTC_USDT", margin.Symbol);
        Assert.Equal("BTC", margin.Currency);
        Assert.Equal(-0.002m, margin.Change);

        var fundingBalance = JsonFixture.Deserialize<GateStreamResponse<List<GateSpotStreamFundingBalance>>>(
            $"{FixtureRoot}/spot.funding_balances.update.json");
        var funding = Assert.Single(fundingBalance.Data);
        Assert.Equal("USDT", funding.Currency);
        Assert.Equal(10.00000000m, funding.Lent);

        var crossBalance = JsonFixture.Deserialize<GateStreamResponse<List<GateSpotStreamCrossMarginBalance>>>(
            $"{FixtureRoot}/spot.cross_balances.update.json");
        var cross = Assert.Single(crossBalance.Data);
        Assert.Equal(222244.3827m, cross.Available);
        Assert.Equal(GateSpotBalanceChangeType.OrderCreate, cross.ChangeType);

        var loan = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamCrossMarginLoan>>(
            $"{FixtureRoot}/spot.cross_loan.update.json");
        Assert.Equal("BTC", loan.Data.Currency);
        Assert.Equal(0.01m, loan.Data.Borrowed);
        Assert.Equal(0.00001375m, loan.Data.Interest);

        var priceOrder = JsonFixture.Deserialize<GateStreamResponse<GateSpotStreamPriceOrder>>(
            $"{FixtureRoot}/spot.priceorders.update.json");
        Assert.Equal("METAN_USDT", priceOrder.Data.Market);
        Assert.Equal(247480109, priceOrder.Data.Id);
        Assert.Equal(GateSpotOrderType.Limit, priceOrder.Data.OrderType);
        Assert.Equal(GateSpotOrderSide.Buy, priceOrder.Data.Side);
        Assert.Equal(0.00302m, priceOrder.Data.TriggerPrice);
    }

    [Fact]
    public void Spot_stream_request_serialization_maps_payloads_and_auth()
    {
        var publicRequest = new GateStreamRequest
        {
            Id = 42,
            Timestamp = 1611541000,
            Channel = "spot.candlesticks",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "1m", "BTC_USDT" },
        };
        var publicJson = JObject.Parse(JsonConvert.SerializeObject(publicRequest));

        Assert.Equal("subscribe", publicJson["event"]!.ToString());
        Assert.Equal("spot.candlesticks", publicJson["channel"]!.ToString());
        Assert.Equal(["1m", "BTC_USDT"], publicJson["payload"]!.Values<string>().ToArray());
        Assert.Null(publicJson["auth"]);

        var privateRequest = new GateStreamRequest
        {
            Id = 43,
            Timestamp = 1611541000,
            Channel = "spot.orders",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "!all" },
            Auth = new StreamRequestAuth
            {
                ApiKey = "key",
                Signature = "signature",
            },
        };
        var privateJson = JObject.Parse(JsonConvert.SerializeObject(privateRequest));

        Assert.Equal("spot.orders", privateJson["channel"]!.ToString());
        Assert.Equal(["!all"], privateJson["payload"]!.Values<string>().ToArray());
        Assert.Equal("api_key", privateJson["auth"]!["method"]!.ToString());
        Assert.Equal("key", privateJson["auth"]!["KEY"]!.ToString());
        Assert.Equal("signature", privateJson["auth"]!["SIGN"]!.ToString());
    }

    [Fact]
    public async Task Spot_order_book_subscription_validation_matches_documented_values()
    {
        var client = new GateWebSocketClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Spot.SubscribeToOrderBookDifferencesAsync("BTC_USDT", 25, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Spot.SubscribeToOrderBookSnapshotsAsync("BTC_USDT", 200, 5, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Spot.SubscribeToOrderBookSnapshotsAsync("BTC_USDT", 100, 25, _ => { }));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Spot.SubscribeToOrderBookV2Async("BTC_USDT", 100, _ => { }));
    }
}
