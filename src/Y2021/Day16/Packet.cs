namespace AdventOfCode.Y2021.Day16
{
    public class Packet
    {
        public Packet(int version, int packetTypeId)
        {
            Version = version;
            PacketType = (PacketType)packetTypeId;
        }

        public int Version { get; set; }

        public PacketType PacketType { get; set; }

        public int? LengthTypeId { get; set; }

        public long? Value { get; set; }

        public IList<Packet> SubPackets { get; set; } = new List<Packet>();

        public int Length { get; set; }

        public int VersionSum => Version + SubPackets.Sum(p => p.VersionSum);

        public long Evaluate()
        {
            return PacketType switch
            {
                PacketType.Literal => Value.Value,
                PacketType.Sum => SubPackets.Sum(p=> p.Evaluate()),
                PacketType.Product => SubPackets.Select(p => p.Evaluate()).Aggregate(1, (long acc, long val) => acc * val),
                PacketType.Minimum => SubPackets.Min(p => p.Evaluate()),
                PacketType.Maximum => SubPackets.Max(p => p.Evaluate()),
                PacketType.GreaterThan => SubPackets[0].Evaluate() > SubPackets[1].Evaluate() ? 1 : 0,
                PacketType.LessThan => SubPackets[0].Evaluate() < SubPackets[1].Evaluate() ? 1 : 0,
                PacketType.EqualTo => SubPackets[0].Evaluate() == SubPackets[1].Evaluate() ? 1 : 0,
                _ => throw new NotSupportedException(),
            };
        }

        public override string ToString() => ToString(0);

        private string ToString(int indent)
        {
            if (PacketType == PacketType.Literal)
            {
                return Value.ToString();
            }
            else
            {
                var subPacketString = string.Join(", ", SubPackets.Select(p => p.ToString(indent + 1)));
                var op = PacketType switch
                {
                    PacketType.Sum => "Sum",
                    PacketType.Product => "Product",
                    PacketType.Minimum => "Min",
                    PacketType.Maximum => "Max",
                    PacketType.GreaterThan => "GreaterThan",
                    PacketType.LessThan => "LessThan",
                    PacketType.EqualTo => "Equal",
                    _ => "?"
                };

                var indentation = "";
                if (indent > 0)
                {
                    indentation = Environment.NewLine + new string(' ', indent);
                }
                return $"{indentation}{op}({subPacketString})[{Evaluate()}]";
            }
        }
    }
}
