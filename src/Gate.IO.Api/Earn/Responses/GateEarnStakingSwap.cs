namespace Gate.IO.Api.Earn;

/// <summary>
/// Staking swap result
/// </summary>
public record GateEarnStakingSwap
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("pid")]
    public long? ProductId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Operation type
    /// </summary>
    [JsonProperty("type")]
    public int Type { get; set; }

    /// <summary>
    /// Subtype
    /// </summary>
    [JsonProperty("subtype")]
    public string Subtype { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Exchange ratio
    /// </summary>
    [JsonProperty("exchange_rate")]
    public decimal ExchangeRate { get; set; }

    /// <summary>
    /// Redemption amount
    /// </summary>
    [JsonProperty("exchange_amount")]
    public decimal? ExchangeAmount { get; set; }

    /// <summary>
    /// Update timestamp
    /// </summary>
    [JsonProperty("updateStamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Transaction timestamp
    /// </summary>
    [JsonProperty("createStamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// DeFi protocol type
    /// </summary>
    [JsonProperty("protocol_type")]
    public int? ProtocolType { get; set; }

    /// <summary>
    /// Reference ID
    /// </summary>
    [JsonProperty("client_order_id")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Order origin
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }
}
