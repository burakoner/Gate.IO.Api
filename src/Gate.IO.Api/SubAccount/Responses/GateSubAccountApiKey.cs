namespace Gate.IO.Api.SubAccount;

/// <summary>
/// SubAccountKey
/// </summary>
public record GateSubAccountApiKey
{
    /// <summary>
    /// The user id of the sub-account
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }
    
    /// <summary>
    /// Mode: 1 - classic 2 - portfolio account
    /// </summary>
    [JsonProperty("mode")]
    public int Mode { get; set; }

    /// <summary>
    /// API key name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or Sets Perms
    /// </summary>
    [JsonProperty("perms")]
    public List<GateSubAccountApiKeyPermission> Permissions { get; set; } = [];
    
    /// <summary>
    /// ip white list (list will be removed if no value is passed)
    /// </summary>
    [JsonProperty("ip_whitelist")]
    public List<string> IpWhitelist { get; set; } = [];

    /// <summary>
    /// API Key
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    /// State 1 - normal 2 - locked 3 - frozen
    /// </summary>
    [JsonProperty("state")]
    public int State { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("created_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Last update time
    /// </summary>
    [JsonProperty("updated_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Last access time
    /// </summary>
    [JsonProperty("last_access")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? LastAccessAt { get; set; }

}
