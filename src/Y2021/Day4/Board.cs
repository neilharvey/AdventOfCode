namespace AdventOfCode.Y2021.Day4
{
    public class Board
    {
        public const int Size = 5;

        private readonly int[] _numbers = new int[Size * Size];
        private readonly bool[] _seen = new bool[Size * Size];
        private int? _winningNumber;

        public int this[int index]
        {
            get => _numbers[index];
            set => _numbers[index] = value;
        }

        public void SetSeen(int number)
        {
            var index = Array.IndexOf(_numbers, number);

            if (index >= 0)
            {
                _seen[index] = true;
            }

            if (HasBingo())
            {
                _winningNumber = number;
            }
        }

        public bool HasBingo()
        {
            // horizontal
            for (var row = 0; row < Size; row++)
            {
                var hasRow = true;
                for (var col = 0; col < Size; col++)
                {
                    hasRow &= _seen[(row * Size) + col];
                }

                if (hasRow)
                {
                    return true;
                }
            }

            // vertical
            for (var col = 0; col < Size; col++)
            {
                // 0 1 2 3 4
                // 5 6 7 8 9 
                var hasCol = true;
                for (var row = 0; row < Size; row++)
                {
                    hasCol &= _seen[(row * Size) + col];
                }

                if (hasCol)
                {
                    return true;
                }
            }

            return false;
        }

        public int Score()
        {
            if (!HasBingo())
            {
                return 0;
            }

            var sumOfUnmarked = 0;
            for (var i = 0; i < 25; i++)
            {
                if (!_seen[i])
                {
                    sumOfUnmarked += _numbers[i];
                }
            }

            return sumOfUnmarked * _winningNumber.Value;
        }
    }
}
