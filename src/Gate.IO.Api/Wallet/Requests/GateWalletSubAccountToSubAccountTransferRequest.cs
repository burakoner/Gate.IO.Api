namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet sub-account to sub-account transfer request
/// </summary>
public record GateWalletSubAccountToSubAccountTransferRequest
{
    /// <summary>
    /// Transfer currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Transfer from the user id of the sub-account
    /// </summary>
    [JsonProperty("sub_account_from")]
    public long SenderSubAccountId { get; set; }

    /// <summary>
    /// Transfer from the account
    /// </summary>
    [JsonProperty("sub_account_from_type")]
    public GateWalletSubAccountType SenderSubAccountType { get; set; }

    /// <summary>
    /// Transfer to the user id of the sub-account
    /// </summary>
    [JsonProperty("sub_account_to")]
    public long RecipientSubAccountId { get; set; }

    /// <summary>
    /// Transfer to the account
    /// </summary>
    [JsonProperty("sub_account_to_type")]
    public GateWalletSubAccountType RecipientSubAccountType { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
