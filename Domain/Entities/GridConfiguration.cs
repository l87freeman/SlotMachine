namespace Domain.Entities;

public record GridConfiguration
{
    public string Id { get; init; }

    public int Height { get; init; }

    public int Width { get; init; }
}