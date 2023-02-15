namespace Gate.IO.Api.Converters;

internal class WithdrawalStatusConverter : BaseConverter<WithdrawalStatus>
{
    public WithdrawalStatusConverter() : this(true) { }
    public WithdrawalStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<WithdrawalStatus, string>> Mapping => new()
    {
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Done, "DONE"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Cancelled, "CANCEL"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Requesting, "REQUEST"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Pending, "PEND"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.PendingManualApproval, "MANUAL"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.PendingConfirmAfterSending, "EXTPEND"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.GateCodeOperation, "BCODE"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.PendingConfirmWhenFail, "FAIL"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.InvalidOrder, "INVALID"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Verifying, "VERIFY"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Processing, "PROCES"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.RequiredManualApproval, "DMOVE"),
        new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Splitted, "SPLITPEND"),
    };
}