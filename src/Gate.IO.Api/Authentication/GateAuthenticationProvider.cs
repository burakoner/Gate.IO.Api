namespace Gate.IO.Api.Authentication;

internal class GateAuthenticationProvider(ApiCredentials credentials) : AuthenticationProvider(credentials)
{
    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        if (!signed) return;

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
        var signbody = new StringBuilder();
        signbody.Append(method.ToString().ToUpper() + "\n");
        signbody.Append(uri.AbsolutePath + "\n");
        signbody.Append(HttpUtility.UrlDecode(uri.Query.TrimStart('?')) + "\n");
        signbody.Append(SignSHA512(bodyContent, SignatureOutputType.Hex).ToLower() + "\n");
        signbody.Append(timestamp);
        var signature = SignHMACSHA512(signbody.ToString()).ToLower();
        headers.Add("SIGN", signature);

        // Broker Id
        headers.Add("X-Gate-Channel-Id", GateConstants.Default.ChannelId);
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
