using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.UseCases.Abstractions;

namespace Domain.UseCases;

public class SpinUseCase : ISpinUseCase
{
    //TODO: d
    private static (int, int)[] Directions = { (0, 1), (1, 1) };

    public Task<SpinAttemptResult> ExecuteAsync(SpinAttempt spin, CancellationToken cancellationToken)
    {
        ThrowOnNotValidBet(spin.Bet);
        //TODO: introduce validator to validate bet, grid
        
        var grid = spin.GridMatrix.Grid;
        var winAmount = CalculateWinAmount(grid, spin.Bet);

        return Task.FromResult(new SpinAttemptResult
        {
            Win = -spin.Bet + winAmount,
        });

        static void ThrowOnNotValidBet(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Bet amount must be greater than 0");
            }
        }
    }

    private static decimal CalculateWinAmount(int[][] grid, decimal bet)
    {
        var lenI = grid.Length;

        var result = 0M;
        for (var i = 0; i < lenI; i++)
        {
            var winsFromCell = CalculateWinsAmountFromCell(grid, i, 0, Directions[0], grid[i][0]) + CalculateWinsAmountFromCell(grid, i, 0, Directions[1], grid[i][0]); 
            result += winsFromCell * bet;

        }

        return result;
    }

    private static int CalculateWinsAmountFromCell(int[][] grid, int i, int j, (int ti, int tj) curDir, int curVal)
    {
        var wins = Dfs(grid, i, j, curDir, curVal);
        if (wins < 2)
        {
            return 0;
        }

        return wins * curVal;
    }

    private static int Dfs(int[][] grid, int i, int j, (int ti, int tj) curDir, int curVal)
    {
        var lenI = grid.Length;
        var lenJ = grid[0].Length;

        if (i < 0 || j < 0 || i == lenI || j == lenJ || grid[i][j] != curVal)
        {
            return 0;
        }

        var (ti, tj) = curDir;

        if (i + ti == lenI || i + ti < 0)
        {
            ti *= -1;
        }

        //if (j + tj == lenJ || j + tj < 0)
        //{
        //    tj *= -1;
        //}

        grid[i][j] = -1;

        var result = 1 + Dfs(grid, i + ti, j + tj, (ti, tj), curVal);

        grid[i][j] = curVal;

        return result;
    }
}