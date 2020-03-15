using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeckLottery : ITilesDeckLottery
    {
        public Func<(TileSample, TileInfo)> OnDraw { get; set; }
            = () => (TileSample.Empty, TileInfo.Empty);

        public TileSample Draw(out TileInfo bonus)
        {
            TileSample sample;
            (sample, bonus) = OnDraw();
            return sample;
        }

        public Func<TilesArea, ITilesDeckLottery> OnCreateNext { get; set; }
            =_ => new FakeTilesDeckLottery();

        public ITilesDeckLottery CreateNext(TilesArea area)
            => OnCreateNext(area);
    }
}
