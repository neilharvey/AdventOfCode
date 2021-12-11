namespace AdventOfCode.Y2021.Day3;
public class Solution : IPuzzleSolution
{
    public int Part1(StreamReader reader)
    {
        var trie = CreateTrie(reader);

        var gammaRate = 0;
        var epsilonRate = 0;
        var nodes = new List<TrieNode>() { trie };

        for(var depth = 0; depth < trie.Depth; depth++)
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

    public int Part2(StreamReader reader)
    {
        var trie = CreateTrie(reader);
        var oxygenGeneratorRating = FindPath(trie, (x, y) => x.Value <= y.Value ? x : y);
        var co2scrubberRating = FindPath(trie, (x, y) => x.Value > y.Value ? x : y);

        return oxygenGeneratorRating * co2scrubberRating;
    }

    private static TrieNode CreateTrie(StreamReader reader)
    {
        var trie = new TrieNode();
        while (reader.TryReadLine(out string line))
        {
            trie.AddWord(line);
        }

        return trie;
    }

    private static int FindPath(TrieNode trie, Func<TrieNode, TrieNode, TrieNode> compare)
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
}
