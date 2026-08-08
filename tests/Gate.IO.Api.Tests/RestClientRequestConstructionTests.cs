using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests;

[Trait("Category", "Unit")]
public class RestClientRequestConstructionTests
{
    [Fact]
    public void Null_options_use_default_rest_client_configuration()
    {
        var client = new GateRestApiClient(null, null);

        Assert.NotNull(client.Otc);
    }

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

    [Fact]
    public async Task Withdrawal_alias_post_request_serializes_request_object_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Withdrawal/withdraw.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Withdrawal.WithdrawAsync(new GateWalletWithdrawalRequest
        {
            Currency = "USDT",
            Amount = 10.5m,
            Chain = "TRX",
            Address = "TVjsyZ7fYF3Qh7xDemoAddress",
            Memo = "memo",
            WithdrawalOrderId = "client-1",
            WithdrawalId = "w1879219868",
            AssetClass = GateWalletAssetClass.MainZone,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/withdrawals", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("USDT", body["currency"]!.ToString());
        Assert.Equal("10.5", body["amount"]!.ToString());
        Assert.Equal("TRX", body["chain"]!.ToString());
        Assert.Equal("client-1", body["withdraw_order_id"]!.ToString());
        Assert.Equal("w1879219868", body["withdraw_id"]!.ToString());
        Assert.Equal("SPOT", body["asset_class"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_wallet_withdrawals_request_serializes_query_and_flattens_response()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Wallet/withdrawals.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Wallet.GetWithdrawalsAsync(new GateWalletWithdrawalQueryRequest
        {
            Currency = "USDT",
            WithdrawalId = "w1879219868",
            WithdrawalOrderId = "order_123456",
            AssetClass = GateWalletAssetClass.MainZone,
            From = DateTimeOffset.FromUnixTimeSeconds(1542000000).UtcDateTime,
            To = DateTimeOffset.FromUnixTimeSeconds(1542003600).UtcDateTime,
            Limit = 2,
            Offset = 3,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.NotNull(result.Data);
        Assert.Single(result.Data);
        Assert.Equal("w1879219868", result.Data[0].Id);

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/wallet/withdrawals", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("USDT", query["currency"]);
        Assert.Equal("w1879219868", query["withdraw_id"]);
        Assert.Equal("order_123456", query["withdraw_order_id"]);
        Assert.Equal("SPOT", query["asset_class"]);
        Assert.Equal("1542000000", query["from"]);
        Assert.Equal("1542003600", query["to"]);
        Assert.Equal("2", query["limit"]);
        Assert.Equal("3", query["offset"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_wallet_saved_address_requests_support_current_optional_filters()
    {
        var json = JsonFixture.Read("Docs/Wallet/saved_address.success.json");
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(json));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var allAddresses = await client.Wallet.GetSavedAddressesAsync();
        var verifiedAddresses = await client.Wallet.GetSavedAddressesAsync(new GateWalletSavedAddressQueryRequest
        {
            Chain = "TRX",
            Verified = true,
        });

        Assert.True(allAddresses.Success, allAddresses.Error?.ToString());
        Assert.True(verifiedAddresses.Success, verifiedAddresses.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);

        var allQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.DoesNotContain("currency", allQuery.Keys);
        Assert.DoesNotContain("chain", allQuery.Keys);
        Assert.DoesNotContain("verified", allQuery.Keys);
        Assert.Equal("100", allQuery["limit"]);
        Assert.Equal("1", allQuery["page"]);

        var filteredRequest = handler.Requests[1];
        Assert.Equal(HttpMethod.Get, filteredRequest.Method);
        Assert.Equal("/api/v4/wallet/saved_address", filteredRequest.RequestUri.AbsolutePath);
        var filteredQuery = ParseQuery(filteredRequest.RequestUri);
        Assert.DoesNotContain("currency", filteredQuery.Keys);
        Assert.Equal("TRX", filteredQuery["chain"]);
        Assert.Equal("1", filteredQuery["verified"]);
        Assert.DoesNotContain("limit", filteredQuery.Keys);
        Assert.DoesNotContain("page", filteredQuery.Keys);
        AssertSignedHeaders(filteredRequest);
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
