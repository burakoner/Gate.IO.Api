namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate.IO Margin REST API Client
/// </summary>
public class GateMarginRestApiClient
{
    /// <summary>
    /// Cross Margin Client
    /// </summary>
    public GateCrossMarginRestApiClient Cross { get; }

    /// <summary>
    /// Isolated Margin Client
    /// </summary>
    public GateIsolatedMarginRestApiClient Isolated { get; }

    // TODO: Unified

    // Constructor
    internal GateMarginRestApiClient(GateRestApiClient root)
    {
        this.Cross = new GateCrossMarginRestApiClient(root);
        this.Isolated = new GateIsolatedMarginRestApiClient(root);
    }
}