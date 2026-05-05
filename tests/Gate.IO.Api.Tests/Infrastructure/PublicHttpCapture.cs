namespace Gate.IO.Api.Tests.Infrastructure;

internal static class PublicHttpCapture
{
    public static async Task<string> GetStringAsync(string url, CancellationToken ct = default)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Gate.IO.Api.Tests/1.0");
        return await client.GetStringAsync(url, ct).ConfigureAwait(false);
    }
}
