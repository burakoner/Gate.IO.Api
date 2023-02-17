namespace Gate.IO.Api.Clients.RestApi;

public class FuturesRestApiClient
{
    public FuturesDeliveryRestApiClient Delivery { get; }
    public FuturesPerpetualRestApiClient Perpetual { get; }

    internal FuturesRestApiClient(GateRestApiClient root)
    {
        this.Delivery = new FuturesDeliveryRestApiClient(root);
        this.Perpetual = new FuturesPerpetualRestApiClient(root);
    }
}