using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Contract")]
public class FuturesAdlRiskStateContractTests
{
    [Fact]
    public void Documented_market_adl_risk_response_preserves_all_states_and_millisecond_timestamps()
    {
        var response = JsonFixture.Deserialize<GateFuturesAdlRiskStates>("Docs/Futures/adl_risk_states.success.json");

        Assert.Equal("usdt", response.Settlement);
        Assert.Equal(3, response.States.Count);
        Assert.Equal("normal", response.States["BTC_USDT"].State);
        Assert.Equal("warning", response.States["GT_USDT"].State);
        Assert.Equal("adl_risk", response.States["HYPE_USDT"].State);
        Assert.All(response.States.Values, state => Assert.Equal(1786695185345L, state.CalculatedAtInMilliseconds));
        var serialized = JObject.FromObject(response);
        Assert.Equal(2, serialized.Count);
        Assert.Equal("usdt", serialized["settle"]!.Value<string>());
        foreach (var item in response.States)
        {
            var state = (JObject)serialized["states"]![item.Key]!;
            Assert.Equal(2, state.Count);
            Assert.Equal(JTokenType.String, state["state"]!.Type);
            Assert.Equal(JTokenType.Integer, state["calculated_at_ms"]!.Type);
            Assert.Equal(item.Value.State, state["state"]!.Value<string>());
            Assert.Equal(item.Value.CalculatedAtInMilliseconds, state["calculated_at_ms"]!.Value<long>());
        }
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(9007199254740993L)]
    [InlineData(long.MaxValue)]
    [InlineData(long.MinValue)]
    public void Adl_calculation_time_is_preserved_as_int64_without_date_or_double_conversion(long timestamp)
    {
        var body = new JObject { ["state"] = "warning", ["calculated_at_ms"] = timestamp };

        var state = JsonConvert.DeserializeObject<GateFuturesAdlRiskState>(body.ToString())!;

        Assert.Equal(timestamp, state.CalculatedAtInMilliseconds);
        Assert.Equal(timestamp, JObject.FromObject(state)["calculated_at_ms"]!.Value<long>());
    }

    [Theory]
    [InlineData("future-risk-state")]
    [InlineData("NORMAL")]
    [InlineData("")]
    public void Unrecognized_adl_states_are_preserved_and_not_classified_as_normal(string value)
    {
        var body = new JObject { ["state"] = value, ["calculated_at_ms"] = 1786695185345L };

        var state = JsonConvert.DeserializeObject<GateFuturesAdlRiskState>(body.ToString())!;

        Assert.Equal(value, state.State);
        Assert.NotEqual("normal", state.State);
    }

    [Fact]
    public void Empty_adl_mapping_is_not_populated_with_normal_markets()
    {
        var response = JsonConvert.DeserializeObject<GateFuturesAdlRiskStates>("{\"settle\":\"usd1\",\"states\":{}}")!;

        Assert.Equal("usd1", response.Settlement);
        Assert.Empty(response.States);
    }

    [Fact]
    public void Adl_mapping_preserves_dynamic_contract_keys_and_null_entries_without_inventing_states()
    {
        const string json = "{\"settle\":\"usd1\",\"states\":{\"1000SHIB_USD1\":{\"state\":\"warning\",\"calculated_at_ms\":1786695185345},\"future-market\":null}}";

        var response = JsonConvert.DeserializeObject<GateFuturesAdlRiskStates>(json)!;

        Assert.Equal(2, response.States.Count);
        Assert.Equal("warning", response.States["1000SHIB_USD1"].State);
        Assert.Null(response.States["future-market"]);
        Assert.False(response.States.ContainsKey("BTC_USD1"));
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"settle\":\"usdt\"}")]
    [InlineData("{\"states\":{}}")]
    [InlineData("{\"settle\":null,\"states\":{}}")]
    [InlineData("{\"settle\":\"usdt\",\"states\":null}")]
    [InlineData("{\"settle\":\"usdt\",\"states\":{\"BTC_USDT\":{}}}")]
    [InlineData("{\"settle\":\"usdt\",\"states\":{\"BTC_USDT\":{\"state\":\"normal\"}}}")]
    [InlineData("{\"settle\":\"usdt\",\"states\":{\"BTC_USDT\":{\"calculated_at_ms\":1786695185345}}}")]
    [InlineData("{\"settle\":\"usdt\",\"states\":{\"BTC_USDT\":{\"state\":null,\"calculated_at_ms\":1786695185345}}}")]
    [InlineData("{\"settle\":\"usdt\",\"states\":{\"BTC_USDT\":{\"state\":\"normal\",\"calculated_at_ms\":null}}}")]
    public void Missing_required_adl_fields_fail_deserialization_instead_of_creating_normal_or_zero_defaults(string json)
        => Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateFuturesAdlRiskStates>(json));
}
