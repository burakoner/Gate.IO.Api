namespace Gate.IO.Api.P2p;

internal record GateP2pMyAdvertisementPage
{
    [JsonProperty("lists")]
    public List<GateP2pAdvertisement> Lists { get; set; } = [];
}
