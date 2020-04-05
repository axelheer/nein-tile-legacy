using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public class ClassicTilesAreaMerger : ITilesAreaMerger
    {
        public bool CanMerge(TileInfo source, TileInfo target)
        {
            return target == TileInfo.Empty || (source.Value, target.Value) switch
            {
                (1, 1) => false,
                (1, 2) => true,
                (2, 1) => true,
                (2, 2) => false,
                var (s, t) => s == t
            };
        }

        public TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder)
        {
            remainder = default;
            return (source, target) switch
            {
                (_, (0, 0)) => source,
                ((1, 0), (2, 0)) => new TileInfo(3, 3),
                ((2, 0), (1, 0)) => new TileInfo(3, 3),
                var ((v, s), _) => new TileInfo(v * 2, s * 3)
            };
        }
    }
}
