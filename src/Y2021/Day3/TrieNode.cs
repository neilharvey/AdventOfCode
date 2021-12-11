namespace AdventOfCode.Y2021.Day3;

public class TrieNode
{
    public TrieNode()
    {
        Parent = null;
        Character = Char.MinValue;
    }

    private TrieNode(TrieNode parent, char character)
    {
        Parent = parent;
        Character = character;
    }

    public TrieNode Parent { get; }

    public TrieNode[] Children { get; } = new TrieNode[2];

    public int Value { get; private set; } = 0;

    public int Depth { get; private set; } = 0;

    public char Character { get; }

    public bool IsLeaf => Children[0] == null && Children[1] == null;

    public void AddWord(string word)
    {
        Value++;
        Depth = Math.Max(Depth, word.Length);

        if (word.Length > 0)
        {
            var index = word[0] - '0';

            if (Children[index] == null)
            {
                Children[index] = new TrieNode(this, word[0]);
            }

            Children[index].AddWord(word[1..]);
        }
    }

    public int ToInt32()
    {
        return Convert.ToInt32(ToString(), 2);
    }

    public override string ToString()
    {
        if (Parent == null)
        {
            return "";
        }
        else
        {
            return Parent.ToString() + Character;
        }
    }
}