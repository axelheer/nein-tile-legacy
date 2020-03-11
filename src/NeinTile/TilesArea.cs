using System;

namespace NeinTile
{
    public class TilesArea
    {
        private static readonly TileInfo[,,] emptyTiles = new TileInfo[0, 0, 0];

        private readonly ITilesAreaMixer mixer;
        private readonly ITilesAreaMerger merger;

        private readonly TileInfo[,,] tiles;

        public TilesArea(ITilesAreaMixer mixer, ITilesAreaMerger merger)
            : this(mixer, merger, emptyTiles)
        {
        }

        private TilesArea(ITilesAreaMixer mixer, ITilesAreaMerger merger, TileInfo[,,] tiles)
        {
            this.mixer = mixer ?? throw new ArgumentNullException(nameof(mixer));
            this.merger = merger ?? throw new ArgumentNullException(nameof(merger));

            this.tiles = tiles.Length == 0 ? mixer.Shuffle() : tiles;
        }

        public int ColCount
            => tiles.GetLength(1);

        public int RowCount
            => tiles.GetLength(0);

        public int LowCount
            => tiles.GetLength(2);

        public TileInfo this[int rowIndex, int columnIndex, int layerIndex]
            => tiles[rowIndex, columnIndex, layerIndex];

        public virtual bool CanMove(MoveDirection direction)
        {
            using var enumerator = new MoveEnumerator(tiles, direction);
            while (enumerator.MoveNext())
            {
                var (tile, other) = enumerator.Current;
                if (merger.CanMerge(tile, other))
                    return true;
            }
            return false;
        }

        public virtual TilesArea Move(MoveDirection direction, TileInfo nextTile)
        {
            throw new NotImplementedException();
        }
    }
}
