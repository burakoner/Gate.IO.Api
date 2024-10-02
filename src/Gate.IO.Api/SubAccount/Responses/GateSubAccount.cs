namespace Gate.IO.Api.SubAccount;

/// <summary>
/// SubAccount
/// </summary>
public class GateSubAccount
{
    /// <summary>
    /// custom text
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; }

    /// <summary>
    /// Sub-account login name: Only letters, numbers and underscores are supported, and cannot contain other illegal characters
    /// </summary>
    [JsonProperty("login_name")]
    public string Login { get; set; }

    /// <summary>
    /// The sub-account&#39;s password. (Default: the same as main account&#39;s password)
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; set; }

    /// <summary>
    /// The sub-account&#39;s email address. (Default: the same as main account&#39;s email address)
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    /// State: 1-normal, 2-locked\&quot;
    /// </summary>
    [JsonProperty("state")]
    public GateSubAccountState State { get; set; }

    /// <summary>
    /// "Sub-account type: 1 - sub-account, 3 - cross margin account
    /// </summary>
    [JsonProperty("type")]
    public GateSubAccountType Type { get; set; }

    /// <summary>
    /// The user id of the sub-account
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }
}
