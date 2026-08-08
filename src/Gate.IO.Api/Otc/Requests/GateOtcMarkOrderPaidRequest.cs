namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order payment confirmation request
/// </summary>
public record GateOtcMarkOrderPaidRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// Client order ID used by some gateway paths
    /// </summary>
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Required payment receipt file key. Supported formats are jpg, jpeg, png, and pdf; maximum file size is 10 MB.
    /// </summary>
    public string PaymentReceiptFileKey { get; set; }

    /// <summary>
    /// Gateway-compatible alias for the payment receipt file key
    /// </summary>
    public string PaymentReceipt { get; set; }
}
