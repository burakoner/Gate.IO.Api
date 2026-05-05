using Gate.IO.Api.EarnUni;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.EarnUni;

[Trait("Category", "Unit")]
public class EarnUniRequestConstructionTests
{
    [Fact]
    public async Task Public_currency_requests_do_not_use_authentication_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/EarnUni/currencies.success.json"),
            JsonFixture.Read("Docs/EarnUni/currency.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);

        var currencies = await client.EarnUni.GetCurrenciesAsync();
        var currency = await client.EarnUni.GetCurrencyAsync("AE");

        Assert.True(currencies.Success, currencies.Error?.ToString());
        Assert.True(currency.Success, currency.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);
        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/earn/uni/currencies", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/earn/uni/currencies/AE", handler.Requests[1].RequestUri.AbsolutePath);
        AssertNoAuthHeaders(handler.Requests[0]);
        AssertNoAuthHeaders(handler.Requests[1]);
    }

    [Fact]
    public async Task Signed_lending_order_requests_serialize_query_and_body_parameters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/EarnUni/lends.success.json"),
            "{}",
            "{}",
            "{}",
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var lends = await client.EarnUni.GetLendsAsync(new GateEarnUniLendQueryRequest
        {
            Currency = "BTC",
            Page = 2,
            Limit = 10,
        });
        var lend = await client.EarnUni.CreateLendAsync(new GateEarnUniLendRequest
        {
            Currency = "AE",
            Amount = 100m,
            Type = GateEarnUniLendOperationType.Lend,
            MinimumRate = 0.00001m,
        });
        var redeem = await client.EarnUni.CreateLendAsync(new GateEarnUniLendRequest
        {
            Currency = "AE",
            Amount = 50m,
            Type = GateEarnUniLendOperationType.Redeem,
        });
        var update = await client.EarnUni.UpdateLendAsync(new GateEarnUniLendUpdateRequest
        {
            Currency = "AE",
            MinimumRate = 0.0001m,
        });

        Assert.True(lends.Success, lends.Error?.ToString());
        Assert.True(lend.Success, lend.Error?.ToString());
        Assert.True(redeem.Success, redeem.Error?.ToString());
        Assert.True(update.Success, update.Error?.ToString());
        Assert.Equal(4, handler.Requests.Count);

        var query = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("/api/v4/earn/uni/lends", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("BTC", query["currency"]);
        Assert.Equal("2", query["page"]);
        Assert.Equal("10", query["limit"]);

        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        var lendBody = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("AE", lendBody["currency"]!.ToString());
        Assert.Equal("100", lendBody["amount"]!.ToString());
        Assert.Equal("lend", lendBody["type"]!.ToString());
        Assert.Equal("0.00001", lendBody["min_rate"]!.ToString());

        var redeemBody = JObject.Parse(handler.Requests[2].Content);
        Assert.Equal("redeem", redeemBody["type"]!.ToString());
        Assert.Equal("50", redeemBody["amount"]!.ToString());
        Assert.Null(redeemBody["min_rate"]);

        Assert.Equal("PATCH", handler.Requests[3].Method.Method);
        var patchBody = JObject.Parse(handler.Requests[3].Content);
        Assert.Equal("AE", patchBody["currency"]!.ToString());
        Assert.Equal("0.0001", patchBody["min_rate"]!.ToString());
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
        AssertSignedHeaders(handler.Requests[3]);
    }

    [Fact]
    public async Task Signed_history_interest_chart_and_rate_requests_serialize_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/EarnUni/lend_records.success.json"),
            JsonFixture.Read("Docs/EarnUni/lend_interest.success.json"),
            JsonFixture.Read("Docs/EarnUni/interest_records.success.json"),
            JsonFixture.Read("Docs/EarnUni/interest_status.success.json"),
            JsonFixture.Read("Docs/EarnUni/chart.success.json"),
            JsonFixture.Read("Docs/EarnUni/estimated_rates.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var from = DateTimeOffset.FromUnixTimeSeconds(1719763200).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1722441600).UtcDateTime;
        var lendRecords = await client.EarnUni.GetLendRecordsAsync(new GateEarnUniLendRecordQueryRequest
        {
            Currency = "BTC",
            From = from,
            To = to,
            Type = GateEarnUniLendOperationType.Lend,
            Page = 3,
            Limit = 20,
        });
        var interest = await client.EarnUni.GetInterestAsync("AE");
        var interestRecords = await client.EarnUni.GetInterestRecordsAsync(new GateEarnUniInterestRecordQueryRequest
        {
            Currency = "AE",
            From = from,
            To = to,
            Page = 4,
            Limit = 30,
        });
        var interestStatus = await client.EarnUni.GetInterestStatusAsync("BTC");
        var chart = await client.EarnUni.GetChartAsync(new GateEarnUniChartQueryRequest
        {
            Asset = "BTC",
            From = from,
            To = to,
        });
        var rates = await client.EarnUni.GetEstimatedRatesAsync();

        Assert.True(lendRecords.Success, lendRecords.Error?.ToString());
        Assert.True(interest.Success, interest.Error?.ToString());
        Assert.True(interestRecords.Success, interestRecords.Error?.ToString());
        Assert.True(interestStatus.Success, interestStatus.Error?.ToString());
        Assert.True(chart.Success, chart.Error?.ToString());
        Assert.True(rates.Success, rates.Error?.ToString());
        Assert.Equal(6, handler.Requests.Count);

        var lendRecordQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("/api/v4/earn/uni/lend_records", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("BTC", lendRecordQuery["currency"]);
        Assert.Equal("1719763200", lendRecordQuery["from"]);
        Assert.Equal("1722441600", lendRecordQuery["to"]);
        Assert.Equal("lend", lendRecordQuery["type"]);
        Assert.Equal("3", lendRecordQuery["page"]);
        Assert.Equal("20", lendRecordQuery["limit"]);

        Assert.Equal("/api/v4/earn/uni/interests/AE", handler.Requests[1].RequestUri.AbsolutePath);

        var interestRecordQuery = ParseQuery(handler.Requests[2].RequestUri);
        Assert.Equal("/api/v4/earn/uni/interest_records", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("AE", interestRecordQuery["currency"]);
        Assert.Equal("4", interestRecordQuery["page"]);
        Assert.Equal("30", interestRecordQuery["limit"]);

        Assert.Equal("/api/v4/earn/uni/interest_status/BTC", handler.Requests[3].RequestUri.AbsolutePath);

        var chartQuery = ParseQuery(handler.Requests[4].RequestUri);
        Assert.Equal("/api/v4/earn/uni/chart", handler.Requests[4].RequestUri.AbsolutePath);
        Assert.Equal("BTC", chartQuery["asset"]);
        Assert.Equal("1719763200", chartQuery["from"]);
        Assert.Equal("1722441600", chartQuery["to"]);

        Assert.Equal("/api/v4/earn/uni/rate", handler.Requests[5].RequestUri.AbsolutePath);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
        AssertSignedHeaders(handler.Requests[3]);
        AssertSignedHeaders(handler.Requests[4]);
        AssertSignedHeaders(handler.Requests[5]);
    }

    [Fact]
    public async Task Lend_operation_requires_minimum_rate()
    {
        var client = new GateRestApiClient();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.EarnUni.CreateLendAsync(new GateEarnUniLendRequest
        {
            Currency = "AE",
            Amount = 100m,
            Type = GateEarnUniLendOperationType.Lend,
        }));

        Assert.Equal("MinimumRate", exception.ParamName);
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
