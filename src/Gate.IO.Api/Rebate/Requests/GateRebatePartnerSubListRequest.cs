namespace Gate.IO.Api.Rebate;

/// <summary>
/// Partner subordinate list request
/// </summary>
public record GateRebatePartnerSubListRequest
{
    /// <summary>
    /// User ID. If not specified, all user records will be returned
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// List offset, starting from 0
    /// </summary>
    public int? Offset { get; set; }
}
