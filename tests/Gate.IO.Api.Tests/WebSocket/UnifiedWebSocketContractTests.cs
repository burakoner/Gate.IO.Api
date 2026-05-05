using Gate.IO.Api.Base;
using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.Unified;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class UnifiedWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/Unified";

    [Fact]
    public void Unified_subscription_requests_and_success_responses_deserialize()
    {
        var assetsRequest = JsonFixture.Deserialize<GateStreamRequest>(
            $"{FixtureRoot}/request.subscribe_unified_assets.json");
        var assetsPayload = Assert.IsType<JArray>(assetsRequest.Payload);

        Assert.Equal(5001, assetsRequest.Id);
        Assert.Equal(1700625194, assetsRequest.Timestamp);
        Assert.Equal("unified.assets", assetsRequest.Channel);
        Assert.Equal(StreamRequestEvent.Subscribe, assetsRequest.Event);
        Assert.Empty(assetsPayload);
        Assert.Equal("api_key", assetsRequest.Auth.Method);
        Assert.Equal("xxxx", assetsRequest.Auth.ApiKey);
        Assert.Equal("xxxx", assetsRequest.Auth.Signature);

        var assetDetailRequest = JsonFixture.Deserialize<GateStreamRequest>(
            $"{FixtureRoot}/request.subscribe_unified_asset_detail.json");
        var detailPayload = Assert.IsType<JArray>(assetDetailRequest.Payload);

        Assert.Equal(5002, assetDetailRequest.Id);
        Assert.Equal("unified.asset_detail", assetDetailRequest.Channel);
        Assert.Equal(["BTC", "ETH"], detailPayload.Values<string>().ToArray());
        Assert.Equal("api_key", assetDetailRequest.Auth.Method);

        var assetsResponse = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.assets_subscribe_success.json");
        Assert.Equal(5001, assetsResponse.Id);
        Assert.Equal("unified.assets", assetsResponse.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, assetsResponse.Event);
        Assert.Null(assetsResponse.Error);
        Assert.Equal("success", assetsResponse.Data.Status);

        var detailResponse = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.asset_detail_subscribe_success.json");
        Assert.Equal(5002, detailResponse.Id);
        Assert.Equal("unified.asset_detail", detailResponse.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, detailResponse.Event);
        Assert.Null(detailResponse.Error);
        Assert.Equal("success", detailResponse.Data.Status);
    }

    [Fact]
    public void Unified_assets_stream_update_deserializes()
    {
        var response = JsonFixture.Deserialize<GateStreamResponse<GateUnifiedStreamAssets>>(
            $"{FixtureRoot}/unified.assets.update.json");
        var assets = response.Data;

        Assert.Equal("unified.assets", response.Channel);
        Assert.Equal(StreamResponseEvent.Update, response.Event);
        Assert.Equal(9008, assets.UserId);
        Assert.Equal(18.56m, assets.TotalInitialMarginRate);
        Assert.Equal(20.10m, assets.TotalMaintenanceMarginRate);
        Assert.Equal(-1005719.51m, assets.TotalMarginBalance);
        Assert.Equal(-617985.29m, assets.UnifiedMarginTotalEquity);
        Assert.Equal(1293939.74m, assets.UnifiedMarginTotalLiabilities);
        Assert.Equal(675222.27m, assets.UnifiedMarginTotal);
        Assert.Equal(-1432719.62m, assets.TotalAvailableMargin);
    }

    [Fact]
    public void Unified_asset_detail_stream_update_deserializes()
    {
        var response = JsonFixture.Deserialize<GateStreamResponse<GateUnifiedStreamAssetDetail>>(
            $"{FixtureRoot}/unified.asset_detail.update.json");
        var detail = response.Data;

        Assert.Equal("unified.asset_detail", response.Channel);
        Assert.Equal(StreamResponseEvent.Update, response.Event);
        Assert.Equal(11027732, detail.UserId);
        Assert.Equal(2, detail.Details.Count);

        var btc = detail.Details["BTC"];
        Assert.Equal(1086390.949548m, btc.Available);
        Assert.Equal(0m, btc.Freeze);
        Assert.Equal(1086390.949548m, btc.Equity);
        Assert.Equal(0m, btc.TotalLiabilities);
        Assert.Equal(1086390.949548m, btc.Balance);
        Assert.Null(btc.CrossBalance);

        var usdt = detail.Details["USDT"];
        Assert.Equal(8724.23263378m, usdt.Available);
        Assert.Equal(8724.23263378m, usdt.CrossBalance);
        Assert.Equal(8724.23263378m, usdt.MarginBalance);
        Assert.Equal(0m, usdt.InitialMargin);
        Assert.Equal(9999.99m, usdt.InitialMarginRate);
        Assert.Equal(0m, usdt.MaintenanceMargin);
        Assert.Equal(9999.99m, usdt.MaintenanceMarginRate);
        Assert.Equal(8724.23263378m, usdt.AvailableMargin);
        Assert.Equal(8724.23263378m, usdt.IsolatedAvailableMargin);
    }

    [Fact]
    public void Unified_stream_request_serialization_maps_payloads_and_auth()
    {
        var assetsRequest = new GateStreamRequest
        {
            Id = 82,
            Timestamp = 1716796362,
            Channel = "unified.assets",
            Event = StreamRequestEvent.Subscribe,
            Payload = Array.Empty<string>(),
            Auth = new StreamRequestAuth
            {
                ApiKey = "key",
                Signature = "signature",
            },
        };
        var assetsJson = JObject.Parse(JsonConvert.SerializeObject(assetsRequest));

        Assert.Equal("subscribe", assetsJson["event"]!.ToString());
        Assert.Equal("unified.assets", assetsJson["channel"]!.ToString());
        Assert.Empty(assetsJson["payload"]!.Values<string>());
        Assert.Equal("api_key", assetsJson["auth"]!["method"]!.ToString());
        Assert.Equal("key", assetsJson["auth"]!["KEY"]!.ToString());
        Assert.Equal("signature", assetsJson["auth"]!["SIGN"]!.ToString());

        var detailRequest = new GateStreamRequest
        {
            Id = 83,
            Timestamp = 1716796362,
            Channel = "unified.asset_detail",
            Event = StreamRequestEvent.Subscribe,
            Payload = new[] { "!all" },
            Auth = new StreamRequestAuth
            {
                ApiKey = "key",
                Signature = "signature",
            },
        };
        var detailJson = JObject.Parse(JsonConvert.SerializeObject(detailRequest));

        Assert.Equal("unified.asset_detail", detailJson["channel"]!.ToString());
        Assert.Equal(["!all"], detailJson["payload"]!.Values<string>().ToArray());
        Assert.Equal("signature", detailJson["auth"]!["SIGN"]!.ToString());
    }

    [Fact]
    public async Task Unified_asset_detail_subscription_rejects_mixed_all_marker()
    {
        var client = new GateWebSocketClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Unified.SubscribeToAssetDetailsAsync(["!all", "BTC"], _ => { }));
    }
}
