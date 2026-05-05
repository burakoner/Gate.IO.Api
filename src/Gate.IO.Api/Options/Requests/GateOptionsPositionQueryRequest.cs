namespace Gate.IO.Api.Options;

/// <summary>
/// Options position query request
/// </summary>
public record GateOptionsPositionQueryRequest
{
    public string Underlying { get; set; }
}
