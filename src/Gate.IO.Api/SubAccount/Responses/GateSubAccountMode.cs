using Gate.IO.Api.Unified;

namespace Gate.IO.Api.SubAccount;

/// <summary>
/// GateSubAccountMode
/// </summary>
public record GateSubAccountMode
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Is it a unified account?
    /// </summary>
    [JsonProperty("is_unified")]
    public bool IsUnified { get; set; }

    /// <summary>
    /// Unified account mode
    /// </summary>
    [JsonProperty("mode")]
    public GateUnifiedAccountMode Mode { get; set; }
}
