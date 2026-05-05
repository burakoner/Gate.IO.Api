namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha quote failure type.
/// </summary>
public enum GateAlphaQuoteErrorType
{
    /// <summary>
    /// Quote succeeded.
    /// </summary>
    Success = 0,

    /// <summary>
    /// Requested amount exceeds the maximum value.
    /// </summary>
    ExceedsMaximumValue = 1,

    /// <summary>
    /// Requested amount is below the minimum value.
    /// </summary>
    BelowMinimumValue = 2,
}
