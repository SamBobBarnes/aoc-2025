using System.Diagnostics;
// ReSharper disable VirtualMemberCallInConstructor

namespace AOC25;

public abstract class BasePart
{
    private readonly int _day;
    private readonly int _part;
    private readonly bool _test;
    private readonly string? _inputText;

    protected BasePart(int day,int part, bool test = false): this("", day, part,null, test){}
    protected BasePart(int day,int part, string inputText, bool test = false): this("", day, part, inputText, test){}

    private BasePart(string title, int day, int part, string? inputText, bool test = false)
    {
        _day = day;
        _test = test;
        _inputText = inputText;
        _part = part;

        Console.WriteLine($"Running day {_day} part {part}{(_test ? " example" : "")}");
        if (!string.IsNullOrEmpty(title)) Console.WriteLine(title);
        // ReSharper disable once VirtualMemberCallInConstructor
        Console.WriteLine();
        var timer = new Stopwatch();
        timer.Start();
        var result = Run();
        timer.Stop();
        var time = timer.ElapsedMilliseconds;
        Console.WriteLine(time < 1000
            ? $"{result}\n\nCompleted in {time} ms\n"
            : $"{result}\n\nCompleted in {time / 1000.0:F2} s\n");
    }

    protected string[] Input()
    {
        if (!_test && _inputText != null)
            return _inputText.Replace("\r\n", "\n").Split('\n');
    
        string filename = $"day{_day,2:D2}";
        if (_test)
        {
            filename += "_test";
        }

        filename += ".txt";
        return Helpers.LoadInputFile(filename).Replace("\r\n", "\n").Split('\n');
    }

    protected char[] InputChars()
    {
        if (!_test && _inputText != null)
            return _inputText.Replace("\r\n", "\n").ToCharArray();
        string filename = $"day{_day,2:D2}_p{_part}";
        if (_test)
        {
            filename += "_test";
        }
        filename += ".txt";
        return Helpers.LoadInputFile(filename).Replace("\r\n","\n").ToCharArray();
    }

    public abstract string Run();
}