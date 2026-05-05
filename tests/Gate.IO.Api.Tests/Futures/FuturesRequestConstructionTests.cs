using Gate.IO.Api.Futures;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Unit")]
public class FuturesRequestConstructionTests
{
    [Fact]
    public async Task Public_futures_contracts_request_serializes_query_without_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/contracts.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetContractsAsync(limit: 1, offset: 2);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/contracts", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("1", query["limit"]);
        Assert.Equal("2", query["offset"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_futures_order_book_request_serializes_depth_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/order_book.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetOrderBookAsync("BTC_USDT", interval: 0m, limit: 5, withId: true);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/order_book", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["contract"]);
        Assert.Equal("0", query["interval"]);
        Assert.Equal("5", query["limit"]);
        Assert.Equal("true", query["with_id"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_futures_batch_funding_rate_request_accepts_documented_nested_response()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/funding_rates.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetBatchFundingRateHistoryAsync(new GateFuturesBatchFundingRateRequest
        {
            Contracts = ["BTC_USDT"],
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Single(result.Data!);
        Assert.Equal("BTC_USDT", result.Data![0].Contract);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/funding_rates", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT", body["contracts"]![0]!.ToString());
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_futures_risk_limit_tiers_request_omits_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/risk_limit_tiers.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetRiskLimitTiersAsync("BTC_USDT", limit: 1, offset: 0);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/risk_limit_tiers", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["contract"]);
        Assert.Equal("1", query["limit"]);
        Assert.Equal("0", query["offset"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_futures_order_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.PlaceOrderAsync(new GateFuturesOrderRequest
        {
            Contract = "BTC_USDT",
            Size = 1,
            Price = 65000m,
            TimeInForce = GateFuturesTimeInForce.GoodTillCancelled,
            ClientOrderId = "t-test",
            ReduceOnly = false,
            Close = false,
            SelfTradeAction = GateFuturesSelfTradeAction.CancelNewest,
            MarketOrderSlipRatio = 0.01m,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT", body["contract"]!.ToString());
        Assert.Equal("1", body["size"]!.ToString());
        Assert.Equal("65000", body["price"]!.ToString());
        Assert.Equal("gtc", body["tif"]!.ToString());
        Assert.Equal("t-test", body["text"]!.ToString());
        Assert.Equal("cn", body["stp_act"]!.ToString());
        Assert.Equal("0.01", body["market_order_slip_ratio"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_price_triggered_order_request_serializes_documented_numeric_trigger_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/price_order_id.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest
        {
            Type = GateFuturesTriggerType.CloseLongOrder,
            Order = new GateFuturesInitial
            {
                Contract = "BTC_USDT",
                Size = 100,
                Price = "5.03",
                Close = false,
                TimeInForce = GateFuturesTimeInForce.GoodTillCancelled,
                ClientOrderId = "t-test",
                ReduceOnly = false,
                AutoSize = GateFuturesOrderAutoSize.CloseLong,
            },
            Trigger = new GateFuturesTrigger
            {
                StrategyType = GateFuturesTriggerStrategy.ByPrice,
                PriceType = GateFuturesTriggerPrice.DealPrice,
                Price = "3000",
                Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo,
                Expiration = 86400,
            },
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1283293, result.Data);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/price_orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("close-long-order", body["order_type"]!.ToString());
        Assert.Equal("BTC_USDT", body["initial"]!["contract"]!.ToString());
        Assert.Equal("gtc", body["initial"]!["tif"]!.ToString());
        Assert.Equal("close_long", body["initial"]!["auto_size"]!.ToString());
        Assert.Equal(0, body["trigger"]!["strategy_type"]!.Value<int>());
        Assert.Equal(0, body["trigger"]!["price_type"]!.Value<int>());
        Assert.Equal(1, body["trigger"]!["rule"]!.Value<int>());
        AssertSignedHeaders(request);
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

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}
