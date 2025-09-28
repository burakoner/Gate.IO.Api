namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Amend Order Request
/// </summary>
public record GateSpotAmendRequest
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client  Order Id
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
    public string Amount { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
    public string Price { get; set; }

    /// <summary>
    /// Amend Text
    /// </summary>
    [JsonProperty("amend_text", NullValueHandling = NullValueHandling.Ignore)]
    public string AmendText { get; set; }

    /// <summary>
    /// Account Type
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    [JsonProperty("account", NullValueHandling = NullValueHandling.Ignore)]
    public GateSpotAccountType? Account { get; set; }

    /// <summary>
    /// Action Mode
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    [JsonProperty("action_mode", NullValueHandling = NullValueHandling.Ignore)]
    public GateSpotActionMode? ActionMode { get; set; }
}