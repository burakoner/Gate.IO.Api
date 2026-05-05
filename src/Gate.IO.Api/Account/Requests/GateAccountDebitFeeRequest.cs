namespace Gate.IO.Api.Account;

/// <summary>
/// Account GT fee deduction request
/// </summary>
public record GateAccountDebitFeeRequest
{
    /// <summary>
    /// Whether GT fee deduction is enabled
    /// </summary>
    public bool Enabled { get; set; }
}
