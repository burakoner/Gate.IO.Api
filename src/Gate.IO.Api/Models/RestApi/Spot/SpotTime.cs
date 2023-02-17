namespace Gate.IO.Api.Models.RestApi.Spot;

internal class SpotTime
{
    /// <summary>
    /// Server current time(ms)
    /// </summary>
    [JsonProperty("server_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}