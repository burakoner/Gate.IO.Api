using ApiSharp.Authentication;
using ApiSharp.Enums;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Authentication;

[Trait("Category", "Unit")]
public class AuthenticationContractTests
{
    public static TheoryData<string> RestSignatureExamples => new()
    {
        "Docs/Authentication/rest-signature.get-futures-orders.json",
        "Docs/Authentication/rest-signature.create-futures-order.json",
    };

    [Theory]
    [MemberData(nameof(RestSignatureExamples))]
    public void Rest_signature_matches_documented_api_v4_examples(string fixturePath)
    {
        var example = JsonFixture.Deserialize<RestSignatureExample>(fixturePath);
        var authentication = new GateAuthentication(new ApiCredentials(example.Key, example.Secret));

        var signature = authentication.CreateRestSignature(
            new HttpMethod(example.Method),
            example.Path,
            example.QueryString,
            example.Body,
            example.Timestamp);

        Assert.Equal(example.ExpectedSignature, signature);
    }

    [Fact]
    public void Unsigned_rest_request_does_not_add_authentication_headers()
    {
        var authentication = new GateAuthentication(new ApiCredentials("key", "secret"));
        var headers = new SortedDictionary<string, string>();

        authentication.AuthenticateRestApi(
            new GateRestApiClient(),
            new Uri("https://api.gateio.ws/api/v4/spot/currency_pairs"),
            HttpMethod.Get,
            signed: false,
            serialization: ArraySerialization.MultipleValues,
            query: new SortedDictionary<string, object>(),
            body: new SortedDictionary<string, object>(),
            bodyContent: string.Empty,
            headers: headers);

        Assert.Empty(headers);
    }

    [Fact]
    public void Signed_rest_request_requires_api_credentials()
    {
        var authentication = new GateAuthentication(new ApiCredentials(string.Empty, string.Empty));

        var exception = Assert.Throws<ArgumentException>(() => authentication.AuthenticateRestApi(
            new GateRestApiClient(),
            new Uri("https://api.gateio.ws/api/v4/wallet/total_balance"),
            HttpMethod.Get,
            signed: true,
            serialization: ArraySerialization.MultipleValues,
            query: new SortedDictionary<string, object>(),
            body: new SortedDictionary<string, object>(),
            bodyContent: string.Empty,
            headers: new SortedDictionary<string, string>()));

        Assert.Contains("No valid API credentials", exception.Message);
    }

    private sealed class RestSignatureExample
    {
        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;

        [JsonProperty("secret")]
        public string Secret { get; set; } = string.Empty;

        [JsonProperty("method")]
        public string Method { get; set; } = string.Empty;

        [JsonProperty("path")]
        public string Path { get; set; } = string.Empty;

        [JsonProperty("query_string")]
        public string QueryString { get; set; } = string.Empty;

        [JsonProperty("body")]
        public string Body { get; set; } = string.Empty;

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonProperty("expected_signature")]
        public string ExpectedSignature { get; set; } = string.Empty;
    }
}
