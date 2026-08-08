namespace Gate.IO.Api.P2p;

/// <summary>
/// Advertisement content risk-control prompt
/// </summary>
public record GateP2pAdvertisementRiskEvent
{
    /// <summary>
    /// Prompt display type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Prompt title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// Prompt message
    /// </summary>
    [JsonProperty("msg")]
    public string Message { get; set; }

    /// <summary>
    /// Available prompt actions
    /// </summary>
    [JsonProperty("action")]
    public List<GateP2pAdvertisementRiskAction> Actions { get; set; } = [];

    /// <summary>
    /// Advertisement content field that triggered risk control
    /// </summary>
    [JsonProperty("content_risk_type")]
    public string ContentRiskType { get; set; }

    /// <summary>
    /// Prompt returned when trade terms triggered risk control
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    /// <summary>
    /// Prompt returned when the automatic reply triggered risk control
    /// </summary>
    [JsonProperty("auto_reply")]
    public string AutoReply { get; set; }
}

/// <summary>
/// Advertisement risk-control prompt action
/// </summary>
public record GateP2pAdvertisementRiskAction
{
    /// <summary>
    /// Action type
    /// </summary>
    [JsonProperty("action_type")]
    public string ActionType { get; set; }

    /// <summary>
    /// Action title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// Whether this is the primary action
    /// </summary>
    [JsonProperty("mainly")]
    public int Mainly { get; set; }

    /// <summary>
    /// Additional action data
    /// </summary>
    [JsonProperty("action_data")]
    public JObject ActionData { get; set; }
}
