namespace Gate.IO.Api;

/// <summary>
/// Represents the Gate Web Socket Client Options.
/// </summary>
public class GateWebSocketClientOptions : WebSocketApiClientOptions
{
    // Stream-Spot
    /// <summary>
    /// Gets or sets the Stream Spot Address.
    /// </summary>
    public string StreamSpotAddress { get; set; }

    // Stream-Perpetual Futures
    /// <summary>
    /// Gets or sets the Stream Perpetual Futures Addresses.
    /// </summary>
    public Dictionary<GateFuturesSettlement, string> StreamPerpetualFuturesAddresses { get; set; }

    // Stream-Delivery Futures
    /// <summary>
    /// Gets or sets the Stream Delivery Futures Addresses.
    /// </summary>
    public Dictionary<GateDeliverySettlement, string> StreamDeliveryFuturesAddresses { get; set; }

    // Stream-Options
    /// <summary>
    /// Gets or sets the Stream Options Address.
    /// </summary>
    public string StreamOptionsAddress { get; set; }

    // Stream-TradFi
    /// <summary>
    /// Gets or sets the Stream TradFi Address.
    /// </summary>
    public string StreamTradFiAddress { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the Gate Web Socket Client Options class.
    /// </summary>
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
            { GateDeliverySettlement.BTC, GateConstants.Default.StreamDeliveryFuturesAddresses[GateDeliverySettlement.BTC] },
            { GateDeliverySettlement.USDT, GateConstants.Default.StreamDeliveryFuturesAddresses[GateDeliverySettlement.USDT] },
        };

        // Stream-Options
        this.StreamOptionsAddress = GateConstants.Default.StreamOptionsAddress;

        // Stream-TradFi
        this.StreamTradFiAddress = GateConstants.Default.StreamTradFiAddress;

        // Limits
        // this.MaxConnections = 300;
        // this.SubscriptionsCombineTarget = 300;
    }
}
