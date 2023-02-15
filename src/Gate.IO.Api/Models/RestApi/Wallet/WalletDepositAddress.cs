namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletDepositAddress
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
    public IEnumerable<WalletMultichainAddress> MultichainAddresses { get; set; }=new List<WalletMultichainAddress>();
}
