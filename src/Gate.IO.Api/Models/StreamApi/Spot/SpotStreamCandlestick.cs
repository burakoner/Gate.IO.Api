namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamCandlestick
{
    [JsonProperty("t")]
    public DateTime Time { get; set; }

    /// <summary>
    /// Close price (quote currency)
    /// </summary>
    [JsonProperty("c")]
    public decimal Close { get; set; }

    /// <summary>
    /// Highest price (quote currency)
    /// </summary>
    [JsonProperty("h")]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price (quote currency)
    /// </summary>
    [JsonProperty("l")]
    public decimal Low { get; set; }

    /// <summary>
    /// Open price (quote currency)
    /// </summary>
    [JsonProperty("o")]
    public decimal Open { get; set; }

    /// <summary>
    /// Total volume
    /// </summary>
    [JsonProperty("v")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Base currency trading amount
    /// </summary>
    [JsonProperty("a")]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// Name of the subscription, in the format of<interval>_<cp>
    /// </summary>
    [JsonProperty("n")]
    public string Subscription { get; set; }
}
