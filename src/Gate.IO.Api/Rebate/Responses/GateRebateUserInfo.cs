namespace Gate.IO.Api.Rebate;

/// <summary>
/// Rebate user information
/// </summary>
public record GateRebateUserInfo
{
    /// <summary>
    /// Inviter UID
    /// </summary>
    [JsonProperty("invite_uid")]
    public long InviteUserId { get; set; }
}
