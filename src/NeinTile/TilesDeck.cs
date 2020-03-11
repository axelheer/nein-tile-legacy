using System;

namespace NeinTile
{
    public class TilesDeck
    {
        private readonly ITilesDeckMixer mixer;
        private readonly ITilesDeckLottery lottery;

        private readonly TileInfo[] tiles;
        private readonly TileSample sample;
        private readonly TileInfo bonus;

        public TilesDeck(ITilesDeckMixer mixer, ITilesDeckLottery lottery)
            : this(mixer, lottery, Array.Empty<TileInfo>())
        {
        }

        private TilesDeck(ITilesDeckMixer mixer, ITilesDeckLottery lottery, TileInfo[] tiles)
        {
            this.mixer = mixer ?? throw new ArgumentNullException(nameof(mixer));
            this.lottery = lottery ?? throw new ArgumentNullException(nameof(lottery));

            this.tiles = tiles.Length == 0 ? mixer.Shuffle() : tiles;
            sample = lottery.Draw(out bonus);
        }

        public int Size
            => tiles.Length;

        public virtual TileSample Preview()
            => sample != TileSample.Empty ? sample : new TileSample(tiles[0]);

        public virtual TilesDeck Draw(out TileInfo nextTile)
        {
            TileInfo[] nextTiles;

            (nextTile, nextTiles) = bonus == TileInfo.Empty
                ? (tiles[0], tiles[..^1])
                : (bonus, tiles);

            return new TilesDeck(mixer, lottery, nextTiles);
        }
    }
}
