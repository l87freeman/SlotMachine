namespace Domain.Entities;

public class GridMatrix
{
    public GridMatrix(int height, int width)
    {
        Grid = new int[height][];
        for (int i = 0; i < height; i++)
        {
            Grid[i] = new int[width];
        }
    }

    public int[][] Grid { get; }
}