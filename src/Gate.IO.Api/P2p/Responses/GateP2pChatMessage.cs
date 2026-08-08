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
    public GateP2pChatMessageObject MessageObject { get; set; }

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

    /// <summary>
    /// File link
    /// </summary>
    [JsonProperty("pic")]
    public string Picture { get; set; }

    /// <summary>
    /// File key
    /// </summary>
    [JsonProperty("file_key")]
    public string FileKey { get; set; }

    /// <summary>
    /// File type
    /// </summary>
    [JsonProperty("file_type")]
    public string FileType { get; set; }

    /// <summary>
    /// Risk-control display type. 1 indicates off-platform traffic diversion risk.
    /// </summary>
    [JsonProperty("risk_type")]
    public int? RiskType { get; set; }

    /// <summary>
    /// Risk-control prompt returned when <see cref="RiskType"/> is 1
    /// </summary>
    [JsonProperty("toast_msg")]
    public string ToastMessage { get; set; }

    /// <summary>
    /// Gets or sets the Additional Data.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JToken> AdditionalData { get; set; }
}
