using Gate.IO.Api.Authentication;
using Gate.IO.Api.Clients.StreamApi.Spot;
using Gate.IO.Api.Models.StreamApi;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamSpotClient : StreamApiClient
{
    // Clients
    public StreamSpotMarketDataClient MarketData { get; }
    public StreamSpotUserStreamClient UserStream { get; }

    // Internal
    internal Log Log { get => this.log; }
    internal CallResult<T> Deserializer<T>(string data, JsonSerializer serializer = null, int? requestId = null) => this.Deserialize<T>(data, serializer, requestId);
    internal CallResult<T> Deserializer<T>(JToken obj, JsonSerializer serializer = null, int? requestId = null) => this.Deserialize<T>(obj, serializer, requestId);

    // Root Client
    internal GateStreamClient RootClient { get; }

    // Options
    public new GateStreamClientOptions ClientOptions { get { return (GateStreamClientOptions)base.Options; } }

    internal StreamSpotClient(GateStreamClient root) : base("Gate.IO Spot Stream", root.ClientOptions)
    {
        RootClient = root;

        RateLimitPerConnectionPerSecond = 4;
        SetDataInterpreter((data) => string.Empty, null);

        this.MarketData = new StreamSpotMarketDataClient(this);
        this.UserStream = new StreamSpotUserStreamClient(this);
    }

    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
    => new GateAuthenticationProvider(credentials);

    internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<StreamDataEvent<T>> onData, CancellationToken ct)
    {
        var request = new BinanceSocketRequest
        {
            Method = "SUBSCRIBE",
            Params = topics.ToArray(),
            Id = NextId()
        };

        return SubscribeAsync(url.AppendPath("stream"), request, null, false, onData, ct);
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

        var bRequest = (BinanceSocketRequest)request;
        if ((int)id != bRequest.Id)
            return false;

        var result = message["result"];
        if (result != null && result.Type == JTokenType.Null)
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

        callResult = new CallResult<object>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.ToString()));
        return true;
    }

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, object request)
    {
        if (message.Type != JTokenType.Object)
            return false;

        var bRequest = (BinanceSocketRequest)request;
        var stream = message["stream"];
        if (stream == null)
            return false;

        return bRequest.Params.Contains(stream.ToString());
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
        var topics = ((BinanceSocketRequest)subscription.Request!).Params;
        var unsub = new BinanceSocketRequest { Method = "UNSUBSCRIBE", Params = topics, Id = NextId() };
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

}