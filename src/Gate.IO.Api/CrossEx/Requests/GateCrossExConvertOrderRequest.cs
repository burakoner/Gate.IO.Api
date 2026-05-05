namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx flash swap order request
/// </summary>
public record GateCrossExConvertOrderRequest
{
    /// <summary>
    /// Quote ID
    /// </summary>
    public string QuoteId { get; set; }
}
