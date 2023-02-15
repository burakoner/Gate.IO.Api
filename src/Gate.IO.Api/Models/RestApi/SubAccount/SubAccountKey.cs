namespace Gate.IO.Api.Models.RestApi.SubAccount;

public class SubAccountKey
{
    /// <summary>
    /// The user id of the sub-account
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get;  set; }

    /// <summary>
    /// API key name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// API Key
    /// </summary>
    [JsonProperty("key")]
    public string Key { get;  set; }

    /// <summary>
    /// ip white list (list will be removed if no value is passed)
    /// </summary>
    [JsonProperty("ip_whitelist")]
    public IEnumerable<string> IpWhitelist { get; set; }=new List<string>();

    /// <summary>
    /// State 1 - normal 2 - locked 3 - frozen
    /// </summary>
    [JsonProperty("state")]
    public int State { get;  set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("created_at"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreatedAt { get;  set; }

    /// <summary>
    /// Last update time
    /// </summary>
    [JsonProperty("updated_at"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdatedAt { get;  set; }

    /// <summary>
    /// Gets or Sets Perms
    /// </summary>
    [JsonProperty("perms")]
    public IEnumerable<SubAccountKeyPermission> Permissions { get; set; } = new List<SubAccountKeyPermission>();
}

public class SubAccountKeyPermission
{
    /// <summary>
    /// Permission name (all permissions will be removed if no value is passed)
    /// </summary>
    [JsonProperty("name"), JsonConverter(typeof(ApiKeyPermissionSectionConverter))]
    public ApiKeyPermissionSection Name { get; set; }

    /// <summary>
    /// Read Only
    /// </summary>
    [JsonProperty("read_only")]
    public bool ReadOnly { get; set; }
}