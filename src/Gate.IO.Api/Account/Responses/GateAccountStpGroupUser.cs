namespace Gate.IO.Api.Account;

public record GateAccountStpGroupUser
{
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("stp_id")]
    public long StpId { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}
