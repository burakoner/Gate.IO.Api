namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPositionClose
/// </summary>
public class GateFuturesPositionClose
{
    /// <summary>
    /// Position close time
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Position side, long or short
    /// </summary>
    [JsonProperty("side")]
    public GateFuturesPositionSide Side { get; set; }

    /// <summary>
    /// PNL
    /// </summary>
    [JsonProperty("pnl")]
    public decimal Pnl { get; set; }

    /// <summary>
    /// PNL - Position P/L
    /// </summary>
    [JsonProperty("pnl_pnl")]
    public decimal PnlPnl { get; set; }

    /// <summary>
    /// PNL - Funding Fees
    /// </summary>
    [JsonProperty("pnl_fund")]
    public decimal PnlFund { get; set; }

    /// <summary>
    /// PNL - Transaction Fees
    /// </summary>
    [JsonProperty("pnl_fee")]
    public decimal PnlFee { get; set; }

    /// <summary>
    /// Text of close order
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Max Trade Size
    /// </summary>
    [JsonProperty("max_size")]
    public decimal MaximumTradeSize { get; set; }

    /// <summary>
    /// Cumulative closed position volume
    /// </summary>
    [JsonProperty("accum_size")]
    public decimal CumulativeClosedPositionVolume { get; set; }

    /// <summary>
    /// First Open Time
    /// </summary>
    [JsonProperty("first_open_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime FirstOpenTime { get; set; }

    /// <summary>
    /// When 'side' is 'long,' it indicates the opening average price; when 'side' is 'short,' it indicates the closing average price.
    /// </summary>
    [JsonProperty("long_price")]
    public decimal? LongPrice { get; set; }

    /// <summary>
    /// When 'side' is 'long,' it indicates the opening average price; when 'side' is 'short,' it indicates the closing average price
    /// </summary>
    [JsonProperty("short_price")]
    public decimal? ShortPrice { get; set; }
}