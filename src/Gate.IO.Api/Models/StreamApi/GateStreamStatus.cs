namespace Gate.IO.Api.Models.StreamApi;

public record GateStreamStatus
{
    [JsonProperty("status")]
    public string Status { get; set; }
}
