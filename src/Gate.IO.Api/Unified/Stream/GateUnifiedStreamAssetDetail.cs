namespace Gate.IO.Api.Unified;

/// <summary>
/// Represents a Unified asset detail stream update.
/// </summary>
public record GateUnifiedStreamAssetDetail
{
    /// <summary>
    /// Gate user ID.
    /// </summary>
    [JsonProperty("u")]
    public long UserId { get; set; }

    /// <summary>
    /// Data refresh time.
    /// </summary>
    [JsonProperty("t")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime RefreshTime { get; set; }

    /// <summary>
    /// Asset details by currency.
    /// </summary>
    [JsonProperty("dts")]
    public Dictionary<string, GateUnifiedStreamAssetDetailItem> Details { get; set; } = [];
}
