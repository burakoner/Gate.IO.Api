namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx flash swap quote
/// </summary>
public record GateCrossExConvertQuote
{
    [JsonProperty("quote_id")]
    public string QuoteId { get; set; }

    [JsonProperty("valid_ms")]
    public long ValidMilliseconds { get; set; }

    [JsonProperty("from_coin")]
    public string FromCoin { get; set; }

    [JsonProperty("to_coin")]
    public string ToCoin { get; set; }

    [JsonProperty("from_amount")]
    public decimal FromAmount { get; set; }

    [JsonProperty("to_amount")]
    public decimal ToAmount { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }
}
