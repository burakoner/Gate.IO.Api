namespace Gate.IO.Api.Base;

internal sealed class GateRequestFactory : ApiSharp.Interfaces.IRequestFactory
{
    private HttpClient httpClient;

    public void Configure(HttpOptions options, ProxyCredentials proxy, HttpClient client = null)
    {
        if (client != null)
        {
            httpClient = client;
            return;
        }

        var handler = new HttpClientHandler
        {
            Proxy = proxy == null ? null : new System.Net.WebProxy
            {
                Address = new Uri($"{proxy.Host}:{proxy.Port}"),
                Credentials = proxy.Password == null ? null : new System.Net.NetworkCredential(proxy.Username.GetString(), proxy.Password.GetString()),
            },
        };

        httpClient = new HttpClient(handler)
        {
            Timeout = options.RequestTimeout,
        };
        httpClient.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public ApiSharp.Interfaces.IRequest Create(HttpMethod method, Uri uri, int requestId)
    {
        if (httpClient == null)
            throw new InvalidOperationException("Cannot create a request before configuring the HTTP client");

        return new GateRequest(new HttpRequestMessage(method, uri), httpClient, requestId);
    }
}

internal sealed class GateRequest : ApiSharp.Interfaces.IRequest
{
    private readonly HttpRequestMessage request;
    private readonly HttpClient httpClient;

    public GateRequest(HttpRequestMessage request, HttpClient httpClient, int requestId)
    {
        this.request = request;
        this.httpClient = httpClient;
        RequestId = requestId;
    }

    public string Accept
    {
        set => request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(value));
    }

    public string Content { get; private set; } = string.Empty;

    public HttpMethod Method
    {
        get => request.Method;
        set => request.Method = value;
    }

    public Uri Uri => request.RequestUri;

    public int RequestId { get; }

    public void SetContent(byte[] data)
        => request.Content = new ByteArrayContent(data);

    public void SetContent(string data, string contentType)
    {
        if (!contentType.StartsWith("multipart/form-data;", StringComparison.OrdinalIgnoreCase))
        {
            Content = data;
            request.Content = new StringContent(data, Encoding.UTF8, contentType);
            return;
        }

        Content = "[multipart/form-data content omitted]";
        request.Content = new StringContent(data, Encoding.UTF8);
        request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
    }

    public void AddHeader(string key, string value)
        => request.Headers.TryAddWithoutValidation(key, value);

    public Dictionary<string, IEnumerable<string>> GetHeaders()
        => request.Headers.ToDictionary(x => x.Key, x => x.Value);

    public async Task<ApiSharp.Interfaces.IResponse> GetResponseAsync(CancellationToken cancellationToken)
        => new GateResponse(await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false));
}

internal sealed class GateResponse : ApiSharp.Interfaces.IResponse
{
    private readonly HttpResponseMessage response;

    public GateResponse(HttpResponseMessage response)
        => this.response = response;

    public System.Net.HttpStatusCode StatusCode => response.StatusCode;

    public bool IsSuccessStatusCode => response.IsSuccessStatusCode;

    public IEnumerable<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders => response.Headers;

    public Task<System.IO.Stream> GetResponseStreamAsync()
        => response.Content.ReadAsStreamAsync();

    public void Close()
        => response.Dispose();
}
