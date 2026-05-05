namespace Gate.IO.Api.P2p;

/// <summary>
/// Payment method list request
/// </summary>
public record GateP2pPaymentMethodsRequest
{
    /// <summary>
    /// Fiat currency
    /// </summary>
    public string Fiat { get; set; }
}
