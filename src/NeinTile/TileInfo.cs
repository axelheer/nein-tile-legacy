using System;

namespace NeinTile
{
    public readonly struct TileInfo : IEquatable<TileInfo>
    {
        public static TileInfo Empty { get; } = new TileInfo();

        public long Value { get; }

        public long Score { get; }

        public TileInfo(long value, long score)
        {
            Value = value;
            Score = score;
        }

        public void Deconstruct(out long value, out long score)
        {
            value = Value;
            score = Score;
        }

        public override string ToString()
            => $"({Value}, {Score})";

        public override int GetHashCode()
            => HashCode.Combine(Value, Score);

        public override bool Equals(object? obj)
            => obj is TileInfo other ? Equals(other) : false;

        public bool Equals(TileInfo other)
            => Value == other.Value
            && Score == other.Score;

        public static bool operator ==(TileInfo left, TileInfo right)
            => left.Equals(right);

        public static bool operator !=(TileInfo left, TileInfo right)
            => !left.Equals(right);
    }
}
