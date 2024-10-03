namespace Gate.IO.Api.Models.StreamApi.Futures;

public class FuturesStreamReduceRiskLimit
{
    [JsonProperty("time")]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("cancel_orders")]
    public int CancelOrders { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("leverage_max")]
    public int LeverageMax { get; set; }

    [JsonProperty("liq_price")]
    public decimal LiqPrice { get; set; }

    [JsonProperty("maintenance_rate")]
    public decimal MaintenanceRate { get; set; }

    [JsonProperty("risk_limit")]
    public decimal RiskLimit { get; set; }
}
