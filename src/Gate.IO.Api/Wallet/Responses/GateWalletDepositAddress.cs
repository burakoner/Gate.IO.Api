namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Deposit Address
/// </summary>
public record GateWalletDepositAddress
{
    /// <summary>
    /// Currency detail
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    /// Gets or Sets MultichainAddresses
    /// </summary>
    [JsonProperty("multichain_addresses")]
    public List<GateWalletMultichainAddress> MultichainAddresses { get; set; } = [];
}
