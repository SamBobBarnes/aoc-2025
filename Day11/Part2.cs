namespace AOC25.Day11;

public class Part2() : BasePart(11,2)
{
    public override string Run()
    {
        var input = Input().Select(x =>
        {
            var parts = x.Split(":");
            var outputs = parts[1].Trim().Split(' ');
            return new Node(parts[0], outputs.ToList());
        }).ToList();

        var server = input.First(x => x.Name == "svr");

        var memo = new Dictionary<State, long>();
        var total = 0L;

        total += Traverse("svr", input, false, false, ref memo);

        return total.ToString();
    }

    private record State(string NodeName, bool fftFound, bool dacFound);

    private long Traverse(string nodeName, List<Node> nodes, bool fft, bool dac, ref Dictionary<State, long> memo)
    {
        if(nodeName == "fft")
            fft = true;
        if(nodeName == "dac")
            dac = true;
        if (nodeName == "out")
            return fft && dac ? 1 : 0;

        var state = new State(nodeName, fft, dac);
        if (memo.TryGetValue(state, out var traverse))
            return traverse;

        var sum = 0L;
        var node = nodes.First(x => x.Name == nodeName);
        foreach (var output in node.Outputs)
        {
            sum += Traverse(output, nodes, fft, dac, ref memo);
        }

        memo[state] = sum;
        return sum;
    }


    private class Node(string name, List<string> outputs)
    {
        public readonly string Name = name;
        public List<string> Outputs = outputs;

        public override string ToString()
        {
            return $"{Name}: {string.Join(", ", Outputs)}";
        }
    }
}
