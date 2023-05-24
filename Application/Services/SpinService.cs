using System.Threading;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.UseCases.Abstractions;
using Infrastructure.Models;

namespace Application.Services;

public class SpinService : ISpinService
{
    private readonly IGridFillInUseCase _gridUseCase;
    private readonly ISpinUseCase _spinUseCase;
    private readonly IDepositToBalanceUseCase _depositToUseCase;

    public SpinService(IGridFillInUseCase gridUseCase, ISpinUseCase spinUseCase, IDepositToBalanceUseCase depositToUseCase)
    {
        _gridUseCase = gridUseCase;
        _spinUseCase = spinUseCase;
        _depositToUseCase = depositToUseCase;
    }

    public async Task<SpinResultModel> MakeSpinAsync(SpinAttemptModel request, CancellationToken cancellationToken)
    {
        var grid = await _gridUseCase.ExecuteAsync(request.GridId, cancellationToken);
        var spinResult = await _spinUseCase.ExecuteAsync(PrepareDomainModel(request, grid), cancellationToken);
        var balance = await _depositToUseCase.ExecuteAsync(request.ConsumerId, spinResult.Win, cancellationToken);

        return new SpinResultModel
        {
            Balance = balance.Balance,
            Win = spinResult.Win,
            Grid = grid.Grid,
        };
    }


    //TODO: all mapping should be moved to appropriate mappers profile
    private SpinAttempt PrepareDomainModel(SpinAttemptModel request, GridMatrix grid)
    {
        return new SpinAttempt
        {
            GridMatrix = grid,
            Bet = request.Bet,
        };
    }
}