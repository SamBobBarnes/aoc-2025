namespace AOC25.Day10;

public class Part2() : BasePart(10,2,true)
{
    public override string Run()
    {
        var input = Input().Select(line => new Machine(line)).ToList();

        var total = 0;
        foreach(var machine in input)
        {
            var state = new State($"({string.Join(",", Enumerable.Repeat(0, machine.GoalState.Length))})");
            var presses = GetButtonPresses(state, machine, 0, new int[machine.ButtonWirings.Count]);

            total += presses.Sum();
        }


        return total.ToString();
    }

    private int[] GetButtonPresses(State state, Machine machine, int buttonIndex, int[] presses)
    {
        presses[buttonIndex] = 0;
        while(!state.ExceedsGoal(machine.GoalState))
        {
             state += machine.ButtonWirings[buttonIndex];
             presses[buttonIndex]++;
        }

        state -= machine.ButtonWirings[buttonIndex];
        presses[buttonIndex]--;

        if (buttonIndex == machine.ButtonWirings.Count - 1) // last button
        {
            if (state.Equals(machine.GoalState)) return presses;
            presses[buttonIndex] = -1; // go back up
            return presses;
        }

        var result = GetButtonPresses(state, machine, buttonIndex + 1, presses);
        while(result[buttonIndex + 1] == -1 && result[buttonIndex] > 0)
        {
            state -= machine.ButtonWirings[buttonIndex];
            presses[buttonIndex]--;
            result = GetButtonPresses(state, machine, buttonIndex + 1, presses);
        }

        if(result[buttonIndex + 1] == -1)
        {
            presses[buttonIndex] = -1;
        }

        return presses;
    }

    private class State(string config)
    {
        private readonly int[] _wiring = config.TrimStart('(').TrimEnd(')').Split(',').Select(int.Parse).ToArray();

        public int Length => _wiring.Length;
        public static State operator +(State a, State b)
        {
            for (int i = 0; i < b._wiring.Length; i++)
            {
                a._wiring[b._wiring[i]]++;
            }

            return a;
        }

        public static State operator -(State a, State b)
        {
            for (int i = 0; i < b._wiring.Length; i++)
            {
                a._wiring[b._wiring[i]]--;
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

        public bool Equals(State other)
        {
            for (int i = 0; i < _wiring.Length; i++)
            {
                if (_wiring[i] != other._wiring[i]) return false;
            }
            return true;
        }
    }

    private class Machine
    {
        public readonly State GoalState;
        public readonly List<State> ButtonWirings = [];
        public Machine(string config)
        {
            var parts = config.Split(' ');

            GoalState = new State(parts[^1].TrimStart('{').TrimEnd('}'));
            foreach (var button in parts[1..^1])
            {
                ButtonWirings.Add(new State(button));
            }
        }

        public override string ToString()
        {
            return $"[{string.Join(",",GoalState)}]: ({string.Join("),(", ButtonWirings)})";
        }
    }
}
