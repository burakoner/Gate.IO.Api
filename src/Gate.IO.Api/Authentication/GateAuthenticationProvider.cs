namespace Gate.IO.Api.Authentication;

internal class GateAuthenticationProvider : AuthenticationProvider
{
    public GateAuthenticationProvider(ApiCredentials credentials) : base(credentials)
    {
    }

    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        if (!signed) return;

        // Set Uri Parameters
        uri = uri.SetParameters(query, serialization);

        // Key
        headers.Add("KEY", Credentials.Key!.GetString());

        // Timestamp
        var timestamp = GetTimestamp(apiClient).ConvertToSeconds().ToString();
        headers.Add("Timestamp", timestamp);

        // Signature
        var signbody = new StringBuilder();
        signbody.Append(method.ToString().ToUpper() + "\n");
        signbody.Append(uri.AbsolutePath + "\n");
        signbody.Append(HttpUtility.UrlDecode(uri.Query.TrimStart('?')) + "\n");
        signbody.Append(SignSHA512(bodyContent, SignatureOutputType.Hex).ToLower() + "\n");
        signbody.Append(timestamp);
        var signature = SignHMACSHA512(signbody.ToString()).ToLower();
        headers.Add("SIGN", signature);

        // Broker Id
        headers.Add("X-Gate-Channel-Id", ((GateRestApiClientOptions)apiClient.ClientOptions).BrokerId);
    }

    public override void AuthenticateTcpSocketApi()
    {
        throw new NotImplementedException();
    }

    public override void AuthenticateWebSocketApi()
    {
        throw new NotImplementedException();
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
}
