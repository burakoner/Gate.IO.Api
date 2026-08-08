namespace Gate.IO.Api.Stock;

/// <summary>
/// Gate.IO Stock REST API client
/// </summary>
public class GateStockRestApiClient
{
    private const string Api = "api";
    private const string V4 = "4";
    private const string Stock = "stock";

    internal GateRestApiClient Root { get; }

    internal GateStockRestApiClient(GateRestApiClient root) => Root = root;

    private async Task<RestCallResult<T>> SendDataRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        bool signed = false,
        ParameterCollection queryParameters = null,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await Root.SendRequestInternal<GateStockResponse<T>>(
            Root.GetUrl(Api, V4, Stock, endpoint),
            method,
            ct,
            signed,
            queryParameters,
            bodyParameters).ConfigureAwait(false);

        if (!result.Success)
            return result.As<T>(default);

        var data = result.Data?.Data;
        if (data == null && typeof(T) == typeof(object))
            data = (T)(object)new object();

        return result.As(data);
    }

    private async Task<RestCallResult<List<T>>> SendListRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        bool signed = false,
        ParameterCollection queryParameters = null,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await SendDataRequestAsync<GateStockList<T>>(
            endpoint,
            method,
            ct,
            signed,
            queryParameters,
            bodyParameters).ConfigureAwait(false);

        if (!result.Success)
            return result.As<List<T>>([]);

        return result.As(result.Data?.List ?? []);
    }

    /// <summary>
    /// Query stock account assets
    /// </summary>
    public Task<RestCallResult<GateStockAssets>> GetAssetsAsync(
        GateStockPnlCalculationType? pnlCalculationType = null,
        GateStockPnlPriceType? pnlPriceType = null,
        CancellationToken ct = default)
        => GetAssetsAsync(new GateStockAssetQueryRequest
        {
            PnlCalculationType = pnlCalculationType,
            PnlPriceType = pnlPriceType,
        }, ct);

    /// <summary>
    /// Query stock account assets
    /// </summary>
    public Task<RestCallResult<GateStockAssets>> GetAssetsAsync(GateStockAssetQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("pnl_calc_type", request.PnlCalculationType);
        parameters.AddOptionalEnum("pnl_calc_price", request.PnlPriceType);

        return SendDataRequestAsync<GateStockAssets>("users/assets", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Query stock symbols
    /// </summary>
    public Task<RestCallResult<GateStockPage<GateStockSymbol>>> GetSymbolsAsync(GateStockSymbolQueryRequest request = null, CancellationToken ct = default)
    {
        request ??= new GateStockSymbolQueryRequest();
        ValidatePage(request.Page, request.PageSize);

        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", JoinValues(request.Symbols));
        parameters.AddOptionalEnum("exchange", request.Exchange);
        parameters.AddOptional("with_desc_i18n", request.IncludeLocalizedDescriptions?.ToString().ToLowerInvariant());
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("page_size", request.PageSize);

        return SendDataRequestAsync<GateStockPage<GateStockSymbol>>("symbols", HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Query stock symbol details
    /// </summary>
    public Task<RestCallResult<GateStockPage<GateStockSymbolDetails>>> GetSymbolDetailsAsync(GateStockSymbolDetailsQueryRequest request = null, CancellationToken ct = default)
    {
        request ??= new GateStockSymbolDetailsQueryRequest();
        ValidatePage(request.Page, request.PageSize);

        var parameters = new ParameterCollection();
        parameters.AddOptional("symbols", JoinValues(request.Symbols));
        parameters.AddOptionalEnum("exchange", request.Exchange);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("page_size", request.PageSize);

        return SendDataRequestAsync<GateStockPage<GateStockSymbolDetails>>("symbols/detail", HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Query a stock symbol order book
    /// </summary>
    public Task<RestCallResult<GateStockOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
    {
        ValidateRequired(symbol, nameof(symbol));
        return SendDataRequestAsync<GateStockOrderBook>($"market/{Uri.EscapeDataString(symbol.Trim())}/orderbook", HttpMethod.Get, ct);
    }

    /// <summary>
    /// Query active stock orders
    /// </summary>
    public Task<RestCallResult<List<GateStockOrder>>> GetOrdersAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", string.IsNullOrWhiteSpace(symbol) ? null : symbol.Trim());
        return SendListRequestAsync<GateStockOrder>("orders", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Place a stock order
    /// </summary>
    public Task<RestCallResult<GateStockOrderId>> PlaceOrderAsync(
        string symbol,
        GateStockOrderSide side,
        decimal volume,
        GateStockOrderPriceType priceType,
        GateStockTradingSession tradingSession,
        decimal? price = null,
        string clientOrderId = null,
        CancellationToken ct = default)
        => PlaceOrderAsync(new GateStockOrderRequest
        {
            Symbol = symbol,
            Side = side,
            Volume = volume,
            PriceType = priceType,
            TradingSession = tradingSession,
            Price = price,
            ClientOrderId = clientOrderId,
        }, ct);

    /// <summary>
    /// Place a stock order
    /// </summary>
    public Task<RestCallResult<GateStockOrderId>> PlaceOrderAsync(GateStockOrderRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        ValidateRequired(request.Symbol, nameof(request.Symbol));
        if (request.Volume <= 0) throw new ArgumentOutOfRangeException(nameof(request.Volume), "Volume must be greater than zero.");
        if (!Enum.IsDefined(typeof(GateStockOrderSide), request.Side)) throw new ArgumentOutOfRangeException(nameof(request.Side));
        if (!Enum.IsDefined(typeof(GateStockOrderPriceType), request.PriceType)) throw new ArgumentOutOfRangeException(nameof(request.PriceType));
        if (!Enum.IsDefined(typeof(GateStockTradingSession), request.TradingSession)) throw new ArgumentOutOfRangeException(nameof(request.TradingSession));
        if (request.TimeInForce != GateStockTimeInForce.Day) throw new ArgumentOutOfRangeException(nameof(request.TimeInForce), "The current Stock API supports day orders only.");
        if (request.PriceType == GateStockOrderPriceType.Limit && (!request.Price.HasValue || request.Price <= 0))
            throw new ArgumentException("A positive price is required for a limit order.", nameof(request));
        if (request.PriceType == GateStockOrderPriceType.Market && request.TradingSession != GateStockTradingSession.Regular)
            throw new ArgumentException("Market orders support the regular trading session only.", nameof(request));

        var parameters = new ParameterCollection();
        parameters.AddString("volume", request.Volume);
        parameters.Add("symbol", request.Symbol.Trim());
        parameters.Add("side", (int)request.Side);
        parameters.AddEnum("price_type", request.PriceType);
        parameters.AddEnum("trading_session", request.TradingSession);
        parameters.AddEnum("time_in_force", request.TimeInForce);
        parameters.AddOptionalString("price", request.Price);
        parameters.AddOptional("client_order_id", string.IsNullOrWhiteSpace(request.ClientOrderId) ? null : request.ClientOrderId.Trim());

        return SendDataRequestAsync<GateStockOrderId>("orders", HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel all active stock orders
    /// </summary>
    public Task<RestCallResult<object>> CancelAllOrdersAsync(CancellationToken ct = default)
        => SendDataRequestAsync<object>("orders", HttpMethod.Delete, ct, true);

    /// <summary>
    /// Query historical stock orders
    /// </summary>
    public Task<RestCallResult<GateStockPage<GateStockOrderHistory>>> GetOrderHistoryAsync(GateStockOrderHistoryQueryRequest request = null, CancellationToken ct = default)
    {
        request ??= new GateStockOrderHistoryQueryRequest();
        ValidatePage(request.Page, request.PageSize);
        ValidateTimeRange(request.BeginTime, request.EndTime);

        var orderIds = request.OrderIds?.ToList() ?? [];
        if (orderIds.Count > 20) throw new ArgumentOutOfRangeException(nameof(request.OrderIds), "A maximum of 20 order identifiers can be queried.");
        if (orderIds.Any(x => x <= 0)) throw new ArgumentOutOfRangeException(nameof(request.OrderIds), "Order identifiers must be positive.");

        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", string.IsNullOrWhiteSpace(request.Symbol) ? null : request.Symbol.Trim());
        parameters.AddOptional("order_ids", orderIds.Count == 0
            ? null
            : string.Join(",", orderIds.Select(x => x.ToString(CultureInfo.InvariantCulture))));
        parameters.AddOptionalSeconds("begin_time", request.BeginTime);
        parameters.AddOptionalSeconds("end_time", request.EndTime);
        parameters.AddOptionalEnum("side", request.Side);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("page_size", request.PageSize);

        return SendDataRequestAsync<GateStockPage<GateStockOrderHistory>>("orders/history", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Update a stock order
    /// </summary>
    public Task<RestCallResult<GateStockOrderUpdateResult>> UpdateOrderAsync(long orderId, decimal volume, decimal price, CancellationToken ct = default)
        => UpdateOrderAsync(orderId, new GateStockOrderUpdateRequest { Volume = volume, Price = price }, ct);

    /// <summary>
    /// Update a stock order
    /// </summary>
    public Task<RestCallResult<GateStockOrderUpdateResult>> UpdateOrderAsync(long orderId, GateStockOrderUpdateRequest request, CancellationToken ct = default)
    {
        if (orderId <= 0) throw new ArgumentOutOfRangeException(nameof(orderId));
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Volume <= 0) throw new ArgumentOutOfRangeException(nameof(request.Volume));
        if (request.Price <= 0) throw new ArgumentOutOfRangeException(nameof(request.Price));

        var parameters = new ParameterCollection();
        parameters.AddString("volume", request.Volume);
        parameters.AddString("price", request.Price);

        return SendDataRequestAsync<GateStockOrderUpdateResult>($"orders/{orderId}", HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel a stock order
    /// </summary>
    public Task<RestCallResult<object>> CancelOrderAsync(long orderId, CancellationToken ct = default)
    {
        if (orderId <= 0) throw new ArgumentOutOfRangeException(nameof(orderId));
        return SendDataRequestAsync<object>($"orders/{orderId}", HttpMethod.Delete, ct, true);
    }

    /// <summary>
    /// Query stock positions
    /// </summary>
    public Task<RestCallResult<List<GateStockPosition>>> GetPositionsAsync(GateStockPositionQueryRequest request = null, CancellationToken ct = default)
    {
        request ??= new GateStockPositionQueryRequest();

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("pnl_calc_type", request.PnlCalculationType);
        parameters.AddOptionalEnum("pnl_calc_price", request.PnlPriceType);
        parameters.AddOptional("symbol", string.IsNullOrWhiteSpace(request.Symbol) ? null : request.Symbol.Trim());
        parameters.AddOptionalEnum("exchange", request.Exchange);

        return SendListRequestAsync<GateStockPosition>("positions", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Close a stock position
    /// </summary>
    public Task<RestCallResult<GateStockPositionCloseResult>> ClosePositionAsync(GateStockClosePositionRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        ValidateRequired(request.Symbol, nameof(request.Symbol));
        if (!Enum.IsDefined(typeof(GateStockPositionCloseType), request.CloseType)) throw new ArgumentOutOfRangeException(nameof(request.CloseType));
        if (request.CloseType == GateStockPositionCloseType.Partial && (!request.CloseVolume.HasValue || request.CloseVolume <= 0))
            throw new ArgumentException("A positive close volume is required for a partial close.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "symbol", request.Symbol.Trim() },
        };
        parameters.AddOptionalString("close_volume", request.CloseVolume);
        parameters.Add("close_type", (int)request.CloseType);

        return SendDataRequestAsync<GateStockPositionCloseResult>("positions/close", HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query stock account transactions
    /// </summary>
    public Task<RestCallResult<GateStockPage<GateStockTransaction>>> GetTransactionsAsync(GateStockTransactionQueryRequest request = null, CancellationToken ct = default)
    {
        request ??= new GateStockTransactionQueryRequest();

        var parameters = new ParameterCollection();
        if (!string.IsNullOrWhiteSpace(request.ReferenceId))
        {
            parameters.Add("ref_id", request.ReferenceId.Trim());
        }
        else
        {
            ValidatePage(request.Page, request.PageSize);
            ValidateTimeRange(request.BeginTime, request.EndTime);
            parameters.AddOptionalSeconds("begin_time", request.BeginTime);
            parameters.AddOptionalSeconds("end_time", request.EndTime);
            parameters.AddOptionalEnum("type", request.Type);
            parameters.AddOptional("page", request.Page);
            parameters.AddOptional("page_size", request.PageSize);
        }

        return SendDataRequestAsync<GateStockPage<GateStockTransaction>>("transactions", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Transfer funds into or out of the stock account
    /// </summary>
    public Task<RestCallResult<object>> CreateTransactionAsync(GateStockTransferRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (!string.Equals(request.Asset, "USDT", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("The current Stock API supports USDT transfers only.", nameof(request.Asset));
        ValidateRequired(request.ReferenceId, nameof(request.ReferenceId));
        if (!Enum.IsDefined(typeof(GateStockTransferType), request.Type)) throw new ArgumentOutOfRangeException(nameof(request.Type));

        var parameters = new ParameterCollection
        {
            { "asset", "USDT" },
            { "ref_id", request.ReferenceId.Trim() },
        };
        parameters.AddString("change", request.Change);
        parameters.AddEnum("type", request.Type);

        return SendDataRequestAsync<object>("transactions", HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query supported stock exchanges
    /// </summary>
    /// <remarks>The official documentation marks this endpoint public, but Gate production currently requires API v4 authentication.</remarks>
    public Task<RestCallResult<List<GateStockExchangeInfo>>> GetExchangesAsync(CancellationToken ct = default)
        => SendListRequestAsync<GateStockExchangeInfo>("exchanges", HttpMethod.Get, ct, true);

    /// <summary>
    /// Query stock fee rates
    /// </summary>
    public Task<RestCallResult<List<GateStockFeeRate>>> GetFeeRatesAsync(CancellationToken ct = default)
        => SendListRequestAsync<GateStockFeeRate>("fee-rate", HttpMethod.Get, ct);

    private static string JoinValues(IEnumerable<string> values)
    {
        var normalized = values?.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList() ?? [];
        return normalized.Count == 0 ? null : string.Join(",", normalized);
    }

    private static void ValidatePage(int? page, int? pageSize)
    {
        page?.ValidateIntBetween(nameof(page), 1, int.MaxValue);
        pageSize?.ValidateIntBetween(nameof(pageSize), 1, 500);
    }

    private static void ValidateTimeRange(DateTime? beginTime, DateTime? endTime)
    {
        if (!beginTime.HasValue || !endTime.HasValue) return;
        if (endTime < beginTime) throw new ArgumentException("End time cannot be earlier than begin time.", nameof(endTime));
        if (endTime > beginTime.Value.AddMonths(3)) throw new ArgumentException("The requested time range cannot exceed three months.", nameof(endTime));
    }

    private static void ValidateRequired(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value is required.", parameterName);
    }
}
