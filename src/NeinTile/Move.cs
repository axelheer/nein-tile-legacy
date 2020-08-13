using System;

namespace NeinTile
{
    public readonly struct Move : IEquatable<Move>
    {
        public static Move Empty { get; }

        public TileIndex Source { get; }

        public TileIndex Target { get; }

        public TileIndex Marker { get; }

        public Move(TileIndex source, TileIndex target, TileIndex marker)
        {
            Source = source;
            Target = target;
            Marker = marker;
        }

        public void Deconstruct(out TileIndex source, out TileIndex target, out TileIndex marker)
        {
            source = Source;
            target = Target;
            marker = Marker;
        }

        public override string ToString()
            => $"{Source} => {Target} ({Marker})";

        public override int GetHashCode()
            => HashCode.Combine(Source, Target, Marker);

        public override bool Equals(object? obj)
            => obj is Move other && Equals(other);

        public bool Equals(Move other)
            => Source == other.Source
            && Target == other.Target
            && Marker == other.Marker;

        public static bool operator ==(Move left, Move right)
            => left.Equals(right);

        public static bool operator !=(Move left, Move right)
            => !left.Equals(right);
    }
}
