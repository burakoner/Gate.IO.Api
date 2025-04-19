namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsOrderSide
/// </summary>
public enum GateOptionsOrderSide : byte
{
    /// <summary>
    /// Ask
    /// </summary>
    [Map("ask")]
    Ask = 1,

    /// <summary>
    /// Bid
    /// </summary>
    [Map("bid")]
    Bid = 2,
}
