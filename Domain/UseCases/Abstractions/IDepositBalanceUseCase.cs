using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.UseCases.Abstractions;

public interface IDepositToBalanceUseCase
{
    Task<ConsumerBalance> ExecuteAsync(string consumerId, decimal amount, CancellationToken cancellationToken);
}