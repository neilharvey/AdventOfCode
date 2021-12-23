namespace AdventOfCode.Y2021.Day18
{
    public class Solution : IPuzzleSolution
    {
        private const char OpenBrace = '(';
        private const char CloseBrace = ')';
        private const char Separator = ',';

        public long Part1(StreamReader reader)
        {
            var sum = CreatePair(reader.ReadLine());
            while (reader.TryReadLine(out string pair))
            {
                sum = Add(sum, CreatePair(pair));
            }

            return 0;
            //return Magnitude(sum);
        }

        public long Part2(StreamReader reader)
        {
            throw new NotImplementedException();
        }

        private List<char> Add(List<char> left, List<char> right)
        {
            // To add two snailfish numbers, form a pair from the left and right parameters of the addition operator.
            // [1,2] + [[3,4],5] becomes [[1,2],[[3,4],5]]
            Console.ForegroundColor = ConsoleColor.Green;
            Debug(right);
            Console.ResetColor();
            var sum = CreatePair($"{OpenBrace}{new string(left.ToArray())}{Separator}{new string(right.ToArray())}{CloseBrace}");
            // To reduce a snailfish number, you must repeatedly do the first action in this list that applies to the snailfish number:
            var isReduced = false;
            while (!isReduced)
            {
                Debug(sum);

                // If any pair is nested inside four pairs, the leftmost such pair explodes.
                if (Explode(sum))
                {
                    continue;
                }

                // If any regular number is 10 or greater, the leftmost such regular number splits.
                // Once no action in the above list applies, the snailfish number is reduced.
                if (Split(sum))
                {
                    continue;
                }

                isReduced = true;
            }

            return sum;
        }

        private static List<char> CreatePair(string value)
        {
            return value
                .Replace("[","(")
                .Replace("]",")")
                .ToList();
        }

        private static bool Explode(List<char> pair)
        {
            var i = 0;
            var nesting = 0;
            while (i < pair.Count)
            {
                if (pair[i] == OpenBrace)
                    nesting++;
                if(pair[i] == CloseBrace)
                    nesting--;

                if (nesting > 4 && IsDigit(pair[i]) && IsDigit(pair[i + 2]))
                {
                    // explode
                    var leftValue = pair[i] - '0';
                    var rightValue = pair[i + 2] - '0';
                    //Console.WriteLine($"[{leftValue},{rightValue}] explodes!");

                    // Add first value to first regular number to the left
                    var b = i - 1;
                    while (b > 0)
                    {
                        if (IsDigit(pair[b]))
                        {
                            pair[b] = (char)(pair[b] + leftValue);
                            break;
                        }

                        b--;
                    }

                    // Add right value to the first regular number to the right
                    var f = i + 3;
                    while (f < pair.Count)
                    {
                        if (IsDigit(pair[f]))
                        {
                            pair[f] = (char)(pair[f] + rightValue);
                            break;
                        }

                        f++;
                    }

                    // Replace original pair with zero
                    pair.RemoveRange(i - 1, 5);
                    pair.Insert(i - 1, '0');
 
                    return true;
                }

                i++;
            }

            return false;
        }

        private static bool Split(List<char> pair)
        {
            var i = 0;
            while (i < pair.Count)
            {
                if (IsDigit(pair[i]))
                {
                    var value = pair[i] - '0';

                    if (value > 9)
                    {
                        //Console.WriteLine($"{value} splits!");
                        var leftValue = (char)(Math.Floor(value / 2D) + '0');
                        var rightValue = (char)(Math.Ceiling(value / 2D) + '0');
                        var newPair = CreatePair($"[{leftValue},{rightValue}]");
                        pair.RemoveAt(i);
                        pair.InsertRange(i, newPair);
                        return true;
                    }
                }

                i++;
            }

            return false;
        }

        private static bool IsDigit(char c)
        {
            return c != OpenBrace && c != CloseBrace && c != Separator;
        }

        private static void Debug(List<char> pair)
        {
            // var syntax = new char[] {OpenBrace, CloseBrace, Separator};
            // var bits = pair.Select(x => syntax.Contains(x) ? x.ToString() : Convert.ToInt64(x - '0').ToString());    
            // var value = string.Join("", bits);        
            // var nesting = 0;
            // for(var i=0; i<value.Length; i++)
            // {
            //     if(nesting <= 4)
            //         Console.ResetColor();
            //     if(value[i] == OpenBrace)
            //         nesting++;
            //     if(value[i] == CloseBrace)
            //         nesting--;
            //     if(nesting > 4) 
            //         Console.ForegroundColor = ConsoleColor.Red;
            //     Console.Write(value[i]);
            // }
            // Console.WriteLine();
        }

        private long Magnitude(List<char> sum)
        {
            return 0;
        }
    }
}