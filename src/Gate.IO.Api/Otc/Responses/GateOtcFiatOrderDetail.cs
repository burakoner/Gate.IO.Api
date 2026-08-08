namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order detail
/// </summary>
public record GateOtcFiatOrderDetail
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public string UserId { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public GateOtcOrderType Type { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("fiat_currency")]
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Fiat amount
    /// </summary>
    [JsonProperty("fiat_amount")]
    public decimal FiatAmount { get; set; }

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("crypto_currency")]
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Cryptocurrency amount
    /// </summary>
    [JsonProperty("crypto_amount")]
    public decimal CryptoAmount { get; set; }

    /// <summary>
    /// Exchange rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// User payment or receiving account name
    /// </summary>
    [JsonProperty("bank_account_name")]
    public string BankAccountName { get; set; }

    /// <summary>
    /// User payment or receiving bank name
    /// </summary>
    [JsonProperty("bank_name")]
    public string BankName { get; set; }

    /// <summary>
    /// User payment or receiving bank country
    /// </summary>
    [JsonProperty("bank_country")]
    public string BankCountry { get; set; }

    /// <summary>
    /// User payment or receiving bank address
    /// </summary>
    [JsonProperty("bank_address")]
    public string BankAddress { get; set; }

    /// <summary>
    /// User payment or receiving bank account number or IBAN
    /// </summary>
    [JsonProperty("bank_account_number_iban")]
    public string BankAccountNumberIban { get; set; }

    /// <summary>
    /// User payment or receiving bank SWIFT code
    /// </summary>
    [JsonProperty("swift_code")]
    public string SwiftCode { get; set; }

    /// <summary>
    /// User payment or receiving intermediary bank name
    /// </summary>
    [JsonProperty("intermediate_bank_name")]
    public string IntermediaryBankName { get; set; }

    /// <summary>
    /// User payment or receiving intermediary bank SWIFT code
    /// </summary>
    [JsonProperty("intermediary_bank_swift_code")]
    public string IntermediaryBankSwiftCode { get; set; }

    /// <summary>
    /// Gate beneficiary name, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_bank_account_name")]
    public string GateBankAccountName { get; set; }

    /// <summary>
    /// Gate beneficiary bank name, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_bank_name")]
    public string GateBankName { get; set; }

    /// <summary>
    /// Gate beneficiary bank country, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_bank_country")]
    public string GateBankCountry { get; set; }

    /// <summary>
    /// Gate beneficiary bank address, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_bank_address")]
    public string GateBankAddress { get; set; }

    /// <summary>
    /// Gate beneficiary bank account number or IBAN, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_bank_account_number_iban")]
    public string GateBankAccountNumberIban { get; set; }

    /// <summary>
    /// Gate beneficiary bank SWIFT code, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_swift_code")]
    public string GateSwiftCode { get; set; }

    /// <summary>
    /// Gate beneficiary intermediary bank name, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_intermediary_bank_name")]
    public string GateIntermediaryBankName { get; set; }

    /// <summary>
    /// Gate beneficiary intermediary bank SWIFT code, shown for buy orders only
    /// </summary>
    [JsonProperty("gate_intermediary_bank_swift_code")]
    public string GateIntermediaryBankSwiftCode { get; set; }

    /// <summary>
    /// Gate transfer remark for buy orders. Empty when <see cref="GateReferenceCode"/> is set.
    /// </summary>
    [JsonProperty("gate_transfer_remark")]
    public string GateTransferRemark { get; set; }

    /// <summary>
    /// Unique Gate bank transfer reference code. Mutually exclusive with <see cref="GateTransferRemark"/>.
    /// </summary>
    [JsonProperty("gate_reference_code")]
    public string GateReferenceCode { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Created time
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }
}
