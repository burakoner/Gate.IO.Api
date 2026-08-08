using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.TradFi;
using System.Text;

namespace Gate.IO.Api.Tests.TradFi;

[Trait("Category", "Unit")]
public class TradFiRequestConstructionTests
{
    [Fact]
    public async Task Public_tradfi_categories_request_omits_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/categories.success.json")));
        var client = CreateClient(handler);

        var result = await client.TradFi.GetSymbolCategoriesAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Single(result.Data!);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/tradfi/symbols/categories", request.RequestUri.AbsolutePath);
        AssertNoAuthHeaders(request);
    }

    [Fact]
    public async Task Public_tradfi_symbols_request_omits_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/symbols.success.json")));
        var client = CreateClient(handler);

        var result = await client.TradFi.GetSymbolsAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("EURUSD", Assert.Single(result.Data!).Symbol);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/tradfi/symbols", request.RequestUri.AbsolutePath);
        AssertNoAuthHeaders(request);
    }

    [Fact]
    public async Task Signed_tradfi_symbol_details_request_serializes_comma_separated_symbols()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/symbol_details.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.TradFi.GetSymbolDetailsAsync(["EURUSD", "XAGUSD"]);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/tradfi/symbols/detail", request.RequestUri.AbsolutePath);
        Assert.Equal("EURUSD,XAGUSD", ParseQuery(request.RequestUri)["symbols"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Public_tradfi_candlestick_request_serializes_interval_and_time_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/candlesticks.success.json")));
        var client = CreateClient(handler);

        var result = await client.TradFi.GetCandlesticksAsync(new GateTradFiCandlestickQueryRequest
        {
            Symbol = "EURUSD",
            Interval = GateTradFiKlineInterval.OneMinute,
            BeginTime = DateTimeOffset.FromUnixTimeSeconds(1755896400).UtcDateTime,
            EndTime = DateTimeOffset.FromUnixTimeSeconds(1756069200).UtcDateTime,
            Limit = 2,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/tradfi/symbols/EURUSD/klines", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("1m", query["kline_type"]);
        Assert.Equal("1755896400", query["begin_time"]);
        Assert.Equal("1756069200", query["end_time"]);
        Assert.Equal("2", query["limit"]);
        AssertNoAuthHeaders(request);
    }

    [Fact]
    public async Task Public_tradfi_ticker_request_omits_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/ticker.success.json")));
        var client = CreateClient(handler);

        var result = await client.TradFi.GetTickerAsync("EURUSD");

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(5074.19m, result.Data!.LastPrice);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/tradfi/symbols/EURUSD/tickers", request.RequestUri.AbsolutePath);
        AssertNoAuthHeaders(request);
    }

    [Fact]
    public async Task Signed_tradfi_transaction_query_serializes_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/transactions.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.TradFi.GetTransactionsAsync(new GateTradFiTransactionQueryRequest
        {
            BeginTime = DateTimeOffset.FromUnixTimeSeconds(1769238545).UtcDateTime,
            EndTime = DateTimeOffset.FromUnixTimeSeconds(1769329389).UtcDateTime,
            Type = GateTradFiTransactionType.Dividend,
            Page = 1,
            PageSize = 10,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/tradfi/transactions", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("1769238545", query["begin_time"]);
        Assert.Equal("1769329389", query["end_time"]);
        Assert.Equal("dividend", query["type"]);
        Assert.Equal("1", query["page"]);
        Assert.Equal("10", query["page_size"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_tradfi_transaction_create_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/transaction_create.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.TradFi.CreateTransactionAsync(new GateTradFiTransactionRequest
        {
            Asset = "USDT",
            Change = 10m,
            Type = GateTradFiTransactionType.Withdraw,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/tradfi/transactions", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("USDT", body["asset"]!.ToString());
        Assert.Equal("10", body["change"]!.ToString());
        Assert.Equal("withdraw", body["type"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_tradfi_order_request_serializes_documented_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/order_id.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.TradFi.PlaceOrderAsync(new GateTradFiOrderRequest
        {
            Symbol = "EURUSD",
            Side = GateTradFiOrderSide.Buy,
            PriceType = GateTradFiOrderPriceType.Trigger,
            Price = 0.9m,
            Volume = 10m,
            TakeProfitPrice = 1.5m,
            StopLossPrice = 0.8m,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(117, result.Data!.Id);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/tradfi/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("EURUSD", body["symbol"]!.ToString());
        Assert.Equal(2, body["side"]!.Value<int>());
        Assert.Equal("trigger", body["price_type"]!.ToString());
        Assert.Equal("0.9", body["price"]!.ToString());
        Assert.Equal("10", body["volume"]!.ToString());
        Assert.Equal("1.5", body["price_tp"]!.ToString());
        Assert.Equal("0.8", body["price_sl"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_tradfi_order_history_request_serializes_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/TradFi/order_history.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.TradFi.GetOrderHistoryAsync(new GateTradFiOrderHistoryQueryRequest
        {
            BeginTime = DateTimeOffset.FromUnixTimeSeconds(1769397512).UtcDateTime,
            EndTime = DateTimeOffset.FromUnixTimeSeconds(1769397522).UtcDateTime,
            Symbol = "USDCAD",
            Side = GateTradFiOrderSide.Buy,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/tradfi/orders/history", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("1769397512", query["begin_time"]);
        Assert.Equal("1769397522", query["end_time"]);
        Assert.Equal("USDCAD", query["symbol"]);
        Assert.Equal("2", query["side"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_tradfi_update_and_cancel_order_requests_target_order_resource()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/TradFi/order_update.success.json"),
            JsonFixture.Read("Docs/TradFi/cancel.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var update = await client.TradFi.UpdateOrderAsync(2651172, new GateTradFiOrderUpdateRequest
        {
            Price = 2m,
            TakeProfitPrice = 1.5m,
            StopLossPrice = 0.8m,
        });
        var cancel = await client.TradFi.CancelOrderAsync(2651172);

        Assert.True(update.Success, update.Error?.ToString());
        Assert.True(cancel.Success, cancel.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);
        Assert.Equal(HttpMethod.Put, handler.Requests[0].Method);
        Assert.Equal("/api/v4/tradfi/orders/2651172", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("2", JObject.Parse(handler.Requests[0].Content)["price"]!.ToString());
        Assert.Equal(HttpMethod.Delete, handler.Requests[1].Method);
        Assert.Equal("/api/v4/tradfi/orders/2651172", handler.Requests[1].RequestUri.AbsolutePath);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
    }

    [Fact]
    public async Task Signed_tradfi_position_requests_serialize_mapped_filters_and_bodies()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/TradFi/position_history.success.json"),
            JsonFixture.Read("Docs/TradFi/position_update.success.json"),
            JsonFixture.Read("Docs/TradFi/position_close.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var history = await client.TradFi.GetPositionHistoryAsync(new GateTradFiPositionHistoryQueryRequest
        {
            Page = 2,
            PageSize = 50,
            BeginTime = DateTimeOffset.FromUnixTimeSeconds(1769223566).UtcDateTime,
            EndTime = DateTimeOffset.FromUnixTimeSeconds(1769223573).UtcDateTime,
            Symbol = "EURUSD",
            Direction = GateTradFiPositionDirection.Long,
        });
        var update = await client.TradFi.UpdatePositionAsync(2648854, new GateTradFiPositionUpdateRequest
        {
            TakeProfitPrice = 1m,
            StopLossPrice = 1m,
        });
        var close = await client.TradFi.ClosePositionAsync(2648854, new GateTradFiClosePositionRequest
        {
            CloseType = 1,
            CloseVolume = 0.1m,
        });

        Assert.True(history.Success, history.Error?.ToString());
        Assert.Equal(1, history.Data!.Total);
        Assert.Equal(1, history.Data.TotalPage);
        Assert.Single(history.Data.List);
        Assert.True(update.Success, update.Error?.ToString());
        Assert.True(close.Success, close.Error?.ToString());
        Assert.Equal(3, handler.Requests.Count);

        var historyQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("2", historyQuery["page"]);
        Assert.Equal("50", historyQuery["page_size"]);
        Assert.Equal("Long", historyQuery["position_dir"]);
        Assert.Equal("EURUSD", historyQuery["symbol"]);
        Assert.Equal("/api/v4/tradfi/positions/2648854", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("1", JObject.Parse(handler.Requests[1].Content)["price_tp"]!.ToString());
        Assert.Equal("/api/v4/tradfi/positions/2648854/close", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal(1, JObject.Parse(handler.Requests[2].Content)["close_type"]!.Value<int>());
        Assert.Equal("0.1", JObject.Parse(handler.Requests[2].Content)["close_volume"]!.ToString());
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
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
