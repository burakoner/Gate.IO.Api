﻿namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsContractPeriod
/// </summary>
public enum GateOptionsContractPeriod
{
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