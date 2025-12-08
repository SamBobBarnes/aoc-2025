namespace AOC25.Day7;

public class Part2() : BasePart(7,2)
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
        var totals = new long[input[0].Length];
        totals[start.X] = 1;

        for (int y = 2; y < input.Length; y += 2)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] == '^' && balls[x])
                {
                    balls[x] = false;
                    balls[x - 1] = true;
                    balls[x + 1] = true;
                    totals[x - 1] += totals[x];
                    totals[x + 1] += totals[x];
                    totals[x] = 0;
                }
            }
        }

        return totals.Sum().ToString();
    }
}
