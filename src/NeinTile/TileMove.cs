using System;

namespace NeinTile
{
    public readonly struct TileMove : IEquatable<TileMove>
    {
        public static TileMove Empty { get; } = new TileMove();

        public TileInfo Source { get; }

        public TileInfo Target { get; }

        public TileMove(TileInfo source, TileInfo target)
        {
            Source = source;
            Target = target;
        }

        public void Deconstruct(out TileInfo source, out TileInfo target)
        {
            source = Source;
            target = Target;
        }

        public override string ToString()
            => $"{Source} => {Target}";

        public override int GetHashCode()
            => HashCode.Combine(Source, Target);

        public override bool Equals(object? obj)
            => obj is TileMove other && Equals(other);

        public bool Equals(TileMove other)
            => Source == other.Source
            && Target == other.Target;

        public static bool operator ==(TileMove left, TileMove right)
            => left.Equals(right);

        public static bool operator !=(TileMove left, TileMove right)
            => !left.Equals(right);
    }
}
