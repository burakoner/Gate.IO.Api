namespace Gate.IO.Api.TradFi;

internal record GateTradFiResponse<T> where T : class
{
    [JsonProperty("code")]
    public int? Code { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("timestamp")]
    public long? Timestamp { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }
}

internal record GateTradFiList<T> where T : class
{
    [JsonProperty("list")]
    public List<T> List { get; set; } = [];

    [JsonProperty("timestamp")]
    public long? Timestamp { get; set; }
}
