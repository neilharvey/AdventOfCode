﻿using AdventOfCode;
using CommandLine;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(options =>
    {
        var year = options.Year ?? 2021;
        var fileInfo = FileLocator.FindFile(options.File, year, options.Day);

        if (!fileInfo.Exists)
        {
            Console.WriteLine($"File {fileInfo.FullName} does not exist");
            return;
        }

        var solution = SolutionFactory.Create(year, options.Day);
        if (solution == null)
        {
            Console.WriteLine($"Solution for {year} day {options.Day} not found");
            return;
        }

        var stream = File.OpenRead(fileInfo.FullName);
        var reader = new StreamReader(stream);
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var answer = options.Part == 1 ? solution.Part1(reader) : solution.Part2(reader);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{answer} [{sw.ElapsedMilliseconds}ms]");
        Console.ResetColor();
    });
