namespace Gate.IO.Api.Models.RestApi.Spot;

internal class SpotTriggerOrderId
{
    [JsonProperty("id")]
    public long OrderId { get; set; }
}