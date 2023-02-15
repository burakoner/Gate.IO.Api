namespace Gate.IO.Api.Converters;

internal class ApiKeyPermissionSectionConverter : BaseConverter<ApiKeyPermissionSection>
{
    public ApiKeyPermissionSectionConverter() : this(true) { }
    public ApiKeyPermissionSectionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<ApiKeyPermissionSection, string>> Mapping => new()
    {
        new KeyValuePair<ApiKeyPermissionSection, string>(ApiKeyPermissionSection.Spot, "spot"),
        new KeyValuePair<ApiKeyPermissionSection, string>(ApiKeyPermissionSection.Wallet, "wallet"),
        new KeyValuePair<ApiKeyPermissionSection, string>(ApiKeyPermissionSection.Futures, "futures"),
        new KeyValuePair<ApiKeyPermissionSection, string>(ApiKeyPermissionSection.Delivery, "delivery"),
        new KeyValuePair<ApiKeyPermissionSection, string>(ApiKeyPermissionSection.Earn, "earn"),
        new KeyValuePair<ApiKeyPermissionSection, string>(ApiKeyPermissionSection.Options, "options"),
    };
}