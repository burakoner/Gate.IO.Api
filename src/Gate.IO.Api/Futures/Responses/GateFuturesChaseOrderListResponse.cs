namespace Gate.IO.Api.Futures;

internal record GateFuturesChaseOrderListResponse
{
    [JsonProperty("orders")]
    public List<GateFuturesChaseOrder> Orders { get; set; } = [];
}
