var path = Environment.GetCommandLineArgs().ElementAtOrDefault(1);

if(path == null)
{
    Console.WriteLine("No path specified");
    return;
}

var fileInfo = new FileInfo(path);
if(!fileInfo.Exists)
{
    Console.WriteLine($"File {fileInfo.FullName} does not exist");
    return;
}
else
{
    var sw = System.Diagnostics.Stopwatch.StartNew();
    var stream = File.OpenRead(fileInfo.FullName);
    var reader = new StreamReader(stream);
    var solver = new Y2021.Day2.Solver();
    var answer = solver.Part2(reader);
    Console.WriteLine($"{answer} [{sw.ElapsedMilliseconds}ms]");
}