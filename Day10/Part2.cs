namespace AOC25.Day10;

public class Part2() : BasePart(10,2,true)
{
    public override string Run()
    {
        var input = Input().Select(line => new Machine(line)).ToList();

        var total = 0;
        foreach(var machine in input)
        {
            var state = new State("(0,0,0,0,0)");
            var presses = new int[machine.ButtonWirings.Count];
            var index = 0;
            foreach (var button in machine.ButtonWirings)
            {
                while(true)
                {
                    // Press the button
                    state += button;

                    presses[index]++;

                    if (!state.ExceedsGoal(machine.GoalSetting)) continue;

                    // Undo the last press
                    state -= button;
                    presses[index]--;
                    break;
                }
                index++;
            }

            total += 0;
        }


        return total.ToString();
    }

    private class State(string config)
    {
        private readonly int[] _wiring = config.TrimStart('(').TrimEnd(')').Split(',').Select(int.Parse).ToArray();

        public static State operator +(State a, State b)
        {
            for (int i = 0; i < a._wiring.Length; i++)
            {
                a._wiring[i] += b._wiring[i];
            }

            return a;
        }

        public static State operator -(State a, State b)
        {
            for (int i = 0; i < a._wiring.Length; i++)
            {
                a._wiring[i] -= b._wiring[i];
            }

            return a;
        }

        public override string ToString()
        {
            return string.Join(",", _wiring);
        }

        public bool ExceedsGoal(State goal)
        {
            for (int i = 0; i < _wiring.Length; i++)
            {
                if (_wiring[i] > goal._wiring[i]) return true;
            }
            return false;
        }
    }

    private class Machine
    {
        public readonly State GoalSetting;
        public readonly List<State> ButtonWirings = [];
        public Machine(string config)
        {
            var parts = config.Split(' ');

            GoalSetting = new State(parts[0].TrimStart('{').TrimEnd('}'));
            foreach (var button in parts[1..^1])
            {
                ButtonWirings.Add(new State(button));
            }
        }

        public override string ToString()
        {
            return $"[{string.Join(",",GoalSetting)}]: ({string.Join("),(", ButtonWirings)})";
        }
    }
}
