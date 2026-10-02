using ApiSharp.Converters;
using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.CrossEx;

[Trait("Category", "Contract")]
public class CrossExMarginModeContractTests
{
    private const string Symbol = "HYPERLIQUID_FUTURE_CXMT_USDC";

    [Theory]
    [InlineData(Symbol)]
    [InlineData("BINANCE_FUTURE_ADA_USDT")]
    [InlineData("LIGHTER_FUTURE_ADA_USDC")]
    [InlineData("FUTURE_VENUE_FUTURE_TOKEN_USDC")]
    [InlineData("HYPERLIQUID_FUTURE_A&symbol=OTHER?/#%_USDC")]
    public async Task GET_is_signed_and_scoped_to_one_unchanged_query_value_without_a_Hyperliquid_only_constraint(string symbol)
    {
        var handler = Handler(Fixture());
        using var client = Client(handler);
        var input = new GateCrossExMarginModeQueryRequest { Symbol = symbol };
        var saved = JsonConvert.SerializeObject(input);
        Assert.Equal(input, JsonConvert.DeserializeObject<GateCrossExMarginModeQueryRequest>(saved));
        foreach (var result in new[] { await client.CrossEx.GetMarginModeAsync(input), await client.CrossEx.GetMarginModeAsync(symbol) })
        {
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(Symbol, result.Data.Symbol); // Returned metadata is not filled from the queried symbol.
            Assert.Equal("ISOLATED", result.Data.MarginMode);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        }
        Assert.Equal(2, handler.Requests.Count);
        foreach (var wire in handler.Requests)
        {
            Assert.Equal(HttpMethod.Get, wire.Method);
            Assert.Equal("api.gateio.ws", wire.RequestUri.Host);
            Assert.Equal("/api/v4/crossex/positions/margin_mode", wire.RequestUri.AbsolutePath);
            Assert.Equal(symbol, Assert.Single(Query(wire.RequestUri)).Value);
            Assert.Equal("symbol", Assert.Single(Query(wire.RequestUri)).Key);
            Assert.Empty(wire.Content);
            AssertSignature(wire);
        }
    }

    [Theory]
    [InlineData(GateCrossExMarginMode.Cross, "CROSS")]
    [InlineData(GateCrossExMarginMode.Isolated, "ISOLATED")]
    public async Task POST_contains_only_the_two_explicit_instructions_and_signs_the_exact_JSON_bytes(GateCrossExMarginMode mode, string wireMode)
    {
        var handler = Handler(Fixture(), HttpStatusCode.Accepted);
        using var client = Client(handler);
        var input = new GateCrossExMarginModeRequest { Symbol = Symbol, MarginMode = mode };
        var saved = JsonConvert.SerializeObject(input);
        Assert.Equal(input, JsonConvert.DeserializeObject<GateCrossExMarginModeRequest>(saved));
        Assert.Equal(wireMode, MapConverter.GetString(mode));
        foreach (var result in new[] { await client.CrossEx.UpdateMarginModeAsync(input), await client.CrossEx.UpdateMarginModeAsync(Symbol, mode) })
        {
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(HttpStatusCode.Accepted, result.Response!.StatusCode);
            Assert.Equal("ISOLATED", result.Data.MarginMode); // Even a different acknowledgement is not overwritten with CROSS.
            Assert.Equal(Symbol, result.Data.Symbol);
        }
        Assert.Equal(2, handler.Requests.Count); // No pre-query, close/cancel, mode follow-up or polling.
        foreach (var wire in handler.Requests)
        {
            Assert.Equal(HttpMethod.Post, wire.Method);
            Assert.Equal("api.gateio.ws", wire.RequestUri.Host);
            Assert.Equal("/api/v4/crossex/positions/margin_mode", wire.RequestUri.AbsolutePath);
            Assert.Empty(wire.RequestUri.Query);
            var body = JObject.Parse(wire.Content);
            Assert.Equal(2, body.Count);
            Assert.Equal(Symbol, (string?)body["symbol"]);
            Assert.Equal(wireMode, (string?)body["margin_mode"]);
            Assert.Equal(JTokenType.String, body["margin_mode"]!.Type);
            Assert.True(JToken.DeepEquals(JObject.Parse(saved), body));
            Assert.Contains("application/json", Assert.Single(wire.Headers["Content-Type"]));
            AssertSignature(wire);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(" " + Symbol)]
    [InlineData(Symbol + "\0")]
    [InlineData(Symbol + ",HYPERLIQUID_FUTURE_ADA_USDC")]
    public async Task Invalid_single_symbol_values_do_not_broaden_GET_or_POST(string? symbol)
    {
        var handler = Handler(Fixture());
        using var client = Client(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.CrossEx.GetMarginModeAsync(symbol!));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.CrossEx.UpdateMarginModeAsync(symbol!, GateCrossExMarginMode.Cross));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("BINANCE_FUTURE_ADA_USDT")]
    [InlineData("LIGHTER_FUTURE_ADA_USDC")]
    [InlineData("HYPERLIQUID_SPOT_ADA_USDC")]
    [InlineData("hyperliquid_FUTURE_ADA_USDC")]
    [InlineData("HYPERLIQUID_FUTURE_")]
    [InlineData("HYPERLIQUID_FUTURE__USDC")]
    [InlineData("HYPERLIQUID_FUTURE_ADA_")]
    public async Task POST_does_not_select_a_venue_or_repair_an_incomplete_Hyperliquid_symbol(string symbol)
    {
        var handler = Handler(Fixture());
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.UpdateMarginModeAsync(symbol, GateCrossExMarginMode.Isolated));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(int.MaxValue)]
    public async Task Undefined_or_omitted_mode_is_never_a_default_CROSS_instruction(int number)
    {
        var input = new GateCrossExMarginModeRequest { Symbol = Symbol, MarginMode = (GateCrossExMarginMode)number };
        var handler = Handler(Fixture());
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.CrossEx.UpdateMarginModeAsync(input));
        Assert.Throws<JsonSerializationException>(() => JsonConvert.SerializeObject(input));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Null_DTOs_fail_before_any_HTTP()
    {
        var handler = Handler(Fixture());
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CrossEx.GetMarginModeAsync((GateCrossExMarginModeQueryRequest)null!));
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CrossEx.UpdateMarginModeAsync((GateCrossExMarginModeRequest)null!));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("query", "symbol")]
    [InlineData("update", "symbol")]
    [InlineData("update", "margin_mode")]
    [InlineData("response", "symbol")]
    [InlineData("response", "margin_mode")]
    public void Every_saved_schema_required_field_must_be_present_and_non_null(string kind, string field)
    {
        foreach (var omit in new[] { false, true })
        {
            var json = kind == "query" ? JObject.FromObject(new GateCrossExMarginModeQueryRequest { Symbol = Symbol }) : JObject.Parse(Fixture());
            if (omit) json.Property(field)!.Remove(); else json[field] = JValue.CreateNull();
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), SavedType(kind)));
        }
    }

    [Theory]
    [InlineData("\"cross\"")]
    [InlineData("\"Cross\"")]
    [InlineData("\"NONE\"")]
    [InlineData("\"FUTURE_MODE\"")]
    [InlineData("\" CROSS\"")]
    [InlineData("\"\"")]
    [InlineData("1")]
    [InlineData("true")]
    [InlineData("[]")]
    [InlineData("{}")]
    public void Saved_update_mode_accepts_only_exact_documented_enum_strings(string modeToken)
        => Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExMarginModeRequest>(
            $"{{\"symbol\":\"{Symbol}\",\"margin_mode\":{modeToken}}}"));

    [Theory]
    [InlineData("query", "symbol", "123")]
    [InlineData("query", "symbol", "true")]
    [InlineData("query", "symbol", "[]")]
    [InlineData("update", "symbol", "123")]
    [InlineData("update", "symbol", "false")]
    [InlineData("update", "symbol", "{}")]
    public void Saved_symbols_do_not_coerce_other_JSON_tokens(string kind, string field, string token)
    {
        var json = JObject.Parse(Fixture());
        if (kind == "query") json.Remove("margin_mode");
        json[field] = JToken.Parse(token);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), SavedType(kind)));
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public async Task Raw_future_response_strings_are_preserved_without_default_enum_or_local_state_inference(bool post, bool raw)
    {
        const string text = "2026-10-02T01:02:03.1234567+03:00";
        var json = $"{{\"symbol\":\"{text}\",\"margin_mode\":\"{text}\"}}";
        var handler = Handler(json, post ? HttpStatusCode.Accepted : HttpStatusCode.OK);
        using var client = Client(handler, raw);
        var result = post ? await client.CrossEx.UpdateMarginModeAsync(Symbol, GateCrossExMarginMode.Cross) : await client.CrossEx.GetMarginModeAsync(Symbol);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(text, result.Data.Symbol);
        Assert.Equal(text, result.Data.MarginMode);
        if (raw) Assert.Equal(json, result.Raw);
        var saved = JsonConvert.SerializeObject(result.Data);
        var copy = JsonConvert.DeserializeObject<GateCrossExMarginModeResponse>(saved)!;
        Assert.Equal(text, copy.Symbol);
        Assert.Equal(text, copy.MarginMode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("CROSS")]
    [InlineData("ISOLATED")]
    [InlineData("FUTURE_MODE")]
    [InlineData("cross")]
    public void Raw_response_modes_round_trip_without_interpretation(string mode)
    {
        var value = JsonConvert.DeserializeObject<GateCrossExMarginModeResponse>($"{{\"symbol\":\"{Symbol}\",\"margin_mode\":\"{mode}\"}}")!;
        Assert.Equal(mode, value.MarginMode);
        Assert.Equal(mode, (string?)JObject.FromObject(value)["margin_mode"]);
    }

    public static IEnumerable<object[]> InvalidResponses()
    {
        foreach (var raw in new[] { false, true })
            foreach (var post in new[] { false, true })
            {
                foreach (var json in new[] { "", "null", "[]", "{}", "{\"symbol\":" }) yield return [json, post, raw];
                foreach (var field in new[] { "symbol", "margin_mode" })
                {
                    var missing = JObject.Parse(Fixture());
                    missing.Property(field)!.Remove();
                    yield return [missing.ToString(), post, raw];
                    foreach (var token in new[] { "null", "123", "true", "[]", "{}", "\"\"", "\" \"" })
                    {
                        var json = JObject.Parse(Fixture());
                        json[field] = JToken.Parse(token);
                        yield return [json.ToString(), post, raw];
                    }
                }
            }
    }

    [Theory]
    [MemberData(nameof(InvalidResponses))]
    public async Task Missing_malformed_or_blank_acknowledgements_are_not_fabricated_from_the_instruction(string json, bool post, bool raw)
    {
        var status = post ? HttpStatusCode.Accepted : HttpStatusCode.OK;
        var handler = Handler(json, status);
        using var client = Client(handler, raw);
        var result = post ? await client.CrossEx.UpdateMarginModeAsync(Symbol, GateCrossExMarginMode.Isolated) : await client.CrossEx.GetMarginModeAsync(Symbol);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Equal(status, result.Response!.StatusCode);
        if (raw) Assert.Equal(json, result.Raw);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(400, "TRADE_CHANGE_MARGIN_MODE_SAME_ERROR")]
    [InlineData(400, "TRADE_MARGIN_MODE_NOT_SUPPORT")]
    [InlineData(429, "TOO_MANY_REQUESTS")]
    [InlineData(500, "INTERNAL")]
    public async Task Server_errors_and_same_mode_errors_keep_their_labels_without_success_conversion_or_financial_followups(int status, string label)
    {
        var json = $"{{\"label\":\"{label}\",\"detail\":\"server diagnostic\"}}";
        var handler = Handler(json, (HttpStatusCode)status);
        using var client = Client(handler, raw: true);
        foreach (var result in new[] { await client.CrossEx.GetMarginModeAsync(Symbol), await client.CrossEx.UpdateMarginModeAsync(Symbol, GateCrossExMarginMode.Cross) })
        {
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal((HttpStatusCode)status, result.Response!.StatusCode);
            Assert.Equal(json, result.Raw);
            Assert.Equal(label, result.Error!.Data);
            Assert.Equal("server diagnostic", result.Error.Message);
        }
        Assert.Equal(2, handler.Requests.Count);
        Assert.All(handler.Requests, x => Assert.Equal("/api/v4/crossex/positions/margin_mode", x.RequestUri.AbsolutePath));
    }

    [Fact]
    public async Task Both_operations_require_credentials_before_HTTP()
    {
        var handler = Handler(Fixture());
        using var client = Client(handler, credentials: false);
        await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.GetMarginModeAsync(Symbol));
        await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.UpdateMarginModeAsync(Symbol, GateCrossExMarginMode.Cross));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public void Model_inventories_match_the_full_current_contract_without_extra_financial_instructions()
    {
        Assert.Equal(new[] { "symbol" }, Fields(new GateCrossExMarginModeQueryRequest { Symbol = Symbol }));
        Assert.Equal(new[] { "margin_mode", "symbol" }, Fields(new GateCrossExMarginModeRequest { Symbol = Symbol, MarginMode = GateCrossExMarginMode.Cross }));
        Assert.Equal(new[] { "margin_mode", "symbol" }, Fields(JsonConvert.DeserializeObject<GateCrossExMarginModeResponse>(Fixture())!));
        Assert.Equal(new[] { 1, 2 }, Enum.GetValues<GateCrossExMarginMode>().Select(x => (int)x));
    }

    private static string[] Fields(object value) => JObject.FromObject(value).Properties().Select(x => x.Name).OrderBy(x => x).ToArray();
    private static Type SavedType(string kind) => kind switch
    {
        "query" => typeof(GateCrossExMarginModeQueryRequest), "update" => typeof(GateCrossExMarginModeRequest), _ => typeof(GateCrossExMarginModeResponse),
    };
    private static string Fixture() => JsonFixture.Read("Docs/CrossEx/margin_mode.success.json");
    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });
    private static GateRestApiClient Client(RecordingHttpMessageHandler handler, bool raw = false, bool credentials = true)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler), RawResponse = raw });
        if (credentials) client.SetApiCredentials("key", "secret");
        return client;
    }
    private static Dictionary<string, string> Query(Uri uri)
        => uri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split('=', 2))
            .ToDictionary(x => Uri.UnescapeDataString(x[0]), x => Uri.UnescapeDataString(x.Length > 1 ? x[1] : ""));
    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var hash = Convert.ToHexString(SHA512.HashData(request.ContentBytes)).ToLowerInvariant();
        // Gate APIv4's signature input uses the query without URL encoding, not the encoded transport URI.
        var unencodedQuery = Uri.UnescapeDataString(request.RequestUri.Query.TrimStart('?'));
        var payload = $"{request.Method.Method}\n{request.RequestUri.AbsolutePath}\n{unencodedQuery}\n{hash}\n{timestamp}";
        var signature = Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant();
        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }
}
