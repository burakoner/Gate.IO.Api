using ApiSharp.Converters;
using Gate.IO.Api.Base;
using Gate.IO.Api.Converters;
using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Options;
using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Tests;

[Trait("Category", "Unit")]
public class ConverterTests
{
    [Fact]
    public void Date_time_converter_reads_seconds_and_milliseconds()
    {
        var probe = JsonConvert.DeserializeObject<DateTimeProbe>(
            """{"seconds":1541993715,"milliseconds":1541993715123}""");

        Assert.NotNull(probe);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1541993715).UtcDateTime, probe!.Seconds);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1541993715123).UtcDateTime, probe.Milliseconds);
    }

    [Fact]
    public void Gate_decimal_converter_accepts_string_number_numeric_token_empty_string_and_infinity()
    {
        var probe = JsonConvert.DeserializeObject<DecimalProbe>(
            """{"string_value":"123.456","numeric_value":789.12,"empty_required":"","empty_optional":"","infinite_required":"∞","infinite_optional":"∞"}""");

        Assert.NotNull(probe);
        Assert.Equal(123.456m, probe!.StringValue);
        Assert.Equal(789.12m, probe.NumericValue);
        Assert.Equal(0m, probe.EmptyRequired);
        Assert.Null(probe.EmptyOptional);
        Assert.Equal(0m, probe.InfiniteRequired);
        Assert.Null(probe.InfiniteOptional);
    }

    [Fact]
    public void Gate_long_converter_accepts_integer_decimal_token_and_numeric_string()
    {
        var probe = JsonConvert.DeserializeObject<LongProbe>(
            """{"integer_value":1548000000123,"decimal_value":1548000000123.456,"string_value":"1548000000123","decimal_string_value":"1548000000123.456","empty_optional":""}""");

        Assert.NotNull(probe);
        Assert.Equal(1548000000123L, probe!.IntegerValue);
        Assert.Equal(1548000000123L, probe.DecimalValue);
        Assert.Equal(1548000000123L, probe.StringValue);
        Assert.Equal(1548000000123L, probe.DecimalStringValue);
        Assert.Null(probe.EmptyOptional);
    }

    [Fact]
    public void Map_converter_reads_and_writes_mapped_enum_values()
    {
        var order = JsonConvert.DeserializeObject<GateAlphaOrder>(
            """{"order_id":"1","side":"sell","currency":"MEME","currency_amount":"1","status":2,"create_time":1541993715}""");

        Assert.NotNull(order);
        Assert.Equal(GateAlphaOrderSide.Sell, order!.Side);

        var json = JsonConvert.SerializeObject(new GateAlphaOrder { Side = GateAlphaOrderSide.Buy });
        var token = JObject.Parse(json);

        Assert.Equal("buy", token["side"]!.ToString());
    }

    [Fact]
    public void Array_converter_reads_spot_candlestick_and_order_book_entries()
    {
        var candlestick = JsonConvert.DeserializeObject<GateSpotCandlestick>(
            """["1541993715","10.5","2.3","2.5","2.1","2.2","4.7",true]""");
        var entry = JsonConvert.DeserializeObject<GateSpotOrderBookEntry>("""["123.45","6.78"]""");

        Assert.NotNull(candlestick);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1541993715).UtcDateTime, candlestick!.Time);
        Assert.Equal(10.5m, candlestick.QuoteVolume);
        Assert.Equal(2.3m, candlestick.Close);
        Assert.Equal(4.7m, candlestick.Volume);
        Assert.True(candlestick.WindowClosed);

        Assert.NotNull(entry);
        Assert.Equal(123.45m, entry!.Price);
        Assert.Equal(6.78m, entry.Quantity);
    }

    [Fact]
    public void Stream_event_converters_read_and_write_request_and_response_events()
    {
        var request = new GateStreamRequest
        {
            Channel = "spot.trades",
            Event = StreamRequestEvent.Subscribe,
            Timestamp = 1541993715,
            Payload = new[] { "BTC_USDT" },
        };

        var requestJson = JObject.Parse(JsonConvert.SerializeObject(request));
        Assert.Equal("subscribe", requestJson["event"]!.ToString());

        var response = JsonConvert.DeserializeObject<GateStreamResponse<JObject>>(
            """{"time":1541993715,"channel":"spot.trades","event":"all","result":{"currency_pair":"BTC_USDT"}}""");

        Assert.NotNull(response);
        Assert.Equal(StreamResponseEvent.All, response!.Event);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1541993715).UtcDateTime, response.Timestamp);
    }

    [Fact]
    public void Stream_specific_converters_handle_empty_decimal_and_array_entries()
    {
        var ticker = JsonConvert.DeserializeObject<GateOptionsStreamBookTicker>(
            """{"t":1541993715000,"u":1,"s":"BTC_USDT-20250101-50000-C","b":"","B":"2.5","a":"123.45","A":"3.5"}""");
        var entry = JsonConvert.DeserializeObject<GateCrossExStreamOrderBookEntry>("""["987.65","4.321"]""");

        Assert.NotNull(ticker);
        Assert.Equal(0m, ticker!.BestBidPrice);
        Assert.Equal(123.45m, ticker.BestAskPrice);
        Assert.Equal(2.5m, ticker.BestBidAmount);
        Assert.Equal(3.5m, ticker.BestAskAmount);

        Assert.NotNull(entry);
        Assert.Equal(987.65m, entry!.Price);
        Assert.Equal(4.321m, entry.Quantity);
    }

    private sealed class DateTimeProbe
    {
        [JsonProperty("seconds")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Seconds { get; set; }

        [JsonProperty("milliseconds")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Milliseconds { get; set; }
    }

    private sealed class DecimalProbe
    {
        [JsonProperty("string_value")]
        [JsonConverter(typeof(GateDecimalConverter))]
        public decimal StringValue { get; set; }

        [JsonProperty("numeric_value")]
        [JsonConverter(typeof(GateDecimalConverter))]
        public decimal NumericValue { get; set; }

        [JsonProperty("empty_required")]
        [JsonConverter(typeof(GateDecimalConverter))]
        public decimal EmptyRequired { get; set; }

        [JsonProperty("empty_optional")]
        [JsonConverter(typeof(GateDecimalConverter))]
        public decimal? EmptyOptional { get; set; }

        [JsonProperty("infinite_required")]
        [JsonConverter(typeof(GateDecimalConverter))]
        public decimal InfiniteRequired { get; set; }

        [JsonProperty("infinite_optional")]
        [JsonConverter(typeof(GateDecimalConverter))]
        public decimal? InfiniteOptional { get; set; }
    }

    private sealed class LongProbe
    {
        [JsonProperty("integer_value")]
        [JsonConverter(typeof(GateLongConverter))]
        public long IntegerValue { get; set; }

        [JsonProperty("decimal_value")]
        [JsonConverter(typeof(GateLongConverter))]
        public long DecimalValue { get; set; }

        [JsonProperty("string_value")]
        [JsonConverter(typeof(GateLongConverter))]
        public long StringValue { get; set; }

        [JsonProperty("decimal_string_value")]
        [JsonConverter(typeof(GateLongConverter))]
        public long DecimalStringValue { get; set; }

        [JsonProperty("empty_optional")]
        [JsonConverter(typeof(GateLongConverter))]
        public long? EmptyOptional { get; set; }
    }
}
