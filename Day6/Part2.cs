namespace AOC25.Day6;

public class Part2() : BasePart(6,2)
{
    public override string Run()
    {
        var input = Input();

        var math = input[..^1].Select(x => x.Split(' ').Where(y => y != "").Select(int.Parse).ToArray()).ToArray();
        var ops = input[^1].Split(' ').Where(x => x != "").ToArray();
        var width = math.Length;

        var correctDigits = new List<List<int>>();
        var currentNumberList = new List<int>();
        for (int i = 0; i < input[0].Length; i++)
        {
            var currentNumber = "";
            var emptyNumber = "";
            for (int j = 0; j < width; j++)
            {
                currentNumber += input[j][i];
                emptyNumber += " ";
            }

            if (currentNumber == emptyNumber)
            {
                correctDigits.Add(currentNumberList);
                currentNumberList = new List<int>();
            }
            else
            {
                currentNumberList.Add(int.Parse(currentNumber));
            }
        }

        correctDigits.Add(currentNumberList);

        var runningTotal = 0L;
        for (int i = 0; i < ops.Length; i++)
        {
            var total = 0L;
            foreach (var num in correctDigits[i])
            {
                if (total == 0)
                {
                    total = num;
                }
                else if (ops[i] == "+")
                    total += num;
                else
                    total *= num;
            }

            runningTotal += total;
        }

        return runningTotal.ToString();
    }
}
