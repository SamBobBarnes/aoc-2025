using Google.OrTools.LinearSolver;

namespace AOC25.Day10;

public class Part2() : BasePart(10,2)
{
    public override string Run()
    {
        var input = Input().Select(line => new Machine(line)).ToList();

        var total = 0;
        foreach(var machine in input)
        {
            var state = new State($"({string.Join(",", Enumerable.Repeat(0, machine.GoalState.Length))})");
            var solver = Solver.CreateSolver("SCIP");
            var x0 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x0");
            var x1 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x1");
            var x2 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x2");
            var x3 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x3");
            var x4 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x4");
            var x5 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x5");
            var x6 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x6");
            var x7 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x7");
            var x8 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x8");
            var x9 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x9");

            for(int i = 0; i < machine.GoalState.Length; i++)
            {
                var c = machine.ButtonWirings.Count;
                var w0 = c > 0 ? machine.ButtonWirings[0].Wiring.Contains(i) ? 1 : 0 : 0;
                var w1 = c > 1 ? machine.ButtonWirings[1].Wiring.Contains(i) ? 1 : 0 : 0;
                var w2 = c > 2 ? machine.ButtonWirings[2].Wiring.Contains(i) ? 1 : 0 : 0;
                var w3 = c > 3 ? machine.ButtonWirings[3].Wiring.Contains(i) ? 1 : 0 : 0;
                var w4 = c > 4 ? machine.ButtonWirings[4].Wiring.Contains(i) ? 1 : 0 : 0;
                var w5 = c > 5 ? machine.ButtonWirings[5].Wiring.Contains(i) ? 1 : 0 : 0;
                var w6 = c > 6 ? machine.ButtonWirings[6].Wiring.Contains(i) ? 1 : 0 : 0;
                var w7 = c > 7 ? machine.ButtonWirings[7].Wiring.Contains(i) ? 1 : 0 : 0;
                var w8 = c > 8 ? machine.ButtonWirings[8].Wiring.Contains(i) ? 1 : 0 : 0;
                var w9 = c > 9 ? machine.ButtonWirings[9].Wiring.Contains(i) ? 1 : 0 : 0;


                solver.Add(
                        x0 * w0 +
                           x1 * w1 +
                           x2 * w2 +
                           x3 * w3 +
                           x4 * w4 +
                           x5 * w5 +
                           x6 * w6 +
                           x7 * w7 +
                           x8 * w8 +
                           x9 * w9
                           == machine.GoalState.Wiring[i]
                        );
            }

            solver.Minimize(x0 + x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9);
            solver.Solve();
            var objective = solver.Objective();
            var x0Val = x0.SolutionValue();
            var x1Val = x1.SolutionValue();
            var x2Val = x2.SolutionValue();
            var x3Val = x3.SolutionValue();
            var x4Val = x4.SolutionValue();
            var x5Val = x5.SolutionValue();
            var x6Val = x6.SolutionValue();
            var x7Val = x7.SolutionValue();
            var x8Val = x8.SolutionValue();
            var x9Val = x9.SolutionValue();
            total += (int)objective.Value();
        }


        return total.ToString();
    }

    private class State(string config)
    {
        public readonly int[] Wiring = config.TrimStart('(').TrimEnd(')').Split(',').Select(int.Parse).ToArray();

        public int Length => Wiring.Length;
        public static State operator +(State a, State b)
        {
            for (int i = 0; i < b.Wiring.Length; i++)
            {
                a.Wiring[b.Wiring[i]]++;
            }

            return a;
        }

        public static State operator -(State a, State b)
        {
            for (int i = 0; i < b.Wiring.Length; i++)
            {
                a.Wiring[b.Wiring[i]]--;
            }

            return a;
        }

        public override string ToString()
        {
            return string.Join(",", Wiring);
        }

        public bool ExceedsGoal(State goal)
        {
            for (int i = 0; i < Wiring.Length; i++)
            {
                if (Wiring[i] > goal.Wiring[i]) return true;
            }
            return false;
        }

        public bool Equals(State other)
        {
            for (int i = 0; i < Wiring.Length; i++)
            {
                if (Wiring[i] != other.Wiring[i]) return false;
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
