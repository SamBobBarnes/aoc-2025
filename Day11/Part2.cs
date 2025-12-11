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
        var fftNode = input.First(x => x.Name == "fft");
        var dacNode = input.First(x => x.Name == "dac");
        var fftTotal = 0;
        var dacTotal = 0;

        Traverse(server, input, ref fftTotal, "fft");
        Traverse(server, input, ref dacTotal, "dac");

        return 0.ToString();
    }

    private void Traverse(Node node, List<Node> nodes, ref int total, string goal)
    {
        foreach (var output in node.Outputs)
        {
            if (node.Outputs.Contains(goal))
            {
                total++;
            }
            var nextNode = nodes.FirstOrDefault(x => x.Name == output);
            if (nextNode != null)
            {
                Traverse(nextNode, nodes, ref total, goal);
            }
        }
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
