namespace Gate.IO.Api.P2p;

/// <summary>
/// Set P2P merchant work mode and custom working hours request
/// </summary>
public record GateP2pMerchantWorkHoursRequest
{
    /// <summary>
    /// Work mode
    /// </summary>
    public GateP2pMerchantWorkMode WorkStatus { get; set; }

    /// <summary>
    /// Custom working cycle. Required in custom-hours mode.
    /// </summary>
    public GateP2pMerchantWorkCycle? CycleType { get; set; }

    /// <summary>
    /// Weekly working days as comma-separated values from 1 (Monday) through 7 (Sunday)
    /// </summary>
    public string DayOfWeek { get; set; }

    /// <summary>
    /// UTC timezone offset from -12 through +14
    /// </summary>
    public string TimeZone { get; set; }

    /// <summary>
    /// Custom working start time in HH:mm format
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// Custom working end time in HH:mm format
    /// </summary>
    public string EndTime { get; set; }
}
