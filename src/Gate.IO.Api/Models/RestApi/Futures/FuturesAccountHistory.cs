namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesAccountHistory
{
    /// <summary>
    /// total amount of deposit and withdraw
    /// </summary>
    [JsonProperty("dnw")]
    public decimal DNW { get; set; }

    /// <summary>
    /// total amount of trading profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    /// <summary>
    /// total amount of fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// total amount of referrer rebates
    /// </summary>
    [JsonProperty("refr")]
    public decimal ReferrerRebates { get; set; }

    /// <summary>
    /// total amount of funding costs
    /// </summary>
    [JsonProperty("fund")]
    public decimal Fund { get; set; }

    /// <summary>
    /// total amount of point deposit and withdraw
    /// </summary>
    [JsonProperty("point_dnw")]
    public decimal PointDNW { get; set; }

    /// <summary>
    /// total amount of point fee
    /// </summary>
    [JsonProperty("point_fee")]
    public decimal PointFee { get; set; }

    /// <summary>
    /// total amount of referrer rebates of point fee
    /// </summary>
    [JsonProperty("point_refr")]
    public decimal PointReferrerRebates { get; set; }

    /// <summary>
    /// total amount of perpetual contract bonus transfer
    /// </summary>
    [JsonProperty("bonus_dnw")]
    public decimal BonusDNW { get; set; }

    /// <summary>
    /// total amount of perpetual contract bonus deduction
    /// </summary>
    [JsonProperty("bonus_offset")]
    public decimal BonusOffset { get; set; }
}
