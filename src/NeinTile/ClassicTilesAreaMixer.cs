using System;

namespace NeinTile
{
    public class ClassicTilesAreaMixer : ITilesAreaMixer
    {
        private readonly TilesDeck deck;
        private readonly TileInfo[,,] tiles;

        public ClassicTilesAreaMixer(TilesDeck initialDeck, int colCount, int rowCount, int layCount)
        {
            if (initialDeck is null)
                throw new ArgumentNullException(nameof(initialDeck));

            var random = new Random();
            tiles = new TileInfo[colCount, rowCount, layCount];
            for (var i = 0; i < tiles.Length; i += 2)
            {
                var (colIndex, rowIndex, layIndex)
                    = (random.Next(colCount), random.Next(rowCount), random.Next(layCount));
                while (tiles[colIndex, rowIndex, layIndex] != TileInfo.Empty)
                    (colIndex, rowIndex, layIndex)
                    = (random.Next(colCount), random.Next(rowCount), random.Next(layCount));
                tiles[colIndex, rowIndex, layIndex] = initialDeck.Show();
                initialDeck = initialDeck.Draw();
            }
            deck = initialDeck;
        }

        public TileInfo[,,] Shuffle()
            => tiles;

        public TilesDeck CreateDeck()
            => deck;
    }
}
