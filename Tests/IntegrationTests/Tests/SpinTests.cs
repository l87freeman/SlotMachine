using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Extensions;
using IntegrationTests.Fixures;
using Moq;
using SlotMachine.Requests;
using SlotMachine.Responses;
using Xunit;

namespace IntegrationTests.Tests;

public class SpinTests : BaseTestFixture
{
    private GridConfiguration _grid;
    private ConsumerBalance _balance;

    public SpinTests(SlotMachineWebApplicationFactory factory) : base(factory)
    {
        _grid = Autofixture.Create<GridConfiguration>();
        _balance = Autofixture.Create<ConsumerBalance>();
    }

    [Fact]
    public async Task Spin_WhenLost_ThenReduceBalance()
    {
        var betAmount = 100;
        var win = -betAmount;
        SpinUseCase.Setup(x => x.ExecuteAsync(It.IsAny<SpinAttempt>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SpinAttemptResult
            {
                Win = win
            });
        await ConfigurationRepository.UpdateAsync(_grid, CancellationToken.None);
        await BalanceRepository.UpdateAsync(_balance, CancellationToken.None);

        var response = await HttpClient.PostAsJsonReadResponseAsync<SpinRequest, SpinResponse>("/spin", new SpinRequest
        {
            Bet = betAmount,
            ConsumerId = _balance.ConsumerId,
            GridId = _grid.Id,
        });

        var storedBalance = await BalanceRepository.GetAsync(_balance.ConsumerId, CancellationToken.None);
        response.Should().NotBeNull()
            .And.Match<SpinResponse>(x => x.Win == win && x.Balance == _balance.Balance - betAmount);
        storedBalance.Balance.Should().Be(_balance.Balance - betAmount);
    }

    [Fact]
    public async Task Spin_WhenWin_ThenIncreaseBalance()
    {
        var betAmount = 100;
        var win = betAmount * 10;
        SpinUseCase.Setup(x => x.ExecuteAsync(It.IsAny<SpinAttempt>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SpinAttemptResult
            {
                Win = win,
            });
        await ConfigurationRepository.UpdateAsync(_grid, CancellationToken.None);
        await BalanceRepository.UpdateAsync(_balance, CancellationToken.None);

        var response = await HttpClient.PostAsJsonReadResponseAsync<SpinRequest, SpinResponse>("/spin", new SpinRequest
        {
            Bet = betAmount,
            ConsumerId = _balance.ConsumerId,
            GridId = _grid.Id,
        });

        var storedBalance = await BalanceRepository.GetAsync(_balance.ConsumerId, CancellationToken.None);
        response.Should().NotBeNull()
            .And.Match<SpinResponse>(x => x.Win == win && x.Balance == _balance.Balance + win);
        storedBalance.Balance.Should().Be(_balance.Balance + win);
    }
}