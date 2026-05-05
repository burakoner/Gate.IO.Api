namespace Gate.IO.Api.Earn;

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
