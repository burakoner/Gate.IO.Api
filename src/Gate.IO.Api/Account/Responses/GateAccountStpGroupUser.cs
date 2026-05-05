namespace Gate.IO.Api.Account;

/// <summary>
/// Represents the Gate Account STP Group User.
/// </summary>
public record GateAccountStpGroupUser
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the STP ID.
    /// </summary>
    [JsonProperty("stp_id")]
    public long StpId { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
