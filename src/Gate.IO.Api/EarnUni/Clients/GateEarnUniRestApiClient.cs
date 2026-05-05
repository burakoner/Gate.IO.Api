namespace Gate.IO.Api.EarnUni;

/// <summary>
/// Gate.IO EarnUni REST API Client
/// </summary>
public class GateEarnUniRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string earn = "earn";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateEarnUniRestApiClient(GateRestApiClient root) => _ = root;

    private static void AddPaging(ParameterCollection parameters, int? page, int? limit)
    {
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);
    }

    private static void AddTimeRange(ParameterCollection parameters, DateTime? from, DateTime? to)
    {
        parameters.AddOptional("from", from?.ConvertToSeconds());
        parameters.AddOptional("to", to?.ConvertToSeconds());
    }

    private static void ValidateLimit(int? limit)
    {
        if (limit.HasValue) limit.Value.ValidateIntBetween(nameof(limit), 1, 100);
    }

    /// <summary>
    /// Query lending currency list
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateEarnUniCurrency>>(_.GetUrl(api, v4, earn, "uni/currencies"), HttpMethod.Get, ct);

    /// <summary>
    /// Query single lending currency details
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnUniCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
        => _.SendRequestInternal<GateEarnUniCurrency>(_.GetUrl(api, v4, earn, "uni/currencies".AppendPath(currency)), HttpMethod.Get, ct);

    /// <summary>
    /// Query user's lending order list
    /// </summary>
    /// <param name="currency">Query by specified currency name</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of items returned. Default: 100, minimum: 1, maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniLend>>> GetLendsAsync(string currency = null, int page = 1, int limit = 100, CancellationToken ct = default)
        => GetLendsAsync(new GateEarnUniLendQueryRequest { Currency = currency, Page = page, Limit = limit }, ct);

    /// <summary>
    /// Query user's lending order list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniLend>>> GetLendsAsync(GateEarnUniLendQueryRequest request, CancellationToken ct = default)
    {
        ValidateLimit(request.Limit);

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        AddPaging(parameters, request.Page, request.Limit);

        return _.SendRequestInternal<List<GateEarnUniLend>>(_.GetUrl(api, v4, earn, "uni/lends"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Create lending or redemption
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="amount">Amount to deposit into lending pool or redeem</param>
    /// <param name="type">Operation type</param>
    /// <param name="minimumRate">Minimum interest rate. Required for lending operations.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> CreateLendAsync(string currency, decimal amount, GateEarnUniLendOperationType type, decimal? minimumRate = null, CancellationToken ct = default)
        => CreateLendAsync(new GateEarnUniLendRequest { Currency = currency, Amount = amount, Type = type, MinimumRate = minimumRate }, ct);

    /// <summary>
    /// Create lending or redemption
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> CreateLendAsync(GateEarnUniLendRequest request, CancellationToken ct = default)
    {
        if (request.Type == GateEarnUniLendOperationType.Lend && !request.MinimumRate.HasValue)
            throw new ArgumentException("MinimumRate is required for lending operations", nameof(request.MinimumRate));

        var parameters = new ParameterCollection
        {
            { "currency", request.Currency },
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddEnum("type", request.Type);
        parameters.AddOptionalString("min_rate", request.MinimumRate);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, earn, "uni/lends"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Create a lending order
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="amount">Amount to deposit into lending pool</param>
    /// <param name="minimumRate">Minimum interest rate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> LendAsync(string currency, decimal amount, decimal minimumRate, CancellationToken ct = default)
        => CreateLendAsync(currency, amount, GateEarnUniLendOperationType.Lend, minimumRate, ct);

    /// <summary>
    /// Create a redemption request
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="amount">Amount to redeem</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> RedeemAsync(string currency, decimal amount, CancellationToken ct = default)
        => CreateLendAsync(currency, amount, GateEarnUniLendOperationType.Redeem, null, ct);

    /// <summary>
    /// Amend user lending information
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="minimumRate">Minimum interest rate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdateLendAsync(string currency, decimal minimumRate, CancellationToken ct = default)
        => UpdateLendAsync(new GateEarnUniLendUpdateRequest { Currency = currency, MinimumRate = minimumRate }, ct);

    /// <summary>
    /// Amend user lending information
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdateLendAsync(GateEarnUniLendUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptionalString("min_rate", request.MinimumRate);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, earn, "uni/lends"), new HttpMethod("PATCH"), ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query lending transaction records
    /// </summary>
    /// <param name="currency">Query by specified currency name</param>
    /// <param name="from">Start time</param>
    /// <param name="to">End time</param>
    /// <param name="type">Operation type</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of items returned. Default: 100, minimum: 1, maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniLendRecord>>> GetLendRecordsAsync(string currency = null, DateTime? from = null, DateTime? to = null, GateEarnUniLendOperationType? type = null, int page = 1, int limit = 100, CancellationToken ct = default)
        => GetLendRecordsAsync(new GateEarnUniLendRecordQueryRequest { Currency = currency, From = from, To = to, Type = type, Page = page, Limit = limit }, ct);

    /// <summary>
    /// Query lending transaction records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniLendRecord>>> GetLendRecordsAsync(GateEarnUniLendRecordQueryRequest request, CancellationToken ct = default)
    {
        ValidateLimit(request.Limit);

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        AddPaging(parameters, request.Page, request.Limit);
        AddTimeRange(parameters, request.From, request.To);
        parameters.AddOptionalEnum("type", request.Type);

        return _.SendRequestInternal<List<GateEarnUniLendRecord>>(_.GetUrl(api, v4, earn, "uni/lend_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query user's total interest income for specified currency
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnUniLendInterest>> GetInterestAsync(string currency, CancellationToken ct = default)
        => _.SendRequestInternal<GateEarnUniLendInterest>(_.GetUrl(api, v4, earn, "uni/interests".AppendPath(currency)), HttpMethod.Get, ct, true);

    /// <summary>
    /// Query user dividend records
    /// </summary>
    /// <param name="currency">Query by specified currency name</param>
    /// <param name="from">Start time</param>
    /// <param name="to">End time</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of items returned. Default: 100, minimum: 1, maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniInterestRecord>>> GetInterestRecordsAsync(string currency = null, DateTime? from = null, DateTime? to = null, int page = 1, int limit = 100, CancellationToken ct = default)
        => GetInterestRecordsAsync(new GateEarnUniInterestRecordQueryRequest { Currency = currency, From = from, To = to, Page = page, Limit = limit }, ct);

    /// <summary>
    /// Query user dividend records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniInterestRecord>>> GetInterestRecordsAsync(GateEarnUniInterestRecordQueryRequest request, CancellationToken ct = default)
    {
        ValidateLimit(request.Limit);

        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        AddPaging(parameters, request.Page, request.Limit);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateEarnUniInterestRecord>>(_.GetUrl(api, v4, earn, "uni/interest_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query currency interest compounding status
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnUniCurrencyInterest>> GetInterestStatusAsync(string currency, CancellationToken ct = default)
        => _.SendRequestInternal<GateEarnUniCurrencyInterest>(_.GetUrl(api, v4, earn, "uni/interest_status".AppendPath(currency)), HttpMethod.Get, ct, true);

    /// <summary>
    /// UniLoan currency annualized trend chart
    /// </summary>
    /// <param name="asset">Currency name</param>
    /// <param name="from">Start time</param>
    /// <param name="to">End time</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniChartPoint>>> GetChartAsync(string asset, DateTime from, DateTime to, CancellationToken ct = default)
        => GetChartAsync(new GateEarnUniChartQueryRequest { Asset = asset, From = from, To = to }, ct);

    /// <summary>
    /// UniLoan currency annualized trend chart
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniChartPoint>>> GetChartAsync(GateEarnUniChartQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "asset", request.Asset },
            { "from", request.From.ConvertToSeconds() },
            { "to", request.To.ConvertToSeconds() },
        };

        return _.SendRequestInternal<List<GateEarnUniChartPoint>>(_.GetUrl(api, v4, earn, "uni/chart"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Currency estimated annualized interest rate
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnUniEstimatedRate>>> GetEstimatedRatesAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateEarnUniEstimatedRate>>(_.GetUrl(api, v4, earn, "uni/rate"), HttpMethod.Get, ct, true);
}
