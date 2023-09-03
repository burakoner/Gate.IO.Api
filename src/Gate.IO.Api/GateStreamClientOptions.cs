namespace Gate.IO.Api;

public class GateStreamClientOptions : WebSocketApiClientOptions
{
    // Stream-Spot
    public string StreamSpotAddress { get; set; }

    // Stream-Perpetual Futures
    public Dictionary<FuturesPerpetualSettle, string> StreamPerpetualFuturesAddresses { get; set; }

    // Stream-Delivery Futures
    public Dictionary<FuturesDeliverySettle, string> StreamDeliveryFuturesAddresses { get; set; }

    // Stream-Options
    public string StreamOptionsAddress { get; set; }

    public GateStreamClientOptions()
    {
        // Base Address
        this.BaseAddress = GateApiAddresses.Default.StreamSpotAddress;

        // Stream-Spot
        this.StreamSpotAddress = GateApiAddresses.Default.StreamSpotAddress;

        // Stream-Perpetual Futures
        this.StreamPerpetualFuturesAddresses = new Dictionary<FuturesPerpetualSettle, string>
        {
            { FuturesPerpetualSettle.BTC, GateApiAddresses.Default.StreamPerpetualFuturesAddresses[FuturesPerpetualSettle.BTC] },
            { FuturesPerpetualSettle.USD, GateApiAddresses.Default.StreamPerpetualFuturesAddresses[FuturesPerpetualSettle.USD] },
            { FuturesPerpetualSettle.USDT, GateApiAddresses.Default.StreamPerpetualFuturesAddresses[FuturesPerpetualSettle.USDT] },
        };

        // Stream-Delivery Futures
        this.StreamDeliveryFuturesAddresses = new Dictionary<FuturesDeliverySettle, string>
        {
            { FuturesDeliverySettle.BTC, GateApiAddresses.Default.StreamDeliveryFuturesAddresses[FuturesDeliverySettle.BTC] },
            { FuturesDeliverySettle.USDT, GateApiAddresses.Default.StreamDeliveryFuturesAddresses[FuturesDeliverySettle.USDT] },
        };

        // Stream-Options
        this.StreamOptionsAddress = GateApiAddresses.Default.StreamOptionsAddress;

        // Limits
        // this.MaxConnections = 300;
        // this.SubscriptionsCombineTarget = 300;
    }
}
