namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin interest deduction record
/// </summary>
public record GateCrossExMarginInterestRecord
{
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("interest_id")]
    public long? InterestId { get; set; }

    [JsonProperty("liability_id")]
    public long? LiabilityId { get; set; }

    [JsonProperty("liability")]
    public decimal? Liability { get; set; }

    [JsonProperty("liability_coin")]
    public string LiabilityCoin { get; set; }

    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("interest_rate")]
    public decimal? InterestRate { get; set; }

    [JsonProperty("interest_type")]
    public string InterestType { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }
}
