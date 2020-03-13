using System;

namespace NeinTile
{
    public class TilesDeck
    {
        private static readonly TileInfo[] emptyTiles = Array.Empty<TileInfo>();

        private readonly ITilesDeckMixer mixer;
        private readonly ITilesDeckLottery lottery;

        private readonly TileInfo[] tiles;

        public TilesDeck(ITilesDeckMixer mixer, ITilesDeckLottery lottery)
            : this(mixer, lottery, emptyTiles)
        {
        }

        private TilesDeck(ITilesDeckMixer mixer, ITilesDeckLottery lottery, TileInfo[] tiles)
        {
            this.mixer = mixer ?? throw new ArgumentNullException(nameof(mixer));
            this.lottery = lottery ?? throw new ArgumentNullException(nameof(lottery));

            this.tiles = tiles.Length == 0 ? mixer.Tiles : tiles;
        }

        public int Size
            => tiles.Length;

        public TileInfo this[int index]
            => tiles[index];

        public virtual TileSample Preview()
            => lottery.Sample != TileSample.Empty ? lottery.Sample : new TileSample(tiles[0]);

        public virtual TilesDeck Draw(out TileInfo nextTile)
        {
            TileInfo[] nextTiles;

            (nextTile, nextTiles) = lottery.Bonus == TileInfo.Empty
                ? (tiles[0], tiles[1..])
                : (lottery.Bonus, tiles);

            return new TilesDeck(mixer.Shuffle(), lottery.Draw(), nextTiles);
        }
    }
}
