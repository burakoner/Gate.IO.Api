using Gate.IO.Api.SubAccount;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.SubAccount;

[Trait("Category", "Unit")]
public class SubAccountRequestConstructionTests
{
    [Fact]
    public async Task Create_sub_account_serializes_signed_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/SubAccount/sub_account.create.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.SubAccount.CreateSubAccountAsync(new GateSubAccountCreateRequest
        {
            Login = "sub_account_for_trades",
            Password = "secret-password",
            Email = "sub@example.com",
            Remark = "remark",
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/sub_accounts", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("sub_account_for_trades", body["login_name"]!.ToString());
        Assert.Equal("secret-password", body["password"]!.ToString());
        Assert.Equal("sub@example.com", body["email"]!.ToString());
        Assert.Equal("remark", body["remark"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task List_sub_accounts_serializes_type_query()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/SubAccount/sub_accounts.list.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.SubAccount.GetSubAccountsAsync(listSubAccountsOnly: false);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/sub_accounts", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("0", query["type"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Create_sub_account_api_key_serializes_permissions_and_ip_whitelist()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/SubAccount/api_key.create.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.SubAccount.CreateApiKeyAsync(100000, new GateSubAccountApiKeyRequest
        {
            Mode = 1,
            Name = "spot",
            Permissions =
            [
                new GateSubAccountApiKeyPermission { Name = GateSubAccountApiKeyPermissionSection.Options, ReadOnly = false },
                new GateSubAccountApiKeyPermission { Name = GateSubAccountApiKeyPermissionSection.Spot, ReadOnly = false },
                new GateSubAccountApiKeyPermission { Name = GateSubAccountApiKeyPermissionSection.Wallet, ReadOnly = true },
            ],
            IpWhitelist = ["127.0.0.1", "127.0.0.2"],
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/sub_accounts/100000/keys", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("1", body["mode"]!.ToString());
        Assert.Equal("spot", body["name"]!.ToString());
        Assert.Equal("options", body["perms"]![0]!["name"]!.ToString());
        Assert.Equal("spot", body["perms"]![1]!["name"]!.ToString());
        Assert.Equal("wallet", body["perms"]![2]!["name"]!.ToString());
        Assert.True(body["perms"]![2]!["read_only"]!.Value<bool>());
        Assert.Equal("127.0.0.2", body["ip_whitelist"]![1]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Get_sub_account_mode_uses_documented_path()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/SubAccount/unified_mode.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.SubAccount.GetSubAccountsModeAsync();

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/sub_accounts/unified_mode", request.RequestUri.AbsolutePath);
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
