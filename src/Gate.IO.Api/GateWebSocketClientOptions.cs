namespace Gate.IO.Api;

public class GateWebSocketClientOptions : WebSocketApiClientOptions
{
    // Stream-Spot
    public string StreamSpotAddress { get; set; }

    // Stream-Perpetual Futures
    public Dictionary<GateFuturesSettlement, string> StreamPerpetualFuturesAddresses { get; set; }

    // Stream-Delivery Futures
    public Dictionary<GateDeliverySettlement, string> StreamDeliveryFuturesAddresses { get; set; }

    // Stream-Options
    public string StreamOptionsAddress { get; set; }
    
    /// <summary>
    /// Gate.IO API Broker Id
    /// </summary>
    public string BrokerId { get; set; }

    public GateWebSocketClientOptions()
    {
        // Base Address
        this.BaseAddress = GateConstants.Default.StreamSpotAddress;

        // Stream-Spot
        this.StreamSpotAddress = GateConstants.Default.StreamSpotAddress;

        // Stream-Perpetual Futures
        this.StreamPerpetualFuturesAddresses = new Dictionary<GateFuturesSettlement, string>
        {
            { GateFuturesSettlement.BTC, GateConstants.Default.StreamPerpetualFuturesAddresses[GateFuturesSettlement.BTC] },
            { GateFuturesSettlement.USDT, GateConstants.Default.StreamPerpetualFuturesAddresses[GateFuturesSettlement.USDT] },
        };

        // Stream-Delivery Futures
        this.StreamDeliveryFuturesAddresses = new Dictionary<GateDeliverySettlement, string>
        {
            { GateDeliverySettlement.USDT, GateConstants.Default.StreamDeliveryFuturesAddresses[GateDeliverySettlement.USDT] },
        };

        // Stream-Options
        this.StreamOptionsAddress = GateConstants.Default.StreamOptionsAddress;

        // Limits
        // this.MaxConnections = 300;
        // this.SubscriptionsCombineTarget = 300;

        // Broker Id
        BrokerId = "phalchatha";
    }
}
