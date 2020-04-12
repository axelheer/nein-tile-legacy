using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class SimpleTilesDeckMixer : ITilesDeckMixer
    {
        private readonly TileInfo[] tiles = new[]
        {
            new TileInfo(2, 2)
        };

        public TileInfo[] Shuffle()
            => tiles;

        public ITilesDeckMixer CreateNext()
            => this;
    }
}
