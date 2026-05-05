namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx WebSocket login payload.
/// </summary>
public record GateCrossExStreamLoginPayload
{
    /// <summary>
    /// Authentication method.
    /// </summary>
    [JsonProperty("method")]
    public string Method { get; set; } = "api_key";

    /// <summary>
    /// API key.
    /// </summary>
    [JsonProperty("api_key")]
    public string ApiKey { get; set; }

    /// <summary>
    /// Request signature.
    /// </summary>
    [JsonProperty("sign")]
    public string Sign { get; set; }
}
