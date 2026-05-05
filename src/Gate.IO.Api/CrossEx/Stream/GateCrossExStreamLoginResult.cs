namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx WebSocket login result payload.
/// </summary>
public record GateCrossExStreamLoginResult
{
    /// <summary>
    /// Connection ID.
    /// </summary>
    [JsonProperty("conn_id")]
    public string ConnectionId { get; set; }
}
