namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan status
/// </summary>
public enum GateEarnAutoInvestPlanStatus : byte
{
    /// <summary>
    /// Active plans
    /// </summary>
    [Map("active")]
    Active = 1,

    /// <summary>
    /// Historical plans
    /// </summary>
    [Map("history")]
    History = 2,
}
