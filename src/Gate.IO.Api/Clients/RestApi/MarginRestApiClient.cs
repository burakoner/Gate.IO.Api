namespace Gate.IO.Api.Clients.RestApi;

public class MarginRestApiClient
{
    public MarginCrossRestApiClient Cross { get; }
    public MarginIsolatedRestApiClient Isolated { get; }

    internal MarginRestApiClient(GateRestApiClient root)
    {
        this.Cross = new MarginCrossRestApiClient(root);
        this.Isolated = new MarginIsolatedRestApiClient(root);
    }
}