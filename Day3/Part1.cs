namespace AOC25.Day3;

public class Part1() : BasePart(3,1)
{
    public override string Run()
    {
        var input = Input().Select(x => x.ToCharArray().Select(y => $"{y}").ToArray()).ToArray();

        var total = 0;

        foreach (var bank in input)
        {
            var max = 0;
            for (int i = 0; i < bank.Length - 1; i++)
            {
                for(int j = i+1; j < bank.Length; j++)
                {
                    var joltage = int.Parse(bank[i] + bank[j]);
                    if (joltage > max)
                        max = joltage;
                }
            }

            total += max;
        }

        return total.ToString();
    }
}
