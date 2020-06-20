using System;

namespace NeinTile
{
    [Serializable]
    public readonly struct Tile : IEquatable<Tile>
    {
        public static Tile Empty { get; } = new Tile();

        public long Value { get; }

        public long Score { get; }

        public Tile(long value, long score)
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
            => obj is Tile other && Equals(other);

        public bool Equals(Tile other)
            => Value == other.Value
            && Score == other.Score;

        public static bool operator ==(Tile left, Tile right)
            => left.Equals(right);

        public static bool operator !=(Tile left, Tile right)
            => !left.Equals(right);
    }
}
