namespace SlotMachine.Responses;

public record SpinResponse
{
    public int[][] Grid { get; init; }

    public decimal Win { get; init; }

    public decimal Balance { get; init; }
}