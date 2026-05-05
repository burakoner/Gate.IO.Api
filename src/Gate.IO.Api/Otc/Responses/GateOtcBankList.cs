namespace Gate.IO.Api.Otc;

internal record GateOtcBankList
{
    [JsonProperty("lists")]
    public List<GateOtcBankAccount> Lists { get; set; } = [];
}
