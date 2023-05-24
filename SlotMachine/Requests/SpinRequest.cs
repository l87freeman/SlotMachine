namespace SlotMachine.Requests;

public record SpinRequest
{
    public string ConsumerId { get; init; }

    public decimal Bet { get; init; }

    public string GridId { get; init; }
}