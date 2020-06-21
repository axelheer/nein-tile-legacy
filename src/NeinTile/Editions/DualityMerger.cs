using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class DualityMerger : IMerger
    {
        public bool CanMerge(Tile source, Tile target)
        {
            return (source.Value, target.Value) switch
            {
                (0, _) when source == Tile.Empty => false,
                (_, 0) when target == Tile.Empty => true,
                var (s, t) => s == t || s + t == 0
            };
        }

        public Tile Merge(Tile source, Tile target)
        {
            return (source, target) switch
            {
                (_, (0, 0)) => source,
                var ((v1, s1), (v2, s2)) when v1 < 0 => new Tile(v1 + v2, s1 + s2 - v1 - v2),
                var ((v1, s1), (v2, s2)) when v1 > 0 => new Tile(v1 + v2, s1 + s2 + v1 + v2),
                var ((_, s1), (_, s2)) => new Tile(0, Math.Max(s1, s2))
            };
        }
    }
}
