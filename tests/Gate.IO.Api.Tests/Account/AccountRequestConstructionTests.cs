using Gate.IO.Api.Account;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Account;

[Trait("Category", "Unit")]
public class AccountRequestConstructionTests
{
    [Fact]
    public async Task Signed_account_read_requests_use_documented_paths()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Account/detail.success.json"),
            JsonFixture.Read("Docs/Account/main_keys.success.json"),
            JsonFixture.Read("Docs/Account/rate_limit.success.json"),
            JsonFixture.Read("Docs/Account/debit_fee.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var detail = await client.Account.GetAccountDetailsAsync();
        var keys = await client.Account.GetMainKeysAsync();
        var limits = await client.Account.GetRateLimitsAsync();
        var debitFee = await client.Account.GetDebitFeeAsync();

        Assert.True(detail.Success, detail.Error?.ToString());
        Assert.True(keys.Success, keys.Error?.ToString());
        Assert.True(limits.Success, limits.Error?.ToString());
        Assert.True(debitFee.Success, debitFee.Error?.ToString());
        Assert.Equal(4, handler.Requests.Count);
        Assert.Equal("/api/v4/account/detail", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/account/main_keys", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/account/rate_limit", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/account/debit_fee", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.All(handler.Requests, request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            AssertSignedHeaders(request);
        });
    }

    [Fact]
    public async Task Stp_group_query_and_create_requests_serialize_signed_payloads()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Account/stp_groups.success.json"),
            JsonFixture.Read("Docs/Account/stp_group.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var groups = await client.Account.GetStpGroupsAsync(new GateAccountStpGroupQueryRequest
        {
            Name = "group",
        });
        var created = await client.Account.CreateStpGroupAsync(new GateAccountStpGroupRequest
        {
            Name = "stp_name",
        });

        Assert.True(groups.Success, groups.Error?.ToString());
        Assert.True(created.Success, created.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);

        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/account/stp_groups", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("group", ParseQuery(handler.Requests[0].RequestUri)["name"]);
        AssertSignedHeaders(handler.Requests[0]);

        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        Assert.Equal("/api/v4/account/stp_groups", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("stp_name", JObject.Parse(handler.Requests[1].Content)["name"]!.ToString());
        AssertSignedHeaders(handler.Requests[1]);
    }

    [Fact]
    public async Task Stp_group_user_requests_serialize_array_body_and_csv_delete_query()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Account/stp_group_users.success.json"),
            JsonFixture.Read("Docs/Account/stp_group_users.success.json"),
            JsonFixture.Read("Docs/Account/stp_group_users.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var users = await client.Account.GetStpGroupUsersAsync(1);
        var added = await client.Account.AddUsersToStpGroupAsync(1, new GateAccountStpGroupUsersRequest
        {
            UserIds = [1, 2, 3],
        });
        var removed = await client.Account.RemoveUsersFromStpGroupAsync(1, new GateAccountStpGroupUsersRequest
        {
            UserIds = [1, 2, 3],
        });

        Assert.True(users.Success, users.Error?.ToString());
        Assert.True(added.Success, added.Error?.ToString());
        Assert.True(removed.Success, removed.Error?.ToString());
        Assert.Equal(3, handler.Requests.Count);
        Assert.Equal("/api/v4/account/stp_groups/1/users", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        Assert.Equal("[1,2,3]", handler.Requests[1].Content);
        Assert.Equal(HttpMethod.Delete, handler.Requests[2].Method);
        Assert.Equal("1,2,3", ParseQuery(handler.Requests[2].RequestUri)["user_id"]);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
    }

    [Fact]
    public async Task Debit_fee_request_serializes_signed_boolean_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{}"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Account.SetDebitFeeAsync(new GateAccountDebitFeeRequest
        {
            Enabled = true,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/account/debit_fee", request.RequestUri.AbsolutePath);
        Assert.True(JObject.Parse(request.Content)["enabled"]!.Value<bool>());
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
