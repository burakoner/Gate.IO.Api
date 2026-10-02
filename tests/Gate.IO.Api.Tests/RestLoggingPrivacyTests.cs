using Gate.IO.Api.Otc;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Text;

namespace Gate.IO.Api.Tests;

[Trait("Category", "Unit")]
public class RestLoggingPrivacyTests
{
    [Theory]
    [InlineData(HttpStatusCode.OK, false)]
    [InlineData(HttpStatusCode.OK, true)]
    [InlineData(HttpStatusCode.BadRequest, false)]
    [InlineData(HttpStatusCode.BadRequest, true)]
    [InlineData(HttpStatusCode.TooManyRequests, false)]
    [InlineData(HttpStatusCode.TooManyRequests, true)]
    [InlineData(HttpStatusCode.InternalServerError, false)]
    [InlineData(HttpStatusCode.InternalServerError, true)]
    public async Task Logs_keep_metadata_not_S3_credentials_headers_or_raw_bodies(HttpStatusCode status, bool rawResponse)
    {
        var json = JsonFixture.Read("Docs/Otc/pre_upload.success.json");
        if (status != HttpStatusCode.OK)
            json = "{\"label\":\"PRIVATE_LABEL\",\"message\":\"SECRET_IBAN\",\"data\":\"AKIAEXAMPLE\"}";
        var logger = new TestLogger();
        var handler = Handler(json, status);
        using var client = Client(logger, handler, rawResponse);
        var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png);
        Assert.Equal(status == HttpStatusCode.OK, result.Success);
        Assert.Equal(status, result.Response!.StatusCode);
        // ApiSharp retains HTTP-error bodies in Raw even when RawResponse is disabled; preserve that behavior.
        Assert.Equal(rawResponse || status != HttpStatusCode.OK ? json : "", result.Raw);
        if (!result.Success) Assert.Equal("SECRET_IBAN", result.Error!.Message);
        var wire = Assert.Single(handler.Requests);
        AssertMetadataOnly(logger, wire);
        Assert.Contains(logger.Entries, x => x.Message.Contains("HTTPStatus="));
        Assert.Contains(logger.Entries, x => x.Message.Contains("/api/v4/otc/upload/pre_upload"));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Malformed_raw_credentials_do_not_enter_logger_or_attached_exceptions(bool rawResponse)
    {
        var json = JsonFixture.Read("Docs/Otc/pre_upload.success.json").TrimEnd();
        json = json[..^1] + ",\"SECRET_IBAN\":!}";
        var logger = new TestLogger();
        var handler = Handler(json);
        using var client = Client(logger, handler, rawResponse);
        var result = await client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png);
        Assert.False(result.Success);
        Assert.Null(result.Error!.Data);
        AssertMetadataOnly(logger, Assert.Single(handler.Requests));
    }

    [Fact]
    public async Task Bank_fields_and_file_bytes_stay_out_of_logs_and_recorded_request_metadata()
    {
        var logger = new TestLogger();
        var handler = Handler(JsonFixture.Read("Docs/Otc/bank_create.success.json"));
        using var client = Client(logger, handler);
        var result = await client.Otc.CreateBankCardAsync(new()
        {
            BankAccountName = "PRIVATE_NAME", BankName = "PRIVATE_BANK", BankCountry = "GB", BankAddress = "PRIVATE_ADDRESS",
            Iban = "SECRET_IBAN", Swift = "PRIVATE_SWIFT",
            DocumentationUpload = new() { Content = Encoding.UTF8.GetBytes("PRIVATE_FILE_CONTENT"), FileName = "PRIVATE_PROOF.pdf" },
        });
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("[multipart/form-data content omitted]", result.Request!.Body);
        AssertMetadataOnly(logger, Assert.Single(handler.Requests));
        foreach (var value in new[] { "PRIVATE_NAME", "PRIVATE_BANK", "PRIVATE_ADDRESS", "PRIVATE_SWIFT", "PRIVATE_PROOF.pdf", "PRIVATE_FILE_CONTENT" })
            Assert.DoesNotContain(value, string.Join("\n", logger.Entries.Select(x => x.Message)));
    }

    [Fact]
    public async Task Unrelated_REST_module_still_returns_error_details_without_logging_them()
    {
        var logger = new TestLogger();
        var handler = Handler("{\"label\":\"PRIVATE_LABEL\",\"message\":\"SECRET_IBAN\"}", HttpStatusCode.BadRequest);
        using var client = Client(logger, handler, rawResponse: true);
        var result = await client.Alpha.GetCurrenciesAsync(limit: 1);
        Assert.False(result.Success);
        Assert.Equal("SECRET_IBAN", result.Error!.Message);
        Assert.Equal("PRIVATE_LABEL", result.Error.Data);
        Assert.NotEmpty(result.Raw!);
        AssertMetadataOnly(logger, Assert.Single(handler.Requests));
        Assert.Contains(logger.Entries, x => x.Message.Contains("Gate REST request failed"));
    }

    [Fact]
    public async Task Exceptions_are_rethrown_unchanged_but_only_the_type_is_logged()
    {
        var logger = new TestLogger();
        var handler = new RecordingHttpMessageHandler(_ => throw new InvalidOperationException("SECRET_IBAN PRIVATE_EXCEPTION_BODY"));
        using var client = Client(logger, handler);
        var error = await Assert.ThrowsAsync<InvalidOperationException>(() => client.Otc.CreatePreUploadAsync(GateOtcUploadContentType.Png));
        Assert.Equal("SECRET_IBAN PRIVATE_EXCEPTION_BODY", error.Message);
        AssertMetadataOnly(logger, Assert.Single(handler.Requests));
        Assert.Contains(logger.Entries, x => x.Message.Contains("ExceptionType=InvalidOperationException"));
    }

    private static void AssertMetadataOnly(TestLogger logger, RecordedHttpRequest wire)
    {
        var diagnostics = string.Join("\n", logger.Entries.Select(x => x.Message));
        foreach (var value in new[] { "SECRET_API_KEY", "SECRET_API_SECRET", "AKIAEXAMPLE", "EXAMPLE_SIGNATURE", "PRIVATE_LABEL", "SECRET_IBAN", "PRIVATE_EXCEPTION_BODY" })
            Assert.DoesNotContain(value, diagnostics);
        if (wire.Headers.TryGetValue("SIGN", out var signatures)) Assert.DoesNotContain(Assert.Single(signatures), diagnostics);
        Assert.All(logger.Entries, x => Assert.Null(x.Exception));
        Assert.Contains(logger.Entries, x => x.Message.Contains("Gate REST request started"));
    }
    private static GateRestApiClient Client(TestLogger logger, RecordingHttpMessageHandler handler, bool rawResponse = false)
    {
        var client = new GateRestApiClient(logger, new GateRestApiClientOptions { HttpClient = new HttpClient(handler), RawResponse = rawResponse });
        client.SetApiCredentials("SECRET_API_KEY", "SECRET_API_SECRET");
        return client;
    }
    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });
}
