namespace AdventOfCode
{
    public class SolutionResolver
    {
        public static IPuzzleSolution Find(int year, int day)
        {
            return typeof(IPuzzleSolution).Assembly
                .GetTypes()
                .Where(t => typeof(IPuzzleSolution).IsAssignableFrom(t))
                .Where(t => t.IsClass)
                .Select(t => Activator.CreateInstance(t))
                .Cast<IPuzzleSolution>()
                .Where(s => s.PuzzleDate.Year == year && s.PuzzleDate.Day == day)
                .FirstOrDefault();                
        }
    }
}
