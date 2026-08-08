namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock position close type
/// </summary>
public enum GateStockPositionCloseType
{
    /// <summary>Partially close the position</summary>
    [Map("1")]
    Partial = 1,
    /// <summary>Close the entire position</summary>
    [Map("2")]
    All = 2,
}
