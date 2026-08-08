namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P action response data
/// </summary>
public record GateP2pActionData
{
    /// <summary>
    /// Advertisement content risk-control sub-code
    /// </summary>
    [JsonProperty("risk_code")]
    public int? RiskCode { get; set; }

    /// <summary>
    /// Advertisement content risk-control prompt
    /// </summary>
    [JsonProperty("risk_event")]
    public GateP2pAdvertisementRiskEvent RiskEvent { get; set; }
}
