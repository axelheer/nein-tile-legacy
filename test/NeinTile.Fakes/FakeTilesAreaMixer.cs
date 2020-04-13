using System;
using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaMixer : ITilesAreaMixer
    {
        public TileInfo[,,] Tiles { get; set; }
            = new TileInfo[0, 0, 0];

        public Func<TileInfo, bool> OnAddNext { get; set; }
            = _ => false;

        public bool AddNext(TileInfo nextTile)
            => OnAddNext(nextTile);
    }
}
