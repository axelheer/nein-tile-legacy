using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeckMixer : ITilesDeckMixer
    {
        public TileInfo[] Tiles { get; set; }
            = new TileInfo[0];

        public Func<ITilesDeckMixer> OnShuffle { get; set; }
            = () => new FakeTilesDeckMixer();

        public ITilesDeckMixer Shuffle()
            => OnShuffle();
    }
}
