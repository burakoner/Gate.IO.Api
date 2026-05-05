namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx fund transfer request
/// </summary>
public record GateCrossExTransferRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Source account
    /// </summary>
    public GateCrossExTransferAccountType From { get; set; }

    /// <summary>
    /// Destination account
    /// </summary>
    public GateCrossExTransferAccountType To { get; set; }

    /// <summary>
    /// Client-defined ID
    /// </summary>
    public string Text { get; set; }
}
