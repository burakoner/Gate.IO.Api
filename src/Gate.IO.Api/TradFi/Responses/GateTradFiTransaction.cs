namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction
/// </summary>
public record GateTradFiTransaction
{
    /// <summary>
    /// Gets or sets the Asset.
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTransactionType Type { get; set; }

    /// <summary>
    /// Gets or sets the Type Description.
    /// </summary>
    [JsonProperty("type_desc")]
    public string TypeDescription { get; set; }

    /// <summary>
    /// Gets or sets the Change.
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// Gets or sets the Balance.
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Gets or sets the Time.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
