using Gate.IO.Api.Otc;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Otc;

[Trait("Category", "Contract")]
public class OtcPreUploadCurrentTests
{
    private const string PolicyKeys = "key,Content-Type,X-Amz-Credential,X-Amz-Algorithm,X-Amz-Date,Policy,X-Amz-Signature";

    public static IEnumerable<object?[]> Instructions()
    {
        var types = new[] { (GateOtcUploadContentType.Png, "aW1hZ2UvcG5n"), (GateOtcUploadContentType.Jpeg, "aW1hZ2UvanBlZw=="),
            (GateOtcUploadContentType.Jpg, "aW1hZ2UvanBn"), (GateOtcUploadContentType.Pdf, "YXBwbGljYXRpb24vcGRm") };
        (GateOtcUploadScene?, string?)[] scenes = [(null, null), (GateOtcUploadScene.General, "general"), (GateOtcUploadScene.Bank, "bank"),
            (GateOtcUploadScene.Assessment, "assessment"), (GateOtcUploadScene.Credit, "credit")];
        foreach (var (content, wire) in types)
            foreach (var (scene, sceneWire) in scenes) yield return [content, wire, scene, sceneWire];
    }

    [Theory]
    [MemberData(nameof(Instructions))]
    public async Task All_MIME_and_scene_values_use_exact_signed_JSON_without_uploading(GateOtcUploadContentType contentType, string wire, GateOtcUploadScene? scene, string? sceneWire)
    {
        var handler = Handler(Fixture().ToString());
        using var client = Client(handler);
        var input = new GateOtcUploadPreUploadRequest { ContentType = contentType, Scene = scene };
        var result = await client.Otc.CreatePreUploadAsync(input);
        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/otc/upload/pre_upload", request.RequestUri.AbsolutePath);
        Assert.Equal("api.gateio.ws", request.RequestUri.Host);
        Assert.Empty(request.RequestUri.Query);
        var expected = new JObject { ["content_type"] = wire };
        if (sceneWire != null) expected["scene"] = sceneWire;
        Assert.True(JToken.DeepEquals(expected, JObject.Parse(request.Content)));
        Assert.True(JToken.DeepEquals(expected, JObject.FromObject(input)));
        Assert.Equal(input, JsonConvert.DeserializeObject<GateOtcUploadPreUploadRequest>(JsonConvert.SerializeObject(input)));
        AssertSignature(request);
        Assert.Equal(0, result.Data.Code);
        Assert.Equal("success", result.Data.Message);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1788162685).UtcDateTime, result.Data.Timestamp);
        Assert.Equal(5400, result.Data.Data.ExpiresIn);
        Assert.Equal((string?)Fixture()["data"]!["file_key"], result.Data.Data.FileKey);
        Assert.Equal((string?)Fixture()["data"]!["url"], result.Data.Data.Url);
        Assert.Equal(7, result.Data.Data.Fields.Count);
        Assert.True(JToken.DeepEquals(Fixture(), JObject.FromObject(result.Data)));
    }

    [Fact]
    public async Task Convenience_overload_omits_scene_and_invalid_inputs_never_send()
    {
        var handler = Handler(Fixture().ToString());
        using var client = Client(handler);
        Assert.True((await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png)).Success);
        Assert.Null(JObject.Parse(Assert.Single(handler.Requests).Content)["scene"]);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.Otc.CreatePreUploadAsync((GateOtcUploadPreUploadRequest)null!));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Otc.CreatePreUploadAsync((GateOtcUploadContentType)0));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Otc.CreatePreUploadAsync((GateOtcUploadContentType)255));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png, (GateOtcUploadScene)0));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png, (GateOtcUploadScene)255));
        Assert.Single(handler.Requests);
    }

    public static IEnumerable<object[]> RequiredPaths() => new[] { "code", "message", "data", "timestamp", "data.file_key", "data.url", "data.fields", "data.expires_in" }
        .Concat(PolicyKeys.Split(',').Select(x => "data.fields." + x)).Select(x => new object[] { x });

    [Theory]
    [MemberData(nameof(RequiredPaths))]
    public async Task Every_required_field_rejects_omission_and_null_without_fabricated_credentials(string path)
    {
        foreach (var missing in new[] { true, false })
        {
            var json = Fixture();
            var property = Property(json, path);
            if (missing) property.Remove(); else property.Value = JValue.CreateNull();
            await Failed(json.ToString());
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateOtcUploadPreUploadResponse>(json.ToString()));
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("null")]
    [InlineData("[]")]
    [InlineData("{}")]
    [InlineData("true")]
    public async Task Missing_or_wrong_root_container_is_not_a_credential_acknowledgement(string json) => await Failed(json);

    [Theory]
    [InlineData("code", "\"0\"")]
    [InlineData("code", "0.0")]
    [InlineData("code", "true")]
    [InlineData("code", "2147483648")]
    [InlineData("message", "123")]
    [InlineData("data", "[]")]
    [InlineData("data.file_key", "123")]
    [InlineData("data.url", "false")]
    [InlineData("data.fields", "[]")]
    [InlineData("data.fields.key", "123")]
    [InlineData("data.fields.Policy", "true")]
    [InlineData("data.expires_in", "\"5400\"")]
    [InlineData("data.expires_in", "0.1")]
    [InlineData("data.expires_in", "2147483648")]
    [InlineData("timestamp", "\"1788162685\"")]
    [InlineData("timestamp", "1788162685.1")]
    [InlineData("timestamp", "true")]
    [InlineData("timestamp", "9223372036854775807")]
    public async Task Response_values_never_coerce_types_or_guess_timestamp_units(string path, string value)
    {
        var json = Fixture();
        Property(json, path).Value = JToken.Parse(value);
        await Failed(json.ToString());
    }

    [Theory]
    [InlineData(10010400)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public async Task HTTP_200_business_errors_preserve_code_and_message_even_without_success_fields(int code)
    {
        var handler = Handler($"{{\"code\":{code},\"message\":\"content type is required.\"}}");
        using var client = Client(handler);
        var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png);
        Assert.False(result.Success);
        Assert.Equal(code, result.Error!.Code);
        Assert.Contains("content type is required.", result.Error.Message);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Opaque_policy_fields_are_case_sensitive_preserve_extra_strings_and_do_not_trigger_network_calls(bool rawResponse)
    {
        var json = Fixture();
        var fields = (JObject)json["data"]!["fields"]!;
        fields["x-amz-security-token"] = "future-token+/==";
        fields["extra_date_string"] = "2026-08-31T07:51:25+03:00";
        var handler = Handler(json.ToString());
        using var client = Client(handler, rawResponse);
        var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png, GateOtcUploadScene.Bank);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(9, result.Data.Data.Fields.Count);
        Assert.Equal("future-token+/==", result.Data.Data.Fields["x-amz-security-token"]);
        Assert.Equal("2026-08-31T07:51:25+03:00", result.Data.Data.Fields["extra_date_string"]);
        Assert.Equal((string?)fields["Policy"], result.Data.Data.Fields["Policy"]);
        Assert.Equal((string?)fields["X-Amz-Credential"], result.Data.Data.Fields["X-Amz-Credential"]);
        Assert.False(result.Data.Data.Fields.ContainsKey("policy"));
        Assert.Single(handler.Requests);
        var saved = JsonConvert.SerializeObject(result.Data);
        var copy = JsonConvert.DeserializeObject<GateOtcUploadPreUploadResponse>(saved)!;
        Assert.Equal(result.Data.Data.Fields["extra_date_string"], copy.Data.Fields["extra_date_string"]);
        fields["policy"] = fields["Policy"]!.DeepClone();
        fields.Property("Policy")!.Remove();
        await Failed(json.ToString());
    }

    [Fact]
    public async Task Explicit_zero_and_different_expiry_are_preserved_without_lifetime_assumptions()
    {
        foreach (var expiry in new[] { 0, 1200 })
        {
            var json = Fixture();
            json["timestamp"] = 0;
            json["data"]!["expires_in"] = expiry;
            var handler = Handler(json.ToString());
            using var client = Client(handler);
            var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Pdf);
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), result.Data.Timestamp);
            Assert.Equal(expiry, result.Data.Data.ExpiresIn);
            Assert.Single(handler.Requests);
        }
    }

    [Theory]
    [InlineData("null")]
    [InlineData("false")]
    [InlineData("123")]
    [InlineData("0.1")]
    [InlineData("{}")]
    [InlineData("[]")]
    public async Task Future_policy_fields_must_still_be_strings(string value)
    {
        var json = Fixture();
        json["data"]!["fields"]!["future_field"] = JToken.Parse(value);
        await Failed(json.ToString());
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateOtcUploadPreUploadResponse>(json.ToString()));
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"content_type\":null}")]
    [InlineData("{\"content_type\":\"image/png\"}")]
    [InlineData("{\"content_type\":1}")]
    [InlineData("{\"content_type\":\"1\"}")]
    [InlineData("{\"content_type\":\"UNKNOWN\"}")]
    [InlineData("{\"content_type\":\"aW1hZ2UvcG5n\",\"scene\":\"BANK\"}")]
    [InlineData("{\"content_type\":\"aW1hZ2UvcG5n\",\"scene\":1}")]
    public void Saved_requests_reject_missing_unknown_or_plaintext_instructions(string json)
        => Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateOtcUploadPreUploadRequest>(json));

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task Transport_errors_keep_http_and_label_metadata_without_retry(HttpStatusCode status)
    {
        var handler = Handler("{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Rejected\"}", status);
        using var client = Client(handler);
        var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png);
        Assert.False(result.Success);
        Assert.Equal(status, result.Response!.StatusCode);
        Assert.Equal("CONTRACT_TEST_ERROR", result.Error!.Data);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Malformed_credentials_do_not_enter_error_payloads_or_wrapper_logs(bool malformedJson)
    {
        var json = Fixture();
        if (!malformedJson) json["data"]!["expires_in"] = true;
        var logger = new TestLogger();
        var wire = json.ToString();
        var handler = Handler(malformedJson ? wire[..^1] + ",\"broken\":!}" : wire);
        using var client = new GateRestApiClient(logger, new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png);
        Assert.False(result.Success);
        Assert.Null(result.Error!.Data);
        var diagnostics = result.Error.ToString() + string.Join("\n", logger.Entries.Select(x => x.Message));
        Assert.DoesNotContain("AKIAEXAMPLE", diagnostics);
        Assert.DoesNotContain("EXAMPLE_SIGNATURE", diagnostics);
        Assert.DoesNotContain((string)json["data"]!["fields"]!["Policy"]!, diagnostics);
    }

    [Fact]
    public async Task Opted_in_raw_capture_stays_explicit_but_parser_errors_do_not_duplicate_credentials()
    {
        foreach (var malformedJson in new[] { false, true })
        {
            var json = Fixture();
            if (!malformedJson) json["data"]!["expires_in"] = false;
            var wire = json.ToString();
            if (malformedJson) wire = wire[..^1] + ",\"broken\":!}";
            var handler = Handler(wire);
            using var client = Client(handler, rawResponse: true);
            var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png);
            Assert.False(result.Success);
            Assert.Null(result.Error!.Data);
            Assert.DoesNotContain("AKIAEXAMPLE", result.Error.ToString());
            Assert.Equal(wire, result.Raw);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            Assert.Single(handler.Requests);
        }
    }

    private static JObject Fixture() => (JObject)JsonFixture.Parse("Docs/Otc/pre_upload.success.json");

    private static JProperty Property(JObject root, string path)
    {
        var parts = path.Split('.');
        var parent = root;
        foreach (var key in parts.Take(parts.Length - 1)) parent = (JObject)parent[key]!;
        return parent.Property(parts[^1])!;
    }

    private static async Task Failed(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png);
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler, bool rawResponse = false)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler), RawResponse = rawResponse });
        client.SetApiCredentials("key", "secret");
        return client;
    }

    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var bodyHash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var payload = $"POST\n{request.RequestUri.AbsolutePath}\n\n{bodyHash}\n{timestamp}";
        Assert.Equal(Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant(), Assert.Single(request.Headers["SIGN"]));
    }
}
