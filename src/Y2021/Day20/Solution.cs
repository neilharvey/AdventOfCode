using System.Text;

namespace AdventOfCode.Y2021.Day20
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var algorithm = reader.ReadLine();
            _ = reader.ReadLine(); // blank

            var image = ReadImage(reader);
        //    Console.WriteLine("--- Initial ---");
          //  PrintImage(image);
            image = EnhanceImage(image, algorithm);
    //        Console.WriteLine("--- Enhancement 1 ---");
      //      PrintImage(image);
            image = EnhanceImage(image, algorithm);
//            Console.WriteLine("--- Enhancement 2 ---");
  //          PrintImage(image);

            var litPixels = 0;
            for (var x = image.MinX; x <= image.MaxX; x++)
            {
                for (var y = image.MinY; y <= image.MaxY; y++)
                {
                    if (image[x, y])
                    {
                        litPixels++;
                    }
                }
            }

            return litPixels;
        }

        private static void PrintImage(Image image)
        {
            for (var y = image.MinY; y <= image.MaxY; y++)
            {
                for (var x = image.MinX; x <= image.MaxX; x++)
                {
                    Console.Write(image[x, y] ? '#' : '.');
                }

                Console.WriteLine();
            }
        }

        private static Image ReadImage(StreamReader reader)
        {
            var image = new Image();
            var y = 0;

            while (reader.TryReadLine(out string line))
            {
                for (var x = 0; x < line.Length; x++)
                {
                    image[x, y] = line[x] == '#';
                }

                y++;
            }

            return image;
        }

        private static Image EnhanceImage(Image image, string algorithm)
        {
            var enhanced = new Image();

            for (var x = image.MinX; x <= image.MaxX; x++)
            {
                for (var y = image.MinY; y <= image.MaxY; y++)
                {
                    var enhancementIndex = GetEnhancementIndex(image, x, y);
                    enhanced[x, y] = algorithm[enhancementIndex] == '#';
                }
            }

            return enhanced;
        }

        private static int GetEnhancementIndex(Image image, int x, int y)
        {
            var sb = new StringBuilder();
            sb.Append(image[x - 1, y - 1] ? "1" : "0");
            sb.Append(image[x, y - 1] ? "1" : "0");
            sb.Append(image[x + 1, y - 1] ? "1" : "0");
            sb.Append(image[x - 1, y] ? "1" : "0");
            sb.Append(image[x, y] ? "1" : "0");
            sb.Append(image[x + 1, y] ? "1" : "0");
            sb.Append(image[x - 1, y + 1] ? "1" : "0");
            sb.Append(image[x, y + 1] ? "1" : "0");
            sb.Append(image[x + 1, y + 1] ? "1" : "0");
            return Convert.ToInt32(sb.ToString(), 2);
        }

        public long Part2(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
