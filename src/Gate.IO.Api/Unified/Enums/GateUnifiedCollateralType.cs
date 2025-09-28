namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Collateral Type
/// </summary>
public enum GateUnifiedCollateralType : byte
{
    /// <summary>
    /// All
    /// </summary>
    [Map("0")]
    All = 0,

    /// <summary>
    /// Custom
    /// </summary>
    [Map("1")]
    Custom = 1
}
