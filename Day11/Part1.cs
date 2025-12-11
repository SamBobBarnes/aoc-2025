namespace AOC25.Day11;

public class Part1() : BasePart(11, 1)
{
    public override string Run()
    {
        var input = Input().Select(x =>
        {
            var parts = x.Split(":");
            var outputs = parts[1].Trim().Split(' ');
            return new Node(parts[0], outputs.ToList());
        }).ToList();

        var startNode = input.First(x => x.Name == "you");
        var total = 0;

        Traverse(startNode, input, ref total);

        return total.ToString();
    }

    private void Traverse(Node node, List<Node> nodes, ref int total)
    {
        if (node.Outputs.Contains("out"))
        {
            total++;
            return;
        }
        foreach (var output in node.Outputs)
        {
            var nextNode = nodes.FirstOrDefault(x => x.Name == output);
            if (nextNode != null)
            {
                Traverse(nextNode, nodes, ref total);
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