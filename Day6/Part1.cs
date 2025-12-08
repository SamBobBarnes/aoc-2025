namespace AOC25.Day6;

public class Part1() : BasePart(6,1)
{
    public override string Run()
    {
        var input = Input();

        var math = input[..^1].Select(x => x.Split(' ').Where(y => y != "").Select(int.Parse).ToArray()).ToArray();
        var ops = input[^1].Split(' ').Where(x => x != "").ToArray();

        var runningTotal = 0L;
        for (int i = 0; i < ops.Length; i++)
        {
            var total = 0L;
            foreach (var line in math)
            {
                if (total == 0)
                {
                    total = line[i];
                }
                else if (ops[i] == "+")
                    total += line[i];
                else
                    total *= line[i];
            }

            runningTotal += total;
        }

        return runningTotal.ToString();
    }
}
