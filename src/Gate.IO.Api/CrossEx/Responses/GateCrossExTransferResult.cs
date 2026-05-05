namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx transfer result
/// </summary>
public record GateCrossExTransferResult
{
    [JsonProperty("tx_id")]
    public long TransactionId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
