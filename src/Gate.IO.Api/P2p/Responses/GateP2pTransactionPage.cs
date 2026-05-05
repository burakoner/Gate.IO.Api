namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P transaction page
/// </summary>
public record GateP2pTransactionPage
{
    /// <summary>
    /// Transactions
    /// </summary>
    [JsonProperty("list")]
    public List<GateP2pTransaction> List { get; set; } = [];

    /// <summary>
    /// Countdown markers
    /// </summary>
    [JsonProperty("trans_time")]
    public List<GateP2pTransactionTimeMarker> TransactionTimes { get; set; } = [];

    /// <summary>
    /// Total count
    /// </summary>
    [JsonProperty("count")]
    public int? Count { get; set; }

    /// <summary>
    /// Exported count
    /// </summary>
    [JsonProperty("exported_num")]
    public int? ExportedNumber { get; set; }
}
