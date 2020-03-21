using NeinTile.Abstractions;

namespace NeinTile.Rules
{
    public class SimpleTilesAreaMerger : ITilesAreaMerger
    {
        public bool CanMerge(TileInfo source, TileInfo target)
            => source.Value == target.Value;

        public TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder)
        {
            remainder = default;
            return new TileInfo(source.Value * 2, source.Score * 3);
        }
    }
}
