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
            return new TileInfo(source.Value + target.Value, source.Score + target.Score + target.Score);
        }
    }
}
