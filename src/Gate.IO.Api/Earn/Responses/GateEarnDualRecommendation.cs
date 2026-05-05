namespace Gate.IO.Api.Earn;

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
