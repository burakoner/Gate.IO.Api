using Gate.IO.Api.Earn;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Earn;

[Trait("Category", "Unit")]
public class EarnRequestConstructionTests
{
    [Fact]
    public async Task Public_dual_and_fixed_term_requests_serialize_without_authentication_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Earn/dual_plans.success.json"),
            JsonFixture.Read("Docs/Earn/fixed_term_products.success.json"),
            JsonFixture.Read("Docs/Earn/fixed_term_products_by_asset.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var dual = await client.Earn.GetDualInvestmentPlansAsync(new GateEarnDualPlanQueryRequest
        {
            PlanId = 272,
            Coin = "BTC",
            Type = GateEarnDualOptionType.Call,
            QuoteCurrency = "USDT",
            Sort = GateEarnDualPlanSort.ShortPeriod,
            Page = 2,
            PageSize = 50,
        });
        var products = await client.Earn.GetFixedTermProductsAsync(new GateEarnFixedTermProductQueryRequest
        {
            Asset = "USDT",
            Type = GateEarnFixedTermProductType.Regular,
            Page = 3,
            Limit = 25,
        });
        var productsByAsset = await client.Earn.GetFixedTermProductsByAssetAsync(new GateEarnFixedTermProductByAssetRequest
        {
            Asset = "USDT",
            Type = GateEarnFixedTermProductType.Vip,
        });

        Assert.True(dual.Success, dual.Error?.ToString());
        Assert.True(products.Success, products.Error?.ToString());
        Assert.True(productsByAsset.Success, productsByAsset.Error?.ToString());
        Assert.Equal(3, handler.Requests.Count);

        var dualQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/earn/dual/investment_plan", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("272", dualQuery["plan_id"]);
        Assert.Equal("BTC", dualQuery["coin"]);
        Assert.Equal("call", dualQuery["type"]);
        Assert.Equal("USDT", dualQuery["quote_currency"]);
        Assert.Equal("short-period", dualQuery["sort"]);
        Assert.Equal("2", dualQuery["page"]);
        Assert.Equal("50", dualQuery["page_size"]);

        var productQuery = ParseQuery(handler.Requests[1].RequestUri);
        Assert.Equal("/api/v4/earn/fixed-term/product", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("USDT", productQuery["asset"]);
        Assert.Equal("1", productQuery["type"]);
        Assert.Equal("3", productQuery["page"]);
        Assert.Equal("25", productQuery["limit"]);

        var byAssetQuery = ParseQuery(handler.Requests[2].RequestUri);
        Assert.Equal("/api/v4/earn/fixed-term/product/USDT/list", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("2", byAssetQuery["type"]);
        Assert.All(handler.Requests, AssertNoAuthHeaders);
    }

    [Fact]
    public async Task Signed_dual_and_staking_requests_serialize_filters_and_bodies()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Earn/dual_orders.success.json"),
            JsonFixture.Read("Docs/Earn/dual_order.success.json"),
            JsonFixture.Read("Docs/Earn/dual_balance.success.json"),
            JsonFixture.Read("Docs/Earn/dual_refund_preview.success.json"),
            "{}",
            "{}",
            JsonFixture.Read("Docs/Earn/dual_recommendations.success.json"),
            JsonFixture.Read("Docs/Earn/staking_coins.success.json"),
            JsonFixture.Read("Docs/Earn/staking_swap.success.json"),
            JsonFixture.Read("Docs/Earn/staking_orders.success.json"),
            JsonFixture.Read("Docs/Earn/staking_awards.success.json"),
            JsonFixture.Read("Docs/Earn/staking_assets.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var from = DateTimeOffset.FromUnixTimeSeconds(1697680000).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1697690000).UtcDateTime;

        var orders = await client.Earn.GetDualInvestmentOrdersAsync(new GateEarnDualOrderQueryRequest
        {
            From = from,
            To = to,
            Type = GateEarnDualOptionType.Put,
            Status = GateEarnDualOrderQueryStatus.All,
            Coin = "BTC",
            Page = 2,
            Limit = 20,
        });
        var placed = await client.Earn.PlaceDualInvestmentOrderAsync(new GateEarnDualOrderRequest
        {
            PlanId = 176,
            Amount = 1m,
            Text = "t-custom-text",
        });
        var balance = await client.Earn.GetDualInvestmentBalanceAsync();
        var preview = await client.Earn.GetDualInvestmentRefundPreviewAsync(9497);
        var refund = await client.Earn.RefundDualInvestmentOrderAsync(new GateEarnDualRefundRequest
        {
            OrderId = 9497,
            RequestId = "tIvdY7nh",
        });
        var reinvest = await client.Earn.UpdateDualInvestmentReinvestAsync(new GateEarnDualReinvestUpdateRequest
        {
            OrderId = 9497,
            Status = 1,
            EffectiveTimeDuration = 86400,
        });
        var recommendations = await client.Earn.GetDualInvestmentRecommendationsAsync(new GateEarnDualRecommendationRequest
        {
            Mode = GateEarnDualRecommendationMode.ApyAscending,
            Coin = "BTC",
            Type = GateEarnDualOptionType.Call,
            HistoryProductIds = [1, 2],
        });
        var coins = await client.Earn.GetStakingCoinsAsync(GateEarnStakingCoinType.Lock);
        var swap = await client.Earn.SwapStakingCoinAsync(new GateEarnStakingSwapRequest
        {
            Coin = "USDT",
            Side = GateEarnStakingOperationType.Stake,
            Amount = 10m,
            ProductId = 64,
        });
        var stakingOrders = await client.Earn.GetStakingOrdersAsync(new GateEarnStakingOrderQueryRequest
        {
            ProductId = 64,
            Coin = "USDT",
            Type = GateEarnStakingOperationType.Redeem,
            Page = 3,
        });
        var awards = await client.Earn.GetStakingAwardsAsync(new GateEarnStakingAwardQueryRequest
        {
            ProductId = 64,
            Coin = "USDT",
            Page = 4,
        });
        var assets = await client.Earn.GetStakingAssetsAsync("USDT");

        Assert.True(orders.Success, orders.Error?.ToString());
        Assert.True(placed.Success, placed.Error?.ToString());
        Assert.True(balance.Success, balance.Error?.ToString());
        Assert.True(preview.Success, preview.Error?.ToString());
        Assert.True(refund.Success, refund.Error?.ToString());
        Assert.True(reinvest.Success, reinvest.Error?.ToString());
        Assert.True(recommendations.Success, recommendations.Error?.ToString());
        Assert.True(coins.Success, coins.Error?.ToString());
        Assert.True(swap.Success, swap.Error?.ToString());
        Assert.True(stakingOrders.Success, stakingOrders.Error?.ToString());
        Assert.True(awards.Success, awards.Error?.ToString());
        Assert.True(assets.Success, assets.Error?.ToString());
        Assert.Equal(12, handler.Requests.Count);

        var orderQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("/api/v4/earn/dual/orders", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("1697680000", orderQuery["from"]);
        Assert.Equal("1697690000", orderQuery["to"]);
        Assert.Equal("put", orderQuery["type"]);
        Assert.Equal("ALL", orderQuery["status"]);
        Assert.Equal("BTC", orderQuery["coin"]);
        Assert.Equal("2", orderQuery["page"]);
        Assert.Equal("20", orderQuery["limit"]);

        var placedBody = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("/api/v4/earn/dual/orders", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("176", placedBody["plan_id"]!.ToString());
        Assert.Equal("1", placedBody["amount"]!.ToString());
        Assert.Equal("t-custom-text", placedBody["text"]!.ToString());
        Assert.Equal("/api/v4/earn/dual/balance", handler.Requests[2].RequestUri.AbsolutePath);

        var previewQuery = ParseQuery(handler.Requests[3].RequestUri);
        Assert.Equal("/api/v4/earn/dual/order-refund-preview", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.Equal("9497", previewQuery["order_id"]);

        var refundBody = JObject.Parse(handler.Requests[4].Content);
        Assert.Equal("/api/v4/earn/dual/order-refund", handler.Requests[4].RequestUri.AbsolutePath);
        Assert.Equal("9497", refundBody["order_id"]!.ToString());
        Assert.Equal("tIvdY7nh", refundBody["req_id"]!.ToString());

        var reinvestBody = JObject.Parse(handler.Requests[5].Content);
        Assert.Equal("9497", reinvestBody["order_id"]!.ToString());
        Assert.Equal("1", reinvestBody["status"]!.ToString());
        Assert.Equal("86400", reinvestBody["effective_time_duration"]!.ToString());

        var recommendQuery = ParseQuery(handler.Requests[6].RequestUri);
        Assert.Equal("/api/v4/earn/dual/project-recommend", handler.Requests[6].RequestUri.AbsolutePath);
        Assert.Equal("apy_up", recommendQuery["mode"]);
        Assert.Equal("BTC", recommendQuery["coin"]);
        Assert.Equal("call", recommendQuery["type"]);
        Assert.Equal("1,2", recommendQuery["history_pids"]);

        var coinQuery = ParseQuery(handler.Requests[7].RequestUri);
        Assert.Equal("/api/v4/earn/staking/coins", handler.Requests[7].RequestUri.AbsolutePath);
        Assert.Equal("lock", coinQuery["cointype"]);

        var swapBody = JObject.Parse(handler.Requests[8].Content);
        Assert.Equal("/api/v4/earn/staking/swap", handler.Requests[8].RequestUri.AbsolutePath);
        Assert.Equal("USDT", swapBody["coin"]!.ToString());
        Assert.Equal("0", swapBody["side"]!.ToString());
        Assert.Equal("10", swapBody["amount"]!.ToString());
        Assert.Equal("64", swapBody["pid"]!.ToString());

        var stakingOrderQuery = ParseQuery(handler.Requests[9].RequestUri);
        Assert.Equal("/api/v4/earn/staking/order_list", handler.Requests[9].RequestUri.AbsolutePath);
        Assert.Equal("64", stakingOrderQuery["pid"]);
        Assert.Equal("USDT", stakingOrderQuery["coin"]);
        Assert.Equal("1", stakingOrderQuery["type"]);
        Assert.Equal("3", stakingOrderQuery["page"]);

        var awardQuery = ParseQuery(handler.Requests[10].RequestUri);
        Assert.Equal("/api/v4/earn/staking/award_list", handler.Requests[10].RequestUri.AbsolutePath);
        Assert.Equal("64", awardQuery["pid"]);
        Assert.Equal("USDT", awardQuery["coin"]);
        Assert.Equal("4", awardQuery["page"]);

        var assetQuery = ParseQuery(handler.Requests[11].RequestUri);
        Assert.Equal("/api/v4/earn/staking/assets", handler.Requests[11].RequestUri.AbsolutePath);
        Assert.Equal("USDT", assetQuery["coin"]);
        Assert.All(handler.Requests, AssertSignedHeaders);
    }

    [Fact]
    public async Task Auto_invest_and_fixed_term_requests_serialize_filters_and_bodies()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Earn/autoinvest_plan_created.success.json"),
            "{}",
            "{}",
            "{}",
            JsonFixture.Read("Docs/Earn/autoinvest_coins.success.json"),
            JsonFixture.Read("Docs/Earn/autoinvest_min_amount.success.json"),
            JsonFixture.Read("Docs/Earn/autoinvest_records.success.json"),
            JsonFixture.Read("Docs/Earn/autoinvest_orders.success.json"),
            JsonFixture.Read("Docs/Earn/autoinvest_config.success.json"),
            JsonFixture.Read("Docs/Earn/autoinvest_plan.success.json"),
            JsonFixture.Read("Docs/Earn/autoinvest_plans.success.json"),
            JsonFixture.Read("Docs/Earn/fixed_term_lends.success.json"),
            JsonFixture.Read("Docs/Earn/fixed_term_lend_result.success.json"),
            JsonFixture.Read("Docs/Earn/fixed_term_redeem.success.json"),
            JsonFixture.Read("Docs/Earn/fixed_term_history.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var items = new[]
        {
            new GateEarnAutoInvestPortfolioItem { Asset = "BTC", Ratio = 33m },
            new GateEarnAutoInvestPortfolioItem { Asset = "GT", Ratio = 33m },
            new GateEarnAutoInvestPortfolioItem { Asset = "ETH", Ratio = 34m },
        };
        var from = DateTimeOffset.FromUnixTimeSeconds(1775606400).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1776211200).UtcDateTime;

        var created = await client.Earn.CreateAutoInvestPlanAsync(new GateEarnAutoInvestPlanCreateRequest
        {
            PlanName = "Monthly",
            PlanDescription = "BTC plan",
            PlanMoney = "USDT",
            PlanAmount = 100m,
            PeriodType = GateEarnAutoInvestPeriodType.Monthly,
            PeriodDay = 1,
            PeriodHour = 1,
            Items = items,
            FundSource = GateEarnAutoInvestFundSource.Spot,
            FundFlow = GateEarnAutoInvestFundFlow.AutoInvest,
            Type = GateEarnAutoInvestCreationType.Normal,
        });
        var update = await client.Earn.UpdateAutoInvestPlanAsync(142583, GateEarnAutoInvestFundSource.Earn, GateEarnAutoInvestFundFlow.Earn);
        var stopped = await client.Earn.StopAutoInvestPlanAsync(142583);
        var added = await client.Earn.AddAutoInvestPositionAsync(142583, 12.345m);
        var coins = await client.Earn.GetAutoInvestCoinsAsync("USDT");
        var minimum = await client.Earn.GetAutoInvestMinimumAmountAsync("USDT", items);
        var records = await client.Earn.GetAutoInvestExecutionRecordsAsync(142583, 2, 50);
        var orders = await client.Earn.GetAutoInvestOrderDetailsAsync(142583, 1770805384904919);
        var config = await client.Earn.GetAutoInvestConfigAsync();
        var detail = await client.Earn.GetAutoInvestPlanAsync(142583);
        var plans = await client.Earn.GetAutoInvestPlansAsync(GateEarnAutoInvestPlanStatus.Active, 3, 25);
        var lends = await client.Earn.GetFixedTermLendsAsync(new GateEarnFixedTermLendQueryRequest
        {
            ProductId = 476,
            OrderId = 5862476630,
            Asset = "USDT",
            OrderType = GateEarnFixedTermOrderType.Current,
            Page = 2,
            Limit = 10,
            SubBusiness = 13,
            BusinessFilter = """[{"business":1,"sub_business":0}]""",
        });
        var lend = await client.Earn.CreateFixedTermLendAsync(new GateEarnFixedTermLendRequest
        {
            ProductId = 476,
            Amount = 1m,
            YearRate = 0.01m,
            ReinvestStatus = 1,
            RedeemAccountType = 1,
            FinancialRateId = 0,
            SubBusiness = 13,
        });
        var redeemed = await client.Earn.RedeemFixedTermOrderAsync(5862476630);
        var history = await client.Earn.GetFixedTermHistoryAsync(new GateEarnFixedTermHistoryRequest
        {
            ProductId = 476,
            OrderId = 5862476630,
            Asset = "USDT",
            Type = GateEarnFixedTermHistoryType.Subscription,
            Page = 4,
            Limit = 20,
            StartAt = from,
            EndAt = to,
            SubBusiness = 13,
            BusinessFilter = """[{"business":1,"sub_business":0}]""",
        });

        Assert.True(created.Success, created.Error?.ToString());
        Assert.True(update.Success, update.Error?.ToString());
        Assert.True(stopped.Success, stopped.Error?.ToString());
        Assert.True(added.Success, added.Error?.ToString());
        Assert.True(coins.Success, coins.Error?.ToString());
        Assert.True(minimum.Success, minimum.Error?.ToString());
        Assert.True(records.Success, records.Error?.ToString());
        Assert.True(orders.Success, orders.Error?.ToString());
        Assert.True(config.Success, config.Error?.ToString());
        Assert.True(detail.Success, detail.Error?.ToString());
        Assert.True(plans.Success, plans.Error?.ToString());
        Assert.True(lends.Success, lends.Error?.ToString());
        Assert.True(lend.Success, lend.Error?.ToString());
        Assert.True(redeemed.Success, redeemed.Error?.ToString());
        Assert.True(history.Success, history.Error?.ToString());
        Assert.Equal(15, handler.Requests.Count);

        var createBody = JObject.Parse(handler.Requests[0].Content);
        Assert.Equal("/api/v4/earn/autoinvest/plans/create", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("USDT", createBody["plan_money"]!.ToString());
        Assert.Equal("100", createBody["plan_amount"]!.ToString());
        Assert.Equal("monthly", createBody["plan_period_type"]!.ToString());
        Assert.Equal("spot", createBody["fund_source"]!.ToString());
        Assert.Equal("auto_invest", createBody["fund_flow"]!.ToString());
        Assert.Equal("BTC", createBody["items"]![0]!["asset"]!.ToString());
        Assert.Equal("33", createBody["items"]![0]!["ratio"]!.ToString());

        var updateBody = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("/api/v4/earn/autoinvest/plans/update", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("142583", updateBody["plan_id"]!.ToString());
        Assert.Equal("earn", updateBody["fund_source"]!.ToString());
        Assert.Equal("earn", updateBody["fund_flow"]!.ToString());

        Assert.Equal("/api/v4/earn/autoinvest/plans/stop", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("142583", JObject.Parse(handler.Requests[2].Content)["plan_id"]!.ToString());
        Assert.Equal("/api/v4/earn/autoinvest/plans/add_position", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.Equal("12.345", JObject.Parse(handler.Requests[3].Content)["amount"]!.ToString());

        var coinQuery = ParseQuery(handler.Requests[4].RequestUri);
        Assert.Equal("/api/v4/earn/autoinvest/coins", handler.Requests[4].RequestUri.AbsolutePath);
        Assert.Equal("USDT", coinQuery["plan_money"]);

        var minimumBody = JObject.Parse(handler.Requests[5].Content);
        Assert.Equal("/api/v4/earn/autoinvest/min_invest_amount", handler.Requests[5].RequestUri.AbsolutePath);
        Assert.Equal("USDT", minimumBody["money"]!.ToString());
        Assert.Equal("ETH", minimumBody["items"]![2]!["asset"]!.ToString());

        var recordsQuery = ParseQuery(handler.Requests[6].RequestUri);
        Assert.Equal("/api/v4/earn/autoinvest/plans/records", handler.Requests[6].RequestUri.AbsolutePath);
        Assert.Equal("142583", recordsQuery["plan_id"]);
        Assert.Equal("2", recordsQuery["page"]);
        Assert.Equal("50", recordsQuery["page_size"]);

        var ordersQuery = ParseQuery(handler.Requests[7].RequestUri);
        Assert.Equal("/api/v4/earn/autoinvest/orders", handler.Requests[7].RequestUri.AbsolutePath);
        Assert.Equal("142583", ordersQuery["plan_id"]);
        Assert.Equal("1770805384904919", ordersQuery["record_id"]);
        Assert.Equal("/api/v4/earn/autoinvest/config", handler.Requests[8].RequestUri.AbsolutePath);
        Assert.Equal("142583", ParseQuery(handler.Requests[9].RequestUri)["plan_id"]);
        Assert.Equal("active", ParseQuery(handler.Requests[10].RequestUri)["status"]);

        var lendsQuery = ParseQuery(handler.Requests[11].RequestUri);
        Assert.Equal("/api/v4/earn/fixed-term/user/lend", handler.Requests[11].RequestUri.AbsolutePath);
        Assert.Equal("1", lendsQuery["order_type"]);
        Assert.Equal("476", lendsQuery["product_id"]);
        Assert.Equal("5862476630", lendsQuery["order_id"]);
        Assert.Equal("USDT", lendsQuery["asset"]);
        Assert.Equal("13", lendsQuery["sub_business"]);

        var lendBody = JObject.Parse(handler.Requests[12].Content);
        Assert.Equal("/api/v4/earn/fixed-term/user/lend", handler.Requests[12].RequestUri.AbsolutePath);
        Assert.Equal("476", lendBody["product_id"]!.ToString());
        Assert.Equal("1", lendBody["amount"]!.ToString());
        Assert.Equal("0.01", lendBody["year_rate"]!.ToString());
        Assert.Equal("1", lendBody["reinvest_status"]!.ToString());
        Assert.Equal("0", lendBody["financial_rate_id"]!.ToString());

        Assert.Equal("/api/v4/earn/fixed-term/user/pre-redeem", handler.Requests[13].RequestUri.AbsolutePath);
        Assert.Equal("5862476630", JObject.Parse(handler.Requests[13].Content)["order_id"]!.ToString());

        var historyQuery = ParseQuery(handler.Requests[14].RequestUri);
        Assert.Equal("/api/v4/earn/fixed-term/user/history", handler.Requests[14].RequestUri.AbsolutePath);
        Assert.Equal("1", historyQuery["type"]);
        Assert.Equal("1775606400", historyQuery["start_at"]);
        Assert.Equal("1776211200", historyQuery["end_at"]);
        Assert.All(handler.Requests, AssertSignedHeaders);
    }

    [Fact]
    public async Task Auto_invest_requests_reject_empty_portfolios()
    {
        var client = new GateRestApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() => client.Earn.CreateAutoInvestPlanAsync(new GateEarnAutoInvestPlanCreateRequest
        {
            PlanMoney = "USDT",
            PlanAmount = 100m,
            PeriodType = GateEarnAutoInvestPeriodType.Monthly,
            PeriodDay = 1,
            PeriodHour = 1,
            Items = [],
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Earn.GetAutoInvestMinimumAmountAsync(new GateEarnAutoInvestMinInvestAmountRequest
        {
            Money = "USDT",
            Items = [],
        }));
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

    private static Dictionary<string, string> ParseQuery(Uri uri)
    {
        return uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .ToDictionary(
                x => Uri.UnescapeDataString(x[0]),
                x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]));
    }

    private static void AssertNoAuthHeaders(RecordedHttpRequest request)
    {
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}
