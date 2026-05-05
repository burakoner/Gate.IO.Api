namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment product
/// </summary>
public record GateEarnDualPlan
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    [JsonProperty("instrument_name")]
    public string InstrumentName { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnDualOptionType Type { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    [JsonProperty("invest_currency")]
    public string InvestCurrency { get; set; }

    /// <summary>
    /// Strike token
    /// </summary>
    [JsonProperty("exercise_currency")]
    public string ExerciseCurrency { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("exercise_price")]
    public decimal ExercisePrice { get; set; }

    /// <summary>
    /// Settlement time
    /// </summary>
    [JsonProperty("delivery_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Minimum share count
    /// </summary>
    [JsonProperty("min_copies")]
    public long MinCopies { get; set; }

    /// <summary>
    /// Maximum share count
    /// </summary>
    [JsonProperty("max_copies")]
    public long MaxCopies { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    [JsonProperty("start_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    [JsonProperty("end_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Product status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Annual yield
    /// </summary>
    [JsonProperty("apy_display")]
    public decimal ApyDisplay { get; set; }

    /// <summary>
    /// Value per unit
    /// </summary>
    [JsonProperty("per_value")]
    public decimal? PerValue { get; set; }
}

/// <summary>
/// Dual investment order
/// </summary>
public record GateEarnDualOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("plan_id")]
    public long PlanId { get; set; }

    /// <summary>
    /// Units
    /// </summary>
    [JsonProperty("copies")]
    public decimal Copies { get; set; }

    /// <summary>
    /// Investment quantity
    /// </summary>
    [JsonProperty("invest_amount")]
    public decimal InvestAmount { get; set; }

    /// <summary>
    /// Settlement quantity
    /// </summary>
    [JsonProperty("settlement_amount")]
    public decimal SettlementAmount { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Completed time
    /// </summary>
    [JsonProperty("complete_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CompleteTime { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    [JsonProperty("invest_currency")]
    public string InvestCurrency { get; set; }

    /// <summary>
    /// Strike token
    /// </summary>
    [JsonProperty("exercise_currency")]
    public string ExerciseCurrency { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("exercise_price")]
    public decimal ExercisePrice { get; set; }

    /// <summary>
    /// Settlement price
    /// </summary>
    [JsonProperty("settlement_price")]
    public decimal SettlementPrice { get; set; }

    /// <summary>
    /// Settlement time
    /// </summary>
    [JsonProperty("delivery_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Annual yield
    /// </summary>
    [JsonProperty("apy_display")]
    public decimal ApyDisplay { get; set; }

    /// <summary>
    /// Settlement annual yield
    /// </summary>
    [JsonProperty("apy_settlement")]
    public decimal ApySettlement { get; set; }

    /// <summary>
    /// Custom order information
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }
}

/// <summary>
/// Dual-currency earning assets
/// </summary>
public record GateEarnDualBalance
{
    /// <summary>
    /// User assets in USDT equivalent
    /// </summary>
    [JsonProperty("user_asset_usdt")]
    public decimal UserAssetUsdt { get; set; }

    /// <summary>
    /// User assets in BTC equivalent
    /// </summary>
    [JsonProperty("user_asset_btc")]
    public decimal UserAssetBtc { get; set; }

    /// <summary>
    /// Total user interest in USDT equivalent
    /// </summary>
    [JsonProperty("user_total_interest_usdt")]
    public decimal UserTotalInterestUsdt { get; set; }

    /// <summary>
    /// Total user interest in BTC equivalent
    /// </summary>
    [JsonProperty("user_total_interest_btc")]
    public decimal UserTotalInterestBtc { get; set; }
}

/// <summary>
/// Dual-currency early redemption preview
/// </summary>
public record GateEarnDualRefundPreview
{
    /// <summary>
    /// Order creation timestamp
    /// </summary>
    [JsonProperty("create_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Order delivery timestamp
    /// </summary>
    [JsonProperty("delivery_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("exercise_price")]
    public decimal ExercisePrice { get; set; }

    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("invest_amount")]
    public decimal InvestAmount { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    [JsonProperty("invest_currency")]
    public string InvestCurrency { get; set; }

    /// <summary>
    /// Order name identifier
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Request ID used for actual redemption
    /// </summary>
    [JsonProperty("req_id")]
    public string RequestId { get; set; }

    /// <summary>
    /// Refund fee
    /// </summary>
    [JsonProperty("refund_service_charge")]
    public decimal RefundServiceCharge { get; set; }

    /// <summary>
    /// Settlement price
    /// </summary>
    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    /// <summary>
    /// Settlement amount
    /// </summary>
    [JsonProperty("settlement_amount")]
    public decimal SettlementAmount { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }

    /// <summary>
    /// Settlement interest
    /// </summary>
    [JsonProperty("settlement_interest")]
    public decimal SettlementInterest { get; set; }

    /// <summary>
    /// Settlement principal
    /// </summary>
    [JsonProperty("settlement_principle")]
    public decimal SettlementPrincipal { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnDualOptionType Type { get; set; }

    /// <summary>
    /// Redemption time
    /// </summary>
    [JsonProperty("money_back_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime MoneyBackTime { get; set; }
}

/// <summary>
/// Dual-currency recommended project
/// </summary>
public record GateEarnDualRecommendation
{
    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Strategy category
    /// </summary>
    [JsonProperty("category")]
    public int Category { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnDualOptionType Type { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    [JsonProperty("invest_currency")]
    public string InvestCurrency { get; set; }

    /// <summary>
    /// Strike token
    /// </summary>
    [JsonProperty("exercise_currency")]
    public string ExerciseCurrency { get; set; }

    /// <summary>
    /// Annual yield
    /// </summary>
    [JsonProperty("apy_display")]
    public decimal ApyDisplay { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("exercise_price")]
    public decimal ExercisePrice { get; set; }

    /// <summary>
    /// Settlement time
    /// </summary>
    [JsonProperty("delivery_timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Minimum investment amount
    /// </summary>
    [JsonProperty("min_amount")]
    public decimal MinAmount { get; set; }

    /// <summary>
    /// Maximum investment amount
    /// </summary>
    [JsonProperty("max_amount")]
    public decimal MaxAmount { get; set; }

    /// <summary>
    /// Minimum units
    /// </summary>
    [JsonProperty("min_copies")]
    public long MinCopies { get; set; }

    /// <summary>
    /// Maximum units
    /// </summary>
    [JsonProperty("max_copies")]
    public long MaxCopies { get; set; }

    /// <summary>
    /// Lock-up days
    /// </summary>
    [JsonProperty("invest_days")]
    public long InvestDays { get; set; }

    /// <summary>
    /// Lock-up hours
    /// </summary>
    [JsonProperty("invest_hours")]
    public decimal InvestHours { get; set; }
}
