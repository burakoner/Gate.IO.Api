namespace Gate.IO.Api.Clients.StreamApi.Spot;

public class StreamSpotMarketDataClient
{
    // Internal References
    internal StreamSpotClient MainClient { get; }
    internal Log Log { get => MainClient.Log; }
    internal string BaseAddress { get => Options.BaseAddress; }
    internal GateStreamClientOptions Options { get => MainClient.RootClient.Options; }
    internal CallResult<T> Deserialize<T>(string data, JsonSerializer serializer = null, int? requestId = null) => MainClient.Deserializer<T>(data, serializer, requestId);
    internal CallResult<T> Deserialize<T>(JToken obj, JsonSerializer serializer = null, int? requestId = null) => MainClient.Deserializer<T>(obj, serializer, requestId);
    internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<StreamDataEvent<T>> onData, CancellationToken ct)
    => MainClient.SubscribeAsync<T>(url, topics, onData, ct);

    internal StreamSpotMarketDataClient(StreamSpotClient main)
    {
        MainClient = main;
    }

}