namespace Gate.IO.Api.Futures;

internal record GateFuturesChaseOrderCreateResponse
{
    [JsonProperty("id")]
    public string OrderId { get; set; }
}
