using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public class InsanityMerger : IMerger
    {
        public bool CanMerge(Tile source, Tile target)
        {
            return (source.Value, target.Value) switch
            {
                (0, _) when source == Tile.Empty => false,
                (_, 0) when target == Tile.Empty => true,
                (-2, -2) => false,
                (-2, -1) => true,
                (-1, -2) => true,
                (-1, -1) => false,
                (1, 1) => false,
                (1, 2) => true,
                (2, 1) => true,
                (2, 2) => false,
                var (s, t) => s == t || s + t == 0
            };
        }

        public Tile Merge(Tile source, Tile target)
        {
            return (source, target) switch
            {
                (_, (0, 0)) => source,
                ((-2, 0), (-1, 0)) => new Tile(-3, 3),
                ((-1, 0), (-2, 0)) => new Tile(-3, 3),
                ((1, 0), (2, 0)) => new Tile(3, 3),
                ((2, 0), (1, 0)) => new Tile(3, 3),
                var ((v1, s1), (v2, s2)) when v1 < 0 => new Tile(v1 + v2, s1 + s2),
                var ((v1, s1), (v2, s2)) when v1 > 0 => new Tile(v1 + v2, s1 + s2 + Math.Min(s1, s2)),
                var ((_, s1), (_, s2)) => new Tile(0, Math.Max(s1, s2)),
            };
        }
    }
}
