namespace Gate.IO.Api.Rebate;

/// <summary>
/// Rebate commission type
/// </summary>
public enum GateRebateCommissionType : byte
{
    /// <summary>
    /// Direct rebate
    /// </summary>
    Direct = 1,

    /// <summary>
    /// Indirect rebate
    /// </summary>
    Indirect = 2,

    /// <summary>
    /// Self rebate
    /// </summary>
    Self = 3,
}
