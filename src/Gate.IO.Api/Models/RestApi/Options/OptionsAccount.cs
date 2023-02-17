namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsAccount
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public int User { get; set; }

    /// <summary>
    /// Settle currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// If the account is allowed to short
    /// </summary>
    [JsonProperty("short_enabled")]
    public bool ShortEnabled { get; set; }

    /// <summary>
    /// Total account balance
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Unrealized PNL
    /// </summary>
    [JsonProperty("unrealised_pnl")]
    public decimal UnrealisedPnl { get; set; }

    /// <summary>
    /// Initial position margin
    /// </summary>
    [JsonProperty("init_margin")]
    public decimal InitMargin { get; set; }

    /// <summary>
    /// Position maintenance margin
    /// </summary>
    [JsonProperty("maint_margin")]
    public decimal MaintenanceMargin { get; set; }

    /// <summary>
    /// Order margin of unfinished orders
    /// </summary>
    [JsonProperty("order_margin")]
    public decimal OrderMargin { get; set; }

    /// <summary>
    /// Available balance to transfer out or trade
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// POINT amount
    /// </summary>
    [JsonProperty("point")]
    public decimal Point { get; set; }
}