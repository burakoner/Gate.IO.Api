namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock profit and loss calculation type
/// </summary>
public enum GateStockPnlCalculationType
{
    /// <summary>Average cost</summary>
    [Map("1")]
    AverageCost = 1,
    /// <summary>Diluted cost</summary>
    [Map("2")]
    DilutedCost = 2,
}
