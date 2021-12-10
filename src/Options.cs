using CommandLine;

namespace AdventOfCode
{
    public class Options
    {
        [Option(Required = true, HelpText = "Name of the input file to use.")]
        public string File { get; set; }

        [Option(Required = true, HelpText = "Year of the puzzle.")]
        public int? Year { get; set; }

        [Option(Required = true, HelpText = "Day of the puzzle.")]
        public int? Day { get; set; }

        [Option(Default = 1, HelpText = "Puzzle part (1 or 2) to run.")]
        public int? Part { get; set; }
    }
}
