using System.Threading;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.UseCases.Abstractions;
using Infrastructure.Models;

namespace Application.Services;

public class AdminService : IAdminService
{
    private readonly IModifyGridConfigUseCase _modifyGridUseCase;

    public AdminService(IModifyGridConfigUseCase modifyGridUseCase)
    {
        _modifyGridUseCase = modifyGridUseCase;
    }

    public Task SetupGridConfigurationAsync(GridConfigurationModel model, CancellationToken cancellationToken)
        => _modifyGridUseCase.ExecuteAsync(new GridConfiguration
        {
            Id = model.Id,
            Width = model.Width,
            Height = model.Height,
        }, cancellationToken);
}