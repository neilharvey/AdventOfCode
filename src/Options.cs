using CommandLine;

namespace AdventOfCode
{
    public class Options
    {
        [Option(Required = true)]
        public string Path { get; set; }

        [Option(Required = true)]
        public int? Year { get; set; }

        [Option(Required = true)]
        public int? Day { get; set; }

        [Option(Default = 1)]
        public int? Part { get; set; }
    }
}
