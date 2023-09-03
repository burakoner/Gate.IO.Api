namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiBaseClient : WebSocketApiClient
{
    // Internal
    internal Log Log { get => this.log; }
    internal GateStreamClient RootClient { get; }

    // Options
    public new GateStreamClientOptions ClientOptions { get { return (GateStreamClientOptions)base.ClientOptions; } }

    internal StreamApiBaseClient(GateStreamClient root) : base("Gate.IO Stream", root.ClientOptions)
    {
        RootClient = root;

        RateLimitPerConnectionPerSecond = 4;
        SetDataInterpreter((data) => string.Empty, null);
        /*
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => new GateStreamRequest
        {
            Id = NextId(),
            Channel = spotPingChannel,
        });
        */
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
    {
        return new GateAuthenticationProvider(credentials);
    }

    protected override bool HandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null;

        // Ping Request
        if (request is GateStreamRequest req && req.Channel.EndsWith(".ping"))
        {
            if (data["channel"] != null && ((string)data["channel"]).EndsWith(".pong"))
            {
                callResult = new CallResult<T>(JsonConvert.DeserializeObject<T>(data.ToString()));
                return true;
            }
        }

        // Unsubscribe Request
        if (request is GateStreamRequest req2 && req2.Event == StreamRequestEvent.Unsubscribe)
        {
            return true;
        }

        return false;
    }

    protected override bool HandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
    {
        callResult = null;
        if (message.Type != JTokenType.Object)
            return false;

        var id = message["id"];
        if (id == null)
            return false;

        var bRequest = (GateStreamRequest)request;
        if ((int)id != bRequest.Id)
            return false;

        var result = message["result"];
        if (result != null && result.Type == JTokenType.Object && result["status"] != null && (string)result["status"] == "success")
        {
            Log.Write(LogLevel.Trace, $"Socket {connection.Id} Subscription completed");
            callResult = new CallResult<object>(new object());
            return true;
        }

        var error = message["error"];
        if (error == null)
        {
            callResult = new CallResult<object>(new ServerError("Unknown error: " + message));
            return true;
        }

        callResult = new CallResult<object>(new ServerError(error["code"]!.Value<int>(), error["message"]!.ToString()));

        return true;
    }

    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, object request)
    {
        if (message.Type != JTokenType.Object)
            return false;

        var bRequest = (GateStreamRequest)request;
        var channel = message["channel"];
        if (channel == null)
            return false;

        return bRequest.Channel == channel.ToString();
    }

    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, string identifier)
    {
        return true;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection connection)
    {
        throw new NotImplementedException();
    }

    protected override async Task<bool> UnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
    {
        var topics = ((GateStreamRequest)subscription.Request!).Payload;
        var unsub = new GateStreamRequest { Event = StreamRequestEvent.Unsubscribe, Payload = topics, Id = NextId() };
        var result = false;

        if (!connection.Connected)
            return true;

        await connection.SendAndWaitAsync(unsub, ClientOptions.ResponseTimeout, data =>
        {
            if (data.Type != JTokenType.Object)
                return false;

            var id = data["id"];
            if (id == null)
                return false;

            if ((int)id != unsub.Id)
                return false;

            var result = data["result"];
            if (result?.Type == JTokenType.Null)
            {
                result = true;
                return true;
            }

            return true;
        }).ConfigureAwait(false);

        return result;
    }
    #endregion

    #region Internal Methods
    internal AuthenticationProvider BaseCreateAuthenticationProvider(ApiCredentials credentials)
        => this.CreateAuthenticationProvider(credentials);

    internal bool BaseHandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T> callResult)
        => this.HandleQueryResponse<T>(connection, request, data, out callResult);

    internal bool BaseHandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        => this.HandleSubscriptionResponse(connection, subscription, request, message, out callResult);

    internal bool BaseMessageMatchesHandler(WebSocketConnection connection, JToken message, object request)
        => this.MessageMatchesHandler(connection, message, request);

    internal bool BaseMessageMatchesHandler(WebSocketConnection connection, JToken message, string identifier)
        => this.MessageMatchesHandler(connection, message, identifier);

    internal Task<CallResult<bool>> BaseAuthenticateAsync(WebSocketConnection connection)
        => this.AuthenticateAsync(connection);

    internal async Task<bool> BaseUnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
        => await this.UnsubscribeAsync(connection, subscription).ConfigureAwait(false);

    internal Task<CallResult<WebSocketUpdateSubscription>> BaseSubscribeAsync<T>(string url, string channel, IEnumerable<string> payload, bool authenticated, Action<WebSocketDataEvent<T>> onData, CancellationToken ct)
    {
        var request = new GateStreamRequest
        {
            Id = NextId(),
            Channel = channel,
            Event = StreamRequestEvent.Subscribe,
            Payload = payload.ToArray(),
        };

        if (authenticated)
        {
            if (AuthenticationProvider == null)
                throw new ArgumentNullException("ApiCredentials is null");

            ((GateAuthenticationProvider)AuthenticationProvider).AuthenticateStreamRequest(request);
        }

        return SubscribeAsync(url, request, null, authenticated: false, onData, ct);
    }

    /*
    internal async Task<CallResult<GateStreamResponse<GateStreamStatus>>> BaseUnsubscribeAsync<T>(string url, string channel, IEnumerable<string> payload, bool authenticated, Action<WebSocketDataEvent<T>> onData, CancellationToken ct)
    {
        var request = new GateStreamRequest
        {
            Id = NextId(),
            Channel = channel,
            Event = StreamRequestEvent.Unsubscribe,
            Payload = payload.ToArray(),
        };

        if (authenticated)
        {
            if (AuthenticationProvider == null)
                throw new ArgumentNullException("ApiCredentials is null");

            ((GateAuthenticationProvider)AuthenticationProvider).AuthenticateStreamRequest(request);
        }

        return await QueryAsync<GateStreamResponse<GateStreamStatus>>(url, request, false).ConfigureAwait(true);
    }
    */

    public async Task BaseUnsubscribeAsync(int subscriptionId)
        => await this.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    public async Task BaseUnsubscribeAsync(WebSocketUpdateSubscription subscription)
        => await this.UnsubscribeAsync(subscription).ConfigureAwait(false);

    public async Task BaseUnsubscribeAllAsync()
        => await this.UnsubscribeAllAsync().ConfigureAwait(false);
    #endregion

    internal async Task<CallResult<GateStreamLatency>> PingAsync(string endpoint, string channel)
    {
        var ping = DateTime.UtcNow;
        var sw = Stopwatch.StartNew();
        var response = await QueryAsync<GateStreamResponse<string>>(endpoint, new GateStreamRequest
        {
            Id = NextId(),
            Channel = channel,
        }, false).ConfigureAwait(true);
        var pong = DateTime.UtcNow;
        sw.Stop();

        if (!response.Success) return new CallResult<GateStreamLatency>(response.Error);
        return new CallResult<GateStreamLatency>(new GateStreamLatency
        {
            PingTime = ping,
            PongTime = pong,
            Latency = sw.Elapsed,
            PongMessage = ""
        });
    }

}