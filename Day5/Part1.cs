namespace AOC25.Day5;

public class Part1() : BasePart(5,1)
{
    public override string Run()
    {
        var input = Input().ToList();
        var space = input.IndexOf("");
        var ranges = input[..space].Select(line =>
        {
            var parts = line.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);
            return (Start: start, End: end);
        }).ToList();
        var ids = input[(space + 1)..].Select(long.Parse);

        var total = 0;
        foreach (var id in ids)
        {
            foreach (var range in ranges)
            {
                if(id >= range.Start && id <= range.End)
                {
                    total++;
                    break;
                }
            }
        }

        return total.ToString();
    }
}
