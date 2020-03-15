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

        public bool IsBonus
            => bonus != TileInfo.Empty;

        public int Size
            => tiles.Length;

        public TileInfo this[int index]
            => tiles[index];

        public virtual TileSample Hint()
            => IsBonus ? sample : new TileSample(tiles[0]);

        public virtual TileInfo Show()
            => IsBonus ? bonus : tiles[0];

        public virtual TilesDeck Draw(TilesArea? area)
            => new TilesDeck(mixer.CreateNext(), lottery.CreateNext(area), IsBonus ? tiles : tiles[1..]);
    }
}
