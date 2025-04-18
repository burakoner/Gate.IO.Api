namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet User Trading Fee
/// </summary>
public record GateWalletUserTradingFee
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// taker fee rate
    /// </summary>
    [JsonProperty("taker_fee")]
    public decimal TakerFee { get; set; }

    /// <summary>
    /// maker fee rate
    /// </summary>
    [JsonProperty("maker_fee")]
    public decimal MakerFee { get; set; }

    /// <summary>
    /// If GT deduction is enabled
    /// </summary>
    [JsonProperty("gt_discount")]
    public bool GtDiscount { get; set; }

    /// <summary>
    /// Taker fee rate if using GT deduction. It will be 0 if GT deduction is disabled
    /// </summary>
    [JsonProperty("gt_taker_fee")]
    public decimal GtTakerFee { get; set; }

    /// <summary>
    /// Maker fee rate if using GT deduction. It will be 0 if GT deduction is disabled
    /// </summary>
    [JsonProperty("gt_maker_fee")]
    public decimal GtMakerFee { get; set; }

    /// <summary>
    /// Loan fee rate of margin lending
    /// </summary>
    [JsonProperty("loan_fee")]
    public decimal LoanFee { get; set; }

    /// <summary>
    /// Point type. 0 - Initial version. 1 - new version since 202009
    /// </summary>
    [JsonProperty("point_type")]
    public int PointType { get; set; }

    /// <summary>
    /// Futures trading taker fee
    /// </summary>
    [JsonProperty("futures_taker_fee")]
    public decimal FuturesTakerFee { get; set; }

    /// <summary>
    /// Future trading maker fee
    /// </summary>
    [JsonProperty("futures_maker_fee")]
    public decimal FuturesMakerFee { get; set; }

    /// <summary>
    /// Delivery trading taker fee
    /// </summary>
    [JsonProperty("delivery_taker_fee")]
    public decimal DeliveryTakerFee { get; set; }

    /// <summary>
    /// Delivery trading maker fee
    /// </summary>
    [JsonProperty("delivery_maker_fee")]
    public decimal DeliveryMakerFee { get; set; }

    /// <summary>
    /// Deduction types for rates, 1 - GT deduction, 2 - Point card deduction, 3 - VIP rates
    /// </summary>
    [JsonProperty("debit_fee")]
    public decimal DebitFee { get; set; }
}