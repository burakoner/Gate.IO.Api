using Gate.IO.Api.Bot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Bot;

[Trait("Category", "Unit")]
public class BotRequestConstructionTests
{
    [Fact]
    public async Task Signed_bot_requests_serialize_queries_bodies_custom_headers_and_authentication_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Bot/recommend.success.json"),
            JsonFixture.Read("Docs/Bot/create.success.json"),
            JsonFixture.Read("Docs/Bot/create.success.json"),
            JsonFixture.Read("Docs/Bot/create.success.json"),
            JsonFixture.Read("Docs/Bot/create.success.json"),
            JsonFixture.Read("Docs/Bot/create.success.json"),
            JsonFixture.Read("Docs/Bot/create.success.json"),
            JsonFixture.Read("Docs/Bot/running.success.json"),
            JsonFixture.Read("Docs/Bot/detail.success.json"),
            JsonFixture.Read("Docs/Bot/stop.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var recommendation = await client.Bot.GetStrategyRecommendationsAsync(new GateBotRecommendationRequest
        {
            Market = "BTC_USDT",
            StrategyType = GateBotStrategyType.SpotGrid,
            Direction = GateBotRecommendationDirection.Neutral,
            InvestAmount = 1000m,
            Scene = GateBotDiscoverScene.Filter,
            RefreshRecommendationId = "spot_grid|BTC_USDT",
            Limit = 5,
            MaxDrawdownLessThanOrEqual = 0.08m,
            BacktestAprGreaterThanOrEqual = 0.12m,
            ServiceId = "svc-test",
            AppLanguage = "en-US",
            RequestId = "req-001",
            TraceId = "trace-001",
        });
        var spotGrid = await client.Bot.CreateSpotGridAsync(new GateBotSpotGridCreateRequest
        {
            Market = "BTC_USDT",
            CreateParameters = new GateBotSpotGridCreateParameters
            {
                Money = 1000m,
                LowPrice = 58000m,
                HighPrice = 65000m,
                GridNumber = 40,
                PriceType = GateBotGridPriceType.Arithmetic,
                TriggerPrice = 59000m,
                StopProfit = 68000m,
                StopLoss = 56000m,
                ProfitSharingRatio = 0.01m,
                IsUseBase = true,
            },
        });
        var marginGrid = await client.Bot.CreateMarginGridAsync(new GateBotMarginGridCreateRequest
        {
            Market = "BTC_USDT",
            CreateParameters = new GateBotMarginGridCreateParameters
            {
                Money = 1000m,
                LowPrice = 58000m,
                HighPrice = 65000m,
                GridNumber = 40,
                PriceType = GateBotGridPriceType.Geometric,
                Leverage = 3m,
                Direction = GateBotFuturesDirection.Long,
                TriggerPrice = 59000m,
                StopProfit = 68000m,
                StopLoss = 56000m,
                ProfitSharingRatio = 0.01m,
                IsUseBase = false,
            },
        });
        var infiniteGrid = await client.Bot.CreateInfiniteGridAsync(new GateBotInfiniteGridCreateRequest
        {
            Market = "BTC_USDT",
            CreateParameters = new GateBotInfiniteGridCreateParameters
            {
                Money = 1000m,
                PriceFloor = 58000m,
                ProfitPerGrid = 0.01m,
                GridNumber = 30,
                PriceType = GateBotGridPriceType.Arithmetic,
                TriggerPrice = 59000m,
                StopProfit = 68000m,
                StopLoss = 56000m,
                ProfitSharingRatio = 0.01m,
                IsUseBase = true,
            },
        });
        var futuresGrid = await client.Bot.CreateFuturesGridAsync(new GateBotFuturesGridCreateRequest
        {
            Market = "BTC_USDT",
            CreateParameters = new GateBotFuturesGridCreateParameters
            {
                Money = 1000m,
                LowPrice = 58000m,
                HighPrice = 65000m,
                GridNumber = 40,
                PriceType = GateBotGridPriceType.Arithmetic,
                Leverage = 5m,
                Direction = GateBotFuturesDirection.Short,
            },
        });
        var spotMartingale = await client.Bot.CreateSpotMartingaleAsync(new GateBotSpotMartingaleCreateRequest
        {
            Market = "BTC_USDT",
            CreateParameters = new GateBotSpotMartingaleCreateParameters
            {
                InvestAmount = 1000m,
                PriceDeviation = 0.02m,
                MaxOrders = 6,
                TakeProfitRatio = 0.01m,
                StopLossPerCycle = 0.2m,
                TriggerPrice = 59000m,
                ProfitSharingRatio = 0.01m,
            },
        });
        var contractMartingale = await client.Bot.CreateContractMartingaleAsync(new GateBotContractMartingaleCreateRequest
        {
            Market = "BTC_USDT",
            CreateParameters = new GateBotContractMartingaleCreateParameters
            {
                InvestAmount = 1000m,
                PriceDeviation = 0.02m,
                MaxOrders = 6,
                TakeProfitRatio = 0.01m,
                Direction = GateBotContractMartingaleDirection.Sell,
                Leverage = 3m,
                StopLossPrice = 56000m,
                ProfitSharingRatio = 0.01m,
            },
        });
        var running = await client.Bot.GetRunningPortfoliosAsync(new GateBotRunningPortfolioQueryRequest
        {
            StrategyType = GateBotStrategyType.SpotGrid,
            Market = "BTC_USDT",
            Page = 2,
            PageSize = 50,
        });
        var detail = await client.Bot.GetPortfolioDetailAsync(new GateBotPortfolioDetailRequest
        {
            StrategyId = "bot-strategy-001",
            StrategyType = GateBotStrategyType.SpotGrid,
        });
        var stop = await client.Bot.StopPortfolioAsync(new GateBotPortfolioStopRequest
        {
            StrategyId = "bot-strategy-001",
            StrategyType = GateBotStrategyType.SpotGrid,
            ServiceId = "svc-stop",
            AppLanguage = "en-US",
            RequestId = "req-stop",
            TraceId = "trace-stop",
        });

        Assert.True(recommendation.Success, recommendation.Error?.ToString());
        Assert.True(spotGrid.Success, spotGrid.Error?.ToString());
        Assert.True(marginGrid.Success, marginGrid.Error?.ToString());
        Assert.True(infiniteGrid.Success, infiniteGrid.Error?.ToString());
        Assert.True(futuresGrid.Success, futuresGrid.Error?.ToString());
        Assert.True(spotMartingale.Success, spotMartingale.Error?.ToString());
        Assert.True(contractMartingale.Success, contractMartingale.Error?.ToString());
        Assert.True(running.Success, running.Error?.ToString());
        Assert.True(detail.Success, detail.Error?.ToString());
        Assert.True(stop.Success, stop.Error?.ToString());
        Assert.Equal(10, handler.Requests.Count);

        var recommendationQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/bot/strategy/recommend", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("BTC_USDT", recommendationQuery["market"]);
        Assert.Equal("spot_grid", recommendationQuery["strategy_type"]);
        Assert.Equal("neutral", recommendationQuery["direction"]);
        Assert.Equal("1000", recommendationQuery["invest_amount"]);
        Assert.Equal("filter", recommendationQuery["scene"]);
        Assert.Equal("spot_grid|BTC_USDT", recommendationQuery["refresh_recommendation_id"]);
        Assert.Equal("5", recommendationQuery["limit"]);
        Assert.Equal("0.08", recommendationQuery["max_drawdown_lte"]);
        Assert.Equal("0.12", recommendationQuery["backtest_apr_gte"]);
        Assert.Equal("svc-test", Assert.Single(handler.Requests[0].Headers["X-Gate-Service-Id"]));
        Assert.Equal("en-US", Assert.Single(handler.Requests[0].Headers["X-Gate-AppLang"]));
        Assert.Equal("req-001", Assert.Single(handler.Requests[0].Headers["X-Request-Id"]));
        Assert.Equal("trace-001", Assert.Single(handler.Requests[0].Headers["X-Trace-Id"]));

        var spotBody = ParseBody(handler.Requests[1]);
        Assert.Equal("/api/v4/bot/spot-grid/create", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("spot_grid", spotBody["strategy_type"]!.ToString());
        Assert.Equal("BTC_USDT", spotBody["market"]!.ToString());
        Assert.Equal("1000", spotBody["create_params"]!["money"]!.ToString());
        Assert.Equal("58000", spotBody["create_params"]!["low_price"]!.ToString());
        Assert.Equal("65000", spotBody["create_params"]!["high_price"]!.ToString());
        Assert.Equal("40", spotBody["create_params"]!["grid_num"]!.ToString());
        Assert.Equal("0", spotBody["create_params"]!["price_type"]!.ToString());
        Assert.Equal("59000", spotBody["create_params"]!["trigger_price"]!.ToString());
        Assert.Equal("True", spotBody["create_params"]!["is_use_base"]!.ToString());

        var marginBody = ParseBody(handler.Requests[2]);
        Assert.Equal("/api/v4/bot/margin-grid/create", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("margin_grid", marginBody["strategy_type"]!.ToString());
        Assert.Equal("3", marginBody["create_params"]!["leverage"]!.ToString());
        Assert.Equal("long", marginBody["create_params"]!["direction"]!.ToString());
        Assert.Equal("1", marginBody["create_params"]!["price_type"]!.ToString());
        Assert.Equal("False", marginBody["create_params"]!["is_use_base"]!.ToString());

        var infiniteBody = ParseBody(handler.Requests[3]);
        Assert.Equal("/api/v4/bot/infinite-grid/create", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.Equal("infinite_grid", infiniteBody["strategy_type"]!.ToString());
        Assert.Equal("58000", infiniteBody["create_params"]!["price_floor"]!.ToString());
        Assert.Equal("0.01", infiniteBody["create_params"]!["profit_per_grid"]!.ToString());
        Assert.Equal("30", infiniteBody["create_params"]!["grid_num"]!.ToString());

        var futuresBody = ParseBody(handler.Requests[4]);
        Assert.Equal("/api/v4/bot/futures-grid/create", handler.Requests[4].RequestUri.AbsolutePath);
        Assert.Equal("futures_grid", futuresBody["strategy_type"]!.ToString());
        Assert.Equal("5", futuresBody["create_params"]!["leverage"]!.ToString());
        Assert.Equal("short", futuresBody["create_params"]!["direction"]!.ToString());

        var spotMartingaleBody = ParseBody(handler.Requests[5]);
        Assert.Equal("/api/v4/bot/spot-martingale/create", handler.Requests[5].RequestUri.AbsolutePath);
        Assert.Equal("spot_martingale", spotMartingaleBody["strategy_type"]!.ToString());
        Assert.Equal("1000", spotMartingaleBody["create_params"]!["invest_amount"]!.ToString());
        Assert.Equal("0.02", spotMartingaleBody["create_params"]!["price_deviation"]!.ToString());
        Assert.Equal("6", spotMartingaleBody["create_params"]!["max_orders"]!.ToString());
        Assert.Equal("0.2", spotMartingaleBody["create_params"]!["stop_loss_per_cycle"]!.ToString());

        var contractMartingaleBody = ParseBody(handler.Requests[6]);
        Assert.Equal("/api/v4/bot/contract-martingale/create", handler.Requests[6].RequestUri.AbsolutePath);
        Assert.Equal("contract_martingale", contractMartingaleBody["strategy_type"]!.ToString());
        Assert.Equal("sell", contractMartingaleBody["create_params"]!["direction"]!.ToString());
        Assert.Equal("3", contractMartingaleBody["create_params"]!["leverage"]!.ToString());
        Assert.Equal("56000", contractMartingaleBody["create_params"]!["stop_loss_price"]!.ToString());

        var runningQuery = ParseQuery(handler.Requests[7].RequestUri);
        Assert.Equal("/api/v4/bot/portfolio/running", handler.Requests[7].RequestUri.AbsolutePath);
        Assert.Equal("spot_grid", runningQuery["strategy_type"]);
        Assert.Equal("BTC_USDT", runningQuery["market"]);
        Assert.Equal("2", runningQuery["page"]);
        Assert.Equal("50", runningQuery["page_size"]);

        var detailQuery = ParseQuery(handler.Requests[8].RequestUri);
        Assert.Equal("/api/v4/bot/portfolio/detail", handler.Requests[8].RequestUri.AbsolutePath);
        Assert.Equal("bot-strategy-001", detailQuery["strategy_id"]);
        Assert.Equal("spot_grid", detailQuery["strategy_type"]);

        var stopBody = ParseBody(handler.Requests[9]);
        Assert.Equal("/api/v4/bot/portfolio/stop", handler.Requests[9].RequestUri.AbsolutePath);
        Assert.Equal("bot-strategy-001", stopBody["strategy_id"]!.ToString());
        Assert.Equal("spot_grid", stopBody["strategy_type"]!.ToString());
        Assert.Equal("svc-stop", Assert.Single(handler.Requests[9].Headers["X-Gate-Service-Id"]));
        Assert.Equal("req-stop", Assert.Single(handler.Requests[9].Headers["X-Request-Id"]));
        Assert.All(handler.Requests, AssertSignedHeaders);
    }

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
        => new(new GateRestApiClientOptions
        {
            HttpClient = new HttpClient(handler),
        });

    private static HttpResponseMessage JsonResponse(string json)
        => new(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

    private static JObject ParseBody(RecordedHttpRequest request)
        => JObject.Parse(request.Content);

    private static Dictionary<string, string> ParseQuery(Uri uri)
    {
        return uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .ToDictionary(
                x => Uri.UnescapeDataString(x[0]),
                x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]));
    }

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}
