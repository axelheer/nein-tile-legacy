using System;
using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public class FakeTilesDeckMixer : ITilesDeckMixer
    {
        public Func<TileInfo[]> OnShuffle { get; set; }
            = () => Array.Empty<TileInfo>();

        public TileInfo[] Shuffle()
            => OnShuffle();

        public Func<ITilesDeckMixer> OnCreateNext { get; set; }
            = () => new FakeTilesDeckMixer();

        public ITilesDeckMixer CreateNext()
            => OnCreateNext();
    }
}
