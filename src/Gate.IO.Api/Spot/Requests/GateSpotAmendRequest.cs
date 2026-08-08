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
    [JsonIgnore]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client  Order Id
    /// </summary>
    [JsonIgnore]
    public string ClientOrderId { get; set; }

    [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
    private string OrderIdValue => OrderId?.ToString(CultureInfo.InvariantCulture) ?? ClientOrderId;

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

    /// <summary>
    /// Take-profit settings for a limit order. For buy orders, the trigger price must be greater than the order price; for sell orders, it must be lower. Use an empty object to cancel take profit; use null to leave it unchanged.
    /// </summary>
    [JsonProperty("stop_profit", NullValueHandling = NullValueHandling.Ignore)]
    public GateSpotOrderTpsl StopProfit { get; set; }

    /// <summary>
    /// Stop-loss settings for a limit order. For buy orders, the trigger price must be lower than the order price; for sell orders, it must be greater. Use an empty object to cancel stop loss; use null to leave it unchanged.
    /// </summary>
    [JsonProperty("stop_loss", NullValueHandling = NullValueHandling.Ignore)]
    public GateSpotOrderTpsl StopLoss { get; set; }
}
