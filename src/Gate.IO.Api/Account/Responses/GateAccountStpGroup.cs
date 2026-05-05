namespace Gate.IO.Api.Account;

public record GateAccountStpGroup
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("creator_id")]
    public long CreatorId { get; set; }

    [JsonIgnore]
    public long CreateId
    {
        get => CreatorId;
        set => CreatorId = value;
    }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
