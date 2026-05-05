namespace Gate.IO.Api.Bot;

/// <summary>
/// Futures grid creation request
/// </summary>
public record GateBotFuturesGridCreateRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Create Parameters.
    /// </summary>
    public GateBotFuturesGridCreateParameters CreateParameters { get; set; }
}
