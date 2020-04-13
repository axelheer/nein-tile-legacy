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
                var ((v1, s1), (v2, s2)) when v1 > 0 => new TileInfo(v1 + v2, s1 + s2 + s2),
                var ((v1, s1), (v2, s2)) => new TileInfo(v1 + v2, s1 + s2)
            };
        }
    }
}
