using System.Collections.Generic;

public class Cave
{
    public string Label { get; }

    public bool IsSmall => char.IsLower(Label[0]);

    public bool IsStart => Label == "start";

    public bool IsEnd => Label == "end";

    public IList<Cave> Adjacent { get; } = new List<Cave>();

    public Cave(string label)
    {
        Label = label;
    }

    public override string ToString()
    {
        return Label;
    }
}