namespace SlotMachine.Requests;

public record GridConfigurationRequest
{
    public string GridId { get; init; }

    public int Height { get; init; }

    public int Width { get; init; }
}