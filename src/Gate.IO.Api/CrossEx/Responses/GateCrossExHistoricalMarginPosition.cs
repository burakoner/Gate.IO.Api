namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx historical margin position
/// </summary>
public record GateCrossExHistoricalMarginPosition : GateCrossExHistoricalPosition
{
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("bussiness_type")]
    private string BusinessTypeTypo { set => BusinessType = value; }
}
