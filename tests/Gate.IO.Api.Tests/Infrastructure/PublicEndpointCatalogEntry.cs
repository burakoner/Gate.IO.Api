namespace Gate.IO.Api.Tests.Infrastructure;

internal sealed record PublicEndpointCatalogEntry(
    string Module,
    string Name,
    string Method,
    string PathAndQuery,
    string DocumentationUrl,
    string? LiveFixturePath,
    bool HasClientSmokeTest,
    string? RequestBodyJson = null,
    string? CapturePathAndQuery = null)
{
    public string Url => $"{PublicEndpointCatalog.RestBaseUrl}{PathAndQuery}";

    public string CapturePath => CapturePathAndQuery ?? PathAndQuery;

    public string CaptureUrl => $"{PublicEndpointCatalog.RestBaseUrl}{CapturePath}";

    public bool HasCommittedLiveFixture => !string.IsNullOrWhiteSpace(LiveFixturePath);

    public bool CanCapture
        => HasCommittedLiveFixture
        && !CapturePath.Contains("{", StringComparison.Ordinal)
        && (Method == "GET" || !string.IsNullOrWhiteSpace(RequestBodyJson));
}
