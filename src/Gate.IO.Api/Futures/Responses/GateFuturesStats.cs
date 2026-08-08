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
    /// Long/short taker ratio
    /// </summary>
    [JsonProperty("lsr_taker")]
    public decimal LongShortTakerRatio { get; set; }

    /// <summary>
    /// Long/short position user ratio
    /// </summary>
    [JsonProperty("lsr_account")]
    public decimal LongShortPositionUserRatio { get; set; }

    /// <summary>
    /// Legacy alias for <see cref="LongShortTakerRatio"/>.
    /// </summary>
    [JsonIgnore]
    [Obsolete("Use LongShortTakerRatio. The lsr_taker field is the long/short taker ratio.")]
    public decimal LongShortAccountNumberRatio
    {
        get => LongShortTakerRatio;
        set => LongShortTakerRatio = value;
    }

    /// <summary>
    /// Legacy alias for <see cref="LongShortPositionUserRatio"/>.
    /// </summary>
    [JsonIgnore]
    [Obsolete("Use LongShortPositionUserRatio. The lsr_account field is the long/short position user ratio.")]
    public decimal LongShortTakerSizeRatio
    {
        get => LongShortPositionUserRatio;
        set => LongShortPositionUserRatio = value;
    }

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
    /// Long liquidations in quote currency. For USDT settlement this is
    /// long liquidation size multiplied by the contract multiplier and mark price.
    /// </summary>
    [JsonProperty("long_liq_usd_new")]
    public decimal? LongLiquidationUsdNew { get; set; }
    
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
    /// Short liquidations in quote currency. For USDT settlement this is
    /// short liquidation size multiplied by the contract multiplier and mark price.
    /// </summary>
    [JsonProperty("short_liq_usd_new")]
    public decimal? ShortLiquidationUsdNew { get; set; }
    
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

    /// <summary>
    /// Top long open interest in contracts
    /// </summary>
    [JsonProperty("top_long_size")]
    public decimal? TopLongSize { get; set; }

    /// <summary>
    /// Top short open interest in contracts
    /// </summary>
    [JsonProperty("top_short_size")]
    public decimal? TopShortSize { get; set; }

    /// <summary>
    /// Long taker trade volume in contracts
    /// </summary>
    [JsonProperty("long_taker_size")]
    public decimal? LongTakerSize { get; set; }

    /// <summary>
    /// Short taker trade volume in contracts
    /// </summary>
    [JsonProperty("short_taker_size")]
    public decimal? ShortTakerSize { get; set; }

    /// <summary>
    /// Number of top long accounts (large holders)
    /// </summary>
    [JsonProperty("top_long_account")]
    public long? TopLongAccount { get; set; }

    /// <summary>
    /// Number of top short accounts (large holders)
    /// </summary>
    [JsonProperty("top_short_account")]
    public long? TopShortAccount { get; set; }

    /// <summary>
    /// Number of users holding long positions
    /// </summary>
    [JsonProperty("long_users")]
    public long? LongUsers { get; set; }

    /// <summary>
    /// Number of users holding short positions
    /// </summary>
    [JsonProperty("short_users")]
    public long? ShortUsers { get; set; }
}
