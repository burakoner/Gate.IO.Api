using ApiSharp.Rest;

namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Perpetual REST API Client
/// </summary>
public class GateFuturesRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
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
        return _.SendRequestInternal<List<GateFuturesContract>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get a single contract
    internal Task<RestCallResult<GateFuturesContract>> GetContractAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/contracts/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesContract>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct);
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
        return _.SendRequestInternal<GateFuturesOrderBook>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures trading history
    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    => GetTradesAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), limit, offset, lastId, ct);

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
        return _.SendRequestInternal<List<GateFuturesTrade>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get futures candlesticks
    internal Task<RestCallResult<List<GateFuturesCandlestick>>> GetCandlesticksAsync(GateFuturesSettlement settle, string prefix, string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetCandlesticksAsync(settle, prefix, contract, interval, from.ConvertToSeconds(), to.ConvertToSeconds(), limit, ct);

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
        return _.SendRequestInternal<List<GateFuturesCandlestick>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Premium Index K-Line
    internal Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(GateFuturesSettlement settle, string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetPremiumIndexCandlesticksAsync(settle, contract, interval, from.ConvertToSeconds(), to.ConvertToSeconds(), limit, ct);

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
        return _.SendRequestInternal<List<GateFuturesCandlestickPremium>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // List futures tickers
    internal Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(GateFuturesSettlement settle, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/tickers".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesTicker>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Funding rate history
    internal Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => GetFundingRateHistoryAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), limit, ct);

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
        return _.SendRequestInternal<List<GateFuturesFundingRate>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Batch Query Historical Funding Rate Data for Perpetual Contracts
    internal async Task<RestCallResult<List<GateFuturesBatchFundingRate>>> GetBatchFundingRateHistoryAsync(GateFuturesSettlement settle, GateFuturesBatchFundingRateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = "{settle}/funding_rates".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, false, bodyParameters: parameters);
        if (!result.Success) return result.As<List<GateFuturesBatchFundingRate>>([]);

        if (result.Data is not JArray array)
            return result.As<List<GateFuturesBatchFundingRate>>([]);

        var rates = array.First?.Type == JTokenType.Array
            ? array.ToObject<List<List<GateFuturesBatchFundingRate>>>()?.SelectMany(x => x).ToList()
            : array.ToObject<List<GateFuturesBatchFundingRate>>();

        return result.As(rates ?? []);
    }

    // Futures insurance balance history
    internal Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(GateFuturesSettlement settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };

        var endpoint = "{settle}/insurance".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesInsuranceBalance>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures stats
    internal Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(GateFuturesSettlement settle, string contract, GateFuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
    => GetStatsAsync(settle, contract, interval, from.ConvertToSeconds(), limit, ct);

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
        return _.SendRequestInternal<List<GateFuturesStats>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get index constituents
    internal Task<RestCallResult<GateFuturesIndexConstituents>> GetIndexConstituentsAsync(GateFuturesSettlement settle, string index, CancellationToken ct = default)
    {
        var endpoint = "{settle}/index_constituents/{index}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{index}", index);
        return _.SendRequestInternal<GateFuturesIndexConstituents>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct);
    }

    // Retrieve liquidation history
    internal Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidationsAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetLiquidationsAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), limit, ct);

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
        return _.SendRequestInternal<List<GateFuturesLiquidation>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // List risk limit tiers
    internal Task<RestCallResult<List<GateFuturesRiskLimitTier>>> GetRiskLimitTiersAsync(GateFuturesSettlement settle, string contract = null, int limit = 100, long? offset = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("offset", offset);

        var endpoint = "{settle}/risk_limit_tiers".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesRiskLimitTier>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Query futures account
    internal Task<RestCallResult<GateFuturesBalance>> GetBalancesAsync(GateFuturesSettlement settle, CancellationToken ct = default)
    {
        var endpoint = "{settle}/accounts".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesBalance>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesBalanceChangeType type, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetBalanceHistoryAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), type, limit, offset, ct);

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
        return _.SendRequestInternal<List<GateFuturesBalanceChange>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List all positions of a user
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(GateFuturesSettlement settle, bool? holding = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("holding", holding);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/positions".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Get user's historical position information list by time
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetHistoricalPositionsAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetHistoricalPositionsAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), limit, offset, ct);

    // Get user's historical position information list by time
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetHistoricalPositionsAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/positions_timerange".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Get single position
    internal Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/positions/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Get leverage information for specified mode
    internal Task<RestCallResult<GateFuturesLeverage>> GetLeverageAsync(GateFuturesSettlement settle, string contract, GateFuturesPositionMarginMode positionMarginMode, GateFuturesDualModeSide dualSide, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("pos_margin_mode", positionMarginMode);
        parameters.AddEnum("dual_side", dualSide);

        var endpoint = "{settle}/get_leverage/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesLeverage>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Update position margin
    internal Task<RestCallResult<GateFuturesPosition>> SetMarginAsync(GateFuturesSettlement settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("change", change);

        var endpoint = "{settle}/positions/{contract}/margin"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position leverage
    internal Task<RestCallResult<GateFuturesPosition>> SetLeverageAsync(GateFuturesSettlement settle, string contract, decimal leverage, decimal? crossLeverageLimit = null, int? pid = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("leverage", leverage);
        parameters.AddOptionalString("cross_leverage_limit", crossLeverageLimit);
        parameters.AddOptional("pid", pid);

        var endpoint = "{settle}/positions/{contract}/leverage"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    internal Task<RestCallResult<GateFuturesPosition>> SetMarginModeAsync(GateFuturesSettlement settle, string contract, GateFuturesMarginMode mode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("contract", contract);
        parameters.AddEnum("mode", mode);

        var endpoint = "{settle}/positions/cross_mode"
            .Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    internal Task<RestCallResult<GateFuturesPosition>> SwithMarginModeUnderHedgeAsync(GateFuturesSettlement settle, string contract, GateFuturesMarginMode mode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("contract", contract);
        parameters.AddEnum("mode", mode);

        var endpoint = "{settle}/dual_comp/positions/cross_mode"
            .Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Update position risk limit
    internal Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(GateFuturesSettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = "{settle}/positions/{contract}/risk_limit"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
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
        return _.SendRequestInternal<GateFuturesBalance>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Retrieve position detail in dual mode
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetDualModePositionsAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = "{settle}/dual_comp/positions/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
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
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
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
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position risk limit in dual mode
    internal Task<RestCallResult<GateFuturesPosition>> SetDualModeRiskLimitAsync(GateFuturesSettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = "{settle}/dual_comp/positions/{contract}/risk_limit"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Create a futures order
    internal Task<RestCallResult<GateFuturesOrder>> PlaceOrderAsync(
        GateFuturesSettlement settle,
        string contract,
        decimal size,
        decimal? iceberg = null,
        decimal price = 0,
        bool? close = null,
        bool? reduceOnly = null,
        string clientOrderId = null,
        GateFuturesTimeInForce? timeInForce = null,
        GateFuturesOrderAutoSize? autoSize = null,
        GateFuturesSelfTradeAction? selfTradeAction = null,
        decimal? marketOrderSlipRatio = null,
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
            MarketOrderSlipRatio = marketOrderSlipRatio,
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
        parameters.AddString("price", request.Price);
        parameters.AddOptional("close", request.Close);
        parameters.AddOptional("reduce_only", request.ReduceOnly);
        parameters.AddOptionalEnum("tif", request.TimeInForce);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptionalEnum("auto_size", request.AutoSize);
        parameters.AddOptionalEnum("stp_act", request.SelfTradeAction);
        parameters.AddOptional("pid", request.PositionId);
        parameters.AddOptionalString("market_order_slip_ratio", request.MarketOrderSlipRatio);
        parameters.AddOptionalEnum("pos_margin_mode", request.PositionMarginMode);
        parameters.AddOptionalEnum("action_mode", request.ActionMode);
        parameters.AddOptionalString("tpsl_tp_trigger_price", request.TakeProfitTriggerPrice);
        parameters.AddOptionalString("tpsl_sl_trigger_price", request.StopLossTriggerPrice);
        parameters.AddOptional("tpsl_tp_bbo_type", request.TakeProfitBboType);
        parameters.AddOptional("tpsl_sl_bbo_type", request.StopLossBboType);

        var endpoint = "{settle}/orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders matched
    internal Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(GateFuturesSettlement settle, string contract, GateFuturesOrderSide? side = null, CancellationToken ct = default)
        => CancelOrdersAsync(settle, new GateFuturesOrderCancelAllRequest { Contract = contract, Side = side }, ct);

    // Cancel all open orders matched
    internal Task<RestCallResult<List<GateFuturesOrder>>> CancelOrdersAsync(GateFuturesSettlement settle, GateFuturesOrderCancelAllRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptionalEnum("action_mode", request.ActionMode);
        parameters.AddOptionalEnum("side", request.Side);
        parameters.AddOptional("exclude_reduce_only", request.ExcludeReduceOnly?.ToString().ToLowerInvariant());
        parameters.AddOptional("text", request.Text);

        var endpoint = "{settle}/orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // List Futures Orders By Time Range
    internal Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(GateFuturesSettlement settle, string contract = null, DateTime? from = null, DateTime? to = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        => GetOrdersAsync(settle, contract, from?.ConvertToSeconds(), to?.ConvertToSeconds(), limit, offset, ct);

    // List Futures Orders By Time Range
    internal Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, int? limit = null, int? offset = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/orders_timerange".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

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
        return _.SendRequestInternal<List<GateFuturesBatchOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Get a single order
    internal Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a single order
    internal Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, GateFuturesActionMode? actionMode = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("action_mode", actionMode);

        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Amend an order
    internal Task<RestCallResult<GateFuturesOrder>> AmendOrderAsync(GateFuturesSettlement settle,
        long? orderId = null,
        string clientOrderId = null,
        decimal? size = null,
        decimal? price = null,
        string amendText = null,
        string text = null,
        GateFuturesActionMode? actionMode = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalString("size", size);
        parameters.AddOptionalString("price", price);
        parameters.AddOptional("amend_text", amendText);
        parameters.AddOptional("text", text);
        parameters.AddOptionalEnum("action_mode", actionMode);

        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", _.CheckOrderId(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Put, ct, true, bodyParameters: parameters);
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
        return _.SendRequestInternal<List<GateFuturesUserTrade>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List personal trading history by time range
    internal Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract, DateTime? from, DateTime? to, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetUserTradesAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), role, limit, offset, ct);

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
        return _.SendRequestInternal<List<GateFuturesUserTrade>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List position close history
    internal Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetPositionClosesAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), side, pnl, limit, offset, ct);

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
        return _.SendRequestInternal<List<GateFuturesPositionClose>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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
        return _.SendRequestInternal<List<GateFuturesUserLiquidation>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List Auto-Deleveraging History
    internal Task<RestCallResult<List<GateFuturesAdlRecord>>> GetAdlHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, DateTime? at = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetAdlHistoryAsync(settle, contract, from.ConvertToSeconds(), to.ConvertToSeconds(), at?.ConvertToSeconds(), limit, offset, ct);

    // List Auto-Deleveraging History
    internal Task<RestCallResult<List<GateFuturesAdlRecord>>> GetAdlHistoryAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, long? at = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptional("at", at);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/auto_deleverages".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesAdlRecord>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Countdown cancel orders
    internal async Task<RestCallResult<DateTime>> CancelAllAsync(GateFuturesSettlement settle, int timeout, string contract = null, CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(contract)) PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new ParameterCollection {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/countdown_cancel_all".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesCountdown>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Time ?? default);
    }


    internal Task<RestCallResult<Dictionary<string, GateFuturesFee>>> GetTradingFeesAsync(GateFuturesSettlement settle, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);

        var endpoint = "{settle}/fee".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<Dictionary<string, GateFuturesFee>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Cancel batch orders by specified ID list
    internal Task<RestCallResult<List<GateFuturesOrderCancel>>> CancelOrdersAsync(GateFuturesSettlement settle, IEnumerable<long> orderIds, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orderIds.Select(x=>x.ToString()).ToList());

        var endpoint = "{settle}/batch_cancel_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrderCancel>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Delete, ct, true, bodyParameters: parameters);
    }

    // Batch modify orders by specified IDs
    internal Task<RestCallResult<List<GateFuturesOrderAmend>>> AmendOrdersAsync(GateFuturesSettlement settle, IEnumerable<GateFuturesOrderAmendRequest> requests, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        var endpoint = "{settle}/batch_amend_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrderAmend>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Query risk limit table by table_id
    internal Task<RestCallResult<List<GateFuturesRiskLimitTable>>> GetRiskLimitTableAsync(GateFuturesSettlement settle, string tableId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("table_id", tableId);

        var endpoint = "{settle}/risk_limit_table".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesRiskLimitTable>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Create trail order
    internal async Task<RestCallResult<long>> PlaceTrailOrderAsync(GateFuturesSettlement settle, GateFuturesTrailOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", request.Contract },
        };
        parameters.AddString("amount", request.Amount);
        parameters.AddOptionalString("activation_price", request.ActivationPrice);
        parameters.AddOptional("is_gte", request.IsGreaterThanOrEqual);
        parameters.AddOptional("price_type", request.PriceType.HasValue ? (int?)request.PriceType.Value : null);
        parameters.AddOptional("price_offset", request.PriceOffset);
        parameters.AddOptional("reduce_only", request.ReduceOnly);
        parameters.AddOptional("position_related", request.PositionRelated);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptionalEnum("pos_margin_mode", request.PositionMarginMode);
        parameters.AddOptional("position_mode", request.PositionMode);

        var endpoint = "{settle}/autoorder/v1/trail/create".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderCreateResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Data?.OrderId ?? default);
    }

    // Terminate trail order
    internal Task<RestCallResult<GateFuturesTrailOrder>> CancelTrailOrderAsync(GateFuturesSettlement settle, GateFuturesTrailOrderCancelRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("id", request.OrderId);
        parameters.AddOptional("text", request.ClientOrderId);

        var endpoint = "{settle}/autoorder/v1/trail/stop".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesTrailOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Batch terminate trail orders
    internal async Task<RestCallResult<List<GateFuturesTrailOrder>>> CancelTrailOrdersAsync(GateFuturesSettlement settle, GateFuturesTrailOrdersCancelRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptional("related_position", request.RelatedPosition);

        var endpoint = "{settle}/autoorder/v1/trail/stop_all".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Orders ?? []);
    }

    // Get trail order list
    internal async Task<RestCallResult<List<GateFuturesTrailOrder>>> GetTrailOrdersAsync(GateFuturesSettlement settle, GateFuturesTrailOrderQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptional("is_finished", request.IsFinished);
        parameters.AddOptional("start_at", request.StartAt?.ConvertToSeconds());
        parameters.AddOptional("end_at", request.EndAt?.ConvertToSeconds());
        parameters.AddOptional("page_num", request.PageNumber);
        parameters.AddOptional("page_size", request.PageSize);
        parameters.AddOptional("sort_by", request.SortBy);
        parameters.AddOptional("hide_cancel", request.HideCancel);
        parameters.AddOptional("related_position", request.RelatedPosition);
        parameters.AddOptional("sort_by_trigger", request.SortByTrigger);
        parameters.AddOptional("reduce_only", request.ReduceOnly);
        parameters.AddOptional("side", request.Side);

        var endpoint = "{settle}/autoorder/v1/trail/list".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        return result.As(result.Data?.Orders ?? []);
    }

    // Get trail order details
    internal async Task<RestCallResult<GateFuturesTrailOrder>> GetTrailOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "id", orderId },
        };

        var endpoint = "{settle}/autoorder/v1/trail/detail".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderDetailResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        return result.As(result.Data?.Data?.Order);
    }

    // Update trail order
    internal Task<RestCallResult<GateFuturesTrailOrder>> UpdateTrailOrderAsync(GateFuturesSettlement settle, GateFuturesTrailOrderUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "id", request.OrderId },
        };
        parameters.AddOptionalString("amount", request.Amount);
        parameters.AddOptionalString("activation_price", request.ActivationPrice);
        parameters.AddOptional("is_gte_str", request.IsGreaterThanOrEqual?.ToString().ToLowerInvariant());
        parameters.AddOptional("price_type", request.PriceType.HasValue ? (int?)request.PriceType.Value : null);
        parameters.AddOptional("price_offset", request.PriceOffset);

        var endpoint = "{settle}/autoorder/v1/trail/update".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesTrailOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Get trail order user modification records
    internal async Task<RestCallResult<List<GateFuturesTrailOrderChange>>> GetTrailOrderChangeLogAsync(GateFuturesSettlement settle, GateFuturesTrailOrderChangeLogQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "id", request.OrderId },
        };
        parameters.AddOptional("page_num", request.PageNumber);
        parameters.AddOptional("page_size", request.PageSize);

        var endpoint = "{settle}/autoorder/v1/trail/change_log".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderChangeLogResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        return result.As(result.Data?.ChangeLog ?? []);
    }

    // Create a chase order
    internal async Task<RestCallResult<string>> PlaceChaseOrderAsync(GateFuturesSettlement settle, GateFuturesChaseOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", request.Contract },
            { "amount", request.Amount },
            { "price_limit", request.PriceLimit },
        };
        parameters.AddOptional("offset_limit", request.OffsetLimit);
        parameters.AddOptional("reduce_only", request.ReduceOnly);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptional("is_dual_mode", request.IsDualMode);
        parameters.AddOptional("price_type", request.PriceType.HasValue ? (int?)request.PriceType.Value : null);
        parameters.AddOptional("price_gap_type", request.PriceGapType.HasValue ? (int?)request.PriceGapType.Value : null);
        parameters.AddOptional("price_gap_value", request.PriceGapValue);
        parameters.AddOptionalEnum("pos_margin_mode", request.PositionMarginMode);
        parameters.AddOptional("position_mode", request.PositionMode);

        var endpoint = "{settle}/autoorder/v1/chase/create".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderCreateResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId);
    }

    // Stop a chase order
    internal async Task<RestCallResult<GateFuturesChaseOrder>> CancelChaseOrderAsync(GateFuturesSettlement settle, GateFuturesChaseOrderCancelRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("id", request.OrderId);
        parameters.AddOptional("text", request.ClientOrderId);

        var endpoint = "{settle}/autoorder/v1/chase/stop".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderDetailResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Order);
    }

    // Stop chase orders in batch
    internal async Task<RestCallResult<List<GateFuturesChaseOrder>>> CancelChaseOrdersAsync(GateFuturesSettlement settle, GateFuturesChaseOrdersCancelRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptionalEnum("pos_margin_mode", request.PositionMarginMode);

        var endpoint = "{settle}/autoorder/v1/chase/stop_all".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Orders ?? []);
    }

    // List chase orders
    internal async Task<RestCallResult<List<GateFuturesChaseOrder>>> GetChaseOrdersAsync(GateFuturesSettlement settle, GateFuturesChaseOrderQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "sort_by", (int)request.SortBy },
        };
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptional("is_finished", request.IsFinished?.ToString().ToLowerInvariant());
        parameters.AddOptional("start_at", request.StartAt?.ConvertToSeconds());
        parameters.AddOptional("end_at", request.EndAt?.ConvertToSeconds());
        parameters.AddOptional("page_num", request.PageNumber);
        parameters.AddOptional("page_size", request.PageSize);
        parameters.AddOptional("hide_cancel", request.HideCancelled?.ToString().ToLowerInvariant());
        parameters.AddOptional("reduce_only", request.ReduceOnly.HasValue ? (int?)request.ReduceOnly.Value : null);
        parameters.AddOptional("side", request.Side.HasValue ? (int?)request.Side.Value : null);

        var endpoint = "{settle}/autoorder/v1/chase/list".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        return result.As(result.Data?.Orders ?? []);
    }

    // Get chase order detail
    internal async Task<RestCallResult<GateFuturesChaseOrder>> GetChaseOrderAsync(GateFuturesSettlement settle, string orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "id", orderId },
        };

        var endpoint = "{settle}/autoorder/v1/chase/detail".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderDetailResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        return result.As(result.Data?.Order);
    }

    // Create a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrderId>> PlacePriceTriggeredOrderAsync(
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
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrderId>> PlacePriceTriggeredOrderAsync(GateFuturesSettlement settle, GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(request.Order.Contract);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrderId>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Modify a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrderId>> AmendPriceTriggeredOrderAsync(GateFuturesSettlement settle, GateFuturesPriceTriggeredOrderUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = "{settle}/price_orders/amend".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrderId>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Put, ct, true, bodyParameters: parameters);
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
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel all open orders
    internal Task<RestCallResult<List<GateFuturesPriceTriggeredOrder>>> CancelPriceTriggeredOrdersAsync(GateFuturesSettlement settle, string contract = null, CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(contract)) PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Get a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Delete, ct, true);
    }

}
