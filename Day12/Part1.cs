namespace AOC25.Day12;

public class Part1() : BasePart(12,1,true)
{
    public override string Run()
    {
        var input = Input().ToList();

        var endOfPackages = input.LastIndexOf("");

        var packages = new List<Package>();
        for (int i = 0; i < endOfPackages; i += 5)
        {
            packages.Add(new Package(input.Skip(i).Take(4).ToArray()));
        }

        var areas = new List<(int Width, int Height, int[] PackagesRequired)>();
        for (int i = endOfPackages + 1; i < input.Count; i++)
        {
            var parts = input[i].Split(' ');
            var width = int.Parse(parts[0].TrimEnd(':').Split('x')[0]);
            var height = int.Parse(parts[0].TrimEnd(':').Split('x')[1]);
            var packagesRequired = parts.Skip(1).Select(int.Parse).ToArray();

            var totalAreaProvided = width * height;
            var totalAreaOfPackages = packagesRequired.Select((count, i )=> count * packages.First(p => p.Id == i).Area).Sum();
            if (totalAreaProvided >= totalAreaOfPackages)
            {
                areas.Add((width, height, packagesRequired));
            }
        }

        return 0.ToString();
    }

    private class Package
    {
        public readonly int Id;
        private readonly bool[][] _definition;
        public List<bool[][]> Permutations;
        public int Area => _definition.Select(b => b.Select(v => v ? 1 : 0).Sum()).Sum();

        private const int Width = 3;
        private const int Height = 3;

        public Package(string[] lines)
        {
            Id = int.Parse(lines[0].TrimEnd(':'));
            _definition = new bool[Height][];
            for(int y = 0; y < Height; y++)
            {
                _definition[y] = new bool[Width];
                for (int x = 0; x < Width; x++)
                {
                    _definition[y][x] = lines[y + 1][x] == '#';
                }
            }

            Permutations = GetAllPermutations();
        }

        private List<bool[][]> GetAllPermutations()
        {
            var permutations = Enumerable.Range(0, 8).Select(_ => new bool[Height][]).ToList();

            for (int y = 0; y < Height; y++)
            {
                permutations[0][y] = new bool[Width];
                permutations[1][y] = new bool[Width];
                permutations[2][y] = new bool[Width];
                permutations[3][y] = new bool[Width];
                permutations[4][y] = new bool[Width];
                permutations[5][y] = new bool[Width];
                permutations[6][y] = new bool[Width];
                permutations[7][y] = new bool[Width];
            }

            for(int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
            {
                permutations[0][y][x] = _definition[y][x]; // 0 degrees
                permutations[1][x][Width - 1 - y] = _definition[y][x]; // 90 degrees
                permutations[2][Height - 1 - y][Width - 1 - x] = _definition[y][x]; // 180 degrees
                permutations[3][Height - 1 - x][y] = _definition[y][x]; // 270 degrees

                permutations[4][y][Width - 1 - x] = _definition[y][x]; // flip 0 degrees
                permutations[5][x][y] = _definition[y][x]; // flip 90 degrees
                permutations[6][Height - 1 - y][x] = _definition[y][x]; // flip 180 degrees
                permutations[7][Height - 1 - x][Width - 1 - y] = _definition[y][x]; // flip 270 degrees
            }
            return permutations;
        }

        public override string ToString()
        {
            var result = $"Package {Id}:\n";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    result += _definition[y][x] ? '#' : ' ';
                }
                result += "\n";
            }
            return result;
        }

        public static string ToString(bool[][] definition)
        {
            var result = "";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    result += definition[y][x] ? '#' : ' ';
                }
                result += "\n";
            }
            return result;
        }
    }
}
