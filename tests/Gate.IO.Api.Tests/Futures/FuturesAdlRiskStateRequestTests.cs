using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Unit")]
public class FuturesAdlRiskStateRequestTests
{
    [Theory]
    [InlineData(GateFuturesSettlement.BTC, "btc", false)]
    [InlineData(GateFuturesSettlement.BTC, "btc", true)]
    [InlineData(GateFuturesSettlement.USDT, "usdt", false)]
    [InlineData(GateFuturesSettlement.USDT, "usdt", true)]
    [InlineData(GateFuturesSettlement.USD1, "usd1", false)]
    [InlineData(GateFuturesSettlement.USD1, "usd1", true)]
    public async Task Market_adl_risk_query_uses_the_selected_public_route_without_filters_or_authentication(
        GateFuturesSettlement settlement, string wireSettlement, bool credentials)
    {
        var body = JObject.Parse(JsonFixture.Read("Docs/Futures/adl_risk_states.success.json"));
        body["settle"] = wireSettlement;
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(body.ToString()));
        using var client = CreateClient(handler);
        if (credentials)
            client.SetApiCredentials("key", "secret");

        var result = await client.Futures[settlement].GetAdlRiskStatesAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(wireSettlement, result.Data!.Settlement);
        Assert.Equal(3, result.Data.States.Count);
        Assert.Equal("normal", result.Data.States["BTC_USDT"].State);
        Assert.Equal("warning", result.Data.States["GT_USDT"].State);
        Assert.Equal("adl_risk", result.Data.States["HYPE_USDT"].State);
        Assert.Equal(1786695185345L, result.Data.States["BTC_USDT"].CalculatedAtInMilliseconds);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal($"/api/v4/futures/{wireSettlement}/adl_risk_states", request.RequestUri.AbsolutePath);
        Assert.Empty(request.RequestUri.Query);
        Assert.Empty(request.Content);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
        Assert.DoesNotContain("Timestamp", request.Headers.Keys);
    }

    [Fact]
    public async Task Returned_adl_settlement_is_not_overwritten_with_the_selected_client()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/adl_risk_states.success.json")));
        using var client = CreateClient(handler);

        var result = await client.Futures.USD1.GetAdlRiskStatesAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("usdt", result.Data!.Settlement);
        Assert.Equal("/api/v4/futures/usd1/adl_risk_states", Assert.Single(handler.Requests).RequestUri.AbsolutePath);
    }

    [Fact]
    public async Task Empty_adl_mapping_remains_successful_without_synthesizing_market_evidence()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"settle\":\"usd1\",\"states\":{}}"));
        using var client = CreateClient(handler);

        var result = await client.Futures.USD1.GetAdlRiskStatesAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Empty(result.Data!.States);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"settle\":\"usd1\",\"states\":{\"BTC_USD1\":{\"state\":\"normal\"}}}")]
    [InlineData("{\"settle\":\"usd1\",\"states\":{\"BTC_USD1\":{\"state\":null,\"calculated_at_ms\":0}}}")]
    public async Task Malformed_adl_success_payloads_do_not_become_successful_risk_snapshots(string json)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(json));
        using var client = CreateClient(handler);

        var result = await client.Futures.USD1.GetAdlRiskStatesAsync();

        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task Adl_http_error_is_preserved_without_retry_or_a_default_normal_state()
    {
        var handler = new RecordingHttpMessageHandler(_ => new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
        {
            Content = new StringContent("{\"label\":\"INVALID_ARGUMENT\",\"message\":\"invalid argument\"}", Encoding.UTF8, "application/json"),
        });
        using var client = CreateClient(handler);

        var result = await client.Futures.USD1.GetAdlRiskStatesAsync();

        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Equal("invalid argument", result.Error.Message);
        Assert.Contains("INVALID_ARGUMENT", result.Error.ToString());
        Assert.Single(handler.Requests);
    }

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
        => new(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });

    private static HttpResponseMessage JsonResponse(string json)
        => new(System.Net.HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
}
