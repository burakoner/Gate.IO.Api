namespace Gate.IO.Api.Models.RestApi.SubAccount;

public class SubAccount
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
    [JsonProperty("state"), JsonConverter(typeof(SubAccountStateConverter))]
    public SubAccountState State { get; private set; }

    /// <summary>
    /// The user id of the sub-account
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; private set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; private set; }
}
