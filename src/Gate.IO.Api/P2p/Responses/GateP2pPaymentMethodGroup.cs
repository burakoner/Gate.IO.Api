namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P payment method group
/// </summary>
public record GateP2pPaymentMethodGroup
{
    /// <summary>
    /// Payment type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PayType { get; set; }

    /// <summary>
    /// Payment display name
    /// </summary>
    [JsonProperty("pay_name")]
    public string PayName { get; set; }

    /// <summary>
    /// Bound payment method IDs
    /// </summary>
    [JsonProperty("ids")]
    public List<long> Ids { get; set; } = [];

    /// <summary>
    /// Payment method accounts
    /// </summary>
    [JsonProperty("list")]
    public List<GateP2pPaymentMethodAccount> List { get; set; } = [];
}
