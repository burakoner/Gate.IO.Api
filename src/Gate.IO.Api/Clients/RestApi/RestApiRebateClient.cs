using Gate.IO.Api.Models.RestApi.Broker;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiRebateClient : RestApiClient
{
    // Api
    protected const string api = "api";
    protected const string version = "4";
    protected const string rebate = "rebate";

    // Endpoints
    private const string transactionHistoryEndpoint = "agency/transaction_history";
    private const string commissionHistoryEndpoint = "agency/commission_history";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("Gate.IO Rebate RestApi");

    // Root Client
    internal GateRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public new GateRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal RestApiRebateClient(GateRestApiClient root) : base("Gate.IO Rebate RestApi", root.ClientOptions)
    {
        RootClient = root;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new GateAuthenticationProvider(credentials);

    protected override Error ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Spot.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    protected override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    internal async Task<RestCallResult<T>> SendRequestInternal<T>(
        Uri uri,
        HttpMethod method,
        CancellationToken cancellationToken,
        bool signed = false,
        Dictionary<string, object> queryParameters = null,
        Dictionary<string, object> bodyParameters = null,
        Dictionary<string, string> headerParameters = null,
        ArraySerialization? arraySerialization = null,
        JsonSerializer deserializer = null,
        bool ignoreRatelimit = false,
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }
    #endregion

    #region The broker obtains the transaction history of the recommended user
    public async Task<RestCallResult<IEnumerable<BrokerRebateTransactionHistory>>> GetTransactionHistoryAsync(
        DateTime from,
        DateTime to,
        string symbol = "",
        int? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => await GetTransactionHistoryAsync(from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), symbol, userId, limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<BrokerRebateTransactionHistory>>> GetTransactionHistoryAsync(
        long? from = null,
        long? to = null,
        string symbol = "",
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

        return await SendRequestInternal<IEnumerable<BrokerRebateTransactionHistory>>(RootClient.GetUrl(api, version, rebate, transactionHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region The broker obtains the commission history of the recommended user
    public async Task<RestCallResult<IEnumerable<BrokerRebateTransactionHistory>>> GetCommissionHistoryAsync(
        DateTime from,
        DateTime to,
        string currency = "",
        int? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => await GetCommissionHistoryAsync(from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), currency, userId, limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<BrokerRebateTransactionHistory>>> GetCommissionHistoryAsync(
        long? from = null,
        long? to = null,
        string currency = "",
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

        return await SendRequestInternal<IEnumerable<BrokerRebateTransactionHistory>>(RootClient.GetUrl(api, version, rebate, commissionHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}