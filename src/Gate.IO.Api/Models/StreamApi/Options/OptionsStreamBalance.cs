namespace Gate.IO.Api.Models.StreamApi.Options;

public class OptionsStreamBalance
{
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("text")]
    public string Comment { get; set; }

    [JsonProperty("type")]
    public OptionsAccountBookType Type { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("time")]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}