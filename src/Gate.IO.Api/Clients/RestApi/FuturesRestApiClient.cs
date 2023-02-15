using Gate.IO.Api.Models.RestApi.FlashSwap;
using Gate.IO.Api.Models.RestApi.Futures;

namespace Gate.IO.Api.Clients.RestApi;

public class FuturesRestApiClient : RestApiClient
{
    // Api
    protected const string api = "api";
    protected const string version = "4";
    protected const string futures = "futures";

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

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("Gate.IO Futures RestApi");

    // Root Client
    internal GateRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public GateRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal FuturesRestApiClient(GateRestApiClient root) : base("Gate.IO Futures RestApi", root.ClientOptions)
    {
        RootClient = root;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new GateAuthenticationProvider(credentials);

    protected override CallError ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Spot.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    public override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    internal async Task<RestCallResult<T>> SendRequestInternal<T>(
        Uri uri,
        HttpMethod method,
        CancellationToken cancellationToken,
        bool signed = false,
        Dictionary<string, object> queryParameters = null,
        Dictionary<string, object> bodyParameters = null,
        Dictionary<string, string> headerParameters = null,
        ArraySerialization? arraySerialization = null,
        JsonSerializer deserializer = null,
        bool ignoreRatelimit = false,
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }
    #endregion

    #region List all futures contracts
    public async Task<RestCallResult<IEnumerable<FuturesContract>>> GetContractsAsync(FuturesSettle settle, CancellationToken ct = default)
    {
        var endpoint = settleContractsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesContract>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Get a single contract
    public async Task<RestCallResult<FuturesContract>> GetContractAsync(FuturesSettle settle, string contract, CancellationToken ct = default)
    {
        var endpoint = settleContractsContractEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)))
            .Replace("{contract}", contract);
        return await SendRequestInternal<FuturesContract>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Futures order book
    public async Task<RestCallResult<FuturesOrderBook>> GetOrderBookAsync(FuturesSettle settle, string contract, decimal interval = 0.0m, int limit = 10, bool withId = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", interval },
            { "limit", limit },
            { "with_id", withId.ToString().ToLower() },
        };

        var endpoint = settleOrderBookEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<FuturesOrderBook>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Futures trading history
    public async Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(FuturesSettle settle, string contract, DateTime from, DateTime to, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
    => await GetTradesAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, lastId, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesTrade>>> GetTradesAsync(FuturesSettle settle, string contract, long? from = null, long? to = null, int limit = 100, int offset = 0, long? lastId = null, CancellationToken ct = default)
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

        var endpoint = settleTradesEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesTrade>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get futures candlesticks
    public async Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(FuturesSettle settle, string contract, CandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetCandlesticksAsync(settle, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetCandlesticksAsync(FuturesSettle settle, string contract, CandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", JsonConvert.SerializeObject(interval, new CandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleCandlesticksEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesCandlestick>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Premium Index K-Line
    public async Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetPremiumIndexCandlesticksAsync(FuturesSettle settle, string contract, CandlestickInterval interval, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetPremiumIndexCandlesticksAsync(settle, contract, interval, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesCandlestick>>> GetPremiumIndexCandlesticksAsync(FuturesSettle settle, string contract, CandlestickInterval interval, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", JsonConvert.SerializeObject(interval, new CandlestickIntervalConverter(false)) },
        };
        if (!from.HasValue && !to.HasValue) parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settlePremiumIndexEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesCandlestick>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List futures tickers
    public async Task<RestCallResult<IEnumerable<FuturesTicker>>> GetTickersAsync(FuturesSettle settle, string contract = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("contract", contract);

        var endpoint = settleTickersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesTicker>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Funding rate history
    public async Task<RestCallResult<IEnumerable<FuturesFundingRate>>> GetFundingRateHistoryAsync(FuturesSettle settle, string contract, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "limit", limit },
        };

        var endpoint = settleFundingRateEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesFundingRate>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Futures insurance balance history
    public async Task<RestCallResult<IEnumerable<FuturesInsuranceBalance>>> GetInsuranceHistoryAsync(FuturesSettle settle, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
        };

        var endpoint = settleInsuranceEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesInsuranceBalance>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Futures stats
    public async Task<RestCallResult<IEnumerable<FuturesStats>>> GetStatsAsync(FuturesSettle settle, string contract, FuturesStatsInterval interval, DateTime from, int limit = 100, CancellationToken ct = default)
    => await GetStatsAsync(settle, contract, interval, from.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesStats>>> GetStatsAsync(FuturesSettle settle, string contract, FuturesStatsInterval interval, long? from = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
            { "interval", JsonConvert.SerializeObject(interval, new FuturesStatsIntervalConverter(false)) },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("from", from);

        var endpoint = settleContractStatsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesStats>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get index constituents
    public async Task<RestCallResult<FuturesIndexConstituents>> GetIndexConstituentsAsync(FuturesSettle settle, string index, CancellationToken ct = default)
    {
        var endpoint = settleIndexConstituentsIndexEndpoint
            .Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)))
            .Replace("{index}", index);
        return await SendRequestInternal<FuturesIndexConstituents>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve liquidation history
    public async Task<RestCallResult<IEnumerable<FuturesPublicLiquidation>>> GetLiquidationHistoryAsync(FuturesSettle settle, string contract, DateTime from, DateTime to, int limit = 100, CancellationToken ct = default)
    => await GetLiquidationHistoryAsync(settle, contract, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<FuturesPublicLiquidation>>> GetLiquidationHistoryAsync(FuturesSettle settle, string contract, long? from = null, long? to = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "contract", contract },
        };
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        var endpoint = settleLiqOrdersEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesPublicLiquidation>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query futures account
    public async Task<RestCallResult<IEnumerable<FuturesPublicLiquidation>>> GetAccountAsync(FuturesSettle settle, CancellationToken ct = default)
    {
        var endpoint = settleAccountsEndpoint.Replace("{settle}", JsonConvert.SerializeObject(settle, new FuturesSettleConverter(false)));
        return await SendRequestInternal<IEnumerable<FuturesPublicLiquidation>>(RootClient.GetUrl(api, version, futures, endpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

}