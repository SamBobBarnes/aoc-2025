using System.Diagnostics;

namespace AOC25.Day2;

public class Part1() : BasePart(2,1)
{
    public override string Run()
    {
        var input = Input()[0].Split(',').Select(x =>
        {
            var ends = x.Split('-').Select(long.Parse).ToArray();
            return new Range(ends[0], ends[1]);
        }).ToList();

        var possibleIds = input.Select(r =>
        {
            if(r.SmallestDigitLength != r.LargestDigitLength)
            {
                if (r.SmallestDigitLength % 2 == 0)
                {
                    return new Range(
                        r.Start,
                        Pow10(r.SmallestDigitLength)-1
                    );
                }
                return new Range(
                    Pow10(r.LargestDigitLength-1),
                    r.End
                );

            }
            if(r.SmallestDigitLength % 2 != 0)
            {
                return new Range(0, 0);
            }

            return r;
        }).Where(r => r != new Range(0,0)).ToList();

        var total = 0L;

        foreach(var r in possibleIds)
            for(long num = r.Start; num <= r.End; num++)
            {
                var digitLength = r.LargestDigitLength;
                var mid = digitLength / 2;
                var stringNum = num.ToString();
                var leftStr = stringNum.Substring(0, mid);
                var rightStr = stringNum.Substring(mid);
                if(leftStr == rightStr)
                    total += num;

            }

        return total.ToString();
    }

    private long Pow10(int exponent)
    {
       return  (long)System.Numerics.BigInteger.Pow(10, exponent);
    }

    private record Range(long Start, long End)
    {
        public bool Contains(long value) => value >= Start && value <= End;
        public long Size => End - Start + 1;
        public int SmallestDigitLength => Start.ToString().Length;
        public int LargestDigitLength => End.ToString().Length;
        public override string ToString() => $"{Start}-{End}; {Size} ({SmallestDigitLength}:{LargestDigitLength})";
    }
}
