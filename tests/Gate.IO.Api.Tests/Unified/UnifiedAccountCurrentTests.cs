using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.Unified;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Unified;

[Trait("Category", "Contract")]
public class UnifiedAccountCurrentTests
{
    private const string RootAmounts = "total,borrowed,total_initial_margin,total_margin_balance,total_maintenance_margin,total_initial_margin_rate,total_maintenance_margin_rate,total_available_margin,unified_account_total,unified_account_total_liab,unified_account_total_equity,leverage,spot_order_loss,options_order_loss";
    private const string BalanceAmounts = "available,freeze,borrowed,negative_liab,futures_pos_liab,equity,total_freeze,total_liab,spot_in_use,cross_balance,iso_balance,im,mm,imr,mmr,margin_balance,available_margin";

    [Theory]
    [InlineData("user_id")]
    [InlineData("balance_version")]
    [InlineData("sub_uid")]
    public void Fractional_ids_never_round_into_another_account_or_balance_version(string key)
    {
        var json = $"{{\"{key}\":9007199254740993.1}}";
        Assert.Throws<JsonSerializationException>(() =>
        {
            if (key == "user_id") JsonConvert.DeserializeObject<GateUnifiedAccountInfo>(json);
            else if (key == "balance_version") JsonConvert.DeserializeObject<GateIoUnifiedAccountBalance>(json);
            else JsonConvert.DeserializeObject<GateUnifiedAccountInfoRequest>(json);
        });
    }

    public static IEnumerable<object[]> AllAmounts() => RootAmounts.Split(',').Select(x => new object[] { false, x })
        .Concat(BalanceAmounts.Split(',').Select(x => new object[] { true, x }));

    [Theory]
    [MemberData(nameof(AllAmounts))]
    public void Every_typed_decimal_rejects_precision_loss_and_underflow(bool balance, string key)
    {
        foreach (var value in new[] { "\"1.12345678901234567890123456789\"", "\"0.00000000000000000000000000001\"", "1.1", "\"NaN\"" })
        {
            var json = $"{{\"{key}\":{value}}}";
            Assert.Throws<JsonSerializationException>(() =>
            {
                if (balance) JsonConvert.DeserializeObject<GateIoUnifiedAccountBalance>(json);
                else JsonConvert.DeserializeObject<GateUnifiedAccountInfo>(json);
            });
        }
    }

    [Theory]
    [InlineData("null")]
    [InlineData("")]
    public async Task Missing_account_object_is_not_a_successful_zero_risk_snapshot(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Unified.GetAccountInfoAsync();
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("[]")]
    [InlineData("1")]
    [InlineData("\"account\"")]
    public async Task Other_response_containers_are_not_account_snapshots(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Unified.GetAccountInfoAsync();
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task Null_account_query_is_rejected_before_io()
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.Unified.GetAccountInfoAsync((GateUnifiedAccountInfoRequest)null!));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [MemberData(nameof(AllAmounts))]
    public void Every_typed_decimal_keeps_exact_values_and_documented_string_serialization(bool balance, string key)
    {
        var json = $"{{\"{key}\":\"0.1234567890123456789012345678\"}}";
        var copy = balance ? JObject.FromObject(JsonConvert.DeserializeObject<GateIoUnifiedAccountBalance>(json)!)
            : JObject.FromObject(JsonConvert.DeserializeObject<GateUnifiedAccountInfo>(json)!);
        Assert.Equal(JTokenType.String, copy[key]!.Type);
        Assert.Equal("0.1234567890123456789012345678", (string?)copy[key]);
        var optional = JsonConvert.DeserializeObject<GateIoUnifiedAccountBalance>("{\"im\":null,\"mm\":\"0\",\"balance_version\":null}")!;
        Assert.Null(optional.InitialMargin);
        Assert.Equal(0m, optional.MaintenanceMargin);
        Assert.Null(optional.BalanceVersion);
    }

    [Fact]
    public void All_current_root_and_balance_fields_are_mapped_and_optional()
    {
        AssertFields<GateUnifiedAccountInfo>("mode,user_id,refresh_time,locked,balances," + RootAmounts + ",spot_hedge,use_funding,is_all_collateral", 22);
        AssertFields<GateIoUnifiedAccountBalance>(BalanceAmounts + ",funding,funding_version,enabled_collateral,balance_version", 21);
        var json = (JObject)JsonFixture.Parse("Docs/Unified/account_info.success.json");
        var account = json.ToObject<GateUnifiedAccountInfo>()!;
        var copy = JObject.FromObject(account);
        foreach (var key in RootAmounts.Split(',')) Assert.Equal((string?)json[key], (string?)copy[key]);
        foreach (var key in BalanceAmounts.Split(',')) Assert.Equal((string?)json["balances"]!["ETH"]![key], (string?)copy["balances"]!["ETH"]![key]);
        Assert.Equal("0", account.Balances["ETH"].Funding);
        Assert.Equal("1", account.Balances["ETH"].FundingVersion);
        Assert.Equal(10001, account.UserId);
        Assert.True(account.UseFunding);
        Assert.False(account.IsAllCollateral);
        Assert.Equal(new DateTime(2023, 1, 9, 6, 50, 54, DateTimeKind.Utc), account.RefreshTime);
    }

    [Theory]
    [InlineData("classic", GateUnifiedAccountMode.Classic)]
    [InlineData("multi_currency", GateUnifiedAccountMode.MultiCurrency)]
    [InlineData("portfolio", GateUnifiedAccountMode.Portfolio)]
    [InlineData("single_currency", GateUnifiedAccountMode.SingleCurrency)]
    public void All_four_documented_modes_decode(string wire, GateUnifiedAccountMode mode)
        => Assert.Equal(mode, JsonConvert.DeserializeObject<GateUnifiedAccountInfo>($"{{\"mode\":\"{wire}\"}}")!.Mode);

    [Fact]
    public async Task Optional_schema_fields_remain_optional_not_client_required_risk_evidence()
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        var result = await client.Unified.GetAccountInfoAsync();
        Assert.True(result.Success);
        Assert.Equal((GateUnifiedAccountMode)0, result.Data.Mode); // Not Classic.
        Assert.Empty(result.Data.Balances);
        Assert.Null(result.Data.UseFunding);
        Assert.Null(result.Data.IsAllCollateral);
        var balance = JsonConvert.DeserializeObject<GateIoUnifiedAccountBalance>("{}")!;
        Assert.Null(balance.IsCollateralEnabled);
        Assert.Null(balance.BalanceVersion);
        Assert.Null(balance.CrossMarginBalance);
    }

    [Fact]
    public async Task Optional_filters_are_signed_query_strings_and_legacy_overload_is_preserved()
    {
        var handler = Handler(JsonFixture.Read("Docs/Unified/account_info.success.json"));
        using var client = Client(handler);
        Assert.True((await client.Unified.GetAccountInfoAsync("ETH", long.MaxValue)).Success);
        Assert.True((await client.Unified.GetAccountInfoAsync("ETH", CancellationToken.None)).Success);
        Assert.True((await client.Unified.GetAccountInfoAsync()).Success);
        Assert.Equal(3, handler.Requests.Count);
        Assert.Contains("sub_uid=9223372036854775807", handler.Requests[0].RequestUri.Query);
        Assert.Contains("currency=ETH", handler.Requests[0].RequestUri.Query);
        Assert.Equal("?currency=ETH", handler.Requests[1].RequestUri.Query);
        Assert.Empty(handler.Requests[2].RequestUri.Query);
        foreach (var request in handler.Requests)
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("/api/v4/unified/accounts", request.RequestUri.AbsolutePath);
            Assert.Empty(request.Content);
            AssertSignature(request);
        }
        var account = JsonConvert.DeserializeObject<GateUnifiedAccountInfo>("{\"user_id\":\"9223372036854775807\"}")!;
        Assert.Equal(long.MaxValue, account.UserId);
        var version = JsonConvert.DeserializeObject<GateIoUnifiedAccountBalance>("{\"balance_version\":\"9007199254740993\"}")!;
        Assert.Equal(9007199254740993, version.BalanceVersion);
        var input = JsonConvert.DeserializeObject<GateUnifiedAccountInfoRequest>("{\"currency\":\"ETH\",\"sub_uid\":\"9223372036854775807\"}")!;
        Assert.Equal(long.MaxValue, input.SubAccountId);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task Errors_preserve_http_and_label_metadata_without_retry(HttpStatusCode status)
    {
        var handler = Handler("{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Rejected by server\"}", status);
        using var client = Client(handler);
        var result = await client.Unified.GetAccountInfoAsync();
        Assert.False(result.Success);
        Assert.Equal(status, result.Response!.StatusCode);
        Assert.Equal("CONTRACT_TEST_ERROR", result.Error!.Data);
        Assert.Single(handler.Requests);
    }

    private static void AssertFields<T>(string names, int count)
    {
        var contract = (Newtonsoft.Json.Serialization.JsonObjectContract)new Newtonsoft.Json.Serialization.DefaultContractResolver().ResolveContract(typeof(T));
        Assert.Equal(count, contract.Properties.Count);
        Assert.Equal(names.Split(',').OrderBy(x => x), contract.Properties.Select(x => x.PropertyName).OrderBy(x => x));
        Assert.All(contract.Properties, x => Assert.Equal(Required.Default, x.Required));
    }

    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }

    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var hash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var payload = $"{request.Method.Method}\n{request.RequestUri.AbsolutePath}\n{request.RequestUri.Query.TrimStart('?')}\n{hash}\n{timestamp}";
        Assert.Equal(Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant(), Assert.Single(request.Headers["SIGN"]));
    }
}
