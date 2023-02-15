namespace Gate.IO.Api.Enums;

/// <summary>
/// Futures contract type
/// </summary>
public enum FuturesContractType
{
    /// <summary>
    /// Enum Inverse for value: inverse
    /// </summary>
    [EnumMember(Value = "inverse")]
    Inverse = 1,

    /// <summary>
    /// Enum Direct for value: direct
    /// </summary>
    [EnumMember(Value = "direct")]
    Direct = 2
}