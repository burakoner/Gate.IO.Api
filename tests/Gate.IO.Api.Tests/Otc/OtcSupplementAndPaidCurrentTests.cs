using ApiSharp.Models;
using Gate.IO.Api.Otc;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Otc;

[Trait("Category", "Contract")]
public class OtcSupplementAndPaidCurrentTests
{
    private const string Ack = "{\"code\":0,\"message\":\"success\",\"timestamp\":1752051076}";
    // The endpoint does not publish the JSON category/container shape. Test opaque caller text, not an invented schema.
    private const string Proof = " { \"caller_category\": [{\"key\":\"otc_temp/42/bank/kanıt.png\",\"file_type\":\"image/png\"}] } ";
    private static readonly string[] Personal = ["IdDocumentFront", "IdDocumentBack", "AddressProof"];
    private static readonly string[] Enterprise = ["Certificate", "ShareHolders", "Passport", "ShareHoldingStructure", "FundsStatement", "Additional"];

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task Pre_upload_JSON_only_needs_bank_identity_not_every_direct_file(int kind)
    {
        var request = Input(kind);
        request.GetType().GetProperty("RelationshipProof")!.SetValue(request, Proof);
        if (kind == 1) ((GateOtcBankEnterpriseSupplementRequest)request).UserId = "";
        var handler = Handler(Ack);
        using var client = Client(handler);
        var result = await Send(client, kind, request);
        Assert.True(result.Success, result.Error?.ToString());
        var wire = Assert.Single(handler.Requests);
        Assert.Contains($"name=\"relationship_proof\"\r\n\r\n{Proof}\r\n", wire.Content);
        Assert.DoesNotContain("filename", wire.Content);
        Assert.Equal(kind == 1 ? 3 : 2, wire.Content.Split("Content-Disposition:").Length - 1);
        AssertSignature(wire);
        Assert.Equal("[multipart/form-data content omitted]", result.Request!.Body);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task All_raw_file_fields_can_mix_with_relationship_JSON_and_sign_exact_bytes(int kind)
    {
        var input = Input(kind);
        var names = kind == 0 ? Personal : Enterprise;
        input.GetType().GetProperty("RelationshipProof")!.SetValue(input, Proof);
        if (kind == 1) ((GateOtcBankEnterpriseSupplementRequest)input).UserId = "42";
        foreach (var name in names)
            input.GetType().GetProperty(name + "Upload")!.SetValue(input, new GateOtcFileUpload
            { Content = [255, 0, 195, 40, 13, 10, 128], FileName = name + "-kanıt.pdf", ContentType = "application/pdf" });
        var copy = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(input), input.GetType())!;
        var handler = Handler(Ack);
        using var client = Client(handler);
        var result = await Send(client, kind, copy);
        Assert.True(result.Success, result.Error?.ToString());
        var wire = Assert.Single(handler.Requests);
        Assert.Equal(names.Length + (kind == 1 ? 3 : 2), wire.Content.Split("Content-Disposition:").Length - 1);
        foreach (var name in names)
        {
            var field = WireName(input.GetType(), name);
            Assert.Contains($"name=\"{field}\"; filename=", wire.Content);
            Assert.DoesNotContain($"name=\"{field}_upload\"", wire.Content);
        }
        Assert.Equal(names.Length, CountBytes(wire.ContentBytes, [255, 0, 195, 40, 13, 10, 128]));
        Assert.NotEqual(Encoding.UTF8.GetBytes(wire.Content), wire.ContentBytes);
        Assert.Contains(Proof, wire.Content);
        AssertSignature(wire);
        Assert.Equal("[multipart/form-data content omitted]", result.Request!.Body);
    }

    public static IEnumerable<object[]> FileFields()
        => Personal.Select(x => new object[] { 0, x }).Concat(Enterprise.Select(x => new object[] { 1, x }));

    [Theory]
    [MemberData(nameof(FileFields))]
    public async Task Each_legacy_Base64_file_is_optional_and_decoded_to_a_real_file(int kind, string name)
    {
        var input = Input(kind);
        var bytes = Encoding.UTF8.GetBytes("PRIVATE_FILE_" + name);
        input.GetType().GetProperty(name)!.SetValue(input, Convert.ToBase64String(bytes));
        var handler = Handler(Ack);
        using var client = Client(handler);
        var result = await Send(client, kind, input);
        Assert.True(result.Success, result.Error?.ToString());
        var wire = Assert.Single(handler.Requests);
        var field = WireName(input.GetType(), name);
        Assert.Contains($"name=\"{field}\"; filename={field};", wire.Content);
        Assert.Contains("Content-Type: application/octet-stream\r\n\r\nPRIVATE_FILE_" + name + "\r\n", wire.Content);
        Assert.Equal(2, wire.Content.Split("Content-Disposition:").Length - 1);
        AssertSignature(wire);
    }

    [Theory]
    [MemberData(nameof(FileFields))]
    public async Task Duplicate_file_representations_never_send_HTTP(int kind, string name)
    {
        var request = Input(kind);
        request.GetType().GetProperty(name)!.SetValue(request, "YWJj");
        request.GetType().GetProperty(name + "Upload")!.SetValue(request, new GateOtcFileUpload { Content = [1], FileName = "proof" });
        var handler = Handler(Ack);
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => Send(client, kind, request));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [MemberData(nameof(FileFields))]
    public async Task Invalid_Base64_placeholders_never_send_HTTP(int kind, string name)
    {
        var request = Input(kind);
        request.GetType().GetProperty(name)!.SetValue(request, "!BASE64-PLACEHOLDER!");
        var handler = Handler(Ack);
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => Send(client, kind, request));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task Invalid_last_file_rejects_the_whole_mixed_submission_before_HTTP(int kind)
    {
        var request = Input(kind);
        var names = kind == 0 ? Personal : Enterprise;
        request.GetType().GetProperty(names[0] + "Upload")!.SetValue(request,
            new GateOtcFileUpload { Content = [1], FileName = "valid.pdf" });
        request.GetType().GetProperty(names[^1] + "Upload")!.SetValue(request,
            new GateOtcFileUpload { Content = [2], FileName = "bad\r\nInjected: value" });
        var handler = Handler(Ack);
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => Send(client, kind, request));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(0, "BankId")]
    [InlineData(1, "BankId")]
    [InlineData(2, "OrderId")]
    [InlineData(2, "PaymentReceiptFileKey")]
    public async Task Missing_or_blank_required_input_never_sends_HTTP(int kind, string name)
    {
        foreach (var value in new string?[] { null, "", " " })
        {
            var request = Input(kind);
            request.GetType().GetProperty(name)!.SetValue(request, value);
            var handler = Handler(Ack);
            using var client = Client(handler);
            var error = await Assert.ThrowsAsync<ArgumentException>(() => Send(client, kind, request));
            Assert.Equal(name, error.ParamName);
            Assert.Empty(handler.Requests);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Null_request_never_sends_HTTP(int kind)
    {
        var handler = Handler(Ack);
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => Send(client, kind, null!));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Paid_request_has_all_four_current_keys_unchanged_and_no_file_upload()
    {
        var request = (GateOtcMarkOrderPaidRequest)Input(2);
        request.OrderId = long.MaxValue.ToString(); // Already-string API remains a string.
        request.ClientOrderId = "merchant / İpek";
        request.PaymentReceiptFileKey = "b3RjX3RlbXAvNDIvZ2VuZXJhbC9wcm9vZi5wZGY=";
        request.PaymentReceipt = "";
        var handler = Handler(Ack);
        using var client = Client(handler);
        var result = await client.Otc.MarkFiatOrderAsPaidAsync(request);
        Assert.True(result.Success, result.Error?.ToString());
        var wire = Assert.Single(handler.Requests);
        Assert.True(JToken.DeepEquals(JObject.FromObject(request), JObject.Parse(wire.Content)));
        Assert.Equal("application/json; charset=utf-8", Assert.Single(wire.Headers["Content-Type"]));
        AssertSignature(wire);
    }

    public static IEnumerable<object[]> InvalidAcknowledgements()
    {
        string[] bodies = ["", "null", "[]", "true", "{}", "{\"code\":!}",
            "{\"code\":\"0\"}", "{\"code\":0.1}", "{\"code\":true}", "{\"code\":2147483648}",
            "{\"code\":0,\"timestamp\":1}", "{\"code\":0,\"message\":123,\"timestamp\":1}",
            "{\"code\":0,\"message\":\"success\"}", "{\"code\":0,\"message\":\"success\",\"timestamp\":null}",
            "{\"code\":0,\"message\":\"success\",\"timestamp\":\"1\"}",
            "{\"code\":0,\"message\":\"success\",\"timestamp\":1.1}",
            "{\"code\":0,\"message\":\"success\",\"timestamp\":true}",
            "{\"code\":0,\"message\":\"success\",\"timestamp\":9223372036854775808}"];
        for (var kind = 0; kind < 4; kind++) foreach (var body in bodies) yield return [kind, body];
    }

    [Theory]
    [MemberData(nameof(InvalidAcknowledgements))]
    public async Task Missing_malformed_or_coerced_acknowledgements_fail_without_payload_diagnostics(int kind, string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await Send(client, kind, Input(kind));
        Assert.False(result.Success);
        Assert.IsType<DeserializeError>(result.Error);
        Assert.Null(result.Error!.Data);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Business_errors_keep_code_and_HTTP_metadata_without_retry(int kind)
    {
        foreach (var status in new[] { HttpStatusCode.OK, HttpStatusCode.BadRequest, HttpStatusCode.TooManyRequests, HttpStatusCode.InternalServerError })
        {
            var json = "{\"code\":10010400,\"message\":\"Invalid parameters file not uploaded\"}";
            var handler = Handler(json, status);
            using var client = Client(handler, raw: true);
            var result = await Send(client, kind, Input(kind));
            Assert.False(result.Success);
            Assert.Equal(10010400, result.Error!.Code);
            Assert.Equal("Invalid parameters file not uploaded", result.Error.Message);
            Assert.Equal(json, result.Raw);
            Assert.Equal(status, result.Response!.StatusCode);
            Assert.Single(handler.Requests);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Date_looking_messages_remain_strings_in_raw_mode_and_logs_remain_bodyless(int kind)
    {
        var json = Ack.Replace("success", "2026-10-02T00:00:00Z");
        var logger = new TestLogger();
        var handler = Handler(json);
        using var client = Client(handler, raw: true, logger);
        var result = await Send(client, kind, Input(kind));
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("2026-10-02T00:00:00Z", result.Data.Message);
        Assert.Equal(json, result.Raw);
        Assert.DoesNotContain("2026-10-02T00:00:00Z", string.Join("\n", logger.Entries.Select(x => x.Message)));
        Assert.All(logger.Entries, x => Assert.Null(x.Exception));
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    [InlineData(1752051076L)]
    [InlineData(1752051076000L)]
    [InlineData(1752051076000000L)]
    [InlineData(1752051076000000000L)]
    public async Task DateTime_API_keeps_existing_unit_heuristics_and_zero_sentinels(long timestamp)
    {
        var json = $"{{\"code\":0,\"message\":\"success\",\"timestamp\":{timestamp}}}";
        var expected = JsonConvert.DeserializeObject<GateOtcActionResult>(json)!.Timestamp;
        for (var kind = 0; kind < 3; kind++)
        {
            var handler = Handler(json);
            using var client = Client(handler);
            var result = await Send(client, kind, Input(kind));
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(expected, result.Data.Timestamp);
        }
        Assert.Equal(typeof(DateTime), typeof(GateOtcActionResult).GetProperty("Timestamp")!.PropertyType);
    }

    public static IEnumerable<object[]> SavedStrings()
    {
        for (var kind = 0; kind < 3; kind++)
            foreach (var property in Input(kind).GetType().GetProperties().Where(x => x.PropertyType == typeof(string)))
                yield return [kind, property.Name];
    }

    [Theory]
    [MemberData(nameof(SavedStrings))]
    public void Saved_submission_strings_use_wire_keys_without_date_or_number_coercion(int kind, string name)
    {
        var input = Input(kind);
        var property = input.GetType().GetProperty(name)!;
        property.SetValue(input, "2026-10-02T00:00:00Z");
        var json = JObject.FromObject(input);
        var key = WireName(input.GetType(), name);
        Assert.Equal("2026-10-02T00:00:00Z", json[key]!.ToString());
        var copy = JsonConvert.DeserializeObject(json.ToString(), input.GetType())!;
        Assert.Equal(property.GetValue(input), property.GetValue(copy));
        json[key] = 123;
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), input.GetType()));
    }

    [Theory]
    [InlineData(0, "bank_id")]
    [InlineData(1, "bank_id")]
    [InlineData(2, "order_id")]
    [InlineData(2, "payment_receipt_file_key")]
    public void Required_saved_keys_reject_missing_and_null(int kind, string key)
    {
        var json = JObject.FromObject(Input(kind));
        json.Remove(key);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), Input(kind).GetType()));
        json[key] = JValue.CreateNull();
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), Input(kind).GetType()));
    }

    private static object Input(int kind) => kind switch
    {
        0 => new GateOtcBankPersonalSupplementRequest { BankId = "762" },
        1 => new GateOtcBankEnterpriseSupplementRequest { BankId = "762" },
        2 => new GateOtcMarkOrderPaidRequest { OrderId = "203", PaymentReceiptFileKey = "opaque-base64-key" },
        _ => new GateOtcFiatOrderRequest { Type = GateOtcOrderType.Buy, Side = GateOtcOrderKind.Pay, CryptoCurrency = "USDT", FiatCurrency = "USD", CryptoAmount = 1m, FiatAmount = 1m, QuoteToken = "quote", BankId = 762 },
    };
    private static Task<RestCallResult<GateOtcActionResult>> Send(GateRestApiClient client, int kind, object input) => kind switch
    {
        0 => client.Otc.SubmitPersonalBankSupplementAsync((GateOtcBankPersonalSupplementRequest)input),
        1 => client.Otc.SubmitEnterpriseBankSupplementAsync((GateOtcBankEnterpriseSupplementRequest)input),
        2 => client.Otc.MarkFiatOrderAsPaidAsync((GateOtcMarkOrderPaidRequest)input),
        _ => client.Otc.CreateFiatOrderAsync((GateOtcFiatOrderRequest)input),
    };
    private static string WireName(Type type, string property)
        => type.GetProperty(property)!.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Cast<JsonPropertyAttribute>().Single().PropertyName!;
    private static int CountBytes(byte[] body, byte[] bytes)
    {
        var count = 0;
        for (var i = 0; i <= body.Length - bytes.Length; i++) if (body.AsSpan(i, bytes.Length).SequenceEqual(bytes)) count++;
        return count;
    }
    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("api.gateio.ws", request.RequestUri.Host);
        Assert.Empty(request.RequestUri.Query);
        var hash = Convert.ToHexString(SHA512.HashData(request.ContentBytes)).ToLowerInvariant();
        var payload = $"POST\n{request.RequestUri.AbsolutePath}\n\n{hash}\n{Assert.Single(request.Headers["Timestamp"])}";
        var signature = Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant();
        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }
    private static GateRestApiClient Client(RecordingHttpMessageHandler handler, bool raw = false, TestLogger? logger = null)
    {
        var client = new GateRestApiClient(logger!, new GateRestApiClientOptions { HttpClient = new HttpClient(handler), RawResponse = raw });
        client.SetApiCredentials("key", "secret");
        return client;
    }
    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });
}
