namespace Gate.IO.Api.Futures;

internal record GateFuturesTrailOrderChangeLogResponse
{
    [JsonProperty("change_log")]
    public List<GateFuturesTrailOrderChange> ChangeLog { get; set; } = [];
}
