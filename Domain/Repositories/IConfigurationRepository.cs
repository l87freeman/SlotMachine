using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories;

public interface IConfigurationRepository
{
    public Task<GridConfiguration> GetAsync(string id, CancellationToken cancellationToken);

    Task<GridConfiguration> UpdateAsync(GridConfiguration model, CancellationToken cancellationToken);
}