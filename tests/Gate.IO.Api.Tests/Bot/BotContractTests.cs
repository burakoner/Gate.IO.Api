using Gate.IO.Api.Bot;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Bot;

[Trait("Category", "Contract")]
public class BotContractTests
{
    [Fact]
    public void Recommendation_and_create_responses_deserialize()
    {
        var recommendation = JsonFixture.Parse("Docs/Bot/recommend.success.json")["data"]!.ToObject<GateBotRecommendationResult>()!;
        var create = JsonFixture.Parse("Docs/Bot/create.success.json")["data"]!.ToObject<GateBotCreateResult>()!;

        Assert.Equal(GateBotDiscoverScene.TopOne, recommendation.Scene);
        Assert.Equal("leverage_lte", Assert.Single(recommendation.UnsupportedFilters));
        Assert.Single(recommendation.Recommendations);
        Assert.Equal("spot_grid|BTC_USDT|bt-001", recommendation.Recommendations[0].RecommendationId);
        Assert.Equal(GateBotStrategyType.SpotGrid, recommendation.Recommendations[0].StrategyType);
        Assert.Equal(0.216m, recommendation.Recommendations[0].BacktestApr);
        Assert.Equal("58000", recommendation.Recommendations[0].StrategyParametersPreview["low_price"]!.ToString());
        Assert.Equal("bot-strategy-001", create.StrategyId);
        Assert.Equal(GateBotStrategyType.SpotGrid, create.StrategyType);
        Assert.Equal("running", create.Status);
    }

    [Fact]
    public void Portfolio_responses_deserialize()
    {
        var running = JsonFixture.Parse("Docs/Bot/running.success.json")["data"]!.ToObject<GateBotRunningStrategiesPage>()!;
        var detail = JsonFixture.Parse("Docs/Bot/detail.success.json")["data"]!.ToObject<GateBotPortfolioDetail>()!;
        var stop = JsonFixture.Parse("Docs/Bot/stop.success.json")["data"]!.ToObject<GateBotStopResult>()!;

        Assert.Equal(1, running.Page);
        Assert.Single(running.Items);
        Assert.Equal(GateBotStrategyType.SpotGrid, running.Items[0].StrategyType);
        Assert.Equal(12.345m, running.Items[0].Pnl);
        Assert.NotEqual(default, running.Items[0].CreatedAt);
        Assert.Equal("bot-strategy-001", detail.StrategyId);
        Assert.Equal(1000m, detail.BaseInfo.InvestAmount);
        Assert.Equal(8.75m, detail.Metrics.GridProfit);
        Assert.Equal(40, detail.Metrics.GridCount);
        Assert.Equal(0.015m, detail.Position.Amount);
        Assert.True(detail.StopSupported);
        Assert.Equal(GateBotStrategyType.SpotGrid, stop.StrategyType);
        Assert.Equal("stopping", stop.Status);
        Assert.Equal("stop request accepted", stop.ResultMessage);
    }
}
