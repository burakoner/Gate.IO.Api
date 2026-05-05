namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P chat message
/// </summary>
public record GateP2pChatMessage
{
    /// <summary>
    /// Seller-side flag
    /// </summary>
    [JsonProperty("is_sell")]
    public int? IsSell { get; set; }

    /// <summary>
    /// Message type
    /// </summary>
    [JsonProperty("msg_type")]
    public int? MessageType { get; set; }

    /// <summary>
    /// Message type alias
    /// </summary>
    [JsonProperty("type")]
    public int? Type { get; set; }

    /// <summary>
    /// Message body
    /// </summary>
    [JsonProperty("msg")]
    public string Message { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }

    /// <summary>
    /// Message object
    /// </summary>
    [JsonProperty("msg_obj")]
    public JToken MessageObject { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public string UserId { get; set; }

    /// <summary>
    /// Message timestamp
    /// </summary>
    [JsonProperty("timest")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Time { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
