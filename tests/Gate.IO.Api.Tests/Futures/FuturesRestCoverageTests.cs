using ApiSharp.Models;
using Gate.IO.Api.Futures;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Unit")]
public class FuturesRestCoverageTests
{
    // Inventory of the current reference on 2026-10-02, not merely v4.106.126's changelog:
    // https://www.gate.com/docs/developers/apiv4/en/futures/
    // GET candlesticks has trading, mark and index variants: 73 cases, 71 operations, 64 paths.
    private record Wire(string Key, string Method, string Path, bool Signed, string Response, Func<GateFuturesRestApiSettleClient, Task<bool>> Call);
    private static string F(string name) => JsonFixture.Read($"Docs/Futures/{name}.success.json");
    private static async Task<bool> Ok<T>(Task<RestCallResult<T>> call)
    {
        var result = await call;
        Assert.True(result.Success, result.Error?.ToString());
        return result.Success;
    }

    private static readonly Wire[] Cases =
    [
        new("contracts", "GET", "contracts", false, F("contracts"), a => Ok(a.GetContractsAsync())),
        new("contracts-all", "GET", "contracts_all", false, F("contracts"), a => Ok(a.GetAllContractsAsync())),
        new("contract", "GET", "contracts/BTC_USDT", false, F("contract"), a => Ok(a.GetContractAsync("BTC_USDT"))),
        new("adl-risk", "GET", "adl_risk_states", false, F("adl_risk_states"), a => Ok(a.GetAdlRiskStatesAsync())),
        new("book", "GET", "order_book", false, F("order_book"), a => Ok(a.GetOrderBookAsync("BTC_USDT"))),
        new("trades", "GET", "trades", false, F("trades"), a => Ok(a.GetTradesAsync("BTC_USDT"))),
        new("candles", "GET", "candlesticks", false, F("candlesticks"), a => Ok(a.GetCandlesticksAsync("BTC_USDT", GateFuturesCandlestickInterval.OneMinute))),
        new("candles-mark", "GET", "candlesticks", false, F("candlesticks"), a => Ok(a.GetMarkPriceCandlesticksAsync("BTC_USDT", GateFuturesCandlestickInterval.OneMinute))),
        new("candles-index", "GET", "candlesticks", false, F("candlesticks"), a => Ok(a.GetIndexPriceCandlesticksAsync("BTC_USDT", GateFuturesCandlestickInterval.OneMinute))),
        new("premium", "GET", "premium_index", false, "[]", a => Ok(a.GetPremiumIndexCandlesticksAsync("BTC_USDT", GateFuturesCandlestickInterval.OneMinute))),
        new("tickers", "GET", "tickers", false, F("tickers"), a => Ok(a.GetTickersAsync())),
        new("funding", "GET", "funding_rate", false, F("funding_rate"), a => Ok(a.GetFundingRateHistoryAsync("BTC_USDT"))),
        new("funding-batch", "POST", "funding_rates", false, F("funding_rates"), a => Ok(a.GetBatchFundingRateHistoryAsync(new[] { "BTC_USDT" }))),
        new("insurance", "GET", "insurance", false, F("insurance"), a => Ok(a.GetInsuranceHistoryAsync())),
        new("stats", "GET", "contract_stats", false, F("contract_stats"), a => Ok(a.GetStatsAsync(new GateFuturesStatsQueryRequest { Contract = "BTC_USDT" }))),
        new("index", "GET", "index_constituents/BTC_USDT", false, F("index_constituents"), a => Ok(a.GetIndexConstituentsAsync("BTC_USDT"))),
        new("public-liquidations", "GET", "liq_orders", false, F("liq_orders"), a => Ok(a.GetLiquidationsAsync())),
        new("risk-tiers", "GET", "risk_limit_tiers", false, F("risk_limit_tiers"), a => Ok(a.GetRiskLimitTiersAsync())),
        new("account", "GET", "accounts", true, F("account"), a => Ok(a.GetBalancesAsync())),
        new("account-book", "GET", "account_book", true, F("account_book"), a => Ok(a.GetBalanceHistoryAsync())),
        new("positions", "GET", "positions", true, F("positions"), a => Ok(a.GetPositionsAsync())),
        new("position-history", "GET", "positions_timerange", true, F("positions"), a => Ok(a.GetHistoricalPositionsAsync("BTC_USDT"))),
        new("position", "GET", "positions/BTC_USDT", true, "{}", a => Ok(a.GetPositionAsync("BTC_USDT"))),
        new("leverage-get", "GET", "get_leverage/BTC_USDT", true, "{\"Lever\":\"10\"}", a => Ok(a.GetLeverageAsync("BTC_USDT", GateFuturesPositionMarginMode.Cross, GateFuturesDualModeSide.DualLong))),
        new("margin", "POST", "positions/BTC_USDT/margin", true, "{}", a => Ok(a.SetPositionMarginAsync("BTC_USDT", -1.25m))),
        new("leverage", "POST", "positions/BTC_USDT/leverage", true, "{}", a => Ok(a.SetLeverageAsync("BTC_USDT", 0m, 10m))),
        new("leverage-mode", "POST", "positions/BTC_USDT/set_leverage", true, "{}", a => Ok(a.SetPositionLeverageAsync("BTC_USDT", 10m, GateFuturesPositionMarginMode.Cross))),
        new("margin-mode", "POST", "positions/cross_mode", true, "{}", a => Ok(a.SetMarginModeAsync("BTC_USDT", GateFuturesMarginMode.Isolated))),
        new("margin-mode-hedge", "POST", "dual_comp/positions/cross_mode", true, "{}", a => Ok(a.SwithMarginModeUnderHedgeAsync("BTC_USDT", GateFuturesMarginMode.Cross))),
        new("risk-limit", "POST", "positions/BTC_USDT/risk_limit", true, "{}", a => Ok(a.SetRiskLimitAsync("BTC_USDT", 1000m))),
        new("dual-mode", "POST", "dual_mode", true, F("account"), a => Ok(a.SetDualModeAsync(false))),
        new("position-mode", "POST", "set_position_mode", true, F("account"), a => Ok(a.SetPositionModeAsync(GateFuturesAccountPositionMode.DualPlus))),
        new("dual-positions", "GET", "dual_comp/positions/BTC_USDT", true, F("positions"), a => Ok(a.GetDualModePositionsAsync("BTC_USDT"))),
        new("dual-margin", "POST", "dual_comp/positions/BTC_USDT/margin", true, "{}", a => Ok(a.SetDualModeMarginAsync("BTC_USDT", GateFuturesDualModeSide.DualShort, -1m))),
        new("dual-leverage", "POST", "dual_comp/positions/BTC_USDT/leverage", true, "{}", a => Ok(a.SetDualModeLeverageAsync("BTC_USDT", 0m, 10m))),
        new("dual-risk", "POST", "dual_comp/positions/BTC_USDT/risk_limit", true, "{}", a => Ok(a.SetDualModeRiskLimitAsync("BTC_USDT", 1000m))),
        new("order-create", "POST", "orders", true, F("order"), a => Ok(a.PlaceOrderAsync(new GateFuturesOrderRequest { Contract = "BTC_USDT", Size = 1.25m, Price = 100m }))),
        new("orders-list", "GET", "orders", true, "[]", a => Ok(a.GetOrdersAsync(new GateFuturesOrderQueryRequest { Status = GateFuturesOrderStatus.Open }))),
        new("orders-cancel-all", "DELETE", "orders", true, "[]", a => Ok(a.CancelOrdersAsync(new GateFuturesOrderCancelAllRequest()))),
        new("orders-history", "GET", "orders_timerange", true, "[]", a => Ok(a.GetOrdersAsync(new GateFuturesOrderTimeRangeQueryRequest()))),
        new("orders-batch-create", "POST", "batch_orders", true, "[{\"id\":117,\"succeeded\":true}]", a => Ok(a.PlaceOrdersAsync(new[] { new GateFuturesOrderRequest { Contract = "BTC_USDT", Size = 1.25m, Price = 100m } }))),
        new("order-get", "GET", "orders/117", true, F("order"), a => Ok(a.GetOrderAsync(117))),
        new("order-amend", "PUT", "orders/117", true, F("order"), a => Ok(a.AmendOrderAsync(117, price: 100.25m))),
        new("order-cancel", "DELETE", "orders/117", true, F("order"), a => Ok(a.CancelOrderAsync(117))),
        new("my-trades", "GET", "my_trades", true, "[]", a => Ok(a.GetUserTradesAsync(new GateFuturesUserTradeQueryRequest()))),
        new("my-trades-history", "GET", "my_trades_timerange", true, "[]", a => Ok(a.GetUserTradesAsync(new GateFuturesUserTradeTimeRangeQueryRequest()))),
        new("position-closes", "GET", "position_close", true, "[]", a => Ok(a.GetPositionClosesAsync())),
        new("my-liquidations", "GET", "liquidates", true, "[]", a => Ok(a.GetUserLiquidationsAsync(new GateFuturesUserLiquidationQueryRequest()))),
        new("adl-history", "GET", "auto_deleverages", true, "[]", a => Ok(a.GetAdlHistoryAsync())),
        new("countdown", "POST", "countdown_cancel_all", true, "{\"triggerTime\":1780000000000}", a => Ok(a.CancelAllAsync(5))),
        new("fee", "GET", "fee", true, "{}", a => Ok(a.GetTradingFeesAsync())),
        new("batch-cancel", "POST", "batch_cancel_orders", true, "[{\"id\":\"117\",\"user_id\":111,\"succeeded\":true}]", a => Ok(a.CancelOrdersAsync(new[] { 117L }))),
        new("batch-amend", "POST", "batch_amend_orders", true, "[{\"id\":117,\"succeeded\":true}]", a => Ok(a.AmendOrdersAsync(new[] { new GateFuturesOrderAmendRequest { OrderId = 117, Price = 100.25m } }))),
        new("risk-table", "GET", "risk_limit_table", false, "[]", a => Ok(a.GetRiskLimitTableAsync("TABLE_ID"))),
        new("bbo", "POST", "bbo_orders", true, F("order"), a => Ok(a.PlaceBboOrderAsync(new GateFuturesBboOrderRequest { Contract = "BTC_USDT", Size = -2, Direction = GateFuturesBboDirection.Buy, Level = 20 }))),
        new("trail-create", "POST", "autoorder/v1/trail/create", true, "{\"code\":0,\"data\":{\"id\":\"117\"},\"message\":\"ok\"}", a => Ok(a.PlaceTrailOrderAsync(new GateFuturesTrailOrderRequest { Contract = "BTC_USDT", Amount = 1.25m }))),
        new("trail-stop", "POST", "autoorder/v1/trail/stop", true, "{\"id\":\"117\"}", a => Ok(a.CancelTrailOrderAsync(117))),
        new("trail-stop-all", "POST", "autoorder/v1/trail/stop_all", true, "{\"orders\":[]}", a => Ok(a.CancelTrailOrdersAsync(new GateFuturesTrailOrdersCancelRequest()))),
        new("trail-list", "GET", "autoorder/v1/trail/list", true, "{\"orders\":[]}", a => Ok(a.GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest()))),
        new("trail-detail", "GET", "autoorder/v1/trail/detail", true, "{\"code\":0,\"data\":{\"order\":{\"id\":\"117\"}}}", a => Ok(a.GetTrailOrderAsync(117))),
        new("trail-update", "POST", "autoorder/v1/trail/update", true, "{\"order\":{\"id\":\"117\"}}", a => Ok(a.UpdateTrailOrderAsync(new GateFuturesTrailOrderUpdateRequest { OrderId = 117, IsGreaterThanOrEqual = false }))),
        new("trail-changes", "GET", "autoorder/v1/trail/change_log", true, "{\"change_log\":[]}", a => Ok(a.GetTrailOrderChangeLogAsync(new GateFuturesTrailOrderChangeLogQueryRequest { OrderId = 117 }))),
        new("chase-create", "POST", "autoorder/v1/chase/create", true, F("chase_order_id"), a => Ok(a.PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest { Contract = "btc_usdt", Amount = "-1.25", PriceLimit = "0" }))),
        new("chase-stop", "POST", "autoorder/v1/chase/stop", true, F("chase_order"), a => Ok(a.CancelChaseOrderAsync("117"))),
        new("chase-stop-all", "POST", "autoorder/v1/chase/stop_all", true, F("chase_orders"), a => Ok(a.CancelChaseOrdersAsync(new GateFuturesChaseOrdersCancelRequest()))),
        new("chase-list", "GET", "autoorder/v1/chase/list", true, F("chase_orders"), a => Ok(a.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest()))),
        new("chase-detail", "GET", "autoorder/v1/chase/detail", true, F("chase_order"), a => Ok(a.GetChaseOrderAsync("117"))),
        new("price-create", "POST", "price_orders", true, F("price_order_id"), a => Ok(a.PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest { Order = new GateFuturesInitial { Contract = "BTC_USDT", Size = 1, Price = "100" }, Trigger = new GateFuturesTrigger { Price = "101", StrategyType = GateFuturesTriggerStrategy.ByPrice, PriceType = GateFuturesTriggerPrice.DealPrice, Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo } }))),
        new("price-list", "GET", "price_orders", true, F("price_orders"), a => Ok(a.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open))),
        new("price-cancel-all", "DELETE", "price_orders", true, F("price_orders"), a => Ok(a.CancelPriceTriggeredOrdersAsync())),
        new("price-get", "GET", "price_orders/117", true, F("price_order"), a => Ok(a.GetPriceTriggeredOrderAsync(117))),
        new("price-cancel", "DELETE", "price_orders/117", true, F("price_order"), a => Ok(a.CancelPriceTriggeredOrderAsync(117))),
        new("price-amend", "PUT", "price_orders/amend", true, F("price_order_id"), a => Ok(a.AmendPriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderUpdateRequest { OrderId = 117, Price = "100.5" }))),
    ];

    public static IEnumerable<object[]> RouteCases()
        => from entry in Cases from settle in new[] { GateFuturesSettlement.BTC, GateFuturesSettlement.USDT, GateFuturesSettlement.USD1 } select new object[] { entry.Key, settle };

    [Fact]
    public void Inventory_has_all_71_documented_operations_on_64_paths_and_two_extra_candle_variants()
    {
        Assert.Equal(73, Cases.Length);
        Assert.Equal(71, Cases.Select(x => (x.Method, x.Path)).Distinct().Count());
        Assert.Equal(64, Cases.Select(x => x.Path).Distinct().Count());
        var operations = Cases.DistinctBy(x => (x.Method, x.Path)).ToArray();
        Assert.Equal(17, operations.Count(x => !x.Signed));
        Assert.Equal(54, operations.Count(x => x.Signed));
    }

    [Theory]
    [MemberData(nameof(RouteCases))]
    public async Task Every_rest_operation_uses_the_selected_settlement_and_documented_method_and_auth(string key, GateFuturesSettlement settle)
    {
        var entry = Cases.Single(x => x.Key == key);
        var handler = new RecordingHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(entry.Response, Encoding.UTF8, "application/json") });
        using var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        Assert.True(await entry.Call(client.Futures[settle]));
        var request = Assert.Single(handler.Requests);
        Assert.Equal(entry.Method, request.Method.Method);
        Assert.Equal($"/api/v4/futures/{settle.ToString().ToLowerInvariant()}/{entry.Path}", request.RequestUri.AbsolutePath);
        Assert.Equal("1", Assert.Single(request.Headers["X-Gate-Size-Decimal"]));
        if (!entry.Signed)
        {
            Assert.DoesNotContain("KEY", request.Headers.Keys);
            Assert.DoesNotContain("SIGN", request.Headers.Keys);
            Assert.DoesNotContain("Timestamp", request.Headers.Keys);
            return;
        }
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        Assert.True(long.TryParse(timestamp, NumberStyles.Integer, CultureInfo.InvariantCulture, out _));
        var hash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var input = $"{entry.Method}\n{request.RequestUri.AbsolutePath}\n{request.RequestUri.Query.TrimStart('?')}\n{hash}\n{timestamp}";
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes("secret"));
        Assert.Equal(Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(input))).ToLowerInvariant(), Assert.Single(request.Headers["SIGN"]));
        if (entry.Method == "GET") Assert.Empty(request.Content);
    }
}
