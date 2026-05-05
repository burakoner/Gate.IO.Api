namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn bonus campaign information
/// </summary>
public record GateEarnFixedTermBonusInfo
{
    /// <summary>
    /// Activity ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Associated product ID
    /// </summary>
    [JsonProperty("product_id")]
    public long ProductId { get; set; }

    /// <summary>
    /// Product currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Reward currency
    /// </summary>
    [JsonProperty("bonus_asset")]
    public string BonusAsset { get; set; }

    /// <summary>
    /// KYC level restrictions
    /// </summary>
    [JsonProperty("kyc_limit")]
    public string KycLimit { get; set; }

    /// <summary>
    /// Tiered annual interest rate
    /// </summary>
    [JsonProperty("ladder_apr")]
    public List<GateEarnFixedTermLadderApr> LadderApr { get; set; } = [];

    /// <summary>
    /// Total reward amount
    /// </summary>
    [JsonProperty("total_bonus_amount")]
    public decimal TotalBonusAmount { get; set; }

    /// <summary>
    /// Maximum reward per user
    /// </summary>
    [JsonProperty("user_total_bonus_amount")]
    public decimal UserTotalBonusAmount { get; set; }

    /// <summary>
    /// Activity status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// Activity start time
    /// </summary>
    [JsonProperty("start_time")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// Activity end time
    /// </summary>
    [JsonProperty("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Activity start timestamp
    /// </summary>
    [JsonProperty("start_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? StartAt { get; set; }

    /// <summary>
    /// Activity end timestamp
    /// </summary>
    [JsonProperty("end_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? EndAt { get; set; }

    /// <summary>
    /// Total rewards distributed
    /// </summary>
    [JsonProperty("total_issued_amount")]
    public decimal TotalIssuedAmount { get; set; }

    /// <summary>
    /// Total rewards distributed to the user
    /// </summary>
    [JsonProperty("user_total_issued_amount")]
    public decimal UserTotalIssuedAmount { get; set; }

    /// <summary>
    /// Reward currency price
    /// </summary>
    [JsonProperty("bonus_asset_price")]
    public decimal BonusAssetPrice { get; set; }

    /// <summary>
    /// Product currency price
    /// </summary>
    [JsonProperty("product_asset_price")]
    public decimal ProductAssetPrice { get; set; }

    /// <summary>
    /// Product base annual interest rate
    /// </summary>
    [JsonProperty("product_year_rate")]
    public decimal ProductYearRate { get; set; }
}
