namespace Gate.IO.Api.Futures;

public class GateFuturesDeliveryRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string delivery = "delivery";

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
    internal GateRestApiClient Root { get; }

    // Public Clients
    public GateFuturesDeliverySettleRestApiClient USDT { get; }
    public Dictionary<GateDeliverySettlement, GateFuturesDeliverySettleRestApiClient> Clients { get; }
    public GateFuturesDeliverySettleRestApiClient this[GateDeliverySettlement settle] => Clients[settle];

    internal GateFuturesDeliveryRestApiClient(GateRestApiClient root)
    {
        Root = root;

        USDT = new GateFuturesDeliverySettleRestApiClient(this, GateDeliverySettlement.USDT);
        Clients = new Dictionary<GateDeliverySettlement, GateFuturesDeliverySettleRestApiClient>
        {
            { GateDeliverySettlement.USDT, USDT },
        };
    }

    #region List all futures contracts
    internal Task<RestCallResult<List<DeliveryContract>>> GetContractsAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = settleContractsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<DeliveryContract>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct);
    }
    #endregion

    #region Get a single contract
    internal Task<RestCallResult<DeliveryContract>> GetContractAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleContractsContractEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<DeliveryContract>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct);
    }
    #endregion

    #region Futures order book
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
        return Root.SendRequestInternal<GateFuturesOrderBook>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Futures trading history
    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateDeliverySettlement settle, string contract, DateTime from, DateTime to, int limit = 100, long? lastId = null, CancellationToken ct = default)
    => GetTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, lastId, ct);

    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateDeliverySettlement settle, string contract, long? from = null, long? to = null, int limit = 100, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleTradesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<GateFuturesTrade>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Get futures candlesticks
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
        return Root.SendRequestInternal<List<GateFuturesCandlestick>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region List futures tickers
    internal Task<RestCallResult<List<DeliveryTicker>>> GetTickersAsync(GateDeliverySettlement settle, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleTickersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<DeliveryTicker>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Futures insurance balance history
    internal Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(GateDeliverySettlement settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };

        var endpoint = settleInsuranceEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<GateFuturesInsuranceBalance>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Query futures account
    internal Task<RestCallResult<List<GateFuturesBalance>>> GetAccountAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = settleAccountsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<GateFuturesBalance>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetAccountBookAsync(GateDeliverySettlement settle, GateFuturesBalanceType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetAccountBookAsync(settle, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetAccountBookAsync(GateDeliverySettlement settle, GateFuturesBalanceType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleAccountBookEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<GateFuturesBalanceChange>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List all positions of a user
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(GateDeliverySettlement settle, CancellationToken ct = default)
    {
        var endpoint = settlePositionsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<GateFuturesPosition>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Get single position
    internal Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settlePositionsContractEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<GateFuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Update position margin
    internal Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(GateDeliverySettlement settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "change", change }
        };

        var endpoint = settlePositionsContractMarginEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<GateFuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position leverage
    internal Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(GateDeliverySettlement settle, string contract, int leverage, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "leverage", leverage }
        };

        var endpoint = settlePositionsContractLeverageEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<GateFuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position risk limit
    internal Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(GateDeliverySettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "risk_limit", riskLimit }
        };

        var endpoint = settlePositionsContractRiskLimitEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<GateFuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Create a futures order
    internal Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(
        GateDeliverySettlement settle,
        string contract,
        long size,
        long iceberg = 0,
        decimal? price = null,
        bool? close = null,
        bool? reduceOnly = null,
        string clientOrderId = null,
        FuturesTimeInForce? timeInForce = null,
        FuturesOrderAutoSize? autoSize = null,
        CancellationToken ct = default)
        => PlaceOrderAsync(settle, new FuturesOrderRequest
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
        }, ct);

    internal Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(GateDeliverySettlement settle, FuturesOrderRequest request, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(request.Contract);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new ParameterCollection {
            { "currency_pair", request.Contract },
            { "size", request.Size },
            { "iceberg", request.Iceberg },
        };
        parameters.AddOptionalParameter("price", request.Price);
        parameters.AddOptionalParameter("close", request.Close);
        parameters.AddOptionalParameter("reduce_only", request.ReduceOnly);
        parameters.AddOptionalEnum("tif", request.TimeInForce);
        parameters.AddOptionalParameter("text", request.ClientOrderId);
        parameters.AddOptionalEnum("auto_size", request.AutoSize);


        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
    #endregion

    #region List futures orders
    internal Task<RestCallResult<List<FuturesOrder>>> GetOrdersAsync(GateDeliverySettlement settle, string contract, FuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "offset", offset },
            { "limit", limit },
            { "count_total", countTotal ? 1 : 0 },
        };
        parameters.AddEnum("status", status);
        parameters.AddOptionalParameter("last_id", lastId);

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<FuturesOrder>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders matched
    internal Task<RestCallResult<List<FuturesOrder>>> CancelOpenOrdersAsync(GateDeliverySettlement settle, string contract, FuturesOrderSide side, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddEnum("side", side);

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<FuturesOrder>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Get a single order
    internal Task<RestCallResult<FuturesOrder>> GetOrderAsync(GateDeliverySettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a single order
    internal Task<RestCallResult<FuturesOrder>> CancelOrderAsync(GateDeliverySettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

    #region List personal trading history
    internal Task<RestCallResult<List<FuturesUserTrade>>> GetUserTradesAsync(GateDeliverySettlement settle, string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
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
        return Root.SendRequestInternal<List<FuturesUserTrade>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List position close history
    internal Task<RestCallResult<List<FuturesPositionClose>>> GetPositionClosesAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settlePositionCloseEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<FuturesPositionClose>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List liquidation history
    internal Task<RestCallResult<List<FuturesUserLiquidate>>> GetUserLiquidatesAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = settleLiquidatesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<FuturesUserLiquidate>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List settlement history
    internal Task<RestCallResult<List<DeliverySettlement>>> GetUserSettlementsAsync(GateDeliverySettlement settle, string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = settleSettlementsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<DeliverySettlement>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Create a price-triggered order
    internal Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        GateDeliverySettlement settle,
        FuturesTriggerOrderType type,
        FuturesTriggerOrderPriceType triggerPriceType,
        FuturesTriggerOrderStrategyType triggerStrategyType,
        decimal triggerPrice,
        GateSpotTriggerCondition triggerCondition,
        TimeSpan triggerExpiration,
        string orderContract,
        decimal orderPrice,
        long orderSize,
        CancellationToken ct = default)
        => PlacePriceTriggeredOrderAsync(settle, new FuturesTriggerOrderRequest
        {
            Type = type,
            Trigger = new FuturesPriceTrigger
            {
                Price = triggerPrice.ToGateString(),
                Rule = triggerCondition,
                Expiration = Convert.ToInt32(triggerExpiration.TotalSeconds),
                PriceType = triggerPriceType,
                StrategyType = triggerStrategyType,
            },
            Order = new FuturesPriceOrder
            {
                Price = orderPrice.ToGateString(),
                Contract = orderContract,
                Size = orderSize,
            }
        }, ct);

    internal async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateDeliverySettlement settle, FuturesTriggerOrderRequest request, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(request.Order.Contract);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        var result = await Root.SendRequestInternal<FuturesTriggerOrderId>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId ?? default);
    }
    #endregion

    #region List all auto orders
    internal Task<RestCallResult<List<FuturesTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(
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
        return Root.SendRequestInternal<List<FuturesTriggerOrderResponse>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders
    internal Task<RestCallResult<List<FuturesTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(GateDeliverySettlement settle, string contract, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(contract);

        var parameters = new ParameterCollection
        {
            { "contract", contract }
        };

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return Root.SendRequestInternal<List<FuturesTriggerOrderResponse>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Get a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> GetPriceTriggeredOrderAsync(GateDeliverySettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return Root.SendRequestInternal<FuturesTriggerOrderResponse>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(GateDeliverySettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return Root.SendRequestInternal<FuturesTriggerOrderResponse>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

}