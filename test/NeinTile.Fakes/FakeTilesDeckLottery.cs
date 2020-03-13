using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeckLottery : ITilesDeckLottery
    {
        public TileInfo Bonus { get; set; }
            = TileInfo.Empty;

        public TileSample Sample { get; set; }
            = TileSample.Empty;

        public Func<ITilesDeckLottery> OnDraw { get; set; }
            = () => new FakeTilesDeckLottery();

        public ITilesDeckLottery Draw()
            => OnDraw();
    }
}
