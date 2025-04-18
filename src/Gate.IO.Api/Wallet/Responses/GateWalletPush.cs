namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Push
/// </summary>
public record GateWalletPush
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }
}
