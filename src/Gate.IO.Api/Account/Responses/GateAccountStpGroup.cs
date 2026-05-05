namespace Gate.IO.Api.Account;

/// <summary>
/// Represents the Gate Account STP Group.
/// </summary>
public record GateAccountStpGroup
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Creator ID.
    /// </summary>
    [JsonProperty("creator_id")]
    public long CreatorId { get; set; }

    /// <summary>
    /// Represents the Create ID.
    /// </summary>
    [JsonIgnore]
    public long CreateId
    {
        get => CreatorId;
        set => CreatorId = value;
    }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
