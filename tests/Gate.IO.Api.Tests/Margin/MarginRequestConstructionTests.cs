using Gate.IO.Api.Margin;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Margin;

[Trait("Category", "Unit")]
public class MarginRequestConstructionTests
{
    [Fact]
    public async Task Signed_margin_balance_history_request_serializes_query()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Margin/account_book.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.IsolatedMargin.GetBalanceHistoryAsync(new GateMarginBalanceHistoryQueryRequest
        {
            Currency = "BTC",
            Symbol = "BTC_USDT",
            Type = "margin_in",
            From = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc),
            To = new DateTime(2024, 1, 3, 3, 4, 5, DateTimeKind.Utc),
            Page = 2,
            Limit = 50,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/margin/account_book", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC", query["currency"]);
        Assert.Equal("BTC_USDT", query["currency_pair"]);
        Assert.Equal("margin_in", query["type"]);
        Assert.Equal("1704164645", query["from"]);
        Assert.Equal("1704251045", query["to"]);
        Assert.Equal("2", query["page"]);
        Assert.Equal("50", query["limit"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Public_margin_markets_request_omits_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Margin/currency_pairs.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.IsolatedMargin.GetMarketsAsync();

        Assert.True(result.Success, result.Error?.ToString());
        var market = JObject.FromObject(Assert.Single(result.Data!));
        Assert.Equal("enabled", market["status"]?.Value<string>());
        Assert.Equal(1786329745L, market["delisted_time"]?.Value<long>());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/margin/uni/currency_pairs", request.RequestUri.AbsolutePath);
        Assert.Empty(request.RequestUri.Query);
        Assert.Empty(request.Content);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
        Assert.DoesNotContain("Timestamp", request.Headers.Keys);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(" BTC_USDT")]
    [InlineData("BTC_USDT ")]
    [InlineData(".")]
    [InlineData("..")]
    [InlineData("BTC_USDT/..")]
    [InlineData("BTC_USDT\\..")]
    [InlineData("BTC_USDT?currency_pair=ETH_USDT")]
    [InlineData("BTC_USDT#fragment")]
    [InlineData("%2e%2e")]
    [InlineData("BTC_USDT%2f..")]
    [InlineData("BTC_USDT\n")]
    [InlineData("BTC_USDT\u0000")]
    public void Single_margin_market_rejects_missing_or_routing_symbols_before_io(string? symbol)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{}"));
        var client = CreateClient(handler);

        var exception = Assert.Throws<ArgumentException>(() => { _ = client.IsolatedMargin.GetMarketsAsync(symbol!); });

        Assert.Equal("symbol", exception.ParamName);
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("AE_USDT", false)]
    [InlineData("BTC_USDT", true)]
    [InlineData("1000SHIB_USDT", true)]
    [InlineData("BTC5L_USDT", false)]
    public async Task Single_margin_market_preserves_the_symbol_and_omits_authentication(string symbol, bool credentials)
    {
        var body = JObject.Parse(JsonFixture.Read("Docs/Margin/currency_pair.success.json"));
        body["currency_pair"] = symbol;
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(body.ToString()));
        var client = CreateClient(handler);
        if (credentials)
            client.SetApiCredentials("key", "secret");

        var result = await client.IsolatedMargin.GetMarketsAsync(symbol);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(symbol, result.Data!.Symbol);
        Assert.Equal(100m, result.Data.MinimumBaseBorrowQuantity);
        Assert.Equal(100m, result.Data.MinimumQuoteBorrowQuantity);
        Assert.Equal(3m, result.Data.Leverage);
        Assert.Equal("enabled", result.Data.Status);
        Assert.Equal(1786329745L, result.Data.DelistedTime);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal($"/api/v4/margin/uni/currency_pairs/{symbol}", request.RequestUri.AbsolutePath);
        Assert.Empty(request.RequestUri.Query);
        Assert.Empty(request.Content);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
        Assert.DoesNotContain("Timestamp", request.Headers.Keys);
    }

    [Fact]
    public async Task Margin_market_queries_preserve_disabled_status_independently_from_delisting_time()
    {
        var body = JObject.Parse(JsonFixture.Read("Docs/Margin/currency_pair.success.json"));
        body["status"] = "disabled";
        body["delisted_time"] = 0L;
        var handler = new RecordingHttpMessageHandler(request => JsonResponse(
            request.RequestUri.AbsolutePath.EndsWith("/AE_USDT", StringComparison.Ordinal)
                ? body.ToString() : new JArray(body).ToString()));
        var client = CreateClient(handler);

        var list = await client.IsolatedMargin.GetMarketsAsync();
        var single = await client.IsolatedMargin.GetMarketsAsync("AE_USDT");

        Assert.True(list.Success, list.Error?.ToString());
        Assert.True(single.Success, single.Error?.ToString());
        foreach (var market in new[] { Assert.Single(list.Data!), single.Data! })
        {
            Assert.Equal("disabled", market.Status);
            Assert.Equal(0L, market.DelistedTime);
        }
        Assert.Equal(2, handler.Requests.Count);
    }

    [Fact]
    public async Task Empty_margin_market_list_remains_successful()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);

        var result = await client.IsolatedMargin.GetMarketsAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Empty(result.Data!);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task Margin_market_http_errors_do_not_become_successful_market_responses()
    {
        var handler = new RecordingHttpMessageHandler(_ => new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
        {
            Content = new StringContent("{\"label\":\"INVALID_ARGUMENT\",\"message\":\"invalid argument\"}", Encoding.UTF8, "application/json"),
        });
        var client = CreateClient(handler);

        var list = await client.IsolatedMargin.GetMarketsAsync();
        var single = await client.IsolatedMargin.GetMarketsAsync("AE_USDT");

        Assert.False(list.Success);
        Assert.False(single.Success);
        Assert.Null(list.Data);
        Assert.Null(single.Data);
        Assert.All(new[] { list.Error, single.Error }, error =>
        {
            Assert.NotNull(error);
            Assert.Equal("invalid argument", error.Message);
            Assert.Contains("INVALID_ARGUMENT", error.ToString());
        });
        Assert.Equal(2, handler.Requests.Count);
    }

    [Fact]
    public async Task Signed_margin_loan_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{}"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.IsolatedMargin.BorrowOrRepayAsync(new GateMarginLoanRequest
        {
            Symbol = "BTC_USDT",
            Currency = "BTC",
            Amount = 0.1m,
            Type = GateMarginUniOrderType.Borrow,
            RepaidAll = false,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/margin/uni/loans", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("borrow", body["type"]!.ToString());
        Assert.Equal("BTC", body["currency"]!.ToString());
        Assert.Equal("BTC_USDT", body["currency_pair"]!.ToString());
        Assert.Equal("0.1", body["amount"]!.ToString());
        Assert.False(body["repaid_all"]!.Value<bool>());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Public_margin_lending_tiers_request_deserializes_response()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Margin/loan_margin_tiers.success.json")));
        var client = CreateClient(handler);

        var result = await client.IsolatedMargin.GetCurrentLendingTiersAsync("BTC_USDT");

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Single(result.Data!);
        Assert.Equal(0.9m, result.Data![0].MMR);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/margin/loan_margin_tiers", request.RequestUri.AbsolutePath);
        Assert.Equal("BTC_USDT", ParseQuery(request.RequestUri)["currency_pair"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_margin_auto_repay_request_serializes_mapped_query()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Margin/auto_repay.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.IsolatedMargin.SetAutoRepaymentAsync(GateMarginAutoRepaymentStatus.Enabled);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(GateMarginAutoRepaymentStatus.Enabled, result.Data!.Status);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/margin/auto_repay", request.RequestUri.AbsolutePath);
        Assert.Equal("on", ParseQuery(request.RequestUri)["status"]);
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
