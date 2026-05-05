namespace Gate.IO.Api.Bot;

internal record GateBotResponse<T> where T : class
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("trace_id")]
    public string TraceId { get; set; }
}
