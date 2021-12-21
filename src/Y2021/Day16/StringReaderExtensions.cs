﻿namespace AdventOfCode.Y2021.Day16
{
    public static class BinaryReaderExtensions
    {
        public static int ReadInt(this StringReader reader, int length)
        {
            var chunk = reader.ReadChunk(length);
            try
            {
                return Convert.ToInt32(chunk, 2);
            }
            catch(FormatException)
            {
                Console.WriteLine($"Unable to read '{chunk}'");
                Console.WriteLine($"Rest of stream '{reader.ReadToEnd()}'");
                return 0;
            }
        }

        public static string ReadChunk(this StringReader reader, int length)
        {
            var buffer = new char[length];
            reader.Read(buffer, 0, buffer.Length);
            return new string(buffer);
        }

        public static bool ReadBoolean(this StringReader reader)
        {
            return reader.Read() == '1';
        }
    }
}