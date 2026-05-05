namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Sub-account API key create/update request
/// </summary>
public record GateSubAccountApiKeyRequest
{
    /// <summary>
    /// Mode: 1 - classic, 2 - portfolio account
    /// </summary>
    [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
    public int? Mode { get; set; }

    /// <summary>
    /// API key name
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// Permissions
    /// </summary>
    [JsonProperty("perms", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<GateSubAccountApiKeyPermission> Permissions { get; set; }

    /// <summary>
    /// IP whitelist. The list will be cleared if no value is passed.
    /// </summary>
    [JsonProperty("ip_whitelist", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> IpWhitelist { get; set; }
}
