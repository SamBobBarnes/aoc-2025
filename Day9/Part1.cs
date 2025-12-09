namespace AOC25.Day9;

public class Part1() : BasePart(9,1)
{
    public override string Run()
    {
        var input = Input().Select(x => x.Split(',').Select(int.Parse).ToArray()).Select(x => new Point(x[0],x[1])).ToList();

        var max = 0;
        var maxPointA = new Point(0,0);
        var maxPointB = new Point(0,0);
        for(int i = 0; i < input.Count-1; i++)
        for (int j = i + 1; j < input.Count; j++)
        {
            var a = input[i];
            var b = input[j];
            var dist = Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
            if (dist > max)
            {
                max = dist;
                maxPointA = a;
                maxPointB = b;
            }
        }

        var area = (long)(Math.Abs(maxPointA.X - maxPointB.X) + 1) * (Math.Abs(maxPointA.Y - maxPointB.Y) + 1);

        return area.ToString();
    }
}
