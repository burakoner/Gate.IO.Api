using Gate.IO.Api.Models.RestApi.Broker;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiBrokerClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string broker = "broker";

    // Endpoints
    private const string infoEndpoint = "info";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal RestApiBrokerClient(GateRestApiClient root) => _ = root;
    #region The broker inquires its own configuration
    public async Task<RestCallResult<List<BrokerRebateTransactionHistory>>> GetBrokerInformationAsync(CancellationToken ct = default)
    {
        return await _.SendRequestInternal<List<BrokerRebateTransactionHistory>>(_.GetUrl(api, version, broker, infoEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

}