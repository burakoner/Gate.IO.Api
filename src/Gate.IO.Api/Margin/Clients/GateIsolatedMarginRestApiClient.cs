namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate.IO Isolated Margin REST API Client
/// </summary>
public class GateIsolatedMarginRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string section = "margin";

    // Endpoints
    private const string accountsEndpoint = "accounts";
    private const string accountBookEndpoint = "account_book";
    private const string autoRepayEndpoint = "auto_repay";
    private const string transferableEndpoint = "transferable";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateIsolatedMarginRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// Margin account list
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginBalance>>> GetBalancesAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);

        return _.SendRequestInternal<List<GateMarginBalance>>(_.GetUrl(api, version, section, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List margin account balance change history
    /// Only transferals from and to margin account are provided for now. Time range allows 30 days at most
    /// </summary>
    /// <param name="currency">List records related to specified currency only. If specified, currency_pair is also required.</param>
    /// <param name="symbol">List records related to specified currency pair. Used in combination with currency. Ignored if currency is not provided</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="type">Only retrieve changes of the specified type. All types will be returned if not specified.</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginBalanceHistory>>> GetBalanceHistoryAsync(
        string currency,
        string symbol,
        DateTime from,
        DateTime to,
        string type = null,
        int page = 1,
        int limit = 100,
        CancellationToken ct = default)
        => GetBalanceHistoryAsync(currency, symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), type, page, limit, ct);
    
    /// <summary>
    /// List margin account balance change history
    /// Only transferals from and to margin account are provided for now. Time range allows 30 days at most
    /// </summary>
    /// <param name="currency">List records related to specified currency only. If specified, currency_pair is also required.</param>
    /// <param name="symbol">List records related to specified currency pair. Used in combination with currency. Ignored if currency is not provided</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="type">Only retrieve changes of the specified type. All types will be returned if not specified.</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginBalanceHistory>>> GetBalanceHistoryAsync(
        string currency = null,
        string symbol = null,
        long? from = null,
        long? to = null,
        string type = null,
        int page = 1,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
            { "page", page },
        };
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptional("type", type);

        return _.SendRequestInternal<List<GateMarginBalanceHistory>>(_.GetUrl(api, version, section, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Funding account list
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginFundingBalance>>> GetFundingBalancesAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return _.SendRequestInternal<List<GateMarginFundingBalance>>(_.GetUrl(api, version, section, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Update user's auto repayment setting
    /// </summary>
    /// <param name="status"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAutoRepayment>> SetAutoRepaymentAsync(GateMarginAutoRepaymentStatus status, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("status", status);

        return _.SendRequestInternal<GateMarginAutoRepayment>(_.GetUrl(api, version, section, autoRepayEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve user auto repayment setting
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAutoRepayment>> GetAutoRepaymentAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateMarginAutoRepayment>(_.GetUrl(api, version, section, autoRepayEndpoint), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Get the max transferable amount for a specific margin currency
    /// </summary>
    /// <param name="currency">	Retrieve data of the specified currency</param>
    /// <param name="symbol">Currency pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAmount>> GetTransferableAmountAsync(string currency, string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);

        return _.SendRequestInternal<GateMarginAmount>(_.GetUrl(api, version, section, transferableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }


}
