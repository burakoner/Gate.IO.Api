namespace Gate.IO.Api;

/// <summary>
/// Gate.IO API Addresses
/// </summary>
public class GateApiAddresses
{
    /// <summary>
    /// Rest Api Address
    /// </summary>
    public string RestApiAddress { get; set; }

    /// <summary>
    /// Stream-Spot Address
    /// </summary>
    public string StreamSpotAddress { get; set; }

    /// <summary>
    /// Stream-Perpetual Futures
    /// </summary>
    public Dictionary<FuturesPerpetualSettle, string> StreamPerpetualFuturesAddresses { get; set; }

    /// <summary>
    /// Stream-Delivery Futures
    /// </summary>
    public Dictionary<FuturesDeliverySettle, string> StreamDeliveryFuturesAddresses { get; set; }

    /// <summary>
    /// Stream-Options
    /// </summary>
    public string StreamOptionsAddress { get; set; }

    /// <summary>
    /// Default Gate.IO API Main Environment
    /// </summary>
    public static GateApiAddresses Default = new()
    {
        // Rest Api
        RestApiAddress = "https://api.gateio.ws",

        // Stream-Spot
        StreamSpotAddress = "wss://api.gateio.ws/ws/v4/",

        // Stream-Perpetual Futures
        StreamPerpetualFuturesAddresses = new Dictionary<FuturesPerpetualSettle, string> {
            { FuturesPerpetualSettle.BTC, "wss://fx-ws.gateio.ws/v4/ws/btc" },
            { FuturesPerpetualSettle.USD, "wss://fx-ws.gateio.ws/v4/ws/usd" },
            { FuturesPerpetualSettle.USDT, "wss://fx-ws.gateio.ws/v4/ws/usdt" },
        },

        // Stream-Delivery Futures
        StreamDeliveryFuturesAddresses = new Dictionary<FuturesDeliverySettle, string> {
            { FuturesDeliverySettle.BTC, "wss://fx-ws.gateio.ws/v4/ws/delivery/btc" },
            { FuturesDeliverySettle.USDT, "wss://fx-ws.gateio.ws/v4/ws/delivery/usdt" },
        },

        // Stream-Options
        StreamOptionsAddress = "wss://op-ws.gateio.live/v4/ws",
    };

    /// <summary>
    /// Default Gate.IO API TestNet Environment
    /// </summary>
    public static GateApiAddresses TestNet = new()
    {
        // Rest Api
        RestApiAddress = "https://fx-api-testnet.gateio.ws/api/v4",

        // Stream-Spot
        StreamSpotAddress = "wss://api.gateio.ws/ws/v4/",

        // Stream-Perpetual Futures
        StreamPerpetualFuturesAddresses = new Dictionary<FuturesPerpetualSettle, string> {
            { FuturesPerpetualSettle.BTC, "wss://fx-ws-testnet.gateio.ws/v4/ws/btc" },
            { FuturesPerpetualSettle.USD, "wss://fx-ws-testnet.gateio.ws/v4/ws/usd" },
            { FuturesPerpetualSettle.USDT, "wss://fx-ws-testnet.gateio.ws/v4/ws/usdt" },
        },

        // Stream-Delivery Futures
        StreamDeliveryFuturesAddresses = new Dictionary<FuturesDeliverySettle, string> {
            { FuturesDeliverySettle.BTC, "wss://fx-ws-testnet.gateio.ws/v4/ws/delivery/btc" },
            { FuturesDeliverySettle.USDT, "wss://fx-ws-testnet.gateio.ws/v4/ws/delivery/usdt" },
        },

        // Stream-Options
        StreamOptionsAddress = "wss://op-ws-testnet.gateio.live/v4/ws",
    };
}
