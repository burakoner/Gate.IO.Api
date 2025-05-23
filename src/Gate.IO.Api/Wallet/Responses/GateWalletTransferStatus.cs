namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Transfer Status
/// </summary>
public record GateWalletTransferStatus
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("tx_id")]
    public long TransactionId { get; set; }

    /// <summary>
    /// Transfer status, PENDING - in process, SUCCESS - successful transfer, FAIL - failed transfer, PARTIAL_SUCCESS - Partially successful (this status will appear when transferring between sub-subs)
    /// </summary>
    [JsonProperty("status")]
    public GateWalletTransferState Status { get; set; }
}
