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
    private const string marginuser = "margin/user";
    private const string marginleverage = "margin/leverage";

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
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);

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
        => GetBalanceHistoryAsync(new GateMarginBalanceHistoryQueryRequest
        {
            Currency = currency,
            Symbol = symbol,
            From = from,
            To = to,
            Type = type,
            Page = page,
            Limit = limit,
        }, ct);

    /// <summary>
    /// List margin account balance change history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginBalanceHistory>>> GetBalanceHistoryAsync(GateMarginBalanceHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("currency_pair", request.Symbol);
        parameters.AddOptional("type", request.Type);
        parameters.AddOptionalSeconds("from", request.From);
        parameters.AddOptionalSeconds("to", request.To);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);

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
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

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

        return _.SendRequestInternal<GateMarginAutoRepayment>(_.GetUrl(api, v4, margin, "auto_repay"), HttpMethod.Post, ct, true, queryParameters: parameters);
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
        => GetTransferableAmountAsync(new GateMarginTransferableAmountRequest
        {
            Currency = currency,
            Symbol = symbol,
        }, ct);

    /// <summary>
    /// Get the max transferable amount for a specific margin currency
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginAmount>> GetTransferableAmountAsync(GateMarginTransferableAmountRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", request.Currency);
        parameters.AddOptional("currency_pair", request.Symbol);

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

        return _.SendRequestInternal<Dictionary<string, decimal>>(_.GetUrl(api, v4, marginuni, "estimate_rate"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Borrow
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="amount">The amount of lending or repaying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> BorrowAsync(
        string symbol,
        string currency,
        decimal amount,
        CancellationToken ct = default)
        => BorrowOrRepayAsync(new GateMarginLoanRequest
        {
            Symbol = symbol,
            Currency = currency,
            Amount = amount,
            Type = GateMarginUniOrderType.Borrow,
        }, ct);

    /// <summary>
    /// Repay
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="amount">The amount of lending or repaying</param>
    /// <param name="repaidAll">Full repayment. Repay operation only. If the value is true, the amount will be ignored and the loan will be repaid in full.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> RepayAsync(
        string symbol,
        string currency,
        decimal amount,
        bool? repaidAll = null,
        CancellationToken ct = default)
        => BorrowOrRepayAsync(new GateMarginLoanRequest
        {
            Symbol = symbol,
            Currency = currency,
            Amount = amount,
            Type = GateMarginUniOrderType.Repay,
            RepaidAll = repaidAll,
        }, ct);

    /// <summary>
    /// Borrow or repay
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> BorrowOrRepayAsync(GateMarginLoanRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("type", request.Type);
        parameters.Add("currency", request.Currency);
        parameters.Add("currency_pair", request.Symbol);
        parameters.AddString("amount", request.Amount);
        parameters.AddOptional("repaid_all", request.RepaidAll);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
        string symbol = null,
        string currency = null,
        int? page = null,
        int? limit = null,
        CancellationToken ct = default)
        => GetLoansAsync(new GateMarginLoanQueryRequest
        {
            Symbol = symbol,
            Currency = currency,
            Page = page,
            Limit = limit,
        }, ct);

    /// <summary>
    /// List loans
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginLoan>>> GetLoansAsync(GateMarginLoanQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("currency_pair", request.Symbol);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);

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
    public Task<RestCallResult<List<GateMarginLoanRecord>>> GetLoanHistoryAsync(
        string symbol = null,
        string currency = null,
        GateMarginUniOrderType? type = null,
        int? page = null,
        int? limit = null,
        CancellationToken ct = default)
        => GetLoanHistoryAsync(new GateMarginLoanRecordQueryRequest
        {
            Symbol = symbol,
            Currency = currency,
            Type = type,
            Page = page,
            Limit = limit,
        }, ct);

    /// <summary>
    /// Get loan records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginLoanRecord>>> GetLoanHistoryAsync(GateMarginLoanRecordQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("currency_pair", request.Symbol);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);

        return _.SendRequestInternal<List<GateMarginLoanRecord>>(_.GetUrl(api, v4, marginuni, "loan_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List interest records
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="type">Deprecated; ignored by the current API.</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum response items. Default: 100, minimum: 1, Maximum: 100</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginInterest>>> GetInterestHistoryAsync(
        string symbol = null,
        string currency = null,
        string type = null,
        int? page = null,
        int? limit = null,
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default)
        => GetInterestHistoryAsync(new GateMarginInterestRecordQueryRequest
        {
            Symbol = symbol,
            Currency = currency,
            Page = page,
            Limit = limit,
            From = from,
            To = to,
        }, ct);

    /// <summary>
    /// List interest records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginInterest>>> GetInterestHistoryAsync(GateMarginInterestRecordQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("currency_pair", request.Symbol);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);
        parameters.AddOptionalSeconds("from", request.From);
        parameters.AddOptionalSeconds("to", request.To);

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
        => GetMaximumBorrowableAsync(new GateMarginBorrowableRequest
        {
            Symbol = symbol,
            Currency = currency,
        }, ct);

    /// <summary>
    /// Get maximum borrowable
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginBorrowable>> GetMaximumBorrowableAsync(GateMarginBorrowableRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", request.Currency);
        parameters.Add("currency_pair", request.Symbol);

        return _.SendRequestInternal<GateMarginBorrowable>(_.GetUrl(api, v4, marginuni, "borrowable"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query user's own leverage lending tiers in current market
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginTier>>> GetUserLendingTiersAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency_pair", symbol);

        return _.SendRequestInternal<List<GateMarginTier>>(_.GetUrl(api, v4, marginuser, "loan_margin_tiers"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query current market leverage lending tiers
    /// </summary>
    /// <param name="symbol">Trading pair</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginTier>>> GetCurrentLendingTiersAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency_pair", symbol);

        return _.SendRequestInternal<List<GateMarginTier>>(_.GetUrl(api, v4, margin, "loan_margin_tiers"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Set user market leverage multiplier
    /// </summary>
    /// <param name="leverage">Position leverage</param>
    /// <param name="symbol">Market</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetLeverageAsync(int leverage, string symbol = null, CancellationToken ct = default)
        => SetLeverageAsync(new GateMarginLeverageSettingRequest
        {
            Leverage = leverage,
            Symbol = symbol,
        }, ct);

    /// <summary>
    /// Set user market leverage multiplier
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetLeverageAsync(GateMarginLeverageSettingRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("leverage", request.Leverage);
        parameters.AddOptional("currency_pair", request.Symbol);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, marginleverage, "user_market_setting"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query user's isolated margin account list
    /// Supports querying risk ratio isolated accounts and margin ratio isolated accounts
    /// </summary>
    /// <param name="symbol">Market</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginBalance>>> GetIsolatedBalancesAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);

        return _.SendRequestInternal<List<GateMarginBalance>>(_.GetUrl(api, v4, marginuser, "account"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
}
