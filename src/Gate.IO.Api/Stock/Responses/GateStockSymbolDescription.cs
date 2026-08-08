namespace Gate.IO.Api.Stock;

/// <summary>
/// Localized stock symbol description
/// </summary>
public record GateStockSymbolDescription
{
    /// <summary>Gets or sets the language code.</summary>
    [JsonProperty("lang")]
    public string Language { get; set; }
    /// <summary>Gets or sets the localized description.</summary>
    [JsonProperty("value")]
    public string Value { get; set; }
}
