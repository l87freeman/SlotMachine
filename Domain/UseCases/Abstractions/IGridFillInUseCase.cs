using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Domain.UseCases.Abstractions;

public interface IGridFillInUseCase
{
    Task<GridMatrix> ExecuteAsync(string gridId, CancellationToken cancellationToken);
}