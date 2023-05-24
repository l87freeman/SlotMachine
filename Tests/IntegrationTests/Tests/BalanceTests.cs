using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Extensions;
using IntegrationTests.Fixures;
using SlotMachine.Requests;
using Xunit;

namespace IntegrationTests.Tests;

public class BalanceTests : BaseTestFixture
{
    private ConsumerBalance _balance;

    public BalanceTests(SlotMachineWebApplicationFactory factory) : base(factory)
    {
        _balance = Autofixture.Create<ConsumerBalance>();
    }

    [Fact]
    public async Task Balance_WhenNewConsumerBalance_ThenCreateNewBalanceEntry()
    {
        var depositAmount = 1599;
        var consumerId = Autofixture.Create<string>();
        
        await HttpClient.PutAsJsonReadResponseAsync<DepositBalanceRequest, object>("/balance", new DepositBalanceRequest
        {
            Amount = depositAmount,
            ConsumerId = consumerId,
        });

        var storedBalance = await BalanceRepository.GetAsync(consumerId, CancellationToken.None);
        storedBalance.Balance.Should().Be(depositAmount);
    }

    [Fact]
    public async Task Balance_WhenConsumerExists_ThenIncreaseBalance()
    {
        var depositAmount = 700;
        await BalanceRepository.UpdateAsync(_balance, CancellationToken.None);

        await HttpClient.PutAsJsonReadResponseAsync<DepositBalanceRequest, object>("/balance", new DepositBalanceRequest
        {
            Amount = depositAmount,
            ConsumerId = _balance.ConsumerId,
        });

        var storedBalance = await BalanceRepository.GetAsync(_balance.ConsumerId, CancellationToken.None);
        storedBalance.Balance.Should().Be(depositAmount + _balance.Balance);
    }
}