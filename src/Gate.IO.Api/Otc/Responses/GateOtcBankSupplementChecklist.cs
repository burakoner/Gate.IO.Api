namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank card supplement checklist
/// </summary>
public record GateOtcBankSupplementChecklist
{
    /// <summary>
    /// Verification type that determines the supplement endpoint
    /// </summary>
    [JsonProperty("user_type")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcBankUserType UserType { get; set; }

    /// <summary>
    /// Required supplementary material descriptions
    /// </summary>
    [JsonProperty("items")]
    public List<GateOtcBankSupplementChecklistItem> Items { get; set; } = [];
}
