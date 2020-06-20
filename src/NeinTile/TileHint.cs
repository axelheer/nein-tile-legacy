using System;

namespace NeinTile
{
    [Serializable]
    public readonly struct TileHint : IEquatable<TileHint>
    {
        public static TileHint Empty { get; } = new TileHint();

        public Tile First { get; }

        public Tile Second { get; }

        public Tile Third { get; }

        public TileHint(Tile first, Tile second, Tile third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public TileHint(Tile first, Tile second)
            : this(first, second, second)
        {
        }

        public TileHint(Tile first)
            : this(first, first)
        {
        }

        public void Deconstruct(out Tile first, out Tile second, out Tile third)
        {
            first = First;
            second = Second;
            third = Third;
        }

        public bool IsSingle
            => First == Second && Second == Third;

        public bool IsEither
            => First != Second && Second == Third;

        public override string ToString()
            => $"{First} | {Second} | {Third}";

        public override int GetHashCode()
            => HashCode.Combine(First, Second, Third);

        public override bool Equals(object? obj)
            => obj is TileHint other && Equals(other);

        public bool Equals(TileHint other)
            => First == other.First
            && Second == other.Second
            && Third == other.Third;

        public static bool operator ==(TileHint left, TileHint right)
            => left.Equals(right);

        public static bool operator !=(TileHint left, TileHint right)
            => !left.Equals(right);
    }
}
