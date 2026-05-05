using Gate.IO.Api.Rebate;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Rebate;

[Trait("Category", "Contract")]
public class RebateContractTests
{
    [Fact]
    public void Documented_agency_and_partner_history_responses_deserialize()
    {
        var agencyTransactions = JsonFixture.Deserialize<GateRebateTransactionHistory>("Docs/Rebate/agency_transaction_history.success.json");
        var agencyCommissions = JsonFixture.Deserialize<GateRebateCommissionHistory>("Docs/Rebate/agency_commission_history.success.json");
        var partnerTransactions = JsonFixture.Deserialize<GateRebateTransactionHistory>("Docs/Rebate/partner_transaction_history.success.json");
        var partnerCommissions = JsonFixture.Deserialize<GateRebateCommissionHistory>("Docs/Rebate/partner_commission_history.success.json");

        Assert.Equal(100, agencyTransactions.Total);
        Assert.Equal("GT_USDT", agencyTransactions.List[0].Symbol);
        Assert.Equal(1m, agencyTransactions.List[0].Fee);
        Assert.Equal(1000m, agencyTransactions.List[0].Amount);
        Assert.Equal(agencyTransactions.List[0].Amount, agencyTransactions.List[0].Commission);
        Assert.NotEqual(default, agencyTransactions.List[0].TransactionTime);
        Assert.Equal(1000m, agencyCommissions.List[0].CommissionAmount);
        Assert.Equal("USDT", agencyCommissions.List[0].CommissionAsset);
        Assert.Equal(agencyCommissions.List[0].CommissionTime, agencyCommissions.List[0].TransactionTime);
        Assert.Equal(15, partnerTransactions.Total);
        Assert.Equal(29.98688m, partnerTransactions.List[0].Amount);
        Assert.Equal(52, partnerCommissions.Total);
        Assert.Equal(0.2216934846m, partnerCommissions.List[0].CommissionAmount);
    }

    [Fact]
    public void Documented_partner_and_broker_responses_deserialize()
    {
        var subList = JsonFixture.Deserialize<GateRebatePartnerSubList>("Docs/Rebate/partner_sub_list.success.json");
        var brokerCommissions = JsonFixture.Deserialize<GateRebateBrokerCommissionHistory>("Docs/Rebate/broker_commission_history.success.json");
        var brokerTransactions = JsonFixture.Deserialize<GateRebateBrokerTransactionHistory>("Docs/Rebate/broker_transaction_history.success.json");

        Assert.Equal(3, subList.Total);
        Assert.Equal(123456789, subList.List[0].UserId);
        Assert.NotEqual(default, subList.List[0].UserJoinTime);
        Assert.Equal(100, brokerCommissions.Total);
        Assert.Equal(0.4m, brokerCommissions.List[0].RebateFee);
        Assert.Equal(0.3m, brokerCommissions.List[0].SubBrokerInfo.CommissionRate);
        Assert.Equal("0x123", brokerCommissions.List[0].AlphaContractAddress);
        Assert.Equal(1000m, brokerTransactions.List[0].Amount);
        Assert.Equal(0.2m, brokerTransactions.List[0].SubBrokerInfo.RelativeCommissionRate);
    }

    [Fact]
    public void Documented_user_and_partner_data_responses_deserialize()
    {
        var userInfo = JsonFixture.Deserialize<GateRebateUserInfo>("Docs/Rebate/user_info.success.json");
        var relation = JsonFixture.Deserialize<GateRebateUserSubRelation>("Docs/Rebate/user_sub_relation.success.json");
        var application = DeserializeWrappedData<GateRebatePartnerApplication>("Docs/Rebate/partner_application.recent.success.json");
        var eligibility = DeserializeWrappedData<GateRebatePartnerEligibility>("Docs/Rebate/partner_eligibility.success.json");
        var aggregated = DeserializeWrappedData<GateRebatePartnerAggregatedData>("Docs/Rebate/partner_aggregated_data.success.json");

        Assert.Equal(0, userInfo.InviteUserId);
        Assert.Single(relation.List);
        Assert.Equal(123456789, relation.List[0].UserId);
        Assert.Equal("broker", relation.List[0].Belong);
        Assert.Equal(987654321, relation.List[0].RefUserId);
        Assert.Equal(100, application.Id);
        Assert.Equal("partner@example.com", application.Email);
        Assert.Equal("partner_contact", application.OtherContact.Value);
        Assert.Equal("https://example.com/proof.png", Assert.Single(application.ProofImageUrls));
        Assert.False(eligibility.Eligible);
        Assert.Equal("kyc_required", Assert.Single(eligibility.BlockReasonCodes));
        Assert.Equal(123.45m, aggregated.RebateAmount);
        Assert.Equal(12, aggregated.CustomerCount);
        Assert.Equal(8, aggregated.TradingUserCount);
        Assert.Equal(GateRebateBusinessType.Spot, aggregated.BusinessType);
    }

    private static T DeserializeWrappedData<T>(string fixturePath)
    {
        var data = JObject.Parse(JsonFixture.Read(fixturePath))["data"];
        Assert.NotNull(data);
        return data!.ToObject<T>()!;
    }
}
