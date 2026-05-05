namespace Gate.IO.Api.Rebate;

internal record GateRebateResponse<T> where T : class
{
    [JsonProperty("code")]
    public int? Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("timestamp")]
    public long? Timestamp { get; set; }
}
