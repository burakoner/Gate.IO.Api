namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin asset interest rate
/// </summary>
public record GateCrossExInterestRate
{
    /// <summary>
    /// Gets or sets the Coin.
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Hourly Interest Rate.
    /// </summary>
    [JsonProperty("hour_interest_rate")]
    public decimal HourlyInterestRate { get; set; }

    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Time { get; set; }
}
