namespace Gate.IO.Api.Margin;

/// <summary>
/// Isolated margin market leverage setting request
/// </summary>
public record GateMarginLeverageSettingRequest
{
    /// <summary>
    /// Position leverage
    /// </summary>
    public int Leverage { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    public string Symbol { get; set; }
}
