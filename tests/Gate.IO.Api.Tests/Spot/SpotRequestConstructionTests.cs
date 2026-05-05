using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Spot;

[Trait("Category", "Unit")]
public class SpotRequestConstructionTests
{
    [Fact]
    public async Task Public_spot_tickers_request_serializes_query_without_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/tickers.success.json")));
        var client = CreateClient(handler);

        var result = await client.Spot.GetTickersAsync("BTC_USDT", GateSpotTickerTimezone.UTC0);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/spot/tickers", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["currency_pair"]);
        Assert.Equal("utc0", query["timezone"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_spot_insurance_history_request_is_unsigned()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/insurance_history.success.json")));
        var client = CreateClient(handler);

        var result = await client.Spot.GetInsuranceHistoryAsync(new GateSpotInsuranceHistoryRequest
        {
            Business = "margin",
            Currency = "BTC",
            From = new DateTime(2024, 9, 23, 8, 2, 27, DateTimeKind.Utc),
            To = new DateTime(2024, 9, 23, 8, 2, 27, DateTimeKind.Utc),
            Page = 1,
            Limit = 1,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/spot/insurance_history", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("margin", query["business"]);
        Assert.Equal("BTC", query["currency"]);
        Assert.Equal("1727078547", query["from"]);
        Assert.Equal("1727078547", query["to"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_spot_order_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.PlaceOrderAsync(new GateSpotOrderRequest
        {
            ClientOrderId = "t-test",
            Symbol = "BTC_USDT",
            Type = GateSpotOrderType.Limit,
            Account = GateSpotAccountType.Unified,
            Side = GateSpotOrderSide.Buy,
            Amount = 0.001m,
            Price = 65000m,
            TimeInForce = GateSpotTimeInForce.GoodTillCancelled,
            Iceberg = 0m,
            Slippage = 0.05m,
            AutoBorrow = false,
            AutoRepay = false,
            SelfTradeAction = GateSpotSelfTradeAction.CancelNewest,
            ActionMode = GateSpotActionMode.Full,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/spot/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("t-test", body["text"]!.ToString());
        Assert.Equal("BTC_USDT", body["currency_pair"]!.ToString());
        Assert.Equal("limit", body["type"]!.ToString());
        Assert.Equal("unified", body["account"]!.ToString());
        Assert.Equal("buy", body["side"]!.ToString());
        Assert.Equal("0.001", body["amount"]!.ToString());
        Assert.Equal("65000", body["price"]!.ToString());
        Assert.Equal("gtc", body["time_in_force"]!.ToString());
        Assert.Equal("0.05", body["slippage"]!.ToString());
        Assert.Equal("cn", body["stp_act"]!.ToString());
        Assert.Equal("FULL", body["action_mode"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_spot_order_query_serializes_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse($"[{JsonFixture.Read("Docs/Spot/order.success.json")}]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.GetOrdersAsync(new GateSpotOrderQueryRequest
        {
            Symbol = "BTC_USDT",
            Status = GateSpotOrderQueryStatus.Finished,
            Account = GateSpotAccountType.CrossMargin,
            Side = GateSpotOrderSide.Sell,
            From = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc),
            To = new DateTime(2024, 1, 3, 3, 4, 5, DateTimeKind.Utc),
            Page = 2,
            Limit = 50,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/spot/orders", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["currency_pair"]);
        Assert.Equal("finished", query["status"]);
        Assert.Equal("cross_margin", query["account"]);
        Assert.Equal("sell", query["side"]);
        Assert.Equal("1704164645", query["from"]);
        Assert.Equal("1704251045", query["to"]);
        Assert.Equal("2", query["page"]);
        Assert.Equal("50", query["limit"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_spot_price_triggered_order_request_serializes_nested_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/price_order_id.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.PlacePriceTriggeredOrderAsync(new GateSpotPriceTriggeredOrderRequest
        {
            Symbol = "GT_USDT",
            Trigger = new GateSpotTriggerPrice
            {
                Price = "1.5",
                Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo,
                Expiration = 86400,
            },
            Order = new GateSpotTriggerOrder
            {
                Account = GateSpotPriceTriggeredOrderAccountType.Normal,
                Type = GateSpotOrderType.Limit,
                Side = GateSpotOrderSide.Buy,
                TimeInForce = GateSpotTriggerTimeInForce.GoodTillCancelled,
                Amount = "10",
                Price = "1.4",
                ClientOrderId = "t-auto",
            },
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1432329, result.Data);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/spot/price_orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("GT_USDT", body["market"]!.ToString());
        Assert.Equal(">=", body["trigger"]!["rule"]!.ToString());
        Assert.Equal("normal", body["put"]!["account"]!.ToString());
        Assert.Equal("limit", body["put"]!["type"]!.ToString());
        Assert.Equal("buy", body["put"]!["side"]!.ToString());
        Assert.Equal("gtc", body["put"]!["time_in_force"]!.ToString());
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
