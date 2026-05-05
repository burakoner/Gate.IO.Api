namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni annualized chart point
/// </summary>
public record GateEarnUniChartPoint
{
    /// <summary>
    /// Time
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    [JsonProperty("value")]
    public decimal Value { get; set; }
}
