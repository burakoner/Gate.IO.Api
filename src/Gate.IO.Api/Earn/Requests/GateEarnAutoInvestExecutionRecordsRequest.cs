namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan execution records request
/// </summary>
public record GateEarnAutoInvestExecutionRecordsRequest
{
    /// <summary>
    /// Plan ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public long? Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    public long? PageSize { get; set; }
}
