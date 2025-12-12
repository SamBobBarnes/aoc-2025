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
            var x10 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x10");
            var x11 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x11");
            var x12 = solver.MakeIntVar(0.0, double.PositiveInfinity, "x12");

            var matrix = new int[machine.GoalState.Length][];
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
                var w10 = c > 10 ? machine.ButtonWirings[10].Wiring.Contains(i) ? 1 : 0 : 0;
                var w11 = c > 11 ? machine.ButtonWirings[11].Wiring.Contains(i) ? 1 : 0 : 0;
                var w12 = c > 12 ? machine.ButtonWirings[12].Wiring.Contains(i) ? 1 : 0 : 0;

                matrix[i] = [
                    w0, w1, w2, w3, w4, w5, w6, w7, w8, w9, w10, w11, w12
                ];
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
                           x9 * w9 +
                           x10 * w10 +
                           x11 * w11 +
                           x12 * w12
                           == machine.GoalState.Wiring[i]
                        );
            }

            for(int y = 0; y < machine.GoalState.Length; y++)
            {
                Console.Write("| ");
                for(int x = 0; x < machine.ButtonWirings.Count; x++)
                {
                    Console.Write($"{matrix[y][x]} ");
                }
                Console.WriteLine($"{machine.GoalState.Wiring[y],3} |");
            }

            solver.Minimize(x0 + x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9 + x10 + x11 + x12);
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
            var x10Val = x10.SolutionValue();
            var x11Val = x11.SolutionValue();
            var x12Val = x12.SolutionValue();
            machine.CheckAnswer([
                (int)x0Val,
                (int)x1Val,
                (int)x2Val,
                (int)x3Val,
                (int)x4Val,
                (int)x5Val,
                (int)x6Val,
                (int)x7Val,
                (int)x8Val,
                (int)x9Val,
                (int)x10Val,
                (int)x11Val,
                (int)x12Val
            ]);
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


        public bool CheckAnswer(int[] answer)
        {
            var state = new State(new string('0', GoalState.Length).Replace("0", "0,").TrimEnd(','));
            for (int i = 0; i < ButtonWirings.Count; i++)
            {
                var a = answer[i];
                for (int j = 0; j < a; j++)
                {
                    state += ButtonWirings[i];
                }
            }
            var match = state.Equals(GoalState);

            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < ButtonWirings.Count; i++)
            {
                Console.Write($"x{i}: {answer[i]},  ");
            }
            Console.WriteLine($"Total: {answer.Sum()}");
            Console.ForegroundColor = match ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Computed State: {state}");
            Console.ResetColor();
            Console.WriteLine($"Goal State:     {GoalState}");

            return match;
        }
    }
}
