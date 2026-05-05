namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot grid price spacing type
/// </summary>
public enum GateBotGridPriceType : byte
{
    /// <summary>
    /// Arithmetic spacing
    /// </summary>
    Arithmetic = 0,

    /// <summary>
    /// Geometric spacing
    /// </summary>
    Geometric = 1,
}
