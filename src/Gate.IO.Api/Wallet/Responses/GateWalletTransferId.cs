namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Push
/// </summary>
public record GateWalletTransferId
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
}
