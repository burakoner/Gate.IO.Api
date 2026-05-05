namespace Gate.IO.Api.Futures;

/// <summary>
/// Represents a Futures user position ADL rank update.
/// </summary>
public record GateFuturesStreamAdlRank
{
    /// <summary>
    /// User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Futures contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Position mode.
    /// </summary>
    [JsonProperty("mode"), JsonConverter(typeof(MapConverter))]
    public GateFuturesPositionMode Mode { get; set; }

    /// <summary>
    /// ADL rank division, ranging from 1 to 5.
    /// </summary>
    [JsonProperty("rank_division")]
    public int RankDivision { get; set; }

    /// <summary>
    /// Update timestamp in milliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
