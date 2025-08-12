namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesTicker
/// </summary>
public record GateFuturesTicker
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
    /// Trade size in recent 24h
    /// </summary>
    [JsonProperty("volume_24h")]
    public decimal Volume24h { get; set; }

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
    /// Index price
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }
    
    /// <summary>
    /// Exchange rate of base currency and settlement currency in Quanto contract. Does not exists in contracts of other types
    /// </summary>
    [JsonProperty("quanto_base_rate")]
    public decimal QuantoBaseRate { get; set; }
    
    /// <summary>
    /// Recent lowest ask
    /// </summary>
    [JsonProperty("lowest_ask")]
    public decimal? BestAskPrice { get; set; }
    
    /// <summary>
    /// The latest seller's lowest price order quantity
    /// </summary>
    [JsonProperty("lowest_size")]
    public decimal? BestAskSize { get; set; }
    
    /// <summary>
    /// Recent highest bid
    /// </summary>
    [JsonProperty("highest_bid")]
    public decimal? BestBidPrice { get; set; }
    
    /// <summary>
    /// The latest buyer's highest price order volume
    /// </summary>
    [JsonProperty("highest_size")]
    public decimal? BestBidSize { get; set; }

    /// <summary>
    /// utc0 涨跌百分比，跌用负数标识，如 -7.45
    /// </summary>
    [JsonProperty("change_utc0")]
    public decimal ChangeUtc0 { get; set; }

    /// <summary>
    /// utc8 涨跌百分比，跌用负数标识，如 -7.45
    /// </summary>
    [JsonProperty("change_utc8")]
    public decimal ChangeUtc8 { get; set; }

    /// <summary>
    /// 24h 涨跌额，跌用负数标识，如 -7.45
    /// </summary>
    [JsonProperty("change_price")]
    public decimal ChangePrice { get; set; }

    /// <summary>
    /// utc0 涨跌额，跌用负数标识，如 -7.45
    /// </summary>
    [JsonProperty("change_utc0_price")]
    public decimal ChangeUtc0Price { get; set; }

    /// <summary>
    /// utc8 涨跌额，跌用负数标识，如 -7.45
    /// </summary>
    [JsonProperty("change_utc8_price")]
    public decimal ChangeUtc8Price { get; set; }
}
