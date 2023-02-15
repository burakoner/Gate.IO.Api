namespace Gate.IO.Api.Converters;

internal class AccountTypeConverter : BaseConverter<AccountType>
{
    public AccountTypeConverter() : this(true) { }
    public AccountTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<AccountType, string>> Mapping => new()
    {
        new KeyValuePair<AccountType, string>(AccountType.Spot, "spot"),
        new KeyValuePair<AccountType, string>(AccountType.Margin, "margin"),
        new KeyValuePair<AccountType, string>(AccountType.CrossMargin, "cross_margin"),
    };
}

internal class AccountType2Converter : BaseConverter<AccountType2>
{
    public AccountType2Converter() : this(true) { }
    public AccountType2Converter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<AccountType2, string>> Mapping => new()
    {
        new KeyValuePair<AccountType2, string>(AccountType2.Spot, "normal"),
        new KeyValuePair<AccountType2, string>(AccountType2.Margin, "margin"),
        new KeyValuePair<AccountType2, string>(AccountType2.CrossMargin, "cross_margin"),
    };
}

internal class AccountType3Converter : BaseConverter<AccountType3>
{
    public AccountType3Converter() : this(true) { }
    public AccountType3Converter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<AccountType3, string>> Mapping => new()
    {
        new KeyValuePair<AccountType3, string>(AccountType3.Spot, "normal"),
        new KeyValuePair<AccountType3, string>(AccountType3.Futures, "futures"),
        new KeyValuePair<AccountType3, string>(AccountType3.CrossMargin, "cross_margin"),
    };
}

internal class WalletAccountConverter : BaseConverter<WalletAccount>
{
    public WalletAccountConverter() : this(true) { }
    public WalletAccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<WalletAccount, string>> Mapping => new()
    {
        new KeyValuePair<WalletAccount, string>(WalletAccount.Spot, "spot"),
        new KeyValuePair<WalletAccount, string>(WalletAccount.Margin, "margin"),
        new KeyValuePair<WalletAccount, string>(WalletAccount.CrossMargin, "cross_margin"),
        new KeyValuePair<WalletAccount, string>(WalletAccount.Futures, "futures"),
        new KeyValuePair<WalletAccount, string>(WalletAccount.Delivery, "delivery"),
        new KeyValuePair<WalletAccount, string>(WalletAccount.Options, "options"),
    };
}