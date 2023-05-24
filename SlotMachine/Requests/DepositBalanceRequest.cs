namespace SlotMachine.Requests;

public record DepositBalanceRequest
{
    public string ConsumerId { get; init; }

    public decimal Amount { get; init; }
}