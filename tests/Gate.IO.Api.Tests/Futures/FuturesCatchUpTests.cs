using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Text;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Unit")]
public class FuturesCatchUpTests
{
    // Current reference: https://www.gate.com/docs/developers/apiv4/en/futures/
    [Theory]
    [InlineData(GateFuturesSettlement.BTC)]
    [InlineData(GateFuturesSettlement.USDT)]
    [InlineData(GateFuturesSettlement.USD1)]
    public async Task Fee_is_signed_get_with_query_and_no_body(GateFuturesSettlement settle)
    {
        var handler = Handler("{\"BTC_USDT\":{\"taker_fee\":\"0.0005\",\"maker_fee\":\"-0.0001\"}}");
        using var client = Client(handler);
        var result = await client.Futures[settle].GetTradingFeesAsync("BTC_USDT");
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(-0.0001m, result.Data["BTC_USDT"].MakerFee);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Contains("contract=BTC_USDT", request.RequestUri.Query);
        Assert.Empty(request.Content);
        Assert.Contains("SIGN", request.Headers.Keys);
    }

    [Theory]
    [InlineData(GateFuturesSettlement.BTC)]
    [InlineData(GateFuturesSettlement.USDT)]
    [InlineData(GateFuturesSettlement.USD1)]
    public async Task Batch_cancel_is_post_and_preserves_int64_ids_as_wire_strings(GateFuturesSettlement settle)
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        Assert.True((await client.Futures[settle].CancelOrdersAsync(new[] { 9007199254740993L, long.MaxValue })).Success);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.EndsWith("/batch_cancel_orders", request.RequestUri.AbsolutePath);
        Assert.Equal("[\"9007199254740993\",\"9223372036854775807\"]", request.Content);
    }

    [Fact]
    public void Current_optional_metadata_is_not_dropped_or_defaulted()
    {
        var account = JsonConvert.DeserializeObject<GateFuturesBalance>("{\"enable_dual_plus\":true,\"position_mode\":\"dual_plus\",\"history\":{\"cross_settle\":\"-1.25\"}}")!;
        var accountJson = JObject.FromObject(account);
        Assert.True(accountJson["enable_dual_plus"]?.Value<bool>());
        Assert.Equal("dual_plus", accountJson["position_mode"]?.Value<string>());
        Assert.Equal(-1.25m, accountJson["history"]?["cross_settle"]?.Value<decimal>());
        var contract = JObject.FromObject(JsonConvert.DeserializeObject<GateFuturesContract>("{\"enable_circuit_breaker\":false}")!);
        Assert.False(contract["enable_circuit_breaker"]?.Value<bool>());
    }

    [Theory]
    [InlineData(GateFuturesSettlement.BTC)]
    [InlineData(GateFuturesSettlement.USDT)]
    [InlineData(GateFuturesSettlement.USD1)]
    public async Task Contract_quantities_and_leverage_keep_fractions_on_all_three_contract_queries(GateFuturesSettlement settle)
    {
        const string json = "{\"name\":\"BTC_USDT\",\"enable_decimal\":true,\"leverage_min\":\"1.5\",\"leverage_max\":\"100.5\",\"order_size_min\":\"0.1\",\"order_size_max\":\"10000.5\",\"trade_size\":\"123456789.125\",\"position_size\":\"100.75\",\"orderbook_id\":\"9007199254740993\",\"trade_id\":\"9223372036854775807\"}";
        var responses = new Queue<string>(["[" + json + "]", json, "[" + json + "]"]);
        var handler = new RecordingHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responses.Dequeue(), Encoding.UTF8, "application/json") });
        using var client = Client(handler);
        var api = client.Futures[settle];
        var list = await api.GetContractsAsync();
        var detail = await api.GetContractAsync("BTC_USDT");
        var all = await api.GetAllContractsAsync();
        Assert.True(list.Success && detail.Success && all.Success);
        foreach (var contract in new[] { Assert.Single(list.Data), detail.Data, Assert.Single(all.Data) })
        {
            Assert.True(contract.EnableDecimal);
            Assert.Equal(1.5m, contract.MinimumLeverage);
            Assert.Equal(100.5m, contract.MaximumLeverage);
            Assert.Equal(0.1m, contract.OrderSizeMinimum);
            Assert.Equal(10000.5m, contract.OrderSizeMaximum);
            Assert.Equal(123456789.125m, contract.TradeSize);
            Assert.Equal(100.75m, contract.PositionSize);
            Assert.Equal(9007199254740993L, contract.OrderbookId);
            Assert.Equal(long.MaxValue, contract.TradeId);
        }
    }

    [Theory]
    [InlineData("leverage_min")] [InlineData("leverage_max")]
    [InlineData("order_size_min")] [InlineData("order_size_max")]
    [InlineData("trade_size")] [InlineData("position_size")]
    public void Contract_numeric_strings_fail_instead_of_rounding_or_underflowing(string field)
    {
        foreach (var value in new[] { "true", "0.1", "\"1e-29\"", "\"79228162514264337593543950336\"", "\"0.12345678901234567890123456789\"", "\"NaN\"", "null" })
            Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateFuturesContract>($"{{\"{field}\":{value}}}"));
    }

    [Theory]
    [InlineData(GateFuturesSettlement.BTC)]
    [InlineData(GateFuturesSettlement.USDT)]
    [InlineData(GateFuturesSettlement.USD1)]
    public async Task New_paths_and_filters_preserve_query_body_and_optional_omission(GateFuturesSettlement settle)
    {
        var responses = new Queue<string>(["[]", "{}", "{\"position_mode\":\"dual_plus\"}", JsonFixture.Read("Docs/Futures/order.success.json"), "[]", "[]", "[]"]);
        var handler = new RecordingHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(responses.Dequeue(), Encoding.UTF8, "application/json") });
        using var client = Client(handler);
        var api = client.Futures[settle];
        Assert.True((await api.GetAllContractsAsync(25, 2)).Success);
        Assert.True((await api.SetPositionLeverageAsync("BTC_USDT", 12.5m, GateFuturesPositionMarginMode.Cross)).Success);
        Assert.Equal("dual_plus", (await api.SetPositionModeAsync(GateFuturesAccountPositionMode.DualPlus)).Data.PositionMode);
        Assert.True((await api.PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USDT", Size = -2, Direction = GateFuturesBboDirection.Buy, Level = 20, Iceberg = 0, ReduceOnly = false, PositionId = 9007199254740993 })).Success);
        Assert.True((await api.GetUserLiquidationsAsync(new GateFuturesUserLiquidationQueryRequest { Contract = "BTC_USDT", From = Epoch(1780000000), To = Epoch(1780000010), At = Epoch(1780000005), Limit = 5, Offset = 0 })).Success);
        Assert.True((await api.GetCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "BTC_USDT", Interval = GateFuturesCandlestickInterval.NaturalWeek, From = Epoch(1780000000), Timezone = "utc8" })).Success);
        Assert.True((await api.GetMarkPriceCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "BTC_USDT", Interval = GateFuturesCandlestickInterval.OneMonth, Timezone = "all" })).Success);
        Assert.Equal("25", Query(handler.Requests[0])["limit"]);
        Assert.Equal("2", Query(handler.Requests[0])["offset"]);
        Assert.Equal(new[] { "leverage", "margin_mode" }, Query(handler.Requests[1]).Keys.Order().ToArray());
        Assert.Equal("12.5", Query(handler.Requests[1])["leverage"]);
        Assert.Equal("cross", Query(handler.Requests[1])["margin_mode"]);
        Assert.Equal("dual_plus", Query(handler.Requests[2])["position_mode"]);
        Assert.Empty(handler.Requests[1].Content);
        Assert.Empty(handler.Requests[2].Content);
        var bbo = JObject.Parse(handler.Requests[3].Content);
        Assert.Equal(JTokenType.Integer, bbo["size"]!.Type);
        Assert.Equal(-2, bbo["size"]!.Value<long>());
        Assert.Equal("buy", bbo["direction"]!.Value<string>());
        Assert.Equal(20, bbo["level"]!.Value<long>());
        Assert.Equal(0, bbo["iceberg"]!.Value<long>());
        Assert.False(bbo["reduce_only"]!.Value<bool>());
        Assert.Equal(9007199254740993, bbo["pid"]!.Value<long>());
        Assert.Null(bbo["price"]);
        var liquidation = Query(handler.Requests[4]);
        Assert.Equal(new[] { "at", "contract", "from", "limit", "offset", "to" }, liquidation.Keys.Order().ToArray());
        Assert.Equal("1780000000", liquidation["from"]);
        Assert.Equal("1780000010", liquidation["to"]);
        Assert.Equal("1780000005", liquidation["at"]);
        Assert.Equal("0", liquidation["offset"]);
        Assert.Equal("1w", Query(handler.Requests[5])["interval"]);
        Assert.Equal("utc8", Query(handler.Requests[5])["timezone"]);
        Assert.False(Query(handler.Requests[5]).ContainsKey("limit"));
        Assert.Equal("mark_BTC_USDT", Query(handler.Requests[6])["contract"]);
        Assert.Equal("30d", Query(handler.Requests[6])["interval"]);
        Assert.Equal("all", Query(handler.Requests[6])["timezone"]);
    }

    [Fact]
    public async Task Public_risk_table_works_without_api_credentials()
    {
        var handler = Handler("[]");
        using var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        Assert.True((await client.Futures.USD1.GetRiskLimitTableAsync("TABLE_ID")).Success);
        Assert.DoesNotContain("SIGN", Assert.Single(handler.Requests).Headers.Keys);
    }

    [Fact]
    public async Task Each_batch_keeps_partial_failures_and_nullable_old_adl_identity()
    {
        var responses = new Queue<string>([
            "[{\"id\":\"9007199254740993\",\"succeeded\":true},{\"succeeded\":false,\"label\":\"BALANCE_NOT_ENOUGH\",\"detail\":\"insufficient\"}]",
            "[{\"id\":117,\"succeeded\":true},{\"succeeded\":false,\"label\":\"ORDER_NOT_FOUND\",\"detail\":\"missing\"}]",
            "[{\"id\":\"117\",\"user_id\":111,\"succeeded\":true},{\"id\":\"118\",\"user_id\":111,\"succeeded\":false,\"message\":\"ORDER_NOT_FOUND\"}]",
            "[{\"user\":\"9007199254740993\",\"order_id\":null}]",
        ]);
        var handler = new RecordingHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responses.Dequeue(), Encoding.UTF8, "application/json") });
        using var client = Client(handler);
        var create = await client.Futures.USD1.PlaceOrdersAsync(new[] { Order(), Order() });
        Assert.True(create.Success);
        Assert.True(create.Data[0].Succeeded);
        Assert.Equal(9007199254740993, create.Data[0].OrderId);
        Assert.False(create.Data[1].Succeeded);
        Assert.Equal("BALANCE_NOT_ENOUGH", create.Data[1].ErrorLabel);
        Assert.Equal(0, create.Data[1].OrderId); // Error-only row is not evidence of a created order.
        var amend = await client.Futures.USD1.AmendOrdersAsync(new[] { new GateFuturesOrderAmendRequest { OrderId = 117 }, new GateFuturesOrderAmendRequest { ClientOrderId = "t-id" } });
        Assert.True(amend.Success);
        Assert.False(amend.Data[1].Succeeded);
        Assert.Equal("ORDER_NOT_FOUND", amend.Data[1].Label);
        var cancel = await client.Futures.USD1.CancelOrdersAsync(new[] { 117L, 118L });
        Assert.True(cancel.Success);
        Assert.Equal(111, cancel.Data[0].UserId);
        Assert.False(cancel.Data[1].Succeeded);
        Assert.Equal("ORDER_NOT_FOUND", cancel.Data[1].Message);
        var adl = await client.Futures.USD1.GetAdlHistoryAsync();
        Assert.True(adl.Success);
        Assert.Null(Assert.Single(adl.Data).OrderId);
    }

    [Fact]
    public async Task Batch_inputs_are_enumerated_once_and_writing_is_culture_invariant()
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        var original = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
            Assert.True((await client.Futures.USD1.PlaceOrdersAsync(new Once<GateFuturesOrderRequest>([Order()]))).Success);
            Assert.True((await client.Futures.USD1.AmendOrdersAsync(new Once<GateFuturesOrderAmendRequest>([new() { OrderId = long.MaxValue, Size = -1.25m, Price = 100.5m }]))).Success);
            Assert.True((await client.Futures.USD1.CancelOrdersAsync(new Once<long>([long.MaxValue]))).Success);
            Assert.Equal("1.25", JArray.Parse(handler.Requests[0].Content)[0]["size"]!.Value<string>());
            Assert.Equal("-1.25", JArray.Parse(handler.Requests[1].Content)[0]["size"]!.Value<string>());
            Assert.Equal(long.MaxValue, JArray.Parse(handler.Requests[1].Content)[0]["order_id"]!.Value<long>());
            Assert.Equal("[\"9223372036854775807\"]", handler.Requests[2].Content);
        }
        finally { CultureInfo.CurrentCulture = original; }
    }

    [Theory]
    [InlineData("create", 0)] [InlineData("create", 11)]
    [InlineData("amend", 0)] [InlineData("amend", 11)]
    [InlineData("cancel", 0)] [InlineData("cancel", 21)]
    public async Task Invalid_batch_counts_do_not_reach_the_transport(string kind, int count)
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        Task Call() => kind switch
        {
            "create" => client.Futures.USD1.PlaceOrdersAsync(Enumerable.Range(0, count).Select(_ => Order())),
            "amend" => client.Futures.USD1.AmendOrdersAsync(Enumerable.Range(0, count).Select(_ => new GateFuturesOrderAmendRequest { OrderId = 117 })),
            _ => client.Futures.USD1.CancelOrdersAsync(Enumerable.Range(1, count).Select(x => (long)x)),
        };
        await Assert.ThrowsAnyAsync<ArgumentException>(Call);
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(0)] [InlineData(5)]
    public async Task Countdown_allows_disable_and_documented_minimum(int timeout)
    {
        var handler = Handler("{\"triggerTime\":1780000000123}");
        using var client = Client(handler);
        var result = await client.Futures.USD1.CancelAllAsync(timeout);
        Assert.True(result.Success);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1780000000123).UtcDateTime, result.Data);
        Assert.Equal(timeout, JObject.Parse(Assert.Single(handler.Requests).Content)["timeout"]!.Value<int>());
    }

    [Theory]
    [InlineData(-1)] [InlineData(1)] [InlineData(4)]
    public async Task Countdown_rejects_invalid_seconds_before_io(int timeout)
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Futures.USD1.CancelAllAsync(timeout));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("{}")] [InlineData("{\"triggerTime\":null}")]
    public async Task Countdown_missing_timestamp_does_not_fabricate_a_successful_date(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Futures.USD1.CancelAllAsync(5);
        Assert.False(result.Success);
        Assert.IsType<ApiSharp.Models.DeserializeError>(result.Error);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("create", "{\"code\":123,\"message\":\"denied\"}")]
    [InlineData("detail", "{\"code\":123,\"message\":\"denied\"}")]
    [InlineData("create", "{\"code\":0}")]
    [InlineData("detail", "{\"code\":0,\"data\":{}}")]
    [InlineData("create", "{\"code\":0.1,\"data\":{\"id\":117}}")]
    [InlineData("detail", "{\"code\":false,\"data\":{\"order\":{\"id\":117}}}")]
    [InlineData("create", "{\"data\":{\"id\":117}}")]
    [InlineData("detail", "{\"code\":\"0\",\"data\":{\"order\":{\"id\":117}}}")]
    public async Task Trail_business_or_malformed_envelopes_are_not_reported_as_success(string operation, string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        if (operation == "create")
        {
            var result = await client.Futures.USD1.PlaceTrailOrderAsync(new GateFuturesTrailOrderRequest { Contract = "BTC_USD1", Amount = 1 });
            Assert.False(result.Success);
            Assert.NotNull(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            Assert.Equal(0, result.Data);
        }
        else
        {
            var result = await client.Futures.USD1.GetTrailOrderAsync(117);
            Assert.False(result.Success);
            Assert.NotNull(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            Assert.Null(result.Data);
        }
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(false)] [InlineData(true)]
    public async Task Trail_stop_and_update_accept_both_documented_shapes_without_losing_order_fields(bool boxed)
    {
        const string order = "{\"id\":\"9007199254740993\",\"amount\":\"-1.25\",\"position_mode\":\"dual_plus\",\"error_label\":\"BALANCE_NOT_ENOUGH\",\"status_code\":\"failed\"}";
        var handler = Handler(boxed ? "{\"order\":" + order + "}" : order);
        using var client = Client(handler);
        var stop = await client.Futures.USD1.CancelTrailOrderAsync("apiv4");
        var update = await client.Futures.USD1.UpdateTrailOrderAsync(new GateFuturesTrailOrderUpdateRequest { OrderId = 117, IsGreaterThanOrEqual = false, PriceType = GateFuturesTrailPriceType.Unknown, PriceOffset = "" });
        foreach (var result in new[] { stop, update })
        {
            Assert.True(result.Success);
            Assert.Equal(9007199254740993, result.Data.OrderId);
            Assert.Equal(-1.25m, result.Data.Amount);
            Assert.Equal("dual_plus", result.Data.PositionMode);
            Assert.Equal("failed", result.Data.StatusCode);
            Assert.Equal("BALANCE_NOT_ENOUGH", result.Data.ErrorLabel); // Reading an error-bearing order is not successful execution.
        }
        var body = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("false", body["is_gte_str"]!.Value<string>());
        Assert.Equal(0, body["price_type"]!.Value<int>());
        Assert.Equal("", body["price_offset"]!.Value<string>());
        Assert.Null(body["is_gte"]);
    }

    [Theory]
    [InlineData("{}")] [InlineData("{\"orders\":null}")] [InlineData("{\"order\":null}")]
    public async Task Missing_strategy_data_is_not_an_empty_success(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        Assert.False((await client.Futures.USD1.GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest())).Success);
        Assert.False((await client.Futures.USD1.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest())).Success);
        Assert.False((await client.Futures.USD1.GetChaseOrderAsync("117")).Success);
        Assert.False((await client.Futures.USD1.PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest { Contract = "BTC_USD1", Amount = "1", PriceLimit = "0" })).Success);
        Assert.Equal(4, handler.Requests.Count);
    }

    [Theory]
    [InlineData("{}")] [InlineData("null")] [InlineData("[null]")] [InlineData("[[],null]")]
    public async Task Malformed_funding_array_is_a_parse_failure_not_empty_success(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var result = await client.Futures.USD1.GetBatchFundingRateHistoryAsync(new[] { "BTC_USD1" });
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task History_response_keeps_string_epochs_decimal_quantities_and_separate_trade_identity_fields()
    {
        var history = Handler("[{\"id\":\"9007199254740993\",\"create_time\":\"1780000000.123\",\"update_time\":\"1780000001.456\",\"finish_time\":\"1780000002.789\",\"size\":\"-1.25\"}]");
        using var client = Client(history);
        var result = await client.Futures.USD1.GetOrdersAsync(new GateFuturesOrderTimeRangeQueryRequest());
        Assert.True(result.Success, result.Error?.ToString());
        var order = Assert.Single(result.Data);
        Assert.Equal(9007199254740993, order.OrderId);
        Assert.Equal(-1.25m, order.Size);
        Assert.Equal(Epoch(1780000000).AddMilliseconds(123), order.CreateTime);
        Assert.Equal(Epoch(1780000001).AddMilliseconds(456), order.UpdateTime);
        Assert.Equal(Epoch(1780000002).AddMilliseconds(789), order.FinishTime);
        var userTrade = JsonConvert.DeserializeObject<GateFuturesUserTrade>("{\"trade_id\":\"9223372036854775807\",\"order_id\":\"9007199254740993\",\"size\":\"-1.25\",\"trade_value\":\"10.25\"}")!;
        Assert.Equal(long.MaxValue, userTrade.TradeId);
        Assert.Equal(9007199254740993, userTrade.OrderId);
        Assert.Equal(-1.25m, userTrade.Size);
        Assert.Equal(10.25m, userTrade.TradeValue);
        Assert.Equal(0, userTrade.Id); // No fabricated alias from a different field.
    }

    [Theory]
    [InlineData("\"-1.25\"", -1.25)] [InlineData("25", 25)] [InlineData("\"0\"", 0)]
    public void Public_trade_quantity_is_decimal_but_identity_remains_exact_int64(string size, decimal expected)
    {
        var trade = JsonConvert.DeserializeObject<GateFuturesTrade>($"{{\"id\":\"9007199254740993\",\"size\":{size}}}")!;
        Assert.Equal(expected, trade.Size);
        Assert.Equal(9007199254740993, trade.Id);
    }

    [Theory]
    [InlineData("1.25")] [InlineData("true")] [InlineData("\"NaN\"")] [InlineData("\"1e-30\"")]
    public void Invalid_public_quantity_cannot_round_or_default_to_zero(string size)
        => Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateFuturesTrade>($"{{\"size\":{size}}}"));

    [Fact]
    public async Task Bbo_full_hedge_close_does_not_import_standard_orders_reduce_only_requirement()
    {
        foreach (var reduceOnly in new bool?[] { null, false, true })
        {
            var handler = Handler(JsonFixture.Read("Docs/Futures/order.success.json"));
            using var client = Client(handler);
            var result = await client.Futures.USD1.PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USD1", Size = 0, Direction = GateFuturesBboDirection.Buy, Level = 1, AutoSize = GateFuturesOrderAutoSize.CloseShort, ReduceOnly = reduceOnly });
            Assert.True(result.Success);
            var body = JObject.Parse(Assert.Single(handler.Requests).Content);
            Assert.Equal(0, body["size"]!.Value<long>());
            Assert.Equal("close_short", body["auto_size"]!.Value<string>());
            if (reduceOnly.HasValue) Assert.Equal(reduceOnly, body["reduce_only"]!.Value<bool>());
            else Assert.Null(body["reduce_only"]);
        }
    }

    [Fact]
    public async Task Invalid_bbo_batch_and_strategy_instructions_are_rejected_before_io()
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        var api = client.Futures.USD1;
        foreach (var level in new[] { 0L, 21L })
            await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USD1", Size = 1, Direction = GateFuturesBboDirection.Buy, Level = level }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USD1", Size = 1, Direction = (GateFuturesBboDirection)99, Level = 1 }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USD1", Size = 1, Direction = GateFuturesBboDirection.Buy, Level = 1, Close = true }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.AmendOrdersAsync(new[] { new GateFuturesOrderAmendRequest() }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceOrdersAsync(new GateFuturesOrderRequest[] { null! }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.CancelOrdersAsync(new[] { 0L }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.SetPositionModeAsync((GateFuturesAccountPositionMode)99));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.SetPositionLeverageAsync("BTC_USD1", 10, (GateFuturesPositionMarginMode)99));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.CancelOrdersAsync(new GateFuturesOrderCancelAllRequest { Contract = " " }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceTrailOrderAsync(new GateFuturesTrailOrderRequest { Contract = "BTC_USD1", Amount = 1, PositionRelated = true, ReduceOnly = false }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceTrailOrderAsync(new GateFuturesTrailOrderRequest { Contract = "BTC_USD1", Amount = 1, PriceType = GateFuturesTrailPriceType.Unknown }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest { Side = 0 }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest { IsFinished = true }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest { PageSize = 101 }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetChaseOrderAsync("1.5"));
        foreach (var amount in new[] { "0", "", "NaN", "1e-30", "1.00000000000000000000000000001" })
            await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest { Contract = "btc_usd1", Amount = amount, PriceLimit = "0" }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest { Contract = "btc_usd1", Amount = "1", PriceLimit = "100", OffsetLimit = "1" }));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Candle_specific_limits_and_timezone_do_not_leak_to_premium_index()
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        var api = client.Futures.USD1;
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetCandlesticksAsync("BTC_USD1", GateFuturesCandlestickInterval.OneMinute, limit: 2001));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "BTC_USD1", Interval = GateFuturesCandlestickInterval.OneMinute, Timezone = "local" }));
        foreach (var interval in new[] { GateFuturesCandlestickInterval.TenSeconds, GateFuturesCandlestickInterval.OneMonth, GateFuturesCandlestickInterval.NaturalWeek })
            await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetPremiumIndexCandlesticksAsync("BTC_USD1", interval));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetPremiumIndexCandlesticksAsync(new GateFuturesCandlestickQueryRequest { Contract = "BTC_USD1", Interval = GateFuturesCandlestickInterval.OneMinute, Timezone = "utc0" }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => api.GetPremiumIndexCandlesticksAsync("BTC_USD1", GateFuturesCandlestickInterval.OneMinute, limit: 1001));
        Assert.Empty(handler.Requests);
        Assert.True((await api.GetCandlesticksAsync("BTC_USD1", GateFuturesCandlestickInterval.TenSeconds, limit: 2000)).Success);
        Assert.True((await api.GetPremiumIndexCandlesticksAsync("BTC_USD1", GateFuturesCandlestickInterval.OneWeek, limit: 1000)).Success);
    }

    private static GateFuturesOrderRequest Order() => new() { Contract = "BTC_USD1", Size = 1.25m, Price = 100.5m };

    [Theory]
    [InlineData(typeof(GateFuturesBalance), "user")]
    [InlineData(typeof(GateFuturesBalanceChange), "id")]
    [InlineData(typeof(GateFuturesBalanceChange), "trade_id")]
    [InlineData(typeof(GateFuturesPosition), "user")]
    [InlineData(typeof(GateFuturesPosition), "update_id")]
    [InlineData(typeof(GateFuturesPosition), "pid")]
    [InlineData(typeof(GateFuturesPosition), "voucher_id")]
    [InlineData(typeof(GateFuturesPositionCloseOrder), "id")]
    [InlineData(typeof(GateFuturesAdlRecord), "user")]
    [InlineData(typeof(GateFuturesAdlRecord), "order_id")]
    [InlineData(typeof(GateFuturesUserLiquidation), "order_id")]
    [InlineData(typeof(GateFuturesOrderBook), "id")]
    [InlineData(typeof(GateFuturesOrderCancel), "id")]
    [InlineData(typeof(GateFuturesOrderCancel), "user_id")]
    [InlineData(typeof(GateFuturesUserTrade), "id")]
    [InlineData(typeof(GateFuturesUserTrade), "trade_id")]
    [InlineData(typeof(GateFuturesUserTrade), "order_id")]
    [InlineData(typeof(GateFuturesTrailOrder), "id")]
    [InlineData(typeof(GateFuturesTrailOrder), "user_id")]
    [InlineData(typeof(GateFuturesTrailOrder), "user")]
    [InlineData(typeof(GateFuturesTrailOrder), "suborder_id")]
    [InlineData(typeof(GateFuturesTrade), "id")]
    [InlineData(typeof(GateFuturesContract), "orderbook_id")]
    [InlineData(typeof(GateFuturesContract), "trade_id")]
    [InlineData(typeof(GateFuturesPriceTriggeredOrder), "id")]
    [InlineData(typeof(GateFuturesPriceTriggeredOrder), "user")]
    [InlineData(typeof(GateFuturesPriceTriggeredOrder), "trade_id")]
    [InlineData(typeof(GateFuturesPriceTriggeredOrder), "me_order_id")]
    [InlineData(typeof(GateFuturesPriceTriggeredOrderId), "id")]
    [InlineData(typeof(GateFuturesPriceTriggeredOrderUpdateRequest), "order_id")]
    [InlineData(typeof(GateFuturesPriceTriggeredOrderUpdateRequest), "size")]
    [InlineData(typeof(GateFuturesInitial), "size")]
    [InlineData(typeof(GateFuturesTrailOrderCancelRequest), "OrderId")]
    [InlineData(typeof(GateFuturesTrailOrderUpdateRequest), "OrderId")]
    [InlineData(typeof(GateFuturesTrailOrderChangeLogQueryRequest), "OrderId")]
    [InlineData(typeof(GateFuturesUserTradeQueryRequest), "OrderId")]
    [InlineData(typeof(GateFuturesUserTradeQueryRequest), "LastId")]
    [InlineData(typeof(GateFuturesTradeQueryRequest), "LastId")]
    [InlineData(typeof(GateFuturesOrderQueryRequest), "LastId")]
    [InlineData(typeof(GateFuturesBboOrderRequest), "size")]
    [InlineData(typeof(GateFuturesBboOrderRequest), "iceberg")]
    [InlineData(typeof(GateFuturesBboOrderRequest), "level")]
    [InlineData(typeof(GateFuturesBboOrderRequest), "pid")]
    public void Long_model_fields_keep_exact_integers_and_reject_lossy_tokens(Type model, string field)
    {
        var json = LongModelJson(model);
        json[field] = long.MaxValue.ToString(CultureInfo.InvariantCulture);
        var data = JsonConvert.DeserializeObject(json.ToString(), model)!;
        Assert.Equal(long.MaxValue, JObject.FromObject(data)[field]!.Value<long>());
        foreach (var id in new[] { "1.5", "true", "\"1.5\"", "\"9223372036854775808\"", "\"not-an-id\"" })
        {
            json[field] = JToken.Parse(id);
            Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject(json.ToString(), model));
        }
    }

    private static JObject LongModelJson(Type model)
    {
        if (model == typeof(GateFuturesPriceTriggeredOrder)) return JObject.Parse("{\"initial\":{\"contract\":\"BTC_USDT\",\"price\":\"1\"},\"trigger\":{\"price\":\"1\",\"rule\":1}}");
        if (model == typeof(GateFuturesInitial)) return JObject.Parse("{\"contract\":\"BTC_USDT\",\"price\":\"1\"}");
        if (model == typeof(GateFuturesPriceTriggeredOrderUpdateRequest)) return JObject.Parse("{\"order_id\":117}");
        if (model == typeof(GateFuturesBboOrderRequest)) return JObject.Parse("{\"contract\":\"BTC_USDT\",\"direction\":\"buy\",\"size\":1,\"level\":1}");
        return new JObject();
    }

    [Theory]
    [InlineData(400)] [InlineData(429)] [InlineData(500)]
    public async Task Remaining_mutation_families_preserve_errors_without_retry_or_fabricating_success(int status)
    {
        var handler = Handler("{\"label\":\"TEST_ERROR\",\"message\":\"denied\"}", (HttpStatusCode)status);
        using var client = Client(handler);
        var api = client.Futures.USD1;
        var calls = new Func<Task>[]
        {
            () => Base(api.PlaceOrdersAsync(new[] { Order() }), status),
            () => Base(api.AmendOrdersAsync(new[] { new GateFuturesOrderAmendRequest { OrderId = 117 } }), status),
            () => Base(api.CancelOrdersAsync(new[] { 117L }), status),
            () => Base(api.PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USD1", Size = 1, Direction = GateFuturesBboDirection.Buy, Level = 1 }), status),
            () => Base(api.SetPositionLeverageAsync("BTC_USD1", 10, GateFuturesPositionMarginMode.Cross), status),
            () => Base(api.SetPositionModeAsync(GateFuturesAccountPositionMode.DualPlus), status),
            () => Base(api.CancelOrdersAsync(new GateFuturesOrderCancelAllRequest()), status),
            () => Base(api.CancelAllAsync(5), status),
            () => Base(api.PlaceTrailOrderAsync(new GateFuturesTrailOrderRequest { Contract = "BTC_USD1", Amount = 1 }), status),
            () => Base(api.CancelTrailOrderAsync(117), status),
            () => Base(api.UpdateTrailOrderAsync(new GateFuturesTrailOrderUpdateRequest { OrderId = 117 }), status),
            () => Base(api.PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest { Contract = "BTC_USD1", Amount = "1", PriceLimit = "0" }), status),
            () => Base(api.CancelChaseOrderAsync("117"), status),
        };
        foreach (var call in calls)
        {
            await call();
        }
        Assert.Equal(calls.Length, handler.Requests.Count);
    }

    [Theory]
    [InlineData("")] [InlineData(" ")] [InlineData("BTC|_USD1")]
    [InlineData("BTC_USD1\n")] [InlineData("BTC_USD1/../BTC_USDT")]
    [InlineData("BTC_USD1%2Forders")]
    public async Task Literal_contract_guards_cover_remaining_state_sensitive_paths(string contract)
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        var api = client.Futures.USD1;
        var calls = new Func<Task>[]
        {
            () => api.SetPositionMarginAsync(contract, -1),
            () => api.SetLeverageAsync(contract, 10),
            () => api.SetPositionLeverageAsync(contract, 10, GateFuturesPositionMarginMode.Cross),
            () => api.SetMarginModeAsync(contract, GateFuturesMarginMode.Cross),
            () => api.SetRiskLimitAsync(contract, 10),
            () => api.SetDualModeMarginAsync(contract, GateFuturesDualModeSide.DualLong, -1),
            () => api.SetDualModeLeverageAsync(contract, 10),
            () => api.SetDualModeRiskLimitAsync(contract, 10),
        };
        foreach (var call in calls) await Assert.ThrowsAnyAsync<ArgumentException>(call);
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Trail_filters_keep_boolean_and_integer_wire_types_and_chase_body_settlement_is_overridden_by_path()
    {
        var handler = Handler("{\"orders\":[]}");
        using var client = Client(handler);
        Assert.True((await client.Futures.USD1.GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest { Contract = "BTC_USD1", IsFinished = false, HideCancel = false, SortByTrigger = true, RelatedPosition = 2, ReduceOnly = 1, Side = 2, SortBy = 1, PageNumber = 1, PageSize = 100 })).Success);
        var query = Query(Assert.Single(handler.Requests));
        Assert.Equal("false", query["is_finished"]);
        Assert.Equal("false", query["hide_cancel"]);
        Assert.Equal("true", query["sort_by_trigger"]);
        Assert.Equal("2", query["related_position"]);
        Assert.Equal("1", query["reduce_only"]);
        Assert.Equal("2", query["side"]);
        var createHandler = Handler("{\"id\":\"117\"}");
        using var createClient = Client(createHandler);
        Assert.True((await createClient.Futures.USD1.PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest { Contract = "btc_usd1", Amount = "-1.2500", PriceLimit = "0", Settlement = GateFuturesSettlement.BTC, PositionMode = "dual_plus" })).Success);
        var request = Assert.Single(createHandler.Requests);
        Assert.EndsWith("/futures/usd1/autoorder/v1/chase/create", request.RequestUri.AbsolutePath);
        Assert.Equal("btc", JObject.Parse(request.Content)["settle"]!.Value<string>());
        Assert.Equal("-1.2500", JObject.Parse(request.Content)["amount"]!.Value<string>());
    }

    private static async Task Base<T>(Task<ApiSharp.Models.RestCallResult<T>> task, int status)
    {
        var result = await task;
        Assert.False(result.Success);
        Assert.Equal("TEST_ERROR", result.Error!.Data);
        Assert.Equal("denied", result.Error.Message);
        Assert.Equal((HttpStatusCode)status, result.Response!.StatusCode);
    }
    private static DateTime Epoch(long seconds) => DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
    private static Dictionary<string, string> Query(RecordedHttpRequest request)
        => request.RequestUri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split('=', 2)).ToDictionary(p => Uri.UnescapeDataString(p[0]), p => Uri.UnescapeDataString(p[1]));
    private sealed class Once<T>(IEnumerable<T> values) : IEnumerable<T>
    {
        private bool read;
        public IEnumerator<T> GetEnumerator()
        {
            if (read) throw new InvalidOperationException("Enumerated twice");
            read = true;
            return values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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
