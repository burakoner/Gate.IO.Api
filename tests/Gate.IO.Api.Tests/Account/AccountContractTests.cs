using Gate.IO.Api.Account;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Account;

[Trait("Category", "Contract")]
public class AccountContractTests
{
    [Fact]
    public void Documented_account_detail_response_deserializes()
    {
        var account = JsonFixture.Deserialize<GateAccountDetails>("Docs/Account/detail.success.json");

        Assert.Equal(1667201533, account.UserId);
        Assert.Equal("127.0.0.1", Assert.Single(account.IpWhitelist));
        Assert.Equal("USDT_BTC", Assert.Single(account.CurrencyPairs));
        Assert.Equal(account.CurrencyPairs, account.Symbols);
        Assert.Equal(2, account.Tier);
        Assert.Equal(GateAccountApiKeyMode.ClassicAccount, account.ApiKey.Mode);
        Assert.Equal(GateAccountCopyTradingRole.OrderLeader, account.CopyTradingRole);
    }

    [Fact]
    public void Documented_main_key_responses_deserialize()
    {
        var keys = JsonFixture.Deserialize<List<GateAccountKeyInfo>>("Docs/Account/main_keys.success.json");

        Assert.Equal(2, keys.Count);
        Assert.Equal(GateAccountApiKeyState.Normal, keys[0].State);
        Assert.Equal(GateAccountApiKeyMode.ClassicAccount, keys[0].Mode);
        Assert.Equal("test1", keys[0].Name);
        Assert.Equal(1, keys[0].UserId);
        Assert.Equal("BTC_USD", keys[0].CurrencyPairs[0]);
        Assert.Equal("c5dcfbf1f3a7*****", keys[0].Key);
        Assert.Equal(GateAccountApiKeyPermissionSection.Account, keys[0].Permissions[0].Name);
        Assert.False(keys[0].Permissions[0].ReadOnly);
        Assert.NotEqual(default, keys[0].CreatedAt);
        Assert.NotEqual(default, keys[0].UpdatedAt);
        Assert.NotNull(keys[0].LastAccessAt);
        Assert.Equal(keys[0].UpdatedAt, keys[0].UpdateAt);
    }

    [Fact]
    public void Documented_rate_limit_response_deserializes_numeric_strings()
    {
        var limits = JsonFixture.Deserialize<List<GateAccountRateLimit>>("Docs/Account/rate_limit.success.json");

        Assert.Equal(2, limits.Count);
        Assert.Equal("spot", limits[0].Type);
        Assert.Equal(1, limits[0].Tier);
        Assert.Equal(0m, limits[0].Ratio);
        Assert.Equal(0m, limits[0].MainRatio);
        Assert.NotEqual(default, limits[0].UpdatedAt);
    }

    [Fact]
    public void Documented_stp_and_debit_fee_responses_deserialize()
    {
        var groups = JsonFixture.Deserialize<List<GateAccountStpGroup>>("Docs/Account/stp_groups.success.json");
        var group = JsonFixture.Deserialize<GateAccountStpGroup>("Docs/Account/stp_group.success.json");
        var users = JsonFixture.Deserialize<List<GateAccountStpGroupUser>>("Docs/Account/stp_group_users.success.json");
        var debitFee = JsonFixture.Deserialize<GateAccountGtDeduction>("Docs/Account/debit_fee.success.json");

        Assert.Single(groups);
        Assert.Equal(123435, group.Id);
        Assert.Equal("group", group.Name);
        Assert.Equal(10000, group.CreatorId);
        Assert.Equal(group.CreatorId, group.CreateId);
        Assert.NotEqual(default, group.CreateTime);
        Assert.Single(users);
        Assert.Equal(10000, users[0].UserId);
        Assert.Equal(1, users[0].StpId);
        Assert.NotEqual(default, users[0].CreateTime);
        Assert.True(debitFee.Enabled);
    }
}
