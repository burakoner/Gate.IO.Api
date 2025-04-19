namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsSettlement
/// </summary>
public record GateOptionsSettlement
{
    /// <summary>
    /// Last changed time of configuration
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Settlement profit per size (quote currency)
    /// </summary>
    [JsonProperty("profit")]
    public decimal Profit { get; set; }

    /// <summary>
    /// Settlement fee per size (quote currency)
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Strike price (quote currency)
    /// </summary>
    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

    /// <summary>
    /// Settlement price (quote currency)
    /// </summary>
    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }
}