using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public class DualityTilesAreaMerger : ITilesAreaMerger
    {
        public bool CanMerge(TileInfo source, TileInfo target)
            => source != TileInfo.Empty && (target == TileInfo.Empty || source.Value == target.Value);

        public TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder)
        {
            remainder = default;
            return (source, target) switch
            {
                (_, (0, 0)) => source,
                var ((v, s), _) when v < 0 => new TileInfo(v * 2, s * 2),
                var ((v, s), _) when v > 0 => new TileInfo(v * 2, s * 3),
                var ((_, s1), (_, s2)) => new TileInfo(0, s1 + s2)
            };
        }
    }
}
