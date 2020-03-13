using System;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaMixer : ITilesAreaMixer
    {
        public TileInfo[,,] Tiles { get; set; }
            = new TileInfo[0, 0, 0];

        public Func<ITilesAreaMixer> OnShuffle { get; set; }
            = () => new FakeTilesAreaMixer();

        public ITilesAreaMixer Shuffle()
            => OnShuffle();
    }
}
