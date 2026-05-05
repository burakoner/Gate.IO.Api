namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha token information.
/// </summary>
public record GateAlphaToken
{
    /// <summary>
    /// Currency symbol.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Currency name.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Main chain corresponding to the token.
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// Contract address.
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    /// Amount scale.
    /// </summary>
    [JsonProperty("amount_precision")]
    public int AmountPrecision { get; set; }

    /// <summary>
    /// Price scale.
    /// </summary>
    [JsonProperty("precision")]
    public int Precision { get; set; }

    /// <summary>
    /// Currency trading status.
    /// </summary>
    [JsonProperty("status")]
    public GateAlphaCurrencyStatus Status { get; set; }
}
