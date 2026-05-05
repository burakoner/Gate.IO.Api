namespace Gate.IO.Api.Futures;

internal record GateFuturesTrailOrderListResponse
{
    [JsonProperty("orders")]
    public List<GateFuturesTrailOrder> Orders { get; set; } = [];
}
