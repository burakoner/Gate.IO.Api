using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;
using ApiSharp.Models;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.CrossEx;

[Trait("Category", "Contract")]
public class CrossExLighterContractTests
{
    // Official CrossEx POST contracts checked on 2026-10-02; synthetic boundary inputs are not live captures.
    private const string Reference = "https://www.gate.com/docs/developers/apiv4/en/crossex/";
    private const string Symbol = "LIGHTER_FUTURE_ADA_USDC";
    private const string Action = "{\"order_id\":\"123456\",\"text\":\"cross-test-1\"}";
    private const string Transfer = "{\"tx_id\":\"23453\",\"text\":\"23453\"}";
    private const string Quote = "{\"quote_id\":\"2074460878500352\",\"valid_ms\":\"5000\",\"from_coin\":\"USDT\",\"to_coin\":\"BTC\",\"from_amount\":\"3\",\"to_amount\":\"0.000027\",\"price\":\"0.000009\"}";

    [Fact]
    public void Current_field_and_requiredness_inventories_are_complete()
    {
        AssertContract<GateCrossExOrderRequest>("text,symbol,side,type,time_in_force,qty,price,quote_qty,reduce_only,position_side", "symbol,side");
        AssertContract<GateCrossExTransferRequest>("coin,amount,from,to,text", "coin,amount,from,to");
        AssertContract<GateCrossExConvertQuoteRequest>("exchange_type,from_coin,to_coin,from_amount", "exchange_type,from_coin,to_coin,from_amount");
        AssertContract<GateCrossExOrderActionResult>("order_id,text", "order_id,text");
        AssertContract<GateCrossExTransferResult>("tx_id,text", "tx_id,text");
        AssertContract<GateCrossExConvertQuote>("quote_id,valid_ms,from_coin,to_coin,from_amount,to_amount,price", "quote_id,valid_ms,from_coin,to_coin,from_amount,to_amount,price");
        Assert.StartsWith("https://www.gate.com/", Reference);
    }

    [Fact]
    public void New_enum_values_do_not_renumber_existing_public_members()
    {
        Assert.Equal(9, (int)GateCrossExExchangeType.Lighter);
        Assert.Equal(10, (int)GateCrossExTransferAccountType.CrossExLighter);
        Assert.Equal(Enumerable.Range(1, 9), Enum.GetValues<GateCrossExExchangeType>().Select(x => (int)x));
        Assert.Equal(Enumerable.Range(1, 10), Enum.GetValues<GateCrossExTransferAccountType>().Select(x => (int)x));
        Assert.Equal("LIGHTER", ApiSharp.Converters.MapConverter.GetString(GateCrossExExchangeType.Lighter));
        Assert.Equal("CROSSEX_LIGHTER", ApiSharp.Converters.MapConverter.GetString(GateCrossExTransferAccountType.CrossExLighter));
    }

    [Fact]
    public void Saved_order_uses_the_complete_current_wire_contract()
    {
        var value = JObject.FromObject(new GateCrossExOrderRequest
        {
            Symbol = Symbol, Side = GateCrossExOrderSide.Buy, Type = GateCrossExOrderType.Limit,
            TimeInForce = GateCrossExTimeInForce.RetailPriceImprovement, Quantity = 0.1m, Price = 0.65m,
            QuoteQuantity = 1.2m, ReduceOnly = false, PositionSide = GateCrossExPositionSide.None, Text = "cross-test-1",
        });
        Assert.Equal("0.1", (string?)value["qty"]);
        Assert.Equal("false", (string?)value["reduce_only"]);
        Assert.Equal("NONE", (string?)value["position_side"]);
        Assert.Equal("RPI", (string?)value["time_in_force"]);
        Assert.Equal(new[] { "position_side", "price", "qty", "quote_qty", "reduce_only", "side", "symbol", "text", "time_in_force", "type" }, value.Properties().Select(x => x.Name).OrderBy(x => x));
    }

    [Fact]
    public void Saved_transfer_and_quote_preserve_numeric_strings_and_wire_names()
    {
        var transfer = JObject.FromObject(new GateCrossExTransferRequest { Coin = "USDT", Amount = 242.45m, From = GateCrossExTransferAccountType.Spot, To = GateCrossExTransferAccountType.CrossEx });
        Assert.Equal("242.45", (string?)transfer["amount"]);
        Assert.Equal("SPOT", (string?)transfer["from"]);
        var quote = JObject.FromObject(new GateCrossExConvertQuoteRequest { ExchangeType = GateCrossExExchangeType.Gate, FromCoin = "BTC", ToCoin = "USDT", FromAmount = 0.00008m });
        Assert.Equal("GATE", (string?)quote["exchange_type"]);
        Assert.Equal("0.00008", (string?)quote["from_amount"]);
    }

    [Theory]
    [InlineData("1.1")]
    [InlineData("true")]
    [InlineData("\"1.5\"")]
    public void Quote_valid_ms_is_an_exact_long_not_a_coerced_integer(string token)
    {
        var json = JObject.Parse(Quote);
        json["valid_ms"] = JToken.Parse(token);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExConvertQuote>(json.ToString()));
    }

    [Theory]
    [InlineData("\"0.12345678901234567890123456789\"")]
    [InlineData("\"1e-29\"")]
    [InlineData("true")]
    public void Quote_money_must_not_be_rounded_or_defaulted(string token)
    {
        var json = JObject.Parse(Quote);
        json["price"] = JToken.Parse(token);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExConvertQuote>(json.ToString()));
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("null")]
    [InlineData("")]
    public async Task All_three_post_endpoints_reject_empty_success_payloads(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var order = await client.CrossEx.PlaceOrderAsync(Symbol, GateCrossExOrderSide.Buy, GateCrossExOrderType.Market, quantity: 0.1m);
        var transfer = await client.CrossEx.TransferAsync("USDT", 1m, GateCrossExTransferAccountType.Spot, GateCrossExTransferAccountType.CrossEx);
        var quote = await client.CrossEx.GetConvertQuoteAsync(GateCrossExExchangeType.Gate, "BTC", "USDT", 1m);
        Assert.False(order.Success);
        Assert.False(transfer.Success);
        Assert.False(quote.Success);
        Assert.Equal(3, handler.Requests.Count);
    }

    [Fact]
    public async Task Saved_lighter_limit_order_and_convenience_overload_send_identical_signed_bodies()
    {
        var handler = Handler(Action);
        using var client = Client(handler);
        var instruction = JsonConvert.DeserializeObject<GateCrossExOrderRequest>("{\"symbol\":\"LIGHTER_FUTURE_ADA_USDC\",\"side\":\"BUY\",\"type\":\"LIMIT\",\"time_in_force\":\"RPI\",\"qty\":\"0.1234567890123456789012345678\",\"price\":\"0.65\",\"reduce_only\":\"false\",\"position_side\":\"NONE\",\"text\":\"cross-test-1\"}")!;
        Assert.Equal(GateCrossExOrderType.Limit, instruction.Type);
        Assert.Equal(0.1234567890123456789012345678m, instruction.Quantity);
        var saved = JsonConvert.DeserializeObject<GateCrossExOrderRequest>(JsonConvert.SerializeObject(instruction))!;
        var dto = await client.CrossEx.PlaceOrderAsync(saved);
        var legacy = await client.CrossEx.PlaceOrderAsync(Symbol, GateCrossExOrderSide.Buy, GateCrossExOrderType.Limit, GateCrossExTimeInForce.RetailPriceImprovement, instruction.Quantity, 0.65m, reduceOnly: false, positionSide: GateCrossExPositionSide.None, text: "cross-test-1");
        Assert.True(dto.Success, dto.Error?.ToString());
        Assert.True(legacy.Success, legacy.Error?.ToString());
        Assert.Equal("123456", dto.Data.OrderId); // Acknowledgement only, no venue/execution state inferred.
        Assert.Equal(handler.Requests[0].Content, handler.Requests[1].Content);
        var body = JObject.Parse(handler.Requests[0].Content);
        Assert.Equal(JTokenType.String, body["qty"]!.Type);
        Assert.Equal("0.1234567890123456789012345678", (string?)body["qty"]);
        Assert.Equal("false", (string?)body["reduce_only"]);
        Assert.Equal("NONE", (string?)body["position_side"]);
        Assert.Equal("RPI", (string?)body["time_in_force"]);
        Assert.All(handler.Requests, x => AssertRequest(x, "/api/v4/crossex/orders"));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Lighter_transfers_work_in_both_documented_directions_without_followup(bool toLighter)
    {
        var from = toLighter ? GateCrossExTransferAccountType.Spot : GateCrossExTransferAccountType.CrossExLighter;
        var to = toLighter ? GateCrossExTransferAccountType.CrossExLighter : GateCrossExTransferAccountType.Spot;
        var handler = Handler(Transfer);
        using var client = Client(handler);
        var saved = JsonConvert.DeserializeObject<GateCrossExTransferRequest>(JsonConvert.SerializeObject(new GateCrossExTransferRequest { Coin = "USDC", Amount = 242.45000001m, From = from, To = to, Text = "transfer-1" }))!;
        var dto = await client.CrossEx.TransferAsync(saved);
        var legacy = await client.CrossEx.TransferAsync("USDC", 242.45000001m, from, to, "transfer-1");
        Assert.True(dto.Success, dto.Error?.ToString());
        Assert.True(legacy.Success, legacy.Error?.ToString());
        Assert.Equal("23453", dto.Data.TransactionId);
        Assert.Equal(handler.Requests[0].Content, handler.Requests[1].Content);
        var body = JObject.Parse(handler.Requests[0].Content);
        Assert.Equal("USDC", (string?)body["coin"]);
        Assert.Equal(toLighter ? "SPOT" : "CROSSEX_LIGHTER", (string?)body["from"]);
        Assert.Equal(toLighter ? "CROSSEX_LIGHTER" : "SPOT", (string?)body["to"]);
        Assert.Equal("242.45000001", (string?)body["amount"]);
        Assert.Equal(new[] { "amount", "coin", "from", "text", "to" }, body.Properties().Select(x => x.Name).OrderBy(x => x));
        Assert.All(handler.Requests, x => AssertRequest(x, "/api/v4/crossex/transfers"));
    }

    [Theory]
    [InlineData("LIGHTER_USDC", "CROSSEX_USDT")]
    [InlineData("CROSSEX_USDT", "LIGHTER_USDC")]
    public async Task Lighter_quotes_preserve_both_asset_directions_without_execution_or_mode_change(string from, string to)
    {
        var response = JObject.Parse(Quote);
        response["from_coin"] = from;
        response["to_coin"] = to;
        var handler = Handler(response.ToString(Formatting.None));
        using var client = Client(handler);
        var saved = JsonConvert.DeserializeObject<GateCrossExConvertQuoteRequest>(JsonConvert.SerializeObject(new GateCrossExConvertQuoteRequest { ExchangeType = GateCrossExExchangeType.Lighter, FromCoin = from, ToCoin = to, FromAmount = 3m }))!;
        var dto = await client.CrossEx.GetConvertQuoteAsync(saved);
        var legacy = await client.CrossEx.GetConvertQuoteAsync(GateCrossExExchangeType.Lighter, from, to, 3m);
        Assert.True(dto.Success, dto.Error?.ToString());
        Assert.True(legacy.Success, legacy.Error?.ToString());
        Assert.Equal(from, dto.Data.FromCoin);
        Assert.Equal(to, dto.Data.ToCoin);
        Assert.Equal(5000L, dto.Data.ValidMilliseconds);
        Assert.Equal(handler.Requests[0].Content, handler.Requests[1].Content);
        var body = JObject.Parse(handler.Requests[0].Content);
        Assert.Equal("LIGHTER", (string?)body["exchange_type"]);
        Assert.Equal(from, (string?)body["from_coin"]);
        Assert.Equal(to, (string?)body["to_coin"]);
        Assert.Equal("3", (string?)body["from_amount"]);
        Assert.Equal(new[] { "exchange_type", "from_amount", "from_coin", "to_coin" }, body.Properties().Select(x => x.Name).OrderBy(x => x));
        Assert.All(handler.Requests, x => AssertRequest(x, "/api/v4/crossex/convert/quote"));
    }

    [Theory]
    [InlineData("BINANCE_SPOT_ADA_USDT", GateCrossExOrderSide.Buy, true)]
    [InlineData("GATE_MARGIN_ADA_USDT", GateCrossExOrderSide.Buy, true)]
    [InlineData("LIGHTER_FUTURE_ADA_USDC", GateCrossExOrderSide.Buy, false)]
    [InlineData("BINANCE_SPOT_ADA_USDT", GateCrossExOrderSide.Sell, false)]
    [InlineData("GATE_MARGIN_ADA_USDT", GateCrossExOrderSide.Sell, false)]
    public async Task Market_orders_send_the_documented_quantity_kind_without_filling_defaults(string symbol, GateCrossExOrderSide side, bool quoteRequired)
    {
        var handler = Handler(Action);
        using var client = Client(handler);
        var result = await client.CrossEx.PlaceOrderAsync(symbol, side, GateCrossExOrderType.Market, quantity: quoteRequired ? null : 10m,
            quoteQuantity: quoteRequired ? 10m : null, positionSide: symbol.Contains("_MARGIN_") ? GateCrossExPositionSide.Long : null);
        Assert.True(result.Success, result.Error?.ToString());
        var body = JObject.Parse(Assert.Single(handler.Requests).Content);
        Assert.Equal("10", (string?)body[quoteRequired ? "quote_qty" : "qty"]);
        Assert.Null(body[quoteRequired ? "qty" : "quote_qty"]);
        Assert.Null(body["price"]);
        Assert.Null(body["time_in_force"]);
        Assert.Null(body["reduce_only"]);
        if (symbol.Contains("_MARGIN_")) Assert.Equal("LONG", (string?)body["position_side"]);
        else Assert.Null(body["position_side"]);
        Assert.Null(body["text"]);
        AssertRequest(handler.Requests[0], "/api/v4/crossex/orders");
    }

    [Fact]
    public async Task Omitted_order_type_and_explicit_zero_values_are_not_replaced_with_client_defaults()
    {
        var handler = Handler("{\"order_id\":\"accepted\",\"text\":\"\"}");
        using var client = Client(handler);
        var result = await client.CrossEx.PlaceOrderAsync(Symbol, GateCrossExOrderSide.Sell, quantity: 0.1m, price: 0m, reduceOnly: false);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("", result.Data.Text); // Required server text is preserved, never invented from the input or ID.
        var body = JObject.Parse(Assert.Single(handler.Requests).Content);
        Assert.Equal("0.1", (string?)body["qty"]);
        Assert.Equal("0", (string?)body["price"]);
        Assert.Equal("false", (string?)body["reduce_only"]);
        Assert.Null(body["type"]);
        Assert.Null(body["position_side"]);
    }

    public static IEnumerable<object[]> RequiredSavedFields()
    {
        foreach (var (kind, fields) in new[] { ("order", "symbol,side"), ("transfer", "coin,amount,from,to"), ("quote", "exchange_type,from_coin,to_coin,from_amount") })
            foreach (var field in fields.Split(','))
                foreach (var explicitNull in new[] { false, true }) yield return new object[] { kind, field, explicitNull };
    }

    [Theory]
    [MemberData(nameof(RequiredSavedFields))]
    public void Saved_requests_require_every_explicit_schema_required_field(string kind, string field, bool explicitNull)
    {
        var json = RequestSeed(kind);
        if (explicitNull) json[field] = JValue.CreateNull();
        else json.Remove(field);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), RequestType(kind)));
    }

    public static IEnumerable<object[]> InvalidSavedEnumValues()
    {
        foreach (var (kind, field) in new[] { ("order", "side"), ("order", "type"), ("order", "time_in_force"), ("order", "position_side"), ("transfer", "from"), ("transfer", "to"), ("quote", "exchange_type") })
            foreach (var token in new[] { "0", "1", "true", "\"UNKNOWN\"", "\"\"", "\"Buy\"", "[]", "{}" }) yield return new object[] { kind, field, token };
    }

    [Theory]
    [MemberData(nameof(InvalidSavedEnumValues))]
    public void Saved_instruction_enums_do_not_guess_unknown_values_or_accept_numeric_CLR_values(string kind, string field, string token)
    {
        var json = RequestSeed(kind);
        json[field] = JToken.Parse(token);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), RequestType(kind)));
    }

    [Theory]
    [InlineData("1.0")]
    [InlineData("\"1e-29\"")]
    [InlineData("\"0.12345678901234567890123456789\"")]
    [InlineData("\"79228162514264337593543950336\"")]
    [InlineData("\"\"")]
    [InlineData("true")]
    [InlineData("[]")]
    [InlineData("{}")]
    public void All_saved_instruction_money_fields_reject_lossy_or_invalid_tokens(string token)
    {
        foreach (var (kind, field) in new[] { ("order", "qty"), ("order", "price"), ("order", "quote_qty"), ("transfer", "amount"), ("quote", "from_amount") })
        {
            var json = RequestSeed(kind);
            json[field] = JToken.Parse(token);
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), RequestType(kind)));
        }
    }

    [Theory]
    [InlineData("\"5000\"", 5000L)]
    [InlineData("9007199254740993", 9007199254740993L)]
    [InlineData("\"9007199254740993\"", 9007199254740993L)]
    [InlineData("\"9223372036854775807\"", long.MaxValue)]
    [InlineData("\"-9223372036854775808\"", long.MinValue)]
    [InlineData("0", 0L)]
    public void Quote_valid_ms_preserves_Int64_boundaries_and_writes_the_documented_string(string token, long expected)
    {
        var json = JObject.Parse(Quote);
        json["valid_ms"] = JToken.Parse(token);
        var value = JsonConvert.DeserializeObject<GateCrossExConvertQuote>(json.ToString())!;
        Assert.Equal(expected, value.ValidMilliseconds);
        var saved = JObject.FromObject(value);
        Assert.Equal(JTokenType.String, saved["valid_ms"]!.Type);
        Assert.Equal(expected.ToString(CultureInfo.InvariantCulture), (string?)saved["valid_ms"]);
        Assert.Equal(expected, saved.ToObject<GateCrossExConvertQuote>()!.ValidMilliseconds);
    }

    [Theory]
    [InlineData("null")]
    [InlineData("false")]
    [InlineData("1.0")]
    [InlineData("\"1e3\"")]
    [InlineData("\"9223372036854775808\"")]
    [InlineData("9223372036854775808")]
    [InlineData("\"\"")]
    [InlineData("[]")]
    [InlineData("{}")]
    public void Invalid_quote_validity_tokens_never_round_or_default(string token)
        => Quote_valid_ms_is_an_exact_long_not_a_coerced_integer(token);

    [Fact]
    public void All_documented_quote_values_roundtrip_as_strings_without_changing_accessor_types()
    {
        var value = JsonConvert.DeserializeObject<GateCrossExConvertQuote>(Quote)!;
        Assert.Equal("2074460878500352", value.QuoteId);
        Assert.Equal("USDT", value.FromCoin);
        Assert.Equal("BTC", value.ToCoin);
        Assert.Equal(3m, value.FromAmount);
        Assert.Equal(0.000027m, value.ToAmount);
        Assert.Equal(0.000009m, value.Price);
        var json = JObject.FromObject(value);
        Assert.All(json.Properties(), property => Assert.Equal(JTokenType.String, property.Value.Type));
        Assert.True(JToken.DeepEquals(JObject.Parse(Quote), json));
        Assert.Equal(value, json.ToObject<GateCrossExConvertQuote>());
    }

    [Theory]
    [InlineData("\"79228162514264337593543950335\"", "79228162514264337593543950335")]
    [InlineData("\"1e-28\"", "0.0000000000000000000000000001")]
    [InlineData("3", "3")]
    [InlineData("\"0\"", "0")]
    public void Quote_amounts_preserve_exact_decimal_boundaries(string token, string expected)
    {
        var json = JObject.Parse(Quote);
        foreach (var field in new[] { "from_amount", "to_amount", "price" }) json[field] = JToken.Parse(token);
        var value = JsonConvert.DeserializeObject<GateCrossExConvertQuote>(json.ToString())!;
        Assert.Equal(decimal.Parse(expected, CultureInfo.InvariantCulture), value.FromAmount);
        Assert.Equal(value.FromAmount, value.ToAmount);
        Assert.Equal(value.FromAmount, value.Price);
    }

    public static IEnumerable<object[]> InvalidSuccessFields()
    {
        foreach (var (kind, fields) in new[] { ("order", "order_id"), ("transfer", "tx_id,text"), ("quote", "quote_id,valid_ms,from_coin,to_coin,from_amount,to_amount,price") })
            foreach (var field in fields.Split(','))
                foreach (var token in new[] { "missing", "null", "true", "[]", "{}" }) yield return new object[] { kind, field, token };
    }

    [Theory]
    [MemberData(nameof(InvalidSuccessFields))]
    public async Task Every_required_success_field_fails_closed_with_HTTP_metadata(string kind, string field, string token)
    {
        var json = ResponseSeed(kind);
        if (token == "missing") json.Remove(field);
        else json[field] = JToken.Parse(token);
        var handler = Handler(json.ToString(Formatting.None));
        using var client = Client(handler);
        await AssertPostFailure(client, kind, HttpStatusCode.OK);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("order", "order_id")]
    [InlineData("transfer", "tx_id")]
    [InlineData("quote", "quote_id")]
    [InlineData("quote", "from_coin")]
    [InlineData("quote", "to_coin")]
    public async Task Blank_required_acknowledgement_identifiers_or_assets_are_not_success(string kind, string field)
    {
        var json = ResponseSeed(kind);
        json[field] = " ";
        var handler = Handler(json.ToString(Formatting.None));
        using var client = Client(handler);
        await AssertPostFailure(client, kind, HttpStatusCode.OK);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("[]")]
    [InlineData("1")]
    [InlineData("true")]
    [InlineData("\"text\"")]
    [InlineData("{invalid")]
    [InlineData("{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Not accepted\"}")]
    public async Task Malformed_or_error_HTTP_200_bodies_never_acknowledge_any_financial_action(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        foreach (var kind in new[] { "order", "transfer", "quote" }) await AssertPostFailure(client, kind, HttpStatusCode.OK);
        Assert.Equal(3, handler.Requests.Count);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task All_three_post_endpoints_preserve_HTTP_server_error_and_raw_metadata_without_retry(HttpStatusCode status)
    {
        const string json = "{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Rejected by server\"}";
        var handler = Handler(json, status);
        using var client = Client(handler);
        var order = await client.CrossEx.PlaceOrderAsync(OrderRequest());
        var transfer = await client.CrossEx.TransferAsync(TransferRequest());
        var quote = await client.CrossEx.GetConvertQuoteAsync(QuoteRequest());
        AssertFailure(order, status);
        AssertFailure(transfer, status);
        AssertFailure(quote, status);
        Assert.All(new[] { order.Error, transfer.Error, quote.Error }, error =>
        {
            Assert.Equal("CONTRACT_TEST_ERROR", error!.Data);
            Assert.Contains("Rejected by server", error.Message);
        });
        Assert.Equal(json, order.Raw);
        Assert.Equal(json, transfer.Raw);
        Assert.Equal(json, quote.Raw);
        Assert.Equal(3, handler.Requests.Count);
    }

    [Theory]
    [InlineData("order-null")]
    [InlineData("order-symbol")]
    [InlineData("order-side")]
    [InlineData("order-type")]
    [InlineData("order-time")]
    [InlineData("order-position")]
    [InlineData("order-price")]
    [InlineData("order-default-price")]
    [InlineData("order-qty")]
    [InlineData("order-spot-quote")]
    [InlineData("order-margin-quote")]
    [InlineData("order-text")]
    [InlineData("transfer-null")]
    [InlineData("transfer-coin")]
    [InlineData("transfer-from")]
    [InlineData("transfer-to")]
    [InlineData("quote-null")]
    [InlineData("quote-exchange")]
    [InlineData("quote-crossex")]
    [InlineData("quote-deribit")]
    [InlineData("quote-from")]
    [InlineData("quote-to")]
    public async Task Invalid_programmatic_instructions_fail_before_HTTP(string scenario)
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        var order = OrderRequest();
        var transfer = TransferRequest();
        var quote = QuoteRequest();
        switch (scenario)
        {
            case "order-symbol": order.Symbol = " "; break;
            case "order-side": order.Side = (GateCrossExOrderSide)0; break;
            case "order-type": order.Type = (GateCrossExOrderType)999; break;
            case "order-time": order.TimeInForce = (GateCrossExTimeInForce)999; break;
            case "order-position": order.PositionSide = (GateCrossExPositionSide)999; break;
            case "order-price": order.Type = GateCrossExOrderType.Limit; order.Price = null; break;
            case "order-default-price": order.Type = null; order.Price = null; break;
            case "order-qty": order.Quantity = null; break;
            case "order-spot-quote": order.Symbol = "BINANCE_SPOT_ADA_USDT"; order.QuoteQuantity = null; break;
            case "order-margin-quote": order.Symbol = "GATE_MARGIN_ADA_USDT"; order.PositionSide = GateCrossExPositionSide.Long; order.QuoteQuantity = null; break;
            case "order-text": order.Text = "CLIENT-ORDER-ID"; break;
            case "transfer-coin": transfer.Coin = " "; break;
            case "transfer-from": transfer.From = (GateCrossExTransferAccountType)0; break;
            case "transfer-to": transfer.To = (GateCrossExTransferAccountType)999; break;
            case "quote-exchange": quote.ExchangeType = (GateCrossExExchangeType)0; break;
            case "quote-crossex": quote.ExchangeType = GateCrossExExchangeType.CrossEx; break;
            case "quote-deribit": quote.ExchangeType = GateCrossExExchangeType.Deribit; break;
            case "quote-from": quote.FromCoin = " "; break;
            case "quote-to": quote.ToCoin = null!; break;
        }
        if (scenario.StartsWith("order"))
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.CrossEx.PlaceOrderAsync(scenario == "order-null" ? null! : order));
        else if (scenario.StartsWith("transfer"))
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.CrossEx.TransferAsync(scenario == "transfer-null" ? null! : transfer));
        else
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.CrossEx.GetConvertQuoteAsync(scenario == "quote-null" ? null! : quote));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public void Every_documented_enum_instruction_roundtrips_with_its_wire_value()
    {
        foreach (var side in Enum.GetValues<GateCrossExOrderSide>()) Assert.Equal(side, Roundtrip(OrderRequest() with { Side = side }).Side);
        foreach (var type in Enum.GetValues<GateCrossExOrderType>()) Assert.Equal(type, Roundtrip(OrderRequest() with { Type = type }).Type);
        foreach (var tif in Enum.GetValues<GateCrossExTimeInForce>()) Assert.Equal(tif, Roundtrip(OrderRequest() with { TimeInForce = tif }).TimeInForce);
        foreach (var side in Enum.GetValues<GateCrossExPositionSide>()) Assert.Equal(side, Roundtrip(OrderRequest() with { PositionSide = side }).PositionSide);
        foreach (var account in Enum.GetValues<GateCrossExTransferAccountType>())
        {
            var value = Roundtrip(TransferRequest() with { From = account, To = account });
            Assert.Equal(account, value.From);
            Assert.Equal(account, value.To);
        }
        foreach (var exchange in Enum.GetValues<GateCrossExExchangeType>()) Assert.Equal(exchange, Roundtrip(QuoteRequest() with { ExchangeType = exchange }).ExchangeType);
        // Deribit/CrossEx are valid shared enum members, but not allowed by the quote endpoint's preflight.
    }

    [Theory]
    [InlineData("1")]
    [InlineData("1.5")]
    [InlineData("true")]
    [InlineData("[]")]
    [InlineData("{}")]
    public void Saved_request_and_response_strings_never_coerce_other_JSON_types(string token)
    {
        foreach (var (kind, field) in new[] { ("order", "symbol"), ("order", "text"), ("transfer", "coin"), ("transfer", "text"), ("quote", "from_coin"), ("quote", "to_coin") })
        {
            var json = RequestSeed(kind);
            json[field] = JToken.Parse(token);
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), RequestType(kind)));
        }
        foreach (var (kind, field, type) in new[]
        {
            ("order", "order_id", typeof(GateCrossExOrderActionResult)), ("order", "text", typeof(GateCrossExOrderActionResult)),
            ("transfer", "tx_id", typeof(GateCrossExTransferResult)), ("transfer", "text", typeof(GateCrossExTransferResult)),
            ("quote", "quote_id", typeof(GateCrossExConvertQuote)), ("quote", "from_coin", typeof(GateCrossExConvertQuote)), ("quote", "to_coin", typeof(GateCrossExConvertQuote)),
        })
        {
            var json = ResponseSeed(kind);
            json[field] = JToken.Parse(token);
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject(json.ToString(), type));
        }
    }

    [Theory]
    [InlineData("1.0")]
    [InlineData("\"1e-29\"")]
    [InlineData("\"0.12345678901234567890123456789\"")]
    [InlineData("\"79228162514264337593543950336\"")]
    [InlineData("\"\"")]
    [InlineData("false")]
    public async Task Every_HTTP_quote_money_field_rejects_lossy_values(string token)
    {
        foreach (var field in new[] { "from_amount", "to_amount", "price" })
        {
            var json = JObject.Parse(Quote);
            json[field] = JToken.Parse(token);
            var handler = Handler(json.ToString(Formatting.None));
            using var client = Client(handler);
            await AssertPostFailure(client, "quote", HttpStatusCode.OK);
            Assert.Single(handler.Requests);
        }
    }

    [Theory]
    [InlineData("1")]
    [InlineData("\"TRUE\"")]
    [InlineData("\"1\"")]
    [InlineData("\"\"")]
    [InlineData("{}")]
    public void Invalid_saved_reduce_only_does_not_choose_false(string token)
    {
        var json = RequestSeed("order");
        json["reduce_only"] = JToken.Parse(token);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExOrderRequest>(json.ToString()));
    }

    [Fact]
    public void Optional_saved_order_values_remain_omitted_or_explicit_without_mode_inference()
    {
        var json = RequestSeed("order");
        foreach (var field in new[] { "type", "time_in_force", "qty", "price", "quote_qty", "reduce_only", "position_side", "text" }) json[field] = JValue.CreateNull();
        var value = JsonConvert.DeserializeObject<GateCrossExOrderRequest>(json.ToString())!;
        Assert.Null(value.Type);
        Assert.Null(value.TimeInForce);
        Assert.Null(value.PositionSide);
        Assert.Null(value.ReduceOnly);
        Assert.Equal(new[] { "side", "symbol" }, JObject.FromObject(value).Properties().Select(x => x.Name).OrderBy(x => x));
        json["reduce_only"] = false; // Legacy boolean JSON remains readable, writes a documented string.
        value = JsonConvert.DeserializeObject<GateCrossExOrderRequest>(json.ToString())!;
        Assert.False(value.ReduceOnly);
        Assert.Equal("false", (string?)JObject.FromObject(value)["reduce_only"]);
    }

    [Fact]
    public void Already_string_acknowledgement_IDs_remain_opaque_and_are_not_migrated_to_long()
    {
        const string id = "922337203685477580800001";
        var order = JObject.Parse(Action); order["order_id"] = id;
        var transfer = JObject.Parse(Transfer); transfer["tx_id"] = id;
        var quote = JObject.Parse(Quote); quote["quote_id"] = id;
        Assert.Equal(id, order.ToObject<GateCrossExOrderActionResult>()!.OrderId);
        Assert.Equal(id, transfer.ToObject<GateCrossExTransferResult>()!.TransactionId);
        Assert.Equal(id, quote.ToObject<GateCrossExConvertQuote>()!.QuoteId);
    }

    [Theory]
    [InlineData("order-text-64")]
    [InlineData("order-qty-zero")]
    [InlineData("order-qty-negative")]
    [InlineData("order-quote-zero")]
    [InlineData("order-quote-negative")]
    [InlineData("order-market-poc")]
    [InlineData("order-market-rpi")]
    [InlineData("order-margin-side-null")]
    [InlineData("order-margin-side-none")]
    [InlineData("quote-amount-zero")]
    [InlineData("quote-amount-negative")]
    [InlineData("quote-amount-17-decimals")]
    [InlineData("quote-amount-excess-trailing-scale")]
    [InlineData("quote-same-asset")]
    public async Task Static_rules_from_the_official_error_reference_fail_before_HTTP_without_repair(string scenario)
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        var order = OrderRequest();
        var quote = QuoteRequest();
        switch (scenario)
        {
            case "order-text-64": order.Text = new string('a', 64); break;
            case "order-qty-zero": order.Quantity = 0m; break;
            case "order-qty-negative": order.Quantity = -1m; break;
            case "order-quote-zero": order.QuoteQuantity = 0m; break;
            case "order-quote-negative": order.QuoteQuantity = -1m; break;
            case "order-market-poc": order.TimeInForce = GateCrossExTimeInForce.PendingOrCancelled; break;
            case "order-market-rpi": order.TimeInForce = GateCrossExTimeInForce.RetailPriceImprovement; break;
            case "order-margin-side-null": order.Symbol = "GATE_MARGIN_ADA_USDT"; order.QuoteQuantity = 10m; break;
            case "order-margin-side-none": order.Symbol = "GATE_MARGIN_ADA_USDT"; order.QuoteQuantity = 10m; order.PositionSide = GateCrossExPositionSide.None; break;
            case "quote-amount-zero": quote.FromAmount = 0m; break;
            case "quote-amount-negative": quote.FromAmount = -1m; break;
            case "quote-amount-17-decimals": quote.FromAmount = 0.00000000000000001m; break;
            case "quote-amount-excess-trailing-scale": quote.FromAmount = 1.00000000000000000m; break;
            case "quote-same-asset": quote.ToCoin = quote.FromCoin; break;
        }
        if (scenario.StartsWith("order")) await Assert.ThrowsAnyAsync<ArgumentException>(() => client.CrossEx.PlaceOrderAsync(order));
        else await Assert.ThrowsAnyAsync<ArgumentException>(() => client.CrossEx.GetConvertQuoteAsync(quote));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Sixty_three_character_text_and_sixteen_decimal_quote_scale_are_preserved()
    {
        var handler = new RecordingHttpMessageHandler(request => new HttpResponseMessage(HttpStatusCode.OK)
        { Content = new StringContent(request.RequestUri.AbsolutePath.EndsWith("/orders") ? Action : Quote, Encoding.UTF8, "application/json") });
        using var client = Client(handler);
        var text = new string('a', 63);
        var order = await client.CrossEx.PlaceOrderAsync(OrderRequest() with { Text = text });
        var quote = await client.CrossEx.GetConvertQuoteAsync(QuoteRequest() with { FromAmount = 0.0000000000000001m });
        Assert.True(order.Success, order.Error?.ToString());
        Assert.True(quote.Success, quote.Error?.ToString());
        Assert.Equal(text, (string?)JObject.Parse(handler.Requests[0].Content)["text"]);
        Assert.Equal("0.0000000000000001", (string?)JObject.Parse(handler.Requests[1].Content)["from_amount"]);
        Assert.Equal(2, handler.Requests.Count);
    }

    [Theory]
    [InlineData("COMMON_PARAM_BIND_ERROR")]
    [InlineData("CONVERT_TRADE_QUOTE_FROM_AMOUNT_INVALID_ERROR")]
    [InlineData("TRADE_LIGHTER_ORDER_LIMIT_ERROR")]
    public async Task Documented_labels_and_detail_alias_are_retained_without_automatic_financial_remedies(string label)
    {
        var handler = Handler(new JObject { ["label"] = label, ["detail"] = "Diagnostic only; do not trade to recover capacity" }.ToString(Formatting.None), HttpStatusCode.BadRequest);
        using var client = Client(handler);
        var result = await client.CrossEx.PlaceOrderAsync(OrderRequest());
        AssertFailure(result, HttpStatusCode.BadRequest);
        Assert.Equal(label, result.Error!.Data);
        Assert.Contains("Diagnostic only", result.Error.Message);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public void Saving_instructions_and_quotes_is_invariant_under_Turkish_culture()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
            Assert.Equal("0.65", (string?)JObject.FromObject(OrderRequest() with { Price = 0.65m })["price"]);
            Assert.Equal("242.45", (string?)JObject.FromObject(TransferRequest() with { Amount = 242.45m })["amount"]);
            Assert.Equal("0.00008", (string?)JObject.FromObject(QuoteRequest() with { FromAmount = 0.00008m })["from_amount"]);
            Assert.True(JToken.DeepEquals(JObject.Parse(Quote), JObject.FromObject(JsonConvert.DeserializeObject<GateCrossExConvertQuote>(Quote)!)));
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }

    private static T Roundtrip<T>(T value) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value))!;
    private static GateCrossExOrderRequest OrderRequest() => new() { Symbol = Symbol, Side = GateCrossExOrderSide.Buy, Type = GateCrossExOrderType.Market, Quantity = 0.1m };
    private static GateCrossExTransferRequest TransferRequest() => new() { Coin = "USDC", Amount = 1m, From = GateCrossExTransferAccountType.Spot, To = GateCrossExTransferAccountType.CrossExLighter };
    private static GateCrossExConvertQuoteRequest QuoteRequest() => new() { ExchangeType = GateCrossExExchangeType.Lighter, FromCoin = "LIGHTER_USDC", ToCoin = "CROSSEX_USDT", FromAmount = 1m };
    private static JObject RequestSeed(string kind) => kind switch
    {
        "order" => JObject.FromObject(OrderRequest()),
        "transfer" => JObject.FromObject(TransferRequest()),
        _ => JObject.FromObject(QuoteRequest()),
    };
    private static JObject ResponseSeed(string kind) => JObject.Parse(kind == "order" ? Action : kind == "transfer" ? Transfer : Quote);
    private static Type RequestType(string kind) => kind == "order" ? typeof(GateCrossExOrderRequest) : kind == "transfer" ? typeof(GateCrossExTransferRequest) : typeof(GateCrossExConvertQuoteRequest);

    private static async Task AssertPostFailure(GateRestApiClient client, string kind, HttpStatusCode status)
    {
        if (kind == "order") AssertFailure(await client.CrossEx.PlaceOrderAsync(OrderRequest()), status);
        else if (kind == "transfer") AssertFailure(await client.CrossEx.TransferAsync(TransferRequest()), status);
        else AssertFailure(await client.CrossEx.GetConvertQuoteAsync(QuoteRequest()), status);
    }

    private static void AssertFailure<T>(RestCallResult<T> result, HttpStatusCode status)
    {
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Null(result.Data);
        Assert.Equal(status, result.Response!.StatusCode);
    }

    private static void AssertContract<T>(string fields, string required)
    {
        var contract = (Newtonsoft.Json.Serialization.JsonObjectContract)new Newtonsoft.Json.Serialization.DefaultContractResolver().ResolveContract(typeof(T));
        Assert.Equal(fields.Split(',').OrderBy(x => x), contract.Properties.Select(x => x.PropertyName).OrderBy(x => x));
        Assert.Equal(required.Split(',').OrderBy(x => x), contract.Properties.Where(x => x.Required == Required.Always).Select(x => x.PropertyName).OrderBy(x => x));
    }

    private static void AssertRequest(RecordedHttpRequest request, string path)
    {
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal(path, request.RequestUri.AbsolutePath);
        Assert.Equal("", request.RequestUri.Query);
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var hash = Convert.ToHexString(SHA512.HashData(request.ContentBytes)).ToLowerInvariant();
        var signed = $"POST\n{path}\n\n{hash}\n{timestamp}";
        var signature = Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(signed))).ToLowerInvariant();
        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }

    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }
}
