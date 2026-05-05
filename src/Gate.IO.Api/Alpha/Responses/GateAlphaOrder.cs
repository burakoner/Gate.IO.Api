namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha order.
/// </summary>
public record GateAlphaOrder
{
    /// <summary>
    /// Order ID.
    /// </summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Transaction hash.
    /// </summary>
    [JsonProperty("tx_hash")]
    public string TransactionHash { get; set; }

    /// <summary>
    /// Buy or sell side.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(MapConverter))]
    public GateAlphaOrderSide Side { get; set; }

    /// <summary>
    /// USDT amount.
    /// </summary>
    [JsonProperty("usdt_amount")]
    public decimal UsdtAmount { get; set; }

    /// <summary>
    /// Token symbol.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Token amount.
    /// </summary>
    [JsonProperty("currency_amount")]
    public decimal CurrencyAmount { get; set; }

    /// <summary>
    /// Order status.
    /// </summary>
    [JsonProperty("status")]
    public GateAlphaOrderStatus Status { get; set; }

    /// <summary>
    /// Trading mode as returned by Gate.
    /// </summary>
    [JsonProperty("gas_mode")]
    public string GasMode { get; set; }

    /// <summary>
    /// Blockchain name.
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// Gas fee in USDT.
    /// </summary>
    [JsonProperty("gas_fee")]
    public decimal GasFee { get; set; }

    /// <summary>
    /// Trading fee in USDT.
    /// </summary>
    [JsonProperty("transaction_fee")]
    public decimal TransactionFee { get; set; }

    /// <summary>
    /// Failure reason, if applicable.
    /// </summary>
    [JsonProperty("failed_reason")]
    public string FailedReason { get; set; }

    /// <summary>
    /// Creation timestamp.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
