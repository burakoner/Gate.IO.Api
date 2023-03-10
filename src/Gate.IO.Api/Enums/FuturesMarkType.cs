namespace Gate.IO.Api.Enums;

/// <summary>
/// Mark price type, internal - based on internal trading, index - based on external index price
/// </summary>
public enum FuturesMarkType
{
    /// <summary>
    /// Enum Internal for value: internal
    /// </summary>
    [EnumMember(Value = "internal")]
    Internal = 1,

    /// <summary>
    /// Enum Index for value: index
    /// </summary>
    [EnumMember(Value = "index")]
    Index = 2
}