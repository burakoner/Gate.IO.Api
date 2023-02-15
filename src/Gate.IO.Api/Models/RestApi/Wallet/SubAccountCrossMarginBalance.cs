namespace Gate.IO.Api.Models.RestApi.Wallet;

public class SubAccountCrossMarginBalance
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("available")]
    public SubAccountCrossMarginBalanceAvailable Available { get; set; }
}

public class SubAccountCrossMarginBalanceAvailable
{
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("locked")]
    public bool Locked { get; set; }

    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    [JsonProperty("borrowed_net")]
    public decimal BorrowedNet { get; set; }

    [JsonProperty("net")]
    public decimal Net { get; set; }

    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    [JsonProperty("total_initial_margin")]
    public decimal TotalInitialMargin { get; set; }

    [JsonProperty("total_margin_balance")]
    public decimal TotalMarginBalance { get; set; }

    [JsonProperty("total_maintenance_margin")]
    public decimal TotalMaintenanceMargin { get; set; }

    [JsonProperty("total_initial_margin_rate")]
    public decimal TotalInitialMarginRate { get; set; }

    [JsonProperty("total_maintenance_margin_rate")]
    public decimal TotalMaintenanceMarginRate { get; set; }

    [JsonProperty("total_available_margin")]
    public decimal TotalAvailableMargin { get; set; }

    [JsonProperty("balances")]
    public Dictionary<string, SubAccountCrossMarginBalanceAvailableItem> Balances { get; set; }
}

public class SubAccountCrossMarginBalanceAvailableItem
{
    [JsonProperty("available")]
    public decimal Available { get; set; }

    [JsonProperty("freeze")]
    public decimal Freeze { get; set; }
    
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }
    
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}