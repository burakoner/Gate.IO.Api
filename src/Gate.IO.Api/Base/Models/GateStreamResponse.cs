namespace Gate.IO.Api.Base;

/// <summary>
/// Represents the Gate Stream Response.
/// </summary>
public record GateStreamResponse<T>
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the connection ID.
    /// </summary>
    [JsonProperty("conn_id")]
    public string ConnectionId { get; set; }

    /// <summary>
    /// Gets or sets the trace ID.
    /// </summary>
    [JsonProperty("trace_id")]
    public string TraceId { get; set; }

    /// <summary>
    /// Gets or sets the Timestamp.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the Time In Mlliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    public long TimeInMlliseconds { get; set; }

    /// <summary>
    /// Gets or sets the Channel.
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; }

    /// <summary>
    /// Gets or sets the Event.
    /// </summary>
    [JsonProperty("event"), JsonConverter(typeof(StreamResponseEventConverter))]
    public StreamResponseEvent Event { get; set; }

    /// <summary>
    /// Gets or sets the Error.
    /// </summary>
    [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
    public GateStreamError Error { get; set; }

    /// <summary>
    /// Gets or sets the Data.
    /// </summary>
    [JsonProperty("result")]
    public T Data { get; set; }
}

/// <summary>
/// Represents the Stream Response Event.
/// </summary>
public enum StreamResponseEvent : byte
{
    /// <summary>
    /// Represents an empty or unspecified response event.
    /// </summary>
    [Map("")]
    None = 255,

    /// <summary>
    /// Represents the Update value.
    /// </summary>
    [Map("update")]
    Update = 0,

    /// <summary>
    /// Represents the Subscribe value.
    /// </summary>
    [Map("subscribe")]
    Subscribe = 1,

    /// <summary>
    /// Represents the Unsubscribe value.
    /// </summary>
    [Map("unsubscribe")]
    Unsubscribe = 2,

    /// <summary>
    /// Represents the Api value.
    /// </summary>
    [Map("api")]
    Api = 3,

    /// <summary>
    /// Represents the All value.
    /// </summary>
    [Map("all")]
    All = 4
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
        new KeyValuePair<StreamResponseEvent, string>(StreamResponseEvent.Api, "api"),
        new KeyValuePair<StreamResponseEvent, string>(StreamResponseEvent.All, "all"),
        new KeyValuePair<StreamResponseEvent, string>(StreamResponseEvent.None, ""),
    };
}

/// <summary>
/// Represents the Gate Stream Error.
/// </summary>
public record GateStreamError
{
    /// <summary>
    /// Gets or sets the Code.
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    /// Gets or sets the Message.
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}
