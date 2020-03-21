using System;
using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaMerger : ITilesAreaMerger
    {
        public Func<TileInfo, TileInfo, bool> OnCanMerge { get; set; }
            = (_, __) => false;

        public bool CanMerge(TileInfo source, TileInfo target)
            => OnCanMerge(source, target);

        public Func<TileInfo, TileInfo, (TileInfo, TileInfo)> OnMerge { get; set; }
            = (_, __) => (TileInfo.Empty, TileInfo.Empty);

        public TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder)
        {
            TileInfo result;
            (result, remainder) = OnMerge(source, target);
            return result;
        }
    }
}
