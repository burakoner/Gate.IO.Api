namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures REST API Client
/// </summary>
public class GateFuturesRestApiClient
{
    /// <summary>
    /// Perpetual Futures Client
    /// </summary>
    public GateFuturesPerpetualRestApiClient Perpetual { get; }

    /// <summary>
    /// Delivery Futures Client
    /// </summary>
    public GateFuturesDeliveryRestApiClient Delivery { get; }

    // Constructor
    internal GateFuturesRestApiClient(GateRestApiClient root)
    {
        Perpetual = new GateFuturesPerpetualRestApiClient(root);
        Delivery = new GateFuturesDeliveryRestApiClient(root);
    }
}