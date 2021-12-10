namespace AdventOfCode
{
    public class SolutionFactory
    {
        public static IPuzzleSolution Create(int year, int day)
        {
            return typeof(IPuzzleSolution).Assembly
                .GetTypes()
                .Where(t => typeof(IPuzzleSolution).IsAssignableFrom(t))
                .Where(t => t.IsClass)
                .Where(t => t.Namespace == $"AdventOfCode.Y{year}.Day{day}")
                .Select(t => Activator.CreateInstance(t))
                .Cast<IPuzzleSolution>()
                .FirstOrDefault();                
        }
    }
}
