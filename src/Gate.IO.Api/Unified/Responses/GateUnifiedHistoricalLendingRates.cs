namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Historical Lending Rates
/// </summary>
public record GateUnifiedHistoricalLendingRates
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// VIP level for the floating rate to be retrieved
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }

    /// <summary>
    /// Floating rate corresponding to VIP level
    /// </summary>
    [JsonProperty("tier_up_rate")]
    public decimal TierUpRate { get; set; }

    /// <summary>
    /// Historical interest rate information, one data point per hour, array size determined by page and limit parameters from the API request, sorted by time from recent to distant
    /// </summary>
    [JsonProperty("rates")]
    public List<GateUnifiedHistoricalLendingRate> Rates { get; set; } = [];
}

/// <summary>
/// Gate Unified Historical Lending Rate
/// </summary>
public record GateUnifiedHistoricalLendingRate
{
    /// <summary>
    /// Hourly timestamp corresponding to this interest rate, in milliseconds
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Historical interest rate for this hour
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }
}