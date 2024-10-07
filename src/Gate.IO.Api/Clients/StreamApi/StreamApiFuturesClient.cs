namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiFuturesClient
{
    public StreamApiFuturesDeliveryClient Delivery { get; }
    public StreamApiFuturesPerpetualClient Perpetual { get; }

    internal StreamApiFuturesClient(GateWebSocketClient root)
    {
        this.Delivery = new StreamApiFuturesDeliveryClient(root);
        this.Perpetual = new StreamApiFuturesPerpetualClient(root);
    }
}