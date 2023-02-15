using Gate.IO.Api.Models.RestApi.Margin;
using Gate.IO.Api.Models.RestApi.Wallet;

namespace Gate.IO.Api.Clients.RestApi;

public class CrossMarginRestApiClient : RestApiClient
{
    // Api
    protected const string api = "api";
    protected const string version = "4";
    protected const string cross = "margin/cross";

    // Endpoints
    private const string currenciesEndpoint = "currencies";
    private const string accountsEndpoint = "accounts";
    private const string accountBookEndpoint = "account_book";
    private const string loansEndpoint = "loans";
    private const string repaymentsEndpoint = "repayments";
    private const string transferableEndpoint = "transferable";
    private const string borrowableEndpoint = "borrowable";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("Gate.IO CrossMargin RestApi");

    // Root Client
    internal GateRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public GateRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal CrossMarginRestApiClient(GateRestApiClient root) : base("Gate.IO CrossMargin RestApi", root.ClientOptions)
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

    protected override CallError ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Spot.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    public override TimeSpan GetTimeOffset()
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

    #region Currencies supported by cross margin.
    public async Task<RestCallResult<IEnumerable<CrossMarginCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return await SendRequestInternal<IEnumerable<CrossMarginCurrency>>(RootClient.GetUrl(api, version, cross, currenciesEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion
    
    #region Retrieve detail of one single currency supported by cross margin
    public async Task<RestCallResult<CrossMarginCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
    {
        return await SendRequestInternal<CrossMarginCurrency>(RootClient.GetUrl(api, version, cross, currenciesEndpoint.AppendPath(currency)), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve cross margin account
    public async Task<RestCallResult<CrossMarginAccount>> GetBalancesAsync(CancellationToken ct = default)
    {
        return await SendRequestInternal<CrossMarginAccount>(RootClient.GetUrl(api, version, cross, accountsEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve cross margin account change history
    public async Task<RestCallResult<IEnumerable<CrossMarginBalanceHistory>>> GetBalancesHistoryAsync(string currency, MarginTransactionType type, DateTime from, DateTime to, int page = 1, int limit = 100, CancellationToken ct = default)
        => await GetBalancesHistoryAsync(currency, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<CrossMarginBalanceHistory>>> GetBalancesHistoryAsync(string currency = "", MarginTransactionType? type = null, long? from = null, long? to = null, int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
            { "page", page },
        };
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new MarginTransactionTypeConverter(false)));
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<CrossMarginBalanceHistory>>(RootClient.GetUrl(api, version, cross, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Create a cross margin borrow loan
    public async Task<RestCallResult<CrossMarginLoanResponse>> LoanAsync(string currency, decimal amount, string clientOrderId = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "amount", amount },
        };
        parameters.AddOptionalParameter("text", clientOrderId);

        return await SendRequestInternal<CrossMarginLoanResponse>(RootClient.GetUrl(api, version, cross, loansEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List cross margin borrow history
    public async Task<RestCallResult<IEnumerable<CrossMarginLoanResponse>>> GetLoansAsync(
        CrossMarginLoanStatus status,
        string currency = "",
        int offset = 0,
        int limit = 100,
        bool reverse = true,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "status", JsonConvert.SerializeObject(status, new CrossMarginLoanStatusConverter(false)) },
            { "offset", offset },
            { "limit", limit },
            { "reverse", reverse },
        };
        parameters.AddOptionalParameter("currency", currency);

        return await SendRequestInternal<IEnumerable<CrossMarginLoanResponse>>(RootClient.GetUrl(api, version, cross, loansEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve single borrow loan detail
    public async Task<RestCallResult<CrossMarginLoanResponse>> GetLoanAsync(long loanId, CancellationToken ct = default)
    {
        return await SendRequestInternal<CrossMarginLoanResponse>(RootClient.GetUrl(api, version, cross, loansEndpoint.AppendPath(loanId.ToString())), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion
    
    #region Cross margin repayments
    public async Task<RestCallResult<CrossMarginLoanRepayment>> RepayLoanAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "amount", amount },
        };

        return await SendRequestInternal<CrossMarginLoanRepayment>(RootClient.GetUrl(api, version, cross, repaymentsEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve cross margin repayments
    public async Task<RestCallResult<IEnumerable<CrossMarginLoanRepaymentRecord>>> GetLoanRepaymentsAsync(
        string currency = "", long? loanId = null, int offset = 0, int limit = 100, bool reverse = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "offset", offset },
            { "limit", limit },
            { "reverse", reverse },
        };
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("loan_id", loanId);

        return await SendRequestInternal<IEnumerable<CrossMarginLoanRepaymentRecord>>(RootClient.GetUrl(api, version, cross, repaymentsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get the max transferable amount for a specific cross margin currency
    public async Task<RestCallResult<CrossMarginAmount>> GetMaxTransferableAmountAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return await SendRequestInternal<CrossMarginAmount>(RootClient.GetUrl(api, version, cross, transferableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get the max borrowable amount for a specific cross margin currency
    public async Task<RestCallResult<CrossMarginAmount>> GetMaxBorrowableAmountAsync(string currency, string symbol = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return await SendRequestInternal<CrossMarginAmount>(RootClient.GetUrl(api, version, cross, borrowableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion
}
