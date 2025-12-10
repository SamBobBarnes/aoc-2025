namespace AOC25.Day10;

public class Part1() : BasePart(10,1)
{
    public override string Run()
    {
        var input = Input().Select(line => new Machine(line)).ToList();

        var total = 0;
        foreach(var machine in input)
        {
            var dictionary = new Dictionary<int, int>
            {
                [machine.GoalSetting] = int.MaxValue
            };
            var q = new Queue<(int state, int presses)>();
            q.Enqueue((0, 0));

            while (q.Count > 0)
            {
                var (state, presses) = q.Dequeue();
                if (state == machine.GoalSetting)
                {
                    dictionary[state] = presses;
                    break;
                }

                var nextPresses = presses + 1;
                foreach (var button in machine.ButtonWirings)
                {
                    var nextState = state ^ button;
                    if (dictionary.ContainsKey(nextState) && dictionary[nextState] <= nextPresses) continue;

                    dictionary[nextState] = nextPresses;
                    q.Enqueue((nextState, nextPresses));
                }
            }
            total += dictionary[machine.GoalSetting];
        }


        return total.ToString();
    }

    private class Machine
    {
        public readonly int GoalSetting;
        public readonly List<int> ButtonWirings = [];
        public Machine(string config)
        {
            var parts = config.Split(' ');

            GoalSetting = ConfigToInt(parts[0]);
            foreach (var button in parts[1..^1])
            {
                var buttons = button.TrimStart('(').TrimEnd(')').Split(',').Select(int.Parse).ToArray();
                var byteMapping = 0;
                foreach (var wire in buttons)
                {
                    byteMapping += GetIntFromButtonWiring(wire);
                }
                ButtonWirings.Add(byteMapping);
            }
        }

        private int ConfigToInt(string lights)
        {
            var result = 0;
            var lightChars = lights.TrimStart('[').TrimEnd(']').ToCharArray();
            for(int i = 0; i < lightChars.Length; i++)
            {
                if(lightChars[i] == '#') result += (int)Math.Pow(2, i);
            }

            return result;
        }

        private int GetIntFromButtonWiring(int button)
        {
            return (int)Math.Pow(2, button);
        }

        public override string ToString()
        {
            var buttons = ButtonWirings.Select(bw => $"{bw:b10}");
            return $"[{GoalSetting:b10}]: ({string.Join("),(", buttons)})";
        }
    }
}
