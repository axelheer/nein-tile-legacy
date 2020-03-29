using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public class SimpleTilesAreaMerger : ITilesAreaMerger
    {
        public bool CanMerge(TileInfo source, TileInfo target)
            => target == TileInfo.Empty || source.Value == target.Value;

        public TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder)
        {
            remainder = default;
            return target != TileInfo.Empty
                ? new TileInfo(source.Value * 2, source.Score * 3)
                : source;
        }
    }
}
