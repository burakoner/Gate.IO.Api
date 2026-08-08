namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC order ID request
/// </summary>
public record GateOtcOrderIdRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public string OrderId { get; set; }
}
