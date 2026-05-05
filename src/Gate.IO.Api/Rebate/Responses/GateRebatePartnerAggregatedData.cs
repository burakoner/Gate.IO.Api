namespace Gate.IO.Api.Rebate;

/// <summary>
/// Aggregated partner agent statistics
/// </summary>
public record GateRebatePartnerAggregatedData
{
    /// <summary>
    /// Rebate amount
    /// </summary>
    [JsonProperty("rebate_amount")]
    public decimal RebateAmount { get; set; }

    /// <summary>
    /// Trading volume
    /// </summary>
    [JsonProperty("trade_volume")]
    public decimal TradeVolume { get; set; }

    /// <summary>
    /// Net fee
    /// </summary>
    [JsonProperty("net_fee")]
    public decimal NetFee { get; set; }

    /// <summary>
    /// Customer count
    /// </summary>
    [JsonProperty("customer_count")]
    public long CustomerCount { get; set; }

    /// <summary>
    /// Transaction participant count
    /// </summary>
    [JsonProperty("trading_user_count")]
    public long? TradingUserCount { get; set; }

    /// <summary>
    /// Time range description
    /// </summary>
    [JsonProperty("time_range_desc")]
    public string TimeRangeDescription { get; set; }

    /// <summary>
    /// Business type
    /// </summary>
    [JsonProperty("business_type")]
    public GateRebateBusinessType BusinessType { get; set; }

    /// <summary>
    /// Business type description
    /// </summary>
    [JsonProperty("business_type_desc")]
    public string BusinessTypeDescription { get; set; }
}
