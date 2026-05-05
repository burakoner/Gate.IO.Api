namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P flash swap conversion information
/// </summary>
public record GateP2pConvertInfo
{
    /// <summary>
    /// Target currency
    /// </summary>
    [JsonProperty("convert_type")]
    public string ConvertType { get; set; }

    /// <summary>
    /// Conversion status
    /// </summary>
    [JsonProperty("convert_status")]
    public string ConvertStatus { get; set; }

    /// <summary>
    /// Expected price
    /// </summary>
    [JsonProperty("pre_rate")]
    public decimal? PreRate { get; set; }

    /// <summary>
    /// Execution price
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Expected fiat price
    /// </summary>
    [JsonProperty("pre_fiat_rate")]
    public decimal? PreFiatRate { get; set; }

    /// <summary>
    /// Fiat execution price
    /// </summary>
    [JsonProperty("fiat_rate")]
    public decimal? FiatRate { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Swap amount
    /// </summary>
    [JsonProperty("convert_amount")]
    public decimal? ConvertAmount { get; set; }

    /// <summary>
    /// Slippage
    /// </summary>
    [JsonProperty("slippage")]
    public decimal? Slippage { get; set; }

    /// <summary>
    /// Display status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
}
