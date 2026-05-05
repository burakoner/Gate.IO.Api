namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx flash swap quote
/// </summary>
public record GateCrossExConvertQuote
{
    /// <summary>
    /// Gets or sets the Quote ID.
    /// </summary>
    [JsonProperty("quote_id")]
    public string QuoteId { get; set; }

    /// <summary>
    /// Gets or sets the Valid Milliseconds.
    /// </summary>
    [JsonProperty("valid_ms")]
    public long ValidMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the From Coin.
    /// </summary>
    [JsonProperty("from_coin")]
    public string FromCoin { get; set; }

    /// <summary>
    /// Gets or sets the To Coin.
    /// </summary>
    [JsonProperty("to_coin")]
    public string ToCoin { get; set; }

    /// <summary>
    /// Gets or sets the From Amount.
    /// </summary>
    [JsonProperty("from_amount")]
    public decimal FromAmount { get; set; }

    /// <summary>
    /// Gets or sets the To Amount.
    /// </summary>
    [JsonProperty("to_amount")]
    public decimal ToAmount { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
}
