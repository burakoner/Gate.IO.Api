namespace Gate.IO.Api.Tests.Infrastructure;

[Trait("Category", "Unit")]
public class PublicEndpointCatalogTests
{
    [Fact]
    public void Public_endpoint_catalog_entries_are_safe_and_unique()
    {
        Assert.NotEmpty(PublicEndpointCatalog.Entries);

        var privateModules = new[] { "Account", "Bot", "Otc", "P2p", "Rebate", "SubAccount" };
        foreach (var entry in PublicEndpointCatalog.Entries)
        {
            Assert.DoesNotContain(entry.Module, privateModules);
            Assert.Contains(entry.Method, ["GET", "POST"]);
            Assert.StartsWith("/", entry.PathAndQuery);
            Assert.StartsWith(PublicEndpointCatalog.RestBaseUrl, entry.Url);
            Assert.StartsWith("https://www.gate.com/docs/developers/", entry.DocumentationUrl);
        }

        var duplicates = PublicEndpointCatalog.Entries
            .GroupBy(x => $"{x.Method} {x.PathAndQuery}", StringComparer.Ordinal)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key)
            .ToArray();

        Assert.Empty(duplicates);
    }

    [Fact]
    public void Public_endpoint_catalog_committed_capture_paths_exist()
    {
        var entriesWithFixtures = PublicEndpointCatalog.Entries.Where(x => x.HasCommittedLiveFixture).ToArray();

        Assert.NotEmpty(entriesWithFixtures);
        foreach (var entry in entriesWithFixtures)
        {
            var fixturePath = $"Live/{entry.LiveFixturePath}";
            var token = JsonFixture.Parse(fixturePath);

            Assert.NotEqual(JTokenType.None, token.Type);
        }
    }

    [Fact]
    public void Public_endpoint_catalog_tracks_live_smoke_modules()
    {
        var expectedModules = new[]
        {
            "Alpha",
            "CrossEx",
            "Delivery",
            "Earn",
            "EarnUni",
            "FlashSwap",
            "Futures",
            "Margin",
            "MultiCollateralLoan",
            "Options",
            "Spot",
            "TradFi",
            "Unified",
            "Wallet",
        };

        Assert.Equal(expectedModules, PublicEndpointCatalog.ModulesWithClientSmokeTests);
    }
}
