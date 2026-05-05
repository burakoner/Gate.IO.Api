namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx historical margin position
/// </summary>
public record GateCrossExHistoricalMarginPosition : GateCrossExHistoricalPosition
{
    /// <summary>
    /// Gets or sets the Interest.
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("bussiness_type")]
    private string BusinessTypeTypo { set => BusinessType = value; }
}
