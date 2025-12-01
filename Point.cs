namespace AOC25;

public class Point(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;

    public override bool Equals(object? obj)
    {
        return obj is Point point && X == point.X && Y == point.Y;
    }

    protected bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{x},{y}";
    }
}