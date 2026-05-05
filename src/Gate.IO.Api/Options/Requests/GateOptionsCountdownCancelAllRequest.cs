namespace Gate.IO.Api.Options;

/// <summary>
/// Options countdown cancel-all request
/// </summary>
public record GateOptionsCountdownCancelAllRequest
{
    public int Timeout { get; set; }
    public string Contract { get; set; }
    public string Underlying { get; set; }
}
