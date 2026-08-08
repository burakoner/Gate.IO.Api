namespace Gate.IO.Api.Stock;

/// <summary>
/// Supported stock exchange
/// </summary>
public record GateStockExchangeInfo
{
    /// <summary>Gets or sets the exchange.</summary>
    [JsonProperty("exchange"), JsonConverter(typeof(MapConverter))]
    public GateStockExchange Exchange { get; set; }
    /// <summary>Gets or sets the exchange description.</summary>
    [JsonProperty("exchange_desc")]
    public string Description { get; set; }
    /// <summary>Gets or sets the icon link.</summary>
    [JsonProperty("icon_link")]
    public string IconLink { get; set; }
    /// <summary>Gets or sets whether stock transfer is supported.</summary>
    [JsonProperty("support_transfer")]
    public bool SupportsTransfer { get; set; }
}
