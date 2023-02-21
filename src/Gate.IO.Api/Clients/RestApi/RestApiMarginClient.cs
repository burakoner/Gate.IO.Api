namespace Gate.IO.Api.Clients.RestApi;

public class RestApiMarginClient
{
    public RestApiCrossMarginClient Cross { get; }
    public RestApiIsolatedMarginClient Isolated { get; }

    internal RestApiMarginClient(GateRestApiClient root)
    {
        this.Cross = new RestApiCrossMarginClient(root);
        this.Isolated = new RestApiIsolatedMarginClient(root);
    }
}