namespace Gate.IO.Api.Futures;

internal record GateFuturesTrailOrderChangeLogResponse
{
    [JsonProperty("change_log", Required = Required.Always)]
    public List<GateFuturesTrailOrderChange> ChangeLog { get; set; } = [];
}
