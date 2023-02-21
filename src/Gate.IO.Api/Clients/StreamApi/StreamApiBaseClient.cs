using Gate.IO.Api.Models.StreamApi;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiBaseClient : StreamApiClient
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
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
    {
        return new GateAuthenticationProvider(credentials);
    }

    protected override bool HandleQueryResponse<T>(StreamConnection connection, object request, JToken data, out CallResult<T> callResult)
    {
        throw new NotImplementedException();
    }

    protected override bool HandleSubscriptionResponse(StreamConnection connection, StreamSubscription subscription, object request, JToken message, out CallResult<object> callResult)
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

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, object request)
    {
        if (message.Type != JTokenType.Object)
            return false;

        var bRequest = (GateStreamRequest)request;
        var channel = message["channel"];
        if (channel == null)
            return false;

        return bRequest.Channel == channel.ToString();
    }

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, string identifier)
    {
        return true;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(StreamConnection connection)
    {
        throw new NotImplementedException();
    }

    protected override async Task<bool> UnsubscribeAsync(StreamConnection connection, StreamSubscription subscription)
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

    internal bool BaseHandleQueryResponse<T>(StreamConnection connection, object request, JToken data, out CallResult<T> callResult)
        => this.HandleQueryResponse<T>(connection, request, data, out callResult);

    internal bool BaseHandleSubscriptionResponse(StreamConnection connection, StreamSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        => this.HandleSubscriptionResponse(connection, subscription, request, message, out callResult);

    internal bool BaseMessageMatchesHandler(StreamConnection connection, JToken message, object request)
        => this.MessageMatchesHandler( connection,  message,  request);

    internal bool BaseMessageMatchesHandler(StreamConnection connection, JToken message, string identifier)
        => this.MessageMatchesHandler(connection, message, identifier);

    internal Task<CallResult<bool>> BaseAuthenticateAsync(StreamConnection connection)
        => this.AuthenticateAsync(connection);

    internal async Task<bool> BaseUnsubscribeAsync(StreamConnection connection, StreamSubscription subscription)
        => await this.UnsubscribeAsync(connection, subscription).ConfigureAwait(false);

    internal Task<CallResult<UpdateSubscription>> BaseSubscribeAsync<T>(string url, string channel, IEnumerable<string> payload, bool authenticated, Action<StreamDataEvent<T>> onData, CancellationToken ct)
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

    public async Task BaseUnsubscribeAsync(int subscriptionId)
        => await this.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    public async Task BaseUnsubscribeAsync(UpdateSubscription subscription)
        => await this.UnsubscribeAsync(subscription).ConfigureAwait(false);

    public async Task BaseUnsubscribeAllAsync()
        => await this.UnsubscribeAllAsync().ConfigureAwait(false);
    #endregion
}