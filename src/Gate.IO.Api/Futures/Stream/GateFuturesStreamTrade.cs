namespace Gate.IO.Api.Futures;

public  class GateFuturesStreamTrade
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }
    
    [JsonProperty("price")]
    public decimal Price { get; set; }
}
