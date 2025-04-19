namespace Gate.IO.Api.Futures;

internal record GateFuturesPriceTriggeredOrderId
{
    [JsonProperty("id")]
    public long OrderId { get; set; }
}