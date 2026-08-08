namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P market advertisement
/// </summary>
public record GateP2pMarketAdvertisement
{
    /// <summary>
    /// Serial number
    /// </summary>
    [JsonProperty("index")]
    public int? Index { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("fiat_unit")]
    public string FiatUnit { get; set; }

    /// <summary>
    /// Advertisement ID
    /// </summary>
    [JsonProperty("adv_no")]
    public long? AdvertisementId { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Remaining tradable cryptocurrency quantity
    /// </summary>
    [JsonProperty("surplus_amount")]
    public decimal? SurplusAmount { get; set; }

    /// <summary>
    /// Maximum crypto size per trade
    /// </summary>
    [JsonProperty("max_single_trans_amount")]
    public decimal? MaximumSingleTransactionAmount { get; set; }

    /// <summary>
    /// Minimum crypto size per trade
    /// </summary>
    [JsonProperty("min_single_trans_amount")]
    public decimal? MinimumSingleTransactionAmount { get; set; }

    /// <summary>
    /// Minimum fiat amount per order
    /// </summary>
    [JsonProperty("fiat_min_amount")]
    public decimal? MinimumFiatAmount { get; set; }

    /// <summary>
    /// Maximum fiat amount per order
    /// </summary>
    [JsonProperty("fiat_max_amount")]
    public decimal? MaximumFiatAmount { get; set; }

    /// <summary>
    /// Trading-limit unit
    /// </summary>
    [JsonProperty("limit_basis")]
    public GateP2pAdLimitBasis? LimitBasis { get; set; }

    /// <summary>
    /// Trading-limit unit label
    /// </summary>
    [JsonProperty("limit_basis_text")]
    public string LimitBasisText { get; set; }

    /// <summary>
    /// Supported payment methods
    /// </summary>
    [JsonProperty("trade_methods")]
    public List<GateP2pMarketPaymentMethod> TradeMethods { get; set; } = [];

    /// <summary>
    /// Advertiser nickname
    /// </summary>
    [JsonProperty("nick_name")]
    public string NickName { get; set; }
}
