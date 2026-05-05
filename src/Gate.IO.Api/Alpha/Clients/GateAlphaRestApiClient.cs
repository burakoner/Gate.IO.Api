namespace Gate.IO.Api.Alpha;

/// <summary>
/// Gate.IO Alpha REST API client.
/// </summary>
public class GateAlphaRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string alpha = "alpha";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateAlphaRestApiClient(GateRestApiClient root) => _ = root;

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

    private static void AddOptionalString(ParameterCollection parameters, string name, decimal? value)
    {
        if (value.HasValue)
            parameters.AddString(name, value.Value);
    }

    private static ParameterCollection CreateTradeBody(string currency, GateAlphaOrderSide side, decimal amount, GateAlphaGasMode gasMode, decimal? slippage)
    {
        var parameters = new ParameterCollection
        {
            { "currency", currency },
        };
        parameters.AddEnum("side", side);
        parameters.AddString("amount", amount);
        parameters.AddEnum("gas_mode", gasMode);
        AddOptionalString(parameters, "slippage", slippage);

        return parameters;
    }

    private static ParameterCollection CreateQuoteBody(GateAlphaQuoteRequest request)
        => CreateTradeBody(request.Currency, request.Side, request.Amount, request.GasMode, request.Slippage);

    private static ParameterCollection CreateOrderBody(GateAlphaOrderRequest request)
    {
        var parameters = CreateTradeBody(request.Currency, request.Side, request.Amount, request.GasMode, request.Slippage);
        parameters.Add("quote_id", request.QuoteId);

        return parameters;
    }

    /// <summary>
    /// Query Alpha account position assets.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaAccount>>> GetAccountsAsync(CancellationToken ct = default)
        => _.SendRequestInternal<List<GateAlphaAccount>>(_.GetUrl(api, v4, alpha, "accounts"), HttpMethod.Get, ct, true);

    /// <summary>
    /// Query Alpha account asset transaction history.
    /// </summary>
    /// <param name="from">Start time for the account book query</param>
    /// <param name="to">End time for the account book query</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of items returned</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaAccountBookRecord>>> GetAccountBookAsync(DateTime from, DateTime? to = null, int? page = null, int? limit = null, CancellationToken ct = default)
        => GetAccountBookAsync(new GateAlphaAccountBookRequest { From = from, To = to, Page = page, Limit = limit }, ct);

    /// <summary>
    /// Query Alpha account asset transaction history.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaAccountBookRecord>>> GetAccountBookAsync(GateAlphaAccountBookRequest request, CancellationToken ct = default)
    {
        if (request.From == default)
            throw new ArgumentException("From must be provided.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "from", request.From.ConvertToSeconds() },
        };
        parameters.AddOptional("to", request.To?.ConvertToSeconds());
        AddPaging(parameters, request.Page, request.Limit);

        return _.SendRequestInternal<List<GateAlphaAccountBookRecord>>(_.GetUrl(api, v4, alpha, "account_book"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get an Alpha quote for a potential order.
    /// </summary>
    /// <param name="currency">Trading symbol</param>
    /// <param name="side">Buy or sell side</param>
    /// <param name="amount">Trade quantity</param>
    /// <param name="gasMode">Trading mode</param>
    /// <param name="slippage">Slippage tolerance</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAlphaQuote>> GetQuoteAsync(string currency, GateAlphaOrderSide side, decimal amount, GateAlphaGasMode gasMode, decimal? slippage = null, CancellationToken ct = default)
        => GetQuoteAsync(new GateAlphaQuoteRequest { Currency = currency, Side = side, Amount = amount, GasMode = gasMode, Slippage = slippage }, ct);

    /// <summary>
    /// Get an Alpha quote for a potential order.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAlphaQuote>> GetQuoteAsync(GateAlphaQuoteRequest request, CancellationToken ct = default)
        => _.SendRequestInternal<GateAlphaQuote>(_.GetUrl(api, v4, alpha, "quote"), HttpMethod.Post, ct, true, bodyParameters: CreateQuoteBody(request));

    /// <summary>
    /// Place an Alpha order.
    /// </summary>
    /// <param name="currency">Trading symbol</param>
    /// <param name="side">Buy or sell side</param>
    /// <param name="amount">Trade quantity</param>
    /// <param name="gasMode">Trading mode</param>
    /// <param name="quoteId">Quote ID returned by the quotation API</param>
    /// <param name="slippage">Slippage tolerance</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAlphaOrderPlacement>> PlaceOrderAsync(string currency, GateAlphaOrderSide side, decimal amount, GateAlphaGasMode gasMode, string quoteId, decimal? slippage = null, CancellationToken ct = default)
        => PlaceOrderAsync(new GateAlphaOrderRequest { Currency = currency, Side = side, Amount = amount, GasMode = gasMode, QuoteId = quoteId, Slippage = slippage }, ct);

    /// <summary>
    /// Place an Alpha order.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAlphaOrderPlacement>> PlaceOrderAsync(GateAlphaOrderRequest request, CancellationToken ct = default)
        => _.SendRequestInternal<GateAlphaOrderPlacement>(_.GetUrl(api, v4, alpha, "orders"), HttpMethod.Post, ct, true, bodyParameters: CreateOrderBody(request));

    /// <summary>
    /// Query Alpha orders.
    /// </summary>
    /// <param name="currency">Trading symbol</param>
    /// <param name="side">Buy or sell side</param>
    /// <param name="status">Order status</param>
    /// <param name="from">Start time for order query</param>
    /// <param name="to">End time for order query</param>
    /// <param name="limit">Maximum number of items returned</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaOrder>>> GetOrdersAsync(string currency = null, GateAlphaOrderSide? side = null, GateAlphaOrderStatus? status = null, DateTime? from = null, DateTime? to = null, int? limit = null, int? page = null, CancellationToken ct = default)
        => GetOrdersAsync(new GateAlphaOrdersQueryRequest { Currency = currency, Side = side, Status = status, From = from, To = to, Limit = limit, Page = page }, ct);

    /// <summary>
    /// Query Alpha orders.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaOrder>>> GetOrdersAsync(GateAlphaOrdersQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptionalEnum("side", request.Side);
        parameters.AddOptional("status", request.Status.HasValue ? (int?)request.Status.Value : null);
        AddTimeRange(parameters, request.From, request.To);
        AddPaging(parameters, request.Page, request.Limit);

        return _.SendRequestInternal<List<GateAlphaOrder>>(_.GetUrl(api, v4, alpha, "orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query a single Alpha order.
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateAlphaOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "order_id", orderId },
        };

        return _.SendRequestInternal<GateAlphaOrder>(_.GetUrl(api, v4, alpha, "order"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query Alpha currency information.
    /// </summary>
    /// <param name="currency">Currency symbol</param>
    /// <param name="limit">Maximum number of records returned in a single list</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaCurrency>>> GetCurrenciesAsync(string currency = null, int? limit = null, int? page = null, CancellationToken ct = default)
        => GetCurrenciesAsync(new GateAlphaCurrencyQueryRequest { Currency = currency, Limit = limit, Page = page }, ct);

    /// <summary>
    /// Query Alpha currency information.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaCurrency>>> GetCurrenciesAsync(GateAlphaCurrencyQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        AddPaging(parameters, request.Page, request.Limit);

        return _.SendRequestInternal<List<GateAlphaCurrency>>(_.GetUrl(api, v4, alpha, "currencies"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Query Alpha currency ticker information.
    /// </summary>
    /// <param name="currency">Currency symbol</param>
    /// <param name="limit">Maximum number of records returned in a single list</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaTicker>>> GetTickersAsync(string currency = null, int? limit = null, int? page = null, CancellationToken ct = default)
        => GetTickersAsync(new GateAlphaTickerQueryRequest { Currency = currency, Limit = limit, Page = page }, ct);

    /// <summary>
    /// Query Alpha currency ticker information.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaTicker>>> GetTickersAsync(GateAlphaTickerQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        AddPaging(parameters, request.Page, request.Limit);

        return _.SendRequestInternal<List<GateAlphaTicker>>(_.GetUrl(api, v4, alpha, "tickers"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Query Alpha token information.
    /// </summary>
    /// <param name="chain">Chain name</param>
    /// <param name="launchPlatform">Launch platform</param>
    /// <param name="address">Contract address</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaToken>>> GetTokensAsync(string chain = null, string launchPlatform = null, string address = null, int? page = null, CancellationToken ct = default)
        => GetTokensAsync(new GateAlphaTokenQueryRequest { Chain = chain, LaunchPlatform = launchPlatform, Address = address, Page = page }, ct);

    /// <summary>
    /// Query Alpha token information.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateAlphaToken>>> GetTokensAsync(GateAlphaTokenQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("chain", request.Chain);
        parameters.AddOptional("launch_platform", request.LaunchPlatform);
        parameters.AddOptional("address", request.Address);
        parameters.AddOptional("page", request.Page);

        return _.SendRequestInternal<List<GateAlphaToken>>(_.GetUrl(api, v4, alpha, "tokens"), HttpMethod.Get, ct, queryParameters: parameters);
    }
}
