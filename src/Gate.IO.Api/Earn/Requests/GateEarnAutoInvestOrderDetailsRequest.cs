namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest order details request
/// </summary>
public record GateEarnAutoInvestOrderDetailsRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Record ID
    /// </summary>
    public long RecordId { get; set; }
}
