namespace Gate.IO.Api.Models.RestApi;

internal class ServerTime
{
    /// <summary>
    /// Server current time(ms)
    /// </summary>
    [JsonProperty("server_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}