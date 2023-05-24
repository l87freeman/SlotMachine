using Infrastructure.Models;
using SlotMachine.Requests;
using SlotMachine.Responses;

namespace SlotMachine.Extensions;

public static class ModelExtensions
{
    //TODO: move everything from here to mapper
    public static SpinAttemptModel ToModel(this SpinRequest request)
    {
        return new SpinAttemptModel
        {
            Bet = request.Bet,
            ConsumerId = request.ConsumerId,
            GridId = request.GridId,
        };
    }

    public static SpinResponse ToResponse(this SpinResultModel model)
    {
        return new SpinResponse
        {
            Balance = model.Balance,
            Grid = model.Grid,
            Win = model.Win,
        };
    }

    public static DepositBalanceModel ToModel(this DepositBalanceRequest request)
    {
        return new DepositBalanceModel
        {
            ConsumerId = request.ConsumerId,
            Amount = request.Amount,
        };
    }

    public static GridConfigurationModel ToModel(this GridConfigurationRequest request)
    {
        return new GridConfigurationModel
        {
            Height = request.Height,
            Width = request.Width,
            Id = request.GridId,
        };
    }
}