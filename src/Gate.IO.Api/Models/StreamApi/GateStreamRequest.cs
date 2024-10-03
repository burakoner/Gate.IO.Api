namespace Gate.IO.Api.Models.StreamApi;

internal class GateStreamRequest
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int Id { get; set; }

    [JsonProperty("time")]
    public long Timestamp { get; set; } = DateTime.UtcNow.ConvertToSeconds();

    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(StreamRequestEventConverter))]
    public StreamRequestEvent? Event { get; set; }

    [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public List<string> Payload { get; set; } = [];

    [JsonProperty("auth", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public StreamRequestAuth Auth { get; set; }
}

public class StreamRequestAuth
{
    [JsonProperty("method")]
    public string Method { get; set; } = "api_key";

    [JsonProperty("KEY")]
    public string ApiKey { get; set; }

    [JsonProperty("SIGN")]
    public string Signature { get; set; }
}

public enum StreamRequestEvent
{
    [Map("subscribe")]
    Subscribe,

    [Map("unsubscribe")]
    Unsubscribe
}

internal class StreamRequestEventConverter : BaseConverter<StreamRequestEvent>
{
    public StreamRequestEventConverter() : this(true) { }
    public StreamRequestEventConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<StreamRequestEvent, string>> Mapping =>
    [
        new KeyValuePair<StreamRequestEvent, string>(StreamRequestEvent.Subscribe, "subscribe"),
        new KeyValuePair<StreamRequestEvent, string>(StreamRequestEvent.Unsubscribe, "unsubscribe"),
    ];
}