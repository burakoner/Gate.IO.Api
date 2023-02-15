namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletTransferRecord : WalletTransfer
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("timest"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }
}