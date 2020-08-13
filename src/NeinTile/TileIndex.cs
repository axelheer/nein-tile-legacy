using System;

namespace NeinTile
{
    [Serializable]
    public readonly struct TileIndex : IEquatable<TileIndex>
    {
        public static TileIndex Empty { get; }

        public int Col { get; }

        public int Row { get; }

        public int Lay { get; }

        public TileIndex(int col, int row, int lay)
        {
            Col = col;
            Row = row;
            Lay = lay;
        }

        public void Deconstruct(out int col, out int row, out int lay)
        {
            col = Col;
            row = Row;
            lay = Lay;
        }

        public override string ToString()
            => $"[{Col}, {Row}, {Lay}]";

        public override int GetHashCode()
            => HashCode.Combine(Col, Row, Lay);

        public override bool Equals(object? obj)
            => obj is TileIndex other && Equals(other);

        public bool Equals(TileIndex other)
            => Col == other.Col
            && Row == other.Row
            && Lay == other.Lay;

        public static bool operator ==(TileIndex left, TileIndex right)
            => left.Equals(right);

        public static bool operator !=(TileIndex left, TileIndex right)
            => !left.Equals(right);
    }
}
