using System;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaMerger : ITilesAreaMerger
    {
        public Func<TileInfo, TileInfo, bool> OnCanMerge { get; set; }
            = (_, __) => false;

        public bool CanMerge(TileInfo tile, TileInfo other)
            => OnCanMerge(tile, other);

        public Func<TileInfo, TileInfo, TileInfo> OnMerge { get; set; }
            = (_, __) => TileInfo.Empty;

        public TileInfo Merge(TileInfo tile, TileInfo other)
            => OnMerge(tile, other);
    }
}
