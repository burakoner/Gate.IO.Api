namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx trading pair information
/// </summary>
public record GateCrossExSymbol
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("min_size")]
    public decimal MinimumSize { get; set; }

    [JsonProperty("min_notional")]
    public decimal MinimumNotional { get; set; }

    [JsonProperty("lot_size")]
    public decimal LotSize { get; set; }

    [JsonProperty("tick_size")]
    public decimal TickSize { get; set; }

    [JsonProperty("max_num_orders")]
    public long MaximumNumberOfOrders { get; set; }

    [JsonProperty("max_market_size")]
    public decimal MaximumMarketSize { get; set; }

    [JsonProperty("max_limit_size")]
    public decimal MaximumLimitSize { get; set; }

    [JsonProperty("contract_size")]
    public decimal ContractSize { get; set; }

    [JsonProperty("liquidation_fee")]
    public decimal LiquidationFee { get; set; }

    [JsonProperty("default_leverage")]
    public decimal? DefaultLeverage { get; set; }

    [JsonProperty("delist_time")]
    public long? DelistTime { get; set; }
}
