namespace Gate.IO.Api.Account;

/// <summary>
/// Account API key information
/// </summary>
public record GateAccountKeyInfo
{
    /// <summary>
    /// API key status
    /// </summary>
    [JsonProperty("state")]
    public GateAccountApiKeyState State { get; set; }

    /// <summary>
    /// User mode
    /// </summary>
    [JsonProperty("mode")]
    public GateAccountApiKeyMode Mode { get; set; }

    /// <summary>
    /// API key remark
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Trading pair whitelist
    /// </summary>
    [JsonProperty("currency_pairs")]
    public List<string> CurrencyPairs { get; set; } = [];

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// IP whitelist
    /// </summary>
    [JsonProperty("ip_whitelist")]
    public List<string> IpWhitelist { get; set; } = [];

    /// <summary>
    /// Permissions
    /// </summary>
    [JsonProperty("perms")]
    public List<GateAccountApiKeyPermission> Permissions { get; set; } = [];

    /// <summary>
    /// API key
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    /// Created time
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
    /// Last update time returned by older Gate responses
    /// </summary>
    [JsonProperty("update_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateAt
    {
        get => UpdatedAt;
        set => UpdatedAt = value;
    }

    /// <summary>
    /// Last access time
    /// </summary>
    [JsonProperty("last_access")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? LastAccessAt { get; set; }
}

/// <summary>
/// Account API key permission
/// </summary>
public record GateAccountApiKeyPermission
{
    /// <summary>
    /// Permission function name
    /// </summary>
    [JsonProperty("name")]
    [JsonConverter(typeof(MapConverter))]
    public GateAccountApiKeyPermissionSection Name { get; set; }

    /// <summary>
    /// Read only
    /// </summary>
    [JsonProperty("read_only")]
    public bool ReadOnly { get; set; }
}
