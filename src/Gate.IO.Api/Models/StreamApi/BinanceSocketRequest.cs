namespace Gate.IO.Api.Models.StreamApi;

internal class BinanceSocketRequest
{
    [JsonProperty("method")]
    public string Method { get; set; } = "";
    [JsonProperty("params")]
    public string[] Params { get; set; } = Array.Empty<string>();
    [JsonProperty("id")]
    public int Id { get; set; }
}
