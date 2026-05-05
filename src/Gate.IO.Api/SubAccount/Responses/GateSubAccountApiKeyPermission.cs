namespace Gate.IO.Api.SubAccount;

/// <summary>
/// SubAccountKeyPermission
/// </summary>
public record GateSubAccountApiKeyPermission
{
    /// <summary>
    /// Permission name (all permissions will be removed if no value is passed)
    /// </summary>
    [JsonProperty("name")]
    [JsonConverter(typeof(MapConverter))]
    public GateSubAccountApiKeyPermissionSection Name { get; set; }

    /// <summary>
    /// Read Only
    /// </summary>
    [JsonProperty("read_only")]
    public bool ReadOnly { get; set; }
}
