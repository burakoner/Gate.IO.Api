namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order request
/// </summary>
public record GateOtcFiatOrderRequest
{
    /// <summary>
    /// BUY for on-ramp or SELL for off-ramp
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcFiatInstructionConverter))]
    public GateOtcOrderType Type { get; set; }

    /// <summary>
    /// Quote side (PAY/GET), not quote order_type (FIAT/STABLE). Legacy FIAT/CRYPTO are also accepted.
    /// The existing C# default FIAT is retained; new integrations should explicitly copy the quote side.
    /// </summary>
    [JsonProperty("side", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcFiatInstructionConverter))]
    public GateOtcOrderKind Side { get; set; } = GateOtcOrderKind.Fiat;

    /// <summary>
    /// Cryptocurrency
    /// </summary>
    [JsonProperty("crypto_currency", Required = Required.Always)]
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("fiat_currency", Required = Required.Always)]
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Amount of cryptocurrency
    /// </summary>
    [JsonProperty("crypto_amount", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal CryptoAmount { get; set; }

    /// <summary>
    /// Fiat amount
    /// </summary>
    [JsonProperty("fiat_amount", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal FiatAmount { get; set; }

    /// <summary>
    /// Promotion code
    /// </summary>
    [JsonProperty("promotion_code", NullValueHandling = NullValueHandling.Ignore)]
    public string PromotionCode { get; set; }

    /// <summary>
    /// Quote token returned by the quote API
    /// </summary>
    [JsonProperty("quote_token", Required = Required.Always)]
    public string QuoteToken { get; set; }

    /// <summary>
    /// Bank card ID used for the order
    /// </summary>
    [JsonProperty("bank_id", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long BankId { get; set; }

    /// <summary>
    /// Optional remittance name: YOU/GATE/RECIPIENT for corporate users, GATE/PERSON for individuals.
    /// The client does not infer account type or choose a default. BankId remains an explicit bank selection.
    /// </summary>
    [JsonProperty("receive_type", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcFiatInstructionConverter))]
    public GateOtcReceiveType? ReceiveType { get; set; }
}
