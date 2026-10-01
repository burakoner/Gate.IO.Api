namespace Gate.IO.Api.Futures;

internal record GateFuturesTrailOrderCreateResponse
{
    [JsonProperty("code")]
    public JToken Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public GateFuturesTrailOrderCreateData Data { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }
}

internal record GateFuturesTrailOrderCreateData
{
    [JsonProperty("id"), JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long OrderId { get; set; }
}
