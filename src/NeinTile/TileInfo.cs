using System;

namespace NeinTile
{
    public struct TileInfo : IEquatable<TileInfo>
    {
        public static TileInfo Empty { get; } = new TileInfo();

        public int Value { get; }

        public int Score { get; }

        public TileInfo(int value, int score)
        {
            Value = value;
            Score = score;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Score);
        }

        public override bool Equals(object? obj)
        {
            return obj is TileInfo other ? Equals(other) : false;
        }

        public bool Equals(TileInfo other)
        {
            return Value == other.Value
                && Score == other.Score;
        }

        public static bool operator ==(TileInfo left, TileInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TileInfo left, TileInfo right)
        {
            return !left.Equals(right);
        }
    }
}
