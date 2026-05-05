namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha currency trading status.
/// </summary>
public enum GateAlphaCurrencyStatus
{
    /// <summary>
    /// Normal trading.
    /// </summary>
    NormalTrading = 1,

    /// <summary>
    /// Suspended trading.
    /// </summary>
    SuspendedTrading = 2,

    /// <summary>
    /// Delisted.
    /// </summary>
    Delisted = 3,
}
