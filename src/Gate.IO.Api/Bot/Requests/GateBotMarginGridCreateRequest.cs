namespace Gate.IO.Api.Bot;

/// <summary>
/// Margin grid creation request
/// </summary>
public record GateBotMarginGridCreateRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Create Parameters.
    /// </summary>
    public GateBotMarginGridCreateParameters CreateParameters { get; set; }
}
