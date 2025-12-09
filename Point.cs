namespace AOC25;

public class Point(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;

    public Point() : this(0,0) { }

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

public class Point3D(int x, int y, int z): Point(x,y)
{
    public readonly int Z = z;

    public override bool Equals(object? obj)
    {
        return obj is Point3D point && X == point.X && Y == point.Y && Z == point.Z;
    }

    public bool Equals(Point3D? other)
    {
        if(other == null) return false;
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }

    public string ToString(int normalLength)
    {
        var points = "";
        points += $"{X}".PadLeft(normalLength, ' ') + ",";
        points += $"{Y}".PadLeft(normalLength, ' ') + ",";
        points += $"{Z}".PadLeft(normalLength, ' ');
        return points;
    }
}