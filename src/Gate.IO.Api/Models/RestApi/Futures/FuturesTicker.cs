namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesTicker
{
    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Last trading price
    /// </summary>
    [JsonProperty("last")]
    public decimal Last { get; set; }

    /// <summary>
    /// Lowest trading price in recent 24h
    /// </summary>
    [JsonProperty("low_24h")]
    public decimal Low24h { get; set; }

    /// <summary>
    /// Highest trading price in recent 24h
    /// </summary>
    [JsonProperty("high_24h")]
    public decimal High24h { get; set; }

    /// <summary>
    /// Change percentage.
    /// </summary>
    [JsonProperty("change_percentage")]
    public decimal ChangePercentage { get; set; }

    /// <summary>
    /// Contract total size
    /// </summary>
    [JsonProperty("total_size")]
    public decimal TotalSize { get; set; }

    /// <summary>
    /// Trade size in recent 24h
    /// </summary>
    [JsonProperty("volume_24h")]
    public decimal Volume24h { get; set; }

    // <summary>
    // Trade volumes in recent 24h in BTC(deprecated, use &#x60;volume_24h_base&#x60;, &#x60;volume_24h_quote&#x60;, &#x60;volume_24h_settle&#x60; instead)
    // </summary>
    //[JsonProperty("volume_24h_btc")]
    //public decimal Volume24hBtc { get; set; }

    // <summary>
    // Trade volumes in recent 24h in USD(deprecated, use &#x60;volume_24h_base&#x60;, &#x60;volume_24h_quote&#x60;, &#x60;volume_24h_settle&#x60; instead)
    // </summary>
    //[JsonProperty("volume_24h_usd")]
    //public decimal Volume24hUsd { get; set; }

    /// <summary>
    /// Trade volume in recent 24h, in base currency
    /// </summary>
    [JsonProperty("volume_24h_base")]
    public decimal Volume24hBase { get; set; }

    /// <summary>
    /// Trade volume in recent 24h, in quote currency
    /// </summary>
    [JsonProperty("volume_24h_quote")]
    public decimal Volume24hQuote { get; set; }

    /// <summary>
    /// Trade volume in recent 24h, in settle currency
    /// </summary>
    [JsonProperty("volume_24h_settle")]
    public decimal Volume24hSettle { get; set; }

    /// <summary>
    /// Recent mark price
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Funding rate
    /// </summary>
    [JsonProperty("funding_rate")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// Indicative Funding rate in next period
    /// </summary>
    [JsonProperty("funding_rate_indicative")]
    public decimal FundingRateIndicative { get; set; }

    /// <summary>
    /// Index price
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }
}
