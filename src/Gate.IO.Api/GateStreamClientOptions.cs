namespace Gate.IO.Api;

public class GateStreamClientOptions : StreamApiClientOptions
{
    public GateStreamClientOptions()
    {
        this.BaseAddress = GateApiAddresses.Default.SocketClientAddress;
    }
}
