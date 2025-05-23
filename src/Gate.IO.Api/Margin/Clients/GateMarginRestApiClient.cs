namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate.IO Margin REST API Client
/// </summary>
public class GateMarginRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string margin = "margin";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateMarginRestApiClient(GateRestApiClient root) => _ = root;

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

        return _.SendRequestInternal<List<GateMarginBalance>>(_.GetUrl(api, v4, margin, "accounts"), HttpMethod.Get, ct, true, queryParameters: parameters);
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
        string currency = null,
        string symbol = null,
        DateTime? from = null,
        DateTime? to = null,
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
        parameters.AddOptionalMilliseconds("from", from);
        parameters.AddOptionalMilliseconds("to", to);
        parameters.AddOptional("type", type);

        return _.SendRequestInternal<List<GateMarginBalanceHistory>>(_.GetUrl(api, v4, margin, "account_book"), HttpMethod.Get, ct, true, queryParameters: parameters);
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

        return _.SendRequestInternal<List<GateMarginFundingBalance>>(_.GetUrl(api, v4, margin, "funding_accounts"), HttpMethod.Get, ct, true, queryParameters: parameters);
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

        return _.SendRequestInternal<GateMarginAutoRepayment>(_.GetUrl(api, v4, margin, "auto_repay"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve user auto repayment setting
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAutoRepayment>> GetAutoRepaymentAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateMarginAutoRepayment>(_.GetUrl(api, v4, margin, "auto_repay"), HttpMethod.Get, ct, true);
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

        return _.SendRequestInternal<GateMarginAmount>(_.GetUrl(api, v4, margin, "transferable"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: Check the user's own leverage lending gradient in the current market
    // TODO: Query the current market leverage lending gradient
    // TODO: Set the user market leverage multiple
    // TODO: Query the user's leverage account list

}