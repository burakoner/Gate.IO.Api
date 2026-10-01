using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;
using Newtonsoft.Json;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Contract")]
public class FuturesStandardOrderTests
{
    private static GateFuturesOrderRequest Order() => new() { Contract = "BTC_USD1", Size = 1.5m, Price = 100m };

    [Theory]
    [InlineData("contract")]
    [InlineData("size")]
    [InlineData("price")]
    public void Required_saved_creation_fields_cannot_become_a_zero_instruction(string field)
    {
        var json = JObject.FromObject(Order());
        json.Property(field)!.Remove();
        Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
        json[field] = JValue.CreateNull();
        Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
    }

    [Theory]
    [InlineData("size")]
    [InlineData("price")]
    [InlineData("iceberg")]
    [InlineData("market_order_slip_ratio")]
    [InlineData("tpsl_tp_trigger_price")]
    [InlineData("tpsl_sl_trigger_price")]
    public void Explicit_invalid_saved_decimals_cannot_be_omitted_or_rounded(string field)
    {
        foreach (var value in new[] { "", " ", "Infinity", "∞", "1e-100", "0.12345678901234567890123456789", "1,000" })
        {
            var json = JObject.FromObject(Order());
            json[field] = value;
            Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
        }
    }

    [Theory]
    [InlineData("tif")]
    [InlineData("auto_size")]
    [InlineData("stp_act")]
    [InlineData("pos_margin_mode")]
    [InlineData("action_mode")]
    public void Unknown_saved_creation_enums_cannot_select_server_defaults(string field)
    {
        var json = JObject.FromObject(Order());
        json[field] = "unknown-explicit-value";
        Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
    }

    [Theory]
    [InlineData("../positions")]
    [InlineData("t-id/../positions")]
    [InlineData("t-id?scope=all")]
    [InlineData("t-id#fragment")]
    [InlineData("t-%2f")]
    [InlineData("t-id\n")]
    [InlineData(" ")]
    [InlineData("web")]
    public async Task Custom_id_cannot_change_a_single_order_route(string id)
    {
        var handler = Handler();
        using var client = Client(handler);
        foreach (var operation in new[] { "detail", "amend", "cancel" })
            await Assert.ThrowsAnyAsync<ArgumentException>(() => Invoke(client.Futures.USD1, operation, null, id));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Invalid_creation_instructions_fail_before_io_without_being_rewritten()
    {
        var handler = Handler();
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.Futures.USD1.PlaceOrderAsync((GateFuturesOrderRequest)null!));
        foreach (var order in new[]
        {
            Order() with { Contract = "BTC|_USD1" },
            Order() with { ClientOrderId = "t-id\n" },
            Order() with { TimeInForce = (GateFuturesTimeInForce)99 },
            Order() with { SelfTradeAction = (GateFuturesSelfTradeAction)99 },
            Order() with { ActionMode = (GateFuturesActionMode)99 },
            Order() with { PositionMarginMode = (GateFuturesPositionMarginMode)99 },
            Order() with { AutoSize = GateFuturesOrderAutoSize.None },
            Order() with { AutoSize = GateFuturesOrderAutoSize.CloseLong },
            Order() with { AutoSize = GateFuturesOrderAutoSize.CloseShort, Size = 0 },
            Order() with { Close = true },
            Order() with { Price = 0 },
            Order() with { Price = 0, TimeInForce = GateFuturesTimeInForce.GoodTillCancelled },
        })
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.PlaceOrderAsync(order));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(GateFuturesSettlement.BTC, "create", "POST")]
    [InlineData(GateFuturesSettlement.USDT, "create", "POST")]
    [InlineData(GateFuturesSettlement.USD1, "create", "POST")]
    [InlineData(GateFuturesSettlement.BTC, "detail", "GET")]
    [InlineData(GateFuturesSettlement.USDT, "detail", "GET")]
    [InlineData(GateFuturesSettlement.USD1, "detail", "GET")]
    [InlineData(GateFuturesSettlement.BTC, "amend", "PUT")]
    [InlineData(GateFuturesSettlement.USDT, "amend", "PUT")]
    [InlineData(GateFuturesSettlement.USD1, "amend", "PUT")]
    [InlineData(GateFuturesSettlement.BTC, "cancel", "DELETE")]
    [InlineData(GateFuturesSettlement.USDT, "cancel", "DELETE")]
    [InlineData(GateFuturesSettlement.USD1, "cancel", "DELETE")]
    public async Task All_four_signed_contracts_route_and_preserve_response_fields(GateFuturesSettlement settlement, string operation, string method)
    {
        var json = JObject.Parse(JsonFixture.Read("Docs/Futures/order.success.json"));
        json["id"] = "9007199254740993";
        json["user"] = long.MaxValue;
        json["refu"] = "9007199254740994";
        json["stp_id"] = long.MaxValue;
        json["create_time"] = 1514764800.125m;
        json["update_time"] = 1514764900.250m;
        json["finish_time"] = 1514765000.375m;
        var handler = Handler(json.ToString(), operation == "create" ? HttpStatusCode.Created : HttpStatusCode.OK);
        using var client = Client(handler);
        var result = await Invoke(client.Futures[settlement], operation);
        Assert.True(result.Success, result.Error?.ToString());
        var data = result.Data;
        Assert.Equal(9007199254740993L, data.OrderId);
        Assert.Equal(long.MaxValue, data.UserId);
        Assert.Equal(9007199254740994L, data.ReferenceUserId);
        Assert.Equal(long.MaxValue, data.SelfTradeActionId);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1514764800).UtcDateTime.AddMilliseconds(125), data.CreateTime);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1514764900).UtcDateTime.AddMilliseconds(250), data.UpdateTime);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1514765000).UtcDateTime.AddMilliseconds(375), data.FinishTime);
        Assert.Equal(GateFuturesOrderStatus.Finished, data.Status);
        Assert.Equal(GateFuturesOrderFinishAs.Filled, data.FinishAs);
        Assert.Equal("BTC_USDT", data.Contract); // Preserve the server's contract, do not substitute the route/body.
        Assert.Equal(10.5m, data.Size);
        Assert.Equal(0.5m, data.Iceberg);
        Assert.Equal(5.03m, data.Price);
        Assert.Equal(0m, data.Left);
        Assert.Equal(5.03m, data.FillPrice);
        Assert.False(data.IsClose);
        Assert.False(data.IsReduceOnly);
        Assert.False(data.IsLiquidation);
        Assert.Equal(GateFuturesTimeInForce.GoodTillCancelled, data.TimeInForce);
        Assert.Equal("t-test", data.ClientOrderId);
        Assert.Equal(0.00075m, data.TakerFee);
        Assert.Equal(-0.00025m, data.MakerFee);
        Assert.Equal(GateFuturesSelfTradeAction.CancelNewest, data.SelfTradeAction);
        Assert.Equal("t-amend", data.AmendText);
        Assert.Equal(0.01m, data.MarketOrderSlipRatio);
        Assert.Equal(GateFuturesPositionMarginMode.Isolated, data.PositionMarginMode);
        Assert.Equal(GateFuturesActionMode.Full, data.ActionMode);
        Assert.Equal(3800m, data.TakeProfitTriggerPrice);
        Assert.Equal(3700m, data.StopLossTriggerPrice);
        Assert.Equal("string", data.TakeProfitBboType);
        Assert.Equal("string", data.StopLossBboType);
        var request = Assert.Single(handler.Requests);
        var suffix = operation == "create" ? "" : "/9007199254740993";
        Assert.Equal($"/api/v4/futures/{settlement.ToString().ToLowerInvariant()}/orders{suffix}", request.RequestUri.AbsolutePath);
        Assert.Equal(method, request.Method.Method);
        Assert.Empty(request.RequestUri.Query);
        if (operation == "create")
            Assert.Equal(new[] { "contract", "price", "size" }, JObject.Parse(request.Content).Properties().Select(p => p.Name).OrderBy(n => n, StringComparer.Ordinal));
        else if (operation == "amend")
            Assert.Equal("100.123", JObject.Parse(request.Content)["price"]!.Value<string>());
        else Assert.Empty(request.Content);
        Assert.False(request.Headers.ContainsKey("x-gate-exptime"));
        AssertSignature(request);
    }

    [Theory]
    [InlineData("detail")]
    [InlineData("amend")]
    [InlineData("cancel")]
    public async Task Custom_identifier_and_numeric_boundaries_remain_literal(string operation)
    {
        var original = CultureInfo.CurrentCulture;
        try
        {
            var handler = Handler();
            using var client = Client(handler);
            foreach (var tag in new[] { "t-", "t-" + new string('A', 28), "t-a.B_c-9" })
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
                Assert.True((await Invoke(client.Futures.USD1, operation, null, tag)).Success);
                Assert.EndsWith("/" + tag, handler.Requests.Last().RequestUri.AbsolutePath);
                AssertSignature(handler.Requests.Last());
            }
            Assert.True((await Invoke(client.Futures.USD1, operation, long.MaxValue)).Success);
            Assert.EndsWith("/9223372036854775807", handler.Requests.Last().RequestUri.AbsolutePath);
            await Assert.ThrowsAnyAsync<ArgumentException>(() => Invoke(client.Futures.USD1, operation, null, "t-" + new string('a', 29)));
            await Assert.ThrowsAnyAsync<ArgumentException>(() => Invoke(client.Futures.USD1, operation, null));
            Assert.Equal(4, handler.Requests.Count);
        }
        finally { CultureInfo.CurrentCulture = original; }
    }

    [Fact]
    public async Task Explicit_creation_and_amendment_values_are_not_omitted_or_invented()
    {
        var handler = Handler();
        using var client = Client(handler);
        var market = Order() with { Price = 0, TimeInForce = GateFuturesTimeInForce.ImmediateOrCancel, Iceberg = 0, Close = false, ReduceOnly = false, MarketOrderSlipRatio = 0 };
        var original = market with { };
        Assert.True((await client.Futures.USD1.PlaceOrderAsync(market)).Success);
        var body = JObject.Parse(handler.Requests.Last().Content);
        Assert.Equal("0", body["price"]!.Value<string>());
        Assert.Equal("0", body["iceberg"]!.Value<string>());
        Assert.Equal("0", body["market_order_slip_ratio"]!.Value<string>());
        Assert.Equal("ioc", body["tif"]!.Value<string>());
        Assert.False(body["close"]!.Value<bool>());
        Assert.False(body["reduce_only"]!.Value<bool>());
        Assert.Equal(original, market);
        foreach (var side in new[] { GateFuturesOrderAutoSize.CloseLong, GateFuturesOrderAutoSize.CloseShort })
        {
            Assert.True((await client.Futures.USD1.PlaceOrderAsync(Order() with { Size = 0, AutoSize = side, ReduceOnly = true })).Success);
            Assert.Equal("0", JObject.Parse(handler.Requests.Last().Content)["size"]!.Value<string>());
        }
        Assert.True((await client.Futures.USD1.PlaceOrderAsync(Order() with { Size = 0, Close = true })).Success);
        Assert.True((await client.Futures.USD1.AmendOrderAsync(117, size: 0, price: 0, amendText: "", text: "", actionMode: GateFuturesActionMode.Acknowledge)).Success);
        body = JObject.Parse(handler.Requests.Last().Content);
        Assert.Equal(new[] { "action_mode", "amend_text", "price", "size", "text" }, body.Properties().Select(p => p.Name).OrderBy(n => n, StringComparer.Ordinal));
        Assert.Equal("0", body["size"]!.Value<string>());
        Assert.Equal("0", body["price"]!.Value<string>());
        Assert.Equal("", body["amend_text"]!.Value<string>());
        Assert.Equal("", body["text"]!.Value<string>());
        Assert.Equal("ACK", body["action_mode"]!.Value<string>());
        Assert.True((await client.Futures.USD1.AmendOrderAsync(117)).Success);
        Assert.Empty(handler.Requests.Last().Content);
        Assert.True((await client.Futures.USD1.CancelOrderAsync(117, actionMode: GateFuturesActionMode.Result)).Success);
        Assert.Equal("?action_mode=RESULT", handler.Requests.Last().RequestUri.Query);
        Assert.Empty(handler.Requests.Last().Content);
        Assert.All(handler.Requests, AssertSignature);
    }

    [Theory]
    [InlineData("create")]
    [InlineData("amend")]
    [InlineData("cancel")]
    public async Task Existing_receive_window_expresses_optional_expiration(string operation)
    {
        var handler = Handler();
        using var client = Client(handler, TimeSpan.FromSeconds(5));
        var before = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Assert.True((await Invoke(client.Futures.USD1, operation)).Success);
        var after = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var request = Assert.Single(handler.Requests);
        var expiry = long.Parse(Assert.Single(request.Headers["x-gate-exptime"]), CultureInfo.InvariantCulture);
        var timestamp = long.Parse(Assert.Single(request.Headers["Timestamp"]), CultureInfo.InvariantCulture);
        Assert.InRange(expiry, before + 4999, after + 5001);
        Assert.InRange(expiry - timestamp * 1000, 4000L, 6000L);
        AssertSignature(request);
    }

    [Theory]
    [InlineData("create")]
    [InlineData("detail")]
    [InlineData("amend")]
    [InlineData("cancel")]
    public async Task Partial_acknowledgement_is_not_fabricated_into_a_terminal_order(string operation)
    {
        var handler = Handler("{\"id\":9007199254740993,\"action_mode\":\"ACK\"}");
        using var client = Client(handler);
        var result = await Invoke(client.Futures.USD1, operation);
        Assert.True(result.Success);
        Assert.Equal(9007199254740993L, result.Data.OrderId);
        Assert.Equal(GateFuturesActionMode.Acknowledge, result.Data.ActionMode);
        Assert.Null(result.Data.FinishAs);
        Assert.Null(result.Data.FinishTime);
        Assert.NotEqual(GateFuturesOrderStatus.Finished, result.Data.Status);
        Assert.Null(result.Data.Contract);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("create")]
    [InlineData("detail")]
    [InlineData("amend")]
    [InlineData("cancel")]
    public async Task Http_errors_keep_metadata_and_are_not_retried(string operation)
    {
        foreach (var status in new[] { HttpStatusCode.BadRequest, HttpStatusCode.TooManyRequests, HttpStatusCode.InternalServerError })
        {
            var handler = Handler("{\"label\":\"INVALID_ARGUMENT\",\"message\":\"rejected\"}", status);
            using var client = Client(handler);
            var result = await Invoke(client.Futures.USD1, operation);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(status, result.Response!.StatusCode);
            Assert.Equal("rejected", result.Error!.Message);
            Assert.Contains("INVALID_ARGUMENT", result.Error.ToString());
            Assert.Single(handler.Requests);
        }
    }

    [Theory]
    [InlineData("0", "0")]
    [InlineData("-0.000", "0")]
    [InlineData("+01.2300e2", "123")]
    [InlineData("1e-28", "0.0000000000000000000000000001")]
    [InlineData("0.1234567890123456789012345678", "0.1234567890123456789012345678")]
    [InlineData("79228162514264337593543950335", "79228162514264337593543950335")]
    [InlineData("-79228162514264337593543950335", "-79228162514264337593543950335")]
    public void Exact_saved_decimal_strings_round_trip_without_precision_loss(string text, string expected)
    {
        var json = JObject.FromObject(Order());
        json["size"] = text;
        var request = json.ToObject<GateFuturesOrderRequest>()!;
        Assert.Equal(decimal.Parse(expected, CultureInfo.InvariantCulture), request.Size);
        Assert.Equal(request.Size, JsonConvert.DeserializeObject<GateFuturesOrderRequest>(JsonConvert.SerializeObject(request))!.Size);
    }

    [Fact]
    public void Numeric_saved_inputs_require_exact_tokens_and_optional_nulls_remain_optional()
    {
        var json = JObject.FromObject(Order());
        json["size"] = long.MaxValue;
        json["iceberg"] = JValue.CreateNull();
        json["tif"] = JValue.CreateNull();
        var request = json.ToObject<GateFuturesOrderRequest>()!;
        Assert.Equal((decimal)long.MaxValue, request.Size);
        Assert.Null(request.Iceberg);
        Assert.Null(request.TimeInForce);
        json["size"] = 0.1234567890123456789012345678m;
        Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
        json["size"] = 1.1d;
        Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
        json["size"] = true;
        Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
    }

    [Fact]
    public void Decimal_aware_reader_cannot_make_floating_json_an_exact_saved_instruction()
    {
        foreach (var value in new[] { "1e-100", "0.12345678901234567890123456789", "1.1" })
        {
            var json = JObject.FromObject(Order());
            json["size"] = new JRaw(value);
            Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateFuturesOrderRequest>(json.ToString(),
                new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal }));
        }
    }

    [Theory]
    [InlineData("9223372036854775808")]
    [InlineData("task-not-numeric")]
    [InlineData("1.5")]
    public async Task Invalid_numeric_response_identity_cannot_be_rounded_or_converted_to_string(string id)
    {
        var handler = Handler(new JObject { ["id"] = id }.ToString());
        using var client = Client(handler);
        var result = await client.Futures.USD1.GetOrderAsync(117);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("id")]
    [InlineData("user")]
    [InlineData("refu")]
    [InlineData("stp_id")]
    public async Task Invalid_numeric_response_ids_are_not_coerced_into_another_identity(string field)
    {
        foreach (var value in new JToken[] { new JValue(1.5m), new JValue(true), new JValue("1.5"), new JValue("9223372036854775808") })
        {
            var handler = Handler(new JObject { [field] = value }.ToString());
            using var client = Client(handler);
            var result = await client.Futures.USD1.GetOrderAsync(117);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }
    }

    [Fact]
    public void Saved_position_id_is_exact_and_retains_numeric_json_output()
    {
        var json = JObject.FromObject(Order());
        foreach (var value in new JToken[] { new JValue(1.5m), new JValue(true), new JValue("1.5"), new JValue("9223372036854775808") })
        {
            json["pid"] = value;
            Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesOrderRequest>());
        }
        json["pid"] = "9007199254740993";
        var request = json.ToObject<GateFuturesOrderRequest>()!;
        Assert.Equal(9007199254740993L, request.PositionId);
        var output = JObject.FromObject(request)["pid"]!;
        Assert.Equal(JTokenType.Integer, output.Type);
        Assert.Equal(9007199254740993L, output.Value<long>());
        json["pid"] = JValue.CreateNull();
        Assert.Null(json.ToObject<GateFuturesOrderRequest>()!.PositionId);
    }

    [Theory]
    [InlineData("Docs/Futures/order.success.json")]
    [InlineData("Docs/Delivery/order.success.json")]
    public void Shared_order_identity_contract_keeps_integer_ranges_and_omission(string fixture)
    {
        var json = JObject.Parse(JsonFixture.Read(fixture));
        json["id"] = long.MinValue;
        json["user"] = "0";
        json["refu"] = 0;
        json["stp_id"] = JValue.CreateNull();
        var order = json.ToObject<GateFuturesOrder>()!;
        Assert.Equal(long.MinValue, order.OrderId); // Parsing does not invent a positive-ID rule for server data.
        Assert.Equal(0L, order.UserId);
        Assert.Equal(0L, order.ReferenceUserId);
        Assert.Null(order.SelfTradeActionId);
        Assert.Equal(JTokenType.Integer, JObject.FromObject(order)["id"]!.Type);
        json.Property("id")!.Remove();
        Assert.Equal(0L, json.ToObject<GateFuturesOrder>()!.OrderId);
    }

    [Fact]
    public void Shared_batch_error_item_does_not_require_a_full_order()
    {
        var item = JsonConvert.DeserializeObject<GateFuturesBatchOrder>("{\"succeeded\":false,\"label\":\"INVALID_ARGUMENT\",\"detail\":\"rejected\"}")!;
        Assert.False(item.Succeeded);
        Assert.Equal("INVALID_ARGUMENT", item.ErrorLabel);
        Assert.Equal(0L, item.OrderId);
        Assert.Null(item.Contract);
    }

    [Fact]
    public async Task Legacy_positional_creation_signature_accepts_its_original_token_argument()
    {
        var handler = Handler();
        using var client = Client(handler);
        using var token = new CancellationTokenSource();
        Assert.True((await client.Futures.USD1.PlaceOrderAsync("BTC_USD1", 1.5m, null, 100m, null, null, "t-legacy", null, null, null, token.Token)).Success);
        AssertSignature(Assert.Single(handler.Requests));
    }

    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.Equal("1", Assert.Single(request.Headers["X-Gate-Size-Decimal"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        Assert.True(long.TryParse(timestamp, NumberStyles.Integer, CultureInfo.InvariantCulture, out _));
        var bodyHash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var input = $"{request.Method.Method}\n{request.RequestUri.AbsolutePath}\n{request.RequestUri.Query.TrimStart('?')}\n{bodyHash}\n{timestamp}";
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes("secret"));
        Assert.Equal(Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(input))).ToLowerInvariant(), Assert.Single(request.Headers["SIGN"]));
    }

    [Fact]
    public async Task One_character_base_contract_is_not_rejected_by_an_undocumented_minimum()
    {
        var handler = Handler();
        using var client = Client(handler);
        Assert.True((await client.Futures.USD1.PlaceOrderAsync(Order() with { Contract = "A_USD1" })).Success);
        Assert.Equal("A_USD1", JObject.Parse(Assert.Single(handler.Requests).Content)["contract"]!.Value<string>());
    }

    [Fact]
    public async Task Numeric_ids_and_action_modes_are_validated_before_io()
    {
        var handler = Handler();
        using var client = Client(handler);
        foreach (var operation in new[] { "detail", "amend", "cancel" })
        {
            foreach (var id in new[] { 0L, -1L })
                await Assert.ThrowsAnyAsync<ArgumentException>(() => Invoke(client.Futures.USD1, operation, id));
            await Assert.ThrowsAnyAsync<ArgumentException>(() => Invoke(client.Futures.USD1, operation, 117, "t-id"));
        }
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.CancelOrderAsync(117, actionMode: (GateFuturesActionMode)99));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.AmendOrderAsync(117, actionMode: (GateFuturesActionMode)99));
        Assert.Empty(handler.Requests);
    }

    private static Task<ApiSharp.Models.RestCallResult<GateFuturesOrder>> Invoke(GateFuturesRestApiSettleClient api, string operation, long? id = 9007199254740993, string text = null!)
        => operation switch
        {
            "create" => api.PlaceOrderAsync(Order()),
            "detail" => api.GetOrderAsync(id, text),
            "amend" => api.AmendOrderAsync(id, text, price: 100.123m),
            "cancel" => api.CancelOrderAsync(id, text),
            _ => throw new ArgumentOutOfRangeException(nameof(operation)),
        };

    private static RecordingHttpMessageHandler Handler(string? json = null, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json ?? JsonFixture.Read("Docs/Futures/order.success.json"), Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler, TimeSpan? receiveWindow = null)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler), ReceiveWindow = receiveWindow });
        client.SetApiCredentials("key", "secret");
        return client;
    }
}
