namespace Gate.IO.Api.Models.RestApi.Futures;

public class PerpetualContract
{
    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("name")]
    public string Contract { get; set; }

    /// <summary>
    /// Futures contract type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(FuturesContractTypeConverter))]
    public FuturesContractType Type { get; set; }

    /// <summary>
    /// Multiplier used in converting from invoicing to settlement currency
    /// </summary>
    [JsonProperty("quanto_multiplier")]
    public decimal QuantoMultiplier { get; set; }

    /// <summary>
    /// Referral fee rate discount
    /// </summary>
    [JsonProperty("ref_discount_rate")]
    public decimal RefDiscountRate { get; set; }

    /// <summary>
    /// deviation between order price and current index price. If price of an order is denoted as order_price, it must meet the following condition:      abs(order_price - mark_price) &lt;&#x3D; mark_price * order_price_deviate
    /// </summary>
    [JsonProperty("order_price_deviate")]
    public decimal OrderPriceDeviate { get; set; }

    /// <summary>
    /// Mark price type, internal - based on internal trading, index - based on external index price
    /// </summary>
    [JsonProperty("mark_type"), JsonConverter(typeof(FuturesMarkTypeConverter))]
    public FuturesMarkType MarkType { get; set; }

    /// <summary>
    /// Last trading price
    /// </summary>
    [JsonProperty("last_price")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Current mark price
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Current index price
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// Funding Rate Indicative
    /// </summary>
    [JsonProperty("funding_rate_indicative")]
    public decimal FundingRateIndicative { get; set; }

    /// <summary>
    /// Minimum mark price increment
    /// </summary>
    [JsonProperty("mark_price_round")]
    public decimal MarkPriceRound { get; set; }
    
    /// <summary>
    /// Minimum mark price increment
    /// </summary>
    [JsonProperty("funding_offset")]
    public decimal FundingOffset { get; set; }

    /// <summary>
    /// Contract is delisting
    /// </summary>
    [JsonProperty("in_delisting")]
    public bool InDelisting { get; set; }

    /// <summary>
    /// Interest Rate
    /// </summary>
    [JsonProperty("interest_rate")]
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Minimum order price increment
    /// </summary>
    /// <value>Minimum order price increment</value>
    [JsonProperty("order_price_round")]
    public decimal OrderPriceRound { get; set; }

    /// <summary>
    /// Minimum order size the contract allowed
    /// </summary>
    [JsonProperty("order_size_min")]
    public long OrderSizeMinimum { get; set; }

    /// <summary>
    /// Maximum order size the contract allowed
    /// </summary>
    [JsonProperty("order_size_max")]
    public long OrderSizeMaximum { get; set; }

    /// <summary>
    /// Referrer commission rate
    /// </summary>
    [JsonProperty("ref_rebate_rate")]
    public decimal RefRebateRate { get; set; }

    /// <summary>
    /// Funding application interval, unit in seconds
    /// </summary>
    [JsonProperty("funding_interval")]
    public int FundingInterval { get; set; }

    /// <summary>
    /// Risk limit base
    /// </summary>
    [JsonProperty("risk_limit_base")]
    public decimal RiskLimitBase { get; set; }

    /// <summary>
    /// Maximum risk limit the contract allowed
    /// </summary>
    [JsonProperty("risk_limit_max")]
    public decimal RiskLimitMax { get; set; }

    /// <summary>
    /// Step of adjusting risk limit
    /// </summary>
    [JsonProperty("risk_limit_step")]
    public decimal RiskLimitStep { get; set; }

    /// <summary>
    /// Minimum leverage
    /// </summary>
    [JsonProperty("leverage_min")]
    public int MinimumLeverage { get; set; }

    /// <summary>
    /// Maximum leverage
    /// </summary>
    [JsonProperty("leverage_max")]
    public int MaximumLeverage { get; set; }

    /// <summary>
    /// Maker fee rate, where negative means rebate
    /// </summary>
    [JsonProperty("maker_fee_rate")]
    public decimal MakerFeeRate { get; set; }

    /// <summary>
    /// Taker fee rate
    /// </summary>
    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }

    /// <summary>
    /// Current funding rate
    /// </summary>
    [JsonProperty("funding_rate")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// Next funding time
    /// </summary>
    [JsonProperty("funding_next_apply"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FundingNextApply { get; set; }
    
    /// <summary>
    /// Next funding time
    /// </summary>
    [JsonProperty("config_change_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime ConfigChangeTime { get; set; }

    /// <summary>
    /// Short Users
    /// </summary>
    [JsonProperty("short_users")]
    public int ShortUsers { get; set; }
    
    /// <summary>
    /// Long Users
    /// </summary>
    [JsonProperty("long_users")]
    public int LongUsers { get; set; }

    /// <summary>
    /// Historical accumulated trade size
    /// </summary>
    [JsonProperty("trade_size")]
    public long TradeSize { get; set; }

    /// <summary>
    /// Current total long position size
    /// </summary>
    [JsonProperty("position_size")]
    public long PositionSize { get; set; }

    /// <summary>
    /// Funding Impact Value
    /// </summary>
    [JsonProperty("funding_impact_value")]
    public decimal FundingImpactValue { get; set; }

    /// <summary>
    /// Maximum number of open orders
    /// </summary>
    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }

    /// <summary>
    /// Current trade ID
    /// </summary>
    [JsonProperty("trade_id")]
    public long TradeId { get; set; }

    /// <summary>
    /// Current orderbook ID
    /// </summary>
    [JsonProperty("orderbook_id")]
    public long OrderbookId { get; set; }

    /// <summary>
    /// Maintenance rate of margin
    /// </summary>
    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    [JsonProperty("enable_bonus")]
    public bool IsEnableBbonus { get; set; }
    
    [JsonProperty("enable_credit")]
    public bool IsEnableCredit { get; set; }
}
