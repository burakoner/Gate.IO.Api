namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank card supplementary material item
/// </summary>
public record GateOtcBankSupplementChecklistItem
{
    /// <summary>
    /// Supplementary document submission description
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }
}
