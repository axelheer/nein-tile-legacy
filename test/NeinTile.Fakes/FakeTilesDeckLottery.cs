using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeckLottery : ITilesDeckLottery
    {
        public Func<TileInfo[], (TileSample, TileInfo)> OnDraw { get; set; }
            = _ => (TileSample.Empty, TileInfo.Empty);

        public TileSample Draw(TileInfo[] tiles, out TileInfo bonus)
        {
            TileSample sample;
            (sample, bonus) = OnDraw(tiles);
            return sample;
        }

        public Func<ITilesDeckLottery> OnCreateNext { get; set; }
            = () => new FakeTilesDeckLottery();

        public ITilesDeckLottery CreateNext()
            => OnCreateNext();
    }
}
