namespace AOC25.Day3;

public class Part2() : BasePart(3,2)
{
    public override string Run()
    {
        var input = Input().Select(x => x.ToCharArray().Select(y => $"{y}").ToArray()).ToArray();

        var bankLength = input[0].Length;
        var total = 0L;

        foreach (var bank in input)
        {
            var batts = bank.Select(int.Parse).ToArray();
            var indices = new int[]
            {
                bankLength - 12,
                bankLength - 11,
                bankLength - 10,
                bankLength - 9,
                bankLength - 8,
                bankLength - 7,
                bankLength - 6,
                bankLength - 5,
                bankLength - 4,
                bankLength - 3,
                bankLength - 2,
                bankLength - 1,
            };

            for(int i = 0; i < 12; i++)
            {
                for(int j = indices[i]-1; j >= 0; j--)
                    if (i != 0 && j == indices[i - 1])
                        break;
                    else if (batts[j] >= batts[indices[i]])
                        indices[i] = j;
            }

            total += long.Parse(
                bank[indices[0]]
                + bank[indices[1]]
                + bank[indices[2]]
                + bank[indices[3]]
                + bank[indices[4]]
                + bank[indices[5]]
                + bank[indices[6]]
                + bank[indices[7]]
                + bank[indices[8]]
                + bank[indices[9]]
                + bank[indices[10]]
                + bank[indices[11]]
            );
        }

        return total.ToString();
    }
}
