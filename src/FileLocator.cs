using System.Reflection;

namespace AdventOfCode;

public static class FileLocator
{
    public static FileInfo FindFile(string filename, int year, int day)
    {
        var assembly = new FileInfo(Assembly.GetExecutingAssembly().Location);
        var directory = assembly.Directory;
        while (directory.Name != "src")
        {
            directory = directory.Parent;
        }

        var path = Path.Combine(directory.FullName, $"Y{year}", $"Day{day}", filename);
        return new FileInfo(path);
    }
}
