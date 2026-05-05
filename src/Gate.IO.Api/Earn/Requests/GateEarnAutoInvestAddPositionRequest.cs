namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest add position request
/// </summary>
public record GateEarnAutoInvestAddPositionRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    public decimal Amount { get; set; }
}
