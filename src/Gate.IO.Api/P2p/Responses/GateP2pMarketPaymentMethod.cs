namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P market advertisement payment method
/// </summary>
public record GateP2pMarketPaymentMethod
{
    /// <summary>
    /// Payment method color icon URL
    /// </summary>
    [JsonProperty("icon_url_color")]
    public string IconUrl { get; set; }

    /// <summary>
    /// Payment method identifier
    /// </summary>
    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// Payment method ID
    /// </summary>
    [JsonProperty("pay_id")]
    public string PaymentId { get; set; }

    /// <summary>
    /// Payment method type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PaymentType { get; set; }

    /// <summary>
    /// Payment method name
    /// </summary>
    [JsonProperty("trade_method_name")]
    public string Name { get; set; }
}
