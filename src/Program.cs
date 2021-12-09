var path = Environment.GetCommandLineArgs().ElementAt(1);

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
    var stream = File.OpenRead(fileInfo.FullName);
    var reader = new StreamReader(stream);
    var solver = new Y2021.Day1.Solver();
    var increases = solver.Part2(reader);

    Console.WriteLine(increases);
}