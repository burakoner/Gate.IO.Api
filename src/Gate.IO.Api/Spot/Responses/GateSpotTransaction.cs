namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Account Transaction
/// </summary>
public record GateSpotTransaction
{
    /// <summary>
    /// Balance change record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// The timestamp of the change (in milliseconds)
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Currency changed
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount changed. Positive value means transferring in, while negative out
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// Balance after change
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Account book type. Please refer to account book type for more detail
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Account change code, see [Asset Record Code] (Asset Record Code)
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// Additional information
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }
}
