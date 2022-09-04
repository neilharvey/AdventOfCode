using Day16;

static Packet ReadPacket(StringReader sr)
{
    var version = sr.ReadInt(3);
    var packetTypeId = sr.ReadInt(3);
    var bitsRead = 6;

    if (packetTypeId == 4)
    {
        var chunks = new List<string>();
        bool finalChunk;

        do
        {
            finalChunk = !sr.ReadBoolean();
            chunks.Add(sr.ReadChunk(4));
            bitsRead += 5;
        }
        while (!finalChunk);

        var str = string.Join("", chunks);
        var value = Convert.ToInt64(str, 2);
        var packet = new Packet(version, packetTypeId)
        {
            Value = value,
            Length = bitsRead
        };

        return packet;
    }
    else
    {
        var lengthTypeId = sr.Read();
        bitsRead++;
        IList<Packet> subPackets;
        if (lengthTypeId == '0')
        {
            var lengthInBits = sr.ReadInt(15);
            bitsRead += 15;
            subPackets = ReadSubPacketsWithLength(sr, lengthInBits);
        }
        else
        {
            var noOfSubPackets = sr.ReadInt(11);
            bitsRead += 11;
            subPackets = ReadSubPackets(sr, noOfSubPackets);
        }

        bitsRead += subPackets.Sum(p => p.Length);
        var packet = new Packet(version, packetTypeId)
        {
            LengthTypeId = lengthTypeId,
            SubPackets = subPackets,
            Length = bitsRead
        };

        return packet;
    }
}

static IList<Packet> ReadSubPackets(StringReader reader, int noOfSubPackets)
{
    var packets = new List<Packet>();
    for (var i = 0; i < noOfSubPackets; i++)
    {
        var packet = ReadPacket(reader);
        packets.Add(packet);
    }

    return packets;
}

static IList<Packet> ReadSubPacketsWithLength(StringReader reader, int lengthInBits)
{
    var packets = new List<Packet>();
    var length = 0;

    while (length < lengthInBits)
    {
        var packet = ReadPacket(reader);
        packets.Add(packet);
        length += packet.Length;
    }

    return packets;
}

static string Map(char c)
{
    return c switch
    {
        '0' => "0000",
        '1' => "0001",
        '2' => "0010",
        '3' => "0011",
        '4' => "0100",
        '5' => "0101",
        '6' => "0110",
        '7' => "0111",
        '8' => "1000",
        '9' => "1001",
        'A' => "1010",
        'B' => "1011",
        'C' => "1100",
        'D' => "1101",
        'E' => "1110",
        'F' => "1111",
        _ => throw new ArgumentOutOfRangeException(nameof(c)),
    };
}

var line = File.ReadAllLines(args[0]).First();
var bits = string.Join("", line.Select(x => Map(x)));
using var sr = new StringReader(bits);
var packet = ReadPacket(sr);
//Console.ForegroundColor = ConsoleColor.Green;
//Console.WriteLine(packet.ToString());
//Console.ResetColor();
Console.WriteLine($"Part One: {packet.VersionSum}");
Console.WriteLine($"Part Two: {packet.Evaluate()}");