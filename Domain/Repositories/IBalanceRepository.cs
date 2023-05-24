using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories;

public interface IBalanceRepository
{
    Task<ConsumerBalance> GetAsync(string consumerId, CancellationToken cancellationToken);

    Task<ConsumerBalance> UpdateAsync(ConsumerBalance balance, CancellationToken cancellationToken);
}