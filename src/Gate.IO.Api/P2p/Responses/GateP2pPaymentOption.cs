namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P payment option
/// </summary>
public record GateP2pPaymentOption
{
    /// <summary>
    /// Payment method ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Payment method description
    /// </summary>
    [JsonProperty("account_des")]
    public string AccountDescription { get; set; }

    /// <summary>
    /// Payment type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PayType { get; set; }

    /// <summary>
    /// Payment name
    /// </summary>
    [JsonProperty("pay_name")]
    public string PayName { get; set; }

    /// <summary>
    /// Payment account
    /// </summary>
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Trading tips
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
