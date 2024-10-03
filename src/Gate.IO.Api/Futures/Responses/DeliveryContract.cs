namespace Gate.IO.Api.Futures;

public class DeliveryContract
{
    [JsonProperty("name")]
    public string Contract { get; set; }

    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    [JsonProperty("cycle")]
    public FuturesDeliveryCycle cycle { get; set; }

    [JsonProperty("type")]
    public GateFuturesContractType Type { get; set; }

    [JsonProperty("quanto_multiplier")]
    public decimal QuantoMultiplier { get; set; }

    [JsonProperty("leverage_min")]
    public int MinimumLeverage { get; set; }

    [JsonProperty("leverage_max")]
    public int MaximumLeverage { get; set; }

    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    [JsonProperty("mark_type")]
    public GateFuturesMarkType MarkType { get; set; }

    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }

    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }

    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    [JsonProperty("maker_fee_rate")]
    public decimal MakerFeeRate { get; set; }

    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }

    [JsonProperty("order_price_round")]
    public decimal OrderPriceRound { get; set; }

    [JsonProperty("mark_price_round")]
    public decimal MarkPriceRound { get; set; }

    /// <summary>
    /// Fair basis rate
    /// </summary>
    [JsonProperty("basis_rate")]
    public decimal BasisRate { get; set; }

    /// <summary>
    /// Fair basis value
    /// </summary>
    [JsonProperty("basis_value")]
    public decimal BasisValue { get; set; }

    /// <summary>
    /// Funding used for calculating impact bid, ask price
    /// </summary>
    [JsonProperty("basis_impact_value")]
    public decimal BasisImpactValue { get; set; }

    /// <summary>
    /// Settle price
    /// </summary>
    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    /// <summary>
    /// Settle price update interval
    /// </summary>
    [JsonProperty("settle_price_interval")]
    public int SettlePriceInterval { get; set; }

    /// <summary>
    /// Settle price update duration in seconds
    /// </summary>
    [JsonProperty("settle_price_duration")]
    public int SettlePriceDuration { get; set; }

    /// <summary>
    /// Contract expiry timestamp
    /// </summary>
    [JsonProperty("expire_time")]
    public DateTime ExpireTime { get; set; }

    [JsonProperty("risk_limit_base")]
    public decimal RiskLimitBase { get; set; }

    [JsonProperty("risk_limit_max")]
    public decimal RiskLimitMax { get; set; }

    [JsonProperty("risk_limit_step")]
    public decimal RiskLimitStep { get; set; }

    [JsonProperty("order_size_min")]
    public long OrderSizeMinimum { get; set; }

    [JsonProperty("order_size_max")]
    public long OrderSizeMaximum { get; set; }

    [JsonProperty("order_price_deviate")]
    public decimal OrderPriceDeviate { get; set; }

    [JsonProperty("ref_discount_rate")]
    public decimal RefDiscountRate { get; set; }

    [JsonProperty("ref_rebate_rate")]
    public decimal RefRebateRate { get; set; }

    [JsonProperty("orderbook_id")]
    public long OrderbookId { get; set; }

    [JsonProperty("trade_id")]
    public long TradeId { get; set; }

    [JsonProperty("trade_size")]
    public long TradeSize { get; set; }

    [JsonProperty("position_size")]
    public long PositionSize { get; set; }

    [JsonProperty("config_change_time")]
    public DateTime ConfigChangeTime { get; set; }

    [JsonProperty("in_delisting")]
    public bool InDelisting { get; set; }

    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }
}
