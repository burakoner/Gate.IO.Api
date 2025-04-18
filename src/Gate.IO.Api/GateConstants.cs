namespace Gate.IO.Api;

/// <summary>
/// Gate.IO API Addresses
/// </summary>
internal class GateConstants
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
    public Dictionary<GateFuturesSettlement, string> StreamPerpetualFuturesAddresses { get; set; }

    /// <summary>
    /// Stream-Delivery Futures
    /// </summary>
    public Dictionary<GateDeliverySettlement, string> StreamDeliveryFuturesAddresses { get; set; }

    /// <summary>
    /// Stream-Options
    /// </summary>
    public string StreamOptionsAddress { get; set; }

    /// <summary>
    /// Gate.IO API Broker Id
    /// </summary>
    public string ChannelId { get; set; }

    /// <summary>
    /// Default Gate.IO API Main Environment
    /// </summary>
    public static GateConstants Default = new()
    {
        // Rest Api
        RestApiAddress = "https://api.gateio.ws",

        // Stream-Spot
        StreamSpotAddress = "wss://api.gateio.ws/ws/v4/",

        // Stream-Perpetual Futures
        StreamPerpetualFuturesAddresses = new Dictionary<GateFuturesSettlement, string> {
            { GateFuturesSettlement.BTC, "wss://fx-ws.gateio.ws/v4/ws/btc" },
            { GateFuturesSettlement.USD, "wss://fx-ws.gateio.ws/v4/ws/usd" },
            { GateFuturesSettlement.USDT, "wss://fx-ws.gateio.ws/v4/ws/usdt" },
        },

        // Stream-Delivery Futures
        StreamDeliveryFuturesAddresses = new Dictionary<GateDeliverySettlement, string> {
            { GateDeliverySettlement.USDT, "wss://fx-ws.gateio.ws/v4/ws/delivery/usdt" },
        },

        // Stream-Options
        StreamOptionsAddress = "wss://op-ws.gateio.live/v4/ws",

        // Broker Id
        ChannelId = "phalchatha"
    };

    /// <summary>
    /// Default Gate.IO API TestNet Environment
    /// </summary>
    public static GateConstants TestNet = new()
    {
        // Rest Api
        RestApiAddress = "https://fx-api-testnet.gateio.ws/api/v4",

        // Stream-Spot
        StreamSpotAddress = "wss://api.gateio.ws/ws/v4/",

        // Stream-Perpetual Futures
        StreamPerpetualFuturesAddresses = new Dictionary<GateFuturesSettlement, string> {
            { GateFuturesSettlement.BTC, "wss://fx-ws-testnet.gateio.ws/v4/ws/btc" },
            { GateFuturesSettlement.USD, "wss://fx-ws-testnet.gateio.ws/v4/ws/usd" },
            { GateFuturesSettlement.USDT, "wss://fx-ws-testnet.gateio.ws/v4/ws/usdt" },
        },

        // Stream-Delivery Futures
        StreamDeliveryFuturesAddresses = new Dictionary<GateDeliverySettlement, string> {
            { GateDeliverySettlement.USDT, "wss://fx-ws-testnet.gateio.ws/v4/ws/delivery/usdt" },
        },

        // Stream-Options
        StreamOptionsAddress = "wss://op-ws-testnet.gateio.live/v4/ws",
    };
}
