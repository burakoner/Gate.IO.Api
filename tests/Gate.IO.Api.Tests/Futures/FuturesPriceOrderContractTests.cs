using Gate.IO.Api.Futures;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using Newtonsoft.Json;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Contract")]
public class FuturesPriceOrderContractTests
{
    [Fact]
    public void Complete_price_order_contract_keeps_quantities_ids_timestamps_and_response_flags()
    {
        var json = JObject.Parse(JsonFixture.Read("Docs/Futures/price_order.success.json"));
        json["id"] = 9007199254740993L;
        json["id_string"] = "9007199254740993";
        json["user"] = int.MaxValue;
        json["trade_id"] = long.MaxValue;
        json["me_order_id"] = 9007199254740994L;
        json["create_time"] = 1514764800.125d;
        json["finish_time"] = 1514764900.5d;
        var order = json.ToObject<GateFuturesPriceTriggeredOrder>()!;

        Assert.Equal(9007199254740993L, order.OrderId);
        Assert.Equal("9007199254740993", order.OrderIdString);
        Assert.Equal(int.MaxValue, order.UserId);
        Assert.Equal(long.MaxValue, order.TradeId);
        Assert.Equal(9007199254740994L, order.MeOrderId);
        Assert.Equal(new DateTime(2018, 1, 1, 0, 0, 0, 125, DateTimeKind.Utc), order.CreateTime);
        Assert.Equal(new DateTime(2018, 1, 1, 0, 1, 40, 500, DateTimeKind.Utc), order.FinishTime);
        Assert.Equal("BTC_USDT", order.Order.Contract);
        Assert.Equal(100, order.Order.Size);
        Assert.Equal("100.5", order.Order.Amount);
        Assert.Equal("5.03", order.Order.Price);
        Assert.False(order.Order.IsClose!.Value);
        Assert.False(order.Order.IsReduceOnly!.Value);
        Assert.Equal(GateFuturesPositionMarginMode.Cross, order.PositionMarginMode);
        Assert.Equal(GateFuturesTriggerPrice.DealPrice, order.Trigger.PriceType);
        Assert.Equal(GateSpotTriggerCondition.GreaterThanOrEqualTo, order.Trigger.Rule);
        Assert.Equal("3000", order.Trigger.Price);
        Assert.Equal(86400, order.Trigger.Expiration);
        Assert.Equal("", order.Reason);
        var roundTrip = JObject.FromObject(order);
        Assert.Equal(9007199254740993L, roundTrip["id"]!.Value<long>());
        Assert.Equal("100.5", roundTrip["initial"]!["amount"]!.Value<string>());
        Assert.Equal(JTokenType.Integer, roundTrip["trigger"]!["price_type"]!.Type);
    }

    [Theory]
    [InlineData("open", GateFuturesPriceTriggerStatus.Open)]
    [InlineData("finished", GateFuturesPriceTriggerStatus.Finished)]
    [InlineData("inactive", GateFuturesPriceTriggerStatus.Inactive)]
    [InlineData("invalid", GateFuturesPriceTriggerStatus.Invalid)]
    public void All_documented_response_statuses_are_readable(string wire, GateFuturesPriceTriggerStatus expected)
    {
        var json = JObject.Parse(JsonFixture.Read("Docs/Futures/price_order.success.json"));
        json["status"] = wire;
        Assert.Equal(expected, json.ToObject<GateFuturesPriceTriggeredOrder>()!.Status);
    }

    [Theory]
    [InlineData("cancelled", GateFuturesOrderFinishAs.Cancelled)]
    [InlineData("succeeded", GateFuturesOrderFinishAs.Succeeded)]
    [InlineData("failed", GateFuturesOrderFinishAs.Failed)]
    [InlineData("expired", GateFuturesOrderFinishAs.Expired)]
    public void All_documented_finish_states_are_readable(string wire, GateFuturesOrderFinishAs expected)
    {
        var json = JObject.Parse(JsonFixture.Read("Docs/Futures/price_order.success.json"));
        json["finish_as"] = wire;
        Assert.Equal(expected, json.ToObject<GateFuturesPriceTriggeredOrder>()!.FinishAs);
    }

    [Theory]
    [InlineData("close-long-order", GateFuturesTriggerType.CloseLongOrder)]
    [InlineData("close-short-order", GateFuturesTriggerType.CloseShortOrder)]
    [InlineData("close-long-position", GateFuturesTriggerType.CloseLongPosition)]
    [InlineData("close-short-position", GateFuturesTriggerType.CloseShortPosition)]
    [InlineData("plan-close-long-position", GateFuturesTriggerType.PlanCloseLongPosition)]
    [InlineData("plan-close-short-position", GateFuturesTriggerType.PlanCloseShortPosition)]
    public void Read_contract_keeps_all_six_order_types_including_the_two_readonly_types(string wire, GateFuturesTriggerType expected)
    {
        var json = JObject.Parse(JsonFixture.Read("Docs/Futures/price_order.success.json"));
        json["order_type"] = wire;
        Assert.Equal(expected, json.ToObject<GateFuturesPriceTriggeredOrder>()!.Type);
    }

    [Theory]
    [InlineData(0, GateFuturesTriggerPrice.DealPrice)]
    [InlineData(1, GateFuturesTriggerPrice.MarkPrice)]
    [InlineData(2, GateFuturesTriggerPrice.IndexPrice)]
    public void All_three_numeric_price_references_and_price_spread_response_strategy_remain_readable(int wire, GateFuturesTriggerPrice expected)
    {
        var json = JObject.Parse(JsonFixture.Read("Docs/Futures/price_order.success.json"));
        json["trigger"]!["price_type"] = wire;
        json["trigger"]!["strategy_type"] = 1;
        Assert.Equal(expected, json.ToObject<GateFuturesPriceTriggeredOrder>()!.Trigger.PriceType);
        Assert.Equal(GateFuturesTriggerStrategy.ByPriceGap, json.ToObject<GateFuturesPriceTriggeredOrder>()!.Trigger.StrategyType);
    }

    [Fact]
    public void Optional_response_fields_are_not_inferred_from_request_or_other_fields()
    {
        var order = JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrder>("{\"initial\":{\"contract\":\"BTC_USD1\",\"price\":\"1\"},\"trigger\":{\"price\":\"2\",\"rule\":1}}")!;
        Assert.Null(order.Order.Size);
        Assert.Null(order.Order.Amount);
        Assert.Null(order.Order.TimeInForce);
        Assert.Null(order.Order.Close);
        Assert.Null(order.Order.IsClose);
        Assert.Null(order.Order.IsReduceOnly);
        Assert.Null(order.FinishAs);
        Assert.Null(order.FinishTime);
        Assert.Null(order.TradeId);
        Assert.Null(order.OrderIdString);
        Assert.Null(order.Type);
        Assert.Null(order.PositionMarginMode);
        Assert.Null(order.Trigger.PriceType);
        Assert.Null(order.Trigger.StrategyType);
        Assert.Null(order.Trigger.Expiration);
        Assert.NotEqual(GateFuturesPriceTriggerStatus.Finished, order.Status);
        Assert.NotEqual(GateFuturesPriceTriggerStatus.Open, order.Status);
    }

    [Theory]
    [InlineData("initial")]
    [InlineData("initial.contract")]
    [InlineData("initial.price")]
    [InlineData("trigger")]
    [InlineData("trigger.price")]
    [InlineData("trigger.rule")]
    public void Missing_or_null_required_shared_fields_are_rejected_by_both_current_schemas(string path)
    {
        foreach (var fixture in new[] { "Docs/Futures/price_order.success.json", "Docs/Delivery/price_order.success.json" })
        {
            var json = JObject.Parse(JsonFixture.Read(fixture));
            var token = json.SelectToken(path)!;
            token.Replace(JValue.CreateNull());
            Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesPriceTriggeredOrder>());
            ((JProperty)json.SelectToken(path)!.Parent!).Remove();
            Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesPriceTriggeredOrder>());
        }
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"order_id\":null}")]
    public void Amendment_id_is_required_when_reading_a_saved_request(string json)
        => Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrderUpdateRequest>(json));

    [Theory]
    [InlineData("order_type")]
    [InlineData("pos_margin_mode")]
    [InlineData("initial.tif")]
    [InlineData("initial.auto_size")]
    public void Explicit_unknown_saved_request_enums_cannot_disappear_into_server_defaults(string path)
    {
        var json = JObject.Parse("{\"initial\":{\"contract\":\"BTC_USD1\",\"price\":\"1\"},\"trigger\":{\"price\":\"2\",\"rule\":1}}");
        var parts = path.Split('.');
        var owner = parts.Length == 1 ? json : (JObject)json[parts[0]]!;
        owner[parts[^1]] = "unsupported-explicit-value";
        Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesPriceTriggeredOrderRequest>());
    }

    [Fact]
    public void Explicit_unknown_saved_amendment_side_cannot_be_omitted()
        => Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrderUpdateRequest>("{\"order_id\":117,\"auto_size\":\"unsupported-explicit-value\"}"));

    [Theory]
    [InlineData("price_type", "1.5")]
    [InlineData("strategy_type", "0.1")]
    [InlineData("price_type", "true")]
    [InlineData("price_type", "2147483648")]
    public void Saved_numeric_trigger_enums_cannot_round_or_coerce_an_explicit_value(string field, string token)
        => Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrderRequest>(
            "{\"initial\":{\"contract\":\"BTC_USD1\",\"price\":\"1\"},\"trigger\":{\"price\":\"2\",\"rule\":1,\"" + field + "\":" + token + "}}"));

    [Theory]
    [InlineData("order_type")]
    [InlineData("pos_margin_mode")]
    [InlineData("initial.tif")]
    [InlineData("initial.auto_size")]
    public void Shared_known_enum_mappings_and_null_omission_remain_compatible(string path)
    {
        foreach (var fixture in new[] { "Docs/Futures/price_order.success.json", "Docs/Delivery/price_order.success.json" })
        {
            var json = JObject.Parse(JsonFixture.Read(fixture));
            var parts = path.Split('.');
            var owner = parts.Length == 1 ? json : (JObject)json[parts[0]]!;
            owner[parts[^1]] = JValue.CreateNull();
            var result = json.ToObject<GateFuturesPriceTriggeredOrder>()!;
            Assert.Null(JObject.FromObject(result).SelectToken(path));
            owner[parts[^1]] = "unsupported-explicit-value";
            Assert.ThrowsAny<JsonException>(() => json.ToObject<GateFuturesPriceTriggeredOrder>());
        }
    }

    [Theory]
    [InlineData("0", GateFuturesTriggerPrice.DealPrice)]
    [InlineData("1", GateFuturesTriggerPrice.MarkPrice)]
    [InlineData("2", GateFuturesTriggerPrice.IndexPrice)]
    public void Exact_numeric_and_legacy_numeric_string_trigger_enums_remain_compatible(string value, GateFuturesTriggerPrice expected)
    {
        foreach (var token in new[] { value, "\"" + value + "\"" })
        {
            var result = JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrderUpdateRequest>("{\"order_id\":117,\"price_type\":" + token + "}")!;
            Assert.Equal(expected, result.PriceType);
            Assert.Equal(int.Parse(value), JObject.FromObject(result)["price_type"]!.Value<int>());
        }
    }
}
