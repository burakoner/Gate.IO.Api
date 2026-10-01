namespace Gate.IO.Api.Futures;

internal record GateFuturesTrailOrderListResponse
{
    [JsonProperty("orders", Required = Required.Always)]
    public List<GateFuturesTrailOrder> Orders { get; set; } = [];
}
