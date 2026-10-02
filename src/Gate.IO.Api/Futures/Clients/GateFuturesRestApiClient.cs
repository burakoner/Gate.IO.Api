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
    /// USD1 Settled Perpetual Futures REST Client. DeFi Futures and WebSocket support are not implied.
    /// </summary>
    public GateFuturesRestApiSettleClient USD1 { get; }

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
        USD1 = new GateFuturesRestApiSettleClient(this, GateFuturesSettlement.USD1);
        Clients = new Dictionary<GateFuturesSettlement, GateFuturesRestApiSettleClient>
        {
            { GateFuturesSettlement.BTC, BTC },
            { GateFuturesSettlement.USDT, USDT },
            { GateFuturesSettlement.USD1, USD1 },
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

    // Include delisted contracts; contracts and contracts_all intentionally remain separate operations.
    internal Task<RestCallResult<List<GateFuturesContract>>> GetAllContractsAsync(GateFuturesSettlement settle, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection { { "limit", limit }, { "offset", offset } };
        var endpoint = "{settle}/contracts_all".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesContract>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get a single contract
    internal Task<RestCallResult<GateFuturesContract>> GetContractAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        var endpoint = "{settle}/contracts/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesContract>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct);
    }

    // List current market-level ADL risk states. The public endpoint has no query parameters.
    internal Task<RestCallResult<GateFuturesAdlRiskStates>> GetAdlRiskStatesAsync(GateFuturesSettlement settle, CancellationToken ct = default)
    {
        var endpoint = $"{MapConverter.GetString(settle)}/adl_risk_states";
        return _.SendRequestInternal<GateFuturesAdlRiskStates>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false);
    }

    // Futures order book
    internal Task<RestCallResult<GateFuturesOrderBook>> GetOrderBookAsync(GateFuturesSettlement settle, string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
    => GetTradesAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), limit, offset, lastId, ct);

    // Futures trading history
    internal Task<RestCallResult<List<GateFuturesTrade>>> GetTradesAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
    => GetCandlesticksAsync(settle, prefix, contract, interval, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), limit, ct);

    // Get futures candlesticks
    internal Task<RestCallResult<List<GateFuturesCandlestick>>> GetCandlesticksAsync(GateFuturesSettlement settle, string prefix, string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default, string timezone = null)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        GateFuturesOrderValidation.Defined<GateFuturesCandlestickInterval>(interval, nameof(interval));
        GateFuturesStrategyValidation.Range(from, to);
        if (!from.HasValue && !to.HasValue && (limit < 1 || limit > 2000)) throw new ArgumentOutOfRangeException(nameof(limit));
        if (timezone != null && timezone != "all" && timezone != "utc0" && timezone != "utc8")
            throw new ArgumentException("Timezone must be all, utc0 or utc8", nameof(timezone));
        var parameters = new ParameterCollection
        {
            { "contract", prefix + contract },
        };
        parameters.AddEnum("interval", interval);
        parameters.AddOptional("timezone", timezone);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);

        var endpoint = "{settle}/candlesticks".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesCandlestick>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Premium Index K-Line
    internal Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(GateFuturesSettlement settle, string contract, GateFuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetPremiumIndexCandlesticksAsync(settle, contract, interval, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), limit, ct);

    // Premium Index K-Line
    internal Task<RestCallResult<List<GateFuturesCandlestickPremium>>> GetPremiumIndexCandlesticksAsync(GateFuturesSettlement settle, string contract, GateFuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        GateFuturesOrderValidation.Defined<GateFuturesCandlestickInterval>(interval, nameof(interval));
        if (interval == GateFuturesCandlestickInterval.TenSeconds || interval == GateFuturesCandlestickInterval.OneMonth || interval == GateFuturesCandlestickInterval.NaturalWeek)
            throw new ArgumentOutOfRangeException(nameof(interval), "Premium index intervals range from 1m through 7d");
        GateFuturesStrategyValidation.Range(from, to);
        if (!from.HasValue && !to.HasValue && (limit < 1 || limit > 1000)) throw new ArgumentOutOfRangeException(nameof(limit));
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
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/tickers".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesTicker>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Funding rate history
    internal Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
        => GetFundingRateHistoryAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), limit, ct);

    // Funding rate history
    internal Task<RestCallResult<List<GateFuturesFundingRate>>> GetFundingRateHistoryAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Contracts == null) throw new ArgumentException("contracts is required", nameof(request));
        var contracts = request.Contracts.ToList();
        foreach (var contract in contracts) GateFuturesPriceOrderValidation.Contract(contract, required: true);
        var parameters = new ParameterCollection();
        parameters.SetBody(new GateFuturesBatchFundingRateRequest { Contracts = contracts });

        var endpoint = "{settle}/funding_rates".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, false, bodyParameters: parameters);
        if (!result.Success) return result.As<List<GateFuturesBatchFundingRate>>([]);

        if (result.Data is not JArray array)
            return result.AsError<List<GateFuturesBatchFundingRate>>(new DeserializeError("Funding rates response must be an array", result.Data));

        try
        {
            // Schema shows a flat array; the example nests it. Both documented shapes are supported.
            if (array.All(x => x.Type == JTokenType.Object)) return result.As(array.ToObject<List<GateFuturesBatchFundingRate>>());
            if (array.All(x => x is JArray nested && nested.All(y => y.Type == JTokenType.Object)))
                return result.As(array.ToObject<List<List<GateFuturesBatchFundingRate>>>().SelectMany(x => x).ToList());
            return result.AsError<List<GateFuturesBatchFundingRate>>(new DeserializeError("Funding rates response contains an invalid array item", result.Data));
        }
        catch (JsonException exception) { return result.AsError<List<GateFuturesBatchFundingRate>>(new DeserializeError(exception.Message, result.Data)); }
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
    => GetStatsAsync(settle, contract, interval, GateFuturesRequestTime.Seconds(from), limit, ct);

    // Futures stats
    internal Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(GateFuturesSettlement settle, string contract, GateFuturesStatsInterval? interval = null, long? from = null, int? limit = 100, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddOptionalEnum("interval", interval);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("limit", limit);

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
    internal Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidationsAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int? limit = null, CancellationToken ct = default)
    => GetLiquidationsAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), limit, ct);

    // Retrieve liquidation history
    internal Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidationsAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int? limit = null, CancellationToken ct = default)
    {
        if (from.HasValue && to.HasValue)
        {
            if (to.Value < from.Value)
                throw new ArgumentException("End time cannot be earlier than start time", nameof(to));
            if (to.Value - from.Value > 3600)
                throw new ArgumentException("The liquidation history time range cannot exceed 3600 seconds", nameof(to));
        }

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = "{settle}/liq_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesLiquidation>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // List risk limit tiers
    internal Task<RestCallResult<List<GateFuturesRiskLimitTier>>> GetRiskLimitTiersAsync(GateFuturesSettlement settle, string contract = null, int limit = 100, long? offset = null, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
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
    => GetBalanceHistoryAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), type, limit, offset, ct);

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesBalanceChangeType? type = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
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
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(GateFuturesSettlement settle, bool? holding = null, int? limit = null, int? offset = null, CancellationToken ct = default)
    {
        if (limit.HasValue && (limit.Value < 1 || limit.Value > 100))
            throw new ArgumentOutOfRangeException(nameof(limit), "Limit must be between 1 and 100");
        if (offset.HasValue && offset.Value < 0)
            throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be greater than or equal to 0");

        var parameters = new ParameterCollection();
        parameters.AddOptional("holding", holding);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/positions".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Get user's historical position information list by time
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetHistoricalPositionsAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetHistoricalPositionsAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), limit, offset, ct);

    // Get user's historical position information list by time
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetHistoricalPositionsAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        var endpoint = "{settle}/positions/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Get leverage information for specified mode
    internal Task<RestCallResult<GateFuturesLeverage>> GetLeverageAsync(GateFuturesSettlement settle, string contract, GateFuturesPositionMarginMode positionMarginMode, GateFuturesDualModeSide dualSide, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        GateFuturesOrderValidation.Defined<GateFuturesPositionMarginMode>(positionMarginMode, nameof(positionMarginMode));
        GateFuturesOrderValidation.Defined<GateFuturesDualModeSide>(dualSide, nameof(dualSide));
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        GateFuturesOrderValidation.Defined<GateFuturesMarginMode>(mode, nameof(mode));
        var parameters = new ParameterCollection();
        parameters.AddParameter("contract", contract);
        parameters.AddEnum("mode", mode);

        var endpoint = "{settle}/positions/cross_mode"
            .Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Set leverage directly for a specified margin mode (distinct from the legacy leverage=0 convention).
    internal Task<RestCallResult<GateFuturesPosition>> SetPositionLeverageAsync(GateFuturesSettlement settle, string contract, decimal leverage, GateFuturesPositionMarginMode marginMode, GateFuturesDualModeSide? dualSide = null, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        GateFuturesOrderValidation.Defined<GateFuturesPositionMarginMode>(marginMode, nameof(marginMode));
        GateFuturesOrderValidation.Defined(dualSide, nameof(dualSide));
        var parameters = new ParameterCollection();
        parameters.AddString("leverage", leverage);
        parameters.AddEnum("margin_mode", marginMode);
        parameters.AddOptionalEnum("dual_side", dualSide);
        var endpoint = "{settle}/positions/{contract}/set_leverage"
            .Replace("{settle}", MapConverter.GetString(settle)).Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    internal Task<RestCallResult<GateFuturesBalance>> SetPositionModeAsync(GateFuturesSettlement settle, GateFuturesAccountPositionMode positionMode, CancellationToken ct = default)
    {
        GateFuturesOrderValidation.Defined<GateFuturesAccountPositionMode>(positionMode, nameof(positionMode));
        var parameters = new ParameterCollection();
        parameters.AddEnum("position_mode", positionMode);
        var endpoint = "{settle}/set_position_mode".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesBalance>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    internal Task<RestCallResult<GateFuturesPosition>> SwithMarginModeUnderHedgeAsync(GateFuturesSettlement settle, string contract, GateFuturesMarginMode mode, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        GateFuturesOrderValidation.Defined<GateFuturesMarginMode>(mode, nameof(mode));
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        var endpoint = "{settle}/dual_comp/positions/{contract}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Update position margin in dual mode
    internal Task<RestCallResult<GateFuturesPosition>> SetDualModeMarginAsync(GateFuturesSettlement settle, string contract, GateFuturesDualModeSide side, decimal change, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
        GateFuturesOrderValidation.Defined<GateFuturesDualModeSide>(side, nameof(side));
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
        GateFuturesPriceOrderValidation.Contract(contract, required: true);
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
        GateFuturesOrderValidation.Create(request);

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
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
        GateFuturesOrderValidation.Defined<GateFuturesOrderStatus>(status, nameof(status));
        var parameters = new ParameterCollection { { "offset", offset }, { "limit", limit } };
        parameters.AddOptional("contract", contract);
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
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesPriceOrderValidation.Contract(request.Contract);
        GateFuturesOrderValidation.Defined(request.Side, nameof(request.Side));
        GateFuturesOrderValidation.Defined(request.ActionMode, nameof(request.ActionMode));
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
        => GetOrdersAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), limit, offset, ct);

    // List Futures Orders By Time Range
    internal Task<RestCallResult<List<GateFuturesOrder>>> GetOrdersAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, int? limit = null, int? offset = null, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
        GateFuturesStrategyValidation.Range(from, to);
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
        var orders = GateFuturesOrderValidation.Batch(requests, 10, nameof(requests));
        foreach (var request in orders) GateFuturesOrderValidation.Create(request);

        var parameters = new ParameterCollection();
        parameters.SetBody(orders);

        var endpoint = "{settle}/batch_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBatchOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Get a single order
    internal Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", GateFuturesOrderValidation.Identity(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a single order
    internal Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, GateFuturesActionMode? actionMode = null, CancellationToken ct = default)
    {
        GateFuturesOrderValidation.Defined(actionMode, nameof(actionMode));
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("action_mode", actionMode);

        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", GateFuturesOrderValidation.Identity(orderId, clientOrderId));
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
        GateFuturesOrderValidation.Defined(actionMode, nameof(actionMode));
        var parameters = new ParameterCollection();
        parameters.AddOptionalString("size", size);
        parameters.AddOptionalString("price", price);
        parameters.AddOptional("amend_text", amendText);
        parameters.AddOptional("text", text);
        parameters.AddOptionalEnum("action_mode", actionMode);

        var endpoint = "{settle}/orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", GateFuturesOrderValidation.Identity(orderId, clientOrderId));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }

    // List personal trading history
    internal Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
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
        => GetUserTradesAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), role, limit, offset, ct);

    // List personal trading history by time range
    internal Task<RestCallResult<List<GateFuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesTradeRole? role = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
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
        => GetPositionClosesAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), side, pnl, limit, offset, ct);

    // List position close history
    internal Task<RestCallResult<List<GateFuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesPositionSide? side = null, decimal? pnl = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
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
        => GetUserLiquidationsAsync(settle, contract, null, null, at, limit, null, ct);

    internal Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidationsAsync(GateFuturesSettlement settle, GateFuturesUserLiquidationQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        return GetUserLiquidationsAsync(settle, request.Contract, GateFuturesRequestTime.Seconds(request.From), GateFuturesRequestTime.Seconds(request.To), GateFuturesRequestTime.Seconds(request.At), request.Limit, request.Offset, ct);
    }

    private Task<RestCallResult<List<GateFuturesUserLiquidation>>> GetUserLiquidationsAsync(GateFuturesSettlement settle, string contract, long? from, long? to, long? at, int? limit, int? offset, CancellationToken ct)
    {
        GateFuturesPriceOrderValidation.Contract(contract);
        GateFuturesStrategyValidation.Range(from, to);
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptional("at", at);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = "{settle}/liquidates".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesUserLiquidation>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List Auto-Deleveraging History
    internal Task<RestCallResult<List<GateFuturesAdlRecord>>> GetAdlHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, DateTime? at = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetAdlHistoryAsync(settle, contract, GateFuturesRequestTime.Seconds(from), GateFuturesRequestTime.Seconds(to), GateFuturesRequestTime.Seconds(at), limit, offset, ct);

    // List Auto-Deleveraging History
    internal Task<RestCallResult<List<GateFuturesAdlRecord>>> GetAdlHistoryAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, long? at = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
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
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
        if (timeout != 0 && timeout < 5) throw new ArgumentOutOfRangeException(nameof(timeout), "Use 0 to disable the countdown or at least 5 seconds");

        var parameters = new ParameterCollection {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = "{settle}/countdown_cancel_all".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesCountdown>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        if (result.Success && result.Data == null)
            return result.AsError<DateTime>(new DeserializeError("Countdown response omitted its timestamp", result.Data));
        return result.As(result.Data?.Time ?? default);
    }


    internal Task<RestCallResult<Dictionary<string, GateFuturesFee>>> GetTradingFeesAsync(GateFuturesSettlement settle, string contract = null, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Contract(contract, required: false);
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);

        var endpoint = "{settle}/fee".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<Dictionary<string, GateFuturesFee>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // Cancel batch orders by specified ID list
    internal Task<RestCallResult<List<GateFuturesOrderCancel>>> CancelOrdersAsync(GateFuturesSettlement settle, IEnumerable<long> orderIds, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        var ids = GateFuturesOrderValidation.Batch(orderIds, 20, nameof(orderIds));
        foreach (var id in ids) GateFuturesPriceOrderValidation.OrderId(id);
        parameters.SetBody(ids.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToList());

        var endpoint = "{settle}/batch_cancel_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrderCancel>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Batch modify orders by specified IDs
    internal Task<RestCallResult<List<GateFuturesOrderAmend>>> AmendOrdersAsync(GateFuturesSettlement settle, IEnumerable<GateFuturesOrderAmendRequest> requests, CancellationToken ct = default)
    {
        var orders = GateFuturesOrderValidation.Batch(requests, 10, nameof(requests));
        foreach (var request in orders)
        {
            if (request == null) throw new ArgumentException("Batch entries cannot be null", nameof(requests));
            if (!request.OrderId.HasValue && string.IsNullOrEmpty(request.ClientOrderId))
                throw new ArgumentException("Specify order_id or text for every amendment", nameof(requests));
            if (request.OrderId.HasValue) GateFuturesPriceOrderValidation.OrderId(request.OrderId.Value);
            if (!string.IsNullOrEmpty(request.ClientOrderId)) GateFuturesOrderValidation.ClientTag(request.ClientOrderId, allowEmpty: false);
            GateFuturesOrderValidation.Defined(request.ActionMode, nameof(request.ActionMode));
        }
        var parameters = new ParameterCollection();
        parameters.SetBody(orders);

        var endpoint = "{settle}/batch_amend_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrderAmend>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Query risk limit table by table_id
    internal Task<RestCallResult<List<GateFuturesRiskLimitTable>>> GetRiskLimitTableAsync(GateFuturesSettlement settle, string tableId, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(tableId)) throw new ArgumentException("Specify a risk limit table ID", nameof(tableId));
        var parameters = new ParameterCollection();
        parameters.Add("table_id", tableId);

        var endpoint = "{settle}/risk_limit_table".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesRiskLimitTable>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    internal Task<RestCallResult<GateFuturesOrder>> PlaceBboOrderAsync(GateFuturesSettlement settle, GateFuturesBboOrderRequest request, CancellationToken ct = default)
    {
        GateFuturesOrderValidation.Bbo(request);
        var parameters = new ParameterCollection();
        parameters.SetBody(request);
        var endpoint = "{settle}/bbo_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Create trail order
    internal async Task<RestCallResult<long>> PlaceTrailOrderAsync(GateFuturesSettlement settle, GateFuturesTrailOrderRequest request, CancellationToken ct = default)
    {
        GateFuturesStrategyValidation.Trail(request);
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
        if (!result.Success) return result.As<long>(default);
        var error = TrailEnvelopeError(result.Data?.Code, result.Data?.Message);
        if (error != null) return result.AsError<long>(error);
        if (result.Data?.Data?.OrderId is not > 0)
            return result.AsError<long>(new DeserializeError("Trail creation response omitted a positive order ID", result.Data));
        return result.As(result.Data.Data.OrderId);
    }

    // Terminate trail order
    internal Task<RestCallResult<GateFuturesTrailOrder>> CancelTrailOrderAsync(GateFuturesSettlement settle, GateFuturesTrailOrderCancelRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.OrderId.HasValue) GateFuturesPriceOrderValidation.OrderId(request.OrderId.Value);
        else if (string.IsNullOrWhiteSpace(request.ClientOrderId)) throw new ArgumentException("Specify an id or text", nameof(request));
        var parameters = new ParameterCollection();
        parameters.AddOptional("id", request.OrderId);
        parameters.AddOptional("text", request.ClientOrderId);

        var endpoint = "{settle}/autoorder/v1/trail/stop".Replace("{settle}", MapConverter.GetString(settle));
        return SendTrailOrderResponseAsync(endpoint, parameters, ct);
    }

    // Batch terminate trail orders
    internal async Task<RestCallResult<List<GateFuturesTrailOrder>>> CancelTrailOrdersAsync(GateFuturesSettlement settle, GateFuturesTrailOrdersCancelRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesPriceOrderValidation.Contract(request.Contract);
        GateFuturesStrategyValidation.Filter(request.RelatedPosition, nameof(request.RelatedPosition));
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptional("related_position", request.RelatedPosition);

        var endpoint = "{settle}/autoorder/v1/trail/stop_all".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        if (result.Success && result.Data?.Orders == null)
            return result.AsError<List<GateFuturesTrailOrder>>(new DeserializeError("Trail response omitted its orders list", result.Data));
        return result.As(result.Data?.Orders ?? []);
    }

    // Get trail order list
    internal async Task<RestCallResult<List<GateFuturesTrailOrder>>> GetTrailOrdersAsync(GateFuturesSettlement settle, GateFuturesTrailOrderQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesPriceOrderValidation.Contract(request.Contract);
        GateFuturesStrategyValidation.Page(request.PageNumber, request.PageSize);
        GateFuturesStrategyValidation.Range(GateFuturesRequestTime.Seconds(request.StartAt), GateFuturesRequestTime.Seconds(request.EndAt));
        GateFuturesStrategyValidation.Filter(request.SortBy, nameof(request.SortBy));
        GateFuturesStrategyValidation.Filter(request.RelatedPosition, nameof(request.RelatedPosition));
        GateFuturesStrategyValidation.Filter(request.ReduceOnly, nameof(request.ReduceOnly));
        GateFuturesStrategyValidation.Filter(request.Side, nameof(request.Side));
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptional("is_finished", request.IsFinished?.ToString().ToLowerInvariant());
        parameters.AddOptional("start_at", GateFuturesRequestTime.Seconds(request.StartAt));
        parameters.AddOptional("end_at", GateFuturesRequestTime.Seconds(request.EndAt));
        parameters.AddOptional("page_num", request.PageNumber);
        parameters.AddOptional("page_size", request.PageSize);
        parameters.AddOptional("sort_by", request.SortBy);
        parameters.AddOptional("hide_cancel", request.HideCancel?.ToString().ToLowerInvariant());
        parameters.AddOptional("related_position", request.RelatedPosition);
        parameters.AddOptional("sort_by_trigger", request.SortByTrigger?.ToString().ToLowerInvariant());
        parameters.AddOptional("reduce_only", request.ReduceOnly);
        parameters.AddOptional("side", request.Side);

        var endpoint = "{settle}/autoorder/v1/trail/list".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        if (result.Success && result.Data?.Orders == null)
            return result.AsError<List<GateFuturesTrailOrder>>(new DeserializeError("Trail response omitted its orders list", result.Data));
        return result.As(result.Data?.Orders ?? []);
    }

    // Get trail order details
    internal async Task<RestCallResult<GateFuturesTrailOrder>> GetTrailOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.OrderId(orderId);
        var parameters = new ParameterCollection
        {
            { "id", orderId },
        };

        var endpoint = "{settle}/autoorder/v1/trail/detail".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderDetailResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        if (!result.Success) return result.As<GateFuturesTrailOrder>(default);
        var error = TrailEnvelopeError(result.Data?.Code, result.Data?.Message);
        if (error != null) return result.AsError<GateFuturesTrailOrder>(error);
        if (result.Data?.Data?.Order?.OrderId is not > 0)
            return result.AsError<GateFuturesTrailOrder>(new DeserializeError("Trail detail response omitted an order", result.Data));
        return result.As(result.Data.Data.Order);
    }

    // Update trail order
    internal Task<RestCallResult<GateFuturesTrailOrder>> UpdateTrailOrderAsync(GateFuturesSettlement settle, GateFuturesTrailOrderUpdateRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesPriceOrderValidation.OrderId(request.OrderId);
        GateFuturesOrderValidation.Defined(request.PriceType, nameof(request.PriceType));
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
        return SendTrailOrderResponseAsync(endpoint, parameters, ct);
    }

    private static Error TrailEnvelopeError(JToken code, string message)
    {
        if (code?.Type != JTokenType.Integer || !int.TryParse(code.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            return new DeserializeError("Trail envelope requires an integer status code", code);
        return value == 0 ? null : new ServerError(value, message ?? "Trail business error");
    }

    private async Task<RestCallResult<GateFuturesTrailOrder>> SendTrailOrderResponseAsync(string endpoint, ParameterCollection parameters, CancellationToken ct)
    {
        // The reference's stop/update examples are flat; their schema wraps the same object in 'order'.
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        if (!result.Success) return result.As<GateFuturesTrailOrder>(default);
        var order = result.Data is JObject root ? (root.ContainsKey("order") ? root["order"] as JObject : root) : null;
        if (order?["id"] == null) return result.AsError<GateFuturesTrailOrder>(new DeserializeError("Trail response omitted an order ID", result.Data));
        try
        {
            var data = order.ToObject<GateFuturesTrailOrder>();
            if (data?.OrderId is not > 0) return result.AsError<GateFuturesTrailOrder>(new DeserializeError("Trail response has no positive order ID", result.Data));
            return result.As(data);
        }
        catch (JsonException exception) { return result.AsError<GateFuturesTrailOrder>(new DeserializeError(exception.Message, result.Data)); }
    }

    // Get trail order user modification records
    internal async Task<RestCallResult<List<GateFuturesTrailOrderChange>>> GetTrailOrderChangeLogAsync(GateFuturesSettlement settle, GateFuturesTrailOrderChangeLogQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesPriceOrderValidation.OrderId(request.OrderId);
        GateFuturesStrategyValidation.Page(request.PageNumber, request.PageSize);
        var parameters = new ParameterCollection
        {
            { "id", request.OrderId },
        };
        parameters.AddOptional("page_num", request.PageNumber);
        parameters.AddOptional("page_size", request.PageSize);

        var endpoint = "{settle}/autoorder/v1/trail/change_log".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesTrailOrderChangeLogResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        if (result.Success && result.Data?.ChangeLog == null)
            return result.AsError<List<GateFuturesTrailOrderChange>>(new DeserializeError("Trail response omitted its change-log list", result.Data));
        return result.As(result.Data?.ChangeLog ?? []);
    }

    // Create a chase order
    internal async Task<RestCallResult<string>> PlaceChaseOrderAsync(GateFuturesSettlement settle, GateFuturesChaseOrderRequest request, CancellationToken ct = default)
    {
        GateFuturesStrategyValidation.Chase(request);
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
        parameters.AddOptionalEnum("settle", request.Settlement);

        var endpoint = "{settle}/autoorder/v1/chase/create".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderCreateResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        if (result.Success && string.IsNullOrWhiteSpace(result.Data?.OrderId))
            return result.AsError<string>(new DeserializeError("Chase creation response omitted an order ID", result.Data));
        return result.As(result.Data?.OrderId);
    }

    // Stop a chase order
    internal async Task<RestCallResult<GateFuturesChaseOrder>> CancelChaseOrderAsync(GateFuturesSettlement settle, GateFuturesChaseOrderCancelRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.OrderId != null && request.OrderId != "0") GateFuturesStrategyValidation.ChaseId(request.OrderId);
        else if (string.IsNullOrWhiteSpace(request.ClientOrderId)) throw new ArgumentException("Specify a nonzero id or text", nameof(request));
        GateFuturesStrategyValidation.Settlement(request.Settlement);
        var parameters = new ParameterCollection();
        parameters.AddOptional("id", request.OrderId);
        parameters.AddOptional("text", request.ClientOrderId);
        parameters.AddOptionalEnum("settle", request.Settlement);

        var endpoint = "{settle}/autoorder/v1/chase/stop".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderDetailResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        if (result.Success && result.Data?.Order == null)
            return result.AsError<GateFuturesChaseOrder>(new DeserializeError("Chase response omitted its order", result.Data));
        return result.As(result.Data?.Order);
    }

    // Stop chase orders in batch
    internal async Task<RestCallResult<List<GateFuturesChaseOrder>>> CancelChaseOrdersAsync(GateFuturesSettlement settle, GateFuturesChaseOrdersCancelRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesStrategyValidation.Contract(request.Contract);
        GateFuturesStrategyValidation.Settlement(request.Settlement);
        GateFuturesOrderValidation.Defined(request.PositionMarginMode, nameof(request.PositionMarginMode));
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptionalEnum("pos_margin_mode", request.PositionMarginMode);
        parameters.AddOptionalEnum("settle", request.Settlement);

        var endpoint = "{settle}/autoorder/v1/chase/stop_all".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        if (result.Success && result.Data?.Orders == null)
            return result.AsError<List<GateFuturesChaseOrder>>(new DeserializeError("Chase response omitted its orders list", result.Data));
        return result.As(result.Data?.Orders ?? []);
    }

    // List chase orders
    internal async Task<RestCallResult<List<GateFuturesChaseOrder>>> GetChaseOrdersAsync(GateFuturesSettlement settle, GateFuturesChaseOrderQueryRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesStrategyValidation.Contract(request.Contract);
        GateFuturesOrderValidation.Defined<GateFuturesChaseOrderSort>(request.SortBy, nameof(request.SortBy));
        GateFuturesOrderValidation.Defined(request.ReduceOnly, nameof(request.ReduceOnly));
        GateFuturesOrderValidation.Defined(request.Side, nameof(request.Side));
        if (request.PageNumber == 0) throw new ArgumentOutOfRangeException(nameof(request.PageNumber));
        if (request.PageSize.HasValue && (request.PageSize < 1 || request.PageSize > 100)) throw new ArgumentOutOfRangeException(nameof(request.PageSize));
        if (request.IsFinished == true && (!request.StartAt.HasValue || !request.EndAt.HasValue))
            throw new ArgumentException("Finished chase orders require both start_at and end_at", nameof(request));
        GateFuturesStrategyValidation.Range(GateFuturesRequestTime.Seconds(request.StartAt), GateFuturesRequestTime.Seconds(request.EndAt));
        var parameters = new ParameterCollection
        {
            { "sort_by", (int)request.SortBy },
        };
        parameters.AddOptional("contract", request.Contract);
        parameters.AddOptional("is_finished", request.IsFinished?.ToString().ToLowerInvariant());
        parameters.AddOptional("start_at", GateFuturesRequestTime.Seconds(request.StartAt));
        parameters.AddOptional("end_at", GateFuturesRequestTime.Seconds(request.EndAt));
        parameters.AddOptional("page_num", request.PageNumber);
        parameters.AddOptional("page_size", request.PageSize);
        parameters.AddOptional("hide_cancel", request.HideCancelled?.ToString().ToLowerInvariant());
        parameters.AddOptional("reduce_only", request.ReduceOnly.HasValue ? (int?)request.ReduceOnly.Value : null);
        parameters.AddOptional("side", request.Side.HasValue ? (int?)request.Side.Value : null);

        var endpoint = "{settle}/autoorder/v1/chase/list".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderListResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        if (result.Success && result.Data?.Orders == null)
            return result.AsError<List<GateFuturesChaseOrder>>(new DeserializeError("Chase response omitted its orders list", result.Data));
        return result.As(result.Data?.Orders ?? []);
    }

    // Get chase order detail
    internal async Task<RestCallResult<GateFuturesChaseOrder>> GetChaseOrderAsync(GateFuturesSettlement settle, string orderId, CancellationToken ct = default)
    {
        GateFuturesStrategyValidation.ChaseId(orderId);
        var parameters = new ParameterCollection
        {
            { "id", orderId },
        };

        var endpoint = "{settle}/autoorder/v1/chase/detail".Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<GateFuturesChaseOrderDetailResponse>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
        if (result.Success && result.Data?.Order == null)
            return result.AsError<GateFuturesChaseOrder>(new DeserializeError("Chase response omitted its order", result.Data));
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
                AutoSize = orderAutoSize == GateFuturesOrderAutoSize.None ? null : orderAutoSize,
            }
        }, ct);

    // Create a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrderId>> PlacePriceTriggeredOrderAsync(GateFuturesSettlement settle, GateFuturesPriceTriggeredOrderRequest request, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Create(request);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrderId>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // Modify a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrderId>> AmendPriceTriggeredOrderAsync(GateFuturesSettlement settle, GateFuturesPriceTriggeredOrderUpdateRequest request, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.Amend(settle, request);

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
        GateFuturesPriceOrderValidation.Query(status, contract, limit, offset);

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
        GateFuturesPriceOrderValidation.Contract(contract);

        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);

        var endpoint = "{settle}/price_orders".Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPriceTriggeredOrder>>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }

    // Get a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> GetPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.OrderId(orderId);

        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString(CultureInfo.InvariantCulture));
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Cancel a price-triggered order
    internal Task<RestCallResult<GateFuturesPriceTriggeredOrder>> CancelPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        GateFuturesPriceOrderValidation.OrderId(orderId);

        var endpoint = "{settle}/price_orders/{order_id}"
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString(CultureInfo.InvariantCulture));
        return _.SendRequestInternal<GateFuturesPriceTriggeredOrder>(_.GetUrl(api, v4, futures, endpoint), HttpMethod.Delete, ct, true);
    }

}
