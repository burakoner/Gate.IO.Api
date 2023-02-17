namespace Gate.IO.Api.Enums;

public enum FuturesPositionMode
{
    /// <summary>
    /// Enum Single for value: single
    /// </summary>
    [EnumMember(Value = "single")]
    Single,

    /// <summary>
    /// Enum Duallong for value: dual_long
    /// </summary>
    [EnumMember(Value = "dual_long")]
    DualLong,

    /// <summary>
    /// Enum Dualshort for value: dual_short
    /// </summary>
    [EnumMember(Value = "dual_short")]
    DualShort,
}
