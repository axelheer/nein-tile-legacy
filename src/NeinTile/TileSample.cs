using System;

namespace NeinTile
{
    public struct TileSample : IEquatable<TileSample>
    {
        public static TileSample Empty { get; } = new TileSample();

        public TileInfo First { get; }

        public TileInfo Second { get; }

        public TileInfo Third { get; }

        public bool IsExplicit { get; }

        public TileSample(TileInfo first, TileInfo second, TileInfo third)
        {
            First = first;
            Second = second;
            Third = third;

            IsExplicit = first == second && second == third;
        }

        public TileSample(TileInfo actual)
            : this(actual, actual, actual)
        {
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second, Third);
        }

        public override bool Equals(object? obj)
        {
            return obj is TileSample other ? Equals(other) : false;
        }

        public bool Equals(TileSample other)
        {
            return First == other.First
                && Second == other.Second
                && Third == other.Third;
        }

        public static bool operator ==(TileSample left, TileSample right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TileSample left, TileSample right)
        {
            return !left.Equals(right);
        }
    }
}
