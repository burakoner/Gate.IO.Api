namespace Gate.IO.Api.Clients.RestApi;

public class RestApiFuturesClient
{
    public RestApiFuturesDeliveryClient Delivery { get; }
    public RestApiFuturesPerpetualClient Perpetual { get; }

    internal RestApiFuturesClient(GateRestApiClient root)
    {
        this.Delivery = new RestApiFuturesDeliveryClient(root);
        this.Perpetual = new RestApiFuturesPerpetualClient(root);
    }
}