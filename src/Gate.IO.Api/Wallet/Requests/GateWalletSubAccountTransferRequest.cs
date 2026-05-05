namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet main-sub account transfer request
/// </summary>
public record GateWalletSubAccountTransferRequest
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
    /// Transfer direction
    /// </summary>
    [JsonProperty("direction"), JsonConverter(typeof(MapConverter))]
    public GateWalletTransferDirection Direction { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Customer-defined ID to prevent duplicate transfers
    /// </summary>
    [JsonProperty("client_order_id", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Target sub-account trading account
    /// </summary>
    [JsonProperty("sub_account_type", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateWalletSubAccountType? SubAccountType { get; set; }
}
