using AdventOfCode;
using CommandLine;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o =>
    {
        var fileInfo = new FileInfo(o.File);

        if (!fileInfo.Exists)
        {
            Console.WriteLine($"File {fileInfo.FullName} does not exist");
            return;
        }

        var solution = SolutionFactory.Create(o.Year.Value, o.Day.Value);
        if (solution == null)
        {
            Console.WriteLine($"Solution for {o.Year} day {o.Day} not found");
            return;
        }

        var stream = File.OpenRead(fileInfo.FullName);
        var reader = new StreamReader(stream);
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var answer = o.Part == 1 ? solution.Part1(reader) : solution.Part2(reader);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{answer} [{sw.ElapsedMilliseconds}ms]");
        Console.ResetColor();
    });