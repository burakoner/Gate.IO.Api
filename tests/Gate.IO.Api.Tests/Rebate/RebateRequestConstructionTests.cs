using Gate.IO.Api.Rebate;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Rebate;

[Trait("Category", "Unit")]
public class RebateRequestConstructionTests
{
    [Fact]
    public async Task Agency_history_requests_serialize_signed_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Rebate/agency_transaction_history.success.json"),
            JsonFixture.Read("Docs/Rebate/agency_commission_history.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var from = DateTimeOffset.FromUnixTimeSeconds(1689220000).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1689227647).UtcDateTime;
        var transactions = await client.Rebate.GetAgencyTransactionHistoryAsync(new GateRebateTransactionHistoryRequest
        {
            From = from,
            To = to,
            Symbol = "GT_USDT",
            UserId = 10002,
            Limit = 10,
            Offset = 1,
        });
        var commissions = await client.Rebate.GetAgencyCommissionHistoryAsync(new GateRebateCommissionHistoryRequest
        {
            From = from,
            To = to,
            Currency = "USDT",
            UserId = 10002,
            Limit = 20,
            Offset = 2,
            CommissionType = GateRebateCommissionType.Direct,
        });

        Assert.True(transactions.Success, transactions.Error?.ToString());
        Assert.True(commissions.Success, commissions.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);
        Assert.Equal("/api/v4/rebate/agency/transaction_history", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("GT_USDT", ParseQuery(handler.Requests[0].RequestUri)["currency_pair"]);
        Assert.Equal("1689220000", ParseQuery(handler.Requests[0].RequestUri)["from"]);
        Assert.Equal("1689227647", ParseQuery(handler.Requests[0].RequestUri)["to"]);
        Assert.Equal("10002", ParseQuery(handler.Requests[0].RequestUri)["user_id"]);
        Assert.Equal("10", ParseQuery(handler.Requests[0].RequestUri)["limit"]);
        Assert.Equal("1", ParseQuery(handler.Requests[0].RequestUri)["offset"]);
        Assert.Equal("/api/v4/rebate/agency/commission_history", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("USDT", ParseQuery(handler.Requests[1].RequestUri)["currency"]);
        Assert.Equal("1", ParseQuery(handler.Requests[1].RequestUri)["commission_type"]);
        Assert.Equal("20", ParseQuery(handler.Requests[1].RequestUri)["limit"]);
        Assert.Equal("2", ParseQuery(handler.Requests[1].RequestUri)["offset"]);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
    }

    [Fact]
    public async Task Partner_history_and_sub_list_requests_serialize_signed_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Rebate/partner_transaction_history.success.json"),
            JsonFixture.Read("Docs/Rebate/partner_commission_history.success.json"),
            JsonFixture.Read("Docs/Rebate/partner_sub_list.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var from = DateTimeOffset.FromUnixTimeSeconds(1746000000).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1746008350).UtcDateTime;
        var transactions = await client.Rebate.GetPartnerTransactionHistoryAsync(new GateRebateTransactionHistoryRequest
        {
            From = from,
            To = to,
            Symbol = "GT_USDT",
            UserId = 123456789,
            Limit = 15,
            Offset = 0,
        });
        var commissions = await client.Rebate.GetPartnerCommissionHistoryAsync(new GateRebateCommissionHistoryRequest
        {
            From = from,
            To = to,
            Currency = "USDT",
            UserId = 123456789,
            Limit = 52,
            Offset = 4,
        });
        var subList = await client.Rebate.GetPartnerSubListAsync(new GateRebatePartnerSubListRequest
        {
            UserId = 123456789,
            Limit = 3,
            Offset = 1,
        });

        Assert.True(transactions.Success, transactions.Error?.ToString());
        Assert.True(commissions.Success, commissions.Error?.ToString());
        Assert.True(subList.Success, subList.Error?.ToString());
        Assert.Equal("/api/v4/rebate/partner/transaction_history", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("GT_USDT", ParseQuery(handler.Requests[0].RequestUri)["currency_pair"]);
        Assert.Equal("/api/v4/rebate/partner/commission_history", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("USDT", ParseQuery(handler.Requests[1].RequestUri)["currency"]);
        Assert.Equal("/api/v4/rebate/partner/sub_list", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("3", ParseQuery(handler.Requests[2].RequestUri)["limit"]);
        Assert.Equal("1", ParseQuery(handler.Requests[2].RequestUri)["offset"]);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
    }

    [Fact]
    public async Task Broker_and_user_requests_serialize_signed_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Rebate/broker_commission_history.success.json"),
            JsonFixture.Read("Docs/Rebate/broker_transaction_history.success.json"),
            JsonFixture.Read("Docs/Rebate/user_info.success.json"),
            JsonFixture.Read("Docs/Rebate/user_sub_relation.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var from = DateTimeOffset.FromUnixTimeSeconds(1743470000).UtcDateTime;
        var to = DateTimeOffset.FromUnixTimeSeconds(1743475200).UtcDateTime;
        var brokerCommissions = await client.Rebate.GetBrokerCommissionHistoryAsync(new GateRebateBrokerHistoryRequest
        {
            From = from,
            To = to,
            UserId = 10002,
            Limit = 50,
            Offset = 5,
        });
        var brokerTransactions = await client.Rebate.GetBrokerTransactionHistoryAsync(new GateRebateBrokerHistoryRequest
        {
            From = from,
            To = to,
            UserId = 10002,
            Limit = 60,
            Offset = 6,
        });
        var userInfo = await client.Rebate.GetUserInfoAsync();
        var relation = await client.Rebate.GetUserSubRelationAsync(new GateRebateUserSubRelationRequest
        {
            UserIds = Enumerable.Range(1, 105).Select(x => (long)x),
        });

        Assert.True(brokerCommissions.Success, brokerCommissions.Error?.ToString());
        Assert.True(brokerTransactions.Success, brokerTransactions.Error?.ToString());
        Assert.True(userInfo.Success, userInfo.Error?.ToString());
        Assert.True(relation.Success, relation.Error?.ToString());
        Assert.Equal("/api/v4/rebate/broker/commission_history", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("50", ParseQuery(handler.Requests[0].RequestUri)["limit"]);
        Assert.Equal("5", ParseQuery(handler.Requests[0].RequestUri)["offset"]);
        Assert.Equal("/api/v4/rebate/broker/transaction_history", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("60", ParseQuery(handler.Requests[1].RequestUri)["limit"]);
        Assert.Equal("6", ParseQuery(handler.Requests[1].RequestUri)["offset"]);
        Assert.Equal("/api/v4/rebate/user/info", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/rebate/user/sub_relation", handler.Requests[3].RequestUri.AbsolutePath);
        var userIds = ParseQuery(handler.Requests[3].RequestUri)["user_id_list"].Split(',');
        Assert.Equal(100, userIds.Length);
        Assert.Equal("1", userIds[0]);
        Assert.Equal("100", userIds[99]);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
        AssertSignedHeaders(handler.Requests[3]);
    }

    [Fact]
    public async Task Wrapped_partner_requests_unwrap_data_and_serialize_aggregate_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Rebate/partner_application.recent.success.json"),
            JsonFixture.Read("Docs/Rebate/partner_eligibility.success.json"),
            JsonFixture.Read("Docs/Rebate/partner_aggregated_data.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var application = await client.Rebate.GetRecentPartnerApplicationAsync();
        var eligibility = await client.Rebate.CheckPartnerEligibilityAsync();
        var aggregated = await client.Rebate.GetPartnerAggregatedDataAsync(new GateRebatePartnerAggregatedDataRequest
        {
            StartDate = "2025-01-01 00:00:00",
            EndDate = "2025-01-31 23:59:59",
            BusinessType = GateRebateBusinessType.Spot,
        });

        Assert.True(application.Success, application.Error?.ToString());
        Assert.True(eligibility.Success, eligibility.Error?.ToString());
        Assert.True(aggregated.Success, aggregated.Error?.ToString());
        Assert.Equal(100, application.Data.Id);
        Assert.False(eligibility.Data.Eligible);
        Assert.Equal(123.45m, aggregated.Data.RebateAmount);
        Assert.Equal("/api/v4/rebate/partner/applications/recent", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/rebate/partner/eligibility", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/rebate/partner/data/aggregated", handler.Requests[2].RequestUri.AbsolutePath);
        var query = ParseQuery(handler.Requests[2].RequestUri);
        Assert.Equal("2025-01-01 00:00:00", query["start_date"]);
        Assert.Equal("2025-01-31 23:59:59", query["end_date"]);
        Assert.Equal("1", query["business_type"]);
        AssertSignedHeaders(handler.Requests[0]);
        AssertSignedHeaders(handler.Requests[1]);
        AssertSignedHeaders(handler.Requests[2]);
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
                x => DecodeQueryPart(x[0]),
                x => x.Length == 1 ? string.Empty : DecodeQueryPart(x[1]));
    }

    private static string DecodeQueryPart(string value)
        => Uri.UnescapeDataString(value.Replace("+", " "));

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}
