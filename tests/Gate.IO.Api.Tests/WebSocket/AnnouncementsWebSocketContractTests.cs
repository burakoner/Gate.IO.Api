using Gate.IO.Api.Announcements;
using Gate.IO.Api.Base;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.WebSocket;

[Trait("Category", "Contract")]
public class AnnouncementsWebSocketContractTests
{
    private const string FixtureRoot = "Docs/WebSocket/Announcements";

    [Fact]
    public void Announcements_subscription_requests_and_success_response_deserialize()
    {
        var listingRequest = JsonFixture.Deserialize<GateAnnouncementStreamRequest>(
            $"{FixtureRoot}/request.subscribe_listing_en.json");
        var listingPayload = Assert.IsType<JArray>(listingRequest.Payload);

        Assert.Equal(1659956033, listingRequest.Timestamp);
        Assert.Equal("announcement.summary_listing", listingRequest.Channel);
        Assert.Equal("subscribe", listingRequest.Event);
        Assert.Equal(["en"], listingPayload.Values<string>().ToArray());

        var engineUpgradeRequest = JsonFixture.Deserialize<GateAnnouncementStreamRequest>(
            $"{FixtureRoot}/request.subscribe_engine_upgrade_multilang.json");
        var engineUpgradePayload = Assert.IsType<JArray>(engineUpgradeRequest.Payload);

        Assert.Equal("announcement.summary_engine_upgrade", engineUpgradeRequest.Channel);
        Assert.Equal("subscribe", engineUpgradeRequest.Event);
        Assert.Equal(["cn", "en"], engineUpgradePayload.Values<string>().ToArray());

        var response = JsonFixture.Deserialize<GateStreamResponse<GateStreamStatus>>(
            $"{FixtureRoot}/response.subscribe_success.json");

        Assert.Equal("announcement.summary_listing", response.Channel);
        Assert.Equal(StreamResponseEvent.Subscribe, response.Event);
        Assert.Equal("success", response.Data.Status);
        Assert.Equal(1690365913056, response.TimeInMlliseconds);
    }

    [Fact]
    public void Announcements_summary_updates_deserialize()
    {
        var listing = JsonFixture.Deserialize<GateStreamResponse<GateAnnouncementStreamSummary>>(
            $"{FixtureRoot}/announcement.summary_listing.update.json");

        Assert.Equal("announcement.summary_listing", listing.Channel);
        Assert.Equal(StreamResponseEvent.Update, listing.Event);
        Assert.Equal(GateAnnouncementLanguage.English, listing.Data.Language);
        Assert.Equal("https://www.gate.com/article/228103", listing.Data.OriginUrl);
        Assert.Contains("Worldcoin", listing.Data.Title);
        Assert.Contains("WLD/USDT", listing.Data.Brief);
        Assert.Equal(2023, listing.Data.PublishedTime.Year);

        var precision = JsonFixture.Deserialize<GateStreamResponse<GateAnnouncementStreamSummary>>(
            $"{FixtureRoot}/announcement.summary_precision.update.cn.json");

        Assert.Equal("announcement.summary_precision", precision.Channel);
        Assert.Equal(GateAnnouncementLanguage.Chinese, precision.Data.Language);
        Assert.Equal("https://www.gate.com/article/precision-update", precision.Data.OriginUrl);
        Assert.Contains("精度", precision.Data.Title);
        Assert.Equal(2024, precision.Data.PublishedTime.Year);
    }

    [Fact]
    public void Announcements_stream_request_serialization_maps_payloads()
    {
        var request = new GateAnnouncementStreamRequest
        {
            Timestamp = 1722844800,
            Channel = "announcement.summary_deposit_withdrawal",
            Event = "subscribe",
            Payload = new[] { "cn", "en" },
        };
        var json = JObject.Parse(JsonConvert.SerializeObject(request));

        Assert.Equal(1722844800, json["time"]!.Value<long>());
        Assert.Equal("announcement.summary_deposit_withdrawal", json["channel"]!.ToString());
        Assert.Equal("subscribe", json["event"]!.ToString());
        Assert.Equal(["cn", "en"], json["payload"]!.Values<string>().ToArray());
    }

    [Fact]
    public async Task Announcements_subscription_requires_at_least_one_language()
    {
        var client = new GateWebSocketClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Announcements.SubscribeToListingAnnouncementsAsync([], _ => { }));
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.Announcements.SubscribeToListingAnnouncementsAsync(null!, _ => { }));
    }
}
