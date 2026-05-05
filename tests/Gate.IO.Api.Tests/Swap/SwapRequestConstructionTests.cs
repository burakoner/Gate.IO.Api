using Gate.IO.Api.Swap;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Swap;

[Trait("Category", "Unit")]
public class SwapRequestConstructionTests
{
    [Fact]
    public async Task Public_flash_swap_markets_request_serializes_query_without_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Swap/currency_pairs.success.json")));
        var client = CreateClient(handler);

        var result = await client.FlashSwap.GetMarketsAsync(new GateSwapMarketQueryRequest
        {
            Currency = "BTC",
            Page = 2,
            Limit = 1,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/flash_swap/currency_pairs", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC", query["currency"]);
        Assert.Equal("2", query["page"]);
        Assert.Equal("1", query["limit"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_flash_swap_preview_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Swap/order_preview.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.FlashSwap.PreviewOrderAsync(new GateSwapPreviewRequest
        {
            SellCurrency = "BTC",
            SellAmount = 0.1m,
            BuyCurrency = "USDT",
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(3453434, result.Data!.PreviewId);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/flash_swap/orders/preview", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC", body["sell_currency"]!.ToString());
        Assert.Equal("0.1", body["sell_amount"]!.ToString());
        Assert.Equal("USDT", body["buy_currency"]!.ToString());
        Assert.Null(body["buy_amount"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_flash_swap_order_request_serializes_required_preview_result()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Swap/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.FlashSwap.PlaceOrderAsync(new GateSwapOrderRequest
        {
            PreviewId = 4564564,
            SellCurrency = "BTC",
            SellAmount = 0.1m,
            BuyCurrency = "USDT",
            BuyAmount = 10m,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/flash_swap/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("4564564", body["preview_id"]!.ToString());
        Assert.Equal("BTC", body["sell_currency"]!.ToString());
        Assert.Equal("0.1", body["sell_amount"]!.ToString());
        Assert.Equal("USDT", body["buy_currency"]!.ToString());
        Assert.Equal("10", body["buy_amount"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_flash_swap_order_list_request_serializes_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Swap/orders.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.FlashSwap.GetOrdersAsync(new GateSwapOrderQueryRequest
        {
            Status = GateSwapOrderStatus.Success,
            SellCurrency = "BTC",
            BuyCurrency = "USDT",
            Reverse = false,
            Page = 2,
            Limit = 50,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/flash_swap/orders", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("1", query["status"]);
        Assert.Equal("BTC", query["sell_currency"]);
        Assert.Equal("USDT", query["buy_currency"]);
        Assert.Equal("false", query["reverse"], StringComparer.OrdinalIgnoreCase);
        Assert.Equal("2", query["page"]);
        Assert.Equal("50", query["limit"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_flash_swap_order_lookup_uses_order_path()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Swap/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.FlashSwap.GetOrderAsync(54646);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/flash_swap/orders/54646", request.RequestUri.AbsolutePath);
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
