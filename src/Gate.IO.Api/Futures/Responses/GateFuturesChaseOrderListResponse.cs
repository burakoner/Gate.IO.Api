namespace Gate.IO.Api.Futures;

internal record GateFuturesChaseOrderListResponse
{
    [JsonProperty("orders", Required = Required.Always)]
    public List<GateFuturesChaseOrder> Orders { get; set; } = [];
}
