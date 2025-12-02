namespace AOC25.Day2;

public class Part2() : BasePart(2,2)
{
    public override string Run()
    {
        var input = Input()[0].Split(',').Select(x =>
        {
            var ends = x.Split('-').Select(long.Parse).ToArray();
            return new Range(ends[0], ends[1]);
        }).ToList();

        var total = 0L;

        foreach(var r in input)
            for(long num = r.Start; num <= r.End; num++)
            {
                bool HasDupes(long number)
                {
                    var charNum = number.ToString().ToCharArray();
                    var digitLength = charNum.Length;
                    var mid = digitLength / 2;
                    for (int l = 1; l <= mid; l++)
                    {
                        var aPart = charNum[..(l)];
                        var bPart = charNum[l..(l + l)];
                        if (aPart.SequenceEqual(bPart))
                        {
                            if(digitLength % l != 0)
                                continue;
                            var reconstruction = new char[digitLength];
                            for (int pos = 0; pos < digitLength; pos += l)
                                for (int k = 0; k < l; k++)
                                    reconstruction[pos + k] = aPart[k];
                            if (reconstruction.SequenceEqual(charNum))
                                return true;
                        }
                    }

                    return false;
                }

                if (HasDupes(num))
                    total+=num;
            }

        return total.ToString();
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
