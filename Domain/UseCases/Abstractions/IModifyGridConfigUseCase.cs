using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.UseCases.Abstractions;

public interface IModifyGridConfigUseCase
{
    Task<GridConfiguration> ExecuteAsync(GridConfiguration gridConfiguration, CancellationToken cancellationToken);
}