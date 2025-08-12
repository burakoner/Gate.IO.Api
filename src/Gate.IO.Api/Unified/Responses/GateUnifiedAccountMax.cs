namespace Gate.IO.Api.Unified;

/// <summary>
/// Max borrowable info
/// </summary>
public record GateUnifiedAccountMax
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("currency")]
    public string Asset { get; set; }

    /// <summary>
    /// Max borrowable
    /// </summary>
    [JsonProperty("amount")]
    public decimal MaxBorrowable { get; set; }
}
