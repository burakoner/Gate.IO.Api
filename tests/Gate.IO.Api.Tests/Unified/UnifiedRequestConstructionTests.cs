using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.Unified;
using System.Text;

namespace Gate.IO.Api.Tests.Unified;

[Trait("Category", "Unit")]
public class UnifiedRequestConstructionTests
{
    [Fact]
    public async Task Signed_unified_account_info_request_serializes_query()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Unified/account_info.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Unified.GetAccountInfoAsync(new GateUnifiedAccountInfoRequest
        {
            Currency = "ETH",
            SubAccountId = 10001,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/unified/accounts", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("ETH", query["currency"]);
        Assert.Equal("10001", query["sub_uid"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_unified_loan_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Unified/loan_result.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Unified.BorrowOrRepayAsync(new GateUnifiedLoanRequest
        {
            Currency = "BTC",
            Amount = 0.1m,
            Type = GateUnifiedLoanDirection.Borrow,
            RepaidAll = false,
            Text = "t-test",
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/unified/loans", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC", body["currency"]!.ToString());
        Assert.Equal("0.1", body["amount"]!.ToString());
        Assert.Equal("borrow", body["type"]!.ToString());
        Assert.False(body["repaid_all"]!.Value<bool>());
        Assert.Equal("t-test", body["text"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_unified_mode_request_serializes_nested_settings()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{}"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Unified.SetAccountModeAsync(new GateUnifiedAccountModeRequest
        {
            Mode = GateUnifiedAccountMode.Portfolio,
            Settings = new GateUnifiedAccountModeSettings
            {
                SpotHedge = true,
                UsdtFutures = true,
                Options = true,
            },
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/api/v4/unified/unified_mode", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("portfolio", body["mode"]!.ToString());
        Assert.True(body["settings"]!["spot_hedge"]!.Value<bool>());
        Assert.True(body["settings"]!["usdt_futures"]!.Value<bool>());
        Assert.True(body["settings"]!["options"]!.Value<bool>());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Public_unified_currencies_request_omits_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Unified/currencies.success.json")));
        var client = CreateClient(handler);

        var result = await client.Unified.GetCurrenciesAsync("BTC");

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/unified/currencies", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC", query["currency"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_collateral_currency_request_serializes_lists()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Unified/collateral_currencies.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Unified.SetCollateralCurrenciesAsync(new GateUnifiedCollateralCurrenciesRequest
        {
            Type = GateUnifiedCollateralType.Custom,
            EnableList = ["BTC", "ETH"],
            DisableList = ["SOL", "GT"],
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/unified/collateral_currencies", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("1", body["collateral_type"]!.ToString());
        Assert.Equal("BTC", body["enable_list"]![0]!.ToString());
        Assert.Equal("GT", body["disable_list"]![1]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_quick_repayment_estimate_request_uses_current_route()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Unified/quick_repayment_estimate.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Unified.GetEstimatedQuickRepaymentAsync();

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/unified/estimated_quick_repayment", request.RequestUri.AbsolutePath);
        Assert.Empty(request.RequestUri.Query);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_quick_repayment_request_serializes_required_lists()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Unified/quick_repayment.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Unified.CreateQuickRepaymentAsync(new GateUnifiedQuickRepaymentRequest
        {
            DebtCurrencies = ["BTC", "ETH"],
            AvailableCurrencies = ["USDT", "USDC"],
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/unified/quick_repayment", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal(["BTC", "ETH"], body["debt_currencies"]!.Values<string>());
        Assert.Equal(["USDT", "USDC"], body["available_currencies"]!.Values<string>());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Quick_repayment_rejects_missing_required_currency_lists()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{}"));
        var client = CreateClient(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Unified.CreateQuickRepaymentAsync(new GateUnifiedQuickRepaymentRequest
        {
            AvailableCurrencies = ["USDT"],
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Unified.CreateQuickRepaymentAsync(new GateUnifiedQuickRepaymentRequest
        {
            DebtCurrencies = ["BTC"],
        }));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Signed_delta_neutral_requests_use_current_contract()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Unified/delta_neutral.success.json"),
            """{"enabled":false}""",
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var queried = await client.Unified.GetDeltaNeutralAsync();
        var updated = await client.Unified.SetDeltaNeutralAsync(false);

        Assert.True(queried.Success, queried.Error?.ToString());
        Assert.True(queried.Data.Enabled);
        Assert.True(updated.Success, updated.Error?.ToString());
        Assert.False(updated.Data.Enabled);
        Assert.Equal(2, handler.Requests.Count);

        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/unified/delta_neutral", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Empty(handler.Requests[0].RequestUri.Query);

        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        Assert.Equal("/api/v4/unified/delta_neutral", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.False(JObject.Parse(handler.Requests[1].Content)["enabled"]!.Value<bool>());
        Assert.All(handler.Requests, AssertSignedHeaders);
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
