using Gate.IO.Api.Earn;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Earn;

[Trait("Category", "Contract")]
public class EarnContractTests
{
    [Fact]
    public void Documented_dual_investment_responses_deserialize()
    {
        var plans = JsonFixture.Deserialize<List<GateEarnDualPlan>>("Docs/Earn/dual_plans.success.json");
        var orders = JsonFixture.Deserialize<List<GateEarnDualOrder>>("Docs/Earn/dual_orders.success.json");
        var balance = JsonFixture.Deserialize<GateEarnDualBalance>("Docs/Earn/dual_balance.success.json");
        var preview = JsonFixture.Deserialize<GateEarnDualRefundPreview>("Docs/Earn/dual_refund_preview.success.json");
        var recommendations = JsonFixture.Deserialize<List<GateEarnDualRecommendation>>("Docs/Earn/dual_recommendations.success.json");

        Assert.Single(plans);
        Assert.Equal(272, plans[0].Id);
        Assert.Equal(GateEarnDualOptionType.Put, plans[0].Type);
        Assert.Equal(0.067m, plans[0].ExercisePrice);
        Assert.Equal(1m, plans[0].PerValue);
        Assert.Single(orders);
        Assert.Equal(373, orders[0].Id);
        Assert.Equal(24500m, orders[0].ExercisePrice);
        Assert.Equal(0.68m, orders[0].ApyDisplay);
        Assert.Equal(30.13m, balance.UserAssetUsdt);
        Assert.Equal(0.00632655m, balance.UserTotalInterestBtc);
        Assert.Equal(9497, preview.OrderId);
        Assert.Equal(GateEarnDualOptionType.Call, preview.Type);
        Assert.Equal(0.99486528m, preview.SettlementPrincipal);
        Assert.Single(recommendations);
        Assert.Equal(72m, recommendations[0].InvestHours);
    }

    [Fact]
    public void Documented_staking_responses_deserialize()
    {
        var coins = JsonFixture.Deserialize<List<GateEarnStakingCoin>>("Docs/Earn/staking_coins.success.json");
        var swap = JsonFixture.Deserialize<GateEarnStakingSwap>("Docs/Earn/staking_swap.success.json");
        var orders = JsonFixture.Deserialize<GateEarnStakingOrderPage>("Docs/Earn/staking_orders.success.json");
        var awards = JsonFixture.Deserialize<GateEarnStakingAwardPage>("Docs/Earn/staking_awards.success.json");
        var assets = JsonFixture.Deserialize<List<GateEarnStakingAsset>>("Docs/Earn/staking_assets.success.json");

        Assert.Single(coins);
        Assert.Equal(64, coins[0].ProductId);
        Assert.Equal(0.038m, coins[0].EstimateApr);
        Assert.Equal(0.01m, coins[0].ExtraInterest[0].SegmentInterest[0].MoneyRate);
        Assert.Equal(123456, swap.Id);
        Assert.Equal(10m, swap.ExchangeAmount);
        Assert.Equal(90, orders.TotalCount);
        Assert.Equal(1m, orders.List[0].ExchangeAmount);
        Assert.Equal(33, awards.TotalCount);
        Assert.Equal(0.00000191m, awards.List[0].Interest);
        Assert.Single(assets);
        Assert.Equal(0.00000762m, assets[0].DefiIncome.Total[0].Amount);
        Assert.Equal("COMP", assets[0].RewardCoins[1].RewardCoin);
    }

    [Fact]
    public void Documented_auto_invest_responses_deserialize()
    {
        var created = JsonFixture.Deserialize<GateEarnAutoInvestPlanCreated>("Docs/Earn/autoinvest_plan_created.success.json");
        var coins = JsonFixture.Deserialize<List<GateEarnAutoInvestCoin>>("Docs/Earn/autoinvest_coins.success.json");
        var minimum = JsonFixture.Deserialize<GateEarnAutoInvestMinimumAmount>("Docs/Earn/autoinvest_min_amount.success.json");
        var records = JsonFixture.Deserialize<GateEarnAutoInvestExecutionRecordPage>("Docs/Earn/autoinvest_records.success.json");
        var orders = JsonFixture.Deserialize<List<GateEarnAutoInvestOrder>>("Docs/Earn/autoinvest_orders.success.json");
        var config = JsonFixture.Deserialize<List<GateEarnAutoInvestConfig>>("Docs/Earn/autoinvest_config.success.json");
        var plan = JsonFixture.Deserialize<GateEarnAutoInvestPlan>("Docs/Earn/autoinvest_plan.success.json");
        var plans = JsonFixture.Deserialize<GateEarnAutoInvestPlanPage>("Docs/Earn/autoinvest_plans.success.json");

        Assert.Equal(142583, created.Id);
        Assert.Equal(GateEarnAutoInvestPeriodType.Monthly, created.PeriodType);
        Assert.Equal(GateEarnAutoInvestFundFlow.AutoInvest, created.FundFlow);
        Assert.Single(coins);
        Assert.Equal("BTC", coins[0].Key);
        Assert.Equal(10m, minimum.MinAmount);
        Assert.Equal(1770805384904919, records.List[0].Id);
        Assert.Equal(100m, records.List[0].Amount);
        Assert.Equal(87.93m, orders[0].Price);
        Assert.Equal(100000m, config[1].MaxLimit);
        Assert.Equal(100m, plan.Portfolio[0].Ratio);
        Assert.Equal(1, plans.TotalCount);
    }

    [Fact]
    public void Documented_fixed_term_responses_deserialize()
    {
        var products = JsonFixture.Parse("Docs/Earn/fixed_term_products.success.json")["data"]!.ToObject<GateEarnFixedTermProductPage>()!;
        var productsByAsset = JsonFixture.Parse("Docs/Earn/fixed_term_products_by_asset.success.json")["data"]!.ToObject<GateEarnFixedTermProductSimpleList>()!;
        var lends = JsonFixture.Parse("Docs/Earn/fixed_term_lends.success.json")["data"]!.ToObject<GateEarnFixedTermLendPage>()!;
        var lendResult = JsonFixture.Parse("Docs/Earn/fixed_term_lend_result.success.json")["data"]!.ToObject<GateEarnFixedTermLendResult>()!;
        var history = JsonFixture.Parse("Docs/Earn/fixed_term_history.success.json")["data"]!.ToObject<GateEarnFixedTermHistoryPage>()!;

        Assert.Equal(12, products.Total);
        Assert.Equal(11, products.List[0].Id);
        Assert.Equal(0.03m, products.List[0].YearRate);
        Assert.Single(productsByAsset.List);
        Assert.Equal(0.0084m, productsByAsset.List[0].YearRate);
        Assert.Equal(5862476630, lends.List[0].OrderId);
        Assert.Equal(0.00019178m, lends.List[0].Interest);
        Assert.NotEqual(default, lends.List[0].CreateAt);
        Assert.Equal(0, lendResult.OrderId);
        Assert.Equal(5862476630, history.List[0].OrderId);
        Assert.Equal(1m, history.List[0].TotalPrincipal);
    }

    [Fact]
    public void Captured_live_public_earn_responses_deserialize()
    {
        var dualPlans = JsonFixture.Deserialize<List<GateEarnDualPlan>>("Live/Earn/dual_investment_plan.BTC.json");
        var products = JsonFixture.Parse("Live/Earn/fixed_term_product.page1.limit1.json")["data"]!.ToObject<GateEarnFixedTermProductPage>()!;
        var productsByAsset = JsonFixture.Parse("Live/Earn/fixed_term_product.USDT.list.json")["data"]!.ToObject<GateEarnFixedTermProductSimpleList>()!;

        Assert.NotEmpty(dualPlans);
        Assert.Contains(dualPlans, x => x.InvestCurrency == "BTC" || x.ExerciseCurrency == "BTC");
        Assert.All(dualPlans, x => Assert.True(x.Id > 0));
        Assert.NotEmpty(products.List);
        Assert.Equal("USDT", products.List[0].Asset);
        Assert.True(products.List[0].MinLendAmount > 0m);
        Assert.NotEmpty(productsByAsset.List);
        Assert.All(productsByAsset.List, x => Assert.Equal("USDT", x.Asset));
    }
}
