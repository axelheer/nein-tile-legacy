using System;

namespace NeinTile
{
    public abstract class TilesDeck
    {
        private readonly TileInfo[] tiles;

        protected TilesDeck(TileInfo[] tiles)
            => this.tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));

        public abstract TileInfo[] Preview();

        public abstract TilesDeck Draw(out TileInfo tile);
    }
}
