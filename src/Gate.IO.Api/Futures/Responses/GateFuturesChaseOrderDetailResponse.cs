namespace Gate.IO.Api.Futures;

internal record GateFuturesChaseOrderDetailResponse
{
    [JsonProperty("order", Required = Required.Always)]
    public GateFuturesChaseOrder Order { get; set; }
}
