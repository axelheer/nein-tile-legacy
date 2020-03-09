using System;

namespace NeinTile
{
    public abstract class TilesArea
    {
        private readonly TileInfo[,,] tiles;

        protected TilesArea(TileInfo[,,] tiles)
            => this.tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));

        public int RowCount
            => tiles.GetLength(1);

        public int ColumnCount
            => tiles.GetLength(2);

        public int LayerCount
            => tiles.GetLength(3);

        public TileInfo this[int rowIndex, int columnIndex, int layerIndex]
            => tiles[rowIndex, columnIndex, layerIndex];

        public abstract bool CanMove(MoveDirection direction);

        public abstract TilesArea Move(MoveDirection direction, TileInfo nextTile);
    }
}
