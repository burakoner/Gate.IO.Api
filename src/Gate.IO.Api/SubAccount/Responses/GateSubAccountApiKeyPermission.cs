namespace Gate.IO.Api.SubAccount;

/// <summary>
/// SubAccountKeyPermission
/// </summary>
public class GateSubAccountApiKeyPermission
{
    /// <summary>
    /// Permission name (all permissions will be removed if no value is passed)
    /// </summary>
    [JsonProperty("name")]
    public GateSubAccountApiKeyPermissionSection Name { get; set; }

    /// <summary>
    /// Read Only
    /// </summary>
    [JsonProperty("read_only")]
    public bool ReadOnly { get; set; }
}