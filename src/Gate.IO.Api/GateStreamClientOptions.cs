namespace Gate.IO.Api;

public class GateStreamClientOptions : StreamApiClientOptions
{
    // Stream-Spot
    public string StreamSpotAddress { get; set; }

    // Stream-Perpetual Futures
    public Dictionary<FuturesPerpetualSettle, string> StreamPerpetualFuturesAddresses { get; set; }

    // Stream-Delivery Futures
    public Dictionary<FuturesDeliverySettle, string> StreamDeliveryFuturesAddresses { get; set; }

    public GateStreamClientOptions()
    {
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
    }
}
