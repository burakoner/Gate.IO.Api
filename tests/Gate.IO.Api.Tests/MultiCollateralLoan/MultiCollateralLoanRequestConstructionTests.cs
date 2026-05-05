using Gate.IO.Api.MultiCollateralLoan;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.MultiCollateralLoan;

[Trait("Category", "Unit")]
public class MultiCollateralLoanRequestConstructionTests
{
    [Fact]
    public async Task Public_multi_collateral_requests_serialize_without_authentication_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/MultiCollateralLoan/currencies.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/ltv.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/fixed_rate.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/current_rate.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);

        var currencies = await client.MultiCollateralLoan.GetCurrenciesAsync();
        var ltv = await client.MultiCollateralLoan.GetLtvAsync();
        var fixedRates = await client.MultiCollateralLoan.GetFixedRatesAsync();
        var currentRates = await client.MultiCollateralLoan.GetCurrentRatesAsync(new GateMultiCollateralLoanCurrentRateRequest
        {
            Currencies = ["BTC", "GT"],
            VipLevel = "1",
        });

        Assert.True(currencies.Success, currencies.Error?.ToString());
        Assert.True(ltv.Success, ltv.Error?.ToString());
        Assert.True(fixedRates.Success, fixedRates.Error?.ToString());
        Assert.True(currentRates.Success, currentRates.Error?.ToString());
        Assert.Equal("/api/v4/loan/multi_collateral/currencies", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/loan/multi_collateral/ltv", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/loan/multi_collateral/fixed_rate", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/loan/multi_collateral/current_rate", handler.Requests[3].RequestUri.AbsolutePath);
        var currentRateQuery = ParseQuery(handler.Requests[3].RequestUri);
        Assert.Equal("BTC,GT", currentRateQuery["currencies"]);
        Assert.Equal("1", currentRateQuery["vip_level"]);
        Assert.All(handler.Requests, AssertNoAuthHeaders);
    }

    [Fact]
    public async Task Signed_order_requests_serialize_query_path_and_body()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/MultiCollateralLoan/orders.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/order_id.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/order.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var orders = await client.MultiCollateralLoan.GetOrdersAsync(new GateMultiCollateralLoanOrderQueryRequest
        {
            Page = 2,
            Limit = 50,
            Sort = GateMultiCollateralLoanOrderSort.LtvDescending,
            OrderType = GateMultiCollateralLoanOrderType.Fixed,
        });
        var placed = await client.MultiCollateralLoan.PlaceOrderAsync(new GateMultiCollateralLoanOrderRequest
        {
            OrderId = 1721387470,
            OrderType = GateMultiCollateralLoanOrderType.Fixed,
            FixedType = GateMultiCollateralLoanFixedType.SevenDays,
            FixedRate = 0.00001m,
            AutoRenew = true,
            AutoRepay = true,
            BorrowCurrency = "BTC",
            BorrowAmount = 1m,
            CollateralCurrencies =
            [
                new GateMultiCollateralLoanCurrencyAmount { Currency = "USDT", Amount = 1000m },
            ],
        });
        var order = await client.MultiCollateralLoan.GetOrderAsync(10005578);

        Assert.True(orders.Success, orders.Error?.ToString());
        Assert.True(placed.Success, placed.Error?.ToString());
        Assert.True(order.Success, order.Error?.ToString());
        Assert.Equal(3, handler.Requests.Count);

        var orderQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/loan/multi_collateral/orders", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("2", orderQuery["page"]);
        Assert.Equal("50", orderQuery["limit"]);
        Assert.Equal("ltv_desc", orderQuery["sort"]);
        Assert.Equal("fixed", orderQuery["order_type"]);

        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        var body = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("1721387470", body["order_id"]!.ToString());
        Assert.Equal("fixed", body["order_type"]!.ToString());
        Assert.Equal("7d", body["fixed_type"]!.ToString());
        Assert.Equal("0.00001", body["fixed_rate"]!.ToString());
        Assert.True(body["auto_renew"]!.Value<bool>());
        Assert.True(body["auto_repay"]!.Value<bool>());
        Assert.Equal("BTC", body["borrow_currency"]!.ToString());
        Assert.Equal("1", body["borrow_amount"]!.ToString());
        Assert.Equal("USDT", body["collateral_currencies"]![0]!["currency"]!.ToString());
        Assert.Equal("1000", body["collateral_currencies"]![0]!["amount"]!.ToString());

        Assert.Equal(HttpMethod.Get, handler.Requests[2].Method);
        Assert.Equal("/api/v4/loan/multi_collateral/orders/10005578", handler.Requests[2].RequestUri.AbsolutePath);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
    }

    [Fact]
    public async Task Signed_repay_collateral_and_quota_requests_serialize_filters_and_bodies()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/MultiCollateralLoan/repayment_records.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/repayment_result.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/collateral_records.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/collateral_adjustment.success.json"),
            JsonFixture.Read("Docs/MultiCollateralLoan/currency_quota.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var from = DateTimeOffset.FromUnixTimeSeconds(1702990000).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1703053927).UtcDateTime;
        var repaymentRecords = await client.MultiCollateralLoan.GetRepaymentRecordsAsync(new GateMultiCollateralLoanRepaymentRecordQueryRequest
        {
            Type = GateMultiCollateralLoanRepaymentType.Repay,
            BorrowCurrency = "BAT",
            Page = 1,
            Limit = 20,
            From = from,
            To = to,
        });
        var repayment = await client.MultiCollateralLoan.RepayAsync(new GateMultiCollateralLoanRepayRequest
        {
            OrderId = 10005578,
            RepayItems =
            [
                new GateMultiCollateralLoanRepayItem { Currency = "btc", Amount = 1m, RepaidAll = false },
            ],
        });
        var collateralRecords = await client.MultiCollateralLoan.GetCollateralRecordsAsync(new GateMultiCollateralLoanCollateralRecordQueryRequest
        {
            Page = 2,
            Limit = 30,
            From = from,
            To = to,
            CollateralCurrency = "BTC",
        });
        var collateral = await client.MultiCollateralLoan.AdjustCollateralAsync(new GateMultiCollateralLoanCollateralAdjustRequest
        {
            OrderId = 10005578,
            Type = GateMultiCollateralLoanCollateralOperationType.Append,
            Collaterals =
            [
                new GateMultiCollateralLoanCurrencyAmount { Currency = "btc", Amount = 0.5m },
            ],
        });
        var quota = await client.MultiCollateralLoan.GetCurrencyQuotasAsync(new GateMultiCollateralLoanCurrencyQuotaRequest
        {
            Type = GateMultiCollateralLoanCurrencyQuotaType.Collateral,
            Currencies = ["BTC", "USDT"],
        });

        Assert.True(repaymentRecords.Success, repaymentRecords.Error?.ToString());
        Assert.True(repayment.Success, repayment.Error?.ToString());
        Assert.True(collateralRecords.Success, collateralRecords.Error?.ToString());
        Assert.True(collateral.Success, collateral.Error?.ToString());
        Assert.True(quota.Success, quota.Error?.ToString());
        Assert.Equal(5, handler.Requests.Count);

        var repayRecordQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("/api/v4/loan/multi_collateral/repay", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("repay", repayRecordQuery["type"]);
        Assert.Equal("BAT", repayRecordQuery["borrow_currency"]);
        Assert.Equal("1702990000", repayRecordQuery["from"]);
        Assert.Equal("1703053927", repayRecordQuery["to"]);

        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        var repayBody = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("10005578", repayBody["order_id"]!.ToString());
        Assert.Equal("btc", repayBody["repay_items"]![0]!["currency"]!.ToString());
        Assert.Equal("1", repayBody["repay_items"]![0]!["amount"]!.ToString());
        Assert.False(repayBody["repay_items"]![0]!["repaid_all"]!.Value<bool>());

        var collateralRecordQuery = ParseQuery(handler.Requests[2].RequestUri);
        Assert.Equal("/api/v4/loan/multi_collateral/mortgage", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("BTC", collateralRecordQuery["collateral_currency"]);
        Assert.Equal("2", collateralRecordQuery["page"]);
        Assert.Equal("30", collateralRecordQuery["limit"]);

        Assert.Equal(HttpMethod.Post, handler.Requests[3].Method);
        var collateralBody = JObject.Parse(handler.Requests[3].Content);
        Assert.Equal("append", collateralBody["type"]!.ToString());
        Assert.Equal("btc", collateralBody["collaterals"]![0]!["currency"]!.ToString());
        Assert.Equal("0.5", collateralBody["collaterals"]![0]!["amount"]!.ToString());

        var quotaQuery = ParseQuery(handler.Requests[4].RequestUri);
        Assert.Equal("/api/v4/loan/multi_collateral/currency_quota", handler.Requests[4].RequestUri.AbsolutePath);
        Assert.Equal("collateral", quotaQuery["type"]);
        Assert.Equal("BTC,USDT", quotaQuery["currency"]);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
        AssertSignedHeaders(handler.Requests[3]);
        AssertSignedHeaders(handler.Requests[4]);
    }

    [Fact]
    public async Task Request_validation_rejects_invalid_multi_collateral_inputs()
    {
        var client = new GateRestApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() => client.MultiCollateralLoan.PlaceOrderAsync(new GateMultiCollateralLoanOrderRequest
        {
            OrderType = GateMultiCollateralLoanOrderType.Fixed,
            BorrowCurrency = "BTC",
            BorrowAmount = 1m,
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.MultiCollateralLoan.RepayAsync(new GateMultiCollateralLoanRepayRequest
        {
            OrderId = 10005578,
            RepayItems = [],
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.MultiCollateralLoan.AdjustCollateralAsync(new GateMultiCollateralLoanCollateralAdjustRequest
        {
            OrderId = 10005578,
            Type = GateMultiCollateralLoanCollateralOperationType.Append,
            Collaterals = [],
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.MultiCollateralLoan.GetCurrencyQuotasAsync(new GateMultiCollateralLoanCurrencyQuotaRequest
        {
            Type = GateMultiCollateralLoanCurrencyQuotaType.Borrow,
            Currencies = ["BTC", "USDT"],
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.MultiCollateralLoan.GetCurrentRatesAsync(new GateMultiCollateralLoanCurrentRateRequest
        {
            Currencies = Enumerable.Range(0, 101).Select(x => $"C{x}"),
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
