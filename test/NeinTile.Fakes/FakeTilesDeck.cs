using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeck : TilesDeck
    {
        public FakeTilesDeck()
            : base(new FakeTilesDeckMixer(), new FakeTilesDeckLottery())
        {
        }

        public Func<TileSample> OnHint { get; set; }
            = () => TileSample.Empty;

        public override TileSample Hint()
            => OnHint();

        public Func<TileInfo> OnShow { get; set; }
            = () => TileInfo.Empty;

        public override TileInfo Show()
            => OnShow();

        public Func<TilesArea?, TilesDeck> OnDraw { get; set; }
            = _ => new FakeTilesDeck();

        public override TilesDeck Draw(TilesArea? area)
            => OnDraw(area);
    }
}
