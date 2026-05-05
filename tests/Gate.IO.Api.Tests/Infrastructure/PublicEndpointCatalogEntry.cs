namespace Gate.IO.Api.Tests.Infrastructure;

internal sealed record PublicEndpointCatalogEntry(
    string Module,
    string Name,
    string Method,
    string PathAndQuery,
    string DocumentationUrl,
    string? LiveFixturePath,
    bool HasClientSmokeTest)
{
    public string Url => $"{PublicEndpointCatalog.RestBaseUrl}{PathAndQuery}";

    public bool HasCommittedLiveFixture => !string.IsNullOrWhiteSpace(LiveFixturePath);
}
