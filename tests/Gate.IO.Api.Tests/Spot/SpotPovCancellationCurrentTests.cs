using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Text;

namespace Gate.IO.Api.Tests.Spot;

[Trait("Category", "Contract")]
public class SpotPovCancellationCurrentTests
{
    [Theory]
    [InlineData(false, "")]
    [InlineData(false, "null")]
    [InlineData(true, "")]
    [InlineData(true, "null")]
    [InlineData(true, "[null]")]
    [InlineData(true, "[{},null]")]
    [InlineData(true, "{}")]
    [InlineData(false, "[]")]
    public async Task Absent_cancellation_results_are_not_successful_acknowledgements(bool bulk, string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        if (bulk)
        {
            var result = await client.Spot.CancelPovOrdersAsync("BTC_USDT");
            Assert.False(result.Success);
            Assert.NotNull(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        }
        else
        {
            var result = await client.Spot.CancelPovOrderAsync("1216");
            Assert.False(result.Success);
            Assert.NotNull(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        }
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("amount")]
    [InlineData("limit_price")]
    [InlineData("trigger_price")]
    public void Cancellation_amounts_and_prices_never_round_or_substitute_zero(string field)
    {
        foreach (var value in new[] { "\"1.12345678901234567890123456789\"", "\"0.00000000000000000000000000001\"", "\"Infinity\"", "\" \"", "1.1", "true" })
        {
            var json = Order();
            json[field] = JToken.Parse(value);
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateSpotPovOrder>(json.ToString(Formatting.None)));
        }
    }

    [Theory]
    [InlineData("start_time_ms")]
    [InlineData("end_time_ms")]
    [InlineData("expire_time_ms")]
    [InlineData("create_time_ms")]
    [InlineData("update_time_ms")]
    public void Every_millisecond_field_rejects_fractional_boolean_and_overflow_values(string field)
    {
        foreach (var value in new[] { "1784279005258.1", "true", "\"9223372036854775808\"" })
        {
            var json = Order();
            json[field] = JToken.Parse(value);
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateSpotPovOrder>(json.ToString(Formatting.None)));
        }
    }

    [Theory]
    [InlineData("amount")]
    [InlineData("limit_price")]
    [InlineData("trigger_price")]
    public void Exact_financial_values_round_trip_as_documented_strings(string field)
    {
        var json = Order();
        json[field] = "0.1234567890123456789012345678";
        var copy = JObject.FromObject(json.ToObject<GateSpotPovOrder>()!);
        Assert.Equal(JTokenType.String, copy[field]!.Type);
        Assert.Equal((string?)json[field], (string?)copy[field]);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Valid_cancellations_keep_the_current_model_and_original_http_metadata(bool bulk)
    {
        var handler = Handler(bulk ? $"[{Order()}]" : Order().ToString());
        using var client = Client(handler);
        GateSpotPovOrder order;
        if (bulk)
        {
            var result = await client.Spot.CancelPovOrdersAsync("BTC_USDT");
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            order = Assert.Single(result.Data);
        }
        else
        {
            var result = await client.Spot.CancelPovOrderAsync("1216");
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            order = result.Data;
        }
        Assert.Equal("1216", order.OrderId);
        Assert.Equal("BTC_USDT", order.Symbol);
        Assert.Equal(GateSpotOrderSide.Buy, order.Side);
        Assert.Equal(0.01m, order.Amount);
        Assert.Equal(GateSpotPovParticipationRate.TenPercent, order.ParticipationRate);
        Assert.Equal(GateSpotPovTimeToLive.OneHour, order.TimeToLive);
        Assert.Equal(GateSpotPovOrderStatus.Created, order.Status); // ACK is not completed cancellation.
        Assert.Equal("", order.TerminatedAs);
        Assert.Equal(0, order.StartTimeInMilliseconds);
        Assert.Equal(0, order.EndTimeInMilliseconds);
        Assert.Equal(1784365405074, order.ExpireTimeInMilliseconds);
        Assert.Equal(1784279005258, order.CreateTimeInMilliseconds);
        Assert.Equal(1784279005258, order.UpdateTimeInMilliseconds);
        Assert.Null(order.ClientOrderId);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public void Optional_prices_and_timestamps_preserve_omission_null_zero_and_exact_Int64_values()
    {
        var json = Order();
        foreach (var field in new[] { "limit_price", "trigger_price", "start_time_ms", "end_time_ms", "expire_time_ms", "update_time_ms" }) json.Remove(field);
        var omitted = json.ToObject<GateSpotPovOrder>()!;
        Assert.Null(omitted.LimitPrice);
        Assert.Null(omitted.TriggerPrice);
        Assert.Null(omitted.StartTimeInMilliseconds);
        Assert.Null(omitted.EndTimeInMilliseconds);
        Assert.Null(omitted.ExpireTimeInMilliseconds);
        Assert.Null(omitted.UpdateTimeInMilliseconds);
        json["limit_price"] = null;
        json["trigger_price"] = "0";
        json["start_time_ms"] = null;
        json["end_time_ms"] = 0;
        json["expire_time_ms"] = "9223372036854775807";
        json["create_time_ms"] = 9007199254740993L;
        var explicitValues = json.ToObject<GateSpotPovOrder>()!;
        Assert.Null(explicitValues.LimitPrice);
        Assert.Equal(0m, explicitValues.TriggerPrice);
        Assert.Null(explicitValues.StartTimeInMilliseconds);
        Assert.Equal(0, explicitValues.EndTimeInMilliseconds);
        Assert.Equal(long.MaxValue, explicitValues.ExpireTimeInMilliseconds);
        Assert.Equal(9007199254740993L, explicitValues.CreateTimeInMilliseconds);
    }

    [Fact]
    public void All_sixteen_current_response_fields_remain_mapped_without_accessor_changes()
    {
        var contract = (Newtonsoft.Json.Serialization.JsonObjectContract)new Newtonsoft.Json.Serialization.DefaultContractResolver().ResolveContract(typeof(GateSpotPovOrder));
        const string fields = "id,currency_pair,side,amount,participation_rate,ttl,limit_price,trigger_price,status,terminated_as,start_time_ms,end_time_ms,expire_time_ms,create_time_ms,update_time_ms,text";
        Assert.Equal(fields.Split(',').OrderBy(x => x), contract.Properties.Select(x => x.PropertyName).OrderBy(x => x));
        var json = Order();
        json["text"] = "t-pov_1";
        Assert.Equal("t-pov_1", json.ToObject<GateSpotPovOrder>()!.ClientOrderId);
    }

    [Theory]
    [InlineData(false, "amount", "\"0.00000000000000000000000000001\"")]
    [InlineData(true, "limit_price", "\"1.12345678901234567890123456789\"")]
    [InlineData(false, "create_time_ms", "1784279005258.1")]
    [InlineData(true, "update_time_ms", "true")]
    public async Task Invalid_order_values_fail_with_original_http_metadata(bool bulk, string field, string value)
    {
        var json = Order();
        json[field] = JToken.Parse(value);
        var handler = Handler(bulk ? $"[{json}]" : json.ToString());
        using var client = Client(handler);
        if (bulk)
        {
            var result = await client.Spot.CancelPovOrdersAsync("BTC_USDT");
            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            Assert.NotNull(result.Error);
        }
        else
        {
            var result = await client.Spot.CancelPovOrderAsync("1216");
            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            Assert.NotNull(result.Error);
        }
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(false, HttpStatusCode.BadRequest)]
    [InlineData(true, HttpStatusCode.BadRequest)]
    [InlineData(false, HttpStatusCode.TooManyRequests)]
    [InlineData(true, HttpStatusCode.TooManyRequests)]
    [InlineData(false, HttpStatusCode.InternalServerError)]
    [InlineData(true, HttpStatusCode.InternalServerError)]
    public async Task Http_failures_preserve_status_and_label_without_retries(bool bulk, HttpStatusCode status)
    {
        var handler = Handler("{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Rejected\"}", status);
        using var client = Client(handler);
        if (bulk)
        {
            var result = await client.Spot.CancelPovOrdersAsync("BTC_USDT");
            Assert.False(result.Success);
            Assert.Equal(status, result.Response!.StatusCode);
            Assert.Equal("CONTRACT_TEST_ERROR", result.Error!.Data);
        }
        else
        {
            var result = await client.Spot.CancelPovOrderAsync("1216");
            Assert.False(result.Success);
            Assert.Equal(status, result.Response!.StatusCode);
            Assert.Equal("CONTRACT_TEST_ERROR", result.Error!.Data);
        }
        Assert.Single(handler.Requests);
    }

    private static JObject Order() => (JObject)JsonFixture.Parse("Docs/Spot/pov_order.success.json");

    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }
}
