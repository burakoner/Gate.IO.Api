namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx trading pair information
/// </summary>
public record GateCrossExSymbol
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Business Type.
    /// </summary>
    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; }

    /// <summary>
    /// Gets or sets the Minimum Size.
    /// </summary>
    [JsonProperty("min_size")]
    public decimal MinimumSize { get; set; }

    /// <summary>
    /// Gets or sets the Minimum Notional.
    /// </summary>
    [JsonProperty("min_notional")]
    public decimal MinimumNotional { get; set; }

    /// <summary>
    /// Gets or sets the Lot Size.
    /// </summary>
    [JsonProperty("lot_size")]
    public decimal LotSize { get; set; }

    /// <summary>
    /// Gets or sets the Tick Size.
    /// </summary>
    [JsonProperty("tick_size")]
    public decimal TickSize { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Number Of Orders.
    /// </summary>
    [JsonProperty("max_num_orders")]
    public long MaximumNumberOfOrders { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Market Size.
    /// </summary>
    [JsonProperty("max_market_size")]
    public decimal MaximumMarketSize { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Limit Size.
    /// </summary>
    [JsonProperty("max_limit_size")]
    public decimal MaximumLimitSize { get; set; }

    /// <summary>
    /// Gets or sets the Contract Size.
    /// </summary>
    [JsonProperty("contract_size")]
    public decimal? ContractSize { get; set; }

    /// <summary>
    /// Gets or sets the Liquidation Fee.
    /// </summary>
    [JsonProperty("liquidation_fee")]
    public decimal LiquidationFee { get; set; }

    /// <summary>
    /// Gets or sets the Default Leverage.
    /// </summary>
    [JsonProperty("default_leverage")]
    public decimal? DefaultLeverage { get; set; }

    /// <summary>
    /// Gets or sets the Delist Time.
    /// </summary>
    [JsonProperty("delist_time")]
    public long? DelistTime { get; set; }
}
