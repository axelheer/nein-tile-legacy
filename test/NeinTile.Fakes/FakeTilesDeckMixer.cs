using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeckMixer : ITilesDeckMixer
    {
        public Func<TileInfo[]> OnShuffle { get; set; } = () => Array.Empty<TileInfo>();

        public TileInfo[] Shuffle()
        {
            return OnShuffle();
        }
    }
}
