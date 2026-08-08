namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order detail or list item
/// </summary>
public record GateFuturesChaseOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public string OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public string UserId { get; set; }

    /// <summary>
    /// Contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settle")]
    public string Settlement { get; set; }

    /// <summary>
    /// Total size in contracts; positive for buy and negative for sell
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; set; }

    /// <summary>
    /// Maximum chase price
    /// </summary>
    [JsonProperty("price_limit")]
    public string PriceLimit { get; set; }

    /// <summary>
    /// Whether the order is reduce-only
    /// </summary>
    [JsonProperty("reduce_only")]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Custom order tag
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Creation time in Unix seconds
    /// </summary>
    [JsonProperty("create_time")]
    public long? CreateTime { get; set; }

    /// <summary>
    /// Finish time in Unix seconds
    /// </summary>
    [JsonProperty("finish_time")]
    public long? FinishTime { get; set; }

    /// <summary>
    /// Raw status value
    /// </summary>
    [JsonProperty("original_status")]
    public int? OriginalStatus { get; set; }

    /// <summary>
    /// Simplified status such as open or finished
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Completion reason
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Filled size
    /// </summary>
    [JsonProperty("fill_amount")]
    public string FillAmount { get; set; }

    /// <summary>
    /// Average fill price
    /// </summary>
    [JsonProperty("average_fill_price")]
    public string AverageFillPrice { get; set; }

    /// <summary>
    /// Current or most recent sub-order ID
    /// </summary>
    [JsonProperty("suborder_id")]
    public string SubOrderId { get; set; }

    /// <summary>
    /// Whether dual-position mode is enabled
    /// </summary>
    [JsonProperty("is_dual_mode")]
    public bool? IsDualMode { get; set; }

    /// <summary>
    /// Side label
    /// </summary>
    [JsonProperty("side_label")]
    public string SideLabel { get; set; }

    /// <summary>
    /// Position side display value
    /// </summary>
    [JsonProperty("position_side_output")]
    public string PositionSideOutput { get; set; }

    /// <summary>
    /// Current chase price
    /// </summary>
    [JsonProperty("chase_price")]
    public string ChasePrice { get; set; }

    /// <summary>
    /// Chase interval in seconds
    /// </summary>
    [JsonProperty("interval_sec")]
    public uint? IntervalSeconds { get; set; }

    /// <summary>
    /// Last update timestamp
    /// </summary>
    [JsonProperty("updated_at")]
    public long? UpdatedAt { get; set; }

    /// <summary>
    /// Current or most recent sub-order price
    /// </summary>
    [JsonProperty("suborder_price")]
    public string SubOrderPrice { get; set; }

    /// <summary>
    /// Whether the sub-order is active
    /// </summary>
    [JsonProperty("suborder_ongoing")]
    public bool? SubOrderOngoing { get; set; }

    /// <summary>
    /// How the sub-order finished
    /// </summary>
    [JsonProperty("suborder_finish_as")]
    public string SubOrderFinishAs { get; set; }

    /// <summary>
    /// Raw response price type
    /// </summary>
    [JsonProperty("price_type")]
    public int? PriceType { get; set; }

    /// <summary>
    /// Raw response price gap type
    /// </summary>
    [JsonProperty("price_gap_type")]
    public string PriceGapType { get; set; }

    /// <summary>
    /// Price gap value
    /// </summary>
    [JsonProperty("price_gap_value")]
    public string PriceGapValue { get; set; }

    /// <summary>
    /// Detailed status code
    /// </summary>
    [JsonProperty("status_code")]
    public string StatusCode { get; set; }

    /// <summary>
    /// Creation time with microsecond precision
    /// </summary>
    [JsonProperty("create_time_precise")]
    public string CreateTimePrecise { get; set; }

    /// <summary>
    /// Finish time with microsecond precision
    /// </summary>
    [JsonProperty("finish_time_precise")]
    public string FinishTimePrecise { get; set; }

    /// <summary>
    /// Position margin mode
    /// </summary>
    [JsonProperty("pos_margin_mode")]
    public string PositionMarginMode { get; set; }

    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("leverage")]
    public string Leverage { get; set; }

    /// <summary>
    /// Error label
    /// </summary>
    [JsonProperty("error_label")]
    public string ErrorLabel { get; set; }
}
