namespace Gate.IO.Api.Futures;

internal record GateFuturesChaseOrderDetailResponse
{
    [JsonProperty("order")]
    public GateFuturesChaseOrder Order { get; set; }
}
