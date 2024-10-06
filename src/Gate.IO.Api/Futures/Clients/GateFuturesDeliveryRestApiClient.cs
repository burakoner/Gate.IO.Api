namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Delivery REST API Client
/// </summary>
public class GateFuturesDeliveryRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string section = "delivery";

    // Endpoints
    private const string settleContractsEndpoint = "{settle}/contracts";
    private const string settleContractsContractEndpoint = "{settle}/contracts/{contract}";
    private const string settleOrderBookEndpoint = "{settle}/order_book";
    private const string settleTradesEndpoint = "{settle}/trades";
    private const string settleCandlesticksEndpoint = "{settle}/candlesticks";
    private const string settleTickersEndpoint = "{settle}/tickers";
    private const string settleInsuranceEndpoint = "{settle}/insurance";
    private const string settleAccountsEndpoint = "{settle}/accounts";
    private const string settleAccountBookEndpoint = "{settle}/account_book";
    private const string settlePositionsEndpoint = "{settle}/positions";
    private const string settlePositionsContractEndpoint = "{settle}/positions/{contract}";
    private const string settlePositionsContractMarginEndpoint = "{settle}/positions/{contract}/margin";
    private const string settlePositionsContractLeverageEndpoint = "{settle}/positions/{contract}/leverage";
    private const string settlePositionsContractRiskLimitEndpoint = "{settle}/positions/{contract}/risk_limit";
    private const string settleOrdersEndpoint = "{settle}/orders";
    private const string settleOrdersOrderIdEndpoint = "{settle}/orders/{order_id}";
    private const string settleMyTradesEndpoint = "{settle}/my_trades";
    private const string settlePositionCloseEndpoint = "{settle}/position_close";
    private const string settleLiquidatesEndpoint = "{settle}/liquidates";
    private const string settleSettlementsEndpoint = "{settle}/settlements";
    private const string settlePriceOrdersEndpoint = "{settle}/price_orders";
    private const string settlePriceOrdersOrderIdEndpoint = "{settle}/price_orders/{order_id}";

    // Root Client
    internal GateRestApiClient _ { get; }

    /// <summary>
    /// USDT Settled Delivery Futures Client
    /// </summary>
    public GateFuturesDeliverySettleRestApiClient USDT { get; }

    /// <summary>
    /// Get a delivery futures settle client
    /// </summary>
    /// <param name="settle">Delivery Settlement Asset</param>
    /// <returns></returns>
    public GateFuturesDeliverySettleRestApiClient this[GateDeliverySettlement settle] => Clients[settle];
    private Dictionary<GateDeliverySettlement, GateFuturesDeliverySettleRestApiClient> Clients { get; }

    // Constructor
    internal GateFuturesDeliveryRestApiClient(GateRestApiClient root)
    {
        _ = root;

        USDT = new GateFuturesDeliverySettleRestApiClient(this, GateDeliverySettlement.USDT);
        Clients = new Dictionary<GateDeliverySettlement, GateFuturesDeliverySettleRestApiClient>
        {
            { GateDeliverySettlement.USDT, USDT },
        };
    }

    // List all futures contracts
    internal Task<RestCallResult<List<GateDeliveryContract>>> GetContractsAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = settleContractsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateDeliveryContract>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct);
    }

    // Get a single contract
    internal Task<RestCallResult<GateDeliveryContract>> GetContractAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleContractsContractEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateDeliveryContract>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct);
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

        var endpoint = settleOrderBookEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrderBook>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
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

        var endpoint = settleTradesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesTrade>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
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

        var endpoint = settleCandlesticksEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesCandlestick>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // List futures tickers
    internal Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(GateDeliverySettlement settle, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleTickersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesTicker>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures insurance balance history
    internal Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(GateDeliverySettlement settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };

        var endpoint = settleInsuranceEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesInsuranceBalance>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Query futures account
    internal Task<RestCallResult<List<GateFuturesBalance>>> GetBalancesAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = settleAccountsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBalance>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true);
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

        var endpoint = settleAccountBookEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBalanceChange>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List all positions of a user
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = settlePositionsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true);
    }

    // Get single position
    internal Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settlePositionsContractEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true);
    }

    // Update position margin
    internal Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(GateDeliverySettlement settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("change", change);

        var endpoint = settlePositionsContractMarginEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, section, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position leverage
    internal Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(GateDeliverySettlement settle, string contract, decimal leverage, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("leverage", leverage);

        var endpoint = settlePositionsContractLeverageEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, section, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position risk limit
    internal Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(GateDeliverySettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = settlePositionsContractRiskLimitEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, section, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
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

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, section, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
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

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders matched
    internal Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(GateDeliverySettlement settle, string contract, GateFuturesOrderSide side, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddEnum("side", side);

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Get a single order
    internal Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(GateDeliverySettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {        
        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a single order
    internal Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(GateDeliverySettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, section, endpoint), HttpMethod.Delete, ct, true);
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

        var endpoint = settleMyTradesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserTrade>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List position close history
    internal Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settlePositionCloseEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPositionClose>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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

        var endpoint = settleLiquidatesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserLiquidation>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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

        var endpoint = settleSettlementsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateDeliveryUserSettlement>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: List risk limit tiers

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

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesPriceTriggeredOrderId>(_.GetUrl(api, version, section, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
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

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders
    internal Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(contract);
        
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, version, section, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Get a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(GateDeliverySettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, version, section, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(GateDeliverySettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, version, section, endpoint), HttpMethod.Delete, ct, true);
    }

}