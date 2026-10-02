using ApiSharp.Models;
using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;
using System.Globalization;
using System.Net;
using System.Text;

namespace Gate.IO.Api.Tests.CrossEx;

[Trait("Category", "Contract")]
public class CrossExRetrospectiveTests
{
    private const string Symbol = "HYPERLIQUID_FUTURE_CXMT_USDC";
    private const string RawText = "2026-10-02T01:02:03.1234567+03:00";

    public static IEnumerable<object[]> RawStrings()
    {
        foreach (var field in new[] { "order_id", "text" })
            foreach (var raw in new[] { false, true }) yield return ["order", field, raw];
        foreach (var field in new[] { "tx_id", "text" })
            foreach (var raw in new[] { false, true }) yield return ["transfer", field, raw];
        foreach (var field in new[] { "quote_id", "from_coin", "to_coin" })
            foreach (var raw in new[] { false, true }) yield return ["quote", field, raw];
    }

    [Theory]
    [MemberData(nameof(RawStrings))]
    public async Task Action_and_quote_strings_remain_exact_in_HTTP_and_saved_JSON(string kind, string field, bool raw)
    {
        var json = Seed(kind);
        json[field] = RawText;
        var body = json.ToString(Formatting.None);
        var handler = Handler(body);
        using var client = Client(handler, raw);
        if (kind == "order") AssertRaw(await client.CrossEx.PlaceOrderAsync(Order()), field, body, raw);
        else if (kind == "transfer") AssertRaw(await client.CrossEx.TransferAsync(Transfer()), field, body, raw);
        else AssertRaw(await client.CrossEx.GetConvertQuoteAsync(Quote()), field, body, raw);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("order", "order_id")]
    [InlineData("order", "text")]
    [InlineData("transfer", "tx_id")]
    [InlineData("transfer", "text")]
    [InlineData("quote", "quote_id")]
    [InlineData("quote", "from_coin")]
    [InlineData("quote", "to_coin")]
    public void Saved_action_and_quote_JSON_preserves_date_looking_string_tokens(string kind, string field)
    {
        var json = Seed(kind);
        json[field] = RawText;
        var type = kind == "order" ? typeof(GateCrossExOrderActionResult)
            : kind == "transfer" ? typeof(GateCrossExTransferResult) : typeof(GateCrossExConvertQuote);
        var copy = JsonConvert.DeserializeObject(json.ToString(), type)!;
        Assert.Equal(RawText, (string?)JObject.FromObject(copy)[field]);
    }

    [Theory]
    [InlineData("order", false)]
    [InlineData("order", true)]
    [InlineData("transfer", false)]
    [InlineData("transfer", true)]
    [InlineData("quote", false)]
    [InlineData("quote", true)]
    public async Task Contract_errors_keep_HTTP_and_raw_metadata_without_putting_response_payload_in_the_error(string kind, bool raw)
    {
        const string secret = "response-private-sentinel";
        var json = Seed(kind);
        json[kind == "order" ? "order_id" : kind == "transfer" ? "tx_id" : "quote_id"] = new JObject { ["private"] = secret };
        var body = json.ToString(Formatting.None);
        var handler = Handler(body);
        using var client = Client(handler, raw);
        if (kind == "order") AssertInvalid(await client.CrossEx.PlaceOrderAsync(Order()), body, raw, secret);
        else if (kind == "transfer") AssertInvalid(await client.CrossEx.TransferAsync(Transfer()), body, raw, secret);
        else AssertInvalid(await client.CrossEx.GetConvertQuoteAsync(Quote()), body, raw, secret);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("order", false)]
    [InlineData("order", true)]
    [InlineData("transfer", false)]
    [InlineData("transfer", true)]
    [InlineData("quote", false)]
    [InlineData("quote", true)]
    [InlineData("mode", false)]
    [InlineData("mode", true)]
    public async Task Invalid_JSON_syntax_does_not_escape_with_payload_bearing_parser_errors(string kind, bool raw)
    {
        const string secret = "response-private-sentinel";
        const string body = "{\"private\":\"response-private-sentinel\",";
        var handler = Handler(body);
        using var client = Client(handler, raw);
        if (kind == "order") AssertInvalid(await client.CrossEx.PlaceOrderAsync(Order()), body, raw, secret);
        else if (kind == "transfer") AssertInvalid(await client.CrossEx.TransferAsync(Transfer()), body, raw, secret);
        else if (kind == "quote") AssertInvalid(await client.CrossEx.GetConvertQuoteAsync(Quote()), body, raw, secret);
        else AssertInvalid(await client.CrossEx.UpdateMarginModeAsync(Symbol, GateCrossExMarginMode.Cross), body, raw, secret);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public async Task Order_acknowledgement_requires_server_text_even_when_request_text_is_omitted(bool omit, bool raw)
    {
        var json = Seed("order");
        if (omit) json.Remove("text"); else json["text"] = JValue.CreateNull();
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExOrderActionResult>(json.ToString()));
        var body = json.ToString(Formatting.None);
        var handler = Handler(body);
        using var client = Client(handler, raw);
        AssertInvalid(await client.CrossEx.PlaceOrderAsync(Order()), body, raw, "response-private-sentinel");
        Assert.Single(handler.Requests);
    }

    [Fact]
    public void Required_order_text_can_be_empty_but_is_not_filled_from_the_order_ID()
    {
        var value = JsonConvert.DeserializeObject<GateCrossExOrderActionResult>("{\"order_id\":\"123\",\"text\":\"\"}")!;
        Assert.Equal("", value.Text);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("0.001")]
    [InlineData("-0.001")]
    [InlineData("0.0099999999999999999999999999")]
    [InlineData("-0.0099999999999999999999999999")]
    [InlineData("0.0000000000000000000000000001")]
    [InlineData("-0.0000000000000000000000000001")]
    public async Task Isolated_margin_rejects_zero_and_absolute_amounts_below_the_published_minimum_before_HTTP(string text)
    {
        var handler = Handler("{\"symbol\":\"" + Symbol + "\",\"margin\":\"0.01\"}");
        using var client = Client(handler);
        var margin = decimal.Parse(text, CultureInfo.InvariantCulture);
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.CrossEx.UpdateIsolatedMarginAsync(Symbol, margin));
        var saved = JsonConvert.SerializeObject(new GateCrossExIsolatedMarginRequest { Symbol = Symbol, Margin = margin });
        var request = JsonConvert.DeserializeObject<GateCrossExIsolatedMarginRequest>(saved)!;
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.CrossEx.UpdateIsolatedMarginAsync(request));
        Assert.Equal(margin, request.Margin);
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("0.01")]
    [InlineData("-0.01")]
    [InlineData("0.0100000000000000000000000001")]
    [InlineData("-0.0100000000000000000000000001")]
    [InlineData("79228162514264337593543950335")]
    [InlineData("-79228162514264337593543950335")]
    public async Task Isolated_margin_boundaries_are_forwarded_without_rounding_or_financial_followup(string text)
    {
        var handler = Handler("{\"symbol\":\"" + Symbol + "\",\"margin\":\"0.01\"}");
        using var client = Client(handler);
        var result = await client.CrossEx.UpdateIsolatedMarginAsync(Symbol, decimal.Parse(text, CultureInfo.InvariantCulture));
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(text, (string?)JObject.Parse(Assert.Single(handler.Requests).Content)["margin"]);
    }

    private static void AssertRaw<T>(RestCallResult<T> result, string field, string body, bool raw)
    {
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(HttpStatusCode.Accepted, result.Response!.StatusCode);
        Assert.Equal(RawText, (string?)JObject.FromObject(result.Data!)[field]);
        if (raw) Assert.Equal(body, result.Raw);
        var copy = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(result.Data))!;
        Assert.Equal(RawText, (string?)JObject.FromObject(copy)[field]);
    }

    private static void AssertInvalid<T>(RestCallResult<T> result, string body, bool raw, string secret)
    {
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(HttpStatusCode.Accepted, result.Response!.StatusCode);
        Assert.NotNull(result.Error);
        Assert.Null(result.Error.Data);
        Assert.DoesNotContain(secret, result.Error.ToString());
        if (raw) Assert.Equal(body, result.Raw);
    }

    private static JObject Seed(string kind) => JObject.Parse(kind == "order" ? "{\"order_id\":\"123\",\"text\":\"client_text\"}"
        : kind == "transfer" ? "{\"tx_id\":\"23453\",\"text\":\"23453\"}"
        : "{\"quote_id\":\"2074460878500352\",\"valid_ms\":\"5000\",\"from_coin\":\"USDT\",\"to_coin\":\"BTC\",\"from_amount\":\"3\",\"to_amount\":\"0.000027\",\"price\":\"0.000009\"}");
    private static GateCrossExOrderRequest Order() => new() { Symbol = "LIGHTER_FUTURE_ADA_USDC", Side = GateCrossExOrderSide.Buy, Quantity = 1m, Price = 1m };
    private static GateCrossExTransferRequest Transfer() => new() { Coin = "USDC", Amount = 1m, From = GateCrossExTransferAccountType.Spot, To = GateCrossExTransferAccountType.CrossExLighter };
    private static GateCrossExConvertQuoteRequest Quote() => new() { ExchangeType = GateCrossExExchangeType.Lighter, FromCoin = "LIGHTER_USDC", ToCoin = "CROSSEX_USDT", FromAmount = 1m };
    private static RecordingHttpMessageHandler Handler(string json)
        => new(_ => new HttpResponseMessage(HttpStatusCode.Accepted) { Content = new StringContent(json, Encoding.UTF8, "application/json") });
    private static GateRestApiClient Client(RecordingHttpMessageHandler handler, bool raw = false)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler), RawResponse = raw });
        client.SetApiCredentials("key", "secret");
        return client;
    }
}
