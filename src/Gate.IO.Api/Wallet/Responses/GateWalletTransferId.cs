namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Push
/// </summary>
public record GateWalletTransferId
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
}
