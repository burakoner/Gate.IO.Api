namespace Gate.IO.Api.Models.RestApi.Margin;

public class MarginAutoRepayment
{
    [JsonProperty("status"), JsonConverter(typeof(AutoRepaymentStatusConverter))]
    public AutoRepaymentStatus Status { get; set; }
}