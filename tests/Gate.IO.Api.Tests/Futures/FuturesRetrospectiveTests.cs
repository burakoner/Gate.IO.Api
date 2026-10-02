using ApiSharp.Models;
using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;
using System.Globalization;
using System.Net;
using System.Text;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Contract")]
public class FuturesRetrospectiveTests
{
    private static readonly string[] TimeRoutes = ["trades", "mark", "index", "premium", "funding", "stats", "liquidations",
        "balance", "positions", "orders", "user-trades", "closes", "adl",
        "trades-dto", "candles-dto", "mark-dto", "index-dto", "premium-dto", "funding-dto", "stats-dto",
        "liquidations-dto", "balance-dto", "positions-dto", "orders-dto", "user-trades-dto", "closes-dto", "adl-dto",
        "user-liquidations-dto", "trail-list", "chase-list"];

    public static IEnumerable<object[]> TimeCases() => from route in TimeRoutes
        from kind in new[] { DateTimeKind.Utc, DateTimeKind.Local, DateTimeKind.Unspecified } select new object[] { route, kind };

    [Theory]
    [MemberData(nameof(TimeCases))]
    public async Task All_futures_date_filters_encode_instants_not_local_wall_clocks(string route, DateTimeKind kind)
    {
        var utc = new DateTime(2026, 10, 2, 10, 0, 0, DateTimeKind.Utc);
        var from = kind == DateTimeKind.Local ? utc.ToLocalTime() : DateTime.SpecifyKind(utc, kind);
        var to = from.AddMinutes(1);
        var response = route is "trail-list" or "chase-list" ? "{\"orders\":[]}" : "[]";
        var handler = Handler(response);
        using var client = Client(handler);
        await CallTime(client.Futures.USD1, route, from, to);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.StartsWith("/api/v4/futures/usd1/", request.RequestUri.AbsolutePath);
        var query = Query(request.RequestUri);
        var startKey = route is "trail-list" or "chase-list" ? "start_at" : "from";
        var endKey = route is "trail-list" or "chase-list" ? "end_at" : "to";
        Assert.Equal(new DateTimeOffset(utc).ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture), query[startKey]);
        if (query.ContainsKey(endKey)) Assert.Equal(new DateTimeOffset(utc.AddMinutes(1)).ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture), query[endKey]);
        if (query.ContainsKey("at")) Assert.Equal(query[startKey], query["at"]);
    }

    [Theory]
    [InlineData("trail-list")]
    [InlineData("chase-list")]
    [InlineData("user-liquidations-dto")]
    [InlineData("candles-dto")]
    public async Task Range_validation_compares_transmitted_instants_when_kinds_differ(string route)
    {
        var from = new DateTime(2026, 10, 2, 10, 0, 0, DateTimeKind.Utc).ToLocalTime();
        var to = from.ToUniversalTime().AddMinutes(1);
        var handler = Handler(route is "trail-list" or "chase-list" ? "{\"orders\":[]}" : "[]");
        using var client = Client(handler);
        await CallTime(client.Futures.USD1, route, from, to);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("null")]
    [InlineData("")]
    public async Task Empty_strategy_and_countdown_bodies_do_not_bypass_required_container_checks(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var api = client.Futures.USD1;
        await Failed(api.CancelAllAsync(5));
        await Failed(api.GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest()));
        await Failed(api.CancelTrailOrdersAsync(new GateFuturesTrailOrdersCancelRequest()));
        await Failed(api.GetTrailOrderChangeLogAsync(new GateFuturesTrailOrderChangeLogQueryRequest { OrderId = 117 }));
        await Failed(api.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest()));
        await Failed(api.CancelChaseOrdersAsync(new GateFuturesChaseOrdersCancelRequest()));
        await Failed(api.GetChaseOrderAsync("117"));
        await Failed(api.CancelChaseOrderAsync("117"));
        Assert.Equal(8, handler.Requests.Count); // No retry or replacement request.
    }

    [Fact]
    public async Task Real_empty_strategy_lists_are_still_successful_and_omitted_times_stay_omitted()
    {
        var handler = Handler("{\"orders\":[],\"change_log\":[]}");
        using var client = Client(handler);
        var api = client.Futures.USD1;
        await Ok(api.GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest()));
        await Ok(api.CancelTrailOrdersAsync(new GateFuturesTrailOrdersCancelRequest()));
        await Ok(api.GetTrailOrderChangeLogAsync(new GateFuturesTrailOrderChangeLogQueryRequest { OrderId = 117 }));
        await Ok(api.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest()));
        await Ok(api.CancelChaseOrdersAsync(new GateFuturesChaseOrdersCancelRequest()));
        Assert.Equal(5, handler.Requests.Count);
        Assert.All(handler.Requests, r => { Assert.DoesNotContain("start_at", r.RequestUri.Query); Assert.DoesNotContain("end_at", r.RequestUri.Query); });
    }

    [Theory]
    [InlineData("0", 0L)]
    [InlineData("1", 1L)]
    [InlineData("\"1000\"", 1000L)]
    [InlineData("1780000000123", 1780000000123L)]
    public async Task Countdown_time_uses_documented_milliseconds_without_unit_guessing(string token, long milliseconds)
    {
        var handler = Handler($"{{\"triggerTime\":{token}}}");
        using var client = Client(handler);
        var result = await client.Futures.USD1.CancelAllAsync(5);
        Assert.True(result.Success);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime, result.Data);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("1.1")]
    [InlineData("\"1.1\"")]
    [InlineData("true")]
    [InlineData("\"9223372036854775808\"")]
    [InlineData("9223372036854775807")]
    public async Task Malformed_countdown_time_cannot_confirm_a_cancellation_schedule(string token)
    {
        var handler = Handler($"{{\"triggerTime\":{token}}}");
        using var client = Client(handler);
        await Failed(client.Futures.USD1.CancelAllAsync(5));
        Assert.Single(handler.Requests);
    }

    private static Task CallTime(GateFuturesRestApiSettleClient api, string route, DateTime from, DateTime to) => route switch
    {
        "trades" => Ok(api.GetTradesAsync("BTC_USD1", from, to)),
        "mark" => Ok(api.GetMarkPriceCandlesticksAsync("BTC_USD1", GateFuturesCandlestickInterval.OneMinute, from, to)),
        "index" => Ok(api.GetIndexPriceCandlesticksAsync("BTC_USD1", GateFuturesCandlestickInterval.OneMinute, from, to)),
        "premium" => Ok(api.GetPremiumIndexCandlesticksAsync("BTC_USD1", GateFuturesCandlestickInterval.OneMinute, from, to)),
        "funding" => Ok(api.GetFundingRateHistoryAsync("BTC_USD1", from, to)),
        "stats" => Ok(api.GetStatsAsync("BTC_USD1", GateFuturesStatsInterval.OneMinute, from)),
        "liquidations" => Ok(api.GetLiquidationsAsync("BTC_USD1", from, to)),
        "balance" => Ok(api.GetBalanceHistoryAsync("BTC_USD1", from, to, GateFuturesBalanceChangeType.Funding)),
        "positions" => Ok(api.GetHistoricalPositionsAsync("BTC_USD1", from, to)),
        "orders" => Ok(api.GetOrdersAsync("BTC_USD1", from, to)),
        "user-trades" => Ok(api.GetUserTradesAsync("BTC_USD1", from, to)),
        "closes" => Ok(api.GetPositionClosesAsync("BTC_USD1", from, to)),
        "adl" => Ok(api.GetAdlHistoryAsync("BTC_USD1", from, to, from)),
        "trades-dto" => Ok(api.GetTradesAsync(new GateFuturesTradeQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "candles-dto" => Ok(api.GetCandlesticksAsync(Candles(from, to))),
        "mark-dto" => Ok(api.GetMarkPriceCandlesticksAsync(Candles(from, to))),
        "index-dto" => Ok(api.GetIndexPriceCandlesticksAsync(Candles(from, to))),
        "premium-dto" => Ok(api.GetPremiumIndexCandlesticksAsync(Candles(from, to))),
        "funding-dto" => Ok(api.GetFundingRateHistoryAsync(new GateFuturesFundingRateQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "stats-dto" => Ok(api.GetStatsAsync(new GateFuturesStatsQueryRequest { Contract = "BTC_USD1", From = from, Interval = GateFuturesStatsInterval.OneMinute })),
        "liquidations-dto" => Ok(api.GetLiquidationsAsync(new GateFuturesLiquidationQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "balance-dto" => Ok(api.GetBalanceHistoryAsync(new GateFuturesBalanceHistoryQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "positions-dto" => Ok(api.GetHistoricalPositionsAsync(new GateFuturesHistoricalPositionQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "orders-dto" => Ok(api.GetOrdersAsync(new GateFuturesOrderTimeRangeQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "user-trades-dto" => Ok(api.GetUserTradesAsync(new GateFuturesUserTradeTimeRangeQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "closes-dto" => Ok(api.GetPositionClosesAsync(new GateFuturesPositionCloseQueryRequest { Contract = "BTC_USD1", From = from, To = to })),
        "adl-dto" => Ok(api.GetAdlHistoryAsync(new GateFuturesAdlHistoryQueryRequest { Contract = "BTC_USD1", From = from, To = to, At = from })),
        "user-liquidations-dto" => Ok(api.GetUserLiquidationsAsync(new GateFuturesUserLiquidationQueryRequest { Contract = "BTC_USD1", From = from, To = to, At = from })),
        "trail-list" => Ok(api.GetTrailOrdersAsync(new GateFuturesTrailOrderQueryRequest { StartAt = from, EndAt = to })),
        "chase-list" => Ok(api.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest { StartAt = from, EndAt = to, IsFinished = true })),
        _ => throw new ArgumentOutOfRangeException(nameof(route)),
    };

    private static GateFuturesCandlestickQueryRequest Candles(DateTime from, DateTime to)
        => new() { Contract = "BTC_USD1", Interval = GateFuturesCandlestickInterval.OneMinute, From = from, To = to };

    private static async Task Ok<T>(Task<RestCallResult<T>> call)
    {
        var result = await call;
        Assert.True(result.Success, result.Error?.ToString());
    }

    private static async Task Failed<T>(Task<RestCallResult<T>> call)
    {
        var result = await call;
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
    }

    private static Dictionary<string, string> Query(Uri uri)
        => uri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split('=', 2))
            .ToDictionary(x => Uri.UnescapeDataString(x[0]), x => Uri.UnescapeDataString(x[1]));

    private static RecordingHttpMessageHandler Handler(string json)
        => new(_ => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }
}
