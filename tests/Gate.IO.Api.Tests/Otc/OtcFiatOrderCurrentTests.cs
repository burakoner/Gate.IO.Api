using Gate.IO.Api.Otc;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Otc;

[Trait("Category", "Contract")]
public class OtcFiatOrderCurrentTests
{
    [Fact]
    public async Task Http_success_with_a_business_error_must_not_report_a_created_order()
    {
        var handler = Handler("{\"code\":10010400,\"message\":\"Invalid quote\",\"timestamp\":1752051076}");
        using var client = Client(handler);
        var result = await client.Otc.CreateFiatOrderAsync(Input());
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Equal(10010400, result.Error!.Code);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("null")]
    [InlineData("")]
    [InlineData("{}")]
    public async Task Missing_action_result_is_not_fabricated_as_business_success(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Otc.CreateFiatOrderAsync(Input());
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task Stable_order_kind_is_not_a_documented_fiat_order_side()
    {
        var handler = Handler(JsonFixture.Read("Docs/Otc/action.success.json"));
        using var client = Client(handler);
        var input = Input() with { Side = GateOtcOrderKind.Stable };
        await Assert.ThrowsAsync<ArgumentException>(() => client.Otc.CreateFiatOrderAsync(input));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(GateOtcReceiveType.Company, "YOU")]
    [InlineData(GateOtcReceiveType.Gate, "GATE")]
    [InlineData(GateOtcReceiveType.Recipient, "RECIPIENT")]
    [InlineData(GateOtcReceiveType.Person, "PERSON")]
    [InlineData(null, null)]
    public async Task Complete_body_and_signature_preserve_remittance_choice_and_exact_values(GateOtcReceiveType? receive, string? wire)
    {
        var handler = Handler(JsonFixture.Read("Docs/Otc/action.success.json"));
        using var client = Client(handler);
        var input = Input() with { Type = GateOtcOrderType.Sell, Side = GateOtcOrderKind.Pay,
            ReceiveType = receive, BankId = 9007199254740993, CryptoAmount = 0.1234567890123456789012345678m,
            FiatAmount = 0m, PromotionCode = "" };
        var result = await client.Otc.CreateFiatOrderAsync(input);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(0, result.Data.Code);
        Assert.Equal("success", result.Data.Message);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1752051076).UtcDateTime, result.Data.Timestamp);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/otc/order/create", request.RequestUri.AbsolutePath);
        Assert.Empty(request.RequestUri.Query);
        var json = JObject.Parse(request.Content);
        var expected = new JObject { ["type"] = "SELL", ["side"] = "PAY", ["crypto_currency"] = "USDT",
            ["fiat_currency"] = "USD", ["crypto_amount"] = "0.1234567890123456789012345678", ["fiat_amount"] = "0",
            ["promotion_code"] = "", ["quote_token"] = "quote-token", ["bank_id"] = "9007199254740993" };
        if (wire != null) expected["receive_type"] = wire;
        Assert.True(JToken.DeepEquals(expected, json));
        AssertSignature(request);
        var saved = JsonConvert.SerializeObject(input);
        var copy = JsonConvert.DeserializeObject<GateOtcFiatOrderRequest>(saved)!;
        Assert.Equal(input, copy);
    }

    [Theory]
    [InlineData(GateOtcOrderKind.Fiat, "FIAT")]
    [InlineData(GateOtcOrderKind.Crypto, "CRYPTO")]
    [InlineData(GateOtcOrderKind.Pay, "PAY")]
    [InlineData(GateOtcOrderKind.Get, "GET")]
    public async Task All_documented_quote_validation_sides_are_supported(GateOtcOrderKind side, string wire)
    {
        var handler = Handler(JsonFixture.Read("Docs/Otc/action.success.json"));
        using var client = Client(handler);
        Assert.True((await client.Otc.CreateFiatOrderAsync(Input() with { Side = side })).Success);
        Assert.Equal(wire, (string?)JObject.Parse(Assert.Single(handler.Requests).Content)["side"]);
        // STABLE remains a valid quote order_type, not a fiat-order side.
        Assert.Equal(GateOtcOrderKind.Stable, JsonConvert.DeserializeObject<GateOtcQuote>("{\"order_type\":\"STABLE\"}")!.OrderType);
    }

    [Fact]
    public async Task Legacy_positional_cancellation_token_and_omitted_optionals_remain_compatible()
    {
        var handler = Handler(JsonFixture.Read("Docs/Otc/action.success.json"));
        using var client = Client(handler);
        var result = await client.Otc.CreateFiatOrderAsync(GateOtcOrderType.Buy, "USDT", "USD", 1, 1, "quote-token", 2, null, CancellationToken.None);
        Assert.True(result.Success);
        var body = JObject.Parse(Assert.Single(handler.Requests).Content);
        Assert.Equal("FIAT", (string?)body["side"]);
        Assert.Null(body["promotion_code"]);
        Assert.Null(body["receive_type"]);
    }

    [Theory]
    [InlineData("type")]
    [InlineData("side")]
    [InlineData("crypto_currency")]
    [InlineData("fiat_currency")]
    [InlineData("crypto_amount")]
    [InlineData("fiat_amount")]
    [InlineData("quote_token")]
    [InlineData("bank_id")]
    public void Saved_json_requires_all_eight_documented_required_keys(string field)
    {
        var json = JObject.FromObject(Input());
        json.Remove(field);
        Assert.Throws<JsonSerializationException>(() => json.ToObject<GateOtcFiatOrderRequest>());
        json[field] = null;
        Assert.Throws<JsonSerializationException>(() => json.ToObject<GateOtcFiatOrderRequest>());
    }

    [Theory]
    [InlineData("type", "\"OTHER\"")]
    [InlineData("type", "1")]
    [InlineData("type", "\"1\"")]
    [InlineData("side", "\"STABLE\"")]
    [InlineData("side", "\"UNKNOWN\"")]
    [InlineData("side", "true")]
    [InlineData("receive_type", "\"UNKNOWN\"")]
    [InlineData("receive_type", "1")]
    [InlineData("receive_type", "\"1\"")]
    [InlineData("bank_id", "9007199254740993.1")]
    [InlineData("bank_id", "true")]
    [InlineData("bank_id", "\"9223372036854775808\"")]
    [InlineData("crypto_amount", "\"0.00000000000000000000000000001\"")]
    [InlineData("fiat_amount", "\"1.12345678901234567890123456789\"")]
    [InlineData("fiat_amount", "1.1")]
    public void Saved_json_never_changes_unknown_instructions_or_rounds_amounts_and_bank_ids(string field, string value)
    {
        var json = JObject.FromObject(Input());
        json[field] = JToken.Parse(value);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateOtcFiatOrderRequest>(json.ToString(Formatting.None)));
    }

    [Fact]
    public void Saved_bank_numeric_string_preserves_long_identity()
    {
        var json = JObject.FromObject(Input());
        json["bank_id"] = "9223372036854775807";
        Assert.Equal(long.MaxValue, json.ToObject<GateOtcFiatOrderRequest>()!.BankId);
    }

    [Fact]
    public async Task Invalid_inputs_do_not_send_or_select_a_bank_quote_or_remittance_name()
    {
        var handler = Handler(JsonFixture.Read("Docs/Otc/action.success.json"));
        using var client = Client(handler);
        GateOtcFiatOrderRequest?[] invalid = [null, Input() with { Type = 0 }, Input() with { Side = 0 },
            Input() with { ReceiveType = (GateOtcReceiveType)255 }, Input() with { CryptoCurrency = " " },
            Input() with { FiatCurrency = null! }, Input() with { QuoteToken = "" }, Input() with { BankId = 0 },
            Input() with { BankId = -1 }];
        foreach (var input in invalid)
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Otc.CreateFiatOrderAsync(input!));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("[]")]
    [InlineData("{\"code\":null}")]
    [InlineData("{\"code\":true}")]
    [InlineData("{\"code\":0.1}")]
    [InlineData("{\"code\":\"0\"}")]
    [InlineData("{\"code\":2147483648}")]
    [InlineData("{\"code\":0,\"timestamp\":1752051076}")]
    [InlineData("{\"code\":0,\"message\":2,\"timestamp\":1752051076}")]
    [InlineData("{\"code\":0,\"message\":\"success\"}")]
    [InlineData("{\"code\":0,\"message\":\"success\",\"timestamp\":null}")]
    [InlineData("{\"code\":0,\"message\":\"success\",\"timestamp\":1752051076.1}")]
    [InlineData("{\"code\":0,\"message\":\"success\",\"timestamp\":\"1752051076\"}")]
    [InlineData("{\"code\":0,\"message\":\"success\",\"timestamp\":9223372036854775807}")]
    public async Task Malformed_success_acknowledgements_fail_with_original_http_metadata(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Otc.CreateFiatOrderAsync(Input());
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task Error_only_business_envelope_preserves_code_and_message_without_timestamp()
    {
        var handler = Handler("{\"code\":10010400,\"message\":\"Invalid quote\"}");
        using var client = Client(handler);
        var result = await client.Otc.CreateFiatOrderAsync(Input());
        Assert.False(result.Success);
        Assert.Equal(10010400, result.Error!.Code);
        Assert.Contains("Invalid quote", result.Error.Message);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task Http_failures_preserve_status_and_label_without_retry(HttpStatusCode status)
    {
        var handler = Handler("{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Rejected by server\"}", status);
        using var client = Client(handler);
        var result = await client.Otc.CreateFiatOrderAsync(Input());
        Assert.False(result.Success);
        Assert.Equal(status, result.Response!.StatusCode);
        Assert.Equal("CONTRACT_TEST_ERROR", result.Error!.Data);
        Assert.Contains("Rejected by server", result.Error.Message);
        Assert.Single(handler.Requests);
    }

    private static GateOtcFiatOrderRequest Input() => new()
    {
        Type = GateOtcOrderType.Buy, CryptoCurrency = "USDT", FiatCurrency = "USD",
        CryptoAmount = 30000m, FiatAmount = 30000m, QuoteToken = "quote-token", BankId = 2,
    };

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
        var bodyHash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var payload = $"{request.Method.Method}\n{request.RequestUri.AbsolutePath}\n{request.RequestUri.Query.TrimStart('?')}\n{bodyHash}\n{timestamp}";
        var signature = Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant();
        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }
}
