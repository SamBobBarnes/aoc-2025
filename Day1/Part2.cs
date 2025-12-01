namespace AOC25.Day1;

public class Part2() : BasePart(1,2)
{
    public override string Run()
    {
        var input = Input().Select(x => (Direction: x.Substring(0, 1), Distance: int.Parse(x.Substring(1)))).ToList();

        var currentPosition = 50;
        var limit = 100;
        var total = 0;

        foreach (var rotation in input)
        {
            if(rotation.Direction == "R")
            {
                currentPosition += rotation.Distance;
                total += currentPosition/limit;
                currentPosition %= limit;
            }
            else
            {
                if(currentPosition != 0)
                    currentPosition = limit - currentPosition;
                currentPosition += rotation.Distance;
                total += currentPosition/limit;
                currentPosition %= limit;
                if(currentPosition != 0)
                    currentPosition = limit - currentPosition;
            }
        }

        return total.ToString();
    }
}
