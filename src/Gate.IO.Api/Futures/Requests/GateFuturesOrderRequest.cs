namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrderRequest
/// </summary>
public class GateFuturesOrderRequest
{
    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Order size. Specify positive number to make a bid, and negative number to ask
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Display size for iceberg order. 0 for non-iceberg. Note that you will have to pay the taker fee for the hidden size
    /// </summary>
    [JsonProperty("iceberg", NullValueHandling = NullValueHandling.Ignore)]
    public long? Iceberg { get; set; }

    /// <summary>
    /// Order price. 0 for market order with &#x60;tif&#x60; set as &#x60;ioc&#x60;
    /// </summary>
    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Price { get; set; }

    /// <summary>
    /// Set as &#x60;true&#x60; to close the position, with &#x60;size&#x60; set to 0
    /// </summary>
    [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Close { get; set; }

    /// <summary>
    /// Set as &#x60;true&#x60; to be reduce-only order
    /// </summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none
    /// </summary>
    [JsonProperty("tif", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; } = null;

    /// <summary>
    /// Set side to close dual-mode position. &#x60;close_long&#x60; closes the long side; while &#x60;close_short&#x60; the short one. Note &#x60;size&#x60; also needs to be set to 0
    /// </summary>
    [JsonProperty("auto_size", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesOrderAutoSize? AutoSize { get; set; }

    /// <summary>
    /// Self-Trading Prevention Action. Users can use this field to set self-trade prevetion strategies
    /// </summary>
    [JsonProperty("stp_act", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesSelfTradeAction? SelfTradeAction { get; set; }
}
