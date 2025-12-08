using System.Diagnostics;

namespace AOC25;

public class Helpers
{
    public static string LoadInputFile(string fileName)
    {
        string workingDir = Path.Combine(Directory.GetCurrentDirectory(), "../../../Inputs");
        string path = Path.Combine(workingDir, fileName);
        if (!File.Exists(path))
        {
            File.Create(path).Close();
            File.WriteAllText(path, "Replace me with your input");
            var isWin = Environment.OSVersion.Platform == PlatformID.Win32NT;
            var processStartInfo = new ProcessStartInfo
            {
                Arguments = $"add \"{fileName}\"",
                FileName = isWin ? "git.exe" : "git",
                WorkingDirectory = workingDir,
            };
            Process.Start(processStartInfo);
        }
        return File.ReadAllText(path);
    }

    public class ReverseComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y)
        {
            // Reverse the comparison: y.CompareTo(x) instead of x.CompareTo(y)
            return x.CompareTo(y);
        }
    }
}