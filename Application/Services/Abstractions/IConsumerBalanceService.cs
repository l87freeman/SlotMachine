using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Application.Services.Abstractions;

public interface IConsumerBalanceService
{
    Task DepositAsync(DepositBalanceModel model, CancellationToken cancellationToken);
}