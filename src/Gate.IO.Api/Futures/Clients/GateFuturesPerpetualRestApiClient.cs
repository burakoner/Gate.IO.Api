using Gate.IO.Api.Models.RestApi.Futures;
using Gate.IO.Api.Models.RestApi.Spot;

namespace Gate.IO.Api.Futures.Clients;

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
    internal GateRestApiClient Root { get; }

    // Public Clients
    public GateFuturesPerpetualSettleRestApiClient BTC { get; }
    public GateFuturesPerpetualSettleRestApiClient USD { get; }
    public GateFuturesPerpetualSettleRestApiClient USDT { get; }
    public Dictionary<FuturesPerpetualSettle, GateFuturesPerpetualSettleRestApiClient> Clients { get; }
    public GateFuturesPerpetualSettleRestApiClient this[FuturesPerpetualSettle settle] => Clients[settle];

    internal GateFuturesPerpetualRestApiClient(GateRestApiClient root)
    {
        Root = root;

        BTC = new GateFuturesPerpetualSettleRestApiClient(this, FuturesPerpetualSettle.BTC);
        USD = new GateFuturesPerpetualSettleRestApiClient(this, FuturesPerpetualSettle.USD);
        USDT = new GateFuturesPerpetualSettleRestApiClient(this, FuturesPerpetualSettle.USDT);
        Clients = new Dictionary<FuturesPerpetualSettle, GateFuturesPerpetualSettleRestApiClient>
        {
            { FuturesPerpetualSettle.BTC, BTC },
            { FuturesPerpetualSettle.USD, USD },
            { FuturesPerpetualSettle.USDT, USDT },
        };
    }

    #region List all futures contracts
    internal Task<RestCallResult<IEnumerable<PerpetualContract>>> GetContractsAsync(FuturesPerpetualSettle settle, CancellationToken ct = default)
    {
        var endpoint = settleContractsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<PerpetualContract>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct);
    }
    #endregion

    #region Get a single contract
    internal Task<RestCallResult<PerpetualContract>> GetContractAsync(FuturesPerpetualSettle settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleContractsContractEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<PerpetualContract>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct);
    }
    #endregion

    #region Futures order book
    internal Task<RestCallResult<FuturesOrderBook>> GetOrderBookAsync(FuturesPerpetualSettle settle, string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        var endpoint = settleOrderBookEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<FuturesOrderBook>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Futures trading history
    internal Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(FuturesPerpetualSettle settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    => GetTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, lastId, ct);

    internal Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(FuturesPerpetualSettle settle, string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleTradesEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesTrade>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Get futures candlesticks
    internal Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(FuturesPerpetualSettle settle, string prefix, string contract, FuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetCandlesticksAsync(settle, prefix, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(FuturesPerpetualSettle settle, string prefix, string contract, FuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", prefix + contract },
            { "interval", JsonConvert.SerializeObject(interval, new FuturesCandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleCandlesticksEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesCandlestick>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Premium Index K-Line
    internal Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetPremiumIndexCandlesticksAsync(FuturesPerpetualSettle settle, string contract, FuturesCandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetPremiumIndexCandlesticksAsync(settle, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetPremiumIndexCandlesticksAsync(FuturesPerpetualSettle settle, string contract, FuturesCandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", JsonConvert.SerializeObject(interval, new FuturesCandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settlePremiumIndexEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesCandlestick>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region List futures tickers
    internal Task<RestCallResult<IEnumerable<PerpetualTicker>>> GetTickersAsync(FuturesPerpetualSettle settle, string contract = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleTickersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<PerpetualTicker>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Funding rate history
    internal Task<RestCallResult<IEnumerable<FuturesFundingRate>>> GetFundingRateHistoryAsync(FuturesPerpetualSettle settle, string contract, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "limit", limit },
        };

        var endpoint = settleFundingRateEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesFundingRate>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Futures insurance balance history
    internal Task<RestCallResult<IEnumerable<FuturesInsuranceBalance>>> GetInsuranceHistoryAsync(FuturesPerpetualSettle settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
        };

        var endpoint = settleInsuranceEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesInsuranceBalance>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Futures stats
    internal Task<RestCallResult<IEnumerable<FuturesStats>>> GetStatsAsync(FuturesPerpetualSettle settle, string contract, FuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
    => GetStatsAsync(settle, contract, interval, from.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<IEnumerable<FuturesStats>>> GetStatsAsync(FuturesPerpetualSettle settle, string contract, FuturesStatsInterval interval, long? from = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", JsonConvert.SerializeObject(interval, new FuturesStatsIntervalConverter(false)) },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("from", from);

        var endpoint = settleContractStatsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesStats>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Get index constituents
    internal Task<RestCallResult<FuturesIndexConstituents>> GetIndexConstituentsAsync(FuturesPerpetualSettle settle, string index, CancellationToken ct = default)
    {
        var endpoint = settleIndexConstituentsIndexEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{index}", index);
        return Root.SendRequestInternal<FuturesIndexConstituents>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct);
    }
    #endregion

    #region Retrieve liquidation history
    internal Task<RestCallResult<IEnumerable<FuturesLiquidate>>> GetLiquidatesAsync(FuturesPerpetualSettle settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetLiquidatesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<IEnumerable<FuturesLiquidate>>> GetLiquidatesAsync(FuturesPerpetualSettle settle, string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
        };
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleLiqOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesLiquidate>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
    #endregion

    #region Query futures account
    internal Task<RestCallResult<IEnumerable<FuturesAccount>>> GetAccountAsync(FuturesPerpetualSettle settle, CancellationToken ct = default)
    {
        var endpoint = settleAccountsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesAccount>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Query account book
    internal Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesPerpetualSettle settle, FuturesAccountBookType type, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => GetAccountBookAsync(settle, type, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct);

    internal Task<RestCallResult<IEnumerable<FuturesAccountBook>>> GetAccountBookAsync(FuturesPerpetualSettle settle, FuturesAccountBookType? type, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FuturesAccountBookTypeConverter(false)));
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleAccountBookEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesAccountBook>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List all positions of a user
    internal Task<RestCallResult<IEnumerable<FuturesPosition>>> GetPositionsAsync(FuturesPerpetualSettle settle, CancellationToken ct = default)
    {
        var endpoint = settlePositionsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesPosition>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Get single position
    internal Task<RestCallResult<FuturesPosition>> GetPositionAsync(FuturesPerpetualSettle settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settlePositionsContractEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Update position margin
    internal Task<RestCallResult<FuturesPosition>> SetPositionMarginAsync(FuturesPerpetualSettle settle, string contract, decimal change, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "change", change }
        };

        var endpoint = settlePositionsContractMarginEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position leverage
    internal Task<RestCallResult<FuturesPosition>> SetLeverageAsync(FuturesPerpetualSettle settle, string contract, int leverage, int? crossLeverageLimit = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "leverage", leverage }
        };
        parameters.AddOptionalParameter("cross_leverage_limit", crossLeverageLimit);

        var endpoint = settlePositionsContractLeverageEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position risk limit
    internal Task<RestCallResult<FuturesPosition>> SetRiskLimitAsync(FuturesPerpetualSettle settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "risk_limit", riskLimit }
        };

        var endpoint = settlePositionsContractRiskLimitEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Enable or disable dual mode
    internal Task<RestCallResult<FuturesAccount>> SetDualModeAsync(FuturesPerpetualSettle settle, bool dualMode, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "dual_mode", dualMode.ToString().ToLower() }
        };

        var endpoint = settleDualModeEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<FuturesAccount>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Retrieve position detail in dual mode
    internal Task<RestCallResult<FuturesPosition>> GetDualModePositionAsync(FuturesPerpetualSettle settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleDualCompPositionsContractEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Update position margin in dual mode
    internal Task<RestCallResult<FuturesPosition>> SetDualModePositionMarginAsync(FuturesPerpetualSettle settle, string contract, FuturesDualModeSide side, decimal change, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "dual_side", JsonConvert.SerializeObject(side, new FuturesDualModeSideConverter(false)) },
            { "change", change },
        };

        var endpoint = settleDualCompPositionsContractMarginEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position leverage in dual mode
    internal Task<RestCallResult<FuturesPosition>> SetDualModeLeverageAsync(FuturesPerpetualSettle settle, string contract, int leverage, int? crossLeverageLimit = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "leverage", leverage }
        };
        parameters.AddOptionalParameter("cross_leverage_limit", crossLeverageLimit);

        var endpoint = settleDualCompPositionsContractLeverageEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Update position risk limit in dual mode
    internal Task<RestCallResult<FuturesPosition>> SetDualModeRiskLimitAsync(FuturesPerpetualSettle settle, string contract, decimal riskLimit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "risk_limit", riskLimit }
        };

        var endpoint = settleDualCompPositionsContractRiskLimitEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{contract}", contract);
        return Root.SendRequestInternal<FuturesPosition>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Create a futures order
    internal Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(
        FuturesPerpetualSettle settle,
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

    internal Task<RestCallResult<FuturesOrder>> PlaceOrderAsync(FuturesPerpetualSettle settle, FuturesOrderRequest request, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(request.Contract);
        ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);

        var parameters = new Dictionary<string, object> {
            { "currency_pair", request.Contract },
            { "size", request.Size },
            { "iceberg", request.Iceberg },
        };
        parameters.AddOptionalParameter("price", request.Price?.ToGateString());
        parameters.AddOptionalParameter("close", request.Close);
        parameters.AddOptionalParameter("reduce_only", request.ReduceOnly);
        parameters.AddOptionalParameter("tif", JsonConvert.SerializeObject(request.TimeInForce, new FuturesTimeInForceConverter(false)));
        parameters.AddOptionalParameter("text", request.ClientOrderId);
        parameters.AddOptionalParameter("auto_size", JsonConvert.SerializeObject(request.AutoSize, new FuturesOrderAutoSizeConverter(false)));


        var endpoint = settleOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
    #endregion

    #region List futures orders
    internal Task<RestCallResult<IEnumerable<FuturesOrder>>> GetOrdersAsync(FuturesPerpetualSettle settle, string contract, FuturesOrderStatus status, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "status", JsonConvert.SerializeObject(status, new FuturesOrderStatusConverter(false)) },
            { "contract", contract },
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("last_id", lastId);

        var endpoint = settleOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesOrder>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders matched
    internal Task<RestCallResult<IEnumerable<FuturesOrder>>> CancelOpenOrdersAsync(FuturesPerpetualSettle settle, string contract, FuturesOrderSide side, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "side", JsonConvert.SerializeObject(side, new FuturesOrderSideConverter(false)) },
            { "contract", contract },
        };

        var endpoint = settleOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesOrder>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Create a futures order
    internal Task<RestCallResult<IEnumerable<FuturesBatchOrder>>> PlaceOrderAsync(FuturesPerpetualSettle settle, IEnumerable<FuturesOrderRequest> requests, CancellationToken ct = default)
    {
        foreach (var request in requests)
        {
            PerpetualHelpers.ValidateContractSymbol(request.Contract);
            ExchangeHelpers.ValidateClientOrderId(request.ClientOrderId, true);
        }

        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        var endpoint = settleBatchOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesBatchOrder>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
    #endregion

    #region Get a single order
    internal Task<RestCallResult<FuturesOrder>> GetOrderAsync(FuturesPerpetualSettle settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a single order
    internal Task<RestCallResult<FuturesOrder>> CancelOrderAsync(FuturesPerpetualSettle settle, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

    #region Amend an order
    internal Task<RestCallResult<FuturesOrder>> AmendOrderAsync(FuturesPerpetualSettle settle, long? orderId = null, string clientOrderId = null, decimal? price = null, long? size = null, CancellationToken ct = default)
    {
        if (orderId.HasValue && !string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        if (!orderId.HasValue && string.IsNullOrWhiteSpace(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("price", price?.ToGateString());
        parameters.AddOptionalParameter("size", size);

        var endpoint = settleOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{order_id}", orderId.HasValue ? orderId.Value.ToString() : clientOrderId);
        return Root.SendRequestInternal<FuturesOrder>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Put, ct, true, bodyParameters: parameters);
    }
    #endregion

    #region List personal trading history
    internal Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(FuturesPerpetualSettle settle, string contract = "", long? orderId = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("last_id", lastId);
        parameters.AddOptionalParameter("order", orderId);

        var endpoint = settleMyTradesEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesUserTrade>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List personal trading history by time range
    internal Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(FuturesPerpetualSettle settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetUserTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);
    internal Task<RestCallResult<IEnumerable<FuturesUserTrade>>> GetUserTradesAsync(FuturesPerpetualSettle settle, string contract = "", long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleMyTradesTimerangeEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesUserTrade>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List position close history
    internal Task<RestCallResult<IEnumerable<FuturesPositionClose>>> GetPositionClosesAsync(FuturesPerpetualSettle settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetPositionClosesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);
    internal Task<RestCallResult<IEnumerable<FuturesPositionClose>>> GetPositionClosesAsync(FuturesPerpetualSettle settle, string contract = "", long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "offset", offset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settlePositionCloseEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesPositionClose>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region List liquidation history
    internal Task<RestCallResult<IEnumerable<FuturesUserLiquidate>>> GetUserLiquidatesAsync(FuturesPerpetualSettle settle, string contract = "", int limit = 100, long? at = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
        };
        parameters.AddOptionalParameter("contract", contract);
        parameters.AddOptionalParameter("at", at);

        var endpoint = settleLiquidatesEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesUserLiquidate>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Countdown cancel orders
    internal async Task<RestCallResult<DateTime>> CountdownCancelOrdersAsync(FuturesPerpetualSettle settle, int timeout, string contract = "", CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(contract)) PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new Dictionary<string, object> {
            { "timeout", timeout },
        };
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleCountdownCancelAllEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        var result = await Root.SendRequestInternal<FuturesCountdown>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.Time ?? default);
    }
    #endregion

    #region Create a price-triggered order
    internal Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(
        FuturesPerpetualSettle settle,
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

    internal async Task<RestCallResult<long>> PlacePriceTriggeredOrderAsync(FuturesPerpetualSettle settle, FuturesTriggerOrderRequest request, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(request.Order.Contract);

        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        var result = await Root.SendRequestInternal<FuturesTriggerOrderId>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
        return result.As(result.Data?.OrderId ?? default);
    }
    #endregion

    #region List all auto orders
    internal Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> GetPriceTriggeredOrdersAsync(
    FuturesPerpetualSettle settle,
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

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesTriggerOrderResponse>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Cancel all open orders
    internal Task<RestCallResult<IEnumerable<FuturesTriggerOrderResponse>>> CancelPriceTriggeredOrdersAsync(FuturesPerpetualSettle settle, string contract, CancellationToken ct = default)
    {
        PerpetualHelpers.ValidateContractSymbol(contract);

        var parameters = new Dictionary<string, object>
        {
            { "contract", contract }
        };

        var endpoint = settlePriceOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)));
        return Root.SendRequestInternal<IEnumerable<FuturesTriggerOrderResponse>>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true, queryParameters: parameters);
    }
    #endregion

    #region Get a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> GetPriceTriggeredOrderAsync(FuturesPerpetualSettle settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{order_id}", orderId.ToString());
        return Root.SendRequestInternal<FuturesTriggerOrderResponse>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true);
    }
    #endregion

    #region Cancel a price-triggered order
    internal Task<RestCallResult<FuturesTriggerOrderResponse>> CancelPriceTriggeredOrderAsync(FuturesPerpetualSettle settle, long orderId, CancellationToken ct = default)
    {
        var endpoint = settlePriceOrdersOrderIdEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesPerpetualSettleConverter(false)))
            .Replace("{order_id}", orderId.ToString());
        return Root.SendRequestInternal<FuturesTriggerOrderResponse>(Root.GetUrl(api, version, futures, endpoint), HttpMethod.Delete, ct, true);
    }
    #endregion

}