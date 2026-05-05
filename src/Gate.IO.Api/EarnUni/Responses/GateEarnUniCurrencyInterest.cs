namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni currency interest compounding status
/// </summary>
public record GateEarnUniCurrencyInterest
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Interest status
    /// </summary>
    [JsonProperty("interest_status")]
    [JsonConverter(typeof(MapConverter))]
    public GateEarnUniInterestStatus InterestStatus { get; set; }
}
