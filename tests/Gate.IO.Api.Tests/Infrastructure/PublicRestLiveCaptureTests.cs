namespace Gate.IO.Api.Tests.Infrastructure;

[Trait("Category", "LiveCapture")]
public class PublicRestLiveCaptureTests
{
    [Fact]
    public async Task Public_rest_catalog_entries_are_captured_when_enabled()
    {
        if (!LiveCaptureSettings.Enabled)
            return;

        var entries = PublicEndpointCatalog.Entries.Where(x => x.CanCapture).ToArray();

        Assert.NotEmpty(entries);
        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

        foreach (var entry in entries)
        {
            var fixturePath = await PublicHttpCapture.CaptureAndWriteFixtureAsync(entry, cts.Token);
            var token = JToken.Parse(await File.ReadAllTextAsync(fixturePath, cts.Token));

            Assert.NotEqual(JTokenType.None, token.Type);
        }
    }
}
