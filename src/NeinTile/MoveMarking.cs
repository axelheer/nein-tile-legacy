using System;

namespace NeinTile
{
    public readonly struct MoveMarking : IEquatable<MoveMarking>
    {
        public static MoveMarking Empty { get; } = new MoveMarking();

        public int ColIndex { get; }

        public int RowIndex { get; }

        public int LowIndex { get; }

        public MoveMarking(int colIndex, int rowIndex, int lowIndex)
        {
            ColIndex = colIndex;
            RowIndex = rowIndex;
            LowIndex = lowIndex;
        }

        public void Deconstruct(out int colIndex, out int rowIndex, out int lowIndex)
        {
            colIndex = ColIndex;
            rowIndex = RowIndex;
            lowIndex = LowIndex;
        }

        public override string ToString()
            => $"[{ColIndex}, {RowIndex}, {LowIndex}]";

        public override int GetHashCode()
            => HashCode.Combine(ColIndex, RowIndex, LowIndex);

        public override bool Equals(object? obj)
            => obj is MoveMarking other ? Equals(other) : false;

        public bool Equals(MoveMarking other)
            => ColIndex == other.ColIndex
            && RowIndex == other.RowIndex
            && LowIndex == other.LowIndex;

        public static bool operator ==(MoveMarking left, MoveMarking right)
            => left.Equals(right);

        public static bool operator !=(MoveMarking left, MoveMarking right)
            => !left.Equals(right);
    }
}
