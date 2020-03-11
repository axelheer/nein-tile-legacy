using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeck : TilesDeck
    {
        public FakeTilesDeck()
            : base(new FakeTilesDeckMixer(), new FakeTilesDeckLottery())
        {
        }

        public Func<TileSample> OnPreview { get; set; }
            = () => TileSample.Empty;

        public override TileSample Preview()
            => OnPreview();

        public Func<(TilesDeck, TileInfo)> OnDraw { get; set; }
            = () => (new FakeTilesDeck(), TileInfo.Empty);

        public override TilesDeck Draw(out TileInfo nextTile)
        {
            TilesDeck result;
            (result, nextTile) = OnDraw();
            return result;
        }
    }
}
