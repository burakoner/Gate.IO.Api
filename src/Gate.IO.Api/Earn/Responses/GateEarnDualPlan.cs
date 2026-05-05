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
