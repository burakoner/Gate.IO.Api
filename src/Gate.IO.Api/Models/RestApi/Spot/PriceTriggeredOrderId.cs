namespace Gate.IO.Api.Models.RestApi.Spot;

internal class PriceTriggeredOrderId
{
    [JsonProperty("id")]
    public long OrderId { get; set; }
}