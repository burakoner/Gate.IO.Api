namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha account asset transaction history record.
/// </summary>
public record GateAlphaAccountBookRecord
{
    /// <summary>
    /// Record ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Asset change amount.
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// Balance after the change.
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Transaction timestamp.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Currency name.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }
}
