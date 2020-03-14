using System;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaMixer : ITilesAreaMixer
    {
        public Func<TileInfo[,,]> OnShuffle { get; set; }
            = () => new TileInfo[0, 0, 0];

        public TileInfo[,,] Shuffle()
            => OnShuffle();
    }
}
