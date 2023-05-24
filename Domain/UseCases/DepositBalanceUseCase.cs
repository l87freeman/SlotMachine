using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UseCases.Abstractions;

namespace Domain.UseCases;

public class DepositToBalanceUseCase : IDepositToBalanceUseCase
{
    private readonly IBalanceRepository _repository;

    public DepositToBalanceUseCase(IBalanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConsumerBalance> ExecuteAsync(string consumerId, decimal amount, CancellationToken cancellationToken)
    {
        //TODO: additional validation should be extracted to separate validator

        var consumerBalance = await GetConsumerBalanceAsync(consumerId, cancellationToken);

        consumerBalance.UpdateBalance(amount);

        await _repository.UpdateAsync(consumerBalance, cancellationToken);

        return consumerBalance;
    }

    private async Task<ConsumerBalance> GetConsumerBalanceAsync(string consumerId, CancellationToken cancellationToken)
    {
        try
        {
            var consumerBalance = await _repository.GetAsync(consumerId, cancellationToken);
            return consumerBalance;
        }
        catch (EntityNotFoundException)
        {
            return new ConsumerBalance(consumerId, 0, 0);
        }
    }
}