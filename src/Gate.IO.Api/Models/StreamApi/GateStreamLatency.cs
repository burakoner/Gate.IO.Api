namespace Gate.IO.Api.Models.StreamApi;

public class GateStreamLatency
{
    public DateTime PingTime { get; set; }
    public DateTime PongTime { get; set; }
    public string PongMessage { get; set; }
    public TimeSpan Latency { get; set; }
}