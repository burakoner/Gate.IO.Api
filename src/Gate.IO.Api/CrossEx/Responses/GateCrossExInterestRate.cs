namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin asset interest rate
/// </summary>
public record GateCrossExInterestRate
{
    [JsonProperty("coin")]
    public string Coin { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("hour_interest_rate")]
    public decimal HourlyInterestRate { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Time { get; set; }
}
