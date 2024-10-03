﻿namespace Gate.IO.Api.Futures.Clients;

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

    internal GateFuturesRestApiClient(GateRestApiClient root)
    {
        Perpetual = new GateFuturesPerpetualRestApiClient(root);
        Delivery = new GateFuturesDeliveryRestApiClient(root);
    }
}