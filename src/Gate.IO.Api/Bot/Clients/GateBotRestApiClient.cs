namespace Gate.IO.Api.Bot;

/// <summary>
/// Gate.IO Bot REST API Client
/// </summary>
public class GateBotRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string bot = "bot";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateBotRestApiClient(GateRestApiClient root) => _ = root;

    private static string ToStringInvariant<T>(T value)
        => Convert.ToString(value, CultureInfo.InvariantCulture);

    private static Dictionary<string, string> CreateHeaders(GateBotRequestHeaders request)
    {
        if (request == null)
            return null;

        var headers = new Dictionary<string, string>();
        AddOptional(headers, "X-Gate-Service-Id", request.ServiceId);
        AddOptional(headers, "X-Gate-AppLang", request.AppLanguage);
        AddOptional(headers, "X-Request-Id", request.RequestId);
        AddOptional(headers, "X-Trace-Id", request.TraceId);

        return headers.Count == 0 ? null : headers;
    }

    private static void AddOptional(Dictionary<string, string> parameters, string key, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            parameters.Add(key, value);
    }

    private static void AddOptional(Dictionary<string, object> parameters, string key, object value)
    {
        if (value != null)
            parameters.Add(key, value);
    }

    private static void AddOptionalString(Dictionary<string, object> parameters, string key, decimal? value)
    {
        if (value.HasValue)
            parameters.Add(key, ToStringInvariant(value.Value));
    }

    private static string FormatDirection(GateBotFuturesDirection value)
        => value switch
        {
            GateBotFuturesDirection.Long => "long",
            GateBotFuturesDirection.Short => "short",
            GateBotFuturesDirection.Neutral => "neutral",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
        };

    private static string FormatDirection(GateBotContractMartingaleDirection value)
        => value switch
        {
            GateBotContractMartingaleDirection.Buy => "buy",
            GateBotContractMartingaleDirection.Sell => "sell",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
        };

    private static void AddOptionalGridParameters(Dictionary<string, object> parameters, decimal? triggerPrice, decimal? stopProfit, decimal? stopLoss, decimal? profitSharingRatio, bool? isUseBase)
    {
        AddOptionalString(parameters, "trigger_price", triggerPrice);
        AddOptionalString(parameters, "stop_profit", stopProfit);
        AddOptionalString(parameters, "stop_loss", stopLoss);
        AddOptionalString(parameters, "profit_sharing_ratio", profitSharingRatio);
        AddOptional(parameters, "is_use_base", isUseBase);
    }

    private static ParameterCollection CreateStrategyBody(GateBotStrategyType strategyType, string market, Dictionary<string, object> createParameters)
    {
        var parameters = new ParameterCollection
        {
            { "market", market },
            { "create_params", createParameters },
        };
        parameters.AddEnum("strategy_type", strategyType);

        return parameters;
    }

    private static Dictionary<string, object> CreateSpotGridParameters(GateBotSpotGridCreateParameters request)
    {
        var parameters = new Dictionary<string, object>
        {
            { "money", ToStringInvariant(request.Money) },
            { "low_price", ToStringInvariant(request.LowPrice) },
            { "high_price", ToStringInvariant(request.HighPrice) },
            { "grid_num", request.GridNumber },
            { "price_type", (int)request.PriceType },
        };
        AddOptionalGridParameters(parameters, request.TriggerPrice, request.StopProfit, request.StopLoss, request.ProfitSharingRatio, request.IsUseBase);

        return parameters;
    }

    private static Dictionary<string, object> CreateMarginGridParameters(GateBotMarginGridCreateParameters request)
    {
        var parameters = new Dictionary<string, object>
        {
            { "money", ToStringInvariant(request.Money) },
            { "low_price", ToStringInvariant(request.LowPrice) },
            { "high_price", ToStringInvariant(request.HighPrice) },
            { "grid_num", request.GridNumber },
            { "price_type", (int)request.PriceType },
            { "leverage", ToStringInvariant(request.Leverage) },
        };
        if (request.Direction.HasValue) parameters.Add("direction", FormatDirection(request.Direction.Value));
        AddOptionalGridParameters(parameters, request.TriggerPrice, request.StopProfit, request.StopLoss, request.ProfitSharingRatio, request.IsUseBase);

        return parameters;
    }

    private static Dictionary<string, object> CreateInfiniteGridParameters(GateBotInfiniteGridCreateParameters request)
    {
        var parameters = new Dictionary<string, object>
        {
            { "money", ToStringInvariant(request.Money) },
            { "price_floor", ToStringInvariant(request.PriceFloor) },
            { "profit_per_grid", ToStringInvariant(request.ProfitPerGrid) },
        };
        AddOptional(parameters, "grid_num", request.GridNumber);
        if (request.PriceType.HasValue) parameters.Add("price_type", (int)request.PriceType.Value);
        AddOptionalGridParameters(parameters, request.TriggerPrice, request.StopProfit, request.StopLoss, request.ProfitSharingRatio, request.IsUseBase);

        return parameters;
    }

    private static Dictionary<string, object> CreateSpotMartingaleParameters(GateBotSpotMartingaleCreateParameters request)
    {
        var parameters = new Dictionary<string, object>
        {
            { "invest_amount", ToStringInvariant(request.InvestAmount) },
            { "price_deviation", ToStringInvariant(request.PriceDeviation) },
            { "max_orders", request.MaxOrders },
            { "take_profit_ratio", ToStringInvariant(request.TakeProfitRatio) },
        };
        AddOptionalString(parameters, "stop_loss_per_cycle", request.StopLossPerCycle);
        AddOptionalString(parameters, "trigger_price", request.TriggerPrice);
        AddOptionalString(parameters, "profit_sharing_ratio", request.ProfitSharingRatio);

        return parameters;
    }

    private static Dictionary<string, object> CreateContractMartingaleParameters(GateBotContractMartingaleCreateParameters request)
    {
        var parameters = new Dictionary<string, object>
        {
            { "invest_amount", ToStringInvariant(request.InvestAmount) },
            { "price_deviation", ToStringInvariant(request.PriceDeviation) },
            { "max_orders", request.MaxOrders },
            { "take_profit_ratio", ToStringInvariant(request.TakeProfitRatio) },
            { "direction", FormatDirection(request.Direction) },
            { "leverage", ToStringInvariant(request.Leverage) },
        };
        AddOptionalString(parameters, "stop_loss_price", request.StopLossPrice);
        AddOptionalString(parameters, "profit_sharing_ratio", request.ProfitSharingRatio);

        return parameters;
    }

    private async Task<RestCallResult<T>> SendBotDataRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        GateBotRequestHeaders request = null,
        ParameterCollection queryParameters = null,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await _.SendRequestInternal<GateBotResponse<T>>(
            _.GetUrl(api, v4, bot, endpoint),
            method,
            ct,
            true,
            queryParameters,
            bodyParameters,
            CreateHeaders(request)).ConfigureAwait(false);

        return result.Success ? result.As(result.Data?.Data) : result.As<T>(default);
    }

    /// <summary>
    /// Get AIHub strategy recommendations
    /// </summary>
    public Task<RestCallResult<GateBotRecommendationResult>> GetStrategyRecommendationsAsync(
        string market = null,
        GateBotStrategyType? strategyType = null,
        GateBotRecommendationDirection? direction = null,
        decimal? investAmount = null,
        GateBotDiscoverScene? scene = null,
        string refreshRecommendationId = null,
        int? limit = null,
        decimal? maxDrawdownLessThanOrEqual = null,
        decimal? backtestAprGreaterThanOrEqual = null,
        CancellationToken ct = default)
        => GetStrategyRecommendationsAsync(new GateBotRecommendationRequest
        {
            Market = market,
            StrategyType = strategyType,
            Direction = direction,
            InvestAmount = investAmount,
            Scene = scene,
            RefreshRecommendationId = refreshRecommendationId,
            Limit = limit,
            MaxDrawdownLessThanOrEqual = maxDrawdownLessThanOrEqual,
            BacktestAprGreaterThanOrEqual = backtestAprGreaterThanOrEqual,
        }, ct);

    /// <summary>
    /// Get AIHub strategy recommendations
    /// </summary>
    public Task<RestCallResult<GateBotRecommendationResult>> GetStrategyRecommendationsAsync(GateBotRecommendationRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("market", request.Market);
        parameters.AddOptionalEnum("strategy_type", request.StrategyType);
        parameters.AddOptionalEnum("direction", request.Direction);
        parameters.AddOptionalString("invest_amount", request.InvestAmount);
        parameters.AddOptionalEnum("scene", request.Scene);
        parameters.AddOptional("refresh_recommendation_id", request.RefreshRecommendationId);
        parameters.AddOptional("limit", request.Limit);
        parameters.AddOptionalString("max_drawdown_lte", request.MaxDrawdownLessThanOrEqual);
        parameters.AddOptionalString("backtest_apr_gte", request.BacktestAprGreaterThanOrEqual);

        return SendBotDataRequestAsync<GateBotRecommendationResult>("strategy/recommend", HttpMethod.Get, ct, request, parameters);
    }

    /// <summary>
    /// Create a spot grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateSpotGridAsync(string market, GateBotSpotGridCreateParameters createParameters, CancellationToken ct = default)
        => CreateSpotGridAsync(new GateBotSpotGridCreateRequest { Market = market, CreateParameters = createParameters }, ct);

    /// <summary>
    /// Create a spot grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateSpotGridAsync(GateBotSpotGridCreateRequest request, CancellationToken ct = default)
        => SendBotDataRequestAsync<GateBotCreateResult>("spot-grid/create", HttpMethod.Post, ct, request, bodyParameters: CreateStrategyBody(GateBotStrategyType.SpotGrid, request.Market, CreateSpotGridParameters(request.CreateParameters)));

    /// <summary>
    /// Create a margin grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateMarginGridAsync(string market, GateBotMarginGridCreateParameters createParameters, CancellationToken ct = default)
        => CreateMarginGridAsync(new GateBotMarginGridCreateRequest { Market = market, CreateParameters = createParameters }, ct);

    /// <summary>
    /// Create a margin grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateMarginGridAsync(GateBotMarginGridCreateRequest request, CancellationToken ct = default)
        => SendBotDataRequestAsync<GateBotCreateResult>("margin-grid/create", HttpMethod.Post, ct, request, bodyParameters: CreateStrategyBody(GateBotStrategyType.MarginGrid, request.Market, CreateMarginGridParameters(request.CreateParameters)));

    /// <summary>
    /// Create an infinite grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateInfiniteGridAsync(string market, GateBotInfiniteGridCreateParameters createParameters, CancellationToken ct = default)
        => CreateInfiniteGridAsync(new GateBotInfiniteGridCreateRequest { Market = market, CreateParameters = createParameters }, ct);

    /// <summary>
    /// Create an infinite grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateInfiniteGridAsync(GateBotInfiniteGridCreateRequest request, CancellationToken ct = default)
        => SendBotDataRequestAsync<GateBotCreateResult>("infinite-grid/create", HttpMethod.Post, ct, request, bodyParameters: CreateStrategyBody(GateBotStrategyType.InfiniteGrid, request.Market, CreateInfiniteGridParameters(request.CreateParameters)));

    /// <summary>
    /// Create a futures grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateFuturesGridAsync(string market, GateBotFuturesGridCreateParameters createParameters, CancellationToken ct = default)
        => CreateFuturesGridAsync(new GateBotFuturesGridCreateRequest { Market = market, CreateParameters = createParameters }, ct);

    /// <summary>
    /// Create a futures grid strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateFuturesGridAsync(GateBotFuturesGridCreateRequest request, CancellationToken ct = default)
        => SendBotDataRequestAsync<GateBotCreateResult>("futures-grid/create", HttpMethod.Post, ct, request, bodyParameters: CreateStrategyBody(GateBotStrategyType.FuturesGrid, request.Market, CreateMarginGridParameters(request.CreateParameters)));

    /// <summary>
    /// Create a spot martingale strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateSpotMartingaleAsync(string market, GateBotSpotMartingaleCreateParameters createParameters, CancellationToken ct = default)
        => CreateSpotMartingaleAsync(new GateBotSpotMartingaleCreateRequest { Market = market, CreateParameters = createParameters }, ct);

    /// <summary>
    /// Create a spot martingale strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateSpotMartingaleAsync(GateBotSpotMartingaleCreateRequest request, CancellationToken ct = default)
        => SendBotDataRequestAsync<GateBotCreateResult>("spot-martingale/create", HttpMethod.Post, ct, request, bodyParameters: CreateStrategyBody(GateBotStrategyType.SpotMartingale, request.Market, CreateSpotMartingaleParameters(request.CreateParameters)));

    /// <summary>
    /// Create a contract martingale strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateContractMartingaleAsync(string market, GateBotContractMartingaleCreateParameters createParameters, CancellationToken ct = default)
        => CreateContractMartingaleAsync(new GateBotContractMartingaleCreateRequest { Market = market, CreateParameters = createParameters }, ct);

    /// <summary>
    /// Create a contract martingale strategy
    /// </summary>
    public Task<RestCallResult<GateBotCreateResult>> CreateContractMartingaleAsync(GateBotContractMartingaleCreateRequest request, CancellationToken ct = default)
        => SendBotDataRequestAsync<GateBotCreateResult>("contract-martingale/create", HttpMethod.Post, ct, request, bodyParameters: CreateStrategyBody(GateBotStrategyType.ContractMartingale, request.Market, CreateContractMartingaleParameters(request.CreateParameters)));

    /// <summary>
    /// Query running bot strategies
    /// </summary>
    public Task<RestCallResult<GateBotRunningStrategiesPage>> GetRunningPortfoliosAsync(GateBotStrategyType? strategyType = null, string market = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        => GetRunningPortfoliosAsync(new GateBotRunningPortfolioQueryRequest { StrategyType = strategyType, Market = market, Page = page, PageSize = pageSize }, ct);

    /// <summary>
    /// Query running bot strategies
    /// </summary>
    public Task<RestCallResult<GateBotRunningStrategiesPage>> GetRunningPortfoliosAsync(GateBotRunningPortfolioQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("strategy_type", request.StrategyType);
        parameters.AddOptional("market", request.Market);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("page_size", request.PageSize);

        return SendBotDataRequestAsync<GateBotRunningStrategiesPage>("portfolio/running", HttpMethod.Get, ct, request, parameters);
    }

    /// <summary>
    /// Query bot strategy details
    /// </summary>
    public Task<RestCallResult<GateBotPortfolioDetail>> GetPortfolioDetailAsync(string strategyId, GateBotStrategyType strategyType, CancellationToken ct = default)
        => GetPortfolioDetailAsync(new GateBotPortfolioDetailRequest { StrategyId = strategyId, StrategyType = strategyType }, ct);

    /// <summary>
    /// Query bot strategy details
    /// </summary>
    public Task<RestCallResult<GateBotPortfolioDetail>> GetPortfolioDetailAsync(GateBotPortfolioDetailRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "strategy_id", request.StrategyId },
        };
        parameters.AddEnum("strategy_type", request.StrategyType);

        return SendBotDataRequestAsync<GateBotPortfolioDetail>("portfolio/detail", HttpMethod.Get, ct, request, parameters);
    }

    /// <summary>
    /// Stop a running bot strategy
    /// </summary>
    public Task<RestCallResult<GateBotStopResult>> StopPortfolioAsync(string strategyId, GateBotStrategyType strategyType, CancellationToken ct = default)
        => StopPortfolioAsync(new GateBotPortfolioStopRequest { StrategyId = strategyId, StrategyType = strategyType }, ct);

    /// <summary>
    /// Stop a running bot strategy
    /// </summary>
    public Task<RestCallResult<GateBotStopResult>> StopPortfolioAsync(GateBotPortfolioStopRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "strategy_id", request.StrategyId },
        };
        parameters.AddEnum("strategy_type", request.StrategyType);

        return SendBotDataRequestAsync<GateBotStopResult>("portfolio/stop", HttpMethod.Post, ct, request, bodyParameters: parameters);
    }
}
