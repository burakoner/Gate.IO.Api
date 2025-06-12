namespace Gate.IO.Api.MarginUni;

/// <summary>
/// Gate.IO Margin Uni REST API Client
/// </summary>
public class GateMarginUniRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string marginuni = "margin/uni";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateMarginUniRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// List lending markets
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMarginUniMarket>>> GetMarketsAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateMarginUniMarket>>(_.GetUrl(api, v4, marginuni, "currency_pairs"), HttpMethod.Get, ct, false);
    }

    /// <summary>
    /// Get detail of lending market
    /// </summary>
    /// <param name="symbol">Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginUniMarket>> GetMarketsAsync(string symbol, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateMarginUniMarket>(_.GetUrl(api, v4, marginuni, $"currency_pairs/{symbol}"), HttpMethod.Get, ct, false);
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
    public Task<RestCallResult<GateMarginUniOrder>> BorrowAsync(
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

        return _.SendRequestInternal<GateMarginUniOrder>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
    public Task<RestCallResult<GateMarginUniOrder>> RepayAsync(
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

        return _.SendRequestInternal<GateMarginUniOrder>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
    public Task<RestCallResult<List<GateMarginUniLoan>>> GetLoansAsync(
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

        return _.SendRequestInternal<List<GateMarginUniLoan>>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Get, ct, true, queryParameters: parameters);
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
    public Task<RestCallResult<List<GateMarginUniLoan>>> GetLoanHistoryAsync(
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

        return _.SendRequestInternal<List<GateMarginUniLoan>>(_.GetUrl(api, v4, marginuni, "loan_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
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
    public Task<RestCallResult<List<GateMarginUniInterest>>> GetInterestHistoryAsync(
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

        return _.SendRequestInternal<List<GateMarginUniInterest>>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get maximum borrowable
    /// </summary>
    /// <param name="symbol">Currency pair</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMarginUniBorrowable>> GetMaximumBorrowableAsync(
        string symbol,
        string currency,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);
        parameters.Add("currency_pair", symbol);

        return _.SendRequestInternal<GateMarginUniBorrowable>(_.GetUrl(api, v4, marginuni, "loans"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

}