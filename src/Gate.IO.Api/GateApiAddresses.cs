namespace Gate.IO.Api;

public class GateApiAddresses
{
    /// <summary>
    /// The address used by the BinanceClient for the Spot API
    /// </summary>
    public string RestClientAddress { get; set; }

    /// <summary>
    /// The address used by the BinanceSocketClient for the Spot API
    /// </summary>
    public string SocketClientAddress { get; set; }

    /// <summary>
    /// The default addresses to connect to the binance.com API
    /// </summary>
    public static GateApiAddresses Default = new GateApiAddresses
    {
        RestClientAddress = "https://api.gateio.ws",
        SocketClientAddress = "wss://api.gateio.ws/ws/v4/",
    };

    /// <summary>
    /// The addresses to connect to the binance testnet
    /// </summary>
    public static GateApiAddresses TestNet = new GateApiAddresses
    {
        RestClientAddress = "https://fx-api-testnet.gateio.ws/api/v4",
        SocketClientAddress = "wss://api.gateio.ws/ws/v4/",
    };
}
