namespace Gate.IO.Api.Earn;

/// <summary>
/// Compact fixed-term Earn product list
/// </summary>
public record GateEarnFixedTermProductSimpleList
{
    /// <summary>
    /// Product list
    /// </summary>
    [JsonProperty("list")]
    public List<GateEarnFixedTermProductSimple> List { get; set; } = [];
}
