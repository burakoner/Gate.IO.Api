namespace Gate.IO.Api.Unified;

/// <summary>
/// Borrow type
/// </summary>
public enum GateUnifiedBorrowType : byte
{
    /// <summary>
    /// Manual borrow
    /// </summary>
    [Map("manual_borrow")]
    ManualBorrow = 1,

    /// <summary>
    /// Auto borrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow = 2,
}
