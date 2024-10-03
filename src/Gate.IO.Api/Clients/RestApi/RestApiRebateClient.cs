using Gate.IO.Api.Models.RestApi.Broker;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiRebateClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string rebate = "rebate";

    // Endpoints
    private const string transactionHistoryEndpoint = "agency/transaction_history";
    private const string commissionHistoryEndpoint = "agency/commission_history";

    // Root Client
    internal GateRestApiClient Root { get; }

    internal RestApiRebateClient(GateRestApiClient root)
    {
        Root = root;
    }

    #region The broker obtains the transaction history of the recommended user
    public async Task<RestCallResult<List<BrokerRebateTransactionHistory>>> GetTransactionHistoryAsync(
        DateTime from,
        DateTime to,
        string symbol = null,
        int? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => await GetTransactionHistoryAsync(from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), symbol, userId, limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<List<BrokerRebateTransactionHistory>>> GetTransactionHistoryAsync(
        long? from = null,
        long? to = null,
        string symbol = null,
        int? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("user_id", userId);

        return await Root.SendRequestInternal<List<BrokerRebateTransactionHistory>>(Root.GetUrl(api, version, rebate, transactionHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region The broker obtains the commission history of the recommended user
    public async Task<RestCallResult<List<BrokerRebateTransactionHistory>>> GetCommissionHistoryAsync(
        DateTime from,
        DateTime to,
        string currency = null,
        int? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => await GetCommissionHistoryAsync(from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), currency, userId, limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<List<BrokerRebateTransactionHistory>>> GetCommissionHistoryAsync(
        long? from = null,
        long? to = null,
        string currency = null,
        int? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("user_id", userId);

        return await Root.SendRequestInternal<List<BrokerRebateTransactionHistory>>(Root.GetUrl(api, version, rebate, commissionHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}