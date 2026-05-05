namespace Gate.IO.Api.P2p;

/// <summary>
/// Confirm payment request
/// </summary>
public record GateP2pConfirmPaymentRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Payment type used for this payment
    /// </summary>
    public string PaymentMethod { get; set; }
}
