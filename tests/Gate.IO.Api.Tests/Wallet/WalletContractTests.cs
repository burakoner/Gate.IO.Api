using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Wallet;

[Trait("Category", "Contract")]
public class WalletContractTests
{
    [Fact]
    public void Documented_withdrawal_responses_deserialize()
    {
        var withdrawal = JsonFixture.Deserialize<GateWalletTransaction>("Docs/Withdrawal/withdraw.success.json");
        var transfer = JsonFixture.Deserialize<GateWalletTransferId>("Docs/Withdrawal/uid-transfer.success.json");
        var cancel = JsonFixture.Deserialize<GateWalletTransaction>("Docs/Withdrawal/cancel-withdrawal.success.json");

        Assert.Equal("210496", withdrawal.Id);
        Assert.Equal("order_123456", withdrawal.WithdrawalOrderId);
        Assert.Equal(222.61m, withdrawal.Amount);
        Assert.Equal(GateWalletWithdrawalStatus.Done, withdrawal.Status);
        Assert.Equal("111", transfer.Id);
        Assert.Equal("210496", cancel.Id);
        Assert.Equal(18217349, cancel.BlockNumber);
        Assert.Equal(GateWalletWithdrawalStatus.Cancelled, cancel.Status);
    }

    [Fact]
    public void Documented_wallet_accounting_responses_deserialize()
    {
        var totalBalance = JsonFixture.Deserialize<GateWalletTotalBalance>("Docs/Wallet/total_balance.success.json");
        var fee = JsonFixture.Deserialize<GateWalletUserTradingFee>("Docs/Wallet/fee.success.json");
        var smallBalances = JsonFixture.Deserialize<List<List<GateWalletSmallBalance>>>("Docs/Wallet/small_balance.success.json")
            .SelectMany(x => x)
            .ToList();

        Assert.Equal(100.5m, totalBalance.Total.Amount);
        Assert.Equal(50m, totalBalance.Details.Spot.Amount);
        Assert.Equal(0.002m, fee.TakerFee);
        Assert.True(fee.GtDiscount);
        Assert.Single(smallBalances);
        Assert.Equal("GT", smallBalances[0].Currency);
        Assert.Equal(1.5m, smallBalances[0].EstimatedAsGT);
    }

    [Fact]
    public void Documented_wallet_currency_and_deposit_responses_deserialize()
    {
        var chains = JsonFixture.Deserialize<List<GateWalletCurencyChain>>("Docs/Wallet/currency_chains.success.json");
        var liveChains = JsonFixture.Deserialize<List<GateWalletCurencyChain>>("Live/Wallet/currency_chains.GT.json");
        var depositAddress = JsonFixture.Deserialize<GateWalletDepositAddress>("Docs/Wallet/deposit_address.success.json");
        var savedAddresses = JsonFixture.Deserialize<List<GateWalletSavedAddress>>("Docs/Wallet/saved_address.success.json");

        Assert.Single(chains);
        Assert.Equal("ETH", chains[0].Chain);
        Assert.False(chains[0].IsDisabled);
        Assert.Contains(liveChains, x => x.Chain == "GT");
        Assert.Equal("USDT", depositAddress.Currency);
        Assert.Equal(0.01m, depositAddress.MinDepositAmount);
        Assert.Contains(depositAddress.MultichainAddresses, x => x.Chain == "TRX" && !x.ObtainFailed);
        Assert.Single(savedAddresses);
        Assert.Equal("TRX", savedAddresses[0].Chain);
        Assert.True(savedAddresses[0].Verified);
    }

    [Fact]
    public void Documented_wallet_transaction_responses_deserialize()
    {
        var withdrawals = JsonFixture.Deserialize<List<List<GateWalletTransaction>>>("Docs/Wallet/withdrawals.success.json")
            .SelectMany(x => x)
            .ToList();
        var deposits = JsonFixture.Deserialize<List<GateWalletTransaction>>("Docs/Wallet/deposits.success.json");

        Assert.Single(withdrawals);
        Assert.Equal("w1879219868", withdrawals[0].Id);
        Assert.Equal("w1879219868", withdrawals[0].WithdrawalId);
        Assert.Equal(GateWalletAssetClass.MainZone, withdrawals[0].AssetClass);
        Assert.Equal(GateWalletWithdrawalStatus.Done, withdrawals[0].Status);
        Assert.Single(deposits);
        Assert.Equal("210496", deposits[0].Id);
        Assert.Equal(222.61m, deposits[0].Amount);
    }

    [Fact]
    public void Documented_wallet_transfer_responses_deserialize()
    {
        var records = JsonFixture.Deserialize<List<GateWalletTransferRecord>>("Docs/Wallet/sub_account_transfers.success.json");
        var status = JsonFixture.Deserialize<GateWalletTransferStatus>("Docs/Wallet/transfer_status.success.json");
        var history = JsonFixture.Deserialize<List<GateWalletTransfer>>("Docs/Wallet/uid-transfer-history.success.json");

        Assert.Single(records);
        Assert.Equal(10001, records[0].UserId);
        Assert.Equal(GateWalletSubAccountType.Spot, records[0].SubAccountType);
        Assert.Equal(GateWalletTransferDirection.To, records[0].Direction);
        Assert.Equal("59636381286", status.TransactionId);
        Assert.Equal(GateWalletTransferState.Success, status.Status);
        Assert.Single(history);
        Assert.Equal(GateWalletUidTransferState.Pending, history[0].Status);
        Assert.Equal(GateWalletTransferType.Withdrawal, history[0].TransferType);
    }

    [Fact]
    public void Documented_wallet_status_and_sub_account_balance_responses_deserialize()
    {
        var withdrawalStatus = JsonFixture.Deserialize<List<GateWalletWithdrawal>>("Docs/Wallet/withdraw_status.success.json");
        var balances = JsonFixture.Deserialize<List<GateWalletSubAccountBalance>>("Docs/Wallet/sub_account_balances.success.json");

        Assert.Single(withdrawalStatus);
        Assert.Equal("GT", withdrawalStatus[0].Currency);
        Assert.Equal(0.01m, withdrawalStatus[0].WithdrawalFix);
        Assert.Equal("0%", withdrawalStatus[0].WithdrawPercentOnChains["ETH"]);
        Assert.Single(balances);
        Assert.Equal(10003, balances[0].UserId);
        Assert.Equal(2000m, balances[0].Available["GT"]);
        Assert.Equal(0m, balances[0].Locked["USDT"]);
    }
}
