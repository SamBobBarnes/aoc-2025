namespace AOC25.Day10;

public class Part2() : BasePart(10,2,true)
{
    public override string Run()
    {
        var input = Input().Select(line => new Machine(line)).ToList();

        var total = 0;
        foreach(var machine in input)
        {
            var initialState = new int[machine.GoalSetting.Length];
            var dictionary = new Dictionary<int[], int>
            {
                [machine.GoalSetting] = int.MaxValue
            };
            var q = new Queue<(int[] state, int presses)>();
            q.Enqueue((initialState, 0));
            var visited = new HashSet<int[]>();

            while (q.Count > 0)
            {
                var (state, presses) = q.Dequeue();
                if(!visited.Add(state)) continue;

                if (state == machine.GoalSetting)
                {
                    dictionary[state] = presses;
                    break;
                }

                var nextPresses = presses + 1;
                foreach (var button in machine.ButtonWirings)
                {
                    var nextState = state.ToArray();
                    foreach (var wire in button)
                    {
                        nextState[wire]++;
                    }

                    if(nextState.Select((s, i) => machine.GoalSetting[i] < s).Any(x => x)) continue;
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
        public readonly int[] GoalSetting;
        public readonly List<int[]> ButtonWirings = [];
        public Machine(string config)
        {
            var parts = config.Split(' ');

            GoalSetting = ConfigToIntArray(parts[^1]);
            foreach (var button in parts[1..^1])
            {
                ButtonWirings.Add(button.TrimStart('(').TrimEnd(')').Split(',').Select(int.Parse).ToArray());
            }
        }

        private int[] ConfigToIntArray(string lights)
        {
            return lights.TrimStart('{').TrimEnd('}').Split(',').Select(int.Parse).ToArray();
        }

        public override string ToString()
        {
            var buttons = ButtonWirings.Select(b => string.Join(",",b));
            return $"[{string.Join(",",GoalSetting)}]: ({string.Join("),(", buttons)})";
        }
    }
}
