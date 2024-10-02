namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Transfer Record
/// </summary>
public class GateWalletTransferRecord : GateWalletTransfer
{
    /// <summary>
    /// Main account user ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Transfer timestamp
    /// </summary>
    [JsonProperty("timest"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Where the operation is initiated from
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }
}