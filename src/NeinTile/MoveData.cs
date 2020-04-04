namespace NeinTile
{
    internal struct MoveData
    {
        private readonly int direction;

        public readonly int Start { get; }
        public readonly int Shift { get; }
        public readonly int Count { get; }

        public readonly bool Blocked { get; }

        public int Index { get; set; }

        public MoveData(int dimension, int count, MoveDirection direction)
        {
            this.direction = (int)direction % 2 == 1 ? 1 : -1;

            var positive = dimension + dimension == (int)direction;
            var negative = dimension + dimension + 1 == (int)direction;

            Start = negative ? 1 : 0;
            Shift = positive ? 1 : negative ? -1 : 0;
            Count = positive ? count - 1 : count;

            Blocked = Start + Shift == Count + Shift;

            Index = -1;
        }

        public int ShiftIndex
            => Index + Shift;

        public int ShiftMark
            => Shift == 0 ? Index : direction < 0 ? 0 : Count - 1;

        public void Reset()
            => Index = direction > 0 ? Start : Count - 1;

        public bool CanMove()
            => Start <= Index && Index < Count;

        public bool MoveNext()
        {
            Index += direction;

            return CanMove();
        }
    }
}
