namespace Gate.IO.Api.Models.StreamApi;

public record GateStreamResponse<T>
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("time")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMlliseconds { get; set; }

    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("event"), JsonConverter(typeof(StreamResponseEventConverter))]
    public StreamResponseEvent Event { get; set; }

    [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
    public GateStreamError Error { get; set; }

    [JsonProperty("result")]
    public T Data { get; set; }
}

public enum StreamResponseEvent
{
    [Map("update")]
    Update,

    [Map("subscribe")]
    Subscribe,

    [Map("unsubscribe")]
    Unsubscribe
}

internal class StreamResponseEventConverter : BaseConverter<StreamResponseEvent>
{
    public StreamResponseEventConverter() : this(true) { }
    public StreamResponseEventConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<StreamResponseEvent, string>> Mapping => new()
    {
        new KeyValuePair<StreamResponseEvent, string>(StreamResponseEvent.Update, "update"),
        new KeyValuePair<StreamResponseEvent, string>(StreamResponseEvent.Subscribe, "subscribe"),
        new KeyValuePair<StreamResponseEvent, string>(StreamResponseEvent.Unsubscribe, "unsubscribe"),
    };
}

public record GateStreamError
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}