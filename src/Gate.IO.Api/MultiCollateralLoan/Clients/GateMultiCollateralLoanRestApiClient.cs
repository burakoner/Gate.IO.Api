namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Gate.IO Multi-Collateral Loan REST API Client
/// </summary>
public class GateMultiCollateralLoanRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string loan = "loan";
    private const string multiCollateral = "multi_collateral";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateMultiCollateralLoanRestApiClient(GateRestApiClient root) => _ = root;

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

    private static List<string> NormalizeValues(IEnumerable<string> values, string name, int? maximum = null)
    {
        var list = values?.Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? [];
        if (maximum.HasValue)
            list.Count.ValidateIntBetween(name, 1, maximum.Value);
        else if (list.Count == 0)
            throw new ArgumentException($"{name} must contain at least one value", name);

        return list;
    }

    private static List<Dictionary<string, object>> BuildCurrencyAmounts(IEnumerable<GateMultiCollateralLoanCurrencyAmount> values)
    {
        return values?
            .Where(x => !string.IsNullOrWhiteSpace(x.Currency))
            .Select(x => new Dictionary<string, object>
            {
                { "currency", x.Currency },
                { "amount", x.Amount.ToString(CultureInfo.InvariantCulture) },
            })
            .ToList();
    }

    private static List<Dictionary<string, object>> BuildRepayItems(IEnumerable<GateMultiCollateralLoanRepayItem> values)
    {
        return values?
            .Where(x => !string.IsNullOrWhiteSpace(x.Currency))
            .Select(x =>
            {
                var item = new Dictionary<string, object>
                {
                    { "currency", x.Currency },
                    { "repaid_all", x.RepaidAll },
                };
                if (x.Amount.HasValue) item.Add("amount", x.Amount.Value.ToString(CultureInfo.InvariantCulture));
                return item;
            })
            .ToList();
    }

    /// <summary>
    /// Query multi-currency collateral order list
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records returned in a single list</param>
    /// <param name="sort">Sort type</param>
    /// <param name="orderType">Order type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanOrder>>> GetOrdersAsync(
        int? page = null,
        int? limit = null,
        GateMultiCollateralLoanOrderSort? sort = null,
        GateMultiCollateralLoanOrderType? orderType = null,
        CancellationToken ct = default)
        => GetOrdersAsync(new GateMultiCollateralLoanOrderQueryRequest
        {
            Page = page,
            Limit = limit,
            Sort = sort,
            OrderType = orderType,
        }, ct);

    /// <summary>
    /// Query multi-currency collateral order list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanOrder>>> GetOrdersAsync(GateMultiCollateralLoanOrderQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddPaging(parameters, request.Page, request.Limit);
        parameters.AddOptionalEnum("sort", request.Sort);
        parameters.AddOptionalEnum("order_type", request.OrderType);

        return _.SendRequestInternal<List<GateMultiCollateralLoanOrder>>(_.GetUrl(api, v4, loan, $"{multiCollateral}/orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Place multi-currency collateral order
    /// </summary>
    /// <param name="borrowCurrency">Borrowed currency</param>
    /// <param name="borrowAmount">Borrowed amount</param>
    /// <param name="collateralCurrencies">Collateral currency and amount</param>
    /// <param name="orderType">Order type</param>
    /// <param name="fixedType">Fixed interest rate lending period</param>
    /// <param name="fixedRate">Fixed interest rate</param>
    /// <param name="autoRenew">Fixed interest rate auto-renewal</param>
    /// <param name="autoRepay">Fixed interest rate auto-repayment</param>
    /// <param name="orderId">Optional order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanOrderId>> PlaceOrderAsync(
        string borrowCurrency,
        decimal borrowAmount,
        IEnumerable<GateMultiCollateralLoanCurrencyAmount> collateralCurrencies = null,
        GateMultiCollateralLoanOrderType? orderType = null,
        GateMultiCollateralLoanFixedType? fixedType = null,
        decimal? fixedRate = null,
        bool? autoRenew = null,
        bool? autoRepay = null,
        long? orderId = null,
        CancellationToken ct = default)
        => PlaceOrderAsync(new GateMultiCollateralLoanOrderRequest
        {
            OrderId = orderId,
            OrderType = orderType,
            FixedType = fixedType,
            FixedRate = fixedRate,
            AutoRenew = autoRenew,
            AutoRepay = autoRepay,
            BorrowCurrency = borrowCurrency,
            BorrowAmount = borrowAmount,
            CollateralCurrencies = collateralCurrencies,
        }, ct);

    /// <summary>
    /// Place multi-currency collateral order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanOrderId>> PlaceOrderAsync(GateMultiCollateralLoanOrderRequest request, CancellationToken ct = default)
    {
        if (request.OrderType == GateMultiCollateralLoanOrderType.Fixed && (!request.FixedType.HasValue || !request.FixedRate.HasValue))
            throw new ArgumentException("FixedType and FixedRate are required for fixed rate orders");

        var parameters = new ParameterCollection
        {
            { "borrow_currency", request.BorrowCurrency },
        };
        if (request.OrderId.HasValue) parameters.Add("order_id", request.OrderId.Value.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalEnum("order_type", request.OrderType);
        parameters.AddOptionalEnum("fixed_type", request.FixedType);
        parameters.AddOptionalString("fixed_rate", request.FixedRate);
        parameters.AddOptional("auto_renew", request.AutoRenew);
        parameters.AddOptional("auto_repay", request.AutoRepay);
        parameters.AddString("borrow_amount", request.BorrowAmount);

        var collateralCurrencies = BuildCurrencyAmounts(request.CollateralCurrencies);
        if (collateralCurrencies?.Count > 0) parameters.Add("collateral_currencies", collateralCurrencies);

        return _.SendRequestInternal<GateMultiCollateralLoanOrderId>(_.GetUrl(api, v4, loan, $"{multiCollateral}/orders"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query order details
    /// </summary>
    /// <param name="orderId">Order ID returned when order is successfully created</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        => _.SendRequestInternal<GateMultiCollateralLoanOrder>(_.GetUrl(api, v4, loan, $"{multiCollateral}/orders".AppendPath(orderId.ToString())), HttpMethod.Get, ct, true);

    /// <summary>
    /// Query multi-currency collateral repayment records
    /// </summary>
    /// <param name="type">Operation type</param>
    /// <param name="borrowCurrency">Borrowed currency</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records returned in a single list</param>
    /// <param name="from">Start timestamp for the query</param>
    /// <param name="to">End timestamp for the query</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanRepaymentRecord>>> GetRepaymentRecordsAsync(
        GateMultiCollateralLoanRepaymentType type,
        string borrowCurrency = null,
        int? page = null,
        int? limit = null,
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default)
        => GetRepaymentRecordsAsync(new GateMultiCollateralLoanRepaymentRecordQueryRequest
        {
            Type = type,
            BorrowCurrency = borrowCurrency,
            Page = page,
            Limit = limit,
            From = from,
            To = to,
        }, ct);

    /// <summary>
    /// Query multi-currency collateral repayment records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanRepaymentRecord>>> GetRepaymentRecordsAsync(GateMultiCollateralLoanRepaymentRecordQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("type", request.Type);
        parameters.AddOptional("borrow_currency", request.BorrowCurrency);
        AddPaging(parameters, request.Page, request.Limit);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<List<GateMultiCollateralLoanRepaymentRecord>>(_.GetUrl(api, v4, loan, $"{multiCollateral}/repay"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Multi-currency collateral repayment
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="repayItems">Repay currency items</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanRepaymentResult>> RepayAsync(long orderId, IEnumerable<GateMultiCollateralLoanRepayItem> repayItems, CancellationToken ct = default)
        => RepayAsync(new GateMultiCollateralLoanRepayRequest { OrderId = orderId, RepayItems = repayItems }, ct);

    /// <summary>
    /// Multi-currency collateral repayment
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanRepaymentResult>> RepayAsync(GateMultiCollateralLoanRepayRequest request, CancellationToken ct = default)
    {
        var repayItems = BuildRepayItems(request.RepayItems);
        if (repayItems == null || repayItems.Count == 0)
            throw new ArgumentException("RepayItems must contain at least one item", nameof(request.RepayItems));

        var parameters = new ParameterCollection
        {
            { "order_id", request.OrderId },
            { "repay_items", repayItems },
        };

        return _.SendRequestInternal<GateMultiCollateralLoanRepaymentResult>(_.GetUrl(api, v4, loan, $"{multiCollateral}/repay"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query collateral adjustment records
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records returned in a single list</param>
    /// <param name="from">Start timestamp for the query</param>
    /// <param name="to">End timestamp for the query</param>
    /// <param name="collateralCurrency">Collateral currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanCollateralRecord>>> GetCollateralRecordsAsync(
        int? page = null,
        int? limit = null,
        DateTime? from = null,
        DateTime? to = null,
        string collateralCurrency = null,
        CancellationToken ct = default)
        => GetCollateralRecordsAsync(new GateMultiCollateralLoanCollateralRecordQueryRequest
        {
            Page = page,
            Limit = limit,
            From = from,
            To = to,
            CollateralCurrency = collateralCurrency,
        }, ct);

    /// <summary>
    /// Query collateral adjustment records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanCollateralRecord>>> GetCollateralRecordsAsync(GateMultiCollateralLoanCollateralRecordQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddPaging(parameters, request.Page, request.Limit);
        AddTimeRange(parameters, request.From, request.To);
        parameters.AddOptional("collateral_currency", request.CollateralCurrency);

        return _.SendRequestInternal<List<GateMultiCollateralLoanCollateralRecord>>(_.GetUrl(api, v4, loan, $"{multiCollateral}/mortgage"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Add or withdraw collateral
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="type">Operation type</param>
    /// <param name="collaterals">Collateral currency list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanCollateralAdjustmentResult>> AdjustCollateralAsync(
        long orderId,
        GateMultiCollateralLoanCollateralOperationType type,
        IEnumerable<GateMultiCollateralLoanCurrencyAmount> collaterals,
        CancellationToken ct = default)
        => AdjustCollateralAsync(new GateMultiCollateralLoanCollateralAdjustRequest { OrderId = orderId, Type = type, Collaterals = collaterals }, ct);

    /// <summary>
    /// Add or withdraw collateral
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanCollateralAdjustmentResult>> AdjustCollateralAsync(GateMultiCollateralLoanCollateralAdjustRequest request, CancellationToken ct = default)
    {
        var collaterals = BuildCurrencyAmounts(request.Collaterals);
        if (collaterals == null || collaterals.Count == 0)
            throw new ArgumentException("Collaterals must contain at least one item", nameof(request.Collaterals));

        var parameters = new ParameterCollection
        {
            { "order_id", request.OrderId },
            { "collaterals", collaterals },
        };
        parameters.AddEnum("type", request.Type);

        return _.SendRequestInternal<GateMultiCollateralLoanCollateralAdjustmentResult>(_.GetUrl(api, v4, loan, $"{multiCollateral}/mortgage"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Add collateral
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="collaterals">Collateral currency list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanCollateralAdjustmentResult>> AppendCollateralAsync(long orderId, IEnumerable<GateMultiCollateralLoanCurrencyAmount> collaterals, CancellationToken ct = default)
        => AdjustCollateralAsync(orderId, GateMultiCollateralLoanCollateralOperationType.Append, collaterals, ct);

    /// <summary>
    /// Withdraw collateral
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="collaterals">Collateral currency list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanCollateralAdjustmentResult>> RedeemCollateralAsync(long orderId, IEnumerable<GateMultiCollateralLoanCurrencyAmount> collaterals, CancellationToken ct = default)
        => AdjustCollateralAsync(orderId, GateMultiCollateralLoanCollateralOperationType.Redeem, collaterals, ct);

    /// <summary>
    /// Query user's collateral and borrowing currency quota information
    /// </summary>
    /// <param name="type">Currency type</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanCurrencyQuota>>> GetCurrencyQuotasAsync(GateMultiCollateralLoanCurrencyQuotaType type, string currency, CancellationToken ct = default)
        => GetCurrencyQuotasAsync(new GateMultiCollateralLoanCurrencyQuotaRequest { Type = type, Currencies = new[] { currency } }, ct);

    /// <summary>
    /// Query user's collateral and borrowing currency quota information
    /// </summary>
    /// <param name="type">Currency type</param>
    /// <param name="currencies">Currencies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanCurrencyQuota>>> GetCurrencyQuotasAsync(GateMultiCollateralLoanCurrencyQuotaType type, IEnumerable<string> currencies, CancellationToken ct = default)
        => GetCurrencyQuotasAsync(new GateMultiCollateralLoanCurrencyQuotaRequest { Type = type, Currencies = currencies }, ct);

    /// <summary>
    /// Query user's collateral and borrowing currency quota information
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanCurrencyQuota>>> GetCurrencyQuotasAsync(GateMultiCollateralLoanCurrencyQuotaRequest request, CancellationToken ct = default)
    {
        var currencies = NormalizeValues(request.Currencies, nameof(request.Currencies));
        if (request.Type == GateMultiCollateralLoanCurrencyQuotaType.Borrow && currencies.Count > 1)
            throw new ArgumentException("Borrow quota queries accept only one currency", nameof(request.Currencies));

        var parameters = new ParameterCollection
        {
            { "currency", string.Join(",", currencies) },
        };
        parameters.AddEnum("type", request.Type);

        return _.SendRequestInternal<List<GateMultiCollateralLoanCurrencyQuota>>(_.GetUrl(api, v4, loan, $"{multiCollateral}/currency_quota"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query borrow currencies and collateral currencies supported by multi-currency collateral
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanCurrencies>> GetCurrenciesAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateMultiCollateralLoanCurrencies>(_.GetUrl(api, v4, loan, $"{multiCollateral}/currencies"), HttpMethod.Get, ct);

    /// <summary>
    /// Query collateralization ratio information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateMultiCollateralLoanLtv>> GetLtvAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateMultiCollateralLoanLtv>(_.GetUrl(api, v4, loan, $"{multiCollateral}/ltv"), HttpMethod.Get, ct);

    /// <summary>
    /// Query currency's 7-day and 30-day fixed interest rates
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanFixedRate>>> GetFixedRatesAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateMultiCollateralLoanFixedRate>>(_.GetUrl(api, v4, loan, $"{multiCollateral}/fixed_rate"), HttpMethod.Get, ct);

    /// <summary>
    /// Query currency's current interest rate
    /// </summary>
    /// <param name="currencies">Currency names, maximum 100</param>
    /// <param name="vipLevel">VIP level. Defaults to 0 if not specified.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanCurrentRate>>> GetCurrentRatesAsync(IEnumerable<string> currencies, string vipLevel = null, CancellationToken ct = default)
        => GetCurrentRatesAsync(new GateMultiCollateralLoanCurrentRateRequest { Currencies = currencies, VipLevel = vipLevel }, ct);

    /// <summary>
    /// Query currency's current interest rate
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateMultiCollateralLoanCurrentRate>>> GetCurrentRatesAsync(GateMultiCollateralLoanCurrentRateRequest request, CancellationToken ct = default)
    {
        var currencies = NormalizeValues(request.Currencies, nameof(request.Currencies), 100);

        var parameters = new ParameterCollection
        {
            { "currencies", string.Join(",", currencies) },
        };
        parameters.AddOptional("vip_level", request.VipLevel);

        return _.SendRequestInternal<List<GateMultiCollateralLoanCurrentRate>>(_.GetUrl(api, v4, loan, $"{multiCollateral}/current_rate"), HttpMethod.Get, ct, queryParameters: parameters);
    }
}
