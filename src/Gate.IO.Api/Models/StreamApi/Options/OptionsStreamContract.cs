namespace Gate.IO.Api.Models.StreamApi.Options;

public record OptionsStreamContract
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("expiration_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime ExpirationTime { get; set; }

    [JsonProperty("init_margin_high")]
    public decimal InitMarginHigh { get; set; }
    
    [JsonProperty("init_margin_low")]
    public decimal InitMarginLow { get; set; }

    [JsonProperty("is_call")]
    public bool IsCall { get; set; }

    [JsonProperty("maint_margin_base")]
    public decimal MaintenanceMarginBase { get; set; }
    
    [JsonProperty("maker_fee_rate")]
    public decimal MakerFeeRate { get; set; }

    [JsonProperty("mark_price_round")]
    public decimal MarkPriceRound { get; set; }

    [JsonProperty("min_balance_short")]
    public decimal MinimumBalanceShort { get; set; }

    [JsonProperty("min_order_margin")]
    public decimal MinimumOrderMargin { get; set; }

    [JsonProperty("multiplier")]
    public decimal Multiplier { get; set; }

    [JsonProperty("order_price_deviate")]
    public decimal OrderPriceDeviate { get; set; }

    [JsonProperty("order_price_round")]
    public decimal OrderPriceRound { get; set; }

    [JsonProperty("order_size_max")]
    public decimal MaximumOrderSize { get; set; }

    [JsonProperty("order_size_min")]
    public decimal MinimumOrderSize { get; set; }

    [JsonProperty("orders_limit")]
    public int OrdersLimit { get; set; }

    [JsonProperty("ref_discount_rate")]
    public decimal RefDiscountRate { get; set; }

    [JsonProperty("ref_rebate_rate")]
    public decimal RefRebateRate { get; set; }

    [JsonProperty("strike_price")]
    public decimal StrikePrice { get; set; }

    [JsonProperty("tag")]
    public GateOptionsContractPeriod Period { get; set; }

    [JsonProperty("taker_fee_rate")]
    public decimal TakerFeeRate { get; set; }

    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}
