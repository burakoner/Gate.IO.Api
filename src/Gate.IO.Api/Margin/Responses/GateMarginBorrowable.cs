namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Uni Borrowable
/// </summary>
public record GateMarginBorrowable
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Borrowable
    /// </summary>
    [JsonProperty("borrowable")]
    public decimal Borrowable { get; set; }
}
