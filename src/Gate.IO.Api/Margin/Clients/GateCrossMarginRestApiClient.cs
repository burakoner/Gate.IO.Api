namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate.IO Cross Margin REST API Client
/// </summary>
public class GateCrossMarginRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string cross = "margin/cross";

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
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateCrossMarginCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateCrossMarginCurrency>>(_.GetUrl(api, version, cross, currenciesEndpoint), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Retrieve detail of one single currency supported by cross margin
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateCrossMarginCurrency>(_.GetUrl(api, version, cross, currenciesEndpoint.AppendPath(currency)), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Retrieve cross margin account
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginBalances>> GetBalancesAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateCrossMarginBalances>(_.GetUrl(api, version, cross, accountsEndpoint), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Retrieve cross margin account change history
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="type"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
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
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="type"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
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

        return _.SendRequestInternal<List<GateCrossMarginAccountBook>>(_.GetUrl(api, version, cross, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Create a cross margin borrow loan
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="ct"></param>
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

        return _.SendRequestInternal<GateCrossMarginLoan>(_.GetUrl(api, version, cross, loansEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List cross margin borrow history
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="reverse"></param>
    /// <param name="ct"></param>
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

        return _.SendRequestInternal<List<GateCrossMarginLoan>>(_.GetUrl(api, version, cross, loansEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve single borrow loan detail
    /// </summary>
    /// <param name="loanId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginLoan>> GetLoanAsync(long loanId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateCrossMarginLoan>(_.GetUrl(api, version, cross, loansEndpoint.AppendPath(loanId.ToString())), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Cross margin repayments
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginLoan>> RepayAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "amount", amount.ToGateString() },
        };

        return _.SendRequestInternal<GateCrossMarginLoan>(_.GetUrl(api, version, cross, repaymentsEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve cross margin repayments
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="loanId"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="reverse"></param>
    /// <param name="ct"></param>
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

        return _.SendRequestInternal<List<GateCrossMarginRepayment>>(_.GetUrl(api, version, cross, repaymentsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: Interest records for the cross margin account

    /// <summary>
    /// Get the max transferable amount for a specific cross margin currency
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginAmount>> GetTransferableAmountAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return _.SendRequestInternal<GateCrossMarginAmount>(_.GetUrl(api, version, cross, transferableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: Estimated interest rates

    /// <summary>
    /// Get the max borrowable amount for a specific cross margin currency
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateCrossMarginAmount>> GetBorrowableAmountAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return _.SendRequestInternal<GateCrossMarginAmount>(_.GetUrl(api, version, cross, borrowableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
}
