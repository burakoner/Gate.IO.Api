namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Is Success
/// </summary>
public record GateUnifiedIsSuccess
{
    /// <summary>
    /// Is Success
    /// </summary>
    [JsonProperty("is_success")]
    public bool IsSuccess { get; set; }
}
