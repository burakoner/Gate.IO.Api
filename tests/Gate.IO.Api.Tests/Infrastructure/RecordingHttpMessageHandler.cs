namespace Gate.IO.Api.Tests.Infrastructure;

internal sealed class RecordingHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<RecordedHttpRequest, HttpResponseMessage> responseFactory;

    public RecordingHttpMessageHandler(Func<RecordedHttpRequest, HttpResponseMessage> responseFactory)
    {
        this.responseFactory = responseFactory;
    }

    public List<RecordedHttpRequest> Requests { get; } = [];

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var content = request.Content == null
            ? string.Empty
            : await request.Content.ReadAsStringAsync(cancellationToken);

        var headers = request.Headers
            .Concat(request.Content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>())
            .ToDictionary(x => x.Key, x => x.Value.ToArray(), StringComparer.OrdinalIgnoreCase);

        var recorded = new RecordedHttpRequest
        {
            Method = request.Method,
            RequestUri = request.RequestUri!,
            Headers = headers,
            Content = content,
        };

        Requests.Add(recorded);

        return responseFactory(recorded);
    }
}
