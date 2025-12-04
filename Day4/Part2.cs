namespace AOC25.Day4;

public class Part2() : BasePart(4,2)
{
    public override string Run()
    {
        var input = Input().Select(r => r.Select(c => c == '@').ToArray()).ToArray();

        var width = input[0].Length;
        var height = input.Length;

        var total = 0;
        var paperRemovedThisRound = true;
        while (paperRemovedThisRound)
        {
            var localTotal = 0;
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (!input[y][x])
                    continue;
                var borderingCells = new bool[8]
                {
                    y > 0 && x > 0 && input[y - 1][x - 1],
                    y > 0 && input[y - 1][x],
                    y > 0 && x < width - 1 && input[y - 1][x + 1],
                    x > 0 && input[y][x - 1],
                    x < width - 1 && input[y][x + 1],
                    y < height - 1 && x > 0 && input[y + 1][x - 1],
                    y < height - 1 && input[y + 1][x],
                    y < height - 1 && x < width - 1 && input[y + 1][x + 1],
                };

                if (borderingCells.Sum(c => c ? 1 : 0) < 4)
                {
                    localTotal++;
                    input[y][x] = false;
                }
            }
            if (localTotal == 0)
                paperRemovedThisRound = false;
            total += localTotal;
        }

        return total.ToString();
    }
}
