namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Multichain Address
/// </summary>
public class GateWalletMultichainAddress
{
    /// <summary>
    /// Name of the chain
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    /// Notes that some currencies required(e.g., Tag, Memo) when depositing
    /// </summary>
    [JsonProperty("payment_id")]
    public string PaymentId { get; set; }

    /// <summary>
    /// Note type, &#x60;Tag&#x60; or &#x60;Memo&#x60;
    /// </summary>
    [JsonProperty("payment_name")]
    public string PaymentName { get; set; }

    /// <summary>
    /// The obtain failed status- 0: address successfully obtained- 1: failed to obtain address
    /// </summary>
    [JsonProperty("obtain_failed")]
    public bool ObtainFailed { get; set; }
}
