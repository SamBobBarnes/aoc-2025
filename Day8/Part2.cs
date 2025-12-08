using System.Text;

namespace AOC25.Day8;

public class Part2() : BasePart(8,2)
{
    public override string Run()
    {
        var circuits = Input().Select(x => new Circuit(x)).ToList();
        (double Distance, Point3D A, Point3D B) lastPair = (0, new(0,0,0), new(0,0,0));

        while (circuits.Count > 1)
        {
            var minDistance = double.MaxValue;
            var pair = new List<Circuit>();
            for (int i = 0; i < circuits.Count-1; i++)
            for (int j = i + 1;  j < circuits.Count; j++)
            {
                var a = circuits[i];
                var b = circuits[j];

                var distance = a.GetDistance(b);
                if (minDistance > distance.Distance)
                {
                    minDistance = distance.Distance;
                    lastPair = distance;
                    pair.Clear();
                    pair.AddRange([a, b]);
                }
            }

            circuits.Remove(pair[0]);
            circuits.Remove(pair[1]);
            circuits.Add(new(pair[0], pair[1]));
        }


        return ((long)lastPair.A.X * lastPair.B.X).ToString();
    }

    private class Circuit
    {
        private readonly List<Point3D> _positions = [];
        public Circuit(string pos)
        {
            var xyz = pos.Split(',').Select(int.Parse).ToArray();
            _positions.Add(new(xyz[0], xyz[1], xyz[2]));
        }

        public Circuit(Circuit a, Circuit b)
        {
            _positions.AddRange(a._positions);
            _positions.AddRange(b._positions);
            _positions = _positions.Distinct().ToList();
        }

        public int Count => _positions.Count;

        public (double Distance, Point3D A, Point3D B) GetDistance(Circuit o)
        {
            (double Distance, Point3D A, Point3D B) pair = (double.MaxValue, new(0,0,0), new(0,0,0));

            foreach(var b in o._positions)
            foreach (var a in _positions)
            {
                var localDistance = Math.Sqrt(
                                                Math.Pow(b.X - a.X, 2)
                                              + Math.Pow(b.Y - a.Y, 2)
                                              + Math.Pow(b.Z - a.Z, 2)
                                            );
                if (localDistance < pair.Distance)
                    pair = (localDistance, a, b);
            }

            return pair;
        }

        public bool ContainsAny(Circuit o)
        {
            foreach (var p in o._positions)
            {
                if (_positions.Contains(p))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Equals(object? obj)
        {
            return obj is Circuit circuit && Equals(circuit);
        }

        protected bool Equals(Circuit other)
        {
            return _positions.Equals(other._positions);
        }

        public override int GetHashCode()
        {
            return _positions.GetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Count}: ");
            foreach (var p in _positions)
                sb.Append(p + "; ");
            return sb.ToString();
        }
    }
}
