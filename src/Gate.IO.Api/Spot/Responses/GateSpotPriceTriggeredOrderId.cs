namespace Gate.IO.Api.Spot;

internal class GateSpotPriceTriggeredOrderId
{
    [JsonProperty("id")]
    public long OrderId { get; set; }
}