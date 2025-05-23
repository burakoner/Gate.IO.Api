namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Perpetual REST API Client
/// </summary>
public class GateFuturesRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string futures = "futures";

    // Root Client
    internal GateRestApiClient _ { get; }

    /// <summary>
    /// BTC Settled Perpetual Futures Client
    /// </summary>
    public GateFuturesRestApiSettleClient BTC { get; }

    /// <summary>
    /// USDT Settled Perpetual Futures Client
    /// </summary>
    public GateFuturesRestApiSettleClient USDT { get; }

    /// <summary>
    /// Get a perpetual futures settle client
    /// </summary>
    /// <param name="settle">Perpetual Settlement Asset</param>
    /// <returns></returns>
    public GateFuturesRestApiSettleClient this[GateFuturesSettlement settle] => Clients[settle];
    private Dictionary<GateFuturesSettlement, GateFuturesRestApiSettleClient> Clients { get; }

    // Constructor
    internal GateFuturesRestApiClient(GateRestApiClient root)
    {
        _ = root;

        BTC = new GateFuturesRestApiSettleClient(this, GateFuturesSettlement.BTC);
        USDT = new GateFuturesRestApiSettleClient(this, GateFuturesSettlement.USDT);
        Clients = new Dictionary<GateFuturesSettlement, GateFuturesRestApiSettleClient>
        {
            { GateFuturesSettlement.BTC, BTC },
            { GateFuturesSettlement.USDT, USDT },
        };
    }

    // List all futures contracts
    internal Task<RestCallResult<List<GateFuturesContract>>> GetContractsAsync(GateFuturesSettlement settle, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "offset", offset },
            { "limit", limit },
        };
        var endpoint = "{settle}/contracts".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesContract>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get a single contract
    internal Task<RestCallResult<GateFuturesContract>> GetContractAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/contracts/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesContract>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct);
    }

    // Futures order book
    internal Task<RestCallResult<GateFuturesOrderBook>> GetOrderBookAsync(GateFuturesSettlement settle, string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        var endpoint = "{settle}/order_book".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrderBook>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures trading history
    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    => GetTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, lastId, ct);

    // Futures trading history
    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
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
        return _.SendRequestInternal<List<GateFuturesTrade>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get futures candlesticks
    internal Task<RestCallResult<List<GateFuturesCandlestick>>> GetCandlesticksAsync(GateFuturesSettlement settle, string prefix, string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetCandlesticksAsync(settle, prefix, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    // Get futures candlesticks
    internal Task<RestCallResult<List<GateFuturesCandlestick>>> GetCandlesticksAsync(GateFuturesSettlement settle, string prefix, string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", prefix + contract },
        };
        parameters.AddEnum("interval", interval);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);

        var endpoint = "{settle}/candlesticks".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesCandlestick>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Premium Index K-Line
    internal Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(GateFuturesSettlement settle, string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetPremiumIndexCandlesticksAsync(settle, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    // Premium Index K-Line
    internal Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(GateFuturesSettlement settle, string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddEnum("interval", interval);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);

        var endpoint = "{settle}/premium_index".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesCandlestickPremium>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // List futures tickers
    internal Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(GateFuturesSettlement settle, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/tickers".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesTicker>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Funding rate history
    internal Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => GetFundingRateHistoryAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    // Funding rate history
    internal Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = "{settle}/funding_rate".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesFundingRate>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures insurance balance history
    internal Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(GateFuturesSettlement settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };

        var endpoint = "{settle}/insurance".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesInsuranceBalance>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures stats
    internal Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(GateFuturesSettlement settle, string contract, GateFuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
    => GetStatsAsync(settle, contract, interval, from.ConvertToMilliseconds(), limit, ct);

    // Futures stats
    internal Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(GateFuturesSettlement settle, string contract, GateFuturesStatsInterval? interval = null, long? from = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "limit", limit },
        };
        parameters.AddOptionalEnum("interval", interval);
        parameters.AddOptionalParameter("from", from);

        var endpoint = "{settle}/contract_stats".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesStats>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get index constituents
    internal Task<RestCallResult<GateFuturesIndexConstituents>> GetIndexConstituentsAsync(GateFuturesSettlement settle, string index, CancellationToken ct = default)
    {
        var endpoint = "{settle}/index_constituents/{index}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{index}", index);
        return _.SendRequestInternal<GateFuturesIndexConstituents>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct);
    }

    // Retrieve liquidation history
    internal Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidationsAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetLiquidationsAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    // Retrieve liquidation history
    internal Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidationsAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = "{settle}/liq_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesLiquidation>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // TODO: List risk limit tiers

    // Query futures account
    internal Task<RestCallResult<GateFuturesBalance>> GetBalancesAsync(GateFuturesSettlement settle, CancellationToken ct = default)
    {
        var endpoint = "{settle}/accounts".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesBalance>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesBalanceChangeType type, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetBalanceHistoryAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), type, limit, offset, ct);

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesBalanceChangeType? type = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/account_book".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBalanceChange>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List all positions of a user
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(GateFuturesSettlement settle, bool? holding = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("holding", holding);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/positions".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Get single position
    internal Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/positions/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Update position margin
    internal Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(GateFuturesSettlement settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("change", change);

        var endpoint = "{settle}/positions/{contract}/margin"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position leverage
    internal Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(GateFuturesSettlement settle, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("leverage", leverage);
        parameters.AddOptionalString("cross_leverage_limit", crossLeverageLimit);

        var endpoint = "{settle}/positions/{contract}/leverage"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // TODO: Switch to the full position-by-store mode

    // Update position risk limit
    internal Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(GateFuturesSettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = "{settle}/positions/{contract}/risk_limit"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Enable or disable dual mode
    internal Task<RestCallResult<GateFuturesBalance>> SetDualModeAsync(GateFuturesSettlement settle, bool dualMode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "dual_mode", dualMode.ToString().ToLower() }
        };

        var endpoint = "{settle}/dual_mode"
            .Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesBalance>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Retrieve position detail in dual mode
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetDualModePositionsAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/dual_comp/positions/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Update position margin in dual mode
    internal Task<RestCallResult<GateFuturesPosition>> SetDualModeMarginAsync(GateFuturesSettlement settle, string contract, GateFuturesDualModeSide side, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("dual_side", side);
        parameters.AddString("change", change);

        var endpoint = "{settle}/dual_comp/positions/{contract}/margin"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position leverage in dual mode
    internal Task<RestCallResult<GateFuturesPosition>> SetDualModeLeverageAsync(GateFuturesSettlement settle, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("leverage", leverage);
        parameters.AddOptionalString("cross_leverage_limit", crossLeverageLimit);

        var endpoint = "{settle}/dual_comp/positions/{contract}/leverage"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position risk limit in dual mode
    internal Task<RestCallResult<GateFuturesPosition>> SetDualModeRiskLimitAsync(GateFuturesSettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = "{settle}/dual_comp/positions/{contract}/risk_limit"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Create a futures order
    internal Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(
        GateFuturesSettlement settle, 
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
    internal Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(GateFuturesSettlement settle, GateFuturesOrderRequest request, CancellationToken ct = default)
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
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // List futures orders
    internal Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(GateFuturesSettlement settle, string contract, GateFuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddEnum("status", status);
        parameters.AddOptionalParameter("last_id", lastId);

        var endpoint = "{settle}/orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders matched
    internal Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(GateFuturesSettlement settle, string contract, GateFuturesOrderSide? side = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddOptionalEnum("side", side);

        var endpoint = "{settle}/orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // TODO: List Futures Orders By Time Range

    // Create a batch of futures orders
    internal Task<RestCallResult<List<GateFuturesBatchOrder>>> PlaceOrdersAsync(GateFuturesSettlement settle, IEnumerable<GateFuturesOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
        {
            PerpetualHelpers.ValidateContractSymbol(request.Contract);
            ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);
        }

        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        var endpoint = "{settle}/batch_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBatchOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Get a single order
    internal Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a single order
    internal Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true);
    }

    // Amend an order
    internal Task<RestCallResult<GateFuturesOrder>> AmendOrderAsync(GateFuturesSettlement settle,
        long? orderId = null,
        string clientOrderId = null,
        long? size = null,
        decimal? price = null,
        string amendText = null,
        string businessInfo = null,
        string bbo = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalString("size", size);
        parameters.AddOptionalString("price", price);
        parameters.AddOptional("amend_text", amendText);
        parameters.AddOptional("biz_info", businessInfo);
        parameters.AddOptional("bbo", bbo);

        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    // List personal trading history
    internal Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("order", orderId);

        var endpoint = "{settle}/my_trades".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserTrade>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List personal trading history by time range
    internal Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetUserTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), role, limit, offset, ct);

    // List personal trading history by time range
    internal Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptional("contract", contract);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptionalEnum("role", role);

        var endpoint = "{settle}/my_trades_timerange".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserTrade>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List position close history
    internal Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetPositionClosesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), side, pnl, limit, offset, ct);

    // List position close history
    internal Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptional("contract", contract);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptionalEnum("side", side);
        parameters.AddOptionalString("pnl", pnl);

        var endpoint = "{settle}/position_close".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPositionClose>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List liquidation history
    internal Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidationsAsync(GateFuturesSettlement settle, string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = "{settle}/liquidates".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserLiquidation>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List Auto-Deleveraging History

    // Countdown cancel orders
    internal async Task<RestCallResult<DateTime>> CancelAllAsync(GateFuturesSettlement settle, int timeout, string contract = null, CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(contract)) PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new ParameterCollection {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/countdown_cancel_all".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesCountdown>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Time ?? default);
    }

    // TODO: Query user trading fee rates
    // TODO: Cancel a batch of orders with an ID list
    // TODO: Batch modify orders with specified IDs

    // Create a price-triggered order
    internal Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        // Settlement
        GateFuturesSettlement settle,

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
    internal async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateFuturesSettlement settle, GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(request.Order.Contract);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesPriceTriggeredOrderId>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId ?? default);
    }

    // List all auto orders
    internal Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> GetPriceTriggeredOrdersAsync(
        GateFuturesSettlement settle,
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
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders
    internal Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(GateFuturesSettlement settle, string contract = null, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Get a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true);
    }

}