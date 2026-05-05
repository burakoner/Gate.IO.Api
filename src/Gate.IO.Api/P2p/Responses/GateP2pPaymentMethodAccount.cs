namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P payment method account
/// </summary>
public record GateP2pPaymentMethodAccount
{
    /// <summary>
    /// Payment account ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long? UserId { get; set; }

    /// <summary>
    /// Payment method record ID
    /// </summary>
    [JsonProperty("bankid")]
    public string BankId { get; set; }

    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickname")]
    public string Nickname { get; set; }

    /// <summary>
    /// Bank name
    /// </summary>
    [JsonProperty("bankname")]
    public string BankName { get; set; }

    /// <summary>
    /// Bank branch
    /// </summary>
    [JsonProperty("bankbranch")]
    public string BankBranch { get; set; }

    /// <summary>
    /// Bank city
    /// </summary>
    [JsonProperty("bankcity")]
    public string BankCity { get; set; }

    /// <summary>
    /// Bank province
    /// </summary>
    [JsonProperty("bankprov")]
    public string BankProvince { get; set; }

    /// <summary>
    /// Bank address or masked card number
    /// </summary>
    [JsonProperty("bankaddr")]
    public string BankAddress { get; set; }

    /// <summary>
    /// Bank note
    /// </summary>
    [JsonProperty("bankdesc")]
    public string BankDescription { get; set; }

    /// <summary>
    /// Cardholder UID
    /// </summary>
    [JsonProperty("hold_uid")]
    public long? HolderUserId { get; set; }

    /// <summary>
    /// Cardholder user name
    /// </summary>
    [JsonProperty("hold_username")]
    public string HolderUserName { get; set; }

    /// <summary>
    /// Real name
    /// </summary>
    [JsonProperty("real_name")]
    public string RealName { get; set; }

    /// <summary>
    /// Payment account description
    /// </summary>
    [JsonProperty("account_des")]
    public string AccountDescription { get; set; }

    /// <summary>
    /// Payment type
    /// </summary>
    [JsonProperty("pay_type")]
    public string PayType { get; set; }

    /// <summary>
    /// File link
    /// </summary>
    [JsonProperty("file")]
    public string File { get; set; }

    /// <summary>
    /// File key
    /// </summary>
    [JsonProperty("file_key")]
    public string FileKey { get; set; }

    /// <summary>
    /// Payment account
    /// </summary>
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Payment method code
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// Additional memo
    /// </summary>
    [JsonProperty("memo_ext")]
    public string MemoExt { get; set; }

    /// <summary>
    /// Trading tips
    /// </summary>
    [JsonProperty("trade_tips")]
    public string TradeTips { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
