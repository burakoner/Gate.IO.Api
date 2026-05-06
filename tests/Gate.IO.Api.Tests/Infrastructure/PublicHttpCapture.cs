using System.Text;

namespace Gate.IO.Api.Tests.Infrastructure;

internal static class PublicHttpCapture
{
    public static async Task<string> GetStringAsync(string url, CancellationToken ct = default)
        => await GetStringAsync(HttpMethod.Get, url, null, ct).ConfigureAwait(false);

    public static async Task<string> GetStringAsync(
        HttpMethod method,
        string url,
        string? requestBodyJson,
        CancellationToken ct = default)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Gate.IO.Api.Tests/1.0");

        using var request = new HttpRequestMessage(method, url);
        if (!string.IsNullOrWhiteSpace(requestBodyJson))
            request.Content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

        using var response = await client.SendAsync(request, ct).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"Public capture request failed with HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {content}");

        return content;
    }

    public static async Task<string> CaptureStringAsync(PublicEndpointCatalogEntry entry, CancellationToken ct = default)
    {
        if (!entry.CanCapture)
            throw new InvalidOperationException($"{entry.Module} {entry.Name} is not configured for live capture.");

        var method = new HttpMethod(entry.Method);
        try
        {
            return await GetStringAsync(method, entry.CaptureUrl, entry.RequestBodyJson, ct).ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            throw new HttpRequestException(
                $"Failed to capture {entry.Module} {entry.Name} from {entry.Method} {entry.CaptureUrl}.",
                exception);
        }
    }

    public static async Task<string> CaptureAndWriteFixtureAsync(PublicEndpointCatalogEntry entry, CancellationToken ct = default)
    {
        var rawJson = await CaptureStringAsync(entry, ct).ConfigureAwait(false);
        var normalizedJson = JToken.Parse(rawJson).ToString(Formatting.Indented) + Environment.NewLine;
        var fixturePath = LiveCaptureSettings.GetFixturePath(entry);

        Directory.CreateDirectory(Path.GetDirectoryName(fixturePath)!);
        await File.WriteAllTextAsync(fixturePath, normalizedJson, ct).ConfigureAwait(false);

        return fixturePath;
    }
}
