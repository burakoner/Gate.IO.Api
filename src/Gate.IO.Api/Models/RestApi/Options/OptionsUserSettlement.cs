namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsUserSettlement
{
    /// <summary>
    /// Last changed time of configuration
    /// </summary>
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

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

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Settlement profit (quote currency)
    /// </summary>
    [JsonProperty("settle_profit")]
    public decimal SettlementProfit { get; set; }

    /// <summary>
    /// The accumulated profit and loss of opening a position, including premium, fee, settlement profit, etc. (quote currency)
    /// </summary>
    [JsonProperty("realised_pnl")]
    public decimal RealisedPnl { get; set; }
}