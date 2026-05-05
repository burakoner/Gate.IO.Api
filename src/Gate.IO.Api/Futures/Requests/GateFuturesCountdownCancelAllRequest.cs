namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures countdown cancel-all request
/// </summary>
public record GateFuturesCountdownCancelAllRequest
{
    public int Timeout { get; set; }
    public string Contract { get; set; }
}
