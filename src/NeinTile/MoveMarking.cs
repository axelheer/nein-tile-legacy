using System;

namespace NeinTile
{
    public readonly struct MoveMarking : IEquatable<MoveMarking>
    {
        public static MoveMarking Empty { get; } = new MoveMarking();

        public int ColIndex { get; }

        public int RowIndex { get; }

        public int LayIndex { get; }

        public MoveMarking(int colIndex, int rowIndex, int layIndex)
        {
            ColIndex = colIndex;
            RowIndex = rowIndex;
            LayIndex = layIndex;
        }

        public void Deconstruct(out int colIndex, out int rowIndex, out int layIndex)
        {
            colIndex = ColIndex;
            rowIndex = RowIndex;
            layIndex = LayIndex;
        }

        public override string ToString()
            => $"[{ColIndex}, {RowIndex}, {LayIndex}]";

        public override int GetHashCode()
            => HashCode.Combine(ColIndex, RowIndex, LayIndex);

        public override bool Equals(object? obj)
            => obj is MoveMarking other && Equals(other);

        public bool Equals(MoveMarking other)
            => ColIndex == other.ColIndex
            && RowIndex == other.RowIndex
            && LayIndex == other.LayIndex;

        public static bool operator ==(MoveMarking left, MoveMarking right)
            => left.Equals(right);

        public static bool operator !=(MoveMarking left, MoveMarking right)
            => !left.Equals(right);
    }
}
