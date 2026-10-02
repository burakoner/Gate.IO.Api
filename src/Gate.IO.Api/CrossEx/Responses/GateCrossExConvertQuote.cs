namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx flash swap quote
/// </summary>
public record GateCrossExConvertQuote
{
    /// <summary>
    /// Gets or sets the Quote ID.
    /// </summary>
    [JsonProperty("quote_id", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string QuoteId { get; set; }

    /// <summary>
    /// Raw valid_ms Int64 value, documented as a millisecond validity timestamp (example: 5000).
    /// The client does not infer an expiry date, duration or clock origin from this value.
    /// </summary>
    [JsonProperty("valid_ms", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public long ValidMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the From Coin.
    /// </summary>
    [JsonProperty("from_coin", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string FromCoin { get; set; }

    /// <summary>
    /// Gets or sets the To Coin.
    /// </summary>
    [JsonProperty("to_coin", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string ToCoin { get; set; }

    /// <summary>
    /// Gets or sets the From Amount.
    /// </summary>
    [JsonProperty("from_amount", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal FromAmount { get; set; }

    /// <summary>
    /// Gets or sets the To Amount.
    /// </summary>
    [JsonProperty("to_amount", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal ToAmount { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Price { get; set; }
}
