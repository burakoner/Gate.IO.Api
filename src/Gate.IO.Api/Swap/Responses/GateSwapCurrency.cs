namespace Gate.IO.Api.Swap;

/// <summary>
/// Gate Swap Currency
/// </summary>
public record GateSwapCurrency
{
    /// <summary>
    /// Currency Symbol
    /// </summary>
    [JsonProperty("currency")]
    public string Symbol { get; set; }

    /// <summary>
    /// Minimum amount required in flash swap
    /// </summary>
    [JsonProperty("min_amount")]
    public decimal MinimumAmount { get; set; }

    /// <summary>
    /// Maximum amount allowed in flash swap
    /// </summary>
    [JsonProperty("max_amount")]
    public decimal MaximumAmount { get; set; }

    /// <summary>
    /// Currencies which can be swapped to from this currency
    /// </summary>
    [JsonProperty("swappable")]
    public List<string> SwappableCurrencies { get; set; } = [];
}
