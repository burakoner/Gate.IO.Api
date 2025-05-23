namespace Gate.IO.Api.Unified;

/// <summary>
/// SubAccount Mode
/// </summary>
public enum GateUnifiedAccountMode : byte
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("classic")]
    Classic = 1,
    
    /// <summary>
    /// Normal
    /// </summary>
    [Map("multi_currency")]
    MultiCurrency = 2,
    
    /// <summary>
    /// Normal
    /// </summary>
    [Map("portfolio")]
    Portfolio = 3,
}
