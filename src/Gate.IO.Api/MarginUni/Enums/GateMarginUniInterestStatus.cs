namespace Gate.IO.Api.MarginUni;

/// <summary>
/// Margin Uni Interest Status
/// </summary>
public enum GateMarginUniInterestStatus : byte
{
    /// <summary>
    /// Fail
    /// </summary>
    [Map("0")]
    Fail = 0,

    /// <summary>
    /// Success
    /// </summary>
    [Map("1")]
    Success = 1,
}