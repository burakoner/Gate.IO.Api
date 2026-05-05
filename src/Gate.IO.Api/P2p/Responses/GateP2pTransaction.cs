namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P transaction
/// </summary>
public record GateP2pTransaction
{
    /// <summary>
    /// Current user's side. 1: buy, 0: sell
    /// </summary>
    [JsonProperty("type_buy")]
    public int? TypeBuy { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("timest")]
    public DateTime? Time { get; set; }

    /// <summary>
    /// Expiration time
    /// </summary>
    [JsonProperty("timest_expire")]
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    /// <summary>
    /// Crypto amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Fiat total
    /// </summary>
    [JsonProperty("total")]
    public decimal? Total { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("txid")]
    public long? TransactionId { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Counterparty real name
    /// </summary>
    [JsonProperty("its_realname")]
    public string CounterpartyRealName { get; set; }

    /// <summary>
    /// Counterparty encrypted UID
    /// </summary>
    [JsonProperty("its_uid")]
    public string CounterpartyUserId { get; set; }

    /// <summary>
    /// Counterparty nickname
    /// </summary>
    [JsonProperty("its_nick")]
    public string CounterpartyNickname { get; set; }

    /// <summary>
    /// Seller real name
    /// </summary>
    [JsonProperty("seller_realname")]
    public string SellerRealName { get; set; }

    /// <summary>
    /// Buyer real name
    /// </summary>
    [JsonProperty("buyer_realname")]
    public string BuyerRealName { get; set; }

    /// <summary>
    /// Cancelable flag
    /// </summary>
    [JsonProperty("cancelable")]
    public int? Cancelable { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("currency_type")]
    public string CurrencyType { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("want_type")]
    public string WantType { get; set; }

    /// <summary>
    /// Hide payment flag
    /// </summary>
    [JsonProperty("hide_payment")]
    public int? HidePayment { get; set; }

    /// <summary>
    /// Selected payment type
    /// </summary>
    [JsonProperty("sel_paytype")]
    public string SelectedPaymentType { get; set; }

    /// <summary>
    /// Additional payment methods
    /// </summary>
    [JsonProperty("pay_others")]
    public List<GateP2pPaymentOption> OtherPaymentOptions { get; set; } = [];

    /// <summary>
    /// Countdown in seconds
    /// </summary>
    [JsonProperty("cd_time")]
    public long? CountdownTime { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("order_type")]
    public int? OrderType { get; set; }

    /// <summary>
    /// Order tags
    /// </summary>
    [JsonProperty("order_tag")]
    public List<string> OrderTags { get; set; } = [];

    /// <summary>
    /// Flash swap conversion information
    /// </summary>
    [JsonProperty("convert_info")]
    public GateP2pConvertInfo ConvertInfo { get; set; }

    /// <summary>
    /// Gets or sets the Additional Data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
