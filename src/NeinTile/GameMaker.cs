using NeinTile.Abstractions;

namespace NeinTile
{
    public sealed class GameMaker
    {
        public IMaker? Maker { get; set; } = null;

        public int ColCount { get; set; } = 4;
        public int RowCount { get; set; } = 4;
        public int LayCount { get; set; } = 1;

        public GameEdition Edition { get; set; } = GameEdition.Simple;

        public bool Slippery { get; set; } = true;

        public Game MakeGame()
        {
            var maker = Maker ?? Edition.Maker();
            var tiles = new Tiles(ColCount, RowCount, LayCount);
            var deck = new TilesDeck(maker.MakeMixer(), maker.MakeLottery());
            var dealer = maker.MakeDealer();
            foreach (var index in dealer.Part(tiles.Indices))
            {
                tiles[index] = deck.Tile;
                deck = deck.Next(0);
            }
            var area = new TilesArea(tiles, dealer, maker.MakeMerger());
            return new Game(deck, area, Slippery);
        }
    }
}
