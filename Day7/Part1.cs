namespace AOC25.Day7;

public class Part1() : BasePart(7,1)
{
    public override string Run()
    {
        var input = Input();
        var start = new Point(0, 0);

        for (int x = 0; x < input[0].Length; x++)
            if (input[0][x] == 'S')
                start = new Point(x, 0);

        var balls = new bool[input[0].Length];
        balls[start.X] = true;
        var total = 0;

        for (int y = 2; y < input.Length; y += 2)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] == '^' && balls[x])
                {
                    balls[x] = false;
                    balls[x - 1] = true;
                    balls[x + 1] = true;
                    total++;
                }
            }
        }

        return total.ToString();
    }
}
