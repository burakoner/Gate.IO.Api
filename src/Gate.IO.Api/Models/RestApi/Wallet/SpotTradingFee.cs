namespace Gate.IO.Api.Models.RestApi.Wallet;

/// <summary>
/// Trading Fee
/// </summary>
public  class SpotTradingFee
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
}