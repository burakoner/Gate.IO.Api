namespace Gate.IO.Api.Rebate;

/// <summary>
/// Partner subordinate list
/// </summary>
public record GateRebatePartnerSubList
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }

    /// <summary>
    /// Subordinate list
    /// </summary>
    [JsonProperty("list")]
    public List<GateRebatePartnerSub> List { get; set; } = [];
}

/// <summary>
/// Partner subordinate
/// </summary>
public record GateRebatePartnerSub
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Time when user joined the system
    /// </summary>
    [JsonProperty("user_join_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UserJoinTime { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public long Type { get; set; }
}
