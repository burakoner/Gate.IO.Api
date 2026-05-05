namespace Gate.IO.Api.Rebate;

/// <summary>
/// Partner application eligibility
/// </summary>
public record GateRebatePartnerEligibility
{
    /// <summary>
    /// Whether eligible for application
    /// </summary>
    [JsonProperty("eligible")]
    public bool Eligible { get; set; }

    /// <summary>
    /// List of ineligibility reason descriptions
    /// </summary>
    [JsonProperty("block_reasons")]
    public List<string> BlockReasons { get; set; } = [];

    /// <summary>
    /// List of ineligibility reason codes
    /// </summary>
    [JsonProperty("block_reason_codes")]
    public List<string> BlockReasonCodes { get; set; } = [];
}
