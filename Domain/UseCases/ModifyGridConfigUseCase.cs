using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.UseCases.Abstractions;

namespace Domain.UseCases;

public class ModifyGridConfigUseCase : IModifyGridConfigUseCase
{
    private readonly IConfigurationRepository _configurationRepository;

    public ModifyGridConfigUseCase(IConfigurationRepository configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }

    public Task<GridConfiguration> ExecuteAsync(GridConfiguration gridConfiguration, CancellationToken cancellationToken)
        => _configurationRepository.UpdateAsync(gridConfiguration, cancellationToken);
}