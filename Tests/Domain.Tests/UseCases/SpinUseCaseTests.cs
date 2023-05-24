using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.UseCases;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.UseCases;

public class SpinUseCaseTests
{
    private readonly SpinUseCase _useCase;

    public SpinUseCaseTests()
    {
        _useCase = new SpinUseCase();
    }

    [Theory]
    [MemberData(nameof(TestCaseProvider))]
    public async Task ExecuteAsync_WhenGridGiven_ThenReturnExpectedNumberOfWins(int expectedWins, int[][] gridSetup)
    {
        var bet = 10;
        var grid = PrepareGrid(gridSetup);

        var result = await _useCase.ExecuteAsync(new SpinAttempt
        {
            GridMatrix = grid,
            Bet = bet,
        }, CancellationToken.None);


        result.Win.Should().Be(bet * expectedWins - bet);
    }

    [Fact]
    public async Task ExecuteAsync_WhenBetIsLessThanZero_ThenArgumentExceptionThrown()
    {
        var bet = -10;
        var grid = PrepareGrid(new[] { 3, 3, 3, 4, 5 }, new[] { 2, 3, 2, 3, 3 }, new[] { 1, 2, 3, 3, 3 });

        await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(new SpinAttempt
        {
            GridMatrix = grid,
            Bet = bet,
        }, CancellationToken.None));
    }

    //TODO: no tests for not valid grids
    public static IEnumerable<object[]> TestCaseProvider()
    {
        yield return new object[] { 27, new[] { new[] { 3, 3, 3, 4, 5 }, new[] { 2, 3, 2, 3, 3 }, new[] { 1, 2, 3, 3, 3 } } };
        yield return new object[] { 0, new[] { new[] { 0, 3, 3, 4, 5 }, new[] { 0, 3, 2, 3, 3 }, new[] { 0, 2, 3, 3, 3 }, new[] { 0, 2, 3, 3, 3 } } };
        yield return new object[] { 32, new[] { new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 } } };
        yield return new object[] { 0, new[] { new[] { 1 }, new[] { 1 }, new[] { 1 }, new[] { 1 } } };

    }

    private static GridMatrix PrepareGrid(params int[][] rows)
    {
        var grid = new GridMatrix(rows.Length, rows[0].Length);
        for (var i = 0; i < rows.Length; i++)
        {
            for (var j = 0; j < rows[i].Length; j++)
            {
                grid.Grid[i][j] = rows[i][j];
            }
        }

        return grid;
    }
}