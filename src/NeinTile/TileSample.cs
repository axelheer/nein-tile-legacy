using System;

namespace NeinTile
{
    public readonly struct TileSample : IEquatable<TileSample>
    {
        public static TileSample Empty { get; } = new TileSample();

        public TileInfo First { get; }

        public TileInfo Second { get; }

        public TileInfo Third { get; }

        public TileSample(TileInfo first, TileInfo second, TileInfo third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public TileSample(TileInfo actual)
            : this(actual, actual, actual)
        {
        }

        public void Deconstruct(out TileInfo first, out TileInfo second, out TileInfo third)
        {
            first = First;
            second = Second;
            third = Third;
        }

        public bool IsExplicit
            => First == Second && Second == Third;

        public override string ToString()
            => $"{First} | {Second} | {Third}";

        public override int GetHashCode()
            => HashCode.Combine(First, Second, Third);

        public override bool Equals(object? obj)
            => obj is TileSample other ? Equals(other) : false;

        public bool Equals(TileSample other)
            => First == other.First
            && Second == other.Second
            && Third == other.Third;

        public static bool operator ==(TileSample left, TileSample right)
            => left.Equals(right);

        public static bool operator !=(TileSample left, TileSample right)
            => !left.Equals(right);
    }
}