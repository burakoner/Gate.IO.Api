namespace Gate.IO.Api.Futures;

/// <summary>
/// Current market-level ADL risk states for a perpetual futures settlement currency.
/// </summary>
public record GateFuturesAdlRiskStates
{
    /// <summary>
    /// Settlement currency returned by the server. It is not inferred from the selected client.
    /// </summary>
    [JsonProperty("settle", Required = Required.Always)]
    public string Settlement { get; set; }

    /// <summary>
    /// Mapping from contract names to their current market ADL risk state.
    /// An empty mapping contains no market evidence; an omitted/null mapping is not a valid response.
    /// </summary>
    [JsonProperty("states", Required = Required.Always)]
    public Dictionary<string, GateFuturesAdlRiskState> States { get; set; }
}
