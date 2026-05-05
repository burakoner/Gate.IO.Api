namespace Gate.IO.Api;

internal class GateAuthentication(ApiCredentials credentials) : AuthenticationProvider(credentials ?? new ApiCredentials("", ""))
{
    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        if (!signed) return;

        // Check Point
        if (Credentials is null || Credentials.Key is null || Credentials.Secret is null || string.IsNullOrEmpty(Credentials.Key.GetString()))
            throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        // Set Uri Parameters
        uri = uri.SetParameters(query, serialization);

        // Key
        headers.Add("KEY", Credentials.Key!.GetString());

        // Timestamp
        var time = GetTimestamp(apiClient);
        var timestamp = time.ConvertToSeconds().ToString();
        headers.Add("Timestamp", timestamp);

        // Receive Window
        var window = ((GateRestApiClientOptions)apiClient.ClientOptions).ReceiveWindow;
        if (window != null)
        {
            headers.Add("x-gate-exptime", time.Add(window.Value).ConvertToMilliseconds().ToString());
        }

        // Signature
        var queryString = HttpUtility.UrlDecode(uri.Query.TrimStart('?'));
        var signature = CreateRestSignature(method, uri.AbsolutePath, queryString, bodyContent, timestamp);
        headers.Add("SIGN", signature);

        // Broker Id
        headers.Add("X-Gate-Channel-Id", GateConstants.Default.ChannelId);
    }

    internal string CreateRestSignature(HttpMethod method, string path, string queryString, string bodyContent, string timestamp)
    {
        var signbody = new StringBuilder();
        signbody.Append(method.ToString().ToUpper() + "\n");
        signbody.Append(path + "\n");
        signbody.Append((queryString ?? string.Empty) + "\n");
        signbody.Append(SignSHA512(bodyContent ?? string.Empty, SignatureOutputType.Hex).ToLower() + "\n");
        signbody.Append(timestamp);
        return SignHMACSHA512(signbody.ToString()).ToLower();
    }

    public void AuthenticateStreamRequest(GateStreamRequest request)
    {
        var eventName = JsonConvert.SerializeObject(request.Event, new StreamRequestEventConverter(false));
        var signatureBody = $"channel={request.Channel}&event={eventName}&time={request.Timestamp}";
        var signature = SignHMACSHA512(signatureBody).ToLower();
        request.Auth = new StreamRequestAuth
        {
            Method = "api_key",
            ApiKey = Credentials.Key!.GetString(),
            Signature = signature
        };
    }

    public GateCrossExStreamLoginPayload CreateCrossExLoginPayload(long timestamp)
    {
        if (Credentials is null || Credentials.Key is null || Credentials.Secret is null || string.IsNullOrEmpty(Credentials.Key.GetString()))
            throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        var signatureBody = $"channel=&event=login&time={timestamp}";
        var signature = SignHMACSHA512(signatureBody).ToLower();
        return new GateCrossExStreamLoginPayload
        {
            ApiKey = Credentials.Key!.GetString(),
            Sign = signature
        };
    }
}
