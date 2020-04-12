using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public class InsanityTilesAreaMerger : ITilesAreaMerger
    {
        public bool CanMerge(TileInfo source, TileInfo target)
        {
            return source != TileInfo.Empty && (target == TileInfo.Empty || (source.Value, target.Value) switch
            {
                (-2, -2) => false,
                (-2, -1) => true,
                (-1, -2) => true,
                (-1, -1) => false,
                (1, 1) => false,
                (1, 2) => true,
                (2, 1) => true,
                (2, 2) => false,
                var (s, t) => s == t
            });
        }

        public TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder)
        {
            remainder = default;
            return (source, target) switch
            {
                (_, (0, 0)) => source,
                ((-2, 0), (-1, 0)) => new TileInfo(-3, 3),
                ((-1, 0), (-2, 0)) => new TileInfo(-3, 3),
                ((1, 0), (2, 0)) => new TileInfo(3, 3),
                ((2, 0), (1, 0)) => new TileInfo(3, 3),
                var ((v, s), _) when v < 0 => new TileInfo(v * 2, s * 2),
                var ((v, s), _) when v > 0 => new TileInfo(v * 2, s * 3),
                var ((_, s1), (_, s2)) => new TileInfo(0, Math.Max(s1, s2))
            };
        }
    }
}
