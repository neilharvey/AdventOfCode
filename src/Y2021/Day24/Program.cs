int[] a = new int[14];
int[] b = new int[14];
int[] c = new int[14];
int[] max = new int[14];
int[] min = new int[14];

void FindMinMax(StreamReader reader)
{
    ReadInputs(reader);
    var stack = new Stack<(int d, int c)>();
    for (var i = 0; i < 14; i++)
    {
        if (a[i] == 1)
        {
            stack.Push((i, c[i]));
        }
        if (a[i] == 26)
        {
            (int d, int c) = stack.Pop();
            var diff = c + b[i];
            if (diff >= 0)
            {
                max[i] = 9;
                max[d] = 9 - diff;
                min[i] = 1 + diff;
                min[d] = 1;
            }
            else
            {
                max[i] = 9 + diff;
                max[d] = 9;
                min[i] = 1;
                min[d] = 1 - diff;
            }
        }
    }
}

static long DigitsToLong(int[] digits)
{
    var value = 0L;
    for (var i = 0; i < 14; i++)
    {
        value += digits[i] * (long)Math.Pow(10, 13 - i);
    }

    return value;
}

void ReadInputs(StreamReader reader)
{
    for (var i = 0; i < 14; i++)
    {
        reader.ReadLine(); // inp w
        reader.ReadLine(); // mul x 0
        reader.ReadLine(); // add x z
        reader.ReadLine(); // mod x 26
        a[i] = Convert.ToInt32(reader.ReadLine()[6..]); // div z {a}
        b[i] = Convert.ToInt32(reader.ReadLine()[6..]); // add x {b}
        reader.ReadLine(); // eql x w
        reader.ReadLine(); // eql x 0
        reader.ReadLine(); // mul y 0
        reader.ReadLine(); // add y 25
        reader.ReadLine(); // mul y x
        reader.ReadLine(); // add y 1
        reader.ReadLine(); // mul z y
        reader.ReadLine(); // mul y 0
        reader.ReadLine(); // add y w
        c[i] = Convert.ToInt32(reader.ReadLine()[6..]); // add y {c}
        reader.ReadLine(); // mul y x
        reader.ReadLine(); // add z y
    }
}

var stream = File.OpenRead(args[0]);
var reader = new StreamReader(stream);
FindMinMax(reader);
Console.WriteLine($"Part One: {DigitsToLong(max)}");
Console.WriteLine($"Part Two: {DigitsToLong(min)}");