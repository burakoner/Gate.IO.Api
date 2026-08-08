namespace Gate.IO.Api.Wallet;

/// <summary>
/// Trading account transfer details
/// </summary>
public record GateWalletTradingAccountTransfer
{
    /// <summary>Gets or sets the transfer transaction ID.</summary>
    [JsonProperty("tx_id")]
    public string TransactionId { get; set; }

    /// <summary>Gets or sets the transfer status.</summary>
    [JsonProperty("status"), JsonConverter(typeof(MapConverter))]
    public GateWalletTradingAccountTransferStatus Status { get; set; }

    /// <summary>Gets or sets the transfer currency.</summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>Gets or sets the transfer amount.</summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>Gets or sets the source account type.</summary>
    [JsonProperty("from_account"), JsonConverter(typeof(MapConverter))]
    public GateWalletTradingAccountType FromAccount { get; set; }

    /// <summary>Gets or sets the destination account type.</summary>
    [JsonProperty("to_account"), JsonConverter(typeof(MapConverter))]
    public GateWalletTradingAccountType ToAccount { get; set; }

    /// <summary>Gets or sets the settlement currency for futures, delivery, and options transfers; otherwise null.</summary>
    [JsonProperty("settle")]
    public string SettlementCurrency { get; set; }

    /// <summary>Gets or sets the currency pair for margin transfers; otherwise null.</summary>
    [JsonProperty("currency_pair")]
    public string CurrencyPair { get; set; }
}
