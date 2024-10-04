namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate.IO Cross Margin REST API Client
/// </summary>
public class GateCrossMarginRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string section = "margin/cross";

    // Endpoints
    private const string currenciesEndpoint = "currencies";
    private const string accountsEndpoint = "accounts";


    private const string accountBookEndpoint = "account_book";
    private const string loansEndpoint = "loans";
    private const string repaymentsEndpoint = "repayments";
    private const string transferableEndpoint = "transferable";
    private const string borrowableEndpoint = "borrowable";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateCrossMarginRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// Currencies supported by cross margin.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossMarginCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateCrossMarginCurrency>>(_.GetUrl(api, version, section, currenciesEndpoint), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Retrieve detail of one single currency supported by cross margin
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateCrossMarginCurrency>(_.GetUrl(api, version, section, currenciesEndpoint.AppendPath(currency)), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Retrieve cross margin account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginBalances>> GetBalancesAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateCrossMarginBalances>(_.GetUrl(api, version, section, accountsEndpoint), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Retrieve cross margin account change history
    /// Record time range cannot exceed 30 days
    /// </summary>
    /// <param name="currency">Filter by currency</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="type">Only retrieve changes of the specified type. All types will be returned if not specified.</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossMarginAccountBook>>> GetBalanceHistoryAsync(
        string currency,
        DateTime from,
        DateTime to,
        string type = null,
        int page = 1,
        int limit = 100,
        CancellationToken ct = default)
        => GetBalanceHistoryAsync(currency, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), type, page, limit, ct);
    
    /// <summary>
    /// Retrieve cross margin account change history
    /// Record time range cannot exceed 30 days
    /// </summary>
    /// <param name="currency">Filter by currency</param>
    /// <param name="from">Start timestamp of the query</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="type">Only retrieve changes of the specified type. All types will be returned if not specified.</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossMarginAccountBook>>> GetBalanceHistoryAsync(
        string currency = null,
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
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptional("type", type);

        return _.SendRequestInternal<List<GateCrossMarginAccountBook>>(_.GetUrl(api, version, section, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Create a cross margin borrow loan
    /// Sort by creation time in descending order by default. Set reverse=false to return ascending results.
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="amount">Borrowed amount</param>
    /// <param name="clientOrderId">User defined custom ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginLoan>> LoanAsync(
        string currency,
        decimal amount,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "amount", amount.ToGateString() },
        };
        parameters.AddOptionalParameter("text", clientOrderId);

        return _.SendRequestInternal<GateCrossMarginLoan>(_.GetUrl(api, version, section, loansEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List cross margin borrow history
    /// Sort by creation time in descending order by default. Set reverse=false to return ascending results.
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="reverse">Whether to sort in descending order, which is the default. Set reverse=false to return ascending results</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossMarginLoan>>> GetLoansAsync(
        string currency = null,
        int limit = 100,
        int offset = 0,
        bool reverse = true,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "offset", offset },
            { "limit", limit },
            { "reverse", reverse.ToString().ToLower() },
        };
        parameters.AddOptionalParameter("currency", currency);

        return _.SendRequestInternal<List<GateCrossMarginLoan>>(_.GetUrl(api, version, section, loansEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve single borrow loan detail
    /// </summary>
    /// <param name="loanId">Borrow loan ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginLoan>> GetLoanAsync(long loanId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateCrossMarginLoan>(_.GetUrl(api, version, section, loansEndpoint.AppendPath(loanId.ToString())), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Cross margin repayments
    /// When the liquidity of the currency is insufficient and the transaction risk is high, the currency will be disabled, and funds cannot be transferred.When the available balance of cross-margin is insufficient, the balance of the spot account can be used for repayment. Please ensure that the balance of the spot account is sufficient, and system uses cross-margin account for repayment first
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="amount">Repayment amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginLoan>> RepayAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "amount", amount.ToGateString() },
        };

        return _.SendRequestInternal<GateCrossMarginLoan>(_.GetUrl(api, version, section, repaymentsEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve cross margin repayments
    /// Sort by creation time in descending order by default. Set reverse=false to return ascending results.
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="loanId">Loan ID</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="reverse">Whether to sort in descending order, which is the default. Set reverse=false to return ascending results</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossMarginRepayment>>> GetRepaymentsAsync(
        string currency = null, long? loanId = null, int limit = 100, int offset = 0, bool reverse = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "limit", limit },
            { "offset", offset },
            { "reverse", reverse.ToString().ToLower() },
        };
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("loan_id", loanId);

        return _.SendRequestInternal<List<GateCrossMarginRepayment>>(_.GetUrl(api, version, section, repaymentsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: Interest records for the cross margin account

    /// <summary>
    /// Get the max transferable amount for a specific cross margin currency
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginAmount>> GetTransferableAmountAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return _.SendRequestInternal<GateCrossMarginAmount>(_.GetUrl(api, version, section, transferableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: Estimated interest rates

    /// <summary>
    /// Get the max borrowable amount for a specific cross margin currency
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginAmount>> GetBorrowableAmountAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return _.SendRequestInternal<GateCrossMarginAmount>(_.GetUrl(api, version, section, borrowableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
}
