using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.UseCases.Abstractions;

namespace Domain.UseCases;

public class GridFillInUseCase : IGridFillInUseCase
{
    private static Random Random = new();
    private readonly IConfigurationRepository _configurationRepository;

    public GridFillInUseCase(IConfigurationRepository configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }

    public async Task<GridMatrix> ExecuteAsync(string gridId, CancellationToken cancellationToken)
    {
        var gridMatrix = await CreateGridMatrixAsync(gridId, cancellationToken);
        PopulateGridMatrix(gridMatrix);
        
        return gridMatrix;
    }

    private static void PopulateGridMatrix(GridMatrix gridMatrix)
    {
        var grid = gridMatrix.Grid;
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                grid[row][col] = Random.Next(0, 9);
            }
        }
    }

    private async Task<GridMatrix> CreateGridMatrixAsync(string gridId, CancellationToken cancellationToken)
    {
        var gridConfig = await _configurationRepository.GetAsync(gridId, cancellationToken);

        var gridMatrix = new GridMatrix(gridConfig.Height, gridConfig.Width);

        return gridMatrix;
    }
}