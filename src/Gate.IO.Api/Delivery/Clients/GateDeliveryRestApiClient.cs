namespace Gate.IO.Api.Delivery;

/// <summary>
/// Gate.IO Futures Delivery REST API Client
/// </summary>
public class GateDeliveryRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string delivery = "delivery";

    // Root Client
    internal GateRestApiClient _ { get; }

    /// <summary>
    /// USDT Settled Delivery Futures Client
    /// </summary>
    public GateDeliveryRestApiSettleClient USDT { get; }

    /// <summary>
    /// Get a delivery futures settle client
    /// </summary>
    /// <param name="settle">Delivery Settlement Asset</param>
    /// <returns></returns>
    public GateDeliveryRestApiSettleClient this[GateDeliverySettlement settle] => Clients[settle];
    private Dictionary<GateDeliverySettlement, GateDeliveryRestApiSettleClient> Clients { get; }

    // Constructor
    internal GateDeliveryRestApiClient(GateRestApiClient root)
    {
        _ = root;

        USDT = new GateDeliveryRestApiSettleClient(this, GateDeliverySettlement.USDT);
        Clients = new Dictionary<GateDeliverySettlement, GateDeliveryRestApiSettleClient>
        {
            { GateDeliverySettlement.USDT, USDT },
        };
    }

    // List all futures contracts
    internal Task<RestCallResult<List<GateDeliveryContract>>> GetContractsAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = "{settle}/contracts".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateDeliveryContract>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct);
    }

    // Get a single contract
    internal Task<RestCallResult<GateDeliveryContract>> GetContractAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/contracts/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateDeliveryContract>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct);
    }

    // Futures order book
    internal Task<RestCallResult<GateFuturesOrderBook>> GetOrderBookAsync(GateDeliverySettlement settle, string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        var endpoint = "{settle}/order_book".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrderBook>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures trading history
    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateDeliverySettlement settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    => GetTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset,lastId, ct);

    // Futures trading history
    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateDeliverySettlement settle, string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = "{settle}/trades".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesTrade>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get futures candlesticks
    internal Task<RestCallResult<List<GateFuturesCandlestick>>> GetCandlesticksAsync(GateDeliverySettlement settle, string prefix, string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetCandlesticksAsync(settle, prefix, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<List<GateFuturesCandlestick>>> GetCandlesticksAsync(GateDeliverySettlement settle, string prefix, string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", prefix + contract },
        };
        parameters.AddEnum("interval", interval);
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = "{settle}/candlesticks".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesCandlestick>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // List futures tickers
    internal Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(GateDeliverySettlement settle, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/tickers".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesTicker>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures insurance balance history
    internal Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(GateDeliverySettlement settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };

        var endpoint = "{settle}/insurance".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesInsuranceBalance>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Query futures account
    internal Task<RestCallResult<List<GateFuturesBalance>>> GetBalancesAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = "{settle}/accounts".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBalance>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true);
    }

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateDeliverySettlement settle, GateFuturesBalanceChangeType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetBalanceHistoryAsync(settle, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateDeliverySettlement settle, GateFuturesBalanceChangeType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("limit", limit);

        var endpoint = "{settle}/account_book".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBalanceChange>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List all positions of a user
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = "{settle}/positions".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true);
    }

    // Get single position
    internal Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/positions/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true);
    }

    // Update position margin
    internal Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(GateDeliverySettlement settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("change", change);

        var endpoint = "{settle}/positions/{contract}/margin"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position leverage
    internal Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(GateDeliverySettlement settle, string contract, decimal leverage, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("leverage", leverage);

        var endpoint = "{settle}/positions/{contract}/leverage"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position risk limit
    internal Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(GateDeliverySettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = "{settle}/positions/{contract}/risk_limit"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Create a futures order
    internal Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(
        GateDeliverySettlement settle, 
        string contract, 
        long size, 
        long? iceberg = null, 
        decimal? price = null, 
        bool? close = null, 
        bool? reduceOnly = null, 
        string clientOrderId = null, 
        GateFuturesTimeInForce? timeInForce = null, 
        GateFuturesOrderAutoSize? autoSize = null, 
        GateFuturesSelfTradeAction? selfTradeAction = null, 
        CancellationToken ct = default)
        => PlaceOrderAsync(settle, new GateFuturesOrderRequest
        {
            Contract = contract,
            Size = size,
            Iceberg = iceberg,
            Price = price,
            Close = close,
            ReduceOnly = reduceOnly,
            ClientOrderId = clientOrderId,
            TimeInForce = timeInForce,
            AutoSize = autoSize,
            SelfTradeAction = selfTradeAction,
        }, ct);

    // Create a futures order
    internal Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(GateDeliverySettlement settle, GateFuturesOrderRequest request, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(request.Contract);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new ParameterCollection();
        parameters.Add("contract", request.Contract);
        parameters.AddString("size", request.Size);
        parameters.AddOptionalString("iceberg", request.Iceberg);
        parameters.AddOptionalString("price", request.Price);
        parameters.AddOptional("close", request.Close);
        parameters.AddOptional("reduce_only", request.ReduceOnly);
        parameters.AddOptionalEnum("tif", request.TimeInForce);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptionalEnum("auto_size", request.AutoSize);
        parameters.AddOptionalEnum("stp_act", request.SelfTradeAction);

        var endpoint = "{settle}/orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // List futures orders
    internal Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(GateDeliverySettlement settle, string contract, GateFuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "limit", limit },
            { "offset", offset },
            { "count_total", countTotal ? 1 : 0 },
        };
        parameters.AddEnum("status", status);
        parameters.AddOptionalParameter("last_id", lastId);

        var endpoint = "{settle}/orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders matched
    internal Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(GateDeliverySettlement settle, string contract, GateFuturesOrderSide side, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddEnum("side", side);

        var endpoint = "{settle}/orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Get a single order
    internal Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(GateDeliverySettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {        
        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a single order
    internal Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(GateDeliverySettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Delete, ct, true);
    }

    // List personal trading history
    internal Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(GateDeliverySettlement settle, string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "offset", offset },
            { "limit", limit },
            { "count_total", countTotal ? 1 : 0 },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("order", orderId);

        var endpoint = "{settle}/my_trades".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserTrade>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List position close history
    internal Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/position_close".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPositionClose>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List liquidation history
    internal Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidationsAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = "{settle}/liquidates".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserLiquidation>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List settlement history
    internal Task<RestCallResult<List<GateDeliveryUserSettlement>>> GetUserSettlementsAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = "{settle}/settlements".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateDeliveryUserSettlement>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List risk limit tiers
    internal Task<RestCallResult<List<GateDeliveryRiskLimitTier>>> GetRiskLimitTiersAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, long? offset = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("offset", offset);

        var endpoint = "{settle}/risk_limit_tiers".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateDeliveryRiskLimitTier>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Create a price-triggered order
    internal Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        // Settlement
        GateDeliverySettlement settle,

        // Type
        GateFuturesTriggerType triggerType,

        // Trigger
        GateFuturesTriggerPrice triggerPriceType,
        GateFuturesTriggerStrategy triggerStrategy,
        GateSpotTriggerCondition triggerCondition,
        decimal triggerPrice,
        TimeSpan triggerExpiration,

        // Initial Order
        string orderContract,
        decimal orderPrice,
        long orderSize,
        bool orderClose,
        GateFuturesTimeInForce orderTimeInForce,
        string orderClientOrderId,
        bool orderReduceOnly,
        GateFuturesOrderAutoSize orderAutoSize,

        // CancellationToken
        CancellationToken ct = default)
        => PlacePriceTriggeredOrderAsync(settle, new GateFuturesPriceTriggeredOrderRequest
        {
            Type = triggerType,
            Trigger = new GateFuturesTrigger
            {
                StrategyType = triggerStrategy,
                PriceType = triggerPriceType,
                Price = triggerPrice.ToGateString(),
                Rule = triggerCondition,
                Expiration = Convert.ToInt32(triggerExpiration.TotalSeconds),
            },
            Order = new GateFuturesInitial
            {
                Contract = orderContract,
                Price = orderPrice.ToGateString(),
                Size = orderSize,
                Close = orderClose,
                TimeInForce = orderTimeInForce,
                ClientOrderId = orderClientOrderId,
                ReduceOnly = orderReduceOnly,
                AutoSize = orderAutoSize,
            }
        }, ct);

    // Create a price-triggered order
    internal async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateDeliverySettlement settle, GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(request.Order.Contract);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesPriceTriggeredOrderId>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId ?? default);
    }

    // List all auto orders
    internal Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> GetPriceTriggeredOrdersAsync(
        GateDeliverySettlement settle,
        GateSpotTriggerFilter status,
        string contract = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddEnum("status", status);
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders
    internal Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(contract);
        
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Get a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(GateDeliverySettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(GateDeliverySettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, v4, delivery, endpoint), HttpMethod.Delete, ct, true);
    }

}