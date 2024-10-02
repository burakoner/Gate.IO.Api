namespace Gate.IO.Api.Clients.RestApi;

public class RestApiCrossMarginClient
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
    internal GateRestApiClient Root { get; }

    // Constructor
    internal RestApiCrossMarginClient(GateRestApiClient root)
    {
        Root = root;
    }

    #region Currencies supported by cross margin.
    public async Task<RestCallResult<IEnumerable<CrossMarginCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<IEnumerable<CrossMarginCurrency>>(Root.GetUrl(api, version, cross, currenciesEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion
    
    #region Retrieve detail of one single currency supported by cross margin
    public async Task<RestCallResult<CrossMarginCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<CrossMarginCurrency>(Root.GetUrl(api, version, cross, currenciesEndpoint.AppendPath(currency)), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve cross margin account
    public async Task<RestCallResult<CrossMarginAccount>> GetAccountAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<CrossMarginAccount>(Root.GetUrl(api, version, cross, accountsEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve cross margin account change history
    public async Task<RestCallResult<IEnumerable<CrossMarginAccountBook>>> GetAccountBookAsync(string currency, MarginTransactionType type, DateTime from, DateTime to, int page = 1, int limit = 100, CancellationToken ct = default)
        => await GetAccountBookAsync(currency, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<CrossMarginAccountBook>>> GetAccountBookAsync(string currency = "", MarginTransactionType? type = null, long? from = null, long? to = null, int page = 1, int limit = 100, CancellationToken ct = default)
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

        return await Root.SendRequestInternal<IEnumerable<CrossMarginAccountBook>>(Root.GetUrl(api, version, cross, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Create a cross margin borrow loan
    public async Task<RestCallResult<CrossMarginLoan>> LoanAsync(string currency, decimal amount, string clientOrderId = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "amount", amount.ToGateString() },
        };
        parameters.AddOptionalParameter("text", clientOrderId);

        return await Root.SendRequestInternal<CrossMarginLoan>(Root.GetUrl(api, version, cross, loansEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List cross margin borrow history
    public async Task<RestCallResult<IEnumerable<CrossMarginLoan>>> GetLoansAsync(
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
            { "reverse", reverse.ToString().ToLower() },
        };
        parameters.AddOptionalParameter("currency", currency);

        return await Root.SendRequestInternal<IEnumerable<CrossMarginLoan>>(Root.GetUrl(api, version, cross, loansEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve single borrow loan detail
    public async Task<RestCallResult<CrossMarginLoan>> GetLoanAsync(long loanId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<CrossMarginLoan>(Root.GetUrl(api, version, cross, loansEndpoint.AppendPath(loanId.ToString())), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion
    
    #region Cross margin repayments
    public async Task<RestCallResult<CrossMarginLoanRepayment>> RepayLoanAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "amount", amount.ToGateString() },
        };

        return await Root.SendRequestInternal<CrossMarginLoanRepayment>(Root.GetUrl(api, version, cross, repaymentsEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve cross margin repayments
    public async Task<RestCallResult<IEnumerable<CrossMarginLoanRepaymentRecord>>> GetLoanRepaymentsAsync(
        string currency = "", long? loanId = null, int offset = 0, int limit = 100, bool reverse = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "offset", offset },
            { "limit", limit },
            { "reverse", reverse.ToString().ToLower() },
        };
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("loan_id", loanId);

        return await Root.SendRequestInternal<IEnumerable<CrossMarginLoanRepaymentRecord>>(Root.GetUrl(api, version, cross, repaymentsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get the max transferable amount for a specific cross margin currency
    public async Task<RestCallResult<CrossMarginAmount>> GetMaxTransferableAmountAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return await Root.SendRequestInternal<CrossMarginAmount>(Root.GetUrl(api, version, cross, transferableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get the max borrowable amount for a specific cross margin currency
    public async Task<RestCallResult<CrossMarginAmount>> GetMaxBorrowableAmountAsync(string currency, string symbol = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return await Root.SendRequestInternal<CrossMarginAmount>(Root.GetUrl(api, version, cross, borrowableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion
}
