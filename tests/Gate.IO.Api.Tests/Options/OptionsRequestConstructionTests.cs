using Gate.IO.Api.Options;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Options;

[Trait("Category", "Unit")]
public class OptionsRequestConstructionTests
{
    [Fact]
    public async Task Public_options_contracts_request_serializes_query_without_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Options/contracts.success.json")));
        var client = CreateClient(handler);

        var result = await client.Options.GetContractsAsync(new GateOptionsContractQueryRequest
        {
            Underlying = "BTC_USDT",
            Expiration = 1637913600,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/options/contracts", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["underlying"]);
        Assert.Equal("1637913600", query["expiration"]);
        AssertNoAuthHeaders(request);
    }

    [Fact]
    public async Task Public_options_order_book_request_serializes_depth_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Options/order_book.success.json")));
        var client = CreateClient(handler);

        var result = await client.Options.GetOrderBookAsync(new GateOptionsOrderBookRequest
        {
            Contract = "BTC_USDT-20210916-5000-C",
            Interval = 0.1m,
            Limit = 5,
            WithId = true,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/options/order_book", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT-20210916-5000-C", query["contract"]);
        Assert.Equal("0.1", query["interval"]);
        Assert.Equal("5", query["limit"]);
        Assert.Equal("true", query["with_id"]);
        AssertNoAuthHeaders(request);
    }

    [Fact]
    public async Task Public_options_candlestick_and_trade_requests_serialize_mapped_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Options/candlesticks.success.json"),
            JsonFixture.Read("Docs/Options/underlying_candlesticks.success.json"),
            JsonFixture.Read("Docs/Options/trades.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);

        var from = DateTimeOffset.FromUnixTimeSeconds(1539852480).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1539856080).UtcDateTime;
        var candlesticks = await client.Options.GetCandlesticksAsync(new GateOptionsCandlestickQueryRequest
        {
            Contract = "BTC_USDT-20210916-5000-C",
            Interval = GateOptionsCandlestickInterval.OneMinute,
            From = from,
            To = to,
            Limit = 1,
        });
        var underlyingCandlesticks = await client.Options.GetUnderlyingCandlesticksAsync(new GateOptionsUnderlyingCandlestickQueryRequest
        {
            Underlying = "BTC_USDT",
            Interval = GateOptionsCandlestickInterval.FiveMinutes,
            From = from,
            To = to,
            Limit = 1,
        });
        var trades = await client.Options.GetTradesAsync(new GateOptionsTradeQueryRequest
        {
            Contract = "BTC_USDT-20210916-5000-C",
            Type = GateOptionsType.Call,
            From = from,
            To = to,
            Limit = 1,
            Offset = 2,
        });

        Assert.True(candlesticks.Success, candlesticks.Error?.ToString());
        Assert.True(underlyingCandlesticks.Success, underlyingCandlesticks.Error?.ToString());
        Assert.True(trades.Success, trades.Error?.ToString());
        Assert.Equal(3, handler.Requests.Count);

        var contractKlineQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("1m", contractKlineQuery["interval"]);
        Assert.Equal("1539852480", contractKlineQuery["from"]);
        Assert.Equal("1539856080", contractKlineQuery["to"]);
        var underlyingKlineQuery = ParseQuery(handler.Requests[1].RequestUri);
        Assert.Equal("5m", underlyingKlineQuery["interval"]);
        Assert.Equal("BTC_USDT", underlyingKlineQuery["underlying"]);
        var tradesQuery = ParseQuery(handler.Requests[2].RequestUri);
        Assert.Equal("C", tradesQuery["type"]);
        Assert.Equal("1", tradesQuery["limit"]);
        Assert.Equal("2", tradesQuery["offset"]);
        AssertNoAuthHeaders(handler.Requests[0]);
        AssertNoAuthHeaders(handler.Requests[1]);
        AssertNoAuthHeaders(handler.Requests[2]);
    }

    [Fact]
    public async Task Signed_options_account_book_request_serializes_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Options/account_book.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Options.GetBalanceHistoryAsync(new GateOptionsBalanceHistoryQueryRequest
        {
            Type = GateOptionsBalanceChangeType.Fee,
            From = DateTimeOffset.FromUnixTimeSeconds(1636426005).UtcDateTime,
            To = DateTimeOffset.FromUnixTimeSeconds(1636427005).UtcDateTime,
            Limit = 10,
            Offset = 1,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/options/account_book", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("fee", query["type"]);
        Assert.Equal("1636426005", query["from"]);
        Assert.Equal("1636427005", query["to"]);
        Assert.Equal("10", query["limit"]);
        Assert.Equal("1", query["offset"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_options_position_requests_serialize_resources()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Options/positions.success.json"),
            JsonFixture.Read("Docs/Options/position.success.json"),
            JsonFixture.Read("Docs/Options/user_liquidations.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var positions = await client.Options.GetUnderlyingPositionsAsync(new GateOptionsPositionQueryRequest { Underlying = "BTC_USDT" });
        var position = await client.Options.GetContractPositionAsync("BTC_USDT-20211216-5000-P");
        var liquidations = await client.Options.GetUserLiquidationsAsync(new GateOptionsUserLiquidationQueryRequest
        {
            Underlying = "BTC_USDT",
            Contract = "BTC_USDT-20211216-5000-P",
        });

        Assert.True(positions.Success, positions.Error?.ToString());
        Assert.True(position.Success, position.Error?.ToString());
        Assert.True(liquidations.Success, liquidations.Error?.ToString());
        Assert.Equal("/api/v4/options/positions", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("BTC_USDT", ParseQuery(handler.Requests[0].RequestUri)["underlying"]);
        Assert.Equal("/api/v4/options/positions/BTC_USDT-20211216-5000-P", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/options/position_close", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("BTC_USDT-20211216-5000-P", ParseQuery(handler.Requests[2].RequestUri)["contract"]);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
    }

    [Fact]
    public async Task Signed_options_order_request_accepts_documented_multi_digit_strike_contract()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Options/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Options.PlaceOrderAsync(new GateOptionsOrderRequest
        {
            Contract = "BTC_USDT-20210916-5000-C",
            Size = -1,
            Iceberg = 0,
            Price = 100m,
            Close = false,
            ReduceOnly = false,
            Mmp = false,
            TimeInForce = GateOptionsTimeInForce.GoodTillCancelled,
            ClientOrderId = "t-test",
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/options/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT-20210916-5000-C", body["contract"]!.ToString());
        Assert.Equal("-1", body["size"]!.ToString());
        Assert.Equal("100", body["price"]!.ToString());
        Assert.Equal("gtc", body["tif"]!.ToString());
        Assert.Equal("t-test", body["text"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_options_order_query_and_cancel_requests_serialize_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Options/orders.success.json"),
            JsonFixture.Read("Docs/Options/orders.success.json"),
            JsonFixture.Read("Docs/Options/order.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var orders = await client.Options.GetOrdersAsync(new GateOptionsOrderQueryRequest
        {
            Status = GateOptionsOrderStatus.Finished,
            Underlying = "BTC_USDT",
            Contract = "BTC_USDT-20210916-5000-C",
            From = DateTimeOffset.FromUnixTimeSeconds(1631763361).UtcDateTime,
            To = DateTimeOffset.FromUnixTimeSeconds(1631763397).UtcDateTime,
            Limit = 10,
            Offset = 1,
        });
        var cancelAll = await client.Options.CancelOrdersAsync(new GateOptionsCancelOrdersRequest
        {
            Underlying = "BTC_USDT",
            Contract = "BTC_USDT-20210916-5000-C",
            Side = GateOptionsOrderSide.Ask,
        });
        var cancelOne = await client.Options.CancelOrderAsync(2);

        Assert.True(orders.Success, orders.Error?.ToString());
        Assert.True(cancelAll.Success, cancelAll.Error?.ToString());
        Assert.True(cancelOne.Success, cancelOne.Error?.ToString());
        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("finished", ParseQuery(handler.Requests[0].RequestUri)["status"]);
        Assert.Equal("BTC_USDT-20210916-5000-C", ParseQuery(handler.Requests[0].RequestUri)["contract"]);
        Assert.Equal(HttpMethod.Delete, handler.Requests[1].Method);
        Assert.Equal("ask", ParseQuery(handler.Requests[1].RequestUri)["side"]);
        Assert.Equal("/api/v4/options/orders/2", handler.Requests[2].RequestUri.AbsolutePath);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
    }

    [Fact]
    public async Task Signed_options_amend_countdown_user_trade_and_mmp_requests_serialize_bodies()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Options/order.success.json"),
            JsonFixture.Read("Docs/Options/countdown_cancel_all.success.json"),
            JsonFixture.Read("Docs/Options/user_trades.success.json"),
            JsonFixture.Read("Docs/Options/mmp.success.json"),
            JsonFixture.Read("Docs/Options/mmp_list.success.json"),
            JsonFixture.Read("Docs/Options/mmp.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var amend = await client.Options.AmendOrderAsync(2, new GateOptionsOrderUpdateRequest
        {
            Contract = "BTC_USDT-20210916-5000-C",
            Price = 101m,
            Size = -1,
        });
        var countdown = await client.Options.CancelAllAsync(new GateOptionsCountdownCancelAllRequest
        {
            Timeout = 5,
            Contract = "BTC_USDT-20210916-5000-C",
            Underlying = "BTC_USDT",
        });
        var userTrades = await client.Options.GetUserTradesAsync(new GateOptionsUserTradeQueryRequest
        {
            Underlying = "BTC_USDT",
            Contract = "BTC_USDT-20211130-65000-C",
            Limit = 10,
            Offset = 0,
        });
        var setMmp = await client.Options.SetMMPAsync(new GateOptionsMMPRequest
        {
            Underlying = "BTC_USDT",
            Window = 5000,
            FrozenPeriod = 200,
            QuantityLimit = 10m,
            DeltaLimit = 10m,
        });
        var getMmp = await client.Options.GetMMPAsync("BTC_USDT");
        var resetMmp = await client.Options.ResetMMPAsync("BTC_USDT");

        Assert.True(amend.Success, amend.Error?.ToString());
        Assert.True(countdown.Success, countdown.Error?.ToString());
        Assert.True(userTrades.Success, userTrades.Error?.ToString());
        Assert.True(setMmp.Success, setMmp.Error?.ToString());
        Assert.True(getMmp.Success, getMmp.Error?.ToString());
        Assert.True(resetMmp.Success, resetMmp.Error?.ToString());
        Assert.Equal(6, handler.Requests.Count);

        Assert.Equal(HttpMethod.Put, handler.Requests[0].Method);
        Assert.Equal("101", JObject.Parse(handler.Requests[0].Content)["price"]!.ToString());
        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        Assert.Equal("5", JObject.Parse(handler.Requests[1].Content)["timeout"]!.ToString());
        Assert.Equal(HttpMethod.Get, handler.Requests[2].Method);
        Assert.Equal("BTC_USDT-20211130-65000-C", ParseQuery(handler.Requests[2].RequestUri)["contract"]);
        Assert.Equal(HttpMethod.Post, handler.Requests[3].Method);
        Assert.Equal("10", JObject.Parse(handler.Requests[3].Content)["qty_limit"]!.ToString());
        Assert.Equal(HttpMethod.Get, handler.Requests[4].Method);
        Assert.Equal("BTC_USDT", ParseQuery(handler.Requests[4].RequestUri)["underlying"]);
        Assert.Equal(HttpMethod.Post, handler.Requests[5].Method);
        Assert.Equal("/api/v4/options/mmp/reset", handler.Requests[5].RequestUri.AbsolutePath);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
        AssertSignedHeaders(handler.Requests[3]);
        AssertSignedHeaders(handler.Requests[4]);
        AssertSignedHeaders(handler.Requests[5]);
    }

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
        => new(new GateRestApiClientOptions
        {
            HttpClient = new HttpClient(handler),
        });

    private static HttpResponseMessage JsonResponse(string json)
        => new(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

    private static Dictionary<string, string> ParseQuery(Uri uri)
    {
        return uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .ToDictionary(
                x => Uri.UnescapeDataString(x[0]),
                x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]));
    }

    private static void AssertNoAuthHeaders(RecordedHttpRequest request)
    {
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}
