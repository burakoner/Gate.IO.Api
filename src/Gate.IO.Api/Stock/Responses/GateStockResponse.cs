namespace Gate.IO.Api.Stock;

internal record GateStockResponse<T> where T : class
{
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("timestamp")]
    public long? Timestamp { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }
}

internal record GateStockList<T> where T : class
{
    [JsonProperty("list")]
    public List<T> List { get; set; } = [];
}
