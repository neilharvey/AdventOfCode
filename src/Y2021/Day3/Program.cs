static long Part1(string[] lines)
{
    var trie = CreateTrie(lines);

    var gammaRate = 0;
    var epsilonRate = 0;
    var nodes = new List<TrieNode>() { trie };

    for (var depth = 0; depth < trie.Depth; depth++)
    {
        var zero = nodes.Where(x => x.Children[0] != null).Sum(x => x.Children[0].Value);
        var one = nodes.Where(x => x.Children[1] != null).Sum(x => x.Children[1].Value);
        var offset = (trie.Depth - 1) - depth;

        if (one > zero)
        {
            gammaRate |= 1 << offset;
        }
        else
        {
            epsilonRate |= 1 << offset;
        }

        nodes = nodes.SelectMany(x => x.Children).Where(x => x != null).ToList();
    }

    return gammaRate * epsilonRate;
}

static long Part2(string[] lines)
{
    var trie = CreateTrie(lines);
    var oxygenGeneratorRating = FindPath(trie, (x, y) => x.Value <= y.Value ? x : y);
    var co2scrubberRating = FindPath(trie, (x, y) => x.Value > y.Value ? x : y);

    return oxygenGeneratorRating * co2scrubberRating;
}

static TrieNode CreateTrie(string[] lines)
{
    var trie = new TrieNode();
    foreach(var line in lines)
    {
        trie.AddWord(line);
    }

    return trie;
}

static int FindPath(TrieNode trie, Func<TrieNode, TrieNode, TrieNode> compare)
{
    var node = trie;

    while (!node.IsLeaf)
    {
        if (node.Children[0] == null)
        {
            node = node.Children[1];
        }
        else if (node.Children[1] == null)
        {
            node = node.Children[0];
        }
        else
        {
            node = compare(node.Children[0], node.Children[1]);
        }
    }

    return node.ToInt32();
}

var lines = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two: {Part2(lines)}");