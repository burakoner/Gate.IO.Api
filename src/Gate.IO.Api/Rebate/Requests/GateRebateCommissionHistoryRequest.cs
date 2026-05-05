namespace Gate.IO.Api.Rebate;

/// <summary>
/// Rebate commission history request
/// </summary>
public record GateRebateCommissionHistoryRequest
{
    /// <summary>
    /// Specify the currency. If not specified, returns all currencies
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Rebate type
    /// </summary>
    public GateRebateCommissionType? CommissionType { get; set; }

    /// <summary>
    /// User ID. If not specified, all user records will be returned
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// Start time for querying records
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp for the query
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// List offset, starting from 0
    /// </summary>
    public int? Offset { get; set; }
}
