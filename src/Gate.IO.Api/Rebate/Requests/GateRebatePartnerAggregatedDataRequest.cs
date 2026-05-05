namespace Gate.IO.Api.Rebate;

/// <summary>
/// Partner aggregated data request
/// </summary>
public record GateRebatePartnerAggregatedDataRequest
{
    /// <summary>
    /// Query start time, format: yyyy-mm-dd hh:ii:ss (UTC+8)
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// Query end time, format: yyyy-mm-dd hh:ii:ss (UTC+8)
    /// </summary>
    public string EndDate { get; set; }

    /// <summary>
    /// Business type filter
    /// </summary>
    public GateRebateBusinessType? BusinessType { get; set; }
}
