namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsContractPeriod
/// </summary>
public enum GateOptionsContractPeriod : byte
{
    /// <summary>
    /// One Day
    /// </summary>
    [Map("day", "DAY")]
    OneDay = 1,

    /// <summary>
    /// One Week
    /// </summary>
    [Map("week", "WEEK")]
    OneWeek = 7,

    /// <summary>
    /// One Month
    /// </summary>
    [Map("month", "MONTH")]
    OneMonth = 30,
}
