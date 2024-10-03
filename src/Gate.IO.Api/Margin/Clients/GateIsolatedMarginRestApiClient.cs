namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate.IO Isolated Margin REST API Client
/// </summary>
public class GateIsolatedMarginRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string margin = "margin";

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
    /// <param name="symbol"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginBalance>>> GetBalancesAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);

        return _.SendRequestInternal<List<GateMarginBalance>>(_.GetUrl(api, version, margin, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List margin account balance change history
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="symbol"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="type"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
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
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="symbol"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="type"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
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

        return _.SendRequestInternal<List<GateMarginBalanceHistory>>(_.GetUrl(api, version, margin, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Funding account list
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginFundingBalance>>> GetFundingBalancesAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return _.SendRequestInternal<List<GateMarginFundingBalance>>(_.GetUrl(api, version, margin, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Update user's auto repayment setting
    /// </summary>
    /// <param name="status"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAutoRepayment>> SetAutoRepaymentAsync(GateMarginAutoRepaymentStatus status, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("status", status);

        return _.SendRequestInternal<GateMarginAutoRepayment>(_.GetUrl(api, version, margin, autoRepayEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve user auto repayment setting
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAutoRepayment>> GetAutoRepaymentAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateMarginAutoRepayment>(_.GetUrl(api, version, margin, autoRepayEndpoint), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Get the max transferable amount for a specific margin currency
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="symbol"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAmount>> GetTransferableAmountAsync(string currency, string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);

        return _.SendRequestInternal<GateMarginAmount>(_.GetUrl(api, version, margin, transferableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }


}
