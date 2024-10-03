using Gate.IO.Api.Models.RestApi.Futures;
using Gate.IO.Api.Models.RestApi.Spot;

namespace Gate.IO.Api.Futures.Clients;

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
    public GateFuturesDeliverySettleRestApiClient BTC { get; }
    public GateFuturesDeliverySettleRestApiClient USDT { get; }
    public Dictionary<FuturesDeliverySettle, GateFuturesDeliverySettleRestApiClient> Clients { get; }
    public GateFuturesDeliverySettleRestApiClient this[FuturesDeliverySettle settle] => Clients[settle];

    internal GateFuturesDeliveryRestApiClient(GateRestApiClient root)
    {
        Root = root;

        BTC = new GateFuturesDeliverySettleRestApiClient(this, FuturesDeliverySettle.BTC);
        USDT = new GateFuturesDeliverySettleRestApiClient(this, FuturesDeliverySettle.USDT);
        Clients = new Dictionary<FuturesDeliverySettle, GateFuturesDeliverySettleRestApiClient>
        {
            { FuturesDeliverySettle.BTC, BTC },
            { FuturesDeliverySettle.USDT, USDT },
        };
    }

    #region List all futures contracts
    internal Task<RestCallResult<IEnumerable<DeliveryContract>>> GetContractsAsync(FuturesDeliverySettle settle, CancellationToken ct = default)
    {
        var endpoint = settleContractsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<DeliveryContract>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct);
    }
    #endregion

    #region Get a single contract
    internal Task<RestCallResult<DeliveryContract>> GetContractAsync(FuturesDeliverySettle settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleContractsContractEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<DeliveryContract>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct);
    }
    #endregion

    #region Futures order book
    internal Task<RestCallResult<FuturesOrderBook>> GetOrderBookAsync(FuturesDeliverySettle settle, string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        var endpoint = settleOrderBookEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<FuturesOrderBook>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Futures trading history
    internal Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(FuturesDeliverySettle settle, string contract, DateTime from, DateTime to, int limit = 100, long? lastId = null, CancellationToken ct = default)
    => GetTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, lastId, ct);

    internal Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(FuturesDeliverySettle settle, string contract, long? from = null, long? to = null, int limit = 100, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleTradesEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesTrade>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Get futures candlesticks
    internal Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(FuturesDeliverySettle settle, string prefix, string contract, FuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetCandlesticksAsync(settle, prefix, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(FuturesDeliverySettle settle, string prefix, string contract, FuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", prefix + contract },
            { "interval", JsonConvert.SerializeObject(interval, new FuturesCandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleCandlesticksEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesCandlestick>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region List futures tickers
    internal Task<RestCallResult<IEnumerable<DeliveryTicker>>> GetTickersAsync(FuturesDeliverySettle settle, string contract = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleTickersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<DeliveryTicker>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Futures insurance balance history
    internal Task<RestCallResult<IEnumerable<FuturesInsuranceBalance>>> GetInsuranceHistoryAsync(FuturesDeliverySettle settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
        };

        var endpoint = settleInsuranceEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesInsuranceBalance>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Query futures account
    internal Task<RestCallResult<IEnumerable<FuturesAccount>>> GetAccountAsync(FuturesDeliverySettle settle, CancellationToken ct = default)
    {
        var endpoint = settleAccountsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesAccount>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Query account book
    internal Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesDeliverySettle settle, FuturesAccountBookType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetAccountBookAsync(settle, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesDeliverySettle settle, FuturesAccountBookType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FuturesAccountBookTypeConverter(false)));
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleAccountBookEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesAccountBook>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List all positions of a user
    internal Task<RestCallResult<IEnumerable<FuturesPosition>>> GetPositionsAsync(FuturesDeliverySettle settle, CancellationToken ct = default)
    {
        var endpoint = settlePositionsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesPosition>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Get single position
    internal Task<RestCallResult<FuturesPosition>> GetPositionAsync(FuturesDeliverySettle settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settlePositionsContractEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Update position margin
    internal Task<RestCallResult<FuturesPosition>> SetPositionMarginAsync(FuturesDeliverySettle settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "change", change }
        };

        var endpoint = settlePositionsContractMarginEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position leverage
    internal Task<RestCallResult<FuturesPosition>> SetLeverageAsync(FuturesDeliverySettle settle, string contract, int leverage, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "leverage", leverage }
        };

        var endpoint = settlePositionsContractLeverageEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position risk limit
    internal Task<RestCallResult<FuturesPosition>> SetRiskLimitAsync(FuturesDeliverySettle settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "risk_limit", riskLimit }
        };

        var endpoint = settlePositionsContractRiskLimitEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Create a futures order
    internal Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(
        FuturesDeliverySettle settle,
        string contract,
        long size,
        long iceberg = 0,
        decimal? price = null,
        bool? close = null,
        bool? reduceOnly = null,
        string clientOrderId = "",
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

    internal Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(FuturesDeliverySettle settle, FuturesOrderRequest request, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(request.Contract);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new Dictionary<string, object> {
            { "currency_pair", request.Contract },
            { "size", request.Size },
            { "iceberg", request.Iceberg },
        };
        parameters.AddOptionalParameter("price", request.Price);
        parameters.AddOptionalParameter("close", request.Close);
        parameters.AddOptionalParameter("reduce_only", request.ReduceOnly);
        parameters.AddOptionalParameter("tif", JsonConvert.SerializeObject(request.TimeInForce, new FuturesTimeInForceConverter(false)));
        parameters.AddOptionalParameter("text", request.ClientOrderId);
        parameters.AddOptionalParameter("auto_size", JsonConvert.SerializeObject(request.AutoSize, new FuturesOrderAutoSizeConverter(false)));


        var endpoint = settleOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
    #endregion

    #region List futures orders
    internal Task<RestCallResult<IEnumerable<FuturesOrder>>> GetOrdersAsync(FuturesDeliverySettle settle, string contract, FuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "status", JsonConvert.SerializeObject(status, new FuturesOrderStatusConverter(false)) },
            { "contract", contract },
            { "offset", offset },
            { "limit", limit },
            { "count_total", countTotal ? 1 : 0 },
        };
        parameters.AddOptionalParameter("last_id", lastId);

        var endpoint = settleOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesOrder>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders matched
    internal Task<RestCallResult<IEnumerable<FuturesOrder>>> CancelOpenOrdersAsync(FuturesDeliverySettle settle, string contract, FuturesOrderSide side, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "side", JsonConvert.SerializeObject(side, new FuturesOrderSideConverter(false)) },
            { "contract", contract },
        };

        var endpoint = settleOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesOrder>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Get a single order
    internal Task<RestCallResult<FuturesOrder>> GetOrderAsync(FuturesDeliverySettle settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a single order
    internal Task<RestCallResult<FuturesOrder>> CancelOrderAsync(FuturesDeliverySettle settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

    #region List personal trading history
    internal Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(FuturesDeliverySettle settle, string contract = "", long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, bool countTotal = false, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "offset", offset },
            { "limit", limit },
            { "count_total", countTotal ? 1 : 0 },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("order", orderId);

        var endpoint = settleMyTradesEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesUserTrade>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List position close history
    internal Task<RestCallResult<IEnumerable<FuturesPositionClose>>> GetPositionClosesAsync(FuturesDeliverySettle settle, string contract = "", int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settlePositionCloseEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesPositionClose>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List liquidation history
    internal Task<RestCallResult<IEnumerable<FuturesUserLiquidate>>> GetUserLiquidatesAsync(FuturesDeliverySettle settle, string contract = "", int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = settleLiquidatesEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesUserLiquidate>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List settlement history
    internal Task<RestCallResult<IEnumerable<DeliverySettlement>>> GetUserSettlementsAsync(FuturesDeliverySettle settle, string contract = "", int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = settleSettlementsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<DeliverySettlement>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Create a price-triggered order
    internal Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        FuturesDeliverySettle settle,
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

    internal async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesDeliverySettle settle, FuturesTriggerOrderRequest request, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(request.Order.Contract);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        var result = await Root.SendRequestInternal<FuturesTriggerOrderId>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId ?? default);
    }
    #endregion

    #region List all auto orders
    internal Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(
    FuturesDeliverySettle settle,
    GateSpotTriggerFilter status,
    string contract = "",
    int limit = 100,
    int offset = 0,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "status", JsonConvert.SerializeObject(status, new PriceTriggerFilterConverter(false)) },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesTriggerOrderResponse>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders
    internal Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(FuturesDeliverySettle settle, string contract, CancellationToken ct = default)
    {
        DeliveryHelpers.ValidateContractSymbol(contract);

        var parameters = new Dictionary<string, object>
        {
            { "contract", contract }
        };

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesTriggerOrderResponse>>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Get a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> GetPriceTriggeredOrderAsync(FuturesDeliverySettle settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{order_id}", orderId.ToString());
        return Root.SendRequestInternal<FuturesTriggerOrderResponse>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(FuturesDeliverySettle settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesDeliverySettleConverter(false)))
            .Replace("{order_id}", orderId.ToString());
        return Root.SendRequestInternal<FuturesTriggerOrderResponse>(Root.GetUrl(api, version, delivery, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

}