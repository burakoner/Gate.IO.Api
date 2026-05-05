namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment order request
/// </summary>
public record GateEarnDualOrderRequest
{
    /// <summary>
    /// Product ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Subscription amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Custom order information
    /// </summary>
    public string Text { get; set; }
}
