namespace Gate.IO.Api.Models.StreamApi;

public record GateStreamLatency
{
    public DateTime PingTime { get; set; }
    public DateTime PongTime { get; set; }
    public string PongMessage { get; set; }
    public TimeSpan Latency { get; set; }
}