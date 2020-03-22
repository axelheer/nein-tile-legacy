using NeinTile.Abstractions;

namespace NeinTile.Editions
{
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
