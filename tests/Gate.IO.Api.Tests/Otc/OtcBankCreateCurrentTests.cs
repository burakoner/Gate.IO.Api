using Gate.IO.Api.Otc;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Otc;

[Trait("Category", "Contract")]
public class OtcBankCreateCurrentTests
{
    [Theory]
    [InlineData("otc_temp/42/bank/proof.png", "image/png")]
    [InlineData("b3RjX3RlbXAvNDIvYmFuay9wcm9vZi5wbmc=", "aW1hZ2UvcG5n")]
    public async Task Key_and_MIME_are_sent_unchanged_as_signed_multipart_without_uploading(string key, string mime)
    {
        var handler = Handler(Fixture().ToString());
        using var client = Client(handler);
        var input = KeyRequest();
        input.DocumentationFileKey = key;
        input.FileType = mime;
        input.BankAccountName = "Ada İpek / 不转换";
        input.RemittanceLineNumber = "";
        input.AgentBankName = "Correspondent Bank";
        input.AgentBankSwift = "CORRGB2L";
        var saved = JsonConvert.SerializeObject(input);
        Assert.Equal(input, JsonConvert.DeserializeObject<GateOtcBankCreateRequest>(saved));
        var result = await client.Otc.CreateBankCardAsync(input);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(762L, result.Data.BankId);
        Assert.Equal(0, result.Data.Code);
        Assert.Equal("success", result.Data.Message);
        Assert.Equal(1769998217L, result.Data.Timestamp);
        var wire = Assert.Single(handler.Requests);
        Assert.Equal("/api/v4/otc/bank/create", wire.RequestUri.AbsolutePath);
        Assert.Equal("api.gateio.ws", wire.RequestUri.Host);
        Assert.Equal(HttpMethod.Post, wire.Method);
        Assert.Empty(wire.RequestUri.Query);
        foreach (var property in JObject.FromObject(input).Properties())
            Assert.Contains($"name=\"{property.Name}\"\r\n\r\n{property.Value}\r\n", wire.Content);
        Assert.Equal(11, wire.Content.Split("Content-Disposition:").Length - 1);
        Assert.DoesNotContain("filename", wire.Content);
        Assert.DoesNotContain("documentation_file\"", wire.Content);
        Assert.Equal("[multipart/form-data content omitted]", result.Request!.Body);
        AssertSignature(wire);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Raw_and_legacy_Base64_inputs_emit_exact_file_bytes_and_sign_those_bytes(bool legacy)
    {
        byte[] bytes = [0, 1, 255, 254, 195, 40, 13, 10, 128]; // Deliberately invalid UTF-8, not text/file paths.
        var handler = Handler(Fixture().ToString());
        using var client = Client(handler);
        var input = KeyRequest();
        input.DocumentationFileKey = null!;
        input.FileType = null!;
        if (legacy) input.DocumentationFile = Convert.ToBase64String(bytes);
        else input.DocumentationUpload = new() { Content = bytes, FileName = "kanıt.pdf", ContentType = "application/pdf" };
        var saved = JsonConvert.SerializeObject(input);
        var copy = JsonConvert.DeserializeObject<GateOtcBankCreateRequest>(saved)!;
        if (!legacy) Assert.Equal(bytes, copy.DocumentationUpload.Content);
        var result = await client.Otc.CreateBankCardAsync(copy);
        Assert.True(result.Success, result.Error?.ToString());
        var wire = Assert.Single(handler.Requests);
        Assert.Equal(7, wire.Content.Split("Content-Disposition:").Length - 1);
        Assert.Contains("name=\"documentation_file\"; filename=", wire.Content);
        Assert.DoesNotContain("documentation_upload", wire.Content);
        var fileStart = wire.ContentBytes.AsSpan().IndexOf(Encoding.UTF8.GetBytes(legacy
            ? "Content-Type: application/octet-stream\r\n\r\n" : "Content-Type: application/pdf\r\n\r\n"));
        Assert.True(fileStart >= 0);
        fileStart += Encoding.UTF8.GetByteCount(legacy ? "Content-Type: application/octet-stream\r\n\r\n" : "Content-Type: application/pdf\r\n\r\n");
        Assert.Equal(bytes, wire.ContentBytes.Skip(fileStart).Take(bytes.Length));
        Assert.Equal("[multipart/form-data content omitted]", result.Request!.Body);
        AssertSignature(wire);
        Assert.NotEqual(Encoding.UTF8.GetBytes(wire.Content), wire.ContentBytes);
        Assert.DoesNotContain(Convert.ToBase64String(bytes), wire.Content);
    }

    [Theory]
    [InlineData("code")]
    [InlineData("message")]
    [InlineData("data")]
    [InlineData("data.bank_id")]
    [InlineData("data.status")]
    public async Task Required_acknowledgement_fields_cannot_be_omitted_or_null(string path)
    {
        foreach (var omit in new[] { false, true })
        {
            var json = Fixture();
            var property = json.SelectToken(path)!.Parent!;
            if (omit) property.Remove(); else ((JProperty)property).Value = JValue.CreateNull();
            await Failed(json.ToString());
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateOtcBankCreateResponse>(json.ToString()));
        }
    }

    [Theory]
    [InlineData("code", "\"0\"")]
    [InlineData("code", "0.1")]
    [InlineData("code", "true")]
    [InlineData("code", "2147483648")]
    [InlineData("message", "123")]
    [InlineData("data", "[]")]
    [InlineData("data.bank_id", "\"762\"")]
    [InlineData("data.bank_id", "762.1")]
    [InlineData("data.bank_id", "true")]
    [InlineData("data.bank_id", "9223372036854775808")]
    [InlineData("data.status", "\"0\"")]
    [InlineData("data.status", "0.1")]
    [InlineData("data.status", "false")]
    [InlineData("data.status", "2147483648")]
    [InlineData("timestamp", "\"1769998217\"")]
    [InlineData("timestamp", "0.1")]
    [InlineData("timestamp", "false")]
    [InlineData("timestamp", "9223372036854775808")]
    public async Task Current_integer_string_and_object_types_are_exact(string path, string value)
    {
        var json = Fixture();
        ((JProperty)json.SelectToken(path)!.Parent!).Value = JToken.Parse(value);
        await Failed(json.ToString());
    }

    [Theory]
    [InlineData(0L, 0)]
    [InlineData(2147483648L, 19)]
    [InlineData(long.MaxValue, -1)]
    public async Task Int64_ids_and_unknown_review_statuses_survive_without_approval_inference(long id, int status)
    {
        var json = Fixture();
        json["data"]!["bank_id"] = id;
        json["data"]!["status"] = status;
        var handler = Handler(json.ToString());
        using var client = Client(handler);
        var result = await client.Otc.CreateBankCardAsync(KeyRequest());
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(id, result.Data.BankId);
        Assert.Equal(status, result.Data.Status);
        Assert.True(JToken.DeepEquals(json["data"], JObject.FromObject(result.Data)));
        Assert.True(JToken.DeepEquals(json, JObject.FromObject(json.ToObject<GateOtcBankCreateResponse>()!)));
    }

    [Fact]
    public async Task Timestamp_is_optional_raw_Int64_not_a_guessed_date_or_required_acknowledgement()
    {
        foreach (long? time in new long?[] { null, 0, long.MinValue, long.MaxValue })
        {
            var json = Fixture();
            if (time == null) json.Property("timestamp")!.Remove(); else json["timestamp"] = time;
            var handler = Handler(json.ToString());
            using var client = Client(handler);
            var result = await client.Otc.CreateBankCardAsync(KeyRequest());
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(time, result.Data.Timestamp);
        }
        var explicitNull = Fixture();
        explicitNull["timestamp"] = JValue.CreateNull();
        using var nullable = Client(Handler(explicitNull.ToString()));
        Assert.Null((await nullable.Otc.CreateBankCardAsync(KeyRequest())).Data.Timestamp);
    }

    [Theory]
    [InlineData("Invalid parameters file_key")]
    [InlineData("Invalid parameters file not uploaded")]
    public async Task Business_errors_are_errors_even_for_HTTP_200_without_success_data(string message)
    {
        var handler = Handler(new JObject { ["code"] = 10010400, ["message"] = message }.ToString());
        using var client = Client(handler);
        var result = await client.Otc.CreateBankCardAsync(KeyRequest());
        Assert.False(result.Success);
        Assert.Equal(10010400, result.Error!.Code);
        Assert.Equal(message, result.Error.Message);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("")]
    [InlineData("null")]
    [InlineData("[]")]
    [InlineData("{}")]
    [InlineData("true")]
    [InlineData("{\"code\":0,\"private\":!}")]
    public async Task Missing_malformed_or_wrong_containers_never_mean_a_created_bank(string json) => await Failed(json);

    [Fact]
    public async Task Preflight_rejects_missing_required_fields_and_ambiguous_or_invalid_proofs_without_I_O()
    {
        var handler = Handler(Fixture().ToString());
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.Otc.CreateBankCardAsync(null!));
        foreach (var property in new[] { "BankAccountName", "BankName", "BankCountry", "BankAddress", "Iban", "Swift" })
        {
            var request = KeyRequest();
            typeof(GateOtcBankCreateRequest).GetProperty(property)!.SetValue(request, " ");
            await Assert.ThrowsAsync<ArgumentException>(() => client.Otc.CreateBankCardAsync(request));
        }
        foreach (var mode in Enumerable.Range(0, 11))
        {
            var request = KeyRequest();
            switch (mode)
            {
                case 0: request.DocumentationFileKey = null!; break;
                case 1: request.FileType = null!; break;
                case 2: request.DocumentationFile = "QQ=="; break;
                case 3: request.DocumentationUpload = new() { Content = [1], FileName = "a" }; break;
                case 4: request.DocumentationFileKey = " "; break;
                case 5: request.FileType = " "; break;
                default:
                    request.DocumentationFileKey = null!;
                    request.DocumentationUpload = new() { Content = [1], FileName = "a" };
                    if (mode == 6) { request.DocumentationUpload = null!; request.DocumentationFile = "NOT-BASE64"; }
                    if (mode == 7) request.DocumentationUpload.Content = [];
                    if (mode == 8) request.DocumentationUpload.FileName = "x\r\nInjected: true";
                    if (mode == 9) request.DocumentationUpload.FileName = "../a";
                    if (mode == 10) request.DocumentationUpload.ContentType = "image/png\r\nInjected: true";
                    break;
            }
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Otc.CreateBankCardAsync(request));
        }
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task HTTP_errors_preserve_metadata_without_retry(HttpStatusCode status)
    {
        var handler = Handler("{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Rejected\"}", status);
        using var client = Client(handler);
        var result = await client.Otc.CreateBankCardAsync(KeyRequest());
        Assert.False(result.Success);
        Assert.Equal(status, result.Response!.StatusCode);
        Assert.Equal("CONTRACT_TEST_ERROR", result.Error!.Data);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task HTTP_error_statuses_also_retain_explicit_OTC_business_codes()
    {
        foreach (var status in new[] { HttpStatusCode.BadRequest, HttpStatusCode.TooManyRequests, HttpStatusCode.InternalServerError })
        {
            var handler = Handler("{\"code\":10010400,\"message\":\"Invalid parameters file_key\"}", status);
            using var client = Client(handler);
            var result = await client.Otc.CreateBankCardAsync(KeyRequest());
            Assert.False(result.Success);
            Assert.Equal(10010400, result.Error!.Code);
            Assert.Equal("Invalid parameters file_key", result.Error.Message);
            Assert.Equal(status, result.Response!.StatusCode);
            Assert.Single(handler.Requests);
        }
    }

    [Theory]
    [InlineData("bank_account_name", true)]
    [InlineData("bank_name", true)]
    [InlineData("bank_country", true)]
    [InlineData("bank_address", true)]
    [InlineData("iban", true)]
    [InlineData("swift", true)]
    [InlineData("bank_account_name", false)]
    [InlineData("bank_name", false)]
    [InlineData("bank_country", false)]
    [InlineData("bank_address", false)]
    [InlineData("iban", false)]
    [InlineData("swift", false)]
    public void Saved_requests_require_all_six_nonnull_string_values(string key, bool omitted)
    {
        var json = JObject.FromObject(KeyRequest());
        if (omitted) json.Property(key)!.Remove(); else json[key] = JValue.CreateNull();
        Assert.Throws<JsonSerializationException>(() => json.ToObject<GateOtcBankCreateRequest>());
    }

    [Theory]
    [InlineData("bank_account_name")]
    [InlineData("bank_name")]
    [InlineData("bank_country")]
    [InlineData("bank_address")]
    [InlineData("iban")]
    [InlineData("swift")]
    [InlineData("remittance_line_number")]
    [InlineData("agent_bank_name")]
    [InlineData("agent_bank_swift")]
    [InlineData("documentation_file")]
    [InlineData("documentation_file_key")]
    [InlineData("file_type")]
    public void Saved_form_values_cannot_silently_coerce_numbers_to_strings(string key)
    {
        var json = JObject.FromObject(KeyRequest());
        json[key] = 123;
        Assert.Throws<JsonSerializationException>(() => json.ToObject<GateOtcBankCreateRequest>());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Raw_response_mode_preserves_strings_and_complete_envelope_metadata(bool rawResponse)
    {
        var json = Fixture();
        json["message"] = "2026-10-02T08:12:13+03:00";
        var handler = Handler(json.ToString());
        using var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler), RawResponse = rawResponse });
        client.SetApiCredentials("key", "secret");
        var result = await client.Otc.CreateBankCardAsync(KeyRequest());
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal((string?)json["message"], result.Data.Message);
        Assert.Equal(1769998217L, result.Data.Timestamp);
        var saved = JsonConvert.SerializeObject(new GateOtcBankCreateResponse { Code = result.Data.Code, Message = result.Data.Message, Data = result.Data, Timestamp = result.Data.Timestamp });
        Assert.Equal(result.Data.Message, JsonConvert.DeserializeObject<GateOtcBankCreateResponse>(saved)!.Message);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public void Saved_request_strings_are_never_normalized_into_dates()
    {
        var input = KeyRequest();
        input.BankAccountName = "2026-10-02T12:00:00+03:00";
        input.AgentBankName = "2026-10-02T00:00:00Z";
        var copy = JsonConvert.DeserializeObject<GateOtcBankCreateRequest>(JsonConvert.SerializeObject(input))!;
        Assert.Equal(input.BankAccountName, copy.BankAccountName);
        Assert.Equal(input.AgentBankName, copy.AgentBankName);
    }

    [Fact]
    public void Saved_file_bytes_cannot_be_rounded_or_coerced_from_invalid_array_items()
    {
        foreach (var value in new[] { "[0.1]", "[true]", "[256]", "[-1]" })
        {
            var json = JObject.FromObject(KeyRequest());
            json.Property("documentation_file_key")!.Remove();
            json.Property("file_type")!.Remove();
            json["documentation_upload"] = new JObject { ["content"] = JToken.Parse(value), ["file_name"] = "a.png" };
            var error = Record.Exception(() => json.ToObject<GateOtcBankCreateRequest>());
            Assert.True(error is JsonException or OverflowException);
        }
    }

    private static GateOtcBankCreateRequest KeyRequest() => new()
    {
        BankAccountName = "Ada", BankName = "Bank", BankCountry = "GB", BankAddress = "1 Street", Iban = "GB123", Swift = "BANKGB2L",
        DocumentationFileKey = "opaque-key", FileType = "image/png",
    };
    private static JObject Fixture() => (JObject)JsonFixture.Parse("Docs/Otc/bank_create.success.json");
    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });
    private static GateRestApiClient Client(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }
    private static async Task Failed(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Otc.CreateBankCardAsync(KeyRequest());
        Assert.False(result.Success);
        Assert.Null(result.Error!.Data);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }
    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var contentType = Assert.Single(request.Headers["Content-Type"]);
        Assert.StartsWith("multipart/form-data; boundary=", contentType);
        Assert.DoesNotContain("charset", contentType);
        var bodyHash = Convert.ToHexString(SHA512.HashData(request.ContentBytes)).ToLowerInvariant();
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var payload = $"POST\n{request.RequestUri.AbsolutePath}\n\n{bodyHash}\n{timestamp}";
        Assert.Equal(Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant(), Assert.Single(request.Headers["SIGN"]));
    }
}
