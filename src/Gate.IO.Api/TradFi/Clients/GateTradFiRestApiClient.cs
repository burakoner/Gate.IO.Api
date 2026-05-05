namespace Gate.IO.Api.TradFi;

/// <summary>
/// Gate.IO TradFi REST API client
/// </summary>
public class GateTradFiRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string tradfi = "tradfi";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateTradFiRestApiClient(GateRestApiClient root) => _ = root;

    private async Task<RestCallResult<T>> SendTradFiDataRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        bool signed = false,
        ParameterCollection queryParameters = null,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await _.SendRequestInternal<GateTradFiResponse<T>>(_.GetUrl(api, v4, tradfi, endpoint), method, ct, signed, queryParameters, bodyParameters).ConfigureAwait(false);
        if (!result.Success) return result.As<T>(default);

        var data = result.Data?.Data;
        if (data == null && typeof(T) == typeof(object))
            data = (T)(object)new object();

        return result.As(data);
    }

    private async Task<RestCallResult<List<T>>> SendTradFiListRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        bool signed = false,
        ParameterCollection queryParameters = null,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await SendTradFiDataRequestAsync<GateTradFiList<T>>(endpoint, method, ct, signed, queryParameters, bodyParameters).ConfigureAwait(false);
        if (!result.Success) return result.As<List<T>>([]);

        return result.As(result.Data?.List ?? []);
    }

    /// <summary>
    /// Query MT5 account information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiMt5Account>> GetMt5AccountAsync(CancellationToken ct = default)
        => SendTradFiDataRequestAsync<GateTradFiMt5Account>("users/mt5-account", HttpMethod.Get, ct, true);

    /// <summary>
    /// Query trading symbol categories
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiCategory>>> GetSymbolCategoriesAsync(CancellationToken ct = default)
        => SendTradFiListRequestAsync<GateTradFiCategory>("symbols/categories", HttpMethod.Get, ct);

    /// <summary>
    /// Query trading symbol list
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        => SendTradFiListRequestAsync<GateTradFiSymbol>("symbols", HttpMethod.Get, ct);

    /// <summary>
    /// Query trading symbol details
    /// </summary>
    /// <param name="symbols">Trading symbol code list, max 10 symbols</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiSymbolDetails>>> GetSymbolDetailsAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        => GetSymbolDetailsAsync(new GateTradFiSymbolDetailsRequest { Symbols = symbols }, ct);

    /// <summary>
    /// Query trading symbol details
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiSymbolDetails>>> GetSymbolDetailsAsync(GateTradFiSymbolDetailsRequest request, CancellationToken ct = default)
    {
        var symbols = request.Symbols?.Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? [];
        symbols.Count.ValidateIntBetween(nameof(request.Symbols), 1, 10);

        var parameters = new ParameterCollection
        {
            { "symbols", string.Join(",", symbols) },
        };

        return SendTradFiListRequestAsync<GateTradFiSymbolDetails>("symbols/detail", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Query trading symbol klines
    /// </summary>
    /// <param name="symbol">Trading symbol code</param>
    /// <param name="interval">Kline type</param>
    /// <param name="beginTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="limit">Kline limit, max 500</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiCandlestick>>> GetCandlesticksAsync(string symbol, GateTradFiKlineInterval interval, DateTime? beginTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        => GetCandlesticksAsync(new GateTradFiCandlestickQueryRequest
        {
            Symbol = symbol,
            Interval = interval,
            BeginTime = beginTime,
            EndTime = endTime,
            Limit = limit,
        }, ct);

    /// <summary>
    /// Query trading symbol klines
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiCandlestick>>> GetCandlesticksAsync(GateTradFiCandlestickQueryRequest request, CancellationToken ct = default)
    {
        request.Limit?.ValidateIntBetween(nameof(request.Limit), 1, 500);

        var parameters = new ParameterCollection();
        parameters.AddEnum("kline_type", request.Interval);
        parameters.AddOptionalSeconds("begin_time", request.BeginTime);
        parameters.AddOptionalSeconds("end_time", request.EndTime);
        parameters.AddOptional("limit", request.Limit);

        return SendTradFiListRequestAsync<GateTradFiCandlestick>("symbols/{symbol}/klines".Replace("{symbol}", request.Symbol), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Query trading symbol ticker
    /// </summary>
    /// <param name="symbol">Trading symbol code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        => SendTradFiDataRequestAsync<GateTradFiTicker>("symbols/{symbol}/tickers".Replace("{symbol}", symbol), HttpMethod.Get, ct);

    /// <summary>
    /// Create TradFi user
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiUser>> CreateUserAsync(CancellationToken ct = default)
        => SendTradFiDataRequestAsync<GateTradFiUser>("users", HttpMethod.Post, ct, true);

    /// <summary>
    /// Query account assets
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiAccountAssets>> GetAccountAssetsAsync(CancellationToken ct = default)
        => SendTradFiDataRequestAsync<GateTradFiAccountAssets>("users/assets", HttpMethod.Get, ct, true);

    /// <summary>
    /// Query fund transfer in/out records
    /// </summary>
    /// <param name="beginTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="type">Transaction type</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Number per page, maximum 50</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiTransactionList>> GetTransactionsAsync(DateTime? beginTime = null, DateTime? endTime = null, GateTradFiTransactionType? type = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        => GetTransactionsAsync(new GateTradFiTransactionQueryRequest
        {
            BeginTime = beginTime,
            EndTime = endTime,
            Type = type,
            Page = page,
            PageSize = pageSize,
        }, ct);

    /// <summary>
    /// Query fund transfer in/out records
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiTransactionList>> GetTransactionsAsync(GateTradFiTransactionQueryRequest request, CancellationToken ct = default)
    {
        request.PageSize?.ValidateIntBetween(nameof(request.PageSize), 1, 50);

        var parameters = new ParameterCollection();
        parameters.AddOptionalSeconds("begin_time", request.BeginTime);
        parameters.AddOptionalSeconds("end_time", request.EndTime);
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("page_size", request.PageSize);

        return SendTradFiDataRequestAsync<GateTradFiTransactionList>("transactions", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Fund deposit and withdrawal
    /// </summary>
    /// <param name="asset">Asset type, currently only USDT is supported</param>
    /// <param name="change">Change quantity</param>
    /// <param name="type">Transaction type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> CreateTransactionAsync(string asset, decimal change, GateTradFiTransactionType type, CancellationToken ct = default)
        => CreateTransactionAsync(new GateTradFiTransactionRequest
        {
            Asset = asset,
            Change = change,
            Type = type,
        }, ct);

    /// <summary>
    /// Fund deposit and withdrawal
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> CreateTransactionAsync(GateTradFiTransactionRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "asset", request.Asset },
        };
        parameters.AddString("change", request.Change);
        parameters.AddEnum("type", request.Type);

        return SendTradFiDataRequestAsync<object>("transactions", HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query active order list
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiOrder>>> GetOrdersAsync(CancellationToken ct = default)
        => SendTradFiListRequestAsync<GateTradFiOrder>("orders", HttpMethod.Get, ct, true);

    /// <summary>
    /// Create an order
    /// </summary>
    /// <param name="symbol">Trading symbol code</param>
    /// <param name="side">Order side</param>
    /// <param name="volume">Order volume</param>
    /// <param name="priceType">Price type</param>
    /// <param name="price">Order price</param>
    /// <param name="takeProfitPrice">Take profit price</param>
    /// <param name="stopLossPrice">Stop loss price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiOrderId>> PlaceOrderAsync(string symbol, GateTradFiOrderSide side, decimal volume, GateTradFiOrderPriceType priceType, decimal price, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, CancellationToken ct = default)
        => PlaceOrderAsync(new GateTradFiOrderRequest
        {
            Symbol = symbol,
            Side = side,
            Volume = volume,
            PriceType = priceType,
            Price = price,
            TakeProfitPrice = takeProfitPrice,
            StopLossPrice = stopLossPrice,
        }, ct);

    /// <summary>
    /// Create an order
    /// </summary>
    /// <param name="request">Order request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiOrderId>> PlaceOrderAsync(GateTradFiOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "symbol", request.Symbol },
            { "side", (int)request.Side },
        };
        parameters.AddEnum("price_type", request.PriceType);
        parameters.AddString("price", request.Price);
        parameters.AddString("volume", request.Volume);
        parameters.AddOptionalString("price_tp", request.TakeProfitPrice);
        parameters.AddOptionalString("price_sl", request.StopLossPrice);

        return SendTradFiDataRequestAsync<GateTradFiOrderId>("orders", HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Modify order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="price">Price</param>
    /// <param name="takeProfitPrice">Take profit price</param>
    /// <param name="stopLossPrice">Stop loss price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiOrderUpdateResult>> UpdateOrderAsync(long orderId, decimal price, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, CancellationToken ct = default)
        => UpdateOrderAsync(orderId, new GateTradFiOrderUpdateRequest
        {
            Price = price,
            TakeProfitPrice = takeProfitPrice,
            StopLossPrice = stopLossPrice,
        }, ct);

    /// <summary>
    /// Modify order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="request">Order update request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateTradFiOrderUpdateResult>> UpdateOrderAsync(long orderId, GateTradFiOrderUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("price", request.Price);
        parameters.AddOptionalString("price_tp", request.TakeProfitPrice);
        parameters.AddOptionalString("price_sl", request.StopLossPrice);

        return SendTradFiDataRequestAsync<GateTradFiOrderUpdateResult>("orders/{order_id}".Replace("{order_id}", orderId.ToString()), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        => _.SendRequestInternal<object>(_.GetUrl(api, v4, tradfi, "orders/{order_id}".Replace("{order_id}", orderId.ToString())), HttpMethod.Delete, ct, true);

    /// <summary>
    /// Query historical order list
    /// </summary>
    /// <param name="beginTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="symbol">Currency pair</param>
    /// <param name="side">Order side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiOrderHistory>>> GetOrderHistoryAsync(DateTime? beginTime = null, DateTime? endTime = null, string symbol = null, GateTradFiOrderSide? side = null, CancellationToken ct = default)
        => GetOrderHistoryAsync(new GateTradFiOrderHistoryQueryRequest
        {
            BeginTime = beginTime,
            EndTime = endTime,
            Symbol = symbol,
            Side = side,
        }, ct);

    /// <summary>
    /// Query historical order list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiOrderHistory>>> GetOrderHistoryAsync(GateTradFiOrderHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalSeconds("begin_time", request.BeginTime);
        parameters.AddOptionalSeconds("end_time", request.EndTime);
        parameters.AddOptional("symbol", request.Symbol);
        if (request.Side.HasValue) parameters.Add("side", (int)request.Side.Value);

        return SendTradFiListRequestAsync<GateTradFiOrderHistory>("orders/history", HttpMethod.Get, ct, true, parameters);
    }

    /// <summary>
    /// Query active position list
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiPosition>>> GetPositionsAsync(CancellationToken ct = default)
        => SendTradFiListRequestAsync<GateTradFiPosition>("positions", HttpMethod.Get, ct, true);

    /// <summary>
    /// Modify position
    /// </summary>
    /// <param name="positionId">Position ID</param>
    /// <param name="takeProfitPrice">Take profit price</param>
    /// <param name="stopLossPrice">Stop loss price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdatePositionAsync(long positionId, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, CancellationToken ct = default)
        => UpdatePositionAsync(positionId, new GateTradFiPositionUpdateRequest
        {
            TakeProfitPrice = takeProfitPrice,
            StopLossPrice = stopLossPrice,
        }, ct);

    /// <summary>
    /// Modify position
    /// </summary>
    /// <param name="positionId">Position ID</param>
    /// <param name="request">Position update request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> UpdatePositionAsync(long positionId, GateTradFiPositionUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalString("price_tp", request.TakeProfitPrice);
        parameters.AddOptionalString("price_sl", request.StopLossPrice);

        return SendTradFiDataRequestAsync<object>("positions/{position_id}".Replace("{position_id}", positionId.ToString()), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Close position
    /// </summary>
    /// <param name="positionId">Position ID</param>
    /// <param name="closeType">Close position type</param>
    /// <param name="closeVolume">Close volume</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> ClosePositionAsync(long positionId, int closeType, decimal? closeVolume = null, CancellationToken ct = default)
        => ClosePositionAsync(positionId, new GateTradFiClosePositionRequest
        {
            CloseType = closeType,
            CloseVolume = closeVolume,
        }, ct);

    /// <summary>
    /// Close position
    /// </summary>
    /// <param name="positionId">Position ID</param>
    /// <param name="request">Close position request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> ClosePositionAsync(long positionId, GateTradFiClosePositionRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "close_type", request.CloseType },
        };
        parameters.AddOptionalString("close_volume", request.CloseVolume);

        return SendTradFiDataRequestAsync<object>("positions/{position_id}/close".Replace("{position_id}", positionId.ToString()), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query historical position list
    /// </summary>
    /// <param name="beginTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="symbol">Trading symbol</param>
    /// <param name="direction">Position direction</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiPositionHistory>>> GetPositionHistoryAsync(DateTime? beginTime = null, DateTime? endTime = null, string symbol = null, GateTradFiPositionDirection? direction = null, CancellationToken ct = default)
        => GetPositionHistoryAsync(new GateTradFiPositionHistoryQueryRequest
        {
            BeginTime = beginTime,
            EndTime = endTime,
            Symbol = symbol,
            Direction = direction,
        }, ct);

    /// <summary>
    /// Query historical position list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateTradFiPositionHistory>>> GetPositionHistoryAsync(GateTradFiPositionHistoryQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalSeconds("begin_time", request.BeginTime);
        parameters.AddOptionalSeconds("end_time", request.EndTime);
        parameters.AddOptional("symbol", request.Symbol);
        parameters.AddOptionalEnum("position_dir", request.Direction);

        return SendTradFiListRequestAsync<GateTradFiPositionHistory>("positions/history", HttpMethod.Get, ct, true, parameters);
    }
}
