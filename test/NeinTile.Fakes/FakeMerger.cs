using System;
using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public class FakeMerger : IMerger
    {
        public Func<Tile, Tile, bool> OnCanMerge { get; set; }
            = (_, __) => false;

        public bool CanMerge(Tile source, Tile target)
            => OnCanMerge(source, target);

        public Func<Tile, Tile, Tile> OnMerge { get; set; }
            = (_, __) => Tile.Empty;

        public Tile Merge(Tile source, Tile target)
            => OnMerge(source, target);
    }
}
