using System.Text;

namespace AOC25.Day8;

public class Part1() : BasePart(8,1,true)
{
    public override string Run()
    {
        var circuits = Input().Select(x => new Circuit(x)).ToList();
        var pairs = new List<(double Distance, Circuit C)>();

        // var limit = 1000;
        var limit = 10;
        var distanceMinimum = 0d;

        for(int t = 0; t < limit; t++)
        {
            var minDistance = double.MaxValue;
            var pair = new List<Circuit>();
            for (int i = 0; i < circuits.Count-1; i++)
            for (int j = i + 1;  j < circuits.Count; j++)
            {
                var a = circuits[i];
                var b = circuits[j];

                var distance = a.GetDistance(b);
                if (minDistance > distance && distance > distanceMinimum)
                {
                    minDistance = distance;
                    pair.Clear();
                    pair.AddRange([a, b]);
                }
            }

            pairs.Add((minDistance, new(pair[0], pair[1])));
            pair.Clear();

            distanceMinimum = minDistance;
        }

        var circuitCollections = pairs.Select(x => x.C).ToList();
        var joins = false;
        do
        {
            for (int i = 0; i < circuitCollections.Count - 1; i++)
            {
                var circuitsJoined = 0;
                for (int j = i + 1; j < circuitCollections.Count; j++)
                {
                    var a = circuitCollections[i];
                    var b = circuitCollections[j];
                    if (a.ContainsAny(b))
                    {
                        circuitsJoined++;
                        circuitCollections.Remove(a);
                        circuitCollections.Remove(b);
                        circuitCollections.Add(new(a, b));
                        joins = true;
                        break;
                    }
                }

                if (circuitsJoined > 0)
                    break;
                joins = false;
            }
        } while (joins);

        circuitCollections.Sort((a, b) => b.Count - a.Count);

        var total = 1L;
        foreach (var c in circuitCollections[..3])
            total *= c.Count;
        return total.ToString();
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

        public double GetDistance(Circuit o)
        {
            var distance = double.MaxValue;

            foreach(var b in o._positions)
            foreach (var a in _positions)
            {
                var localDistance = Math.Sqrt(
                                                Math.Pow(b.X - a.X, 2)
                                              + Math.Pow(b.Y - a.Y, 2)
                                              + Math.Pow(b.Z - a.Z, 2)
                                            );
                if (localDistance < distance) distance = localDistance;
            }

            return distance;
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
