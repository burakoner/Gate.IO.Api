using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests;

[Trait("Category", "Unit")]
public class RestClientRequestConstructionTests
{
    [Fact]
    public async Task Public_alpha_get_request_serializes_path_and_query_without_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);

        var result = await client.Alpha.GetCurrenciesAsync(new GateAlphaCurrencyQueryRequest
        {
            Currency = "memeboxquq",
            Page = 2,
            Limit = 1,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/alpha/currencies", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("memeboxquq", query["currency"]);
        Assert.Equal("2", query["page"]);
        Assert.Equal("1", query["limit"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_alpha_get_request_serializes_query_and_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Alpha/orders.list.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Alpha.GetOrdersAsync(new GateAlphaOrdersQueryRequest
        {
            Currency = "MEME",
            Side = GateAlphaOrderSide.Buy,
            Status = GateAlphaOrderStatus.Processing,
            From = DateTimeOffset.FromUnixTimeSeconds(1742972931).UtcDateTime,
            To = DateTimeOffset.FromUnixTimeSeconds(1742972999).UtcDateTime,
            Page = 3,
            Limit = 10,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/alpha/orders", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("MEME", query["currency"]);
        Assert.Equal("buy", query["side"]);
        Assert.Equal("1", query["status"]);
        Assert.Equal("1742972931", query["from"]);
        Assert.Equal("1742972999", query["to"]);
        Assert.Equal("3", query["page"]);
        Assert.Equal("10", query["limit"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_alpha_post_request_serializes_request_object_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Alpha/orders.create.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Alpha.PlaceOrderAsync(new GateAlphaOrderRequest
        {
            Currency = "memeboxquq",
            Side = GateAlphaOrderSide.Sell,
            Amount = 12.34m,
            GasMode = GateAlphaGasMode.Custom,
            Slippage = 5.5m,
            QuoteId = "quote-123",
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/alpha/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("memeboxquq", body["currency"]!.ToString());
        Assert.Equal("sell", body["side"]!.ToString());
        Assert.Equal("12.34", body["amount"]!.ToString());
        Assert.Equal("custom", body["gas_mode"]!.ToString());
        Assert.Equal("5.5", body["slippage"]!.ToString());
        Assert.Equal("quote-123", body["quote_id"]!.ToString());
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
