namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Sub-account create request
/// </summary>
public record GateSubAccountCreateRequest
{
    /// <summary>
    /// Sub-account login name
    /// </summary>
    [JsonProperty("login_name")]
    public string Login { get; set; }

    /// <summary>
    /// The sub-account's password. Defaults to the main account's password when omitted.
    /// </summary>
    [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
    public string Password { get; set; }

    /// <summary>
    /// The sub-account's email address. Defaults to the main account's email address when omitted.
    /// </summary>
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string Email { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("remark", NullValueHandling = NullValueHandling.Ignore)]
    public string Remark { get; set; }
}
