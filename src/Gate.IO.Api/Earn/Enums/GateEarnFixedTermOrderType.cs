namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn order type
/// </summary>
public enum GateEarnFixedTermOrderType : byte
{
    /// <summary>
    /// Current orders
    /// </summary>
    Current = 1,

    /// <summary>
    /// Historical orders
    /// </summary>
    Historical = 2,
}
