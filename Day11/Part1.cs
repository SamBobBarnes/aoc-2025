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

        var q = new Queue<string>();
        q.Enqueue(startNode.Name);
        var visited = new List<string>();
        while(q.Count > 0)
        {
            var current = q.Dequeue();
            if (visited.Contains(current))
            {
                continue;
            }
            visited.Add(current);
            var node = input.First(x => x.Name == current);
            foreach (var output in node.Outputs)
            {
                if (output == "out") continue;
                q.Enqueue(output);
            }
        }

        var reachableNodes = input.Where(x => visited.Contains(x.Name)).ToList();

        var step = reachableNodes.Where(n => n.Outputs.Contains("out")).ToList();
        while (step.Count > 0)
        {
            total += step.Count;
            var nextStep = new List<Node>();
            foreach (var node in step)
            {
                if(node.Name == "you")
                {
                    continue;
                }
                var parentQ = new Queue<Node>(reachableNodes.FindAll(n => n.Outputs.Contains(node.Name)));
                while (parentQ.Count > 0)
                {
                    var parent = parentQ.Dequeue();
                    if (parent.Outputs.Count == 1)
                    {
                        foreach(var subParent in reachableNodes.FindAll(n => n.Outputs.Contains(parent.Name)))
                            parentQ.Enqueue(subParent);
                        continue;
                    }
                    nextStep.Add(parent);
                }
            }

            step = nextStep.Distinct().Where(n => n.Name != "you").ToList();
        }

        return total.ToString();
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