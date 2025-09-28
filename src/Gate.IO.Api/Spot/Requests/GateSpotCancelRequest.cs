namespace Gate.IO.Api.Spot;

/// <summary>
/// SpotCancelOrderRequest
/// </summary>
public record GateSpotCancelRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// If cancelled order is cross margin order or is portfolio margin account's API key, this field must be set and can only be cross_marginIf cancelled order is cross margin order, this field must be set and can only be cross_margin
    /// </summary>
    [JsonProperty("account", NullValueHandling = NullValueHandling.Ignore)]
    public GateSpotAccountType? Account { get; set; }

    /// <summary>
    /// Processing Mode:
    /// When placing an order, different fields are returned based on action_mode. This field is only valid during the request and is not included in the response result
    /// ACK: Asynchronous mode, only returns key order fields
    /// RESULT: No clearing informatio
    /// FULL: Full mode (default)
    /// </summary>
    [JsonProperty("action_mode", NullValueHandling = NullValueHandling.Ignore)]
    public GateSpotActionMode? ActionMode { get; set; }
}