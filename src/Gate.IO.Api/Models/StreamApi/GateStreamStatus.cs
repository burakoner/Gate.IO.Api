namespace Gate.IO.Api.Models.StreamApi;

public class GateStreamStatus
{
    [JsonProperty("status")]
    public string Status { get; set; }
}
