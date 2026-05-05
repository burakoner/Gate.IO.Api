namespace Gate.IO.Api.Earn;

/// <summary>
/// Currency supporting auto invest
/// </summary>
public record GateEarnAutoInvestCoin
{
    /// <summary>
    /// Currency code
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }

    /// <summary>
    /// Currency icon URL
    /// </summary>
    [JsonProperty("asset_icon_url")]
    public string AssetIconUrl { get; set; }

    /// <summary>
    /// Sort
    /// </summary>
    [JsonProperty("sort")]
    public long Sort { get; set; }
}
