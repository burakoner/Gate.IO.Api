namespace Gate.IO.Api.Margin;

/// <summary>
/// CrossMarginTransferable
/// </summary>
public  class GateCrossMarginAmount
{
    /// <summary>
    /// Currency detail
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
