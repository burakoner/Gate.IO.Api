namespace Gate.IO.Api.P2p;

internal record GateP2pResponse<T> where T : class
{
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }
}
