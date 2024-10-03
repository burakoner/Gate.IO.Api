namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures contract type
/// </summary>
public enum FuturesContractType
{
    /// <summary>
    /// Enum Inverse for value: inverse
    /// </summary>
    [Map("inverse")]
    Inverse,

    /// <summary>
    /// Enum Direct for value: direct
    /// </summary>
    [Map("direct")]
    Direct
}