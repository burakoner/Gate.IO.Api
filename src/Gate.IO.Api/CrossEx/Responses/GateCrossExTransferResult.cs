namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx transfer result
/// </summary>
public record GateCrossExTransferResult
{
    /// <summary>
    /// Gets or sets the Transaction ID.
    /// </summary>
    [JsonProperty("tx_id", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string TransactionId { get; set; }

    /// <summary>
    /// Gets or sets the Text.
    /// </summary>
    [JsonProperty("text", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Text { get; set; }
}
