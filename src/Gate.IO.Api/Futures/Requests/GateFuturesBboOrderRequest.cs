namespace Gate.IO.Api.Futures;

/// <summary>BBO order instructions. Unlike standard orders, this endpoint documents integer contract quantities.</summary>
public record GateFuturesBboOrderRequest
{
    /// <summary>Futures contract.</summary>
    [JsonProperty("contract", Required = Required.Always)]
    public string Contract { get; set; }

    /// <summary>Signed integer quantity; zero for a full close.</summary>
    [JsonProperty("size", Required = Required.Always), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long Size { get; set; }

    /// <summary>buy fetches asks; sell fetches bids. Does not rewrite the signed quantity.</summary>
    [JsonProperty("direction", Required = Required.Always), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesBboDirection Direction { get; set; }

    /// <summary>Book depth, from 1 to 20.</summary>
    [JsonProperty("level", Required = Required.Always), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long Level { get; set; }

    /// <summary>Displayed integer quantity.</summary>
    [JsonProperty("iceberg", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? Iceberg { get; set; }

    /// <summary>Full close; requires size zero.</summary>
    [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Close { get; set; }

    /// <summary>Only reduce a position.</summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>Time in force.</summary>
    [JsonProperty("tif", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesTimeInForce? TimeInForce { get; set; }

    /// <summary>Optional custom ID, t- followed by at most 28 permitted ASCII characters.</summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    /// <summary>Hedge-mode full close direction; requires size zero. No reduce_only default is inferred.</summary>
    [JsonProperty("auto_size", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesOrderAutoSize? AutoSize { get; set; }

    /// <summary>Self-trade prevention action.</summary>
    [JsonProperty("stp_act", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesPriceOrderMapConverter))]
    public GateFuturesSelfTradeAction? SelfTradeAction { get; set; }

    /// <summary>Position ID, retained as Int64.</summary>
    [JsonProperty("pid", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? PositionId { get; set; }
}
