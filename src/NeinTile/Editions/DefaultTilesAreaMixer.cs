using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DefaultTilesAreaMixer : ITilesAreaMixer
    {
        private readonly Random random = new Random();

        private readonly TileInfo[,,] tiles;

        private readonly int colCount;
        private readonly int rowCount;
        private readonly int layCount;

        private int iteration;

        public DefaultTilesAreaMixer(GameOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            colCount = options.ColCount;
            rowCount = options.RowCount;
            layCount = options.LayCount;

            tiles = new TileInfo[colCount, rowCount, layCount];
        }

        private bool IsMixing
            => iteration < tiles.Length;

        public TileInfo[,,]? Shuffle()
            => !IsMixing ? tiles : null;

        public bool AddNext(TileInfo nextTile)
        {
            if (IsMixing)
            {
                var (colIndex, rowIndex, layIndex) = (0, 0, 0);
                while (tiles[colIndex, rowIndex, layIndex] != TileInfo.Empty)
                {
                    (colIndex, rowIndex, layIndex)
                        = (random.Next(colCount), random.Next(rowCount), random.Next(layCount));
                }
                tiles[colIndex, rowIndex, layIndex] = nextTile;
                iteration += 2;
            }
            return IsMixing;
        }
    }
}
