using Gate.IO.Api.SubAccount;
using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.Unified;

namespace Gate.IO.Api.Tests.SubAccount;

[Trait("Category", "Contract")]
public class SubAccountContractTests
{
    [Fact]
    public void Documented_sub_account_responses_deserialize()
    {
        var accounts = JsonFixture.Deserialize<List<GateSubAccount>>("Docs/SubAccount/sub_accounts.list.success.json");
        var created = JsonFixture.Deserialize<GateSubAccount>("Docs/SubAccount/sub_account.create.success.json");

        Assert.Single(accounts);
        Assert.Equal("sub_account_for_trades", accounts[0].Login);
        Assert.Equal(10001, accounts[0].UserId);
        Assert.Equal(GateSubAccountState.Normal, accounts[0].State);
        Assert.Equal(GateSubAccountType.SubAccount, accounts[0].Type);
        Assert.NotEqual(default, accounts[0].CreateTime);
        Assert.Equal("remark", created.Remark);
        Assert.Equal(GateSubAccountState.Normal, created.State);
    }

    [Fact]
    public void Documented_sub_account_api_key_responses_deserialize()
    {
        var keys = JsonFixture.Deserialize<List<GateSubAccountApiKey>>("Docs/SubAccount/api_keys.list.success.json");
        var created = JsonFixture.Deserialize<GateSubAccountApiKey>("Docs/SubAccount/api_key.create.success.json");
        var single = JsonFixture.Deserialize<GateSubAccountApiKey>("Docs/SubAccount/api_key.get.success.json");

        Assert.Single(keys);
        Assert.Equal(1000000, keys[0].UserId);
        Assert.Equal(GateSubAccountApiKeyState.Normal, keys[0].State);
        Assert.Equal(GateSubAccountApiKeyPermissionSection.Futures, keys[0].Permissions[0].Name);
        Assert.Contains(keys[0].IpWhitelist, x => x == "127.0.0.2");
        Assert.NotEqual(default, keys[0].UpdatedAt);
        Assert.NotNull(keys[0].LastAccessAt);
        Assert.Equal("eb8815bf99d7bb5f8ad6497bdc4774a8", created.Key);
        Assert.NotEmpty(created.Secret);
        Assert.Equal(GateSubAccountApiKeyPermissionSection.Options, created.Permissions[0].Name);
        Assert.Equal("75c3264105b74693d8cb5c7f1a8e2420", single.Key);
        Assert.NotEqual(default, single.UpdatedAt);
    }

    [Fact]
    public void Documented_sub_account_mode_response_deserializes()
    {
        var modes = JsonFixture.Deserialize<List<GateSubAccountMode>>("Docs/SubAccount/unified_mode.success.json");

        Assert.Single(modes);
        Assert.Equal(110285555, modes[0].UserId);
        Assert.True(modes[0].IsUnified);
        Assert.Equal(GateUnifiedAccountMode.MultiCurrency, modes[0].Mode);
    }
}
