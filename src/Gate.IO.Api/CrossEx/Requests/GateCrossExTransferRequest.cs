namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx fund transfer request
/// </summary>
public record GateCrossExTransferRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("coin", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Coin { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal Amount { get; set; }

    /// <summary>
    /// Source account
    /// </summary>
    [JsonProperty("from", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExInstructionEnumConverter))]
    public GateCrossExTransferAccountType From { get; set; }

    /// <summary>
    /// Destination account
    /// </summary>
    [JsonProperty("to", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExInstructionEnumConverter))]
    public GateCrossExTransferAccountType To { get; set; }

    /// <summary>
    /// Client-defined ID
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Text { get; set; }
}
