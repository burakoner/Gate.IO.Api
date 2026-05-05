namespace Gate.IO.Api.Earn;

/// <summary>
/// Gate.IO Earn REST API Client
/// </summary>
public class GateEarnRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string earn = "earn";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateEarnRestApiClient(GateRestApiClient root) => _ = root;

    private static void AddTimeRange(ParameterCollection parameters, DateTime? from, DateTime? to)
    {
        parameters.AddOptional("from", from?.ConvertToSeconds());
        parameters.AddOptional("to", to?.ConvertToSeconds());
    }

    private static void AddPageSize(ParameterCollection parameters, long? page, long? pageSize)
    {
        parameters.AddOptional("page", page);
        parameters.AddOptional("page_size", pageSize);
    }

    private static List<Dictionary<string, object>> BuildAutoInvestItems(IEnumerable<GateEarnAutoInvestPortfolioItem> items)
    {
        return items?
            .Where(x => !string.IsNullOrWhiteSpace(x.Asset))
            .Select(x => new Dictionary<string, object>
            {
                { "asset", x.Asset },
                { "ratio", x.Ratio.ToString(CultureInfo.InvariantCulture) },
            })
            .ToList();
    }

    private async Task<RestCallResult<T>> SendFixedTermDataRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        bool signed = false,
        ParameterCollection queryParameters = null,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await _.SendRequestInternal<GateEarnFixedTermResponse<T>>(_.GetUrl(api, v4, earn, endpoint), method, ct, signed, queryParameters, bodyParameters).ConfigureAwait(false);
        if (!result.Success) return result.As<T>(default);

        var data = result.Data?.Data;
        if (data == null && typeof(T) == typeof(object))
            data = (T)(object)new object();

        return result.As(data);
    }

    /// <summary>
    /// Dual Investment product list
    /// </summary>
    /// <param name="planId">Financial project ID</param>
    /// <param name="coin">Investment token</param>
    /// <param name="type">Product type</param>
    /// <param name="quoteCurrency">Settlement currency. Defaults to USDT; GUSD optional</param>
    /// <param name="sort">Sort field</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Items per page</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnDualPlan>>> GetDualInvestmentPlansAsync(
        long? planId = null,
        string coin = null,
        GateEarnDualOptionType? type = null,
        string quoteCurrency = null,
        GateEarnDualPlanSort? sort = null,
        int? page = null,
        int? pageSize = null,
        CancellationToken ct = default)
        => GetDualInvestmentPlansAsync(new GateEarnDualPlanQueryRequest
        {
            PlanId = planId,
            Coin = coin,
            Type = type,
            QuoteCurrency = quoteCurrency,
            Sort = sort,
            Page = page,
            PageSize = pageSize,
        }, ct);

    /// <summary>
    /// Dual Investment product list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnDualPlan>>> GetDualInvestmentPlansAsync(GateEarnDualPlanQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("plan_id", request.PlanId);
        parameters.AddOptional("coin", request.Coin);
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptional("quote_currency", request.QuoteCurrency);
        parameters.AddOptionalEnum("sort", request.Sort);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("page_size", request.PageSize);

        return _.SendRequestInternal<List<GateEarnDualPlan>>(_.GetUrl(api, v4, earn, "dual/investment_plan"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Dual Investment order list
    /// </summary>
    /// <param name="from">Start settlement time</param>
    /// <param name="to">End settlement time</param>
    /// <param name="type">Product type</param>
    /// <param name="status">Order status</param>
    /// <param name="coin">Investment token</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records returned in a single list</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnDualOrder>>> GetDualInvestmentOrdersAsync(
        DateTime? from = null,
        DateTime? to = null,
        GateEarnDualOptionType? type = null,
        GateEarnDualOrderQueryStatus? status = null,
        string coin = null,
        int? page = null,
        int? limit = null,
        CancellationToken ct = default)
        => GetDualInvestmentOrdersAsync(new GateEarnDualOrderQueryRequest
        {
            From = from,
            To = to,
            Type = type,
            Status = status,
            Coin = coin,
            Page = page,
            Limit = limit,
        }, ct);

    /// <summary>
    /// Dual Investment order list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnDualOrder>>> GetDualInvestmentOrdersAsync(GateEarnDualOrderQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddTimeRange(parameters, request.From, request.To);
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptionalEnum("status", request.Status);
        parameters.AddOptional("coin", request.Coin);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);

        return _.SendRequestInternal<List<GateEarnDualOrder>>(_.GetUrl(api, v4, earn, "dual/orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Place Dual Investment order
    /// </summary>
    /// <param name="planId">Product ID</param>
    /// <param name="amount">Subscription amount</param>
    /// <param name="text">Custom order information</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnDualOrder>> PlaceDualInvestmentOrderAsync(long planId, decimal amount, string text = null, CancellationToken ct = default)
        => PlaceDualInvestmentOrderAsync(new GateEarnDualOrderRequest { PlanId = planId, Amount = amount, Text = text }, ct);

    /// <summary>
    /// Place Dual Investment order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnDualOrder>> PlaceDualInvestmentOrderAsync(GateEarnDualOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "plan_id", request.PlanId.ToString(CultureInfo.InvariantCulture) },
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddOptional("text", request.Text);

        return _.SendRequestInternal<GateEarnDualOrder>(_.GetUrl(api, v4, earn, "dual/orders"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Dual-Currency Earning Assets
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnDualBalance>> GetDualInvestmentBalanceAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateEarnDualBalance>(_.GetUrl(api, v4, earn, "dual/balance"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Dual-currency early redemption preview
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnDualRefundPreview>> GetDualInvestmentRefundPreviewAsync(long orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "order_id", orderId.ToString(CultureInfo.InvariantCulture) },
        };

        return _.SendRequestInternal<GateEarnDualRefundPreview>(_.GetUrl(api, v4, earn, "dual/order-refund-preview"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Dual-currency order early redemption
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="requestId">Request ID returned by order-refund-preview</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> RefundDualInvestmentOrderAsync(long orderId, string requestId, CancellationToken ct = default)
        => RefundDualInvestmentOrderAsync(new GateEarnDualRefundRequest { OrderId = orderId, RequestId = requestId }, ct);

    /// <summary>
    /// Dual-currency order early redemption
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> RefundDualInvestmentOrderAsync(GateEarnDualRefundRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "order_id", request.OrderId.ToString(CultureInfo.InvariantCulture) },
            { "req_id", request.RequestId },
        };

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, earn, "dual/order-refund"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Modify dual-currency order reinvest
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="status">Reinvest status. 0: off, 1: on</param>
    /// <param name="effectiveTimeDuration">Effective duration in seconds</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdateDualInvestmentReinvestAsync(long? orderId = null, int? status = null, long? effectiveTimeDuration = null, CancellationToken ct = default)
        => UpdateDualInvestmentReinvestAsync(new GateEarnDualReinvestUpdateRequest { OrderId = orderId, Status = status, EffectiveTimeDuration = effectiveTimeDuration }, ct);

    /// <summary>
    /// Modify dual-currency order reinvest
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdateDualInvestmentReinvestAsync(GateEarnDualReinvestUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("order_id", request.OrderId);
        parameters.AddOptional("status", request.Status);
        parameters.AddOptional("effective_time_duration", request.EffectiveTimeDuration);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, earn, "dual/modify-order-reinvest"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Dual-currency recommended projects
    /// </summary>
    /// <param name="mode">Sort mode</param>
    /// <param name="coin">Investment token</param>
    /// <param name="type">Product type</param>
    /// <param name="historyProductIds">Project IDs to exclude</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnDualRecommendation>>> GetDualInvestmentRecommendationsAsync(
        GateEarnDualRecommendationMode? mode = null,
        string coin = null,
        GateEarnDualOptionType? type = null,
        IEnumerable<long> historyProductIds = null,
        CancellationToken ct = default)
        => GetDualInvestmentRecommendationsAsync(new GateEarnDualRecommendationRequest { Mode = mode, Coin = coin, Type = type, HistoryProductIds = historyProductIds }, ct);

    /// <summary>
    /// Dual-currency recommended projects
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnDualRecommendation>>> GetDualInvestmentRecommendationsAsync(GateEarnDualRecommendationRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("mode", request.Mode);
        parameters.AddOptional("coin", request.Coin);
        parameters.AddOptionalEnum("type", request.Type);
        if (request.HistoryProductIds != null) parameters.Add("history_pids", string.Join(",", request.HistoryProductIds));

        return _.SendRequestInternal<List<GateEarnDualRecommendation>>(_.GetUrl(api, v4, earn, "dual/project-recommend"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Staking coins
    /// </summary>
    /// <param name="coinType">Currency type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnStakingCoin>>> GetStakingCoinsAsync(GateEarnStakingCoinType? coinType = null, CancellationToken ct = default)
        => GetStakingCoinsAsync(new GateEarnStakingCoinQueryRequest { CoinType = coinType }, ct);

    /// <summary>
    /// Staking coins
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnStakingCoin>>> GetStakingCoinsAsync(GateEarnStakingCoinQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("cointype", request.CoinType);

        return _.SendRequestInternal<List<GateEarnStakingCoin>>(_.GetUrl(api, v4, earn, "staking/coins"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// On-chain token swap for earned coins
    /// </summary>
    /// <param name="coin">Currency</param>
    /// <param name="side">Operation type</param>
    /// <param name="amount">Size</param>
    /// <param name="productId">DeFi-type mining protocol identifier</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnStakingSwap>> SwapStakingCoinAsync(string coin, GateEarnStakingOperationType side, decimal amount, long? productId = null, CancellationToken ct = default)
        => SwapStakingCoinAsync(new GateEarnStakingSwapRequest { Coin = coin, Side = side, Amount = amount, ProductId = productId }, ct);

    /// <summary>
    /// On-chain token swap for earned coins
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnStakingSwap>> SwapStakingCoinAsync(GateEarnStakingSwapRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "coin", request.Coin },
            { "side", (int)request.Side },
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddOptional("pid", request.ProductId);

        return _.SendRequestInternal<GateEarnStakingSwap>(_.GetUrl(api, v4, earn, "staking/swap"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List of on-chain coin-earning orders
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="coin">Currency name</param>
    /// <param name="type">Operation type</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnStakingOrderPage>> GetStakingOrdersAsync(long? productId = null, string coin = null, GateEarnStakingOperationType? type = null, int? page = null, CancellationToken ct = default)
        => GetStakingOrdersAsync(new GateEarnStakingOrderQueryRequest { ProductId = productId, Coin = coin, Type = type, Page = page }, ct);

    /// <summary>
    /// List of on-chain coin-earning orders
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnStakingOrderPage>> GetStakingOrdersAsync(GateEarnStakingOrderQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("pid", request.ProductId);
        parameters.AddOptional("coin", request.Coin);
        if (request.Type.HasValue) parameters.Add("type", (int)request.Type.Value);
        parameters.AddOptional("page", request.Page);

        return _.SendRequestInternal<GateEarnStakingOrderPage>(_.GetUrl(api, v4, earn, "staking/order_list"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// On-chain coin-earning dividend records
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="coin">Currency name</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnStakingAwardPage>> GetStakingAwardsAsync(long? productId = null, string coin = null, int? page = null, CancellationToken ct = default)
        => GetStakingAwardsAsync(new GateEarnStakingAwardQueryRequest { ProductId = productId, Coin = coin, Page = page }, ct);

    /// <summary>
    /// On-chain coin-earning dividend records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnStakingAwardPage>> GetStakingAwardsAsync(GateEarnStakingAwardQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("pid", request.ProductId);
        parameters.AddOptional("coin", request.Coin);
        parameters.AddOptional("page", request.Page);

        return _.SendRequestInternal<GateEarnStakingAwardPage>(_.GetUrl(api, v4, earn, "staking/award_list"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// On-chain coin-earning assets
    /// </summary>
    /// <param name="coin">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnStakingAsset>>> GetStakingAssetsAsync(string coin = null, CancellationToken ct = default)
        => GetStakingAssetsAsync(new GateEarnStakingAssetQueryRequest { Coin = coin }, ct);

    /// <summary>
    /// On-chain coin-earning assets
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnStakingAsset>>> GetStakingAssetsAsync(GateEarnStakingAssetQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("coin", request.Coin);

        return _.SendRequestInternal<List<GateEarnStakingAsset>>(_.GetUrl(api, v4, earn, "staking/assets"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Create auto invest plan
    /// </summary>
    /// <param name="planMoney">Pricing currency</param>
    /// <param name="planAmount">Per-period auto invest amount</param>
    /// <param name="periodType">Cycle type</param>
    /// <param name="periodDay">Cycle day</param>
    /// <param name="periodHour">Execution hour</param>
    /// <param name="items">Investment portfolio</param>
    /// <param name="planName">Plan name</param>
    /// <param name="planDescription">Plan description</param>
    /// <param name="fundSource">Fund source</param>
    /// <param name="fundFlow">Fund flow direction</param>
    /// <param name="type">Creation type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestPlanCreated>> CreateAutoInvestPlanAsync(
        string planMoney,
        decimal planAmount,
        GateEarnAutoInvestPeriodType periodType,
        long periodDay,
        long periodHour,
        IEnumerable<GateEarnAutoInvestPortfolioItem> items,
        string planName = null,
        string planDescription = null,
        GateEarnAutoInvestFundSource? fundSource = null,
        GateEarnAutoInvestFundFlow? fundFlow = null,
        GateEarnAutoInvestCreationType? type = null,
        CancellationToken ct = default)
        => CreateAutoInvestPlanAsync(new GateEarnAutoInvestPlanCreateRequest
        {
            PlanName = planName,
            PlanDescription = planDescription,
            PlanMoney = planMoney,
            PlanAmount = planAmount,
            PeriodType = periodType,
            PeriodDay = periodDay,
            PeriodHour = periodHour,
            Items = items,
            FundSource = fundSource,
            FundFlow = fundFlow,
            Type = type,
        }, ct);

    /// <summary>
    /// Create auto invest plan
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestPlanCreated>> CreateAutoInvestPlanAsync(GateEarnAutoInvestPlanCreateRequest request, CancellationToken ct = default)
    {
        var items = BuildAutoInvestItems(request.Items);
        if (items == null || items.Count == 0)
            throw new ArgumentException("Items must contain at least one portfolio item", nameof(request.Items));

        var parameters = new ParameterCollection
        {
            { "plan_money", request.PlanMoney },
            { "items", items },
            { "plan_period_day", request.PeriodDay },
            { "plan_period_hour", request.PeriodHour },
        };
        parameters.AddOptional("plan_name", request.PlanName);
        parameters.AddOptional("plan_des", request.PlanDescription);
        parameters.AddString("plan_amount", request.PlanAmount);
        parameters.AddEnum("plan_period_type", request.PeriodType);
        parameters.AddOptionalEnum("fund_source", request.FundSource);
        parameters.AddOptionalEnum("fund_flow", request.FundFlow);
        if (request.Type.HasValue) parameters.Add("type", (int)request.Type.Value);

        return _.SendRequestInternal<GateEarnAutoInvestPlanCreated>(_.GetUrl(api, v4, earn, "autoinvest/plans/create"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Update auto invest plan
    /// </summary>
    /// <param name="planId">Plan ID</param>
    /// <param name="fundSource">Fund source</param>
    /// <param name="fundFlow">Fund flow direction</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdateAutoInvestPlanAsync(long planId, GateEarnAutoInvestFundSource? fundSource = null, GateEarnAutoInvestFundFlow? fundFlow = null, CancellationToken ct = default)
        => UpdateAutoInvestPlanAsync(new GateEarnAutoInvestPlanUpdateRequest { PlanId = planId, FundSource = fundSource, FundFlow = fundFlow }, ct);

    /// <summary>
    /// Update auto invest plan
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdateAutoInvestPlanAsync(GateEarnAutoInvestPlanUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "plan_id", request.PlanId },
        };
        parameters.AddOptionalEnum("fund_source", request.FundSource);
        parameters.AddOptionalEnum("fund_flow", request.FundFlow);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, earn, "autoinvest/plans/update"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Stop auto invest plan
    /// </summary>
    /// <param name="planId">Plan ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> StopAutoInvestPlanAsync(long planId, CancellationToken ct = default)
        => StopAutoInvestPlanAsync(new GateEarnAutoInvestPlanStopRequest { PlanId = planId }, ct);

    /// <summary>
    /// Stop auto invest plan
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> StopAutoInvestPlanAsync(GateEarnAutoInvestPlanStopRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "plan_id", request.PlanId },
        };

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, earn, "autoinvest/plans/stop"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Add position immediately
    /// </summary>
    /// <param name="planId">Plan ID</param>
    /// <param name="amount">Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> AddAutoInvestPositionAsync(long planId, decimal amount, CancellationToken ct = default)
        => AddAutoInvestPositionAsync(new GateEarnAutoInvestAddPositionRequest { PlanId = planId, Amount = amount }, ct);

    /// <summary>
    /// Add position immediately
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> AddAutoInvestPositionAsync(GateEarnAutoInvestAddPositionRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "plan_id", request.PlanId },
        };
        parameters.AddString("amount", request.Amount);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, earn, "autoinvest/plans/add_position"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query currencies supporting auto invest
    /// </summary>
    /// <param name="planMoney">Pricing currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnAutoInvestCoin>>> GetAutoInvestCoinsAsync(string planMoney = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("plan_money", planMoney);

        return _.SendRequestInternal<List<GateEarnAutoInvestCoin>>(_.GetUrl(api, v4, earn, "autoinvest/coins"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get minimum investment amount
    /// </summary>
    /// <param name="money">Currency</param>
    /// <param name="items">Investment portfolio</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestMinimumAmount>> GetAutoInvestMinimumAmountAsync(string money, IEnumerable<GateEarnAutoInvestPortfolioItem> items, CancellationToken ct = default)
        => GetAutoInvestMinimumAmountAsync(new GateEarnAutoInvestMinInvestAmountRequest { Money = money, Items = items }, ct);

    /// <summary>
    /// Get minimum investment amount
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestMinimumAmount>> GetAutoInvestMinimumAmountAsync(GateEarnAutoInvestMinInvestAmountRequest request, CancellationToken ct = default)
    {
        var items = BuildAutoInvestItems(request.Items);
        if (items == null || items.Count == 0)
            throw new ArgumentException("Items must contain at least one portfolio item", nameof(request.Items));

        var parameters = new ParameterCollection
        {
            { "money", request.Money },
            { "items", items },
        };

        return _.SendRequestInternal<GateEarnAutoInvestMinimumAmount>(_.GetUrl(api, v4, earn, "autoinvest/min_invest_amount"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List plan execution records
    /// </summary>
    /// <param name="planId">Plan ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Items per page</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestExecutionRecordPage>> GetAutoInvestExecutionRecordsAsync(long planId, long? page = null, long? pageSize = null, CancellationToken ct = default)
        => GetAutoInvestExecutionRecordsAsync(new GateEarnAutoInvestExecutionRecordsRequest { PlanId = planId, Page = page, PageSize = pageSize }, ct);

    /// <summary>
    /// List plan execution records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestExecutionRecordPage>> GetAutoInvestExecutionRecordsAsync(GateEarnAutoInvestExecutionRecordsRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "plan_id", request.PlanId },
        };
        AddPageSize(parameters, request.Page, request.PageSize);

        return _.SendRequestInternal<GateEarnAutoInvestExecutionRecordPage>(_.GetUrl(api, v4, earn, "autoinvest/plans/records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List plan execution record details
    /// </summary>
    /// <param name="planId">Plan ID</param>
    /// <param name="recordId">Record ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnAutoInvestOrder>>> GetAutoInvestOrderDetailsAsync(long planId, long recordId, CancellationToken ct = default)
        => GetAutoInvestOrderDetailsAsync(new GateEarnAutoInvestOrderDetailsRequest { PlanId = planId, RecordId = recordId }, ct);

    /// <summary>
    /// List plan execution record details
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnAutoInvestOrder>>> GetAutoInvestOrderDetailsAsync(GateEarnAutoInvestOrderDetailsRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "plan_id", request.PlanId },
            { "record_id", request.RecordId },
        };

        return _.SendRequestInternal<List<GateEarnAutoInvestOrder>>(_.GetUrl(api, v4, earn, "autoinvest/orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List investment currency configuration
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateEarnAutoInvestConfig>>> GetAutoInvestConfigAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateEarnAutoInvestConfig>>(_.GetUrl(api, v4, earn, "autoinvest/config"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Query auto invest plan details
    /// </summary>
    /// <param name="planId">Plan ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestPlan>> GetAutoInvestPlanAsync(long planId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "plan_id", planId },
        };

        return _.SendRequestInternal<GateEarnAutoInvestPlan>(_.GetUrl(api, v4, earn, "autoinvest/plans/detail"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query auto invest plan list
    /// </summary>
    /// <param name="status">Plan status</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Items per page</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestPlanPage>> GetAutoInvestPlansAsync(GateEarnAutoInvestPlanStatus status, long? page = null, long? pageSize = null, CancellationToken ct = default)
        => GetAutoInvestPlansAsync(new GateEarnAutoInvestPlanListRequest { Status = status, Page = page, PageSize = pageSize }, ct);

    /// <summary>
    /// Query auto invest plan list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnAutoInvestPlanPage>> GetAutoInvestPlansAsync(GateEarnAutoInvestPlanListRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("status", request.Status);
        AddPageSize(parameters, request.Page, request.PageSize);

        return _.SendRequestInternal<GateEarnAutoInvestPlanPage>(_.GetUrl(api, v4, earn, "autoinvest/plans/list_info"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get fixed-term Earn product list
    /// </summary>
    /// <param name="asset">Currency</param>
    /// <param name="type">Product type</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Page size</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermProductPage>> GetFixedTermProductsAsync(string asset = null, GateEarnFixedTermProductType? type = null, int page = 1, int limit = 100, CancellationToken ct = default)
        => GetFixedTermProductsAsync(new GateEarnFixedTermProductQueryRequest { Asset = asset, Type = type, Page = page, Limit = limit }, ct);

    /// <summary>
    /// Get fixed-term Earn product list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermProductPage>> GetFixedTermProductsAsync(GateEarnFixedTermProductQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "page", request.Page },
            { "limit", request.Limit },
        };
        parameters.AddOptional("asset", request.Asset);
        if (request.Type.HasValue) parameters.Add("type", (int)request.Type.Value);

        return SendFixedTermDataRequestAsync<GateEarnFixedTermProductPage>("fixed-term/product", HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Get fixed-term Earn product list by single currency
    /// </summary>
    /// <param name="asset">Currency name</param>
    /// <param name="type">Product type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermProductSimpleList>> GetFixedTermProductsByAssetAsync(string asset, GateEarnFixedTermProductType? type = null, CancellationToken ct = default)
        => GetFixedTermProductsByAssetAsync(new GateEarnFixedTermProductByAssetRequest { Asset = asset, Type = type }, ct);

    /// <summary>
    /// Get fixed-term Earn product list by single currency
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermProductSimpleList>> GetFixedTermProductsByAssetAsync(GateEarnFixedTermProductByAssetRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (request.Type.HasValue) parameters.Add("type", (int)request.Type.Value);

        return SendFixedTermDataRequestAsync<GateEarnFixedTermProductSimpleList>($"fixed-term/product/{request.Asset}/list", HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Fixed-term Earn subscription list
    /// </summary>
    /// <param name="orderType">Order type</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Page size</param>
    /// <param name="productId">Product ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="asset">Currency</param>
    /// <param name="subBusiness">Sub-business type</param>
    /// <param name="businessFilter">Business filter JSON</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermLendPage>> GetFixedTermLendsAsync(
        GateEarnFixedTermOrderType orderType,
        int page = 1,
        int limit = 100,
        long? productId = null,
        long? orderId = null,
        string asset = null,
        int? subBusiness = null,
        string businessFilter = null,
        CancellationToken ct = default)
        => GetFixedTermLendsAsync(new GateEarnFixedTermLendQueryRequest
        {
            ProductId = productId,
            OrderId = orderId,
            Asset = asset,
            OrderType = orderType,
            Page = page,
            Limit = limit,
            SubBusiness = subBusiness,
            BusinessFilter = businessFilter,
        }, ct);

    /// <summary>
    /// Fixed-term Earn subscription list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermLendPage>> GetFixedTermLendsAsync(GateEarnFixedTermLendQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "order_type", ((int)request.OrderType).ToString(CultureInfo.InvariantCulture) },
            { "page", request.Page },
            { "limit", request.Limit },
        };
        parameters.AddOptional("product_id", request.ProductId);
        parameters.AddOptional("order_id", request.OrderId);
        parameters.AddOptional("asset", request.Asset);
        parameters.AddOptional("sub_business", request.SubBusiness);
        parameters.AddOptional("business_filter", request.BusinessFilter);

        return SendFixedTermDataRequestAsync<GateEarnFixedTermLendPage>("fixed-term/user/lend", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Subscribe to a fixed-term Earn product
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="amount">Subscription amount</param>
    /// <param name="yearRate">Annual interest rate</param>
    /// <param name="reinvestStatus">Auto-renewal status</param>
    /// <param name="redeemAccountType">Redemption payout account type</param>
    /// <param name="financialRateId">Interest rate boost coupon ID</param>
    /// <param name="subBusiness">Sub-business type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermLendResult>> CreateFixedTermLendAsync(
        long productId,
        decimal amount,
        decimal? yearRate = null,
        int? reinvestStatus = null,
        int? redeemAccountType = null,
        long? financialRateId = null,
        int? subBusiness = null,
        CancellationToken ct = default)
        => CreateFixedTermLendAsync(new GateEarnFixedTermLendRequest
        {
            ProductId = productId,
            Amount = amount,
            YearRate = yearRate,
            ReinvestStatus = reinvestStatus,
            RedeemAccountType = redeemAccountType,
            FinancialRateId = financialRateId,
            SubBusiness = subBusiness,
        }, ct);

    /// <summary>
    /// Subscribe to a fixed-term Earn product
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermLendResult>> CreateFixedTermLendAsync(GateEarnFixedTermLendRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "product_id", request.ProductId },
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddOptionalString("year_rate", request.YearRate);
        parameters.AddOptional("reinvest_status", request.ReinvestStatus);
        parameters.AddOptional("redeem_account_type", request.RedeemAccountType);
        parameters.AddOptional("financial_rate_id", request.FinancialRateId);
        parameters.AddOptional("sub_business", request.SubBusiness);

        return SendFixedTermDataRequestAsync<GateEarnFixedTermLendResult>("fixed-term/user/lend", HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Early redemption of a fixed-term Earn order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> RedeemFixedTermOrderAsync(long orderId, CancellationToken ct = default)
        => RedeemFixedTermOrderAsync(new GateEarnFixedTermPreRedeemRequest { OrderId = orderId }, ct);

    /// <summary>
    /// Early redemption of a fixed-term Earn order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> RedeemFixedTermOrderAsync(GateEarnFixedTermPreRedeemRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "order_id", request.OrderId.ToString(CultureInfo.InvariantCulture) },
        };

        return SendFixedTermDataRequestAsync<object>("fixed-term/user/pre-redeem", HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Fixed-term Earn subscription history
    /// </summary>
    /// <param name="type">History type</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Page size</param>
    /// <param name="productId">Product ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="asset">Currency</param>
    /// <param name="startAt">Start timestamp</param>
    /// <param name="endAt">End timestamp</param>
    /// <param name="subBusiness">Sub-business type</param>
    /// <param name="businessFilter">Business filter JSON</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermHistoryPage>> GetFixedTermHistoryAsync(
        GateEarnFixedTermHistoryType type,
        int page = 1,
        int limit = 100,
        long? productId = null,
        long? orderId = null,
        string asset = null,
        DateTime? startAt = null,
        DateTime? endAt = null,
        int? subBusiness = null,
        string businessFilter = null,
        CancellationToken ct = default)
        => GetFixedTermHistoryAsync(new GateEarnFixedTermHistoryRequest
        {
            ProductId = productId,
            OrderId = orderId,
            Asset = asset,
            Type = type,
            Page = page,
            Limit = limit,
            StartAt = startAt,
            EndAt = endAt,
            SubBusiness = subBusiness,
            BusinessFilter = businessFilter,
        }, ct);

    /// <summary>
    /// Fixed-term Earn subscription history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateEarnFixedTermHistoryPage>> GetFixedTermHistoryAsync(GateEarnFixedTermHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "type", ((int)request.Type).ToString(CultureInfo.InvariantCulture) },
            { "page", request.Page },
            { "limit", request.Limit },
        };
        parameters.AddOptional("product_id", request.ProductId);
        parameters.AddOptional("order_id", request.OrderId);
        parameters.AddOptional("asset", request.Asset);
        parameters.AddOptional("start_at", request.StartAt?.ConvertToSeconds());
        parameters.AddOptional("end_at", request.EndAt?.ConvertToSeconds());
        parameters.AddOptional("sub_business", request.SubBusiness);
        parameters.AddOptional("business_filter", request.BusinessFilter);

        return SendFixedTermDataRequestAsync<GateEarnFixedTermHistoryPage>("fixed-term/user/history", HttpMethod.Get, ct, true, parameters);
    }
}
