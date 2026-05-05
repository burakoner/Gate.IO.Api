namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC currency information
/// </summary>
public record GateOtcCurrencyInfo
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Icon URL
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; }
}
