namespace AOC25.Day5;

public class Part2() : BasePart(5,2)
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

        var newRanges = new List<(long Start, long End)>();

        var total = 0L;
        var modified = false;
        do
        {
            foreach (var r in ranges)
            {
                var added = false;
                for (int i = 0; i < newRanges.Count; i++)
                {
                    var nr = newRanges[i];
                    if (r.Start >= nr.Start && r.Start <= nr.End && r.End > nr.End)
                    {
                        newRanges[i] = (nr.Start, r.End);
                        added = true;
                        break;
                    }

                    if (r.End >= nr.Start && r.End <= nr.End && r.Start < nr.Start)
                    {
                        newRanges[i] = (r.Start, nr.End);
                        added = true;
                        break;
                    }

                    if (r.Start < nr.Start && r.End > nr.End)
                    {
                        newRanges[i] = (r.Start, r.End);
                        added = true;
                        break;
                    }

                    if (r.Start >= nr.Start && r.End <= nr.End)
                    {
                        added = true;
                    }
                }

                if (!added)
                {
                    newRanges.Add(r);
                }
            }
            if(newRanges.Count != ranges.Count)
            {
                modified = true;
                ranges = new List<(long Start, long End)>(newRanges);
                newRanges.Clear();
            }
            else
            {
                modified = false;
            }
        } while (modified);

        foreach(var nr in newRanges)
        {
            total += (nr.End - nr.Start + 1);
        }

        return total.ToString();
    }
}
