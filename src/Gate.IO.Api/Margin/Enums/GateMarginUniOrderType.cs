namespace Gate.IO.Api.Margin;

/// <summary>
/// Margin Uni Order Type
/// </summary>
public enum GateMarginUniOrderType : byte
{
    /// <summary>
    /// Borrow
    /// </summary>
    [Map("borrow")]
    Borrow = 1,

    /// <summary>
    /// Repay
    /// </summary>
    [Map("repay")]
    Repay = 2,
}