namespace NeinTile
{
    public class ClassicTilesAreaMerger : ITilesAreaMerger
    {
        public bool CanMerge(TileInfo source, TileInfo target)
        {
            return (source.Value, target.Value) switch
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
                ((1, _), (2, _)) => new TileInfo(3, 3),
                ((2, _), (1, _)) => new TileInfo(3, 3),
                var ((v, s), _) => new TileInfo(v * 2, s * 3)
            };
        }
    }
}
