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
        this.BaseAddress = GateApiAddresses.Default.StreamSpotAddress;

        // Stream-Spot
        this.StreamSpotAddress = GateApiAddresses.Default.StreamSpotAddress;

        // Stream-Perpetual Futures
        this.StreamPerpetualFuturesAddresses = new Dictionary<GateFuturesSettlement, string>
        {
            { GateFuturesSettlement.BTC, GateApiAddresses.Default.StreamPerpetualFuturesAddresses[GateFuturesSettlement.BTC] },
            { GateFuturesSettlement.USD, GateApiAddresses.Default.StreamPerpetualFuturesAddresses[GateFuturesSettlement.USD] },
            { GateFuturesSettlement.USDT, GateApiAddresses.Default.StreamPerpetualFuturesAddresses[GateFuturesSettlement.USDT] },
        };

        // Stream-Delivery Futures
        this.StreamDeliveryFuturesAddresses = new Dictionary<GateDeliverySettlement, string>
        {
            { GateDeliverySettlement.USDT, GateApiAddresses.Default.StreamDeliveryFuturesAddresses[GateDeliverySettlement.USDT] },
        };

        // Stream-Options
        this.StreamOptionsAddress = GateApiAddresses.Default.StreamOptionsAddress;

        // Limits
        // this.MaxConnections = 300;
        // this.SubscriptionsCombineTarget = 300;

        // Broker Id
        BrokerId = "phalchatha";
    }
}
