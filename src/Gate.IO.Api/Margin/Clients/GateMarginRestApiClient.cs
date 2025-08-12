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
    private const string marginuni = "margin/uni";

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
        parameters.AddOptional("type", type);
        parameters.AddOptionalMilliseconds("from", from);
        parameters.AddOptionalMilliseconds("to", to);

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

    /// <summary>
    /// List lending markets
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginMarket>>> GetMarketsAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateMarginMarket>>(_.GetUrl(api, v4, marginuni, "currency_pairs"), HttpMethod.Get, ct, false);
    }

    /// <summary>
    /// Get detail of lending market
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginMarket>> GetMarketsAsync(string symbol, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateMarginMarket>(_.GetUrl(api, v4, marginuni, $"currency_pairs/{symbol}"), HttpMethod.Get, ct, false);
    }

    /// <summary>
    /// Estimate interest Rate
    /// Please note that the interest rates are subject to change based on the borrowing and lending demand, and therefore, the provided rates may not be entirely accurate.
    /// </summary>
    /// <param name="currencies">An array of up to 10 specifying the currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<Dictionary<string, decimal>>> GetEstimatedInterestRateAsync(IEnumerable<string> currencies, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("currencies", string.Join(",", currencies));

        return _.SendRequestInternal<Dictionary<string, decimal>>(_.GetUrl(api, v4, marginuni, "estimate_rate"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Borrow
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="amount">The amount of lending or repaying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginOrder>> BorrowAsync(
        string symbol,
        string currency,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "type", "borrow" },
            { "currency", currency },
            { "currency_pair", symbol },
        };
        parameters.AddString("amount", amount);

        return _.SendRequestInternal<GateMarginOrder>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Repay
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="amount">The amount of lending or repaying</param>
    /// <param name="repaidAll">Full repayment. Repay operation only. If the value is true, the amount will be ignored and the loan will be repaid in full.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginOrder>> RepayAsync(
        string symbol,
        string currency,
        decimal amount,
        bool? repaidAll = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "type", "repay" },
            { "currency", currency },
            { "currency_pair", symbol },
        };
        parameters.AddString("amount", amount);
        parameters.AddOptional("repaid_all", repaidAll?.ToString().ToLowerInvariant());

        return _.SendRequestInternal<GateMarginOrder>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List loans
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum response items. Default: 100, minimum: 1, Maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginLoan>>> GetLoansAsync(
        string symbol,
        string currency,
        int? page = null,
        int? limit = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "currency", currency },
            { "currency_pair", symbol },
        };
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);

        return _.SendRequestInternal<List<GateMarginLoan>>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get loan records
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="type">type: borrow - borrow, repay - repay</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum response items. Default: 100, minimum: 1, Maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginLoan>>> GetLoanHistoryAsync(
        string symbol = null,
        string currency = null,
        GateMarginUniOrderType? type = null,
        int? page = null,
        int? limit = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);

        return _.SendRequestInternal<List<GateMarginLoan>>(_.GetUrl(api, v4, marginuni, "loan_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List interest records
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="type">type: borrow - borrow, repay - repay</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum response items. Default: 100, minimum: 1, Maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginInterest>>> GetInterestHistoryAsync(
        string symbol = null,
        string currency = null,
        string type = null,
        int? page = null,
        int? limit = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("type", type);
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);

        return _.SendRequestInternal<List<GateMarginInterest>>(_.GetUrl(api, v4, marginuni, "interest_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get maximum borrowable
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginBorrowable>> GetMaximumBorrowableAsync(
        string symbol,
        string currency,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);
        parameters.Add("currency_pair", symbol);

        return _.SendRequestInternal<GateMarginBorrowable>(_.GetUrl(api, v4, marginuni, "borrowable"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: GET /margin/user/loan_margin_tiers
    // TODO: GET /margin/loan_margin_tiers
    // TODO: POST /margin/leverage/user_market_setting
    // TODO: GET /margin/user/account
}