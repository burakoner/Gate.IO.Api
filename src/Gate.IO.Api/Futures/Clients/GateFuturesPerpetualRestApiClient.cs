﻿namespace Gate.IO.Api.Futures;

/// <summary>
/// Gate.IO Futures Perpetual REST API Client
/// </summary>
public class GateFuturesPerpetualRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string futures = "futures";

    // Endpoints
    private const string settleContractsEndpoint = "{settle}/contracts";
    private const string settleContractsContractEndpoint = "{settle}/contracts/{contract}";
    private const string settleOrderBookEndpoint = "{settle}/order_book";
    private const string settleTradesEndpoint = "{settle}/trades";
    private const string settleCandlesticksEndpoint = "{settle}/candlesticks";
    private const string settlePremiumIndexEndpoint = "{settle}/premium_index";
    private const string settleTickersEndpoint = "{settle}/tickers";
    private const string settleFundingRateEndpoint = "{settle}/funding_rate";
    private const string settleInsuranceEndpoint = "{settle}/insurance";
    private const string settleContractStatsEndpoint = "{settle}/contract_stats";
    private const string settleIndexConstituentsIndexEndpoint = "{settle}/index_constituents/{index}";
    private const string settleLiqOrdersEndpoint = "{settle}/liq_orders";
    private const string settleAccountsEndpoint = "{settle}/accounts";
    private const string settleAccountBookEndpoint = "{settle}/account_book";
    private const string settlePositionsEndpoint = "{settle}/positions";
    private const string settlePositionsContractEndpoint = "{settle}/positions/{contract}";
    private const string settlePositionsContractMarginEndpoint = "{settle}/positions/{contract}/margin";
    private const string settlePositionsContractLeverageEndpoint = "{settle}/positions/{contract}/leverage";
    private const string settlePositionsContractRiskLimitEndpoint = "{settle}/positions/{contract}/risk_limit";
    private const string settleDualModeEndpoint = "{settle}/dual_mode";
    private const string settleDualCompPositionsContractEndpoint = "{settle}/dual_comp/positions/{contract}";
    private const string settleDualCompPositionsContractMarginEndpoint = "{settle}/dual_comp/positions/{contract}/margin";
    private const string settleDualCompPositionsContractLeverageEndpoint = "{settle}/dual_comp/positions/{contract}/leverage";
    private const string settleDualCompPositionsContractRiskLimitEndpoint = "{settle}/dual_comp/positions/{contract}/risk_limit";
    private const string settleOrdersEndpoint = "{settle}/orders";
    private const string settleBatchOrdersEndpoint = "{settle}/batch_orders";
    private const string settleOrdersOrderIdEndpoint = "{settle}/orders/{order_id}";
    private const string settleMyTradesEndpoint = "{settle}/my_trades";
    private const string settleMyTradesTimerangeEndpoint = "{settle}/my_trades_timerange";
    private const string settlePositionCloseEndpoint = "{settle}/position_close";
    private const string settleLiquidatesEndpoint = "{settle}/liquidates";
    private const string settleCountdownCancelAllEndpoint = "{settle}/countdown_cancel_all";
    private const string settlePriceOrdersEndpoint = "{settle}/price_orders";
    private const string settlePriceOrdersOrderIdEndpoint = "{settle}/price_orders/{order_id}";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Public Clients
    public GateFuturesPerpetualSettleRestApiClient BTC { get; }
    public GateFuturesPerpetualSettleRestApiClient USD { get; }
    public GateFuturesPerpetualSettleRestApiClient USDT { get; }
    public Dictionary<GateFuturesSettlement, GateFuturesPerpetualSettleRestApiClient> Clients { get; }
    public GateFuturesPerpetualSettleRestApiClient this[GateFuturesSettlement settle] => Clients[settle];

    // Constructor
    internal GateFuturesPerpetualRestApiClient(GateRestApiClient root)
    {
        _ = root;

        BTC = new GateFuturesPerpetualSettleRestApiClient(this, GateFuturesSettlement.BTC);
        USD = new GateFuturesPerpetualSettleRestApiClient(this, GateFuturesSettlement.USD);
        USDT = new GateFuturesPerpetualSettleRestApiClient(this, GateFuturesSettlement.USDT);
        Clients = new Dictionary<GateFuturesSettlement, GateFuturesPerpetualSettleRestApiClient>
        {
            { GateFuturesSettlement.BTC, BTC },
            { GateFuturesSettlement.USD, USD },
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
        var endpoint = settleContractsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesContract>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get a single contract
    internal Task<RestCallResult<GateFuturesContract>> GetContractAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleContractsContractEndpoint
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

        var endpoint = settleOrderBookEndpoint.Replace("{settle}", MapConverter.GetString(settle));
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

        var endpoint = settleTradesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
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

        var endpoint = settleCandlesticksEndpoint.Replace("{settle}", MapConverter.GetString(settle));
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

        var endpoint = settlePremiumIndexEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesCandlestickPremium>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // List futures tickers
    internal Task<RestCallResult<List<GateFuturesTicker>>> GetTickersAsync(GateFuturesSettlement settle, string contract = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleTickersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
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

        var endpoint = settleFundingRateEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesFundingRate>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures insurance balance history
    internal Task<RestCallResult<List<GateFuturesInsuranceBalance>>> GetInsuranceHistoryAsync(GateFuturesSettlement settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };

        var endpoint = settleInsuranceEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesInsuranceBalance>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Futures stats
    internal Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(GateFuturesSettlement settle, string contract, FuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
    => GetStatsAsync(settle, contract, interval, from.ConvertToMilliseconds(), limit, ct);

    // Futures stats
    internal Task<RestCallResult<List<GateFuturesStats>>> GetStatsAsync(GateFuturesSettlement settle, string contract, FuturesStatsInterval? interval = null, long? from = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
            { "limit", limit },
        };
        parameters.AddOptionalEnum("interval", interval);
        parameters.AddOptionalParameter("from", from);

        var endpoint = settleContractStatsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesStats>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // Get index constituents
    internal Task<RestCallResult<GateFuturesIndexConstituents>> GetIndexConstituentsAsync(GateFuturesSettlement settle, string index, CancellationToken ct = default)
    {
        var endpoint = settleIndexConstituentsIndexEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{index}", index);
        return _.SendRequestInternal<GateFuturesIndexConstituents>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct);
    }

    // Retrieve liquidation history
    internal Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidatesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetLiquidatesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    // Retrieve liquidation history
    internal Task<RestCallResult<List<GateFuturesLiquidation>>> GetLiquidatesAsync(GateFuturesSettlement settle, string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleLiqOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesLiquidation>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    // TODO: List risk limit tiers

    // Query futures account
    internal Task<RestCallResult<GateFuturesBalance>> GetBalanceAsync(GateFuturesSettlement settle, CancellationToken ct = default)
    {
        var endpoint = settleAccountsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesBalance>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, GateFuturesBalanceType type, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetBalanceHistoryAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), type, limit, offset, ct);

    // Query account book
    internal Task<RestCallResult<List<GateFuturesBalanceChange>>> GetBalanceHistoryAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, GateFuturesBalanceType? type = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("contract", contract);
        parameters.AddOptional("from", from);
        parameters.AddOptional("to", to);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = settleAccountBookEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesBalanceChange>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // List all positions of a user
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetPositionsAsync(
        GateFuturesSettlement settle,
        bool? holding = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("holding", holding);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        var endpoint = settlePositionsEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Get single position
    internal Task<RestCallResult<GateFuturesPosition>> GetPositionAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settlePositionsContractEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Update position margin
    internal Task<RestCallResult<GateFuturesPosition>> SetPositionMarginAsync(GateFuturesSettlement settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("change", change);

        var endpoint = settlePositionsContractMarginEndpoint
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

        var endpoint = settlePositionsContractLeverageEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position risk limit
    internal Task<RestCallResult<GateFuturesPosition>> SetRiskLimitAsync(GateFuturesSettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = settlePositionsContractRiskLimitEndpoint
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

        var endpoint = settleDualModeEndpoint
            .Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesBalance>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Retrieve position detail in dual mode
    internal Task<RestCallResult<List<GateFuturesPosition>>> GetDualModePositionsAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleDualCompPositionsContractEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<List<GateFuturesPosition>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }

    // Update position margin in dual mode
    internal Task<RestCallResult<GateFuturesPosition>> SetDualModeMarginAsync(GateFuturesSettlement settle, string contract, FuturesDualModeSide side, decimal change, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("dual_side", side);
        parameters.AddString("change", change);

        var endpoint = settleDualCompPositionsContractMarginEndpoint
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

        var endpoint = settleDualCompPositionsContractLeverageEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{contract}", contract);
        return _.SendRequestInternal<GateFuturesPosition>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }

    // Update position risk limit in dual mode
    internal Task<RestCallResult<GateFuturesPosition>> SetDualModeRiskLimitAsync(GateFuturesSettlement settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddString("risk_limit", riskLimit);

        var endpoint = settleDualCompPositionsContractRiskLimitEndpoint
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
        GateFuturesSelfTradeAction?  selfTradeAction = null,
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

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    #region List futures orders
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

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders matched
    internal Task<RestCallResult<List<GateFuturesOrder>>> CancelOpenOrdersAsync(GateFuturesSettlement settle, string contract, FuturesOrderSide side, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "contract", contract },
        };
        parameters.AddEnum("side", side);

        var endpoint = settleOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<GateFuturesOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Create a futures order
    internal Task<RestCallResult<List<FuturesBatchOrder>>> PlaceOrderAsync(GateFuturesSettlement settle, IEnumerable<GateFuturesOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
        {
            PerpetualHelpers.ValidateContractSymbol(request.Contract);
            ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);
        }

        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        var endpoint = settleBatchOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<FuturesBatchOrder>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
    #endregion

    #region Get a single order
    internal Task<RestCallResult<GateFuturesOrder>> GetOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a single order
    internal Task<RestCallResult<GateFuturesOrder>> CancelOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

    #region Amend an order
    internal Task<RestCallResult<GateFuturesOrder>> AmendOrderAsync(GateFuturesSettlement settle, long? orderId = null, string clientOrderId = null, decimal? price = null, long? size = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("price", price?.ToGateString());
        parameters.AddOptionalParameter("size", size);

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return _.SendRequestInternal<GateFuturesOrder>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }
    #endregion

    #region List personal trading history
    internal Task<RestCallResult<List<FuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract = null, long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("order", orderId);

        var endpoint = settleMyTradesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<FuturesUserTrade>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List personal trading history by time range
    internal Task<RestCallResult<List<FuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetUserTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);
    internal Task<RestCallResult<List<FuturesUserTrade>>> GetUserTradesAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleMyTradesTimerangeEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<FuturesUserTrade>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List position close history
    internal Task<RestCallResult<List<FuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetPositionClosesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);
    internal Task<RestCallResult<List<FuturesPositionClose>>> GetPositionClosesAsync(GateFuturesSettlement settle, string contract = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settlePositionCloseEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<FuturesPositionClose>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List liquidation history
    internal Task<RestCallResult<List<FuturesUserLiquidate>>> GetUserLiquidatesAsync(GateFuturesSettlement settle, string contract = null, int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = settleLiquidatesEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<FuturesUserLiquidate>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Countdown cancel orders
    internal async Task<RestCallResult<DateTime>> CountdownCancelOrdersAsync(GateFuturesSettlement settle, int timeout, string contract = null, CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(contract)) PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new ParameterCollection {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleCountdownCancelAllEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<FuturesCountdown>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Time ?? default);
    }
    #endregion

    #region Create a price-triggered order
    internal Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        GateFuturesSettlement settle,
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

    internal async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(GateFuturesSettlement settle, FuturesTriggerOrderRequest request, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(request.Order.Contract);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        var result = await _.SendRequestInternal<FuturesTriggerOrderId>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId ?? default);
    }
    #endregion

    #region List all auto orders
    internal Task<RestCallResult<List<FuturesTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(
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

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<FuturesTriggerOrderResponse>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders
    internal Task<RestCallResult<List<FuturesTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(GateFuturesSettlement settle, string contract, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new ParameterCollection
        {
            { "contract", contract }
        };

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", MapConverter.GetString(settle));
        return _.SendRequestInternal<List<FuturesTriggerOrderResponse>>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Get a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> GetPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<FuturesTriggerOrderResponse>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(GateFuturesSettlement settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", MapConverter.GetString(settle))
            .Replace("{order_id}", orderId.ToString());
        return _.SendRequestInternal<FuturesTriggerOrderResponse>(_.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

}