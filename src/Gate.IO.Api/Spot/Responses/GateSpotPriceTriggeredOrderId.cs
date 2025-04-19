namespace Gate.IO.Api.Spot;

internal record GateSpotPriceTriggeredOrderId
{
    [JsonProperty("id")]
    public long OrderId { get; set; }
}