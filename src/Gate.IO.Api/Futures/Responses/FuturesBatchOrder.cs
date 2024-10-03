namespace Gate.IO.Api.Futures;

public class FuturesBatchOrder : GateFuturesOrder
{
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}