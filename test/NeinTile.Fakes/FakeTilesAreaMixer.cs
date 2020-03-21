using System;
using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaMixer : ITilesAreaMixer
    {
        public Func<TileInfo, bool> OnAddNext { get; set; }
            = _ => false;

        public bool AddNext(TileInfo nextTile)
            => OnAddNext(nextTile);

        public Func<TileInfo[,,]> OnShuffle { get; set; }
            = () => new TileInfo[0, 0, 0];

        public TileInfo[,,] Shuffle()
            => OnShuffle();
    }
}
