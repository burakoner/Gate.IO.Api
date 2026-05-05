namespace Gate.IO.Api.Base;

/// <summary>
/// Represents the Stream API Base Client.
/// </summary>
public class GateBaseStreamApiClient : WebSocketApiClient
{
    // Internal
    internal ILogger Log { get => this.RootClient.Logger; }
    internal GateWebSocketClient RootClient { get; }

    // Options
    /// <summary>
    /// Executes the Client Options operation.
    /// </summary>
    public new GateWebSocketClientOptions ClientOptions { get { return (GateWebSocketClientOptions)base.ClientOptions; } }

    internal GateBaseStreamApiClient(GateWebSocketClient root) : base(root.Logger, root.ClientOptions)
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
    /// <summary>
    /// Represents the Create Authentication Provider value.
    /// </summary>
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
    {
        return new GateAuthentication(credentials);
    }

    /// <summary>
    /// Represents the Handle Query Response value.
    /// </summary>
    protected override bool HandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null;

        if (request is GateCrossExStreamRequest crossExRequest)
        {
            var channel = data["channel"]?.ToString();
            var eventName = data["event"]?.ToString();
            if ((crossExRequest.Event == "login" && eventName == "login") || (!string.IsNullOrEmpty(crossExRequest.Channel) && channel == crossExRequest.Channel))
            {
                var crossExError = data["error"];
                if (crossExError != null && crossExError.Type != JTokenType.Null)
                {
                    var errorCode = crossExError["code"]?.ToString();
                    int.TryParse(errorCode, out var code);
                    callResult = new CallResult<T>(new ServerError(code, crossExError["message"]?.ToString()));
                    return true;
                }

                var crossExResult = data["result"];
                if (crossExResult?.Type == JTokenType.Object)
                {
                    var resultCode = crossExResult["code"]?.ToString();
                    if (!string.IsNullOrEmpty(resultCode) && resultCode != "100000")
                    {
                        int.TryParse(resultCode, out var code);
                        callResult = new CallResult<T>(new ServerError(code, crossExResult["message"]?.ToString()));
                        return true;
                    }
                }

                callResult = new CallResult<T>(JsonConvert.DeserializeObject<T>(data.ToString()));
                return true;
            }
        }

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

    /// <summary>
    /// Represents the Handle Subscription Response value.
    /// </summary>
    protected override bool HandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
    {
        callResult = null;
        if (message.Type != JTokenType.Object)
            return false;

        if (request is GateCrossExStreamRequest crossExRequest)
        {
            var crossExChannel = message["channel"]?.ToString();
            if (!string.IsNullOrEmpty(crossExRequest.Channel) && crossExChannel != crossExRequest.Channel)
                return false;

            var eventName = message["event"]?.ToString();
            if (eventName != crossExRequest.Event)
                return false;

            var crossExError = message["error"];
            if (crossExError != null && crossExError.Type != JTokenType.Null)
            {
                var errorCode = crossExError["code"]?.ToString();
                int.TryParse(errorCode, out var code);
                callResult = new CallResult<object>(new ServerError(code, crossExError["message"]?.ToString()));
                return true;
            }

            var crossExResult = message["result"];
            if (crossExResult?.Type == JTokenType.Object && crossExResult["code"] != null)
            {
                var resultCode = crossExResult["code"]?.ToString();
                if (resultCode == "100000")
                {
                    Log?.LogTrace($"Socket {connection.Id} CrossEx subscription completed");
                    callResult = new CallResult<object>(new object());
                }
                else
                {
                    int.TryParse(resultCode, out var code);
                    callResult = new CallResult<object>(new ServerError(code, crossExResult["message"]?.ToString()));
                }

                return true;
            }

            return false;
        }

        var id = message["id"];
        if (id == null)
            return false;

        var bRequest = (GateStreamRequest)request;
        if ((int)id != bRequest.Id)
            return false;

        var result = message["result"];
        if (result != null && result.Type == JTokenType.Object && result["status"] != null && (string)result["status"] == "success")
        {
            Log?.LogTrace($"Socket {connection.Id} Subscription completed");
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

    /// <summary>
    /// Represents the Message Matches Handler value.
    /// </summary>
    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, object request)
    {
        if (message.Type != JTokenType.Object)
            return false;

        if (request is GateCrossExStreamRequest crossExRequest)
        {
            var crossExChannel = message["channel"];
            if (crossExChannel == null)
                return false;

            return crossExRequest.Channel == crossExChannel.ToString();
        }

        var bRequest = (GateStreamRequest)request;
        var channel = message["channel"];
        if (channel == null)
            return false;

        return bRequest.Channel == channel.ToString();
    }

    /// <summary>
    /// Represents the Message Matches Handler value.
    /// </summary>
    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, string identifier)
    {
        return true;
    }

    /// <summary>
    /// Represents the Authenticate value.
    /// </summary>
    protected override Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection connection)
    {
        return AuthenticateCrossExAsync(connection);
    }

    /// <summary>
    /// Represents the Unsubscribe value.
    /// </summary>
    protected override async Task<bool> UnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
    {
        if (subscription.Request is GateCrossExStreamRequest crossExRequest)
        {
            var crossExUnsub = new GateCrossExStreamRequest
            {
                Channel = crossExRequest.Channel,
                Event = "unsubscribe",
                Payload = crossExRequest.Payload
            };
            var crossExSuccess = false;

            if (!connection.Connected)
                return true;

            await connection.SendAndWaitAsync(crossExUnsub, ClientOptions.ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if (data["channel"]?.ToString() != crossExUnsub.Channel)
                    return false;

                if (data["event"]?.ToString() != crossExUnsub.Event)
                    return false;

                var result = data["result"];
                if (result?.Type == JTokenType.Object && result["code"]?.ToString() == "100000")
                {
                    crossExSuccess = true;
                    return true;
                }

                var error = data["error"];
                return error != null && error.Type != JTokenType.Null;
            }).ConfigureAwait(false);

            return crossExSuccess;
        }

        var request = (GateStreamRequest)subscription.Request!;
        var unsub = new GateStreamRequest
        {
            Id = NextId(),
            Channel = request.Channel,
            Event = StreamRequestEvent.Unsubscribe,
            Payload = request.Payload
        };
        var success = false;

        if (request.Auth != null)
        {
            if (AuthenticationProvider == null)
                throw new ArgumentNullException("ApiCredentials is null");

            ((GateAuthentication)AuthenticationProvider).AuthenticateStreamRequest(unsub);
        }

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
            if (result?.Type == JTokenType.Object && result["status"]?.ToString() == "success")
            {
                success = true;
                return true;
            }

            var error = data["error"];
            if (error != null && error.Type != JTokenType.Null)
                return true;

            return false;
        }).ConfigureAwait(false);

        return success;
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

    internal GateCrossExStreamLoginPayload CreateCrossExLoginPayload(long timestamp)
    {
        if (AuthenticationProvider == null)
            throw new ArgumentNullException("ApiCredentials is null");

        return ((GateAuthentication)AuthenticationProvider).CreateCrossExLoginPayload(timestamp);
    }

    internal Task<CallResult<WebSocketUpdateSubscription>> BaseSubscribeAsync<T>(string url, string channel, IEnumerable<string> payload, bool authenticated, Action<WebSocketDataEvent<T>> onData, CancellationToken ct)
        => BaseSubscribeAsync(url, channel, (object)payload?.ToList(), authenticated, onData, ct);

    internal Task<CallResult<WebSocketUpdateSubscription>> BaseSubscribeAsync<T>(string url, string channel, object payload, bool authenticated, Action<WebSocketDataEvent<T>> onData, CancellationToken ct)
    {
        var request = new GateStreamRequest
        {
            Id = NextId(),
            Channel = channel,
            Event = StreamRequestEvent.Subscribe,
            Payload = payload,
        };

        if (authenticated)
        {
            if (AuthenticationProvider == null)
                throw new ArgumentNullException("ApiCredentials is null");

            ((GateAuthentication)AuthenticationProvider).AuthenticateStreamRequest(request);
        }

        return SubscribeAsync(url, request, null, authenticated: false, onData, ct);
    }

    internal Task<CallResult<WebSocketUpdateSubscription>> CrossExSubscribeAsync<T>(string url, string channel, object payload, Action<WebSocketDataEvent<T>> onData, CancellationToken ct)
        => CrossExSubscribeAsync(url, channel, payload, false, onData, ct);

    internal Task<CallResult<WebSocketUpdateSubscription>> CrossExSubscribeAsync<T>(string url, string channel, object payload, bool authenticated, Action<WebSocketDataEvent<T>> onData, CancellationToken ct)
    {
        var request = new GateCrossExStreamRequest
        {
            Channel = channel,
            Event = "subscribe",
            Payload = payload,
        };

        return SubscribeAsync(url, request, null, authenticated, onData, ct);
    }

    internal async Task<CallResult<T>> CrossExQueryAsync<T>(string url, string channel, string eventName, object payload)
        => await CrossExQueryAsync<T>(url, channel, eventName, payload, false).ConfigureAwait(false);

    internal async Task<CallResult<T>> CrossExQueryAsync<T>(string url, string channel, string eventName, object payload, bool authenticated)
    {
        var request = new GateCrossExStreamRequest
        {
            Channel = channel,
            Event = eventName,
            Payload = payload,
        };

        return await QueryAsync<T>(url, request, authenticated).ConfigureAwait(false);
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

    /// <summary>
    /// Executes the Base Unsubscribe operation.
    /// </summary>
    public async Task BaseUnsubscribeAsync(int subscriptionId)
        => await this.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    /// <summary>
    /// Executes the Base Unsubscribe operation.
    /// </summary>
    public async Task BaseUnsubscribeAsync(WebSocketUpdateSubscription subscription)
        => await this.UnsubscribeAsync(subscription).ConfigureAwait(false);

    /// <summary>
    /// Executes the Base Unsubscribe All operation.
    /// </summary>
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

    private async Task<CallResult<bool>> AuthenticateCrossExAsync(WebSocketConnection connection)
    {
        if (AuthenticationProvider == null)
            throw new ArgumentNullException("ApiCredentials is null");

        var timestamp = DateTime.UtcNow.ConvertToSeconds();
        var request = new GateCrossExStreamRequest
        {
            Event = "login",
            Payload = ((GateAuthentication)AuthenticationProvider).CreateCrossExLoginPayload(timestamp),
        };

        CallResult<bool> result = null;
        await connection.SendAndWaitAsync(request, ClientOptions.ResponseTimeout, data =>
        {
            if (data.Type != JTokenType.Object || data["event"]?.ToString() != "login")
                return false;

            var error = data["error"];
            if (error != null && error.Type != JTokenType.Null)
            {
                var errorCode = error["code"]?.ToString();
                int.TryParse(errorCode, out var code);
                result = new CallResult<bool>(new ServerError(code, error["message"]?.ToString()));
                return true;
            }

            var response = data["result"];
            if (response?.Type == JTokenType.Object && response["code"]?.ToString() == "100000")
            {
                result = new CallResult<bool>(true);
                return true;
            }

            if (response?.Type == JTokenType.Object && response["code"] != null)
            {
                var responseCode = response["code"]?.ToString();
                int.TryParse(responseCode, out var code);
                result = new CallResult<bool>(new ServerError(code, response["message"]?.ToString()));
                return true;
            }

            return false;
        }).ConfigureAwait(false);

        return result ?? new CallResult<bool>(new ServerError("CrossEx WebSocket login timed out."));
    }

}
