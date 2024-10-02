using Gate.IO.Api.Models.RestApi.Margin;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiIsolatedMarginClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string margin = "margin";

    // Endpoints
    private const string currencyPairsEndpoint = "currency_pairs";
    private const string fundingBookEndpoint = "funding_book";
    private const string accountsEndpoint = "accounts";
    private const string accountBookEndpoint = "account_book";
    private const string loansEndpoint = "loans";
    private const string mergedLoansEndpoint = "merged_loans";
    private const string loanRecordsEndpoint = "loan_records";
    private const string autoRepayEndpoint = "auto_repay";
    private const string transferableEndpoint = "transferable";
    private const string borrowableEndpoint = "borrowable";

    // Root Client
    internal GateRestApiClient Root { get; }

    internal RestApiIsolatedMarginClient(GateRestApiClient root)
    {
        Root = root;
    }

    #region List all supported currency pairs supported in margin trading
    public async Task<RestCallResult<IEnumerable<MarginMarket>>> GetAllPairsAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<IEnumerable<MarginMarket>>(Root.GetUrl(api, version, margin, currencyPairsEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Query one single margin currency pair
    public async Task<RestCallResult<MarginMarket>> GetPairAsync(string symbol, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<MarginMarket>(Root.GetUrl(api, version, margin, currencyPairsEndpoint.AppendPath(symbol)), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Order book of lending loans
    public async Task<RestCallResult<IEnumerable<MarginFundingAccountBook>>> GetFundingAccountBookAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
        };

        return await Root.SendRequestInternal<IEnumerable<MarginFundingAccountBook>>(Root.GetUrl(api, version, margin, fundingBookEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Margin account list
    public async Task<RestCallResult<IEnumerable<MarginAccount>>> GetAccountAsync(string symbol = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await Root.SendRequestInternal<IEnumerable<MarginAccount>>(Root.GetUrl(api, version, margin, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List margin account balance change history
    public async Task<RestCallResult<IEnumerable<MarginAccountBook>>> GetAccountBookAsync(string currency, string symbol, DateTime from, DateTime to, int page = 1, int limit = 100, CancellationToken ct = default)
        => await GetBalancesHistoryAsync(currency, symbol, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), page, limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<MarginAccountBook>>> GetBalancesHistoryAsync(string currency = "", string symbol = "", long? from = null, long? to = null, int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
            { "page", page },
        };
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await Root.SendRequestInternal<IEnumerable<MarginAccountBook>>(Root.GetUrl(api, version, margin, accountBookEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Funding account list
    public async Task<RestCallResult<IEnumerable<MarginFundingAccount>>> GetFundingAccountAsync(string currency = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return await Root.SendRequestInternal<IEnumerable<MarginFundingAccount>>(Root.GetUrl(api, version, margin, accountsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Lend or borrow
    public async Task<RestCallResult<MarginLoan>> LoanAsync(
        MarginLoanSide side,
        string currency,
        decimal amount,
        decimal? rate = null,
        decimal? feeRate = null,
        int days = 10,
        bool? autoRenew = null,
        string symbol = "",
        string clientOrderId = "",
        CancellationToken ct = default)
        => await LoanAsync(new MarginLoanRequest
        {
            Side = side,
            Currency = currency,
            Symbol = symbol,
            Amount = amount,
            Rate = rate,
            FeeRate = feeRate,
            Days = days,
            AutoRenew = autoRenew,
            ClientOrderId = clientOrderId,
        }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<MarginLoan>> LoanAsync(MarginLoanRequest request, CancellationToken ct = default)
    {
        if (request.Rate.HasValue) request.Rate.Value.ValidateDecimalBetween(nameof(request.Rate), 0.0001m, 0.01m);

        var parameters = new Dictionary<string, object> {
            { "side", JsonConvert.SerializeObject(request.Side, new MarginLoanSideConverter(false)) },
            { "currency", request.Currency },
            { "amount", request.Amount.ToGateString() },
        };
        parameters.AddOptionalParameter("rate", request.Rate?.ToGateString());
        parameters.AddOptionalParameter("days", request.Days?.ToString());
        parameters.AddOptionalParameter("auto_renew", request.AutoRenew);
        parameters.AddOptionalParameter("currency_pair", request.Symbol);
        parameters.AddOptionalParameter("fee_rate", request.FeeRate?.ToGateString());
        parameters.AddOptionalParameter("text", request.ClientOrderId);

        return await Root.SendRequestInternal<MarginLoan>(Root.GetUrl(api, version, margin, loansEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List all loans
    public async Task<RestCallResult<IEnumerable<MarginLoan>>> GetLoansAsync(
        MarginLoanSide side,
        MarginLoanStatus status,
        string currency = "",
        string symbol = "",
        string sortBy = "create_time",
        bool reverse = true,
        int page = 1,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "side", JsonConvert.SerializeObject(side, new MarginLoanSideConverter(false)) },
            { "status", JsonConvert.SerializeObject(status, new MarginLoanStatusConverter(false)) },
            { "sort_by", sortBy },
            { "reverse_sort", reverse.ToString().ToLower() },
            { "page", page },
            { "limit", limit }
        };
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await Root.SendRequestInternal<IEnumerable<MarginLoan>>(Root.GetUrl(api, version, margin, loansEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Merge multiple lending loans
    public async Task<RestCallResult<MarginLoan>> MergeLoansAsync(string currency, IEnumerable<long> ids, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "ids", string.Join(",", ids) }
        };

        return await Root.SendRequestInternal<MarginLoan>(Root.GetUrl(api, version, margin, mergedLoansEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve one single loan detail
    public async Task<RestCallResult<MarginLoan>> GetLoanAsync(MarginLoanSide side, long loanId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "side", JsonConvert.SerializeObject(side, new MarginLoanSideConverter(false)) },
        };

        return await Root.SendRequestInternal<MarginLoan>(Root.GetUrl(api, version, margin, loansEndpoint.AppendPath(loanId.ToString())), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Modify a loan
#if NETSTANDARD2_1
    public async Task<RestCallResult<MarginLoan>> ModifyLoanAsync(
        int loanId,
        string currency,
        MarginLoanSide side,
        bool autoRenew,
        string symbol = "",
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "side", JsonConvert.SerializeObject(side, new MarginLoanSideConverter(false)) },
            { "auto_renew", autoRenew.ToString().ToLower() },
            { "loan_id", loanId },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await Root.SendRequestInternal<MarginLoan>(Root.GetUrl(api, version, margin, loansEndpoint.AppendPath(loanId.ToString())), HttpMethod.Patch, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
#endif
    #endregion

    #region Cancel lending loan
    public async Task<RestCallResult<MarginLoan>> CancelLendingLoanAsync(long loanId, string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };

        return await Root.SendRequestInternal<MarginLoan>(Root.GetUrl(api, version, margin, loansEndpoint.AppendPath(loanId.ToString())), HttpMethod.Delete, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Repay a loan
    public async Task<RestCallResult<MarginLoan>> RepayLoanAsync(long loanId, string currency, string symbol, MarginLoanRepayMode mode, decimal? amount = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "currency_pair", symbol },
            { "mode", JsonConvert.SerializeObject(mode, new MarginLoanRepayModeConverter(false)) },
        };
        parameters.AddOptionalParameter("amount", amount?.ToGateString());

        return await Root.SendRequestInternal<MarginLoan>(Root.GetUrl(api, version, margin, loansEndpoint.AppendPath(loanId.ToString()).AppendPath("repayment")), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List loan repayment records
    public async Task<RestCallResult<IEnumerable<MarginLoanRepayment>>> GetLoanRepaymentsAsync(long loanId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<IEnumerable<MarginLoanRepayment>>(Root.GetUrl(api, version, margin, loansEndpoint.AppendPath(loanId.ToString()).AppendPath("repayment")), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region List repayment records of a specific loan
    public async Task<RestCallResult<IEnumerable<MarginLoanRecord>>> GetLoanRecordsAsync(long loanId, MarginLoanStatus? status = null, int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "loan_id", loanId },
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new MarginLoanStatusConverter(false)));

        return await Root.SendRequestInternal<IEnumerable<MarginLoanRecord>>(Root.GetUrl(api, version, margin, loanRecordsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get one single loan record
    public async Task<RestCallResult<MarginLoanRecord>> GetLoanRecordAsync(long loanId, long loanRecordId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "loan_id", loanId },
        };

        return await Root.SendRequestInternal<MarginLoanRecord>(Root.GetUrl(api, version, margin, loanRecordsEndpoint.AppendPath(loanRecordId.ToString())), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Modify a loan record
#if NETSTANDARD2_1
    public async Task<RestCallResult<MarginLoanRecord>> ModifyLoanRecordAsync(
        int loanId,
        long loanRecordId,
        string currency,
        MarginLoanSide side,
        bool autoRenew,
        string symbol = "",
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
            { "side", JsonConvert.SerializeObject(side, new MarginLoanSideConverter(false)) },
            { "auto_renew", autoRenew.ToString().ToLower() },
            { "loan_id", loanId },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await Root.SendRequestInternal<MarginLoanRecord>(Root.GetUrl(api, version, margin, loanRecordsEndpoint.AppendPath(loanRecordId.ToString())), HttpMethod.Patch, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
#endif
    #endregion

    #region Update user's auto repayment setting
    public async Task<RestCallResult<MarginAutoRepayment>> SetAutoRepaymentAsync(AutoRepaymentStatus status, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "status", JsonConvert.SerializeObject(status, new AutoRepaymentStatusConverter(false)) },
        };

        return await Root.SendRequestInternal<MarginAutoRepayment>(Root.GetUrl(api, version, margin, autoRepayEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve user auto repayment setting
    public async Task<RestCallResult<MarginAutoRepayment>> GetAutoRepaymentAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<MarginAutoRepayment>(Root.GetUrl(api, version, margin, autoRepayEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Get the max transferable amount for a specific margin currency
    public async Task<RestCallResult<MarginAmount>> GetMaxTransferableAmountAsync(string currency, string symbol = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await Root.SendRequestInternal<MarginAmount>(Root.GetUrl(api, version, margin, transferableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get the max borrowable amount for a specific margin currency
    public async Task<RestCallResult<MarginAmount>> GetMaxBorrowableAmountAsync(string currency, string symbol = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "currency", currency },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);

        return await Root.SendRequestInternal<MarginAmount>(Root.GetUrl(api, version, margin, borrowableEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}
