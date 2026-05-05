namespace Gate.IO.Api.Base;

internal record GateStreamRequest
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

/// <summary>
/// Represents the Stream Request Auth.
/// </summary>
public record StreamRequestAuth
{
    /// <summary>
    /// Gets or sets the Method.
    /// </summary>
    [JsonProperty("method")]
    public string Method { get; set; } = "api_key";

    /// <summary>
    /// Gets or sets the API Key.
    /// </summary>
    [JsonProperty("KEY")]
    public string ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the Signature.
    /// </summary>
    [JsonProperty("SIGN")]
    public string Signature { get; set; }
}

/// <summary>
/// Represents the Stream Request Event.
/// </summary>
public enum StreamRequestEvent : byte
{
    /// <summary>
    /// Represents the Subscribe value.
    /// </summary>
    [Map("subscribe")]
    Subscribe=1,

    /// <summary>
    /// Represents the Unsubscribe value.
    /// </summary>
    [Map("unsubscribe")]
    Unsubscribe=2
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
