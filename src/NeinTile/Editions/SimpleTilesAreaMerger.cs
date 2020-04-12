using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public class SimpleTilesAreaMerger : ITilesAreaMerger
    {
        public bool CanMerge(TileInfo source, TileInfo target)
            => source != TileInfo.Empty && (target == TileInfo.Empty || source.Value == target.Value);

        public TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder)
        {
            remainder = default;
            return target != TileInfo.Empty
                ? new TileInfo(source.Value * 2, source.Score * 3)
                : source;
        }
    }
}
