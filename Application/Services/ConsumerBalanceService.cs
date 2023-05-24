using System.Threading;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Domain.UseCases.Abstractions;
using Infrastructure.Models;

namespace Application.Services;

public class ConsumerBalanceService : IConsumerBalanceService
{
    private readonly IDepositToBalanceUseCase _depositToBalance;

    public ConsumerBalanceService(IDepositToBalanceUseCase depositToBalance)
    {
        _depositToBalance = depositToBalance;
    }

    public Task DepositAsync(DepositBalanceModel model, CancellationToken cancellationToken)
        => _depositToBalance.ExecuteAsync(model.ConsumerId, model.Amount, cancellationToken);
}