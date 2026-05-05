namespace Gate.IO.Api.Futures;

internal record GateFuturesTrailOrderDetailResponse
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public GateFuturesTrailOrderDetailData Data { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }
}

internal record GateFuturesTrailOrderDetailData
{
    [JsonProperty("order")]
    public GateFuturesTrailOrder Order { get; set; }
}
