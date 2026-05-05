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
    /// Advertiser nickname
    /// </summary>
    [JsonProperty("nick_name")]
    public string NickName { get; set; }
}
