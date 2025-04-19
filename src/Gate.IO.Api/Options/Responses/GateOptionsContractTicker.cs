namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsContractTicker
/// </summary>
public record GateOptionsContractTicker
{
    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Last trading price (quote currency)
    /// </summary>
    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Current mark price (quote currency)
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Current index price (quote currency)
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// Best ask size
    /// </summary>
    [JsonProperty("ask1_size")]
    public long BestAskSize { get; set; }

    /// <summary>
    /// Best ask price
    /// </summary>
    [JsonProperty("ask1_price")]
    public decimal BestAskPrice { get; set; }

    /// <summary>
    /// Best bid size
    /// </summary>
    [JsonProperty("bid1_size")]
    public long BestBidSize { get; set; }

    /// <summary>
    /// Best bid price
    /// </summary>
    [JsonProperty("bid1_price")]
    public decimal BestBidPrice { get; set; }

    /// <summary>
    /// Current total long position size
    /// </summary>
    [JsonProperty("position_size")]
    public long PositionSize { get; set; }

    /// <summary>
    /// Implied volatility
    /// </summary>
    [JsonProperty("mark_iv")]
    public decimal MarkIv { get; set; }

    /// <summary>
    /// Bid side implied volatility
    /// </summary>
    [JsonProperty("bid_iv")]
    public decimal BidIv { get; set; }

    /// <summary>
    /// Ask side implied volatility
    /// </summary>
    [JsonProperty("ask_iv")]
    public decimal AskIv { get; set; }

    /// <summary>
    /// Current leverage. Formula: underlying_price / mark_price * delta
    /// NOTE: May return decimal or "nan". Sot It must be string
    /// </summary>
    [JsonProperty("leverage")]
    public string Leverage { get; set; }

    /// <summary>
    /// Delta
    /// </summary>
    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// Gamma
    /// </summary>
    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// Vega
    /// </summary>
    [JsonProperty("vega")]
    public decimal Vega { get; set; }

    /// <summary>
    /// Theta
    /// </summary>
    [JsonProperty("theta")]
    public decimal Theta { get; set; }

    /// <summary>
    /// Rho
    /// </summary>
    [JsonProperty("rho")]
    public decimal Rho { get; set; }
}