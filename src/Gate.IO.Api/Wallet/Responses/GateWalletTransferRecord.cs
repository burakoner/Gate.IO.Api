namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Transfer Record
/// </summary>
public record GateWalletTransferRecord
{
    /// <summary>
    /// Transfer timestamp
    /// </summary>
    [JsonProperty("timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Main account user ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Sub account user ID
    /// </summary>
    [JsonProperty("sub_account")]
    public long SubAccountId { get; set; }

    /// <summary>
    /// Target sub user's account
    /// </summary>
    [JsonProperty("sub_account_type")]
    public GateWalletSubAccountType SubAccountType { get; set; }

    /// <summary>
    /// Transfer currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Transfer direction. to - transfer into sub account; from - transfer out from sub account
    /// </summary>
    [JsonProperty("direction")]
    public GateWalletTransferDirection Direction { get; set; }

    /// <summary>
    /// Where the operation is initiated from
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

    /// <summary>
    /// The custom ID provided by the customer serves as a safeguard against duplicate transfers. It can be a combination of letters (case-sensitive), numbers, hyphens '-', and underscores '_', with a length ranging from 1 to 64 characters.
    /// </summary>
    [JsonProperty("client_order_id")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Sub-account transfer record status, currently only 'success'
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
}