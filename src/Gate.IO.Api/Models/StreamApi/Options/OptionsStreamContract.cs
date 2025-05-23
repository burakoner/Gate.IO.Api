namespace Gate.IO.Api.Models.StreamApi.Options;

/// <summary>
/// Represents an options contract stream object with detailed contract information.
/// </summary>
public record OptionsStreamContract
{
    /// <summary>
    /// The contract symbol or identifier.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// The creation time of the contract.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// The expiration time of the contract.
    /// </summary>
    [JsonProperty("expiration_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime ExpirationTime { get; set; }

    /// <summary>
    /// The initial margin requirement for high leverage.
    /// </summary>
    [JsonProperty("init_margin_high")]
    public decimal InitMarginHigh { get; set; }
    
    /// <summary>
    /// The initial margin requirement for low leverage.
    /// </summary>
    [JsonProperty("init_margin_low")]
    public decimal InitMarginLow { get; set; }

    /// <summary>
    /// Indicates whether the contract is a call option.
    /// </summary>
    [JsonProperty("is_call")]
    public bool IsCall { get; set; }

    /// <summary>
    /// The base maintenance margin requirement.
    /// </summary>
    [JsonProperty("maint_margin_base")]
    public decimal MaintenanceMarginBase { get; set; }
    
    /// <summary>
    /// The maker fee rate for the contract.
    /// </summary>
    [JsonProperty("maker_fee_rate")]
    public decimal MakerFeeRate { get; set; }

    /// <summary>
    /// The rounding value for the mark price.
    /// </summary>
    [JsonProperty("mark_price_round")]
    public decimal MarkPriceRound { get; set; }

    /// <summary>
    /// The minimum balance required for short positions.
    /// </summary>
    [JsonProperty("min_balance_short")]
    public decimal MinimumBalanceShort { get; set; }

    /// <summary>
    /// The minimum margin required for placing an order.
    /// </summary>
    [JsonProperty("min_order_margin")]
    public decimal MinimumOrderMargin { get; set; }

    /// <summary>
    /// The contract multiplier.
    /// </summary>
    [JsonProperty("multiplier")]
    public decimal Multiplier { get; set; }

    /// <summary>
    /// The allowed deviation for the order price.
    /// </summary>
    [JsonProperty("order_price_deviate")]
    public decimal OrderPriceDeviate { get; set; }

    /// <summary>
    /// The rounding value for the order price.
    /// </summary>
    [JsonProperty("order_price_round")]
    public decimal OrderPriceRound { get; set; }

    /// <summary>
    /// The maximum allowed order size.
    /// </summary>
    [JsonProperty("order_size_max")]
    public decimal MaximumOrderSize { get; set; }

    /// <summary>
    /// The minimum allowed order size.
    /// </summary>
    [JsonProperty("order_size_min")]
    public decimal MinimumOrderSize { get; set; }

    /// <summary>
    /// The maximum number of allowed open orders.
    /// </summary>
    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }

    /// <summary>
    /// The referral discount rate.
    /// </summary>
    [JsonProperty("ref_discount_rate")]
    public decimal RefDiscountRate { get; set; }

    /// <summary>
    /// The referral rebate rate.
    /// </summary>
    [JsonProperty("ref_rebate_rate")]
    public decimal RefRebateRate { get; set; }

    /// <summary>
    /// The strike price of the option contract.
    /// </summary>
    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

    /// <summary>
    /// The period tag of the contract (e.g., week, month).
    /// </summary>
    [JsonProperty("tag")]
    public GateOptionsContractPeriod Period { get; set; }

    /// <summary>
    /// The taker fee rate for the contract.
    /// </summary>
    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }

    /// <summary>
    /// The underlying asset symbol.
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// The timestamp of the contract data.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// The timestamp of the contract data in milliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}
