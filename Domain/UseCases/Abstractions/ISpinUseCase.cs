using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.UseCases.Abstractions;

public interface ISpinUseCase
{
    Task<SpinAttemptResult> ExecuteAsync(SpinAttempt spinAttempt, CancellationToken cancellationToken);
}