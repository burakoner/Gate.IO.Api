namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesStats
/// </summary>
public record GateFuturesStats
{
    /// <summary>
    /// Stat timestamp
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Long/short account number ratio
    /// </summary>
    [JsonProperty("lsr_taker")]
    public decimal LongShortAccountNumberRatio { get; set; }

    /// <summary>
    /// Long/short taker size ratio
    /// </summary>
    [JsonProperty("lsr_account")]
    public decimal LongShortTakerSizeRatio { get; set; }

    /// <summary>
    /// Long liquidation size
    /// </summary>
    [JsonProperty("long_liq_size")]
    public decimal LongLiquidationSize { get; set; }
    
    /// <summary>
    /// Long liquidation amount(base currency)
    /// </summary>
    [JsonProperty("long_liq_amount")]
    public decimal LongLiquidationAmount { get; set; }
    
    /// <summary>
    /// Long liquidation volume(quote currency)
    /// </summary>
    [JsonProperty("long_liq_usd")]
    public decimal LongLiquidationUsd { get; set; }
    
    /// <summary>
    /// Short liquidation size
    /// </summary>
    [JsonProperty("short_liq_size")]
    public decimal ShortLiquidationSize { get; set; }
    
    /// <summary>
    /// Short liquidation amount(base currency)
    /// </summary>
    [JsonProperty("short_liq_amount")]
    public decimal ShortLiquidationAmount { get; set; }
    
    /// <summary>
    /// Short liquidation volume(quote currency)
    /// </summary>
    [JsonProperty("short_liq_usd")]
    public decimal ShortLiquidationUsd { get; set; }
    
    /// <summary>
    /// Open interest size
    /// </summary>
    [JsonProperty("open_interest")]
    public decimal OpenInterest { get; set; }
    
    /// <summary>
    /// Open interest volume(quote currency)
    /// </summary>
    [JsonProperty("open_interest_usd")]
    public decimal OpenInterestUsd { get; set; }
    
    /// <summary>
    /// Top trader long/short account ratio
    /// </summary>
    [JsonProperty("top_lsr_account")]
    public decimal TopTraderLongShortAccountRatio { get; set; }

    /// <summary>
    /// Top trader long/short position ratio
    /// </summary>
    [JsonProperty("top_lsr_size")]
    public decimal TopTraderLongShortPositionRatio { get; set; }

    /// <summary>
    /// Mark Price
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }
}
