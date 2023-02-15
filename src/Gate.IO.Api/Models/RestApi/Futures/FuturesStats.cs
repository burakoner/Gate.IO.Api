namespace Gate.IO.Api.Models.RestApi.Futures;

public  class FuturesStats
{
    /// <summary>
    /// Stat timestamp
    /// </summary>
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Long/short account number ratio
    /// </summary>
    [JsonProperty("lsr_taker")]
    public decimal LsrTaker { get; set; }

    /// <summary>
    /// Long/short taker size ratio
    /// </summary>
    [JsonProperty("lsr_account")]
    public decimal LsrAccount { get; set; }

    /// <summary>
    /// Long liquidation size
    /// </summary>
    [JsonProperty("long_liq_size")]
    public decimal LongLiqSize { get; set; }

    /// <summary>
    /// Short liquidation size
    /// </summary>
    [JsonProperty("short_liq_size")]
    public decimal ShortLiqSize { get; set; }

    /// <summary>
    /// Open interest size
    /// </summary>
    [JsonProperty("open_interest")]
    public decimal OpenInterest { get; set; }

    /// <summary>
    /// Short liquidation volume(quote currency)
    /// </summary>
    [JsonProperty("short_liq_usd")]
    public decimal ShortLiqUsd { get; set; }

    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Top trader long/short position ratio
    /// </summary>
    [JsonProperty("top_lsr_size")]
    public decimal TopLsrSize { get; set; }

    /// <summary>
    /// Short liquidation amount(base currency)
    /// </summary>
    [JsonProperty("short_liq_amount")]
    public decimal ShortLiqAmount { get; set; }

    /// <summary>
    /// Long liquidation amount(base currency)
    /// </summary>
    [JsonProperty("long_liq_amount")]
    public decimal LongLiqAmount { get; set; }

    /// <summary>
    /// Open interest volume(quote currency)
    /// </summary>
    [JsonProperty("open_interest_usd")]
    public decimal OpenInterestUsd { get; set; }

    /// <summary>
    /// Top trader long/short account ratio
    /// </summary>
    [JsonProperty("top_lsr_account")]
    public decimal TopLsrAccount { get; set; }

    /// <summary>
    /// Long liquidation volume(quote currency)
    /// </summary>
    [JsonProperty("long_liq_usd")]
    public decimal LongLiqUsd { get; set; }
}
