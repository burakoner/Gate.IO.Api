namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order payment confirmation request
/// </summary>
[JsonConverter(typeof(GateOtcBankJsonConverter))]
public record GateOtcMarkOrderPaidRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string OrderId { get; set; }

    /// <summary>
    /// Client order ID used by some gateway paths
    /// </summary>
    [JsonProperty("client_order_id", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Required payment receipt file key, returned from pre-upload or the legacy production bucket, sent unchanged.
    /// The service/gateway validate jpg/jpeg/png/pdf and 10 MB. No file or automatic upload occurs here.
    /// </summary>
    [JsonProperty("payment_receipt_file_key", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string PaymentReceiptFileKey { get; set; }

    /// <summary>
    /// Gateway-compatible alias for the payment receipt file key
    /// </summary>
    [JsonProperty("payment_receipt", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string PaymentReceipt { get; set; }
}
