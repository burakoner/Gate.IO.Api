namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Transfer
/// </summary>
public class GateWalletTransfer
{
    /// <summary>
    /// Transfer currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Sub account user ID
    /// </summary>
    [JsonProperty("sub_account")]
    public long SubAccountId { get; set; }

    /// <summary>
    /// Transfer direction. to - transfer into sub account; from - transfer out from sub account
    /// </summary>
    [JsonProperty("direction")]
    public GateWalletTransferDirection Direction { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Target sub user's account
    /// </summary>
    [JsonProperty("sub_account_type")]
    public GateWalletSubAccountType SubAccountType { get; set; }
}