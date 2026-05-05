namespace Gate.IO.Api.Rebate;

/// <summary>
/// User subordinate relationship
/// </summary>
public record GateRebateUserSubRelation
{
    /// <summary>
    /// Subordinate relationship list
    /// </summary>
    [JsonProperty("list")]
    public List<GateRebateUserSub> List { get; set; } = [];
}

/// <summary>
/// User subordinate relationship item
/// </summary>
public record GateRebateUserSub
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// User's system affiliation
    /// </summary>
    [JsonProperty("belong")]
    public string Belong { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public long Type { get; set; }

    /// <summary>
    /// Inviter user ID
    /// </summary>
    [JsonProperty("ref_uid")]
    public long RefUserId { get; set; }
}
