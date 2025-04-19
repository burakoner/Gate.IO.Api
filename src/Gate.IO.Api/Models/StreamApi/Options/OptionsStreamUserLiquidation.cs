namespace Gate.IO.Api.Models.StreamApi.Options;

public record OptionsStreamUserLiquidation
{
    [JsonProperty("user")]
    public long UserId { get; set; }

    [JsonProperty("init_margin")]
    public decimal InitialMargin { get; set; }
    
    [JsonProperty("maint_margin")]
    public decimal MaintenanceMargin { get; set; }
    
    [JsonProperty("order_margin")]
    public decimal OrderMargin { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
    
    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}