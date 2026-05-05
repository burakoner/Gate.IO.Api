namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx leverage update request
/// </summary>
public record GateCrossExLeverageRequest
{
    /// <summary>
    /// Trading pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    public decimal Leverage { get; set; }
}
