namespace Gate.IO.Api.Spot;

/// <summary>
/// Gate Spot Cancel Order Response
/// </summary>
public class GateSpotCancelOrder
{
    /// <summary>
    /// Order currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }
    
    /// <summary>
    /// Custom order information
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Whether cancellation succeeded
    /// </summary>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Error label when failed to cancel the order; emtpy if succeeded
    /// </summary>
    [JsonProperty("label")]
    public string ErrorLabel { get; set; }

    /// <summary>
    /// Error message when failed to cancel the order; empty if succeeded
    /// </summary>
    [JsonProperty("message")]
    public string ErrorMessage { get; set; }
    
    /// <summary>
    /// Empty by default. If cancelled order is cross margin order, this field is set to cross_margin
    /// </summary>
    [JsonProperty("account")]
    public GateSpotAccountType? Account { get; set; }
}