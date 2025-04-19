namespace Gate.IO.Api.Margin;

/// <summary>
/// GateMarginAutoRepayment
/// </summary>
public record GateMarginAutoRepayment
{
    /// <summary>
    /// Current auto repayment setting
    /// </summary>
    [JsonProperty("status")]
    public GateMarginAutoRepaymentStatus Status { get; set; }
}