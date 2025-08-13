namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate Futures ADL Record
/// </summary>
public record GateFuturesAdlRecord
{
    /// <summary>
    /// Automatic deleveraging time
    /// </summary>
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public long User { get; set; }

    /// <summary>
    /// Order ID. Order IDs before 2023-02-20 are null
    /// </summary>
    [JsonProperty("order_id")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Position leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Cross margin leverage (valid only when leverage is 0)
    /// </summary>
    [JsonProperty("cross_leverage_limit")]
    public decimal CrossLeverageLimit { get; set; }

    /// <summary>
    /// Average entry price
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    /// <summary>
    /// Average fill price
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// Trading size
    /// </summary>
    [JsonProperty("trade_size")]
    public decimal? TradeSize { get; set; }

    /// <summary>
    /// Positions after auto-deleveraging
    /// </summary>
    [JsonProperty("position_size")]
    public decimal? PositionSize { get; set; }
}
