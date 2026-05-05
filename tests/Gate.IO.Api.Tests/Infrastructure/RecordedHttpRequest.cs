namespace Gate.IO.Api.Tests.Infrastructure;

internal sealed class RecordedHttpRequest
{
    public HttpMethod Method { get; init; } = HttpMethod.Get;

    public Uri RequestUri { get; init; } = new("https://api.gateio.ws/");

    public IReadOnlyDictionary<string, string[]> Headers { get; init; } = new Dictionary<string, string[]>();

    public string Content { get; init; } = string.Empty;
}
